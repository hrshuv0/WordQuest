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
            { "targets": 7, width: "15%" },
            {
                "targets": 9,
                "width": "15%",
                "orderable": false,
                "render": function (data, type, row) {
                    return `
                        <button type="submit" class="btn btn-sm btn-outline-info" onclick="window.location.href='/admin/Vocabulary/CreateEdit/${data}'" value='${data}'>
                            <i class="bi bi-pencil-square"></i> Edit
                        </button>
                        <button type="submit" class="btn btn-sm btn-outline-danger show-bs-modal" data-id='${data}' value='${data}'>
                            <i class="bi bi-trash"></i> Delete
                        </button>
                    `;
                }
            }
        ],
        // "pagingType": 'numbers',
        // "ordering": false,
        "scrollX": true
        
    });
}

function StatusUpdate(url)
{
    toastr.info("Status Update Clicked");
}

$('#tblData').on('click', '.show-bs-modal', function (event) {
    var id = $(this).data("id");
    var modal = $("#modal-default");
    modal.find('.modal-body p').text('Are you sure you want to delete this record?');
    $("#deleteId").val(id);
    $("#deleteForm").attr("action", "/Admin/Vocabulary/Delete");
    modal.modal('show');
});
$("#deleteButton").click(function () {
    $("#deleteForm").submit();
});
    
