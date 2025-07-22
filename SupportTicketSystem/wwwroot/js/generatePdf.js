
export async function generateTicketPDF(tickets, renderTickets, currentPage, applyFilters) {
    const originalTickets = [...tickets];
    const originalPage = currentPage;

    renderTickets(tickets); // show ticket

    await new Promise(resolve => setTimeout(resolve, 300)); // wait to render table

    const element = document.getElementById("report-content");
    const opt = {
        margin: 0.3,
        filename: 'ticket-report.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2, scrollY: 0 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' },
        pagebreak: { mode: ['css', 'legacy'] }
    };

    await html2pdf().set(opt).from(element).save();

    currentPage = originalPage;
    applyFilters();
}
