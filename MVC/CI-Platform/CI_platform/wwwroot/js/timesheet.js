
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
    } );
       