var options = {
    series: [],
    chart: {
        height: 350,
        type: 'rangeBar'
    },
    plotOptions: {
        bar: {
            horizontal: true,
            distributed: true,
            dataLabels: {
                hideOverflowingLabels: false
            }
        }
    },
    dataLabels: {
        enabled: true,
        formatter: function (val, opts) {
            var label = opts.w.globals.labels[opts.dataPointIndex]
            var a = moment(val[0])
            var b = moment(val[1])
            var diff = b.diff(a, 'days')
            return label + ': ' + diff + (diff > 1 ? ' days' : ' day')
        },
        style: {
            colors: ['#f3f4f5', '#fff']
        }
    },
    xaxis: {
        type: 'datetime'
    },
    yaxis: {
        show: false
    },
    grid: {
        row: {
            colors: ['#f3f4f5', '#fff'],
            opacity: 1
        }
    }
};

var chart = new ApexCharts(document.querySelector("#chart"), options);
chart.render();

$.ajax({
    url: 'https://localhost:44314/projects/getmanagerid/' + mID,
    method: 'GET'
}).done(res => {
    let itemHtml = "";

    $.each(res, (key, val) => {
        itemHtml += `
            <option value="${val.id}">${val.name}</option>
        `;
        $('#project-select').html(itemHtml)
    })
})

let data = {
    data: []
}

const handleSelectChange = () => {
    let selectElem = $('#project-select').val()
    //sessionStorage.setItem("projectId", selectElem)
    data.data.splice(0, data.data.length)
    $.ajax({
        url: 'https://localhost:44314/Activities/GetByProjectId/' + selectElem, //sessionStorage.getItem("projectId"),
        type: "GET"
    }).done(res => {
        console.log(res)
        $.each(res, (key, val) => {
            let obj = {
                x: val.name,
                y: [
                    new Date(val.startDate).getTime(),
                    new Date(val.endDate).getTime(),
                ]
            }
            data.data.push(obj)
        })
        chart.updateOptions({
            series: [
                data
            ]
        })
    })
}


