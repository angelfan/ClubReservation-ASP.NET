function bodyLoad() {
    pageInit();
}

function pageInit() {
    //user/create
    dateInit();
    //user/index
    resetPassword();//重置密码
    deleteUser(); //删除用户
    getUserPowers();//获取用户权限
    setUserPowers();//设置用户权限
    tableInit();//table初始化
    valMaxClub();//权限验证初始化
    breadcrume();//breadcrume初始化
}

//重置密码
function resetPassword() {
    $("#table_report").on("click", ".reset-password",function (e) {
        e.preventDefault();
        $.ajax({
            type:"post",
            dataType: "json",
            url: $(this).attr("href"),
            data: {"id":parseInt($(this).attr("value"))},
            success: function (ret) {
                switch (ret) {
                    case 200:
                        promptMessage("重置密码成功", true);
                        break;
                    case 202:
                        promptMessage("用户信息不完整，请补充完整后再进行操作", false);
                        break;
                    case 204:
                        promptMessage("没有找到该用户", false);
                        break;
                    default:
                        promptMessage("重置密码失败", false);
                        break;
                }
            },
            error:function(){
                promptMessage("重置密码失败", false);
            }   
        });
    });
}
//删除用户
function deleteUser() {
    $("#table_report").on("click", ".delete-user", function (e) {
        e.preventDefault();
        var con = confirm("确认删除？");
        if (con == true) {
            $.ajax({
                type: "post",
                dataType: "html",
                url: $(this).attr("href"),
                data: { "id": parseInt($(this).attr("value")) },
                success: function (ret) {
                    if (ret == undefined) {
                        promptMessage("删除失败", false);
                    } else {
                        $("#div-table-user").html(ret);
                        promptMessage("删除成功", true);
                        pageInit();//初始化
                    }
                },
                error: function () {
                    promptMessage("删除失败", false);
                }
            });
        }
    });
}
//获取权限
function getUserPowers() {
    $("#table_report").on("click", ".get_user_power", function (e) {
        var id = $(this).attr("value");
        $.ajax({
            type: "post",
            dataType: "html",
            url: $(this).attr("ahref"),
            data: { "id": parseInt(id) },
            success: function (ret) {
                if (ret == undefined) {
                    promptMessage("获取权限失败", false);
                } else {
                    $("#myModal").find(".controls").html(ret);
                    $("#mymodel-submit").attr("value", id);
                    valMaxClub();
                }
            },
            error: function () {
                promptMessage("获取权限失败", false);
            }
        });
    });
}
//设置权限
function setUserPowers() {
    $("#myModal").on("click", ".mymodel-submit", function (e) {
        var str = "";
        $("#club-table>tbody>tr").each(function () {
            var s1 = $(this).find(".join");
            var s2 = $(this).find(".role");
            str += s1.attr("name") + "=" + s1.val() + "&";
            str += s2.attr("name") + "=" + s2.val() + "&";
        });
        $.ajax({
            type: "post",
            dataType: "json",
            url: $(this).attr("href"),
            data: {"obj":str,"id": parseInt($(this).attr("value"))},
            success: function (ret) {
                switch (ret) {
                    case 200:
                        promptMessage("权限设置成功", true);
                        break;
                    case 202:
                        promptMessage("用户信息不完整，请补充完整后再进行操作", false);
                        break;
                    case 204:
                        promptMessage("没有找到该用户", false);
                        break;
                    default:
                        promptMessage("权限设置失败", false);
                        break;
                }
            },
            error: function () {
                promptMessage("权限设置失败", false);
            }
        });
    });
}

//俱乐部选择数
function valMaxClub() {
    var count = 0;
    $(".join").each(function (e) {
        if (parseInt($(this).val()) == 1) {
            count++;
        }
    });
    if (count == 1) {
        $(".join").each(function (e) {
            $(this).removeAttr("disabled");
        });
    }
    if (count >= 2) {
        $(".join").each(function (e) {
            if (parseInt($(this).val()) == 0) {
                $(this).attr("disabled", "disabled");
            }
        });
    }
}

function tableInit(){
    $('#table_report').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null, null,
            { "bSortable": false }
        ]
    });
    $('[data-rel=tooltip]').tooltip();
}

function dateInit() {
    if ($("#userFrom").length > 0) {
        $('#datetimepicker').datetimepicker({
            language: 'zh-CN',
            autoclose: true,
            todayHighlight: true,
            startView: "year",
            minView: 'month',
            pickerPosition: "bottom-left",
            todayBtn: true
        });
    }
}
function show_box(id) {
    $('.widget-box.visible').removeClass('visible');
    $('#' + id).addClass('visible');
}