using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Data.Migrations;

[NopMigration("2026/04/27 11:00:00", "Widgets.CampaignProgress base schema", MigrationProcessType.Installation)]
public class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<Campaign>();
    }
}
