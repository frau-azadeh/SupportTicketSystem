/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Pages/**/*.cshtml", "./Views/**/*.cshtml", "./wwwroot/js/**/*.js"],
    theme: {
        extend: {
            fontFamily: {
                vazir: ['Vazir', 'sans-serif'],
            },
        },
    },
    plugins: [],
}

