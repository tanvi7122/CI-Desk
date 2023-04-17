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
