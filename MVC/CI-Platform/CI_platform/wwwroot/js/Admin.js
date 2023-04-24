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









