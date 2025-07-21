<script>
    const sidebar = document.getElementById("sidebar");
    const toggle = document.getElementById("menuToggle");
    const overlay = document.getElementById("overlay");

    toggle?.addEventListener("click", () => {
        sidebar.classList.toggle("-translate-x-full");
    overlay.classList.toggle("hidden");
    });

    overlay?.addEventListener("click", () => {
        sidebar.classList.add("-translate-x-full");
    overlay.classList.add("hidden");
    });
</script>
