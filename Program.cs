var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

// واجهة الويب مدمجة ومباشرة على الرابط الرئيسي
app.MapGet("/", () => Results.Content(@"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>لوحة تحكم SimpleApp</title>
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; font-family: 'Segoe UI', Tahoma, sans-serif; }
        body { background: #0f172a; color: #f8fafc; display: flex; justify-content: center; align-items: center; min-height: 100vh; padding: 20px; }
        .card { background: #1e293b; width: 100%; max-width: 650px; padding: 30px; border-radius: 16px; box-shadow: 0 10px 25px rgba(0,0,0,0.4); border: 1px solid #334155; text-align: center; }
        h1 { color: #38bdf8; font-size: 24px; margin-bottom: 10px; }
        p { color: #94a3b8; font-size: 14px; margin-bottom: 25px; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        th, td { padding: 12px; border-bottom: 1px solid #334155; font-size: 14px; }
        th { background: #0284c7; color: #ffffff; }
        .badge { background: #0369a1; padding: 4px 10px; border-radius: 6px; font-weight: 600; }
        .success-box { background: rgba(34, 197, 94, 0.1); border: 1px solid #22c55e; color: #4ade80; padding: 10px; border-radius: 8px; margin-bottom: 20px; font-size: 13px; }
    </style>
</head>
<body>
    <div class=""card"">
        <div class=""success-box"">✓ تم تشغيل وتحديث تطبيق الـ Web API بنجاح عبر Jenkins & IIS</div>
        <h1>🚀 SimpleApp Web Dashboard</h1>
        <p>البيانات المعروضة مسترجعة مباشرة من الـ Backend Controller</p>
        <table>
            <thead>
                <tr>
                    <th>التاريخ</th>
                    <th>الحرارة (°C)</th>
                    <th>الحالة</th>
                </tr>
            </thead>
            <tbody id=""data-table"">
                <tr><td colspan=""3"">جاري تحميل البيانات...</td></tr>
            </tbody>
        </table>
    </div>

    <script>
        fetch('/weatherforecast')
            .then(res => res.json())
            .then(data => {
                const tbody = document.getElementById('data-table');
                tbody.innerHTML = '';
                data.forEach(item => {
                    tbody.innerHTML += `
                        <tr>
                            <td>${item.date}</td>
                            <td>${item.temperatureC}°C</td>
                            <td><span class=""badge"">${item.summary}</span></td>
                        </tr>
                    `;
                });
            })
            .catch(err => {
                document.getElementById('data-table').innerHTML = '<tr><td colspan=""3"" style=""color:#f87171"">فشل جلب بيانات الطقس</td></tr>';
            });
    </script>
</body>
</html>
", "text/html; charset=utf-8"));

app.MapControllers();

app.Run();