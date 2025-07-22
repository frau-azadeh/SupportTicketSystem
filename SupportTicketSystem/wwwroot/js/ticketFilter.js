export function setupTicketFilters(allTickets, renderPaginated, setPage, getPage) {
    let filtered = [];

    function applyFilters() {
        const search = document.getElementById("searchInput").value.trim().toLowerCase();
        const status = document.getElementById("statusFilter").value;

        filtered = allTickets.filter(t => {
            const matchName = t.userName.toLowerCase().includes(search);
            const matchStatus = !status || t.status === status;
            return matchName && matchStatus;
        });

        renderPaginated(filtered);
    }

    document.getElementById("searchInput").addEventListener("input", () => {
        setPage(1);
        applyFilters();
    });

    document.getElementById("statusFilter").addEventListener("change", () => {
        setPage(1);
        applyFilters();
    });

    return {
        applyFilters,
        getFiltered: () => filtered
    };
}
