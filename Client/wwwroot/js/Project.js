// reset modal when closed
$('body').on('hidden.bs.modal', '.modal', function () {
    $(this).removeData('bs.modal');
});

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
        console.log(res)
        var htmlItem = ""
        
        res.forEach(item => {
            if (item.status === 0) {
                item.status = "Not started"
            } else if (item.status === 1) {
                item.status = "Started"
            } else {
                item.status = "Completed"
            }

            htmlItem += `
                <div class="col-xl-3 col-md-6 mt-4">
                    <a onClick="redirectPage('projects/projectdetail', '${item.id}', '${item.name}', '${item.description}', '${item.status}')" style="text-decoration:none; cursor:pointer;">
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


const redirectPage = (url, projectId, projectName, description, status) => {
    // save data to browser session storage
    sessionStorage.setItem("project_id", projectId);
    sessionStorage.setItem("project_name", projectName);
    sessionStorage.setItem("description", description);
    sessionStorage.setItem("projectStatus", status)
    window.location = url;

}

// get item from session
let projectId = sessionStorage.getItem("project_id");
let projectName = sessionStorage.getItem("project_name");

if (sessionStorage.getItem("projectStatus") === "Completed") {
    document.querySelector('.close-project-tag').removeAttribute("hidden")
    //document.querySelector('.close-project').setAttribute("disabled")
}
// set project name
$('.project-name').html(projectName);

// get activity
$.ajax({
    url: "https://localhost:44314/Activities/GetByProjectId/" + projectId,
    type: "GET"
}).done(res => {
    if (res !== null) {
        let checkStatus = res.find(item => item.status === 0 || item.status === 1)
        if (!checkStatus) {
            $('.close-project').removeAttr("disabled");
        }
    }
    let notStarted = '';
    let started = '';
    let completed = '';
    $.each(res, (key, val) => {
        if (val.status === 0) {
            notStarted += `<div class="border rounded mb-3">
                        <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-3">
                            <div class="title-text">
                                <span class="font-weight-bold">${val.name}</span><br />
                                <span class="text-black-50 small">${(val.startDate).split("T")[0]}</span>
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
                                <span class="text-black-50 small">${(val.startDate).split("T")[0]}</span>
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
                                <span class="text-black-50 small">${(val.startDate).split("T")[0]}</span>
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
});

// add activity
$('#btnActivity').click(e => {
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
        console.log(res)
        sessionStorage.setItem("activityId", res.id)
        let htmlItem = `
                <input type="text"
                    class="form-control form-control-lg font-weight-bold text-primary "
                    name="name"
                    id="ActivityTitle"
                    value="${res.name}"
                    style="background-color:transparent; border:none; font-size: 2em;" disabled>
                <table class="table mt-3">
                    <tr>
                        <th>Start Date:</th>
                        <td><input type="date" class="form-control" name="startdate" id="detailStartDate" value="${(res.startDate).split("T")[0]}" disabled></td>
                    </tr>
                    <tr>
                        <th>End Date:</th>
                        <td><input type="date" class="form-control" name="enddate" id="detailEndDate" value="${(res.endDate).split("T")[0]}" disabled></td>
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
                                <button type="button" onClick="saveStatus('${res.id}', '${res.status}')" class="btn btn-sm btn-primary mt-2 btn-save ml-2" hidden disabled>Save <i class="fas fa-edit"></i></button>
                            </div>
                        </td>
                    </tr>
                </table>`;
        $('.activityDetail').html(htmlItem);
        $(`#activityStatus option[value='${res.status}']`).attr('selected', 'selected')
    });

    
    displayEmployeeAssign(id)
    
}

