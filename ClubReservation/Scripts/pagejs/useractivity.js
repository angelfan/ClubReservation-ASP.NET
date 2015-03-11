function CloseSuccess() {
    var box=document.getElementById("success");
    box.style.display="none";	
}

//报名
function Enroll(elementId) {
    var activityId = elementId.parentNode.parentNode.children[9].innerHTML;
    $.post("Enroll", { activityId: activityId },
    function (data) {
        if(data=='200')
        {
            document.getElementById('success').style.display = "";  //弹出报名成功提示框
            document.getElementById('Enroll').style.display = "none";  //隐藏报名按钮
            //document.getElementById('Cancel').style.display = "";   //显示取消报名按钮
            setTimeout("CloseSuccess()", 3000);                //3秒后提示框消失
            window.location.href = "/UserZone/index";
        }
        else
        {
            document.getElementById('error').style.display = "";  //弹出报名失败提示框
            etTimeout("CloseError()", 3000);
        }
    });
}

//function Cancel(elementId) {
//    var activityId = elementId.parentNode.parentNode.children[10].innerHTML;
//    $.post("CancelEnroll", { activityId: activityId },
//    function (data) {
//        if (data == '200') {
//            document.getElementById('cancel').style.display = "";
//            document.getElementById('Enroll').style.display = "";
//            document.getElementById('Cancel').style.display = "none";
//            setTimeout("CloseCancel()", 3000);
//        }
//        else {
//            document.getElementById('error').style.display = "";
//            etTimeout("CloseError()", 3000);
//        }
//    });
//}

