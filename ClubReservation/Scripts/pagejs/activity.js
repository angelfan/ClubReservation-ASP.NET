function bodyLoad() {
    pageinit();
}
function pageinit() {
    $("#prompt_message").hide();
    //Activity/index
    if ($("#table_report").length > 0) {
        deleteActivity(); //删除活动
        tableInit();//table初始化
    }
    //Activity/create
    if ($("#activityFrom").length > 0) {
        dateInit();
    }
    $("#activity-submit").click(function (e) {
        checkvalue();
        e.result
    });
}
function dateInit() {
    $('.datetimepicker').datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        language: 'zh-CN',
        autoclose: true,
        todayHighlight: true,
        startView: "year",
        minView: "hour",
        pickerPosition: "bottom-left",
        todayBtn: true
    });
}
//删除活动
function deleteActivity() {
    $("#table_report").on("click", ".delete-activity", function (e) {
        e.preventDefault();
        var con = confirm("确认删除？");
        if (con == true) {
            $.ajax({
                type: "post",
                dataType: "html",
                url: $(this).attr("href"),
                data: { "id": $(this).attr("value") },
                success: function (ret) {
                    if (ret == undefined) {
                        promptMessage("删除失败", false);
                    } else {
                        $("#div-table-activity").html(ret);
                        promptMessage("删除成功", true);
                        pageinit();
                    }
                },
                error: function () {
                    promptMessage("删除失败", false)
                }
            });
        }
    });
}
function tableInit() {
    var oTable1 = $('#table_report').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null, null, null, null, null,
            { "bSortable": false }
        ]
    });
    $('[data-rel=tooltip]').tooltip();
}
//function promptMessage(text, flag) {
//    var info = $("#prompt_message");
//    info.text("");
//    info.removeClass("alert-error");
//    info.removeClass("alert-success");
//    if (flag) {
//        成功
//        info.addClass("alert-success");
//    } else {
//        info.addClass("alert-error");
//    }
//    info.text(text);
//    info.show();
//}

