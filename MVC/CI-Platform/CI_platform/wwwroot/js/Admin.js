// Get the element where the live time will be displayed
var liveTimeElement = document.getElementById("live-time");

// Update the live time every second
setInterval(function () {
    // Get the current date and time
    var currentTime = new Date();

    // Format the time as "hh:mm:ss AM/PM"
    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();
    var seconds = currentTime.getSeconds();
    var amOrPm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    seconds = seconds < 10 ? '0' + seconds : seconds;
    var formattedTime = hours + ':' + minutes + ':' + seconds + ' ' + amOrPm;

    // Format the date as "Day, Month Date, Year"
    var daysOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    var monthsOfYear = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var dayOfWeek = daysOfWeek[currentTime.getDay()];
    var month = monthsOfYear[currentTime.getMonth()];
    var date = currentTime.getDate();
    var year = currentTime.getFullYear();
    var formattedDate = dayOfWeek + ", " + month + " " + date + ", " + year;

    // Update the live time element
    liveTimeElement.innerHTML = formattedTime + " - " + formattedDate;
}, 1000); // Update every second (1000 milliseconds)
function DeleteMissionApplication(Id) {
    showDeleteConfirmation('/Admin/DeleteMissionApplication', { Id: Id }).then(() => {
        // Reload the page to show the updated story list
        location.reload();
    });
}

function DeleteBanner(Id) {
    showDeleteConfirmation('/Admin/DeleteBanner', { Id: Id }).then(() => {
        // Reload the page to show the updated user list
        location.reload();
    });
}

function DeleteUser(UserId) {
    showDeleteConfirmation('/Admin/DeleteUser', { UserId: UserId }).then(() => {
        // Reload the page to show the updated user list
        location.reload();
    });
}
function Delete_Admin_MissionApplication(Id) {
    showDeleteConfirmation('/Admin/Delete_Admin_MissionApplication', { Id: Id }).then(() => {
        // Reload the page to show the updated user list
        location.reload();
    });
}
function DeletemissionThemes(Id) {
    showDeleteConfirmation('/Admin/DeletemissionThemes', { Id: Id }).then(() => {
        // Reload the page to show the updated CMS list
        location.reload();
    });
}
function DeleteCms(Id) {
    showDeleteConfirmation('/Admin/DeleteCms', { Id: Id }).then(() => {
        // Reload the page to show the updated CMS list
        location.reload();
    });
}

function DeleteStory(Id) {
    showDeleteConfirmation('/Admin/DeleteStory', { Id: Id }).then(() => {
        // Reload the page to show the updated story list
        location.reload();
    });
}
function DeleteMission(Id) {
    showDeleteConfirmation('/Admin/DeleteMission', { Id: Id }).then(() => {
        // Reload the page to show the updated story list
        location.reload();
    });
}
function DeleteSkill(Id) {
    showDeleteConfirmation('/Admin/DeleteSkill', { Id: Id }).then(() => {
        // Reload the page to show the updated story list
        location.reload();
    });
}

function showDeleteConfirmation(url, data) {
    return Swal.fire({
        
        text: 'Are you sure you want to delete this item?',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            // If the user clicks on "Yes, delete it!", make an AJAX call to delete the data
            return $.ajax({
                url: url,
                type: 'POST',
                data: data
            }).then(() => {
                // If the data is deleted successfully, show a success message
                return Swal.fire(
                    'Deleted!',
                    'The data has been deleted.',
                    'success'
                );
            }).catch(() => {
                // If there's an error in making the AJAX call, show an error message
                return Swal.fire(
                    'Error!',
                    'An error occurred while deleting the data.',
                    'error'
                );
            });
        }
    });
}
function ApproveStatusStory(Id, status) {
    showStatusApproveConfirmation('/Admin/ApproveStatusStory', { Id: Id, status: status }).then(() => {
        location.reload();
    });
}
function ApproveStatusmissionThemes(Id, status) {
    showStatusApproveConfirmation('/Admin/ApprovemissionThemes', { Id: Id, status: status }).then(() => {
        location.reload();
    });
}
function ApproveStatusMissionApplication(Id, status) {
    showStatusApproveConfirmation('/Admin/ApproveStatusMissionApplication', { Id: Id, status: status }).then(() => {
  
    });
}

