document.getElementById("uploadImage").addEventListener("change", function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const imgTag = `<figure><img src="${e.target.result}" alt="Uploaded Image"><figcaption>Click to edit caption</figcaption></figure>`;
            document.getElementById("editor").innerHTML += imgTag;
        };
        reader.readAsDataURL(file);
    }
});

function execCommand(command) {
    document.execCommand(command, false, null);
}
function execCommandFontSize(command, size) {
    if (size >= 1 && size <= 7) {
        document.execCommand(command, false, size);
    }
}

function getEditorContent(sequences) {
    return sequences == null ? document.getElementById(`editor`).innerHTML : document.getElementById(`editor_${sequences}`).innerHTML;
}