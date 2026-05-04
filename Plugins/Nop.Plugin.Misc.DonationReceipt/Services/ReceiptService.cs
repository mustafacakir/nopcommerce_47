using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;

namespace Nop.Plugin.Misc.DonationReceipt.Services;

public class ReceiptService
{
    public string GenerateHtml(Order order, Customer customer, IList<OrderItem> items, IList<string> productNames, Store store)
    {
        var donorName = string.IsNullOrWhiteSpace(customer.FirstName)
            ? customer.Email
            : $"{customer.FirstName} {customer.LastName}".Trim();

        var date = order.CreatedOnUtc.ToLocalTime().ToString("dd.MM.yyyy HH:mm");
        var receiptNo = order.CustomOrderNumber ?? order.Id.ToString();

        var rows = new System.Text.StringBuilder();
        for (var i = 0; i < items.Count; i++)
        {
            var name = i < productNames.Count ? productNames[i] : $"Bağış #{i + 1}";
            rows.AppendLine($@"
                <tr>
                    <td>{name}</td>
                    <td style='text-align:center'>{items[i].Quantity}</td>
                    <td style='text-align:right'>{items[i].UnitPriceInclTax:N2} {order.CustomerCurrencyCode}</td>
                    <td style='text-align:right'>{items[i].PriceInclTax:N2} {order.CustomerCurrencyCode}</td>
                </tr>");
        }

        return $@"<!DOCTYPE html>
<html lang=""tr"">
<head>
<meta charset=""utf-8"">
<meta name=""viewport"" content=""width=device-width,initial-scale=1"">
<title>Bağış Belgeniz</title>
<style>
  body {{ font-family: Arial, sans-serif; background: #f4f6fb; margin: 0; padding: 24px; color: #1a1a1a; }}
  .wrap {{ max-width: 620px; margin: 0 auto; background: #fff; border-radius: 12px; overflow: hidden; box-shadow: 0 2px 16px rgba(0,0,0,.08); }}
  .header {{ background: linear-gradient(135deg, #1d4ed8, #2563eb); color: #fff; padding: 32px 36px; }}
  .header .store-name {{ font-size: 22px; font-weight: 800; margin-bottom: 4px; }}
  .header .receipt-title {{ font-size: 13px; opacity: .85; letter-spacing: 1.5px; text-transform: uppercase; }}
  .meta {{ display: flex; gap: 32px; padding: 20px 36px; background: #f9fafb; border-bottom: 1px solid #e5e7eb; font-size: 13px; }}
  .meta-item label {{ color: #6b7280; display: block; margin-bottom: 2px; font-weight: 600; font-size: 11px; text-transform: uppercase; letter-spacing: .5px; }}
  .meta-item span {{ font-weight: 700; color: #111827; }}
  .section {{ padding: 24px 36px; }}
  .section-title {{ font-size: 11px; font-weight: 800; color: #6b7280; text-transform: uppercase; letter-spacing: .8px; margin-bottom: 12px; }}
  .donor-info {{ display: flex; gap: 40px; }}
  .donor-info div label {{ font-size: 12px; color: #9ca3af; display: block; }}
  .donor-info div span {{ font-weight: 700; font-size: 14px; }}
  table {{ width: 100%; border-collapse: collapse; font-size: 13px; }}
  thead tr {{ background: #f3f4f6; }}
  th {{ padding: 10px 12px; text-align: left; font-weight: 700; font-size: 11px; text-transform: uppercase; letter-spacing: .5px; color: #6b7280; }}
  td {{ padding: 10px 12px; border-bottom: 1px solid #f3f4f6; }}
  .total-row {{ background: #eff6ff; }}
  .total-row td {{ font-weight: 800; font-size: 15px; color: #1d4ed8; border: none; padding: 14px 12px; }}
  .footer {{ padding: 20px 36px; background: #f9fafb; border-top: 1px solid #e5e7eb; font-size: 12px; color: #9ca3af; text-align: center; }}
  .badge {{ display: inline-block; background: #dcfce7; color: #15803d; font-size: 11px; font-weight: 800; padding: 4px 12px; border-radius: 20px; margin-bottom: 16px; }}
</style>
</head>
<body>
<div class=""wrap"">

  <div class=""header"">
    <div class=""store-name"">{store?.Name ?? "Bağış Platformu"}</div>
    <div class=""receipt-title"">Bağış Belgesi</div>
  </div>

  <div class=""meta"">
    <div class=""meta-item"">
      <label>Belge No</label>
      <span>#{receiptNo}</span>
    </div>
    <div class=""meta-item"">
      <label>Tarih</label>
      <span>{date}</span>
    </div>
    <div class=""meta-item"">
      <label>Durum</label>
      <span style=""color:#15803d"">✓ Alındı</span>
    </div>
  </div>

  <div class=""section"">
    <div class=""section-title"">Bağışçı Bilgileri</div>
    <div class=""donor-info"">
      <div><label>Ad Soyad</label><span>{donorName}</span></div>
      <div><label>E-posta</label><span>{customer.Email}</span></div>
    </div>
  </div>

  <div class=""section"" style=""padding-top:0"">
    <div class=""section-title"">Bağış Detayları</div>
    <table>
      <thead>
        <tr>
          <th>Bağış</th>
          <th style=""text-align:center"">Adet</th>
          <th style=""text-align:right"">Birim</th>
          <th style=""text-align:right"">Tutar</th>
        </tr>
      </thead>
      <tbody>
        {rows}
        <tr class=""total-row"">
          <td colspan=""3"">Toplam</td>
          <td style=""text-align:right"">{order.OrderTotal:N2} {order.CustomerCurrencyCode}</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div class=""footer"">
    Bu belge elektronik ortamda otomatik olarak oluşturulmuştur.<br>
    {store?.Name} &mdash; {store?.Url?.TrimEnd('/')}
  </div>

</div>
</body>
</html>";
    }
}
