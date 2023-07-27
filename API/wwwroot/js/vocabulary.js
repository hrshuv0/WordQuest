var dataTable;

$(document).ready( function () {
    loadDataTable();
} );

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "processing": true,
        "serverSide": true,
        "retrieve": true,
        "responsive": true,
        "destroy": true,
        "ajax": {
            "url": "/Admin/Vocabulary/GetAll",
        },        
        "columnDefs": [
            { "className": "dt-head-center", "targets": "_all"},
            { "className": "dt-body-center", "targets": [9]},
        ],
        // "pagingType": 'numbers',
        // "ordering": false,
        // "scrollX": true
        
    });
}

function StatusUpdate(url)
{
    toastr.info("Status Update Clicked");
}

function Delete(url)
{
    toastr.warning("Delete Clicked");
}