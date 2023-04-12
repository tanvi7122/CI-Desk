
  $(document).ready(function() {
    var table = $('#timebase').DataTable( {
        lengthChange: false,
        buttons: [ 'copy', 'excel', 'pdf', 'colvis' ]
    } );
 
    table.buttons().container()
        .appendTo( '#timebase_wrapper .col-md-6:eq(0)' );
} );

      $(document).ready(function() {
        var table = $('#goalbase').DataTable( {
            lengthChange: false,
            buttons: [ 'copy', 'excel', 'pdf', 'colvis' ]
        } );
     
        table.buttons().container()
            .appendTo( '#goalbase_wrapper.col-md-6:eq(0)' );
      });
$(document).ready(function () {
    $('#submittimesheet').click(function (e) {
        e.preventDefault(); // prevent the form from submitting normally

        // get the form data
        var formData = {
            MissionId: $('#Mission').val(),
            DateVolunteered: $('#DateVolunteered').val(),
            Hour: $('#Hour').val(),
            minutes: $('#minutes').val(),
            Notes: $('#Message').val()
        };

  
        $.ajax({
            type: 'POST',
            url: '/TimeSheet/AddTimeSheet',
            data: formData,
            success: function (result) {
                // handle the success response here
                alert('Data saved successfully!');
            },
            error: function (error) {
                // handle the error response here
                console.log(error);
                alert('Error: ' + error);
            }
        });
    });
});

$(document).ready(function () {
    $('#submitGoalsheet').click(function (e) {
        e.preventDefault(); // prevent the form from submitting normally

        // get the form data
        var formdata = {
            MissionId: $('#Mission').val(),
            Action: $('#Action').val(),
            DateVolunteered: $('#DateVolunteered').val(),
            Notes: $('#Message').val()
        };


        $.ajax({
            type: 'POST',
            url: '/TimeSheet/AddGoalSheet',
            data: formdata,
            success: function (result) {
                // handle the success response here
                alert('Data saved successfully!');
            },
            error: function (error) {
                // handle the error response here
                console.log(error);
                alert('Error: ' + error);
            }
        });
    });
});
success: function (data) {
    var tableBody = $('#timebase');
    tableBody.empty();

    $.each(data, function (i, item) {
        var row = $('<tr>');
   
        $('<td>').text(item.MissionId).appendTo(row);
        $('<td>').text(item.DateVolunteered).appendTo(row);
   
        $('<td>').text(item.Notes).appendTo(row);
        tableBody.append(row);
    });
},
