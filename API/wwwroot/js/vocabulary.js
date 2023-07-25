
var dataTable;

$(document).ready( function () {
    loadDataTable();
} );

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Vocabulary/GetAll",
            "type": "POST",
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "status", "width": "30%" },
        ],
    });
}