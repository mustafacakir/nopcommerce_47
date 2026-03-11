using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Framework.Controllers;
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

        public FastRegisterController(
            ICustomerService customerService,
            ICustomerRegistrationService customerRegistrationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            CustomerSettings customerSettings)
        {
            _customerService = customerService;
            _customerRegistrationService = customerRegistrationService;
            _workContext = workContext;
            _storeContext = storeContext;
            _customerSettings = customerSettings;
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

        public class FastRegisterModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
        }
    }
}
