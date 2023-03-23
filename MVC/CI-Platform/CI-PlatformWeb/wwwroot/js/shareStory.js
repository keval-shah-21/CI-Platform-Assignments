//CKSource.Editor
//    .create(document.querySelector('#editor'))
//    .catch(error => {
//        console.error(error);
//    });

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
    showImages();
}


let action = "";
$("#ssForm").on('submit', (e) => {
    e.preventDefault();
    const videourl = $('#VideoUrl').val();

    let error = false;
    if (files.length < 1) {
        $('#MediaError').text("Upload at least one image.");
        error = true;
    } else
        $('#MediaError').text("");

    const missionId = $('#StoryMission').find(":selected").val();
    if (missionId == 0) {
        $('#StoryMissionError').text("This field is required.");
        error = true;
    } else
        $('#StoryMissionError').text('');

    const title = $('#Title').val();
    if (title == null || title == "") {
        $("#TitleError").text("This field is required.");
        error = true;
    } else
        $("#TitleError").text("");

    var myContent = tinymce.get("tiny").getContent();
    if (myContent == null || myContent == "") {
        $("#DescriptionError").text('This field is required.')
        error = true;
    } else
        $("#DescriptionError").text('')

    if (error == true) return;


    let formData = new FormData()
    files.forEach(f => {
        formData.append('file', f)
    })
    var obj = {
        missionId: missionId,
        title: title,
        description: myContent,
        videourl: videourl,
        files: formData ,
        action: action,
    }
    console.log(obj);
    $.ajax({
        url: "/Volunteer/Story/ShareStory",
        method: "POST",
        data: obj,
        contentType: false,
        processData: false,
        success: result => {
            console.log(result);
        },
        error: error => {
            console.log(error);
        }
    });
})

$("#saveBtn").click((e) => {
    action = 'save';
})
$("#submitBtn").click((e) => {
    action = 'submit';
})