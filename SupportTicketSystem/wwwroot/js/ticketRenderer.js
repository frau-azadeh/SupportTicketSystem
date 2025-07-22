// /js/ticketRenderer.js

export function renderTickets(tickets, itUsers, currentPage, rowsPerPage) {
    const table = document.getElementById("ticketTable");
    table.innerHTML = "";
    tickets.forEach((item, index) => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td class="border px-4 py-2">${(currentPage - 1) * rowsPerPage + index + 1}</td>
            <td class="border px-4 py-2">${item.userName}</td>
            <td class="border px-4 py-2">${item.title}</td>
            <td class="border px-4 py-2">${item.status}</td>
            <td class="border px-4 py-2">
                <select class="assign-dropdown border rounded px-2 py-1 text-sm">
                    <option value="">انتخاب کارشناس</option>
                    ${itUsers.map(u => `
                        <option value="${u.id}" ${u.id === item.assignedToId ? 'selected' : ''}>${u.fullName}</option>
                    `).join('')}
                </select>
            </td>
            <td class="border px-4 py-2">
                <button class="update-btn bg-blue-500 text-white px-3 py-1 rounded text-xs" data-id="${item.id}">ثبت</button>
            </td>
        `;
        table.appendChild(row);
    });
}

export function renderPagination(totalRows, rowsPerPage, currentPage, onPageChange) {
    const pagination = document.getElementById("pagination");
    const pageCount = Math.ceil(totalRows / rowsPerPage);
    pagination.innerHTML = "";

    for (let i = 1; i <= pageCount; i++) {
        const btn = document.createElement("button");
        btn.textContent = i;
        btn.className = `px-3 py-1 border rounded ${i === currentPage ? 'bg-blue-600 text-white' : 'bg-white text-blue-600'}`;

        btn.addEventListener("click", () => {
            onPageChange(i);  // این تابع باید در فایل اصلی مقدار currentPage رو ست کنه و applyFilters بزنه
        });

        pagination.appendChild(btn);
    }
}
