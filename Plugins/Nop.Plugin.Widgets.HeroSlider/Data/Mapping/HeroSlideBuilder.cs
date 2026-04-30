using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.HeroSlider.Domain;

namespace Nop.Plugin.Widgets.HeroSlider.Data.Mapping;

public class HeroSlideBuilder : NopEntityBuilder<HeroSlide>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(HeroSlide.Title)).AsString(400).NotNullable()
            .WithColumn(nameof(HeroSlide.Subtitle)).AsString(400).Nullable()
            .WithColumn(nameof(HeroSlide.BadgeLabel)).AsString(200).Nullable()
            .WithColumn(nameof(HeroSlide.PriceBadge)).AsString(200).Nullable()
            .WithColumn(nameof(HeroSlide.PrimaryButtonText)).AsString(200).Nullable()
            .WithColumn(nameof(HeroSlide.PrimaryButtonUrl)).AsString(500).Nullable()
            .WithColumn(nameof(HeroSlide.SecondaryButtonText)).AsString(200).Nullable()
            .WithColumn(nameof(HeroSlide.SecondaryButtonUrl)).AsString(500).Nullable()
            .WithColumn(nameof(HeroSlide.ImageUrl)).AsString(500).Nullable()
            .WithColumn(nameof(HeroSlide.CategoryName)).AsString(200).NotNullable()
            .WithColumn(nameof(HeroSlide.CategoryIcon)).AsString(2000).Nullable()
            .WithColumn(nameof(HeroSlide.DisplayOrder)).AsInt32().NotNullable()
            .WithColumn(nameof(HeroSlide.IsActive)).AsBoolean().NotNullable();
    }
}
