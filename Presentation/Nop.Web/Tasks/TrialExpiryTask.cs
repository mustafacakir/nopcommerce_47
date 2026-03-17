using System;
using System.Threading.Tasks;
using Nop.Core.Domain.Messages;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Messages;
using Nop.Services.ScheduleTasks;
using Nop.Services.Stores;

namespace Nop.Web.Tasks
{
    public class TrialExpiryTask : IScheduleTask
    {
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreService _storeService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IEmailAccountService _emailAccountService;

        public TrialExpiryTask(
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IStoreService storeService,
            IQueuedEmailService queuedEmailService,
            IEmailAccountService emailAccountService)
        {
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _storeService = storeService;
            _queuedEmailService = queuedEmailService;
            _emailAccountService = emailAccountService;
        }

        public async Task ExecuteAsync()
        {
            var storeOwnerRole = await _customerService.GetCustomerRoleBySystemNameAsync("StoreOwner");
            if (storeOwnerRole == null) return;

            var customers = await _customerService.GetAllCustomersAsync(
                customerRoleIds: new[] { storeOwnerRole.Id });

            var emailAccounts = await _emailAccountService.GetAllEmailAccountsAsync();
            var emailAccount = emailAccounts.Count > 0 ? emailAccounts[0] : null;

            foreach (var customer in customers)
            {
                var status = await _genericAttributeService.GetAttributeAsync<string>(customer, "SubscriptionStatus");
                if (status != "trial") continue;

                var trialEndDate = await _genericAttributeService.GetAttributeAsync<DateTime?>(customer, "TrialEndDate");
                if (trialEndDate == null || DateTime.UtcNow <= trialEndDate.Value) continue;

                // Trial süresi dolmuş — durumu güncelle
                await _genericAttributeService.SaveAttributeAsync(customer, "SubscriptionStatus", "suspended");

                // Admin'e bildirim gönder
                if (emailAccount != null)
                {
                    var storeId = await _genericAttributeService.GetAttributeAsync<int>(customer, "OwnedStoreId");
                    var store = storeId > 0 ? await _storeService.GetStoreByIdAsync(storeId) : null;
                    var storeName = store?.Name ?? "Bilinmeyen Mağaza";

                    await _queuedEmailService.InsertQueuedEmailAsync(new QueuedEmail
                    {
                        From = emailAccount.Email,
                        FromName = emailAccount.DisplayName,
                        To = "bilgi@pekinteknoloji.com",
                        ToName = "Pekin Teknoloji",
                        Subject = $"Trial Süresi Doldu: {storeName}",
                        Body = $@"<p>Merhaba,</p>
<p><strong>{storeName}</strong> mağazasının trial süresi dolmuştur.</p>
<ul>
  <li><strong>Müşteri:</strong> {customer.Email}</li>
  <li><strong>Mağaza:</strong> {storeName}</li>
  <li><strong>Trial Bitiş:</strong> {trialEndDate.Value:dd.MM.yyyy}</li>
</ul>
<p>Müşteri ile iletişime geçilmesi önerilir.</p>",
                        EmailAccountId = emailAccount.Id,
                        Priority = QueuedEmailPriority.High,
                        CreatedOnUtc = DateTime.UtcNow
                    });
                }
            }
        }
    }
}
