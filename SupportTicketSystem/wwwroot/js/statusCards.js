export function renderStatusCards(containerId, summary) {
    const container = document.getElementById(containerId);
    if (!container) return;

    const statusMap = {
        "در انتظار بررسی": { color: "yellow", count: summary.pending || 0 },
        "در حال انجام": { color: "blue", count: summary.inprogress || 0 },
        "انجام شده": { color: "green", count: summary.done || 0 },
        "باطل شده": { color: "red", count: summary.canceled || 0 }
    };

    container.innerHTML = "";

    for (const [label, info] of Object.entries(statusMap)) {
        const card = document.createElement("div");
        card.className = `bg-${info.color}-100 border border-${info.color}-300 text-${info.color}-800 rounded-lg p-4 shadow`;
        card.innerHTML = `
            <h3 class="text-lg font-semibold mb-1">${label}</h3>
            <p class="text-2xl font-bold">${info.count}</p>
        `;
        container.appendChild(card);
    }
}
