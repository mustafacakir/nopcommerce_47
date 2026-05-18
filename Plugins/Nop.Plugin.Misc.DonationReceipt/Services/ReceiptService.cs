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

        var storeName = store?.Name ?? "Bağış Platformu";
        var storeUrl  = store?.Url?.TrimEnd('/') ?? string.Empty;

        var rows = new System.Text.StringBuilder();
        for (var i = 0; i < items.Count; i++)
        {
            var name   = i < productNames.Count ? productNames[i] : $"Bağış #{i + 1}";
            var rowBg  = i % 2 == 0 ? "#ffffff" : "#f9fafb";
            rows.AppendLine(
                $"<tr style=\"background:{rowBg};\">" +
                $"<td style=\"padding:14px 18px;font-size:13px;color:#374151;border-bottom:1px solid #f0f0f0;\">{name}</td>" +
                $"<td style=\"padding:14px 18px;font-size:13px;color:#374151;text-align:center;border-bottom:1px solid #f0f0f0;\">{items[i].Quantity}</td>" +
                $"<td style=\"padding:14px 18px;font-size:13px;color:#374151;text-align:right;border-bottom:1px solid #f0f0f0;\">{items[i].UnitPriceInclTax:N2} {order.CustomerCurrencyCode}</td>" +
                $"<td style=\"padding:14px 18px;font-size:13px;font-weight:700;color:#374151;text-align:right;border-bottom:1px solid #f0f0f0;\">{items[i].PriceInclTax:N2} {order.CustomerCurrencyCode}</td>" +
                "</tr>");
        }

        return
$@"<!DOCTYPE html>
<html lang=""tr"">
<head>
<meta charset=""utf-8"">
<meta name=""viewport"" content=""width=device-width,initial-scale=1"">
<title>{storeName} – Bağış Belgesi</title>
</head>
<body style=""margin:0;padding:0;background:#f0f4f4;font-family:Arial,Helvetica,sans-serif;color:#1a1a2e;"">

