// create project
$('.project-form').submit(e => {
    e.preventDefault();

    var data = new Object();
    data.ManagerId = parseInt($('#managerId').val())
    data.Name = $('#project-name').val()
    data.Description = $('#description').val()
    data.Status = 0

    console.log(data)

    $.ajax({
        url: 'Projects/post',
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
                icon: "error"
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
                    <a onClick="redirectPage('projects/projectdetail', '${item.id}', '${item.name}')" style="text-decoration:none; cursor:pointer;">
                        <div class="card border-left-primary shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-lg font-weight-bold text-primary text-uppercase mb-1">
                                            ${item.name}
                                        </div>
                                        <div class="badge badge-secondary h5 mb-0 text-md mt-2">${item.status}</div>
                                    </div>
                                    <div class="col-auto">
                                    <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>`
        });

        $('.project-wrapper').html(htmlItem);
    });

})


const redirectPage = (url, projectId, projectName) => {
    // save data to browser session storage
    sessionStorage.setItem("project_id", projectId);
    sessionStorage.setItem("project_name", projectName);

    window.location = url;
}

// get item from session
let projectId = sessionStorage.getItem("project_id");
let projectName = sessionStorage.getItem("project_name");

// set project name
$('.project-name').html(projectName);

// activity data

// get activity
let activityData = []
$.ajax({
    url: "https://localhost:44314/Activities/GetByProjectId/" + projectId,
    type: "GET"
}).done(res => {
    let notStarted = '';
    let started = '';
    let completed = '';
    $.each(res, (key, val) => {
        activityData.push(val)
        if (val.status === 0) {
            notStarted += `<div class="border rounded mb-3">
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
        }
        else if (val.status === 1) {
            started += `<div class="border rounded mb-3">
                        <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-3">
                            <div class="title-text">
                                <span class="font-weight-bold">${val.name}</span><br />
                                <span class="text-black-50 small">${val.startDate}</span>
                            </div>
                            <div class="btn-container">
                                <button onClick="console.log('${val.id}')" class="btn btn-sm btn-info" data-toggle="modal" data-target="#activityDetail">Detail <i class="fas fa-eye"></i></button>
                                <button onClick="deleteActivity('${val.id}')" class="btn btn-sm btn-danger">Delete <i class="fas fa-trash-alt"></i></button>
                            </div>
                        </div>
                    </div>`
        }
        else {
            completed += `<div class="border rounded mb-3">
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
        }
        $('.notstarted').html(notStarted);
        $('.started').html(started);
        $('.completed').html(completed);
    });
});

// add activity
activityData.forEach(item => {
    console.log(item)
})


$('.activity-form').submit(e => {
    e.preventDefault();

    var activityObj = {
        Name: $('#activity-name').val(),
        StartDate: $('#start-date').val(),
        EndDate: $('#end-date').val(),
        status: parseInt($('#status').val()),
        ProjectId: parseInt(projectId)
    };

    console.log(JSON.stringify(activityObj));

    $.ajax({
        url: 'https://localhost:44314/Activities/Post/',
        method: 'POST',
        data: activityObj,
        dataType: 'JSON',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (data) {
            swal({
                title: "Success Create Activity",
                icon: "success"
            }).then(val => {
                $("#activityForm").modal("hide");
            });
        },
        error: () => {
            swal({
                title: "Failed Create Project",
                icon: "error"
            }).then(val => {
                $("#activityForm").modal("hide");
            });
        }
    });
});

// delete Activity
const deleteActivity = (activityId) => {
    $.ajax({
        url: "https://localhost:44314/Activities/Delete/" + activityId,
        method: "DELETE",
        success: () => {
            console.log("success")
        },
        error: () => {
            console.log("failed")
        },
    })
}