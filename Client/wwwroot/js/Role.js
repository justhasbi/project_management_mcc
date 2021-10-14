$(document).ready(function () {
    $('#dataTable').DataTable({
        "filter": true,
        "ajax": {
            "url": "https://localhost:44315/api/Roles/GetRoles",
            "dataType": "json",
            "dataSrc": ""
        },
        "columns": [
            {
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
                "orderable": false
            },
            {
                "data": "employeeId"
            },
            {
                "data": "fullName",
                "autoWidth": true
            },
            {
                "data": "roleId",
                "autoWidth": true
            },
            {
                "data": "roleName",
                "autoWidth": true
            }
        ]
    });
});