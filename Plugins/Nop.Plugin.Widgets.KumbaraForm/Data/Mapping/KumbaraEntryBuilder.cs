using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.KumbaraForm.Domain;

namespace Nop.Plugin.Widgets.KumbaraForm.Data.Mapping;

public class KumbaraEntryBuilder : NopEntityBuilder<KumbaraEntry>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(KumbaraEntry.FirstName)).AsString(100).NotNullable()
            .WithColumn(nameof(KumbaraEntry.LastName)).AsString(100).NotNullable()
            .WithColumn(nameof(KumbaraEntry.Email)).AsString(200).NotNullable()
            .WithColumn(nameof(KumbaraEntry.Phone)).AsString(30).NotNullable()
            .WithColumn(nameof(KumbaraEntry.City)).AsString(100).NotNullable()
            .WithColumn(nameof(KumbaraEntry.District)).AsString(100).NotNullable()
            .WithColumn(nameof(KumbaraEntry.Address)).AsString(500).NotNullable()
            .WithColumn(nameof(KumbaraEntry.Quantity)).AsInt32().NotNullable()
            .WithColumn(nameof(KumbaraEntry.UsagePlace)).AsString(50).Nullable()
            .WithColumn(nameof(KumbaraEntry.Message)).AsString(1000).Nullable()
            .WithColumn(nameof(KumbaraEntry.IsRead)).AsBoolean().NotNullable()
            .WithColumn(nameof(KumbaraEntry.CreatedOnUtc)).AsDateTime2().NotNullable();
    }
}
