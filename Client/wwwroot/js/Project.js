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
            }).then(res => location.reload())
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

// get activity
$.ajax({
    url: "https://localhost:44314/Activities/GetByProjectId/" + projectId,
    type: "GET"
}).done(res => {
    console.log(res)
    let notStarted = '';
    let started = '';
    let completed = '';
    $.each(res, (key, val) => {
        if (val.status === 0) {
            notStarted += `<div class="border rounded mb-3">
                        <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-3">
                            <div class="title-text">
                                <span class="font-weight-bold">${val.name}</span><br />
                                <span class="text-black-50 small">${val.startDate}</span>
                            </div>
                            <div class="btn-container">
                                <button onClick="activityDetail('${val.id}')" class="btn btn-sm btn-info" data-toggle="modal" data-target="#activityDetail">Detail <i class="fas fa-eye"></i></button>
                                <button onClick="deleteActivity('${val.id}')" class="btn btn-sm btn-danger">Delete <i class="fas fa-trash-alt"></i></button>
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
                                <button onClick="activityDetail('${val.id}')" class="btn btn-sm btn-info" data-toggle="modal" data-target="#activityDetail">Detail <i class="fas fa-eye"></i></button>
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
                                <button onClick="activityDetail('${val.id}')" class="btn btn-sm btn-info" data-toggle="modal" data-target="#activityDetail">Detail <i class="fas fa-eye"></i></button>
                                <button onClick="deleteActivity('${val.id}')" class="btn btn-sm btn-danger">Delete <i class="fas fa-trash-alt"></i></button>
                            </div>
                        </div>
                    </div>`
        }
        $('.notstarted').html(notStarted);
        $('.started').html(started);
        $('.completed').html(completed);
    });
    //console.log(res)
    //const activityData = res.filter(item => item.status === 0 || item.status === 1);

    //if (activityData.length === 0) {
    //    $('.close-project').removeAttr('disabled');
    //}
});

// add activity

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
                location.reload();
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
const deleteActivity = (id) => {
    swal({
        title: 'Apakah anda yakin menghapus Activity ?',
        icon: 'warning',
        buttons: ['Cancel', 'Yes!']
    }).then(result => {
        if (result) {
            $.ajax({
                url: "/Activities/Delete/" + id,
                method: "DELETE",
                success: function (data) {
                    swal({
                        title: "Success Delete Activity",
                        icon: "success"
                    }).then(val => {
                        window.location.reload();
                    });
                },
                error: () => {
                    swal({
                        title: "Failed Delete Activity",
                        icon: "error"
                    }).then(val => {
                        window.location.reload();
                    });
                }
            });
        }
    });
}

// activity detail
const activityDetail = (id) => {
    $.ajax({
        url: '/Activities/Get/' + id,
        method: 'GET'
    }).done(res => {
        let htmlItem = `
                <h3 class="text-primary font-weight-bold mt-3">${res.name}</h3>
                <table class="table mt-3">
                    <tr>
                        <th>Start Date:</th>
                        <td>${res.startDate}</td>
                    </tr>
                    <tr>
                        <th>End Date:</th>
                        <td>${res.endDate}</td>
                    </tr>
                    <tr>
                        <th>Status:</th>
                        <td>
                            <select class="form-control" id="activityStatus" disabled>
                                <option value="0">Unstarted</option>
                                <option value="1">Started</option>
                                <option value="2">Completed</option>
                            </select>
                            <div class="button-container">
                                <button type="button" onClick="updateActivityStatus('${res.id}')"
                                        class="btn btn-sm btn-success mt-2 btn-update">Update <i class="fas fa-edit"></i></button>
                            </div>
                        </td>
                    </tr>
                </table>`;

        $('.activityDetail').html(htmlItem);
        $(`#activityStatus option[value='${res.status}']`).attr('selected', 'selected')
    });

    $.ajax({
        url: '/EmployeeActivities/GetEmployeeActivity/' + id, // + activity_id
        method: "GET",
    }).done(res => {
        let empList = ''
        $.each(res, (key, val) => {
            console.log(val)
            empList += `
            <tr>
                <td>${key + 1}</td>
                <td>${val.fullname}</td>
                <td>${val.jobName}</td>
                <td>${val.departmentName}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onClick="deleteEmpActivity()">Delete <i class="fas fa-trash-alt"></i></button>
                </td>
            </tr>
        `
            $('.employeeTable').html(empList)
        })
    })


}



// update activity status
const updateActivityStatus = (activityId) => {
    console.log('Select box enabled')
    //$('#activityStatus').removeAttr("disabled")
    document.querySelector('#activityStatus').toggleAttribute("disabled");
    //$('.button-container').html()

    let saveButton = `<button type="button" onClick="saveStatus('${activityId}')" class="btn btn-sm btn-primary mt-2 btn-save ml-2">Save <i class="fas fa-edit"></i></button>`;
    $(saveButton).insertAfter('.btn-update');
}

// save status
const saveStatus = (activityId) => {
    console.log("data saved")
}


const deleteEmpActivity = () => {
    console.log("deleted")
}