/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Pages/**/*.cshtml", "./Views/**/*.cshtml", "./wwwroot/js/**/*.js"],
    safelist: [
        'bg-red-100',
        'text-red-800',
        'border-red-300',
        'bg-yellow-100',
        'text-yellow-800',
        'border-yellow-300',
        'bg-green-100',
        'text-green-800',
        'border-green-300',
        'bg-blue-100',
        'text-blue-800',
        'border-blue-300',
    ],
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
