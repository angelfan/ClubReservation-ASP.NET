function bodyLoad() {
    pageInit();
}
function pageInit() {
    clubInit();//俱乐部页面初始化
    propertyInit();//资产管理初始化
    examineInit();//报名审核
    deleteClub();//删除俱乐部
    deleteProperty();//删除俱乐部资产
    createExamine();//申请加入俱乐部
    passExamine();//通过审核
    returnExamine();//未通过审核
}
//删除俱乐部
function deleteClub() {
    $("#table_club").on("click", ".delete-club", function (e) {
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
                        $("#div-table-club").html(ret);
                        promptMessage("删除成功", true);
                        pageInit();//页面初始化
                    }
                },
                error: function () {
                    promptMessage("删除失败", false);
                }
            });
        }
    });
}

//删除俱乐部资产
function deleteProperty() {
    $("#table_property").on("click", ".delete-property", function (e) {
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
                        $("#div-table-property").html(ret);
                        promptMessage("删除成功", true);
                        pageInit();//页面初始化
                    }
                },
                error: function () {
                    promptMessage("删除失败", false);
                }
            });
        }
    });
}

//申请加入俱乐部
function createExamine() {
    $("#table_club").on("click", ".create-examine", function (e) {
        e.preventDefault();
        $.ajax({
            type: "post",
            dataType: "json",
            url: $(this).attr("href"),
            data: { "clubId": parseInt($(this).attr("value")) },
            success: function (ret) {
                switch (ret) {
                    case 200:
                        promptMessage("申请成功", true);
                        break;
                    case 202:
                        promptMessage("已经加入该俱乐部", false);
                        break;
                    case 203:
                        promptMessage("只能加入2个俱乐部", false);
                        break;
                    case 204:
                        promptMessage("没有找到该俱乐部", false);
                        break;
                    case 205:
                        promptMessage("已经申请加入该俱乐部", false);
                        break;
                    default:
                        promptMessage("申请失败", false);
                        break;
                }
            },
            error: function () {
                promptMessage("申请失败", false);
            }
        });
    });
}

//通过审核
function passExamine() {
    $("#table_examine").on("click", ".pass-examine", function (e) {
        e.preventDefault();
        $.ajax({
            type: "post",
            dataType: "html",
            url: $(this).attr("href"),
            data: { "id": parseInt($(this).attr("value")) },
            success: function (ret) {
                if (ret == undefined) {
                    promptMessage("审核通过失败", false);
                } else {
                    $("#div-table-examine").html(ret);
                    promptMessage("审核通过成功", true);
                    pageInit();//页面初始化
                }
            },
            error: function () {
                promptMessage("审核通过失败", false);
            }
        });
    });
}

//未通过审核
function returnExamine() {
    $("#table_examine").on("click", ".return-examine", function (e) {
        e.preventDefault();
        $.ajax({
            type: "post",
            dataType: "html",
            url: $(this).attr("href"),
            data: { "id": parseInt($(this).attr("value")) },
            success: function (ret) {
                if (ret == undefined) {
                    promptMessage("审核退回失败", false);
                } else {
                    $("#div-table-examine").html(ret);
                    promptMessage("审核退回成功", true);
                    pageInit();//页面初始化
                }
            },
            error: function () {
                promptMessage("审核退回失败", false);
            }
        });
    });
}

function clubInit() {
    $('#table_club').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null, null,
            { "bSortable": false }
        ]
    });
    $('[data-rel=tooltip]').tooltip();
}
function propertyInit() {
    $('#table_property').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null,
            { "bSortable": false }
        ]
    });
}

function examineInit() {
    $('#table_examine').dataTable({
        "aoColumns": [
            { "bSortable": false },
            null, null, null, null,
            { "bSortable": false }
        ]
    });
}