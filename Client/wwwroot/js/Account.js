

$('.user').submit((e) => {
    e.preventDefault();

    let loginObj = {
        "Email": $('#InputEmail').val(),
        "Password": $('#InputPassword').val()
    };

    $.ajax({
        url: '',
        data:
    }).done(res => console.log(res))
        .fail(res => console.log(res))
});
