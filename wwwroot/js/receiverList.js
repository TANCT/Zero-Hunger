var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/receiverlist",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "receiver.receiverIC" },
            { "data": "receiver.receiverName" },
            { "data": "receiver.receiverOccupation" },
            {
                "data": "receiver.receiverSalaryGroup.salaryRange"
                //"data": "receiverSalaryGroupID",
                //"render": function (data) {
                //    return `@Html.DisplayFor(SalaryGroup => Model.salaryGroups.ElementAt(${data}).salaryRange)`
                //}
            },

            { "data": "receiver.receiverFamilyNo" },
            {
                "data": "receiver.receiverAdrs1",
                "render": function (data, type, row) {
                    return data + '<br>' + row.receiver.receiverAdrs2;
                }
            },
            {
                "data": "receiver.applicationStatusID",
                "render": function (data) {
                    if (data == 2) {
                        return "Seen";
                    }
                    else if (data == 3) {
                        return "New Updates!";
                    }
                }
            },
            { "data": "date" },
            {
                "data": "receiver.receiverIC",
                "render": function (data) {
                    return `
                        <a href="/ReceiverList/ViewReceiver?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
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
