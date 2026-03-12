using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Core.Domain.Messages;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;
using System;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [EnableCors("AllowMarketingSite")]
    public class FastRegisterController : BasePublicController
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly CustomerSettings _customerSettings;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IQueuedEmailService _queuedEmailService;

        public FastRegisterController(
            ICustomerService customerService,
            ICustomerRegistrationService customerRegistrationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            CustomerSettings customerSettings,
            IEmailAccountService emailAccountService,
            IQueuedEmailService queuedEmailService)
        {
            _customerService = customerService;
            _customerRegistrationService = customerRegistrationService;
            _workContext = workContext;
            _storeContext = storeContext;
            _customerSettings = customerSettings;
            _emailAccountService = emailAccountService;
            _queuedEmailService = queuedEmailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] FastRegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest(new { success = false, message = "Eksik bilgi." });

            var customer = await _workContext.GetCurrentCustomerAsync();
            if (await _customerService.IsRegisteredAsync(customer))
            {
                // Is already registered
                return BadRequest(new { success = false, message = "Zaten üyesiniz." });
            }

            var store = await _storeContext.GetCurrentStoreAsync();

            var registrationRequest = new CustomerRegistrationRequest(
                customer,
                model.Email,
                _customerSettings.UsernamesEnabled ? model.Email : model.Email,
                model.Password,
                _customerSettings.DefaultPasswordFormat,
                store.Id,
                true); // isApproved = true

            var result = await _customerRegistrationService.RegisterCustomerAsync(registrationRequest);

            if (result.Success)
            {
                // Update additional fields
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Phone = model.Phone;
                await _customerService.UpdateCustomerAsync(customer);

                // Sign in the user
                await _customerRegistrationService.SignInCustomerAsync(customer, null, true);

                return Ok(new { success = true, message = "Kayıt başarılı." });
            }

            return BadRequest(new { success = false, errors = result.Errors });
        }

        [HttpPost("consultation")]
        public async Task<IActionResult> Consultation([FromBody] ConsultationModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name))
                return BadRequest(new { success = false, message = "Eksik bilgi." });

            var accounts = await _emailAccountService.GetAllEmailAccountsAsync();
            var emailAccount = accounts.Count > 0 ? accounts[0] : null;

            if (emailAccount == null)
                return StatusCode(500, new { success = false, message = "E-posta hesabı bulunamadı." });

            var body = $@"
                <h2>Yeni Danışmanlık Talebi</h2>
                <table cellpadding='6' style='font-family:sans-serif;font-size:14px'>
                    <tr><td><b>Ad Soyad:</b></td><td>{model.Name}</td></tr>
                    <tr><td><b>E-posta:</b></td><td>{model.Email}</td></tr>
                    <tr><td><b>Telefon:</b></td><td>{model.Phone}</td></tr>
                    <tr><td><b>Konu:</b></td><td>{model.Subject}</td></tr>
                    <tr><td><b>Mesaj:</b></td><td>{model.Message}</td></tr>
                </table>";

            await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
            {
                Priority = QueuedEmailPriority.High,
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = "bilgi@pekinteknoloji.com",
                ToName = "Pekin Teknoloji",
                ReplyTo = model.Email,
                ReplyToName = model.Name,
                Subject = $"Danışmanlık Talebi: {model.Subject} — {model.Name}",
                Body = body,
                CreatedOnUtc = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            });

            return Ok(new { success = true });
        }

        public class FastRegisterModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
        }

        public class ConsultationModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
        }
    }
}
