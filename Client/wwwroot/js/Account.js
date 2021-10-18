//$('.user').submit((e) => {
//    e.preventDefault();

//    let loginObj = {
//        "Email": $('#InputEmail').val(),
//        "Password": $('#InputPassword').val()
//    };

//    $.ajax({
//        url: 'accounts/loginuser/',
//        method: 'POST',
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//        data: JSON.stringify(loginObj),
//    }).done((res) => {
//        console.log(res)
//        alert("Success Login")
//    }).fail((res) => {
//        console.log(res)
//    })
//});

// Register JS
$('.register-form').submit((e) => {

    e.preventDefault();

    if ($('#first').val() === "" || $('#last').val() === "" || $('#phone').val() === "" || $('#email').val() === "" || $('#pass').val() === "") {
        swal({
            title: "Failed Create Account",
            text: "Please fill the empty registration form",
            icon: "error"
        })
    }

    let data = new Object();

    data.FirstName = $('#first').val();
    data.LastName = $('#last').val();
    data.Phone = $('#phone').val();
    data.Gender = parseInt($('#gender').val());
    data.Email = $('#email').val();
    data.Password = $('#pass').val();
    data.DepartmentId = parseInt($('#department').val());
    data.JobId = parseInt($('#job').val());
    data.RoleId = parseInt($('#role').val());

    console.log(JSON.stringify(data))

    $.ajax({
        url: 'register/',
        type: 'post',
        dataType: 'json',
        //contentType: 'application/json; charset=utf-8',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        success: function (data) {
            swal({
                title: "Success Create Account",
                icon: "success"
            }).then(() => {
                window.location = '/Accounts'
            })
        },
        error: function () {
            swal({
                title: "Failed Create Account",
                icon: "error"
            }).then(() => window.location.reload());
        }
    });
});

// job Select Box
$.ajax({
    url: 'https://localhost:44315/api/jobs',
    method: 'get'
}).done(res => {
    console.log(res)
    let selectItem = '';
    $.each(res, (key, val) => {
        selectItem += `<option value="${val.id}" data-department="${val.DepartmentId}">${val.name}</option>`
    });
    $('#job').html(selectItem);
}).fail(res => {
    console.log(res);
});

// Department Select Box
$.ajax({
    url: 'https://localhost:44315/api/departments',
    method: 'get'
}).done(res => {
    console.log(res)
    let selectItem = '';
    $.each(res, (key, val) => {
        selectItem += `<option value="${val.id}">${val.name}</option>`
    });
    $('#department').html(selectItem);
}).fail(res => {
    console.log(res);
});


// --------------------


$('.forgot-password').submit(e => {
    e.preventDefault();

    let data = new Object();

    data.Email =  $('#inputForgotEmail').val()

    console.log(data);

    $.ajax({
        url: 'forgotpassword/',
        type: 'put',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        success: function (data) {
            swal({
                title: "Success Reset Password",
                icon: "success",
                text: "please check email for temporary password"
            }).then(() => {
                window.location = '/Accounts'
            })
        },
        error: function () {
            swal({
                title: "Failed reset password",
                text: "Please enter a valid email address",
                icon: "error"
            });
        }
    })
});


$('.change-password').submit(e => {
    e.preventDefault();
    let data = new Object();
    data.Email = $('#inputEmail').val();
    data.CurrentPassword = $('#inputcurentpassword').val();
    data.NewPassword = $('#inputnewpassword').val();
    data.ConfirmPassword = $('#confirmpassword').val();
    console.log(data);

    $.ajax({
        url: 'changePassword/',
        type: 'put',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: data,
        success: function (data) {
            swal({
                title: "Success Change Password",
                icon: "success",
            }).then(() => {
                window.location = '/Accounts'
            })
        },
        error: function () {
            swal({
                title: "Failed Change password",
                icon: "error"
            });
        }
    })
})