
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
    data.DepartmentId = parseInt($('#departmen').val());
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
            console.log("success")
        },
        error: function () {
            console.log("Error")
        }
    });
});


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