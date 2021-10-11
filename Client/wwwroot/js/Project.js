
$('.project-form').submit(e => {
    e.preventDefault();

    var data = new Object();
    data.ManagerId = parseInt($('#managerId').val())
    data.Name = $('#project-name').val()
    data.Description = $('#description').val()
    data.Status = 0

    $.ajax({
        url: 'Projects/',
        type: 'post',
        dataType: 'json',
        //contentType: 'application/json; charset=utf-8',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        success: function (data) {
            swal({
                title: "Success Create Project",
                icon: "success"
            })
        },
        error: function () {
            swal({
                title: "Failed Create Project",
                icon: "success"
            })

        }
    });
});


$('document').ready(() => {

    var managerId = $('.mId').text()
    $.ajax({
        url: "https://localhost:44314/projects/getmanagerid/" + managerId,
        method: "GET",
    }).done(res => {
        var htmlItem = ""
        
        res.forEach(item => {
            if (item.status === 0) {
                item.status = "Unstarted"
            } else if (item.status === 1) {
                item.status = "Started"
            } else {
                item.status = "Completed"
            }

            htmlItem += `
                <div class="col-xl-3 col-md-6 mt-4">
                    <a onClick="redirectPage('projects/projectdetail', ${item.id})" style="text-decoration:none; cursor:pointer;">
                        <div class="card border-left-primary shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-lg font-weight-bold text-primary text-uppercase mb-1">
                                            ${item.name}
                                        </div>
                                        <div class="text-secondary h5 mb-0 text-md  mt-2">${item.status}</div>
                                    </div>
                                    <div class="col-auto">
                                    <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                `
        });

        $('.project-wrapper').html(htmlItem);
    });

})


const redirectPage = (url, projectId) => {
    // save data to browser session storage
    sessionStorage.setItem("project_id", projectId);
    window.location = url


}

var projectId = sessionStorage.getItem("project_id")


$.ajax({
    url: "https://localhost:44314/Activities/GetByProjectId/" + projectId,
    type: "GET"
}).done(res => {
    let htmlItem = ''

    $.each(res, (key, val) => {
        console.log(val)
        htmlItem += `<div class="border rounded mb-3">
                        <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-3">
                            <div class="title-text">
                                <span class="font-weight-bold">${val.name}</span><br />
                                <span class="text-black-50 small">${val.startDate}</span>
                            </div>
                            <div class="btn-container">
                                <button onClick="console.log('${val.id}')" class="btn btn-sm btn-info" data-toggle="modal" data-target="#activityDetail">Detail <i class="fas fa-eye"></i></button>
                                <button onClick="console.log('delete ${val.id}')" class="btn btn-sm btn-danger">Delete <i class="fas fa-trash-alt"></i></button>
                            </div>
                        </div>
                    </div>`

        if (val.status === 0) {
            $('.notstarted').html(htmlItem);
        }
        else if (val.status === 1) {
            $('.started').html(htmlItem);
        }
        else {
            $('.completed').html(htmlItem);
        }
    });
})