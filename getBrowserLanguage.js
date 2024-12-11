window.getBrowserLanguage = function () {
    return navigator.language || navigator.userLanguage; // For older browsers
};