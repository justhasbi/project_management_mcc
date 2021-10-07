
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