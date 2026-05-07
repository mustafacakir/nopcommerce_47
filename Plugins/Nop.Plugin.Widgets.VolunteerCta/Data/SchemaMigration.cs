using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.VolunteerCta.Domain;

namespace Nop.Plugin.Widgets.VolunteerCta.Data;

[NopMigration("2026/05/07 11:00:00", "Widgets.VolunteerCta base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<GonulluBasvuru>();
    }
}
