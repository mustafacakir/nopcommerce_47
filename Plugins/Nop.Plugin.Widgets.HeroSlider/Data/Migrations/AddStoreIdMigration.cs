using FluentMigrator;
using Nop.Data.Migrations;

namespace Nop.Plugin.Widgets.HeroSlider.Data.Migrations;

[NopMigration("2026/05/04 10:00:00", "Widgets.HeroSlider add StoreId", MigrationProcessType.NoMatter)]
public class AddStoreIdMigration : Migration
{
    public override void Up()
    {
        Execute.Sql(@"ALTER TABLE ""HeroSlide"" ADD COLUMN IF NOT EXISTS ""StoreId"" INT NOT NULL DEFAULT 0");
    }

    public override void Down()
    {
    }
}
