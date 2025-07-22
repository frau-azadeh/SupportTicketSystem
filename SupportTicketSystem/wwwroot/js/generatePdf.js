export function generateTicketPDF(tickets) {
    const container = document.createElement("div");
    container.innerHTML = `
        <h2 style="text-align:center; font-family:Tahoma">گزارش تیکت‌ها</h2>
        <table dir="rtl" border="1" cellspacing="0" cellpadding="5" style="width:100%; border-collapse: collapse; font-family: Tahoma; font-size: 12px;">
            <thead>
                <tr style="background-color:#f1f1f1">
                    <th>#</th>
                    <th>کاربر</th>
                    <th>عنوان</th>
                    <th>وضعیت</th>
                </tr>
            </thead>
            <tbody>
                ${tickets.map((t, i) => `
                    <tr>
                        <td>${i + 1}</td>
                        <td>${t.userName}</td>
                        <td>${t.title}</td>
                        <td>${t.status}</td>
                    </tr>
                `).join('')}
            </tbody>
        </table>
    `;

    const opt = {
        margin: 1,
        filename: 'ticket-report.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2, scrollY: 0 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
        pagebreak: { mode: ['css', 'legacy'] }
    };

    html2pdf().set(opt).from(container).save();
}
