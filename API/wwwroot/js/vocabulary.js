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
            { "data": "name", "width": "10%" },
            { "data": "definition"},
            { "data": "partOfSpeech"},
            { "data": "pronunciation"},
            { "data": "example"},
            { "data": "translation"},
            { "data": "difficultyLevel"},
            { "data": "createdTime"},
            { "data": "status",},
            { "data": "id",
                "render": function (data) {
                return `
                    <div class="w-50 btn-group" role="group">
                    <a href="/Admin/Vocabulary/CreateEdit?id=${data}"
                        class="btn btn-sm btn-outline-secondary m-2">
                        <i class="bi bi-pencil-square"></i>
                    </a>
                    <a onclick="StatusUpdate('/Admin/Vocabulary/StatusUpdate/${data}')"
                        class="btn btn-sm btn-outline-info m-2">
                        <i class="bi bi-info-circle"></i>
                    </a>
                    <a onclick="Delete('/Admin/Vocabulary/Delete/${data}')"
                        class="btn btn-sm btn-outline-danger m-2">
                        <i class="bi bi-trash-fill"></i>
                    </a>
                    
                    
                    </div>
                    `
                },
                "width": "20%"
            },
        ],
        "columnDefs": [
            { "className": "dt-head-center", "targets": "_all"},
            { "className": "dt-body-center", "targets": [9]},
        ],
        "pagingType": 'numbers',
        "ordering": false,
        "scrollX": true
        
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