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
            // { "targets": 0, width: "15%" },
            {
                "targets": 10,
                "orderable": false,
                "render": function (data, type, row) {
                    return `
                        <button type="submit" class="btn btn-sm btn-outline-secondary" onclick="window.location.href='/admin/Vocabulary/CreateEdit/${data}'" value='${data}'
                             data-bs-toggle="tooltip" title="Edit"> <i class="bi bi-pencil-square"></i>
                        </button>
                        <button type="button" class="btn btn-sm btn-outline-info" onclick="window.location.href='/admin/Vocabulary/Details/${data}'" value='${data}'
                            data-bs-toggle="tooltip" title="Details"> <i class="bi bi-eye"></i> 
                        </button>
                        <button type="button" class="btn btn-sm btn-outline-primary" onclick="StatusUpdate('/Admin/Vocabulary/StatusUpdate/${data}')" value='${data}'
                            data-bs-toggle="tooltip" title="Status Update"> <i class="bi bi-activity"></i> 
                        </button>
                        <button type="submit" class="btn btn-sm btn-outline-danger show-bs-modal" data-id='${data}' value='${data}'
                            data-bs-toggle="tooltip" title="Delete"> <i class="bi bi-trash"></i>
                        </button>
                        

                        
                    `;
                }
            }
        ],
        // "pagingType": 'numbers',
        // "ordering": false,
        "scrollX": true,
        "autoWidth": true
        
    });
}

function StatusUpdate(url)
{
    $.ajax({
        url:url,
        type:'PUT',
        success:function (data){
            if(data.isSuccess){
                dataTable.ajax.reload(null, false);
                toastr.success(data.message);
            }
            else{
                toastr.error(data.message);
            }
        }
    });
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
    
