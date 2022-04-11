var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/dfdHistory",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "dryFoodName", "width": "20%" },
            { "data": "dryFoodQuantity", "width": "20%" },
            { "data": "deliveryMethod", "width": "20%" },
            { "data": "dryFoodPickDate", "width": "20%" },
            { "data": "dryFoodRemark", "width": "20%" },
            
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100vw"
    });
}