const displayEmployeeAssign = (id) => {
    
    $.ajax({
        url: '/EmployeeActivities/GetEmployeeActivity/' + id, // + activity_id
        method: "GET",
    }).done(res => {
        let empList = ''
        //if (res.length > 0) {

        //}
        $.each(res, (key, val) => {
            empList += `
                <tr class="emp-item">
                    <td>${key + 1}</td>
                    <td>${val.fullname}</td>
                    <td>${val.jobName}</td>
                    <td>${val.departmentName}</td>
                    <td class="delete-emp-act">
                        <button class="btn btn-sm btn-danger" onClick="deleteEmpActivity('${JSON.stringify(val).replace(/"/g, "&quot;")}')">Delete <i class="fas fa-trash-alt"></i></button>
                    </td>
                </tr>`
        })
        $('.employeeTable').html(empList)

        if (!roles) {
            $('.delete-emp-act').remove();
        }
    });
}

// update activity status
const updateActivityStatus = (activityId) => {
    console.log('Select box enabled')
    //$('#activityStatus').removeAttr("disabled")
    document.querySelector('#activityStatus').toggleAttribute("disabled");
    //$('.button-container').html()
    document.querySelector('.btn-save').toggleAttribute('hidden')
    document.querySelector('.btn-save').toggleAttribute('disabled')
}

// save status
const saveStatus = (activityId, status) => {
    console.log([activityId, status])

    if (parseInt($('#activityStatus').val()) !== parseInt(status)) {
        var data = {
            id: activityId,
            status: parseInt($('#activityStatus').val())
        };

        $.ajax({
            url: '/Activities/UpdateActivityStatus',
            type: 'put',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: data,
            success: function (data) {
                swal({
                    title: "Success Update Status",
                    icon: "success"
                }).then(val => {
                    window.location.reload();
                });
            },
            error: () => {
                swal({
                    title: "Failed Update Status",
                    icon: "error"
                }).then(val => {
                    window.location.reload();
                });
            }
        })
    } else {
        swal({
            title: "Failed Update Status",
            icon: "error"
        });
    }
}


const deleteEmpActivity = (empData) => {

    const dataJson = JSON.parse(empData)

    swal({
        title: 'Apakah anda yakin menghapus Employee ?',
        icon: 'warning',
        buttons: ['Cancel', 'Yes!']
    }).then(result => {
        if (result) {
            $.ajax({
                url: "/EmployeeActivities/DeleteEmployeeAssignment/",
                method: "POST",
                dataType: 'JSON',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: dataJson,
                success: function (data) {
                    swal({
                        title: "Success Delete Employee",
                        icon: "success"
                    }).then(val => {
                        window.location.reload();
                    });
                },
                error: () => {
                    swal({
                        title: "Failed Delete Employee",
                        icon: "error"
                    }).then(val => {
                        window.location.reload();
                    });
                }
            });
        }
    });
}

const activityUpdate = () => {
    document.querySelector('#detailStartDate').toggleAttribute("disabled");
    document.querySelector('#detailEndDate').toggleAttribute("disabled");
    document.querySelector('#activityStatus').toggleAttribute("disabled");
    document.querySelector('.save-update-activity').toggleAttribute("hidden");
    document.querySelector('.save-update-activity').toggleAttribute("disabled");
    $('#ActivityTitle').removeAttr("disabled");
    document.querySelector('.btn-update').toggleAttribute("disabled")
    document.querySelector('.btn-update').toggleAttribute("hidden")
};

const saveActivityUpdate = () => {
    let updateObj = {
        id: parseInt(sessionStorage.getItem("activityId")),
        name: $('#ActivityTitle').val(),
        startDate: $('#detailStartDate').val(),
        endDate: $('#detailEndDate').val(),
        status: parseInt($('#activityStatus').val()),
        projectId: parseInt(projectId)
    }
    console.log(updateObj)

    swal({
        title: 'Apakah anda yakin Mengubah Activity ?',
        icon: 'warning',
        buttons: ['Cancel', 'Yes!']
    }).then(result => {
        if (result) {
            $.ajax({
                url: "/Activities/UpdateActivity",
                method: "PUT",
                dataType: 'JSON',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: updateObj,
                success: function (data) {
                    swal({
                        title: "Success Update Activity",
                        icon: "success"
                    }).then(val => {
                        window.location.reload();
                    });
                },
                error: () => {
                    swal({
                        title: "Failed Update Activity",
                        icon: "error"
                    }).then(val => {
                        window.location.reload();
                    });
                }
            });
        }
    });
}

