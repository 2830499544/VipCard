$(document).ready(function () {
    MemBirthdayRemind();
    MemPastTime(0);
    MemPontReset();
    GoodsStore();
    CustomRemind();
});

//会员有效期提醒
function MemPastTime(a) {
    doAjax("../",
               "MemPassTime",
               {
                   "myday": a
               },
               "json",
               function (json) {
                   CreateMemPassTimeTable(json);
               }
        )
}

function SelectMemPassTimeAll() {
    if ($(".chkAllMem").attr("checked") == true) {
        $(".chkmem").each(function () { //遍历html中class为chk的复选框标签        
            $(this).attr("checked", true);
        })
    }
    else {
        $(".chkmem").each(function () {
            $(this).attr("checked", false);
        })
    }
}

function CreateMemPassTimeTable(obj) {
    var html = ''
    + '<tr class=\"th\">'
    + '<th><input type="checkbox" id="chkAllMem" class="chkAllMem" onclick="SelectMemPassTimeAll()"/></th><th>会员卡号</th><th>会员姓名</th><th>会员等级</th><th>手机号码</th><th>过期时间</th><th>积分</th><th>所属商家</th>'
    + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
                         + '<td align="center"><input type="checkbox" class="chkmem" value="' + item.MemMobile + '" mobiles="' + item.MemMobile + '" memname="' + item.MemName + '" memcard="' + item.MemCard + '"/></td>'
                                    + '<td align="left">' + item.MemCard + '</td>'
                                    + '<td align="left">' + item.MemName + '</td>'
                                    + '<td align="left">' + item.LevelName + '</td>'
                                    + '<td align="left">' + item.MemMobile + '</td>'
                                    + '<td align="left"><font color=red>' + TransFormTime(item.MemPastTime) + '</font></td>'
                                    + '<td align="left">' + item.MemPoint + '</td>'
                                    + '<td align="left">' + item.ShopName + '</td>'
                                    + '</tr>';
        });
    }
    else {
        html += '<tr class=\"td\" ><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="10">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemPassTime").html(html);
}

function MemPassTime_ShowLoading() {
    $("#MemPassTime").html('<tr><td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;" colspan="9" type="LoadingBar"><div class="listLoading">数据正在加载，请稍候……&nbsp;&nbsp;<a href="javascript:void(0);" onclick="javascript:location.reload();">您可以刷新页面重试！</a></div></td></tr>');
}

//库存不足提醒
function GoodsStore() {
    doAjax("../",
               "GetGoodsStockOut",
               {
                   "myday": $("#txtGoods").val()
               },
               "json",
               function (json) {
                   CreateGoodsResetTable(json);
               }
        )
}

function CreateGoodsResetTable(obj) {
    var html = ''
    + '<tr class=\"th\">'
    + '<th>商品名称</th><th>商品编码</th><th>商品简码</th><th>库存数量</th><th>创建时间</th><th>单价</th><th>所属商家</th>'
    + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\" >"
                                    + '<td align="left">' + item.Name + '</td>'
                                    + '<td align="left">' + item.GoodsCode + '</td>'
                                    + '<td align="left">' + item.NameCode + '</td>'
                                    + '<td align="left"><font color=red>' + item.Number + '</font></td>'
                                    + '<td align="left"><font color=red>' + TransFormTime(item.GoodsCreateTime) + '</font></td>'
                                    + '<td align="left">' + item.Price.toString("0.00") + '</td>'
                                    + '<td align="left">' + item.ShopName + '</td>'
                                    + '</tr>';
        });
    }
    else {
        html += '<tr class=\"td\" ><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="10">未找到符合此条件的数据！</td></tr>';
    }
    $("#GoodsNumberList").html(html);
}

