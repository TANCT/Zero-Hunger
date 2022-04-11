var userTable;

$(document).ready(function () {
    loadUserTable();
});

function loadUserTable() {
    userTable = $('#user_load').DataTable({
        "ajax": {
            "url": "/api/userlist",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            
            { "data": "userID", "width": "5%" },
            { "data": "typeId", "width": "5%" },
            { "data": "userName", "width": "5%" },
            
            { "data": "userPwd", "width": "5%" },
            { "data": "userEmail", "width": "5%" },
            
            { "data": "userPhone", "width": "5%" },
            
            { "data": "userBirth", "width": "8%" },
            { "data": "userAdrs1", "width": "10%" },
            { "data": "userAdrs2", "width": "10%" },

            {
                "data": "userID",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/UserPage/EditUser?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                             onclick=Delete('/api/userList?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