function showStatusApproveConfirmation(url, data) {
    let message = '';
    let successMessage = '';
    if (data.status === true) {
        message = 'Are you sure you want to Approve this Application?';
        successMessage = 'The data has been Approved.';
    } else {
        message = 'Are you sure you want to Decline this Application?';
        successMessage = 'The data has been Declined.';
    }
    return Swal.fire({
        text: message,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirm'
    }).then((result) => {
        if (result.isConfirmed) {
            // If the user clicks on "Confirm", make an AJAX call to approve or decline the data
            return $.ajax({
                url: url,
                type: 'POST',
                data: data
            }).then(() => {
                // If the data is approved or declined successfully, show a success message
                return Swal.fire(
                    'Success',
                    successMessage,
                    'success'
                );
            }).catch(() => {
                // If there's an error in making the AJAX call, show an error message
                return Swal.fire(
                    'Error!',
                    'An error occurred while approving or declining the data.',
                    'error'
                );
            });
        }
    });
}



function ViewStory(Id)
{
    $.ajax({
        url: "/Admin/AdminViewStory",
        type: "GET",
        data: { Id: Id },
        success: function (result) {
            $("#partialView").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
}
       function AdminCms(Id) {
            $.ajax({
                url: "/Admin/AdminAddCms",
                type: "GET",
                data: { Id: Id },
                success: function (result) {
                    $("#partialView").html(result);
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        }


function AdminUser(Id) {
    $.ajax({
        url: "/Admin/AdminAddUser",
        type: "GET",
        data: { Id: Id },
        success: function (result) {
            $("#partialViewContainer").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function AdminSkill(Id) {
    $.ajax({
        url: "/Admin/AdminSkill",
        type: "GET",
        data: { Id: Id },
        success: function (result) {
            $("#partialViewContainer").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function UpdateMissionTheme(Id) {
    $.ajax({
        url: "/Admin/UpdateMissionTheme",
        type: "GET",
        data: { Id: Id },
        success: function (result) {
            $("#partialViewContainer").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function AdminBanner(Id) {
    $.ajax({
        url: "/Admin/AdminAddBanner",
        type: "GET",
        data: { Id: Id },
        success: function (result) {
            $("#partialViewContainer").html(result);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

let files = [],
    docs = [],
    container = document.querySelector('.container-img'),
    doc_container = document.querySelector(".container-doc");

let defaultImageIndex = -1;




/** Function to get Upsert Mission partial view */
function GetUpsertMissionPage() {
    let MissionId = $(event.target).attr("value");
    let param = {
        MissionId: MissionId
    }

    $.ajax({
        url: "/AdminMission/GetUpsertMissionPage",
        data: param,
        success: function (data) {
            if (data == "SessionExpired") {
                location.reload();
            }
            else if (data == "Error") {
                location.href = "/InvalidRequest"
            }
            $("#mainContentBody").html(data);
            ClassicEditor
                .create(document.querySelector('#description'))
                .then(newEditor => {
                    descEditor = newEditor;
                    descEditor.model.document.on('change:data', () => {
                        $("#errDescription").html("")
                    });
                })
                .catch(error => {
                    console.error(error);
                });

            ClassicEditor
                .create(document.querySelector('#orgDetail'))
                .then(newEditor => {
                    orgDetailEditor = newEditor;
                    orgDetailEditor.model.document.on('change:data', () => {
                        $("#errOrgDetail").html("")
                    });
                })
                .catch(error => {
                    console.error(error);
                });
            changeFormFields();
            $("#images").on('change', addImages);
            $("#documents").on('change', addDocs);
            container = document.querySelector('.container-img')
            doc_container = document.querySelector(".container-doc")

            // Retreiving Old Images in files global array
            oldImgs = document.getElementsByClassName('oldImgs');
            for (let i = 0; i < oldImgs.length; i++) {
                img = oldImgs[i];
                let isDefault = $(oldImgs[i]).attr("value");
                console.log("isDefault:" + isDefault);
                let src = img.src;
                let fileName = src.substr(src.lastIndexOf('/') + 1)
                if (isDefault == 1) {
                    fetch(src)
                        .then(res => res.blob())
                        .then(blob => {
                            const file = new File([blob], fileName, blob)
                            files.push(file)
                            defaultImageIndex = files.indexOf(file);
                            showImages();
                            transferImages();
                        })
                }
                else {
                    fetch(src)
                        .then(res => res.blob())
                        .then(blob => {
                            const file = new File([blob], fileName, blob)
                            files.push(file)
                            showImages();
                            transferImages();
                        })
                }

            }

            // Retreiving Old Documents in docs global array
            oldDocs = document.getElementsByClassName('oldDoc');
            for (let i = 0; i < oldDocs.length; i++) {
                doc = oldDocs[i];
                let src = $(doc).attr("href");
                let fileName = doc.innerText;
                fetch(src)
                    .then(res => res.blob())
                    .then(blob => {
                        const file = new File([blob], fileName, blob)
                        docs.push(file)
                        showDocs();
                        transferDocs();
                    })
            }
        },
        error: function (err) {
            console.error(err);
        }
    });
}

/** Function for removing validation on input change */
function removeValidation() {
    let targetId = $(event.target).attr("id");
    let validationStrings = document.getElementsByClassName("text-danger");

    //if (targetId == "countrySelect") {
    //    getCityList();
    //}
    //else 
    if (targetId == "typeSelect") {
        changeFormFields();
    }
    else if (targetId == "startDate") {
        restrictEndDate();
    }

    for (const item of validationStrings) {
        if ($(item).attr("for") == targetId) {
            $(item).html("");
            break;
        }
    }
}

/** This function will set EndDate input min restriction according to start date */
function restrictEndDate() {
    let startDate = $("#startDate").val();
    if (startDate != "") {
        $("#endDate").attr("min", startDate);
    }
    else {
        const date = new Date();
        $("#endDate").attr("min", date.toISOString().substring(0, 10));
    }
}

/** This function will display appropriate form field for Mission Type */
function changeFormFields() {
    let missionType = $("#typeSelect").val();
    if (missionType == "Time") {
        $("#totalSeatsDiv").removeClass("d-none");
        $("#deadlineDiv").removeClass("d-none");
        $("#goalTextDiv").addClass("d-none");
        $("#goalValueDiv").addClass("d-none");

        $("#goalText").val("")
        $("#goalValue").val("")
        $("#errGoalText").html("")
        $("#errGoalValue").html("")
    }
    else if (missionType == "Goal") {
        $("#totalSeatsDiv").addClass("d-none");
        $("#deadlineDiv").addClass("d-none");
        $("#goalTextDiv").removeClass("d-none");
        $("#goalValueDiv").removeClass("d-none");

        $("#totalSeats").val("")
        $("#deadline").val("")
        $("#errTotalSeats").html("")
        $("#errDeadline").html("")
    }
    else {
        $("#totalSeatsDiv").addClass("d-none");
        $("#deadlineDiv").addClass("d-none");
        $("#goalTextDiv").addClass("d-none");
        $("#goalValueDiv").addClass("d-none");

        $("#goalText").val("")
        $("#goalValue").val("")
        $("#errGoalText").html("")
        $("#errGoalValue").html("")
        $("#totalSeats").val("")
        $("#deadline").val("")
        $("#errTotalSeats").html("")
        $("#errDeadline").html("")
    }
}

/** It will fetch City list for selected country */
function getCityList() {
    let countryId = $("#countrySelect").val();
    $.ajax({
        url: "/AdminMission/GetCityListMission?countryId=" + countryId,
        success: function (data) {
            if (data == "SessionExpired") {
                location.reload();
            }
            else if (data == "Error") {
                location.href = "/InvalidRequest"
            }
            $("#cityListDiv").html(data);
        },
        error: function (err) {
            console.error(err);
        }
    });
}

/** This function will add images to files global array on image input change event */
function addImages() {
    let file = event.target.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != 'image') continue;

        if (file[i].size > 4 * 1024 * 1024) {
            $("#errImage").html("file size cannot be greater than 4MB!");
            break;
        }
        else {
            $("#errImage").html("");
        }

        if (!files.some(e => e.name == file[i].name))
            files.push(file[i])
    }

    showImages();
    transferImages();
}

/** This function will add images to docs global array on document input change event */
function addDocs() {
    let file = event.target.files;

    // if user select no image
    if (file.length == 0) return;

    for (let i = 0; i < file.length; i++) {
        if (file[i].type.split("/")[0] != 'application') continue;
        if (!docs.some(e => e.name == file[i].name))
            docs.push(file[i])
    }

    showDocs();
    transferDocs();
}

/** This will show uploaded images for preview */
function showImages() {
    var temp = ""
    for (let i = 0; i < files.length; i++) {
        if (i == defaultImageIndex) {
            temp += `<div class="image">
                        <input type="radio" name="DefaultSelect" id="img_${i}" value="${i}" checked />
			            <span onclick="delImage(${i})">&times;</span>
			            <img src="${URL.createObjectURL(files[i])}" />
			        </div>`;
        }
        else {
            temp += `<div class="image">
                        <input type="radio" name="DefaultSelect" id="img_${i}" value="${i}" />
			            <span onclick="delImage(${i})">&times;</span>
			            <img src="${URL.createObjectURL(files[i])}" />
			        </div>`;
        }
    }

    container.innerHTML = temp;
}

/** This will show uploaded documents for preview */
function showDocs() {
    var temp = ""
    for (let i = 0; i < docs.length; i++) {
        temp += `<div class="doc-pill">
                        <a class="mb-0 me-3 nav-link" target="_blank" href="${URL.createObjectURL(docs[i])}">${docs[i].name}</a>
                        <span onclick="deleteDoc(${i})">X</span>
                    </div>`;
    }

    doc_container.innerHTML = temp;
}


function delImage(index) {
    files.splice(index, 1);
    showImages();
    transferImages();
}


function deleteDoc(index) {
    docs.splice(index, 1);
    showDocs();
    transferDocs();
}

/** This function will transfer images from files array to image input filelist */
function transferImages() {
    const dt = new DataTransfer();
    for (const img of files) {
        dt.items.add(img);
    }
    document.getElementById("images").files = dt.files;
}

/** This function will transfer documents from files array to document input filelist */
function transferDocs() {
    const dt = new DataTransfer();
    for (const doc of docs) {
        dt.items.add(doc);
    }
    document.getElementById("documents").files = dt.files;
}

function validateMissionForm() {
 
    let flag = 0;

    if ($("#title").val() == "") {
        $("#errTitle").html("Please Enter Mission Title!");
        flag = 1;
    }
    else if ($("#title").val().length > 128) {
        $("#errTitle").html("Mission Title can have only 128 characters!");
        flag = 1;
    }
    else {
        $("#errTitle").html("");
    }

    if ($("#shortDesc").val() == "") {
        $("#errShortDesc").html("Please Enter Short Description for Mission!")
        flag = 1;
    }
    else {
        $("#errShortDesc").html("");
    }

    const descData = descEditor.getData();
    $("#description").val(descData);

    if ($("#description").val() == "") {
        $("#errDescription").html("Please Enter Description for Mission!")
        flag = 1;
    }
    else {
        $("#errDescription").html("");
    }

    if ($("#orgName").val() == "") {
        $("#errOrgName").html("Please Enter Mission Organization Name!");
        flag = 1;
    }
    else if ($("#orgName").val().length > 255) {
        $("#errOrgName").html("Organization Name can have only 255 characters!");
        flag = 1;
    }
    else {
        $("#errOrgName").html("");
    }

    const orgDetailData = orgDetailEditor.getData();
    $("#orgDetail").val(orgDetailData);

    if ($("#orgDetail").val() == "") {
        $("#errOrgDetail").html("Please Enter Mission Organization Details!")
        flag = 1;
    }
    else {
        $("#errOrgDetail").html("");
    }

    if ($("#countrySelect").val() == 0) {
        $("#errCountry").html("Please Select Mission Country!");
        flag = 1;
    }
    else {
        $("#errCountry").html("");
    }

    if ($("#citySelect").val() == 0) {
        $("#errCity").html("Please Select Mission City!");
        flag = 1;
    }
    else {
        $("#errCity").html("");
    }

    if ($("#startDate").val() == "") {
        $("#errStartDate").html("Please Select Mission StartDate!")
        flag = 1;
    }
    else {
        $("#errStartDate").html("")
    }

    if ($("#endDate").val() == "") {
        $("#errEndDate").html("Please Select Mission EndDate!")
        flag = 1;
    }
    else if ($("#startDate").val() != "" && $("#endDate").val() < $("#startDate").val()) {
        $("#errEndDate").html("Mission End Date must be greater than or equal to Mission Start Date!")
        flag = 1;
    }
    else {
        $("#errEndDate").html("")
    }

    /* Mission Type Validations */

    if ($("#typeSelect").val() == 0) {
        $("#errType").html("Please Select Mission Type!");
        flag = 1;
    }
    else {
        $("#errType").html("");
    }

    /* TotalSeats and Mission Deadline Validations (For Mission Type = 'Time') */

    if ($("#typeSelect").val() == "Time") {
        if ($("#totalSeats").val() == "") {
            $("#errTotalSeats").html("Mission Total Seats cannot be empty!")
            flag = 1;
        }
        else if (isNaN(Number.parseInt($("#totalSeats").val()))) {
            $("#errTotalSeats").html("Mission Total Seats must be a number!")
            flag = 1;
        }
        else {
            $("#errTotalSeats").html("")
        }

        if ($("#deadline").val() == "") {
            $("#errDeadline").html("Please Select Mission Deadline!")
            flag = 1;
        }
        else if ($("#endDate").val() != "" && $("#deadline").val() > $("#endDate").val()) {
            $("#errDeadline").html("Mission Deadline must be less or equal to mission end date!")
            flag = 1;
        }
        else {
            $("#errDeadline").html("")
        }
    }

    /* Goal Value and Goal Text Validations (For Mission Type = 'Goal') */

    if ($("#typeSelect").val() == "Goal") {
        if ($("#goalValue").val() == "") {
            $("#errGoalValue").html("Mission Goal value cannot be empty!")
            flag = 1;
        }
        else if (isNaN(Number.parseInt($("#goalValue").val()))) {
            $("#errGoalValue").html("Mission Goal value must be a number!")
            flag = 1;
        }
        else {
            $("#errGoalValue").html("")
        }

        if ($("#goalText").val() == "") {
            $("#errGoalText").html("Please Enter Mission Goal Objective Text!")
            flag = 1;
        }
        else {
            $("#errGoalText").html("")
        }
    }

    /* Mission Theme Validations */

    if ($("#themeSelect").val() == 0) {
        $("#errTheme").html("Please Select Mission Theme!");
        flag = 1;
    }
    else {
        $("#errTheme").html("");
    }

    /* Mission Availabilty Validations */

    if ($("#availabilitySelect").val() == "None") {
        $("#errAvailabilty").html("Please Select Mission Availability")
        flag = 1;

    }
    else {
        $("#errAvailabilty").html("")
    }

    /* Video URL Validations */
    let urls = $("#videoUrl").val();
    if (!urls.includes("http") && urls != "") {
        $("#errVideoUrl").html("Please Enter a Valid URL");
        flag = 1;
    }
    else if (urls != "") {
        let urlArr = urls.split("http").map(x => { return "http" + x }).slice(1);
        let regex = /^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$/gm
        let urlFlag = 0;
        urlArr.forEach(function (ele, index) {
            let i = ele.match(regex);
            if (i == null) {
                urlFlag++;
            }
        });

        if (urlFlag != 0) {
            var err = urlFlag + " out of " + urlArr.length + " url are not valid Youtube URL";
            $("#errVideoUrl").html(err);
            flag = 1;
        }
        else {
            if (urlArr.length == 1 && urls.length > 100) {
                $("#errVideoUrl").html("Please separate all url by comma, space or enter!")
                flag = 1;
            }
            else if (urlArr.length > 20) {
                $("#errVideoUrl").html("You can only enter 20 url at one time!")
                flag = 1;
            }
            else {
                $("#errVideoUrl").html("")
            }
        }
    }



    if (flag != 0) {
        return false;
    }
}



