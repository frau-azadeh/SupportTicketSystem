/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Pages/**/*.cshtml", "./Views/**/*.cshtml", "./wwwroot/js/**/*.js"],
    theme: {
        extend: {
            fontFamily: {
                vazir: ['Vazir', 'sans-serif'],
            },
            animation: {
                'fade-in': 'fadeIn 0.5s ease-out',
            },
            keyframes: {
                fadeIn: {
                    '0%': { opacity: '0', transform: 'scale(0.95)' },
                    '100%': { opacity: '1', transform: 'scale(1)' },
                },
            },
        },
        plugins: [],
    }
}
