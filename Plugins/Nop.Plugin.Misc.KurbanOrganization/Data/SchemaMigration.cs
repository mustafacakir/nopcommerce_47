using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.KurbanOrganization.Domain;

namespace Nop.Plugin.Misc.KurbanOrganization.Data;

[NopMigration("2026/05/07 10:00:00", "Misc.KurbanOrganization base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<KurbanHisse>();
    }
}
