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
        if (!files.some((e) => e.name == file[i].name)) {
            files.push(file[i]);
            showImage(URL.createObjectURL(file[i]), files.length - 1)
        }
    }
    addEvents();
});
ssDragndrop.addEventListener("dragover", e => e.preventDefault());
ssDragndrop.addEventListener("dragenter", e => e.preventDefault());
ssDragndrop.addEventListener("drop", e => {
    e.preventDefault();
    let file = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {
        /** Check selected file is image */
        if (file[i].type.split("/")[1] != "png" && file[i].type.split("/")[1] != "jpg" && file[i].type.split("/")[1] != "jpeg") continue;
        if (!files.some((e) => e.name == file[i].name)) {
            files.push(file[i]);
            showImage(URL.createObjectURL(file[i]), files.length - 1)
        }
    }
    addEvents();
});
function showImage(src, index) {
    ssSelectedImages.innerHTML += `<div class="position-relative">
        <img src="${src}" alt="story image" class="object-fit-cover" height="90px" width="90px" />
        <div class="position-absolute top-0 end-0 p-1 bg-dark cursor-pointer" data-img="${index}">
          <img src="/images/static/cross.png" alt="cross" height="10px"/>
        </div>`;
}
function resetData() {
    Array.from(document.querySelectorAll(`[data-img]`)).forEach((element, i) => {
        $(element).data("img", i);
    })
}
function addEvents() {
    Array.from(document.querySelectorAll(`[data-img]`)).forEach(img => {
        $(img).click(() => {
            files.splice($(img).data('img'), 1)
            $(img).parent().remove();
            resetData();
        })
    });
}

//handle draft images
const isDraft = $('#isDraft').val();
if (isDraft == "true") {
    async function fetchAndCreateFiles() {
        const images = Array.from(document.querySelectorAll('[data-path]'));
        for (let i = 0; i < images.length; i++) {
            const image = images[i];
            const fileName = image.value;
            const url = $(image).data("path");
            const type = $(image).data("type");

            const response = await fetch(url);
            const buffer = await response.arrayBuffer();
            const myFile = new File([buffer], fileName, { type: `image/${type.slice(1)}` });
            files.push(myFile);

            showImage(url, i);
        }
        addEvents();
    }
    fetchAndCreateFiles()
}
const form = document.querySelector('#ssForm');
form.addEventListener('submit', (e) => {
    e.preventDefault();
    $('#ssForm').valid();
    let error = false;

    if (tinymce.get("tiny").getContent() == null || tinymce.get("tiny").getContent() == "") {
        error = true;
        $('#DescriptionError').text("The My Story field is required.");
    } else
        $('#DescriptionError').text("");

    if (files.length == 0) {
        error = true;
        $('#mediaError').text("Please upload at least one image.");
    } else
        $('#mediaError').text("");

    $('#action').val(e.submitter.getAttribute('value'));
    if (!error && $("#ssForm").valid()) {
        setImageInput();
        if (e.submitter.getAttribute("value") == 'submit') {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to edit this later! Admin will review and approve it.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Submit!'
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();
                }
            })
        } else {
            form.submit();
        }
    }
})

function setImageInput() {
    let myFileList = new DataTransfer();
    files.forEach(function (file) {
        myFileList.items.add(file);
    });
    ssImagesInput.files = myFileList.files;
}

$("#cancelBtn").click(() => {
    if (isDraft == "true") {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to retrieve data back!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Remove!'
        }).then((result) => {
            if (result.isConfirmed) {
                const storyId = $('#storyId').val();
                window.location.replace(`/Volunteer/Story/RemoveDraftStory?storyId=${storyId}`);
            }
        })
    } else {
        window.location.href = "/Volunteer/Story/StoryList";
    }
})