using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Misc.SimpleCheckout.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Logging;
using Nop.Services.Configuration;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Web.Controllers;
using Nop.Web.Framework.Controllers;
using Nop.Core.Domain.Discounts;

namespace Nop.Plugin.Misc.SimpleCheckout.Controllers;

public class SimpleCheckoutController : BasePublicController
{
    private readonly IWorkContext _workContext;
    private readonly IStoreContext _storeContext;
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IProductService _productService;
    private readonly IOrderTotalCalculationService _orderTotalCalculationService;
    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;
    private readonly ICountryService _countryService;
    private readonly IGenericAttributeService _genericAttributeService;
    private readonly IOrderProcessingService _orderProcessingService;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentPluginManager _paymentPluginManager;
    private readonly IPriceFormatter _priceFormatter;
    private readonly ILogger _logger;
    private readonly IWebHelper _webHelper;
    private readonly ISettingService _settingService;

    public SimpleCheckoutController(
        IWorkContext workContext,
        IStoreContext storeContext,
        IShoppingCartService shoppingCartService,
        IProductService productService,
        IOrderTotalCalculationService orderTotalCalculationService,
        ICustomerService customerService,
        IAddressService addressService,
        ICountryService countryService,
        IGenericAttributeService genericAttributeService,
        IOrderProcessingService orderProcessingService,
        IPaymentService paymentService,
        IPaymentPluginManager paymentPluginManager,
        IPriceFormatter priceFormatter,
        ILogger logger,
        IWebHelper webHelper,
        ISettingService settingService)
    {
        _workContext = workContext;
        _storeContext = storeContext;
        _shoppingCartService = shoppingCartService;
        _productService = productService;
        _orderTotalCalculationService = orderTotalCalculationService;
        _customerService = customerService;
        _addressService = addressService;
        _countryService = countryService;
        _genericAttributeService = genericAttributeService;
        _orderProcessingService = orderProcessingService;
        _paymentService = paymentService;
        _paymentPluginManager = paymentPluginManager;
        _priceFormatter = priceFormatter;
        _logger = logger;
        _webHelper = webHelper;
        _settingService = settingService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        var store = await _storeContext.GetCurrentStoreAsync();
        var cart = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart, store.Id);

        if (!cart.Any())
            return RedirectToRoute("ShoppingCart");

        var (useEft, eftDescription) = await IsEftModeAsync(customer, store);
        var model = new SimpleCheckoutModel
        {
            FirstName      = customer.FirstName ?? string.Empty,
            LastName       = customer.LastName  ?? string.Empty,
            Phone          = customer.Phone     ?? string.Empty,
            Email          = IsRealEmail(customer.Email) ? customer.Email : string.Empty,
            UseEft         = useEft,
            EftDescription = eftDescription
        };

