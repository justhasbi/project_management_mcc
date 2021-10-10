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
                title: "Success Create Account",
                icon: "success"
            }).then(() => window.location.reload());
            
        }
    });
});

// job Select Box
$.ajax({
    url: 'https://localhost:44315/api/jobs',
    method: 'get'
}).done(res => {
    console.log(res.data)
    let selectItem = '';
    $.each(res.data, (key, val) => {
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
    console.log(res.data)
    let selectItem = '';
    $.each(res.data, (key, val) => {
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
        success: (data) => {
            console.log("success")
        },
        error: () => {
            console.log("error")
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
        success: (data) => {
            console.log("success")
        },
        error: () => {
            console.log("error")
        }
    })
})