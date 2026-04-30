using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.DonationSection.Domain;

namespace Nop.Plugin.Widgets.DonationSection.Data.Mapping;

public class DonSectionBuilder : NopEntityBuilder<DonSection>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(DonSection.Name)).AsString(300).NotNullable()
            .WithColumn(nameof(DonSection.IconSvg)).AsString(2000).Nullable()
            .WithColumn(nameof(DonSection.Color)).AsString(50).Nullable()
            .WithColumn(nameof(DonSection.DisplayOrder)).AsInt32().NotNullable()
            .WithColumn(nameof(DonSection.IsActive)).AsBoolean().NotNullable();
    }
}
