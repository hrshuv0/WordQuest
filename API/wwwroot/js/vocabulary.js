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
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "30%" },
            { "data": "status", "width": "10%"},
            { "data": "id",
                "render": function (data) {
                return `
                    <div class="w-75 btn-group" role="group">
                    <a href="/Admin/Vocabulary/CreateEdit?id=${data}"
                        class="btn btn-sm btn-outline-secondary m-2">
                        <i class="bi bi-pencil-square"></i> Edit
                    </a>
                    <a onclick="StatusUpdate('/Admin/Vocabulary/StatusUpdate/${data}')"
                        class="btn btn-sm btn-outline-info m-2">
                        <i class="bi bi-info-circle"></i> Status
                    </a>
                    <a onclick="Delete('/Admin/Vocabulary/Delete/${data}')"
                        class="btn btn-sm btn-outline-danger m-2">
                        <i class="bi bi-trash-fill"></i> Delete
                    </a>
                    
                    
                    </div>
                    `
                },
                "width": "30%"
            },
        ],
        "columnDefs": [
            { "className": "dt-head-center", "targets": "_all"},
            { "className": "dt-body-center", "targets": [2, 3]},
        ],
        "pagingType": 'numbers',
        "ordering": false
        
    });
}

function StatusUpdate(url)
{
    alert("Status Update Clicked");
}

function Delete(url)
{
    alert("Delete Clicked");
}