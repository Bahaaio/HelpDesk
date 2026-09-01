window.generateQRCode = (elementId, text) => {
    const el = document.getElementById(elementId);
    if (!el) return;
    el.innerHTML = "";
    new QRCode(el, {
        text: text,
        width: 200,
        height: 200,
        colorDark: "#000000",
        colorLight: "#ffffff",
        correctLevel: QRCode.CorrectLevel.M
    });
};
