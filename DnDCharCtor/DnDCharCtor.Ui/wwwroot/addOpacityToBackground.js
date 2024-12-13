window.addOpacityToBackground = (elementId, opacity) => {
    //console.log(`addOpacityToBackground to ${elementId} by ${opacity}`)
    const element = document.getElementById(elementId);
    if (!element) {
        console.error(`Element with ID ${elementId} not found.`);
        return;
    }
    const computedStyle = getComputedStyle(element);
    const backgroundColor = computedStyle.backgroundColor;
    console.log(backgroundColor);

    function addOpacityToColor(color, opacity) {
        if (color.startsWith('rgb(')) {
            return color.replace('rgb', 'rgba').replace(')', `, ${opacity})`);
        } else if (color.startsWith('rgba(')) {
            return color.replace(/[\d\.]+\)$/g, `${opacity})`);
        } else {
            console.error('Unsupported color format:', color);
            return color;
        }
    }

    const backgroundColorWithOpacity = addOpacityToColor(backgroundColor, opacity);
    //console.log(backgroundColorWithOpacity);
    element.style.backgroundColor = backgroundColorWithOpacity;
    //console.log(`addOpacityToBackground completed`)
};