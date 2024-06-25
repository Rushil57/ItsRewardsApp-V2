function googleRecaptcha(dotNetObject, selector, sitekeyValue) {
    return grecaptcha.render(document.getElementById(selector), {
        'sitekey': sitekeyValue,
        'callback': (response) => { dotNetObject.invokeMethodAsync('CallbackOnSuccess', response); },
        'expired-callback': () => { expCallback }
    });
};
function expCallback() {
    setInterval(function () { grecaptcha.reset(); }, 5 * 60 * 1000);
}
function getResponse(response) {
    return grecaptcha.getResponse(response);
} 