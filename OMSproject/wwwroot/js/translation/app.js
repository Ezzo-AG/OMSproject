////import translations from "~/js/translation/translations.js";
const translations = {
    en: {
        home: "Home",
        orders: "Orders",
        products: "Products",
        clients: "Clients",
        profits: "Profits",
        news: "News",
        selectLanguage: "Select a language",
        english: "English",
        arabic: "Arabic",
    },
    ar: {
        home: "الصفحة الرئيسية",
        orders: "من نحن",
        products: "المنتجات",
        clients: "الزبائن",
        profits: "الارباح",
        news: "الاخبار",
        selectLanguage: "إختر لغة",
        english: "الانجليزية",
        arabic: "العربية",
    },
};


const languageSelector = document.querySelector("select");
languageSelector.addEventListener("change", (event) => {
    setLanguage(event.target.value);
    localStorage.setItem("lang", event.target.value);
});

document.addEventListener("DOMContentLoaded", () => {
    const language = localStorage.getItem("lang") || "en"; // اذا لم تكن اللغة متوفرة استخدم الانجليزية
    setLanguage(language);
});

const setLanguage = (language) => {
    const elements = document.querySelectorAll("[data-i18n]");
    elements.forEach((element) => {
        const translationKey = element.getAttribute("data-i18n");
        element.textContent = translations[language][translationKey];
    });
    document.dir = language === "ar" ? "rtl" : "ltr";
};
console.log("im working")