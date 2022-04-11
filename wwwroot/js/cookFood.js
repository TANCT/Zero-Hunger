var foodTable;

$(document).ready(function () {
    loadFoodTable();
});

function loadFoodTable() {
    foodTable = $('#food_load').DataTable({
        "ajax": {
            "url": "/api/cookfood",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [

            /*{ "data": "cookID", "width": "5%" },*/
            { "data": "cookName", "width": "8%" },
            { "data": "cookQuantity", "width": "3%" },

            //{ "data": "cookLongtitude", "width": "5%" },
            //{ "data": "cookLatitude", "width": "5%" },

            {
                "data": "openDate",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    if (date.getHours() == 0) {
                        var hours = "00";
                    } else {
                        var hours = date.getHours();
                    }
                    if (date.getMinutes() == 0) {
                        var min = "00";
                    } else {
                        var min = date.getMinutes();
                    }
                    return (month.length > 1 ? month : "" + month) + "/" + date.getDate() + "/" + date.getFullYear() +
                        "<br>" + hours + ":" + min;
                }, "width": "8%"
            },

            {
                "data": "closeDate",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    if (date.getHours() == 0) {
                        var hours = "00";
                    } else {
                        var hours = date.getHours();
                    }
                    if (date.getMinutes() == 0) {
                        var min = "00";
                    } else {
                        var min = date.getMinutes();
                    }
                    return (month.length > 1 ? month : "" + month) + "/" + date.getDate() + "/" + date.getFullYear() +
                        "<br>" + hours + ":" + min;
                }, "width": "8%"
            },

            {
                "data": "remainQuantity",
                 "width": "15%"
            },

            {
                "data": "cookID",
                "render": function (data, type, row) {
                    return `<div class="text-center"><a class='btn btn-info text-white' style='cursor:pointer;'
                             href=DonationApplier?cookFood=${data}>` + row.reservation + '</a>';
                },
                "width": "5%"
            },
            { "data": "shopName", "width": "8%" },
            {
                "data": "cookID",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href='/CookFoodPage/EditCookFood?id=${data}' class='btn btn-success text-white' style='cursor:pointer; width:80px;'>
                 Edit <i class="bi bi-pencil-square"></i>
                        </a>
                        <a class='btn btn-info text-white' style='cursor:pointer; width:80px;'
                             onclick=End('/api/cookfood/End?endID='+${data})>
                            End <i class="bi bi-x-octagon"></i>
                        </a>
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;'
                             onclick=Delete('/api/cookfood?id='+${data})>
               Delete <i class="bi bi-trash-fill"></i>
                        </a>
                        </div>`;
                }, "width": "45%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}



function Add(url) {
    //swal({
    //    title: "Are you sure?",
    //    text: "Once ended, the donation will not be displayed on map.",
    //    icon: "warning",
    //    buttons: true,
    //    dangerMode: true
    //}).then((willAdd) => {
    //    if (willAdd) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
    });
        //}
        //});

}

function Minus(url) {
//    swal({
//        title: "Are you sure?",
//        text: "Once ended, the donation will not be displayed on map.",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//}).then((willMinus) => {
//        if (willMinus) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
    //    }
    //});
}

function End(url) {
    swal({
        title: "Are you sure?",
        text: "Once ended, the donation will not be displayed on map.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willEnd) => {
        if (willEnd) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
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
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
