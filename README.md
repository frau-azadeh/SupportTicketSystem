# 🎫 Support Ticket System

A modern **Support Ticket Management System** built with **ASP.NET Core Razor Pages**, leveraging **TailwindCSS**, **modular JavaScript**, and a clean component-based architecture.

[Live GitHub Repo](https://github.com/frau-azadeh/SupportTicketSystem)

## 📌 Features

- 🎨 Responsive and elegant dashboard UI using TailwindCSS
- 📊 Status summary cards with dynamic counts
- 🔍 Paginated, filterable ticket table
- 📁 File attachment support per ticket
- 👤 Role-based dashboards for Admin, IT, and Employee
- ✅ Inline ticket assignment and status update (AJAX + REST API)
- 📦 Modular JS components (e.g. `statusCards.js`, `ticketRenderer.js`)
- 📥 Export to PDF support for ticket reports
- 🌐 Persian (RTL) layout and full i18n direction support

---

## 📁 Folder Structure
```
SupportTicketSystem/
│
├── wwwroot/
│ ├── js/ # Frontend logic: charts, filters, sidebar, rendering, etc.
│ ├── css/ # Tailwind base + custom styles
│ ├── uploads/ # Uploaded ticket files
│ └── favicon.ico # Project icon
│
├── Controllers/ # API controllers (REST endpoints)
├── Data/ # EF Core DbContext
├── Dtos/ # Data Transfer Objects
├── Migrations/ # EF Migrations
├── Models/ # Entity models (User, Ticket, Notification)
├── Pages/
│ ├── Dashboard/
│ │ ├── Admin.cshtml # Admin dashboard
│ │ ├── IT.cshtml # IT panel
│ │ └── Employee.cshtml # Employee view
│ └── Shared/ # Shared layout, partials, views
│
└── README.md
```

---

## ⚙️ Technologies Used

| Layer        | Tech Stack                              |
|--------------|------------------------------------------|
| Backend      | ASP.NET Core Razor Pages + EF Core       |
| Frontend     | TailwindCSS + Modular Vanilla JS         |
| Database     | SQLite (can be switched to MSSQL/Postgres)|
| Styling      | Custom RTL UI with Tailwind              |
| Build Tool   | dotnet CLI + Visual Studio               |

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio or VS Code
- Node.js (if Tailwind build is needed – optional)

### 🔧 Run the Project


    git clone https://github.com/frau-azadeh/SupportTicketSystem.git
    cd SupportTicketSystem
    dotnet restore
    dotnet ef database update
    dotnet run
---

## 📦 API Endpoints

Endpoint	Method	Description

    /api/dashboard/it	GET	Fetch all tickets for IT panel
    /api/tickets/{id}/status	PUT	Update ticket status
    /api/tickets/{id}/assign	PUT	Assign ticket to user

## 🧩 Components

renderStatusCards(containerId, summary) — renders status overview dynamically

renderTickets(tickets, itUsers, currentPage, rowsPerPage) — modular ticket renderer

renderPagination(total, perPage, page, onPageChange) — pluggable pagination logic

ticketFilter.js, generatePdf.js, chartRender.js — optional dynamic UI add-ons

## 🌟 Contributing
Developed with 🌻 by Azadeh Sharifi Soltani Feel free to connect and collaborate!





