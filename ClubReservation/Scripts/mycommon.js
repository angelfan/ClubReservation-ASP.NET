$(function(){ 
    var thisUrl = window.location.pathname
    var spUrl = c = thisUrl.split('/');
    var controller = spUrl[1];
    var method = spUrl[2];
    if (controller == 'UserZone') {     //个人中心
        var bread = "个人中心"
        $("#bread").html(bread)
        $("#activity").removeClass("active open")
        $("#activity").siblings().removeClass("active open")
    } else if (controller == "User") {  //用户管理
        if (method == "Home") {
            $("#home").addClass("active")
            $("#home").siblings().removeClass("active open")
        }
        else if (method == "Create") {
            var bread = "用户管理"
            $("#bread").html(bread)
            $("#user").addClass("active open")
            $("#user").siblings().removeClass("active open")
            $("#user").find("li").eq(1).addClass("active")
        }
        else if (method == "Index") {
            var bread = "用户管理"
            $("#bread").html(bread)
            $("#user").addClass("active open")
            $("#user").siblings().removeClass("active open")
            $("#user").find("li").eq(0).addClass("active")
        }
        else if (method == "Input") {
            var bread = "用户管理"
            $("#bread").html(bread)
            $("#user").addClass("active open")
            $("#user").siblings().removeClass("active open")
            $("#user").find("li").eq(2).addClass("active")
        } else {
            var bread = "用户管理"
            $("#bread").html(bread)
            $("#user").addClass("active open")
            $("#user").siblings().removeClass("active open")
        }
        
    } else if (controller == "Club") { //俱乐部管理
        var bread = "俱乐部管理"
        $("#bread").html(bread)
        $("#club").addClass("active open")
        $("#club").siblings().removeClass("active open")
        if (method == "Create") {
            $("#club").find("li").eq(1).addClass("active") //创建俱乐部
        }
        if (method == "Index") {
            $("#club").find("li").eq(0).addClass("active") //俱乐部一览
        }  
    } else if (controller == "Activity") {  //活动管理
        var bread = "活动管理"
        $("#bread").html(bread)
        $("#activity").addClass("active open")
        $("#activity").siblings().removeClass("active open")
        if (method == "Create") {
            $("#activity").find("li").eq(1).addClass("active")//活动创建
        }
        if (method == "Index") {
            $("#activity").find("li").eq(0).addClass("active")//活动一览
        }    
    } else if (controller == "UserActivity") {  //活动报名
        var bread = "活动报名"
        $("#user-activity").addClass("active")
        $("#user-activity").siblings().removeClass("active open")
    } else if (controller == "Statistics") { //数据统计
        var bread = "数据统计"
        $("#bread").html(bread)
        $("#statistic").addClass("active open")
        $("#statistic").siblings().removeClass("active open")
    }
    //删除俱乐部申请信息
    $("#delete-user-examines").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "post",
            dataType: "json",
            url: $(this).attr("href"),
            success: function (ret) {
                return;
            },
            error: function () {
                return;
            }
        });
    });//end delete-user-examines
})
//错误信息显示
function promptMessage(text, flag) {
    var m = $("#div-alert-message");
    var s = "";
    if (flag) {
        //成功
        s = "<div class='alert alert-block alert-success'>"
        + "<button type='button' class='close' data-dismiss='alert'><i class='icon-remove'></i></button>"
        + "<span><i class='icon-ok green'></i>" + text + "</span></div>";
    } else {
        s = "<div class='alert alert-block alert-error'>"
        + "<button type='button' class='close' data-dismiss='alert'><i class='icon-remove'></i></button>"
        + "<span><i class='icon-remove red'></i>" + text + "</span></div>";
    }
    m.html(s);
}