using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.VolunteerCta.Domain;

namespace Nop.Plugin.Widgets.VolunteerCta.Data;

public class GonulluBasvuruBuilder : NopEntityBuilder<GonulluBasvuru>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(GonulluBasvuru.Ad)).AsString(100).NotNullable()
            .WithColumn(nameof(GonulluBasvuru.Soyad)).AsString(100).NotNullable()
            .WithColumn(nameof(GonulluBasvuru.Telefon)).AsString(20).NotNullable()
            .WithColumn(nameof(GonulluBasvuru.Email)).AsString(200).Nullable()
            .WithColumn(nameof(GonulluBasvuru.Not)).AsString(1000).Nullable()
            .WithColumn(nameof(GonulluBasvuru.CreatedOnUtc)).AsDateTime2().NotNullable();
    }
}