<table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""background:#f0f4f4;padding:36px 16px;"">
<tr><td align=""center"">
<table width=""600"" cellpadding=""0"" cellspacing=""0"" border=""0""
       style=""max-width:600px;width:100%;background:#ffffff;border-radius:16px;
               overflow:hidden;box-shadow:0 4px 24px rgba(0,0,0,0.10);"">

  <!-- ── HEADER ── -->
  <tr>
    <td style=""background:linear-gradient(135deg,#2ABFBF 0%,#1a9090 100%);
               padding:40px 40px 36px;text-align:center;"">
      <div style=""font-size:28px;font-weight:800;color:#ffffff;
                  letter-spacing:-0.5px;margin-bottom:10px;"">{storeName}</div>
      <div style=""font-size:12px;color:rgba(255,255,255,0.80);
                  text-transform:uppercase;letter-spacing:2.5px;font-weight:600;"">Bağış Belgesi</div>
    </td>
  </tr>

  <!-- ── BADGE ── -->
  <tr>
    <td style=""background:#f6fffe;padding:20px 40px;text-align:center;
               border-bottom:1px solid #e0f5f5;"">
      <span style=""display:inline-block;background:#dcfce7;color:#15803d;
                    font-size:13px;font-weight:700;padding:7px 22px;
                    border-radius:20px;letter-spacing:0.5px;"">✓ Bağışınız Alındı</span>
    </td>
  </tr>

  <!-- ── META CARDS ── -->
  <tr>
    <td style=""padding:32px 40px 0;"">
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
        <tr>
          <td style=""background:#f8fafb;border:1px solid #e5e7eb;border-radius:10px;
                     padding:18px 20px;text-align:center;width:33%;"">
            <div style=""font-size:11px;color:#9ca3af;text-transform:uppercase;
                        letter-spacing:0.8px;font-weight:700;margin-bottom:8px;"">Belge No</div>
            <div style=""font-size:20px;font-weight:800;color:#111827;"">#{receiptNo}</div>
          </td>
          <td width=""16"" style=""font-size:0;line-height:0;"">&nbsp;</td>
          <td style=""background:#f8fafb;border:1px solid #e5e7eb;border-radius:10px;
                     padding:18px 20px;text-align:center;width:33%;"">
            <div style=""font-size:11px;color:#9ca3af;text-transform:uppercase;
                        letter-spacing:0.8px;font-weight:700;margin-bottom:8px;"">Tarih</div>
            <div style=""font-size:15px;font-weight:700;color:#111827;"">{date}</div>
          </td>
          <td width=""16"" style=""font-size:0;line-height:0;"">&nbsp;</td>
          <td style=""background:#f8fafb;border:1px solid #e5e7eb;border-radius:10px;
                     padding:18px 20px;text-align:center;width:33%;"">
            <div style=""font-size:11px;color:#9ca3af;text-transform:uppercase;
                        letter-spacing:0.8px;font-weight:700;margin-bottom:8px;"">Durum</div>
            <div style=""font-size:15px;font-weight:700;color:#15803d;"">✓ Alındı</div>
          </td>
        </tr>
      </table>
    </td>
  </tr>

  <!-- ── DONOR INFO ── -->
  <tr>
    <td style=""padding:32px 40px 0;"">
      <div style=""font-size:11px;font-weight:800;color:#9ca3af;text-transform:uppercase;
                  letter-spacing:1px;margin-bottom:14px;"">Bağışçı Bilgileri</div>
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
             style=""border:1px solid #e5e7eb;border-radius:10px;overflow:hidden;"">
        <tr>
          <td style=""padding:16px 20px;background:#f9fafb;width:38%;
                     border-right:1px solid #e5e7eb;border-bottom:1px solid #e5e7eb;"">
            <span style=""font-size:12px;color:#6b7280;font-weight:600;"">Ad Soyad</span>
          </td>
          <td style=""padding:16px 22px;background:#ffffff;border-bottom:1px solid #e5e7eb;"">
            <span style=""font-size:14px;font-weight:700;color:#111827;"">{donorName}</span>
          </td>
        </tr>
        <tr>
          <td style=""padding:16px 20px;background:#f9fafb;width:38%;
                     border-right:1px solid #e5e7eb;"">
            <span style=""font-size:12px;color:#6b7280;font-weight:600;"">E-posta</span>
          </td>
          <td style=""padding:16px 22px;background:#ffffff;"">
            <span style=""font-size:14px;font-weight:600;color:#2ABFBF;"">{customer.Email}</span>
          </td>
        </tr>
      </table>
    </td>
  </tr>

  <!-- ── ITEMS TABLE ── -->
  <tr>
    <td style=""padding:32px 40px 0;"">
      <div style=""font-size:11px;font-weight:800;color:#9ca3af;text-transform:uppercase;
                  letter-spacing:1px;margin-bottom:14px;"">Bağış Detayları</div>
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0""
             style=""border:1px solid #e5e7eb;border-radius:10px;overflow:hidden;"">
        <tr style=""background:#f9fafb;"">
          <th style=""padding:12px 18px;text-align:left;font-size:11px;font-weight:700;
                     color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;
                     border-bottom:1px solid #e5e7eb;"">Bağış</th>
          <th style=""padding:12px 18px;text-align:center;font-size:11px;font-weight:700;
                     color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;
                     border-bottom:1px solid #e5e7eb;"">Adet</th>
          <th style=""padding:12px 18px;text-align:right;font-size:11px;font-weight:700;
                     color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;
                     border-bottom:1px solid #e5e7eb;"">Birim</th>
          <th style=""padding:12px 18px;text-align:right;font-size:11px;font-weight:700;
                     color:#6b7280;text-transform:uppercase;letter-spacing:0.5px;
                     border-bottom:1px solid #e5e7eb;"">Tutar</th>
        </tr>
        {rows}
        <tr style=""background:#f0fdf9;"">
          <td colspan=""3"" style=""padding:18px 18px;font-size:15px;font-weight:800;
                                   color:#111827;border-top:2px solid #e5e7eb;"">Toplam</td>
          <td style=""padding:18px 18px;text-align:right;font-size:18px;font-weight:800;
                     color:#2ABFBF;border-top:2px solid #e5e7eb;"">{order.OrderTotal:N2} {order.CustomerCurrencyCode}</td>
        </tr>
      </table>
    </td>
  </tr>

  <!-- ── FOOTER ── -->
  <tr>
    <td style=""padding:36px 40px 32px;text-align:center;border-top:1px solid #e5e7eb;margin-top:32px;"">
      <p style=""margin:0 0 6px;font-size:12px;color:#9ca3af;"">
        Bu belge elektronik ortamda otomatik olarak oluşturulmuştur.
      </p>
      <p style=""margin:0;font-size:12px;color:#9ca3af;"">
        {storeName}{(string.IsNullOrEmpty(storeUrl) ? "" : $" &mdash; {storeUrl}")}
      </p>
    </td>
  </tr>

</table>
</td></tr>
</table>

</body>
</html>";
    }
}
