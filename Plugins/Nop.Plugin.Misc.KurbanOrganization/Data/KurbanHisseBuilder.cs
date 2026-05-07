using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.KurbanOrganization.Domain;

namespace Nop.Plugin.Misc.KurbanOrganization.Data;

public class KurbanHisseBuilder : NopEntityBuilder<KurbanHisse>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(KurbanHisse.OrderId)).AsInt32().NotNullable()
            .WithColumn(nameof(KurbanHisse.OrderItemId)).AsInt32().NotNullable()
            .WithColumn(nameof(KurbanHisse.CustomerId)).AsInt32().NotNullable()
            .WithColumn(nameof(KurbanHisse.HisseKodu)).AsString(20).NotNullable()
            .WithColumn(nameof(KurbanHisse.KurbanTuru)).AsString(100).NotNullable()
            .WithColumn(nameof(KurbanHisse.HisseSayisi)).AsInt32().NotNullable()
            .WithColumn(nameof(KurbanHisse.Kesildi)).AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn(nameof(KurbanHisse.KesimTarihi)).AsDateTime2().Nullable()
            .WithColumn(nameof(KurbanHisse.BildirimGonderildi)).AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn(nameof(KurbanHisse.MusteriAd)).AsString(200).Nullable()
            .WithColumn(nameof(KurbanHisse.MusteriTelefon)).AsString(20).Nullable()
            .WithColumn(nameof(KurbanHisse.CreatedOnUtc)).AsDateTime2().NotNullable();
    }
}
