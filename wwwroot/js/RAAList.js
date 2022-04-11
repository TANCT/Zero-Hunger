var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/receiveraccountapplication",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "receiverIC"},
            { "data": "receiverName" },
            { "data": "receiverOccupation"},
            {
                "data":"receiverSalaryGroup.salaryRange"
                //"data": "receiverSalaryGroupID",
                //"render": function (data) {
                //    return `@Html.DisplayFor(SalaryGroup => Model.salaryGroups.ElementAt(${data}).salaryRange)`
                //}
            },

            { "data": "receiverFamilyNo" },
            {
                "data": "receiverAdrs1",
                "render": function (data, type, row) {
                    return data + '<br>' + row.receiverAdrs2;
                }
                    },
            {
                "data": "applicationStatusID",
                "render": function (data) {
                    if (data == 0) {
                        return "New!";
                    }
                    else if (data == 1) {
                        return "Processing...";
                    }
                }
            },
            {
                "data": "receiverIC",
                "render": function (data) {
                    return `
                        <a href="/ReceiverAccountApplication/ViewReceiverAccountApplication?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Details
                        </a>`;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}
