let donutChartInstance = null;
let barChartInstance = null;

export function renderCharts(tickets, itUsers) {
    // Destroy previous charts if they exist
    if (donutChartInstance) {
        donutChartInstance.destroy();
    }
    if (barChartInstance) {
        barChartInstance.destroy();
    }

    const ticketCountByUser = {};
    tickets.forEach(t => {
        if (t.assignedTo !== "هنوز ارجاع نشده") {
            ticketCountByUser[t.assignedTo] = (ticketCountByUser[t.assignedTo] || 0) + 1;
        }
    });

    const donutLabels = Object.keys(ticketCountByUser);
    const donutValues = Object.values(ticketCountByUser);

    donutChartInstance = new Chart(document.getElementById("donutChart"), {
        type: 'doughnut',
        data: {
            labels: donutLabels,
            datasets: [{
                data: donutValues,
                backgroundColor: ['#3b82f6', '#f59e0b', '#10b981', '#ef4444', '#8b5cf6', '#ec4899']
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { position: 'bottom' } }
        }
    });

    const statusTypes = ["در انتظار بررسی", "در حال انجام", "انجام شده", "باطل شده"];
    const userStatusCounts = {};

    itUsers.forEach(u => {
        userStatusCounts[u.fullName] = { "در انتظار بررسی": 0, "در حال انجام": 0, "انجام شده": 0, "باطل شده": 0 };
    });

    tickets.forEach(t => {
        if (t.assignedTo !== "هنوز ارجاع نشده") {
            const status = t.status ?? "در انتظار بررسی";
            userStatusCounts[t.assignedTo][status] += 1;
        }
    });

    const barLabels = Object.keys(userStatusCounts);
    const barDatasets = statusTypes.map((status, i) => ({
        label: status,
        data: barLabels.map(user => userStatusCounts[user][status]),
        backgroundColor: ['#facc15', '#3b82f6', '#10b981', '#ef4444'][i]
    }));

    barChartInstance = new Chart(document.getElementById("barChart"), {
        type: 'bar',
        data: {
            labels: barLabels,
            datasets: barDatasets
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { position: 'top' } },
            scales: { x: { stacked: true }, y: { stacked: true, beginAtZero: true } }
        }
    });
}
