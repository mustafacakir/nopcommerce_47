using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Data.Mapping;

public class CampaignBuilder : NopEntityBuilder<Campaign>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Campaign.Title)).AsString(300).NotNullable()
            .WithColumn(nameof(Campaign.Slug)).AsString(200).NotNullable()
            .WithColumn(nameof(Campaign.Description)).AsString(4000).Nullable()
            .WithColumn(nameof(Campaign.ImageUrl)).AsString(500).Nullable()
            .WithColumn(nameof(Campaign.GoalAmount)).AsDecimal(18, 4).NotNullable()
            .WithColumn(nameof(Campaign.ManualBonus)).AsDecimal(18, 4).NotNullable()
            .WithColumn(nameof(Campaign.LinkedProductId)).AsInt32().NotNullable()
            .WithColumn(nameof(Campaign.IsActive)).AsBoolean().NotNullable()
            .WithColumn(nameof(Campaign.DisplayOrder)).AsInt32().NotNullable()
            .WithColumn(nameof(Campaign.CreatedOnUtc)).AsDateTime2().NotNullable();
    }
}
