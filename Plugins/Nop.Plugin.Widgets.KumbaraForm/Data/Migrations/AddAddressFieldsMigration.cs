using FluentMigrator;
using Nop.Data.Migrations;

namespace Nop.Plugin.Widgets.KumbaraForm.Data.Migrations;

[NopMigration("2026/04/28 10:00:00", "Widgets.KumbaraForm add address fields", MigrationProcessType.Update)]
public class AddAddressFieldsMigration : Migration
{
    public override void Up()
    {
        // Eski sütunları kaldır (eğer varsa)
        if (Schema.Table("KumbaraEntry").Column("Amount").Exists())
            Delete.Column("Amount").FromTable("KumbaraEntry");
        if (Schema.Table("KumbaraEntry").Column("DonationType").Exists())
            Delete.Column("DonationType").FromTable("KumbaraEntry");

        // Yeni sütunları ekle (eğer yoksa)
        if (!Schema.Table("KumbaraEntry").Column("City").Exists())
            Alter.Table("KumbaraEntry").AddColumn("City").AsString(100).NotNullable().WithDefaultValue("");
        if (!Schema.Table("KumbaraEntry").Column("District").Exists())
            Alter.Table("KumbaraEntry").AddColumn("District").AsString(100).NotNullable().WithDefaultValue("");
        if (!Schema.Table("KumbaraEntry").Column("Address").Exists())
            Alter.Table("KumbaraEntry").AddColumn("Address").AsString(500).NotNullable().WithDefaultValue("");
        if (!Schema.Table("KumbaraEntry").Column("Quantity").Exists())
            Alter.Table("KumbaraEntry").AddColumn("Quantity").AsInt32().NotNullable().WithDefaultValue(1);
        if (!Schema.Table("KumbaraEntry").Column("UsagePlace").Exists())
            Alter.Table("KumbaraEntry").AddColumn("UsagePlace").AsString(50).Nullable();
    }

    public override void Down()
    {
    }
}
