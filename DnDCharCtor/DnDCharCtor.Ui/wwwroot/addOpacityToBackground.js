window.addOpacityToBackground = (elementId, opacity) => {
    const element = document.getElementById(elementId);
    const computedStyle = getComputedStyle(element);
    const backgroundColor = computedStyle.backgroundColor;

    function addOpacityToColor(color, opacity) {
        const rgbaColor = color.replace('rgb', 'rgba').replace(')', `, ${opacity})`);
        return rgbaColor;
    }

    const backgroundColorWithOpacity = addOpacityToColor(backgroundColor, opacity);
    element.style.backgroundColor = backgroundColorWithOpacity;
};