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
        "ajax": {
            "url": "/Admin/Vocabulary/GetAll",
        },
        "columns": [
            { "data": "id", "width": "30%" },
            { "data": "name", "width": "30%" },
            { "data": "status", "width": "30%" },
        ],
        "pagingType": 'numbers',
        "ordering": false
        
    });
}