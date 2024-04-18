const addedImage = document.querySelector('#addition-image');
const preview = document.querySelector('#preview');

addedImage.onchange = evt => {
    const file = addedImage.files[0]
    if (file) {
        preview.src = URL.createObjectURL(file)
    }
}