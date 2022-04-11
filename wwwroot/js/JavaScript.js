var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/cookreservation",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "date", "width": "25%" }, 
            { "data": "reservationRefCook.shopName", "width": "25%" },
            { "data": "reservationRefCook.cookName", "width": "25%" },
            { "data": "status", "width": "25%" },
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}


