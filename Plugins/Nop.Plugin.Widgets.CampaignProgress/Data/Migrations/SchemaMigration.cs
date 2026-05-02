using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.CampaignProgress.Domain;

namespace Nop.Plugin.Widgets.CampaignProgress.Data.Migrations;

[NopMigration("2026/04/27 11:00:00", "Widgets.CampaignProgress base schema", MigrationProcessType.Installation)]
public class SchemaMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table("Campaign").Exists())
            Create.TableFor<Campaign>();
    }

    public override void Down()
    {
        if (Schema.Table("Campaign").Exists())
            Delete.Table("Campaign");
    }
}
