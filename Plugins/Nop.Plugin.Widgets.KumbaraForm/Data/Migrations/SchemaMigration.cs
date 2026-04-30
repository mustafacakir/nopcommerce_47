using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.KumbaraForm.Domain;

namespace Nop.Plugin.Widgets.KumbaraForm.Data.Migrations;

[NopMigration("2026/04/27 10:00:00", "Widgets.KumbaraForm base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<KumbaraEntry>();
    }
}