        await PrepareCartAsync(model, customer, store, cart);
        return View("~/Plugins/Misc.SimpleCheckout/Views/Index.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(SimpleCheckoutModel model)
    {
        var customer = await _workContext.GetCurrentCustomerAsync();
        var store    = await _storeContext.GetCurrentStoreAsync();
        var cart     = await _shoppingCartService.GetShoppingCartAsync(customer, ShoppingCartType.ShoppingCart, store.Id);

        if (!cart.Any())
            return RedirectToRoute("ShoppingCart");

        if (!ModelState.IsValid)
        {
            await PrepareCartAsync(model, customer, store, cart);
            return View("~/Plugins/Misc.SimpleCheckout/Views/Index.cshtml", model);
        }

        try
        {
            // Update customer email
            if (!IsRealEmail(customer.Email))
            {
                customer.Email = model.Email;
                await _customerService.UpdateCustomerAsync(customer);
            }

            // Save customer fields
            customer.FirstName = model.FirstName;
            customer.LastName  = model.LastName;
            customer.Phone     = model.Phone;
            await _customerService.UpdateCustomerAsync(customer);

            // Create billing address
            var country = (await _countryService.GetAllCountriesAsync()).FirstOrDefault(c => c.TwoLetterIsoCode == "TR");
            var address = new Address
            {
                FirstName    = model.FirstName,
                LastName     = model.LastName,
                Email        = model.Email,
                PhoneNumber  = model.Phone,
                Address1     = model.Phone,
                CountryId    = country?.Id,
                CreatedOnUtc = DateTime.UtcNow
            };
            await _addressService.InsertAddressAsync(address);
            await _customerService.InsertCustomerAddressAsync(customer, address);
            customer.BillingAddressId  = address.Id;
            customer.ShippingAddressId = address.Id;
            await _customerService.UpdateCustomerAsync(customer);

            // PayTR varsa önce onu kullan, yoksa EFT/havale'ye (CheckMoneyOrder) düş
            var paymentMethods = await _paymentPluginManager.LoadActivePluginsAsync(customer, store.Id);
            var paymentMethod  = paymentMethods.FirstOrDefault(p => p.PluginDescriptor.SystemName == "Payments.PaytrIframe")
                              ?? paymentMethods.FirstOrDefault(p => p.PluginDescriptor.SystemName == "Payments.CheckMoneyOrder");
            if (paymentMethod == null)
                throw new Exception("Bu mağaza için aktif ödeme yöntemi bulunamadı.");

            await _genericAttributeService.SaveAttributeAsync(
                customer,
                NopCustomerDefaults.SelectedPaymentMethodAttribute,
                paymentMethod.PluginDescriptor.SystemName,
                store.Id);

            // Place order
            var processPaymentRequest = new ProcessPaymentRequest();
            await _paymentService.GenerateOrderGuidAsync(processPaymentRequest);
            processPaymentRequest.StoreId                  = store.Id;
            processPaymentRequest.CustomerId               = customer.Id;
            processPaymentRequest.PaymentMethodSystemName  = paymentMethod.PluginDescriptor.SystemName;

            var result = await _orderProcessingService.PlaceOrderAsync(processPaymentRequest);

            if (result.Success)
            {
                var postPaymentRequest = new PostProcessPaymentRequest { Order = result.PlacedOrder };
                await _paymentService.PostProcessPaymentAsync(postPaymentRequest);

                if (_webHelper.IsRequestBeingRedirected || _webHelper.IsPostBeingDone)
                    return new EmptyResult();

                return RedirectToRoute("CheckoutCompleted", new { orderId = result.PlacedOrder.Id });
            }

            foreach (var error in result.Errors)
                model.Errors.Add(error);
        }
        catch (Exception ex)
        {
            await _logger.ErrorAsync("SimpleCheckout: " + ex.Message, ex);
            model.Errors.Add(ex.Message);
        }

        (model.UseEft, model.EftDescription) = await IsEftModeAsync(customer, store);
        await PrepareCartAsync(model, customer, store, cart);
        return View("~/Plugins/Misc.SimpleCheckout/Views/Index.cshtml", model);
    }

    private async Task<(bool UseEft, string EftDescription)> IsEftModeAsync(Customer customer, Nop.Core.Domain.Stores.Store store)
    {
        var methods = await _paymentPluginManager.LoadActivePluginsAsync(customer, store.Id);
        var hasPaytr = methods.Any(p => p.PluginDescriptor.SystemName == "Payments.PaytrIframe");
        var hasEft   = methods.Any(p => p.PluginDescriptor.SystemName == "Payments.CheckMoneyOrder");
        if (!hasPaytr && hasEft)
        {
            var settings = await _settingService.LoadSettingAsync<SimpleCheckoutSettings>();
            return (true, settings.EftDescription);
        }
        return (false, null);
    }

    private async Task PrepareCartAsync(SimpleCheckoutModel model, Customer customer, Nop.Core.Domain.Stores.Store store, IList<ShoppingCartItem> cart)
    {
        foreach (var item in cart)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null) continue;

            var (subTotal, _, _, _) = await _shoppingCartService.GetSubTotalAsync(item, true);
            model.CartItems.Add(new CartItemModel
            {
                ProductName      = product.Name,
                Quantity         = item.Quantity,
                SubTotalFormatted = await _priceFormatter.FormatPriceAsync(subTotal)
            });
        }

        var (orderTotal, _, _, _, _, _) = await _orderTotalCalculationService.GetShoppingCartTotalAsync(cart, usePaymentMethodAdditionalFee: false);
        model.OrderTotalFormatted = await _priceFormatter.FormatPriceAsync(orderTotal ?? 0);
    }

    private static bool IsRealEmail(string email) =>
        !string.IsNullOrEmpty(email) && email.Contains('@') && !email.Contains("@nopCommerce.com");
}
