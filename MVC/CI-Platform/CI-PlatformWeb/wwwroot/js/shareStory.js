let draftMedia = [];
const mediaDiv = document.querySelectorAll("#data-media");

if (mediaDiv.length != 0) {
    Array.from(mediaDiv).forEach(element => {
        console.log($(element).data('media'));
        draftMedia.push(+$(element).data('media'));
    })

    $(mediaDiv).click(() => {
        console.log(draftMedia)
        draftMedia.splice(draftMedia.indexOf(+$(this).data()), 1)
        $(this).parent().remove();
        console.log(draftMedia)
    })
}

ssDragndrop = document.querySelector(".ss-dragndrop");
ssImagesInput = document.querySelector("#ssImagesInput");
ssSelectedImages = document.querySelector(".ss-selected-images");
ssDragndrop.addEventListener("click", () => { ssImagesInput.click(); });
files = [];

ssImagesInput.addEventListener("change", () => {
    let file = ssImagesInput.files;
    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != "image") continue;
        if (!files.some((e) => e.name == file[i].name)) files.push(file[i]);
    }
    showImages();
});
ssDragndrop.addEventListener("dragover", e => e.preventDefault());
ssDragndrop.addEventListener("dragenter", e => e.preventDefault());
ssDragndrop.addEventListener("drop", e => {
    e.preventDefault();
    let file = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {
        /** Check selected file is image */
        if (file[i].type.split("/")[1] != "png" && file[i].type.split("/")[1] != "jpg" && file[i].type.split("/")[1] != "jpeg") continue;
        if (!files.some((e) => e.name == file[i].name)) files.push(file[i]);
    }
    setImageInput();
    showImages();
});
function showImages() {
    ssSelectedImages.innerHTML = '';
    files.forEach((element, index, _) => {
        ssSelectedImages.innerHTML += `<div class="position-relative">
        <img src="${URL.createObjectURL(element)}" alt="story image" height="90px" width="90px" />
        <div class="position-absolute top-0 end-0 p-1 bg-dark cursor-pointer" onClick=delImage(${index})>
          <img src="/images/static/cross.png" alt="cross" height="10px"/>
        </div>`;
    });
}
function delImage(index) {
    files.splice(index, 1);
    setImageInput();
    showImages();
}
function setImageInput() {
    let myFileList = new DataTransfer();
    files.forEach(function (file) {
        myFileList.items.add(file);
    });
    ssImagesInput.files = myFileList.files;
}