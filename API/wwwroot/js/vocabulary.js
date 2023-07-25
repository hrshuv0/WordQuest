var dataTable;

$(document).ready( function () {
    loadDataTable();
} );

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Admin/Vocabulary/GetAll",            
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "status", "width": "30%" },
        ],
    });
}