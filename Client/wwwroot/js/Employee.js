$.ajax({
    url: "https://localhost:44315/api/Employees/GetEmployeeJobs"
}).done(res => {
    console.log(res)
})

$(document).ready(function () {
    $('#dataTable').DataTable({
        "filter": true,
        "ajax": {
            "url": "https://localhost:44315/api/Employees/GetEmployeeJobs",
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
                "data": "fullname",
                "autoWidth": true
            },
            {
                "data": "departmentName",
                "autoWidth": true
            },
            {
                "data": "jobName",
                "autoWidth": true
            },
            //{
            //    "data": "phone",
            //    "render": function (data, type, row) {
            //        if (row['phone'].startsWith('0')) {
            //            return `+62${row['phone'].substr(1)}`
            //        }
            //        return `+62${row['phone']}`
            //    },
            //    "autoWidth": true
            //},
            {
                "data": "roleName",
                "autoWidth": true
            },

            {
                "data": "email",
                "autoWidth": true
            },
        ]
    });
});