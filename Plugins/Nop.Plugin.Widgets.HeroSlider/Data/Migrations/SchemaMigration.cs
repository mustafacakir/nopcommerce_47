using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.HeroSlider.Domain;

namespace Nop.Plugin.Widgets.HeroSlider.Data.Migrations;

[NopMigration("2026/04/28 10:05:00", "Widgets.HeroSlider schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<HeroSlide>();
    }
}
