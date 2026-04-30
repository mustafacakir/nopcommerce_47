using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.DonationSection.Domain;

namespace Nop.Plugin.Widgets.DonationSection.Data.Migrations;

[NopMigration("2026/04/28 10:35:00", "Widgets.DonationSection schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<DonSection>();
        Create.TableFor<DonItem>();
    }
}
