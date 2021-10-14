$(document).ready(function () {
    $('#dataTable').DataTable({
        "filter": true,
        "ajax": {
            "url": "https://localhost:44315/api/Employees/GetEmployee",
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
                "data" : "employeeId"
            },
            {
                "data": "fullName",
                "autoWidth": true
            },
            {
                "data": "phone",
                "render": function (data, type, row) {
                    if (row['phone'].startsWith('0')) {
                        return `+62${row['phone'].substr(1)}`
                    }
                    return `+62${row['phone']}`
                },
                "autoWidth": true
            },
            {
                "data": "gender", render: function (result) {
                    console.log(result)
                    var gender;
                    if (result === 0) {
                        gender = "Male"
                    } else {
                        gender = "Female"
                    }
                    return gender;
                },
                "orderable": false
            },
            {
                "data": "email",
                "autoWidth": true
            },
        ]
    });
});