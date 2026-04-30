using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.DonationSection.Domain;

namespace Nop.Plugin.Widgets.DonationSection.Data.Mapping;

public class DonItemBuilder : NopEntityBuilder<DonItem>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(DonItem.SectionId)).AsInt32().NotNullable()
            .WithColumn(nameof(DonItem.Name)).AsString(300).NotNullable()
            .WithColumn(nameof(DonItem.Description)).AsString(1000).Nullable()
            .WithColumn(nameof(DonItem.ImageUrl)).AsString(500).Nullable()
            .WithColumn(nameof(DonItem.Price)).AsDecimal(18, 4).NotNullable()
            .WithColumn(nameof(DonItem.ProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(DonItem.DisplayOrder)).AsInt32().NotNullable()
            .WithColumn(nameof(DonItem.IsActive)).AsBoolean().NotNullable();
    }
}
