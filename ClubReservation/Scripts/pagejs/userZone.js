function bodyLoad() {
    validate()//验证初始化
    confirm()//取消报名确认
    if ($("#userFrom").length > 0) {
        dateInit();
        resetPassword();
    }
}

//表单前台验证
Init_UserZone_ChangePwd:function validate() {
    $('#form-field-icon-1').blur(function () {
        if (this.value == "") {
            var errorMsg = "请输入新密码";
            $('#form-field-icon-1').next().html(errorMsg)
            a = false;
        } else if (this.value.length < 6 || this.value.length > 16) {
            var errorMsg = '请输入6-16位的密码';
            $('#form-field-icon-1').next().html(errorMsg)
            a = false;
        } else {
            var errorMsg = '';
            $('#form-field-icon-1').next().html(errorMsg)
            a = true;
        }
    }).keyup(function () {
        $(this).triggerHandler("blur");
    }).focus(function () {
        $(this).triggerHandler("blur");
    });

    $('#form-field-icon-2').blur(function () {
        if (this.value == "") {
            var errorMsg = "请再次输入新密码";
            $('#form-field-icon-2').next().html(errorMsg)
            b = false;
        } else if (this.value != $('#form-field-icon-1').val()) {
            var errorMsg = '两次输入密码不一致';
            $('#form-field-icon-2').next().html(errorMsg)
            b = false;
        } else {
            var errorMsg = '';
            $('#form-field-icon-2').next().html(errorMsg)
            b = true;
        }
    }).keyup(function () {
        $(this).triggerHandler("blur");
    }).focus(function () {
        $(this).triggerHandler("blur");
    });
    $('#send').click(function () {
        $('#form input').trigger('blur');
        if (a && b) {
            changePwd();
        }
        else { return false; }
    })
}
//var insert_html_ok = "<div id='messageBoxOk' class='alert alert-block alert-success' style='display:block'>" +
//        "<button type='button' class='close' data-dismiss='alert'>" + "<i class='icon-remove'>" + "</i>" + "</button>" +
//        "<i class='icon-ok green'></i>" +
//        "<span></span>" +
//        "</div>"
//var insert_html_no = "<div id='messageBoxNO' class='alert alert-block alert-erroe' style='display:block'>" +
//        "<button type='button' class='close' data-dismiss='alert'>" + "<i class='icon-remove'>" + "</i>" + "</button>" +
//        "<i class='icon-remove red'></i>" +
//        "<span></span>" +
//        "</div>"
Init_UserZone_CancelEnroll: function confirm() {
    $("#bootbox-confirm").on('click', function (e) {
        e.preventDefault();
        bootbox.confirm("确定取消报名？", function (result) {
            if (result) {
                $.ajax({
                    type: "post",
                    dataType: "html",
                    url: $("#bootbox-confirm").attr("data-href"),
                    data: { "id": $("#bootbox-confirm").attr("data-value") },
                    success: function (ret) {
                        if (ret == undefined) {
                            promptMessage("取消报名失败", false);
                            //$(".row-fluid").eq(0).before(insert_html_no)
                            //$("#messageBoxNo span").html("取消报名失败")
                            //$("#messageBoxNo span").show()
                            //setTimeout(function ()
                            //{ $("#messageBoxNo").remove() }, 3000);
                        } else {
                            $("#table_report_2 tbody").html(ret);
                            promptMessage("取消报名成功", true);
                            //$(".row-fluid").eq(0).before(insert_html_ok)
                            //$("#messageBoxOk span").html("取消报名成功")
                            //setTimeout(function ()
                            //{ $("#messageBoxOk").remove() }, 3000);
                        }
                    },
                    error: function () {
                        promptMessage("取消报名失败", false);
                        //$(".row-fluid").eq(0).before(insert_html_no)
                        //$("#messageBoxNo span").html("取消报名失败")
                        //$("#messageBoxNo span").show()
                        //setTimeout(function ()
                        //{ $("#messageBoxOk").remove() }, 3000);
                    }
                });
            }
        });

    });
}

Init_UserZone_ChangePwd: function changePwd() {
    $.ajax({
        type: "post",
        dataType: "JSON",
        url: $("#send").attr("data-href"),
        data: { "newPwd": $("#form-field-icon-1").val() },
        success: function (ret) {
            if (ret == 200) {
                promptMessage("密码修改成功", true);
                //$(".row-fluid").eq(0).before(insert_html_ok)
                //$("#messageBoxOk span").html("密码修改成功")
                //$("#form-field-icon-1").val("")
                //$("#form-field-icon-2").val("")
                //setTimeout(function ()
                //{ $("#messageBoxOk").remove() }, 3000);      
            }
            else {
                promptMessage("密码修改失败", false);
                $(".row-fluid").eq(0).before(insert_html_no)
                $("#messageBoxNo span").html("密码修改失败")
                $("#form-field-icon-1").val("")
                $("#form-field-icon-2").val("")
                setTimeout(function ()
                { $("#messageBoxNo").remove() }, 3000);
            }        
        },
        error: function () {
            promptMessage("密码修改失败", false);
                //$(".row-fluid").eq(0).before(insert_html_no)
                //$("#messageBoxNo span").html("密码修改失败")
                //$("#form-field-icon-1").val("")
                //$("#form-field-icon-2").val("")
                //setTimeout(function ()
                //{ $("#messageBoxNo").remove() }, 3000)
        }
    });
}

//重置密码
function resetPassword() {
    $("#resetPassword").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "post",
            dataType: "json",
            url: $(this).attr("href"),
            data: { "id": parseInt($(this).attr("value")) },
            success: function (ret) {
                switch (ret) {
                    case 200:
                        promptMessage("重置密码成功", true);
                        break;
                    case 202:
                        promptMessage("用户信息不完整，请补充完整后再进行操作", false);
                        break;
                    default:
                        promptMessage("重置密码失败", false);
                        break;
                }
            },
            error: function () {
                promptMessage("重置密码失败", false);
            }
        });
    });
}

function dateInit() {
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
