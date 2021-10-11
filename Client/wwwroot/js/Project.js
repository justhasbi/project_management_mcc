
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
    $.ajax({
        url: "projects/getall",
        method: "GET",
    }).done(res => {
        var htmlItem = ""
        res.forEach(item => {
            console.log(item)
            htmlItem += `
        <div class="col-xl-3 col-md-6 mt-4">
            <a onClick="redirectPage('/projectdetail')" style="text-decoration:none; cursor:pointer;">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <div class="text-lg font-weight-bold text-primary text-uppercase mb-1">
                                    ${item.name}
                                </div>
                                <div class="h5 mb-0 text-md font-weight-bold text-gray-800 mt-2">3 Activity</div>
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


const redirectPage = () => {
    window.location = 'projects/projectdetail'
}