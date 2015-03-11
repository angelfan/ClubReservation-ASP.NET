function bodyLoad() {
    usersInit();//用户统计初始化
    userDetailsInit();//用户详细统计
    clubsInit();//俱乐部统计
    clubDetailsInit();//俱乐部详细统计
    $('[data-rel="tooltip"]').tooltip();
}

function clubDetailsInit() {
    $('#table_club_details').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null, null, null
        ]
    });
    if ($('#table_club_details').length > 0) {
        selectChange("day");
        $("#tab-day").click(function (e) {
            selectChange("day");
        });
        $("#tab-week").click(function (e) {
            selectChange("week");
        });
        $("#tab-year").click(function (e) {
            selectChange("year");
        });
    }
}

function usersInit() {
    $('#table_users').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null, null,
            { "bSortable": false }
        ]
    });
}
function userDetailsInit() {
    $("#table_user_details").dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null, null,
            { "bSortable": false }
        ]
    });
    if ($("#table_user_details").length > 0) {
        userDetailsImg();
    }
}
function clubsInit() {
    $("#table_clubs").dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null,
            { "bSortable": false }
        ]
    });
}
//图表显示事件
function selectChange(value) {
    var clubId = $("#clubId").val();
    var sales_charts = $('#sales-charts').css({ 'width': '100%', 'height': '300px' });
    var url = $("#getClubById").val();
    var options = {
        hoverable: true,
        shadowSize: 0,
        series: {
            lines: { show: true },
            points: { show: true }
        },
        xaxis: {
            tickLength: 0,
            tickSize: 1,
            ticks: ""
        },
        yaxis: {
            ticks: 10,
            min: '0',
            max: '100',
            tickDecimals: 1
        },
        grid: {
            backgroundColor: { colors: ["#fff", "#fff"] },
            borderWidth: 1,
            borderColor: '#555'
        }
    };

    $.ajax({
        type: "post",
        dateType: "json",
        url: url,
        data: { t: value, id: parseInt(clubId) },
        success: function (ret) {
            switch (ret.state) {
                case 200:
                    options["xaxis"]["ticks"] = parseArray(ret.ticks);
                    $.plot("#sales-charts", [
                        { label: "参加/上限", data: parseArray(ret.d1) },
                        { label: "参加/总数", data: parseArray(ret.d2) },
                        { label: "上限/总数", data: parseArray(ret.d3) }
                    ], options);
                    break;
                case 204:
                    promptMessage("没有找到该俱乐部", false);
                    break;
                default:
                    promptMessage("获取数据失败", false);
                    break;
            }
        },
        error: function () {
            promptMessage("获取数据失败", false);
            return;
        }
    });
}
//把数组转换成图表中需要的2维数组
function parseArray(arr) {
    var result = new Array(arr.length);
    for (var i = 0; i < arr.length; i++) {
        var a = new Array(2);
        a[0] = i;
        a[1] = arr[i];
        result[i] = a;
    }
    return result;
}
function userDetailsImg() {
    var d1 = parseInt($("#user-activity-ycj").text());
    var d2 = parseInt($("#user-activity-wcj").text());
    var data = [
    { label: "已参加", data: d1, color: "#68BC31" },
    { label: "未参加", data: d2, color: "#2091CF" },
    ];
    var placeholder = $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });
    $.plot(placeholder, data, {
        series: {
            pie: {
                show: true,
                tilt: 0.8,
                highlight: {
                    opacity: 0.25
                },
                stroke: {
                    color: '#fff',
                    width: 2
                },
                startAngle: 2
            }
        },
        legend: {
            show: true,
            position: "ne",
            labelBoxBorderColor: null,
            margin: [-30, 15]
        },
        grid: {
            hoverable: true,
            clickable: true
        },
        tooltip: true, //activate tooltip
        tooltipOpts: {
            content: "%s : %y.1",
            shifts: {
                x: -30,
                y: -50
            }
        }
    });
    var $tooltip = $("<div class='tooltip top in' style='display:none;'><div class='tooltip-inner'></div></div>").appendTo('body');
    placeholder.data('tooltip', $tooltip);
    var previousPoint = null;
    placeholder.on('plothover', function (event, pos, item) {
        if (item) {
            if (previousPoint != item.seriesIndex) {
                previousPoint = item.seriesIndex;
                var tip = item.series['label'] + " : " + Math.round(item.series['percent']) + '%';
                $(this).data('tooltip').show().children(0).text(tip);
            }
            $(this).data('tooltip').css({ top: pos.pageY + 10, left: pos.pageX + 10 });
        } else {
            $(this).data('tooltip').hide();
            previousPoint = null;
        }
    });
    var d1 = [];
    for (var i = 0; i < Math.PI * 2; i += 0.5) {
        d1.push([i, Math.sin(i)]);
    }
    var d2 = [];
    for (var i = 0; i < Math.PI * 2; i += 0.5) {
        d2.push([i, Math.cos(i)]);
    }
    var d3 = [];
    for (var i = 0; i < Math.PI * 2; i += 0.2) {
        d3.push([i, Math.tan(i)]);
    }
    var sales_charts = $('#sales-charts').css({ 'width': '100%', 'height': '220px' });
}