

$('.project-form').submit(e => {
    e.preventDefault();

    var data = new Object();
    data.ManagerId = 
    data.Name = $('#project-name').val()
    data.Description = $('#description').val()
    data.Status = 0

    $.ajax({
        url: 'Projects/Post',
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