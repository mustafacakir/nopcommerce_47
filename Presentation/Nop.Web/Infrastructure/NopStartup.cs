using Nop.Core.Infrastructure;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Filters;
using Nop.Web.Areas.Admin.Helpers;
using Nop.Web.Framework.Factories;
using Nop.Web.Infrastructure.Installation;
using Nop.Web.Infrastructure.Services;

namespace Nop.Web.Infrastructure;

/// <summary>
/// Represents the registering services on application startup
/// </summary>
public partial class NopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //installation localization service
        services.AddScoped<IInstallationLocalizationService, InstallationLocalizationService>();

        //common factories
        services.AddScoped<IAclSupportedModelFactory, AclSupportedModelFactory>();
        services.AddScoped<IDiscountSupportedModelFactory, DiscountSupportedModelFactory>();
        services.AddScoped<ILocalizedModelFactory, LocalizedModelFactory>();
        services.AddScoped<IStoreMappingSupportedModelFactory, StoreMappingSupportedModelFactory>();

        //admin factories
        services.AddScoped<IBaseAdminModelFactory, TenantBaseAdminModelFactory>();
        services.AddScoped<IActivityLogModelFactory, ActivityLogModelFactory>();
        services.AddScoped<IAddressModelFactory, AddressModelFactory>();
        services.AddScoped<IAddressAttributeModelFactory, AddressAttributeModelFactory>();
        services.AddScoped<IAffiliateModelFactory, AffiliateModelFactory>();
        services.AddScoped<IBlogModelFactory, BlogModelFactory>();
        services.AddScoped<ICampaignModelFactory, CampaignModelFactory>();
        services.AddScoped<ICategoryModelFactory, CategoryModelFactory>();
        services.AddScoped<ICheckoutAttributeModelFactory, CheckoutAttributeModelFactory>();
        services.AddScoped<ICommonModelFactory, CommonModelFactory>();
        services.AddScoped<ICountryModelFactory, CountryModelFactory>();
        services.AddScoped<ICurrencyModelFactory, CurrencyModelFactory>();
        services.AddScoped<ICustomerAttributeModelFactory, CustomerAttributeModelFactory>();
        services.AddScoped<ICustomerModelFactory, CustomerModelFactory>();
        services.AddScoped<ICustomerRoleModelFactory, CustomerRoleModelFactory>();
        services.AddScoped<IDiscountModelFactory, DiscountModelFactory>();
        services.AddScoped<IEmailAccountModelFactory, EmailAccountModelFactory>();
        services.AddScoped<IExternalAuthenticationMethodModelFactory, ExternalAuthenticationMethodModelFactory>();
        services.AddScoped<IForumModelFactory, ForumModelFactory>();
        services.AddScoped<IGiftCardModelFactory, GiftCardModelFactory>();
        services.AddScoped<IHomeModelFactory, HomeModelFactory>();
        services.AddScoped<ILanguageModelFactory, LanguageModelFactory>();
        services.AddScoped<ILogModelFactory, LogModelFactory>();
        services.AddScoped<IManufacturerModelFactory, ManufacturerModelFactory>();
        services.AddScoped<IMeasureModelFactory, MeasureModelFactory>();
        services.AddScoped<IMessageTemplateModelFactory, MessageTemplateModelFactory>();
        services.AddScoped<IMultiFactorAuthenticationMethodModelFactory, MultiFactorAuthenticationMethodModelFactory>();
        services.AddScoped<INewsletterSubscriptionModelFactory, NewsletterSubscriptionModelFactory>();
        services.AddScoped<INewsModelFactory, NewsModelFactory>();
        services.AddScoped<IOrderModelFactory, OrderModelFactory>();
        services.AddScoped<IPaymentModelFactory, PaymentModelFactory>();
        services.AddScoped<IPluginModelFactory, PluginModelFactory>();
        services.AddScoped<IPollModelFactory, PollModelFactory>();
        services.AddScoped<IProductModelFactory, ProductModelFactory>();
        services.AddScoped<IProductAttributeModelFactory, TenantProductAttributeModelFactory>();
        services.AddScoped<IProductReviewModelFactory, ProductReviewModelFactory>();
        services.AddScoped<IReportModelFactory, TenantReportModelFactory>();
        services.AddScoped<IQueuedEmailModelFactory, QueuedEmailModelFactory>();
        services.AddScoped<IRecurringPaymentModelFactory, RecurringPaymentModelFactory>();
        services.AddScoped<IReturnRequestModelFactory, ReturnRequestModelFactory>();
        services.AddScoped<IReviewTypeModelFactory, ReviewTypeModelFactory>();
        services.AddScoped<IScheduleTaskModelFactory, ScheduleTaskModelFactory>();
        services.AddScoped<ISecurityModelFactory, SecurityModelFactory>();
        services.AddScoped<ISettingModelFactory, SettingModelFactory>();
        services.AddScoped<IShippingModelFactory, ShippingModelFactory>();
        services.AddScoped<IShoppingCartModelFactory, ShoppingCartModelFactory>();
        services.AddScoped<ISpecificationAttributeModelFactory, SpecificationAttributeModelFactory>();
        services.AddScoped<IStoreModelFactory, StoreModelFactory>();
        services.AddScoped<ITaxModelFactory, TaxModelFactory>();
        services.AddScoped<ITemplateModelFactory, TemplateModelFactory>();
        services.AddScoped<ITopicModelFactory, TopicModelFactory>();
        services.AddScoped<IVendorAttributeModelFactory, VendorAttributeModelFactory>();
        services.AddScoped<IVendorModelFactory, VendorModelFactory>();
        services.AddScoped<Areas.Admin.Factories.IWidgetModelFactory, Areas.Admin.Factories.WidgetModelFactory>();

        //factories
        services.AddScoped<Factories.IAddressModelFactory, Factories.AddressModelFactory>();
        services.AddScoped<Factories.IBlogModelFactory, Factories.BlogModelFactory>();
        services.AddScoped<Factories.ICatalogModelFactory, Factories.CatalogModelFactory>();
        services.AddScoped<Factories.ICheckoutModelFactory, Factories.CheckoutModelFactory>();
        services.AddScoped<Factories.ICommonModelFactory, Factories.CommonModelFactory>();
        services.AddScoped<Factories.ICountryModelFactory, Factories.CountryModelFactory>();
        services.AddScoped<Factories.ICustomerModelFactory, Factories.CustomerModelFactory>();
        services.AddScoped<Factories.IForumModelFactory, Factories.ForumModelFactory>();
        services.AddScoped<Factories.IExternalAuthenticationModelFactory, Factories.ExternalAuthenticationModelFactory>();
        services.AddScoped<Factories.IJsonLdModelFactory, Factories.JsonLdModelFactory>();
        services.AddScoped<Factories.INewsModelFactory, Factories.NewsModelFactory>();
        services.AddScoped<Factories.INewsletterModelFactory, Factories.NewsletterModelFactory>();
        services.AddScoped<Factories.IOrderModelFactory, Factories.OrderModelFactory>();
        services.AddScoped<Factories.IPollModelFactory, Factories.PollModelFactory>();
        services.AddScoped<Factories.IPrivateMessagesModelFactory, Factories.PrivateMessagesModelFactory>();
        services.AddScoped<Factories.IProductModelFactory, Factories.ProductModelFactory>();
        services.AddScoped<Factories.IProfileModelFactory, Factories.ProfileModelFactory>();
        services.AddScoped<Factories.IReturnRequestModelFactory, Factories.ReturnRequestModelFactory>();
        services.AddScoped<Factories.IShoppingCartModelFactory, Factories.ShoppingCartModelFactory>();
        services.AddScoped<Factories.ISitemapModelFactory, Factories.SitemapModelFactory>();
        services.AddScoped<Factories.ITopicModelFactory, Factories.TopicModelFactory>();
        services.AddScoped<Factories.IVendorModelFactory, Factories.VendorModelFactory>();

        //helpers classes
        services.AddScoped<ITinyMceHelper, TinyMceHelper>();

        // PEKIN_CUSTOM: Tenant izolasyonu - admin panelinde her tenant sadece kendi store'unu görür
        services.AddScoped<IStoreService, TenantStoreService>();

        // PEKIN_CUSTOM: StoreOwner filter - listing'lerde DB seviyesinde filtreleme, create/edit'te otomatik store ataması
        services.AddScoped<StoreOwnerFilter>();

        // PEKIN_CUSTOM: CORS - PekinTeknolojiWeb (React) sitesinden gelen isteklere izin ver
        services.AddCors(options =>
        {
            options.AddPolicy("AllowMarketingSite", policy =>
            {
                policy.WithOrigins(
                        "https://pekinteknoloji.com",
                        "https://www.pekinteknoloji.com",
                        "https://test.pekinteknoloji.com",
                        "http://localhost:5173",
                        "http://localhost:5174",
                        "http://localhost:5175",
                        "http://localhost:5176",
                        "http://localhost:5177"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        // PEKIN_CUSTOM: CORS middleware
        application.UseCors("AllowMarketingSite");

        // PEKIN_CUSTOM: Per-store log context ve metrics (Loki + Prometheus)
        application.UseMiddleware<StoreMetricsMiddleware>();
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 3000; // Framework NopStartup (2000)'den sonra çalışsın — TenantStoreService override için
}