function Remind_ShowLoading() {
    $("#MemBirthdayListTable").html('<tr class=\"td\"><td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;" colspan="9" type="LoadingBar"><div class="listLoading">数据正在加载，请稍候……&nbsp;&nbsp;<a href="javascript:void(0);" onclick="javascript:location.reload();">您可以刷新页面重试！</a></div></td></tr>');
}
//会员生日提醒
function MemBirthdayRemind(n) {
    var day = (n != "undefined") ? n : 0;
    var html = $("#MemBirthdayListTable").html();
    if (html.indexOf("LoadingBar") != -1) {
        doAjax("../",
               "GetMemBirthday",
               {
                   "day": day
               },
               "json",
               function (json) {
                   CreateBirthdayTable(json);
               }
        )
    }
}
//创建会员生日列表项
function CreateBirthdayTable(obj) {
    var html = ''
    + '<tr class=\"th\">'
    + '<th><input type="checkbox" id="chkAll" class="chkAll" onclick="SelectAll()"/></th><th>会员卡号</th><th>会员姓名</th><th>会员等级</th><th>手机号码</th><th>生日</th><th>积分</th><th>所属商家</th>'
    + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
                         + '<td align="center"><input type="checkbox" class="chk" value="' + item.MemMobile + '" mobiles="' + item.MemMobile + '" memname="' + item.MemName + '" memcard="' + item.MemCard + '"/></td>'
                                    + '<td align="left">' + item.MemCard + '</td>'
                                    + '<td align="left">' + item.MemName + '</td>'
                                    + '<td align="left">' + item.LevelName + '</td>'
                                    + '<td align="left">' + item.MemMobile + '</td>'
                                    + '<td align="left"><font color=red>' + TransFormTime(item.MemBirthday) + '</font></td>'
                                    + '<td align="left">' + item.MemPoint + '</td>'
                                    + '<td align="left">' + item.ShopName + '</td>'
                                    + '</tr>';
        });
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="10">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemBirthdayListTable").html(html);
}


//会员积分清零提醒
function MemPontReset(n) {
    doAjax("../",
               "GetMemPointReset",
               {
                   "time": $("#txthidden").val()
               },
               "json",
               function (json) {
                   CreateMemPontResetTable(json);
               }
        )
}
//创建积分到期的列表
function CreateMemPontResetTable(obj) {
    var html = ''
    + '<tr class=\"th\">'
    + '<th>会员卡号</th><th>会员姓名</th><th>会员等级</th><th>手机号码</th><th>积分</th><th>最后一次消费时间</th><th>消费总次数</th><th>所属商家</th><th>操作</th>'
    + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
            //                         + '<td align="center"><input type="hidden" style="display:none" id="ckName_' + index + '" value="' + item.MemName + '" /></td>'
                                    + '<td align="left">' + item.MemCard + '</td>'
                                    + '<td align="left">' + item.MemName + '</td>'
                                    + '<td align="left">' + item.LevelName + '</td>'
                                    + '<td align="left">' + item.MemMobile + '</td>'
                                    + '<td align="left"><font color=red>' + item.MemPoint + '</font></td>'
                                    + '<td align="left"><font color=red>' + TransFormTime(item.PointEndTime) + '</font></td>'
                                    + '<td align="left"><font color=red>' + (item.CountingDown == undefined ? "未消费" : item.CountingDown) + '</font></td>'
                                    + '<td align="left">' + item.ShopName + '</td>'
                                    + '<td align="center"><a href="javascript:void(0);"   onclick="javascript:MemPointClear(' + item.MemID + ');" title="清零">' + '清零</a></td>'
                                    + '</tr>';
        });
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="10">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemPontResetList").html(html);
}

function MemPointClear(MemID) {
    doAjax(
                "../",
               "MemPointClear",
               {
                   "MemID": MemID
               },
               "json",
                function (json) {
                    switch (json) {
                        case 0:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                            break;
                        case 1:
                            art.dialog
                                ({ title: '系统提示',
                                    time: 1.5,
                                    content: '会员积分清除成功！',
                                    close: function () { MemPontReset(); }
                                });
                            break;
                    }
                });
}

//自定义提醒
function CustomRemind() {
    doAjax("../",
               "GetCustomRemindList", {},
               "json",
               function (json) {
                   CreateCustomRemindTable(json);
               }
        )
}
//创建自定义提醒的列表
function CreateCustomRemindTable(obj) {
    var html = ''
    + '<tr class=\"th\">'
    + '<th>提醒标题</th><th>提醒详情</th><th>提醒时间</th><th>发布提醒时间</th><th>发布提醒商家</th><th>发布提醒操作员'
    + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
                                    + '<td align="left">' + item.CustomRemindTitle + '</td>'
                                    + '<td align="left">' + item.CustomRemindDetail + '</td>'
                                    + '<td align="left"><font color=red>' + TransFormTime(item.CustomRemindTime) + '</font></td>'
                                    + '<td align="left">' + TransFormTime(item.CustomRemindCreateTime) + '</td>'
                                    + '<td align="left">' + item.ShopName + '</td>'
                                    + '<td align="left">' + item.UserName + '</td>'
                                    + '</tr>';
        });
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="6">未找到符合此条件的数据！</td></tr>';
    }
    $("#CustomRemindList").html(html);

}
//将长日期转换成短日期格式
function TransFormTime(time) {
    var a = time;
    try {
        a = a.split(" ")[0];
    } catch (e) {
        a = "未消费";
    }
    return a;
}