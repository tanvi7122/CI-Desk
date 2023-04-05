////$('.recommend-story-btn').click(function () {
////    var button = $(this)
////    var storyId = $(this).data('story-id');
////    var missionId = $(this).data('mission-id');
////    var fromuserId = $(this).data('fromuser-id');
////    var touserId = $(this).data('touser-id');
////    console.log(missionId);
////    console.log(fromuserId);
////    console.log(touserId);
////    $.ajax({
////        url: '/Story/RecommendToCoWorker',
////        type: 'POST',
////        data: { storyId: storyId, missionId: missionId, fromuserId: fromuserId, touserId: touserId },
////        success: function (result) {
////            // Show a success message or update the UI
////            console.log(missionId)
////            console.log(fromuserId)
////            console.log(touserId)
////            var allrecommendedId = $('.recommend-story-btn')
////            allrecommendedId.each(function () {
////                if ($(this).data('story-id') === storyId && $(this).data('mission-id') === missionId && $(this).data('fromuser-id') == fromuserId && $(this).data('touser-id') == touserId) {
////                    if ($(this).hasClass('btn-primary')) {
////                        $(this).addClass('btn-success')
////                        $(this).removeClass('btn-primary')
////                        $(this).html('Recommended')
////                        console.log("added")
////                    }
////                    else {
////                        $(this).addClass('btn-primary')
////                        $(this).removeClass('btn-success')
////                        $(this).html('Recommend')
////                        console.log("remove")
////                    }
////                }
////            })
////        },
////        error: function (error) {
////            // Show an error message or handle the error
////            console.log("error")

////        }
////    });
////});



// share story
let files = [],
    dragArea = document.querySelector('.drag-area'),
    input = document.querySelector('.drag-area input'),
    button = document.querySelector('.drag-card button'),
    select = document.querySelector('.drag-area .select'),
    container = document.querySelector('.container-img');

/** CLICK LISTENER */
select.addEventListener('click', () => input.click());

/* INPUT CHANGE EVENT */
input.addEventListener('change', () => {
    let file = input.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != 'image') continue;
        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
    }

    showImages();
});

/** SHOW IMAGES */
function showImages() {
    container.innerHTML = files.reduce((prev, curr, index) => {
        return `${prev}
		    <div class="image">
			    <span onclick="delImage(${index})">&times;</span>
			    <img src="${URL.createObjectURL(curr)}" />
			</div>`
    }, '');
    for (let i = 0; i < files.length; i++) {
        console.log(files[i].name)
    }
}

/* DELETE IMAGE */
function delImage(index) {
    files.splice(index, 1);
    showImages();
}

/* DRAG & DROP */
dragArea.addEventListener('dragover', e => {
    e.preventDefault()
    dragArea.classList.add('dragover')
})

/* DRAG LEAVE */
dragArea.addEventListener('dragleave', e => {
    e.preventDefault()
    dragArea.classList.remove('dragover')
});

/* DROP EVENT */
dragArea.addEventListener('drop', e => {
    e.preventDefault()
    dragArea.classList.remove('dragover');

    let file = e.dataTransfer.files;
    for (let i = 0; i < file.length; i++) {
        /** Check selected file is image */
        if (file[i].type.split("/")[0] != 'image') continue;

        if (!files.some(e => e.name == file[i].name)) files.push(file[i])
    }
    showImages();
});

function imageTransfer() {
    var dt = new DataTransfer();
    for (let i = 0; i < files.length && i < 20; i++) {
        dt.items.add(files[i]);
    }
    document.querySelector("#upload-files").files = dt.files;
}

$(".saveStory , .submitStory").click(function (e) {
    e.preventDefault();
    imageTransfer();


    var value = $(this).val();
    var formData = new FormData();
    formData.append('MissionTitle', $('#shareStoryMission').val());
    formData.append('Title', $('#storyTitle').val());

    var contenttext = CKEDITOR.instances.editor.getData();
    /* formData.append('Description', $('#editors').val());*/
    formData.append('Description', contenttext);
    formData.append('Date', $('#storyDate').val());

    formData.append('Value', value);

    var videoUrls = $('#storyURL').val().split('\n');
    for (var i = 0; i < videoUrls.length && i < 20; i++) {
        var videoUrl = videoUrls[i].trim();
        if (videoUrl) {
            formData.append('VideoUrl[' + i + ']', videoUrl);
        }
    }

    $.each($('#upload-files')[0].files, function (i, file) {
        formData.append('Photos', file);
    });

    console.log(formData)

    $.ajax({
        type: 'POST',
        url: "/Story/AddStory",
        data: formData,
        processData: false,
        contentType: false,

        success: function (message) {
            console.log("story save");

            if (value == 1) {
                console.log($('#shareStoryMission').val())
                //$(".alert-success").html("Story Saved Sucessfully with Status Draft");
                //$(".alert-success").slideToggle().delay(2000);
                $('.submitStory').prop('disabled', false);
                $('#previewStory').prop('disabled', false);
                const previewBtn = document.getElementById("previewStory");
                previewBtn.addEventListener("click", (e) => {
                    e.preventDefault();
                    const missionId = $('#shareStoryMission').val();
                    const newUrl = `/Story/PreviewStory?missionId=${missionId}`;
                    console.log(newUrl);
                    /*  window.location.href = newUrl;*/
                    window.open(newUrl,'_blank');
                });
            }
            else {
                //$(".alert-success").html("Story Submitted Sucessfully with Status Pending");
                //$(".alert-success").slideToggle().delay(2000);

                alert("Story Added Successfully with Pending Status!!Admin Approve it soon");

                var element = document.getElementById("storyFormData");
                element.reset();
                CKEDITOR.instances.editor.setData('');
                console.log(container.innerHTML);
                container.innerHTML = " ";
                console.log(container.innerHTML);
                //var imagepreviewContainer = $('#imagesShow').val();
                //console.log("HEllo" + imagepreviewContainer);
                //imagepreviewContainer.innerHTML = " ";


                $('.submitStory').prop('disabled', true);
                $('#previewStory').prop('disabled', true);


               
            }


        },
        error: function (error) {
            console.log(error);
            console.log("error in adding story")
        }


    });


});



$('#shareStoryMission').on('change', function () {
    var missionId = $('#shareStoryMission').val();
    files.length = 0;

   

    $.ajax({
        type: "GET",
        url: "/Story/ShareYourStory",

        success: function () {

            $.ajax({
                type: "GET",
                url: "/Story/GetDraftStory",
                data: { missionId: missionId },
                success: function (story) {

                    if (story == "nodraft") {

                        $('#storyTitle').val('');
                        /*  tinymce.get('editorhtml').setContent('');*/
                        CKEDITOR.instances.editor.setData(' ');
                        $('#storyDate').val('');
                        $('#storyurl').val('');
                        files.length = 0;
                        showImages();
                        $('.submitStory').prop('disabled', true);

                    }
                    else {

                        CKEDITOR.instances.editor.setData($(story.value.description).text());
                        //tinymce.get('editorhtml').setContent(plainTextDescription);


                        $('#storyTitle').val(story.value.title);
                        $('#storyDate').val(story.value.date);



                        let videoUrls = '';

                        story.value.storyMediaVideoUrl.forEach(media => {
                            {
                                videoUrls += media + '\n';
                            }
                        });
                        let Upload = '';
                        story.value.storyMediaPath.forEach(media => {
                            fetch('/Upload/' + media)
                                .then(response => response.blob())
                                .then(blob => {
                                    const file = new File([blob], media, { type: blob.type });
                                    files.push(file);
                                    showImages();
                                    console.log(files);
                                    UPLOADED_FILES = document.querySelectorAll(".js-remove-image");
                                    removeFile();
                                })
                                .catch(error => {
                                    console.error('Error fetching file:', error);
                                    // Handle error, such as displaying a message to the user
                                });
                        });






                        $('#upload-files').val(Upload);
                        $('#storyURL').val(videoUrls);
                        $('.submitStory').prop('disabled', false);

                        
                      
                    }
                },
                error: function (error) {
                    alert("Error getting story data.");
                    console.log(error)
                }
            });

        },
        error: function () {
            consol.log("error")
        }
    });
})