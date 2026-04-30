using FluentMigrator;
using Nop.Data.Migrations;

namespace Nop.Plugin.Widgets.HeroSlider.Data.Migrations;

[NopMigration("2026/04/29 13:00:00", "Widgets.HeroSlider schema v2", MigrationProcessType.NoMatter)]
public class FixSchemaMigration : Migration
{
    public override void Up()
    {
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""BadgeLabel"" VARCHAR(200) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""PriceBadge"" VARCHAR(200) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""PrimaryButtonText"" VARCHAR(200) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""PrimaryButtonUrl"" VARCHAR(500) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""SecondaryButtonText"" VARCHAR(200) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""SecondaryButtonUrl"" VARCHAR(500) NULL");
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""CategoryIcon"" VARCHAR(2000) NULL");
    }

    public override void Down()
    {
    }
}