// employee assignment
$.ajax({
    url: "/employees/GetEmployeeJobs",
    method: "GET"
}).done(res => {
    let htmlItem = '';
    $.each(res, (key, value) => {
        if(value.roleId === 1) {
            htmlItem += `
                <div class="border rounded mb-2">
                    <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-1">
                        <div class="title-text">
                            <span class="font-weight-bold">${value.fullname}</span><br />
                            <span class="text-black-50 small mt-0">${value.jobName} - ${value.departmentName}</span>
                        </div>
                        <div class="btn-container">
                            <button onClick="appendSelected('${JSON.stringify(value).replace(/"/g, "&quot;")}')" class="btn btn-sm btn-primary"><i class="fas fa-plus"></i></button>
                        </div>
                    </div>
                </div>`
        }
        $(".select-employee-container").html(htmlItem);
    })
})

// employee cart
let employeeCart = []

const appendSelected = (empDataObj) => {

    var empJson = JSON.parse(empDataObj);

    var data = {
        ActivityId: parseInt(sessionStorage.getItem("activityId")),
        Email: empJson.email,
        EmployeeId: empJson.employeeId
    };

    const findDupEmp = employeeCart.find(item => item.EmployeeId === empJson.employeeId);
    console.log(findDupEmp);

    if (!findDupEmp) {
        let selectedEmp = `
        <div class="border rounded mb-2">
            <div class="d-flex flex-row justify-content-between align-items-center flex-sm-wrap p-1">
                <div class="title-text">
                    <span class="font-weight-bold">${empJson.fullname}</span><br />
                    <span class="text-black-50 small mt-0">${empJson.jobName} - ${empJson.departmentName}</span>
                </div>
                <div class="btn-container">
                    <button  class="btn btn-sm btn-danger" id="deleteAssign" onClick="deleteEmpCart(this)"><i class="fas fa-times"></i></button>
                </div>
            </div>
        </div>`;

        employeeCart.push(data);
        console.log(employeeCart);
        document.querySelector('.selected-employee-container').innerHTML += selectedEmp;

    } else {
        console.log("you have already selected that employee");
    }
}

function deleteEmpCart(event) {
    employeeCart.pop();
    event.parentNode.parentNode.parentNode.remove()
}


$('.saveEmp').click(e => {
    let data = {
        CreateAssignEmployeeVMs: employeeCart
    }
    $.ajax({
        url: "/EmployeeActivities/AssignMultipleEmployee",
        method: "POST",
        dataType: 'JSON',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        //contentType: "application/json; charset=utf8",
        data: data,
        success: function (data) {
            swal({
                title: "Success Assign Employee",
                icon: "success"
            }).then(val => {
                window.location.reload();
            });
        },
        error: () => {
            swal({
                title: "Failed Assign Employee",
                icon: "error"
            }).then(val => {
                window.location.reload();
            });
        }
    });
});


const closeProject = () => {
    var data = {
        id: parseInt(sessionStorage.getItem("project_id")),
        status: "Completed"
    }

    swal({
        title: 'Apakah anda yakin Close Project?',
        icon: 'warning',
        buttons: ['Cancel', 'Yes!']
    }).then(result => {
        if (result) {
            $.ajax({
                url: "/projects/CloseProject",
                method: "PUT",
                dataType: 'JSON',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: data,
                success: function (data) {
                    swal({
                        title: "Success Close Project",
                        icon: "success"
                    }).then(val => {
                        window.location.reload();
                    });
                },
                error: () => {
                    swal({
                        title: "Failed Close Project",
                        icon: "error"
                    }).then(val => {
                        window.location.reload();
                    });
                }
            });
        }
    });
}