var locationUrl;
var locationCardUrl;
    var strShop="";
    
$(document).ready(function () {

 $("#ShopPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/ShopPhoto",
        'queueID': 'ShopPhoto_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "ShopPhoto",
        'auto': false,
        'multi': false,
        'method': 'get',
        'sizeLimit': 512000,
        'onError': function (event, ID, fileObj, errorObj) {
            if (errorObj.type == "File Size")
                alert("对不起，上传的图片不能超过500K");
            else
                alert(errorObj.type + ' Error: ' + errorObj.info);
        },
        'onComplete': function (event, ID, fileObj, response, data) {
            if (response.length > 1) {

                $("#imgShopPhoto").attr("src", "../Upload/ShopPhoto/" + response + "?" + GetGuid());
                $("#txtShopPhoto").val("../Upload/ShopPhoto/" + response);
            }
        }

    });

     $("#sltCity").bind("change", City);
    $("#sltProvince").bind("change", Province);
    var shoptype = $("#txtShopType").val();

    if (shoptype == 2) {
        locationUrl = "AllianceList.aspx?PID=140";
         locationCardUrl = "ShopSendCard.aspx?ShopType=2";
        strShop="联盟商";
           $("#rdislms").attr("checked", "checked");
    }
    if (shoptype == 3) {
        locationUrl = "ShopList.aspx?PID=31";
        locationCardUrl = "ShopSendCard.aspx?ShopType=3";
        strShop="商家";
           $("#rdisnotlms").attr("checked", "checked");
    }
    if (shoptype == 1) {
            
               locationUrl = "MainInfo.aspx?PID=168";
        locationCardUrl = "ShopSendCard.aspx?ShopType=1";
      
    }

    if ($("#txtSearch").val() != '') {
        $("#lblsearch").hide();
    }
    $("#txtSearch").keydown(function () {
        $("#lblsearch").hide();
    }).focusout(function () {
        var value = $(this).val().replace(/(^\s*)|(\s*$)/, "");
        if ('' == value) {
            $("#lblsearch").show();
        }
    });
 
    $("#rbbzfs").attr("checked", "checked");
    $("#rdbzxf").attr("checked", "checked");

    //是否启用商盟版功能
    if ($("#union").val() == "False" || $("#union").val() == "") {
        $(".tdZoom").removeAttr("class");
        $(".top").css("display", "none");
    }

    if (parseInt($("#hdShopID").val()) == 1) {
        $("#sltShopList").val("0");
        $("#sltShopList").attr("disabled", "");
    }
    else {
        $("#sltShopList").val($("#hdShopID").val());
        $("#sltShopList").attr("disabled", "disabled");
    }
    //"保存"按钮响应函数
    $("#btnShopSave").bind("click", ShopSave);

    //"重置"按钮响应函数
    $("#btnShopReset").bind("click", ShopReset);


    $("#btnShopAdd").click(function () {
          $("#radMainShopNo").attr("checked", "checked");
       btnShopAdd();
    });
     $("#btnMainShopAdd").click(function () {
           $("#radMainShopYes").attr("checked", "checked");
            btnShopAdd();
         $("#trSltShop").css("display", "none");
        
    });

    //商家积分变动
    $("#btSavePoint").bind("click", btSavePoint);

    //商家短信变动
    $("#btSaveSms").bind("click", btSaveSms);


    //商家购卡
    $("#btSaveCardLog").bind("click", btSaveCardLog);

    //是否启用购卡功能
    var temp = $("#txtIsSendCard").val();
    if (temp == "0" || $.trim(temp) == "") {
        $(".sendcard").css("display", "none");
        $("#btnShopBuyCard").css("display", "none");
        $("a[id$=hySendCard]").css("display", "none");
    }
    //是否启用商家结算功能
    temp = $.trim($("#txtIsSettlement").val());
    if (temp == "0" || temp == "") {
        $("#btnSettlement").css("display", "none");
        $("#txtSettlementInterval").val("65535");
        $("#txtShopProportion").val("0");
        $("#trSettlement").css("display", "none");
        $(".settlement").css("display", "none");
    }
    //是否启用联盟商短信管理功能
    temp = $("#txtSmsManage").val();
    if (temp == "0" || $.trim(temp) == "") {
        $(".smsmanage").css("display", "none");
        $("#btnMsgRecord").css("display", "none");
        $(".tSms").css("display", "none");
        $("#trShopSms").css("display", "none");
    }
    //是否启用联盟商积分管理功能
    temp = $("#txtPointManage").val();
    if (temp == "0" || $.trim(temp) == "") {
        $(".pointmanage").css("display", "none");
        $("#btnPointRecord").css("display", "none");
        $(".tPoint").css("display", "none");
        $("#trShopPoint").css("display", "none");
    }

    $("#btnSettlement").click(function () {
        location.href = "ShopSettlement.aspx?PID=135";
    });

    $("#btnMsgRecord").click(function () {
        location.href = "ShopSmsTransfer.aspx?PID=137";
    });

    $("#btnPointRecord").click(function () {
        location.href = "ShopPointTransfer.aspx?PID=136";
    });

    $("#btnShopBuyCard").click(function () {

        location.href = locationCardUrl;


    });

    $("#rdislms").bind("click", btnChangeAlliance);
    $("#rdisnotlms").bind("click", btnChangeAlliance);


    $(".tdZoom").bind("click", SubShopProcess);

    $("#radRechargeYes").click(function () {
         $("#trShopRecharge").show();
    });
      $("#radRechargeNo").click(function () {
         $("#trShopRecharge").hide();
    });
    //动态加载 伸缩图标
    var $tdZooms = $(".tdZoom");
    $.each($tdZooms, function () {
        if ($(this).attr("union") == "True") {
            var $img = $("<img alt='' class='top' src='../images/ico/plus.gif' />");
            $(this).prepend($img);
            $(this).attr("style", "text-align:left");
        }
    });
});
function Province() {

    $("#sltCity").empty();
    $("#sltCounty").empty();

    $("#sltCity").append("<option value=''>=== 请选择 ===</option>");
    $("#sltCounty").append("<option value=''>=== 请选择 ===</option>");
    var provinceID = $("#sltProvince").val();
    GetNextName(provinceID, "sltCity");
}
function City() {

    $("#sltCounty").empty();

    $("#sltCounty").append("<option value=''>=== 请选择 ===</option>");

    var CityID = $("#sltCity").val();
    GetNextName(CityID, "sltCounty");
}

function GetNextName(pid, controlID) {
    doAjax("../",
        "GetNextName",
        { "pid": pid },
        "json",
        function (json) {
            if (json != "") {
                for (var i = 0; i < json.length; i++) {
                    $("#" + controlID).append("<option value='" + json[i].ID + "'>" + json[i].Name + "</option>");
                }
            }
        },
        false
       );
}
/***************************************************************************************************
    点击父级商家时   显示或隐藏其所属的子商家    
****************************************************************************************************/
function SubShopProcess() {
    if ($(this).attr("showAlready") == 0) {
        //子商家列表未显示时的操作
        var $trTop = $(this).parent();
        var $img = $(this).children("img");
        $(this).attr("showAlready", "1");
        //获取父商家的序号  用来拼子商家的序号
         var num = $(this).children().last().text();
         //获得该商家id   根据id查询子商家列表       为什么通过客户端ID读不到值？？
        var shopid = $(this).attr("shopId");
        //判断tr是否有该自定义属性
        var $temp = $(this).parent().attr("sub");

        doAjax("../", "GetSubShopList",
                { "ShopID": shopid },
                "json",
                function (data) {
                    var i = data.length;
                    //根据返回的json数据生成表格  然后追加到当前行的下面  每行加个自定义属性sub  方便移除      
                    $.each(data, function () {
                        if (typeof ($temp) == "undefined") {
                            var $tr = $("<tr class='td' sub='" + shopid + "'></tr>");
                        }
                        else {
                            var $tr = $("<tr class='td' sub='" + $temp + "-" + shopid + "'></tr>");
                        }
                        //子商家序号 1-2   2-2 这个样子   NND  倒序的怎么弄 2-2  2-1  
                        var $td = $("<td style='text-align: right'></td>");
                        var $label = $("<label>" + num + "." + i + "</label>")
                        //判断子商家下是否还有子商家
                        if (this.HasChildShop == 1) {
                            var $img1 = $("<img alt='' class='top' src='../images/ico/plus.gif'/>");
                            $td.append($img1);
                            $td.attr("showAlready", "0");
                            $td.attr("shopId", this.ShopID);
                            $td.attr("class", "tdZoom");
                        }
                        $td.append($label);
                        $tr.append($td);
                        $td = $("<td style='text-align: left'>" + this.ShopName + "</td>");
                        $tr.append($td);
                        $td = $("<td>" + this.ShopContactMan + "</td>");
                        $tr.append($td);
                        $td = $("<td>" + this.ShopTelephone + "</td>");
                        $tr.append($td);
                        $td = $("<td style='text-align: left; padding-left: 4px;'>" + this.ShopAddress + "</td>");
                        $tr.append($td);
                        if ($("#txtIsSettlement").val() != "0" && $("#txtIsSettlement").val() != "") {
                            $td = $("<td style='text-align: left; padding-left: 4px;'>" + this.SettlementInterval + "</td>");
                            $tr.append($td);
                            $td = $("<td style='text-align: left; padding-left: 4px;'>" + this.ShopProportion + "</td>");
                            $tr.append($td);
                        }
                        if (this.ShopState == "True") {
                            $td = $("<td>是</td>");
                        } else {
                            $td = $("<td>否</td>");
                        }
                        $tr.append($td);
                        if ($("#txtPointManage").val() != "0" && $("#txtPointManage").val() != "") {
                            $td = $("<td style='text-align: left; padding-left: 4px;'>" + this.PointCount + "</td>");
                            $tr.append($td);
                        }
                        if ($("#txtSmsManage").val() != 0 && $("#txtSmsManage").val() != "") {
                            $td = $("<td style='text-align: left; padding-left: 4px;'>" + this.SmsCount + "</td>");
                            $tr.append($td);
                        }
                        $td = $("<td class='listtd' style='width: 200px;'></td>");
                        $tr.append($td);
                        var $label = $("<asp:Label ID='lblShopID' runat='server' Text='" + this.ShopID + "' Visible='false'></asp:Label>");
                        $td.append($label);
                        var $a = $("<a href='#' onclick=' ShopEdit(\"" + this.ShopName + "\",\"" + this.ShopID + "\") ' id='hyShopEdit' runat='server'> <img src='../images/Gift/eit.png' alt='编辑' title='编辑' /></a> '");
                        $td.append($a);
                        $a = $("<a href='ShopAuthority.aspx?PID=38&SID=" + this.ShopID + "' id='hyAuthority' runat='server'> <img src='../images/Gift/system.png' alt='权限设定' title='权限设定' /></a> '");
                        $td.append($a);
                        if ($("#txtIsSendCard").val() != "0" && $.trim($("#txtIsSendCard").val()) != "") {
                            $a = $("<a href='#' id='hySendCard' runat='server' onclick=' btnShopBuyCards(\"" + this.ShopName + "\",\"" + this.ShopID + "\")'> <img src='../images/ico/privilege.png' alt='购卡' title='购卡' /></a> '");
                            $td.append($a);
                        }
                        if ($("#txtPointManage").val() != "0" && $.trim($("#txtPointManage").val()) != "") {
                            $a = $("<a href='#' id='hyShopPointEdit' runat='server' onclick=' ShopEditPoint(\"" + this.ShopName + "\",\"" + this.ShopID + "\")'> &nbsp;<img src='../images/ico/point.png' alt='积分管理' title='积分管理' /></a> '");
                            $td.append($a);
                        }
                        if ($("#txtSmsManage").val() != "0" && $.trim($("#txtSmsManage").val()) != "") {
                            $a = $("<a href='#' id='hyShopSmsEdit' runat='server' onclick=' ShopEditSms(\"" + this.ShopName + "\",\"" + this.ShopID + "\")'> &nbsp;<img src='../images/ico/mobile.png' alt='短信管理' title='短信管理' /></a> '");
                            $td.append($a);
                        }
                        
                        var $a = $("<a href='#' onclick='ForeverTicketUrl(" + this.ShopID + ")' >二维码</a>");
                        $td.append($a);

                        //人才啊   这种拼法
                        $trTop.after($tr);
                        i--;
                    });
                    //改图标
                    $img.attr("src", "../images/ico/minus.gif");
                    //绑定点击事件
                 //   $(".tdZoom").bind("click", SubShopProcess);
                });
    }
    else {
        //子商家列表已显示时的移除操作
        if (typeof ($(this).parent().attr("sub")) == "undefined") {
            $(this).parent().nextAll("[sub ^=" + $(this).attr("shopId") + "]").remove();
        }
        else {
            $(this).parent().nextAll("[sub ^= '" + $(this).parent().attr("sub") + "-']").remove();
        }
        $(this).children("img").attr("src", "../images/ico/plus.gif");
        $(this).attr("showAlready", "0");
    }
}


function ForeverTicketUrl(shopid) {
    doAjax("../",
     'ForeverTicketUrl',
      { "shopid": shopid },
       "json",
        function (json) {
            $("#tdForeverTicketUrl").html(json);
            art.dialog({
                lock: true,
                title: strShop+'二维码',
                content: document.getElementById('divForeverTicketUrl'),
                id: 'divForeverTicketUrl',
                close: function () {
                    $("#tdForeverTicketUrl").html("");
                }
            });
        });
}

/*----------------------------------------------------------------------------------------------
   如果选择了非联盟商   新增商家就是某个联盟商的子商家   下拉框可用 
-------------------------------------------------------------------------------------------------*/
function btnChangeAlliance() {
    if ($("#rdislms").attr("checked")) {
        $("#sltShopList").attr("disabled", "disabled");
        $("#sltShopList").val("0");
    }
    else {
        $("#sltShopList").attr("disabled", "");
    }
}

/*****************************************************************************************************
*保存按钮响应函数
*****************************************************************************************************/
function ShopSave() {

    var shopType = $("#txtShopType").val();
    var strSettlementInterval = 0;
    var strTotalRate = 0;
    var strErrorMsg = "";
 
    var type = ($("#txtShopID").val() == "") ? "ShopAdd" : "ShopEdit";
    var telephone = $.trim($("#txtShopTelephone").val());
    if ($.trim($("#txtShopName").val()) == "") strErrorMsg += "<li>"+strShop+"名称不能为空;</li>";
    if($("#txtShopName").val().length >10)
    {
    strErrorMsg += "<li>"+strShop+"名称最多只能10个汉字;</li>";
    }
        if ($.trim($("#txtShopPhoto").val()) == "" &&shopType=="3") strErrorMsg += "<li>请上传商家图片;</li>";
    if ($.trim($("#txtShopContactMan").val()) == "") strErrorMsg += "<li>"+strShop+"联系人不能为空;</li>";
    if (telephone != "" && !telephone.IsNumber()) strErrorMsg += "<li>联系电话只能为数字且不能为空;</li>"    
    if ($("#chkSMS").attr("checked") && $("#txtShopSmsName").val() == "") 
    {
        strErrorMsg += "<li>服务参数已启动短信功能，短信后缀名不能为空;</li>"
    }
       if ($("#union").val() == "True" && $("#txtIsSettlement").val() == "1") {
        if ($.trim($("#txtSettlementInterval").val()) == "") strErrorMsg += "<li>结算周期不能为空</li>";
        if (!/^[-]?\d+$/.test($.trim($("#txtSettlementInterval").val())) || $.trim($("#txtSettlementInterval").val()) < 0) strErrorMsg += "<li>结算周期只能为正整数</li>";
        if ($.trim($("#txtShopProportion").val()) == "") strErrorMsg += "<li>提成比例不能为空</li>";
        if ($.trim($("#txtShopProportion").val()) != 0 && $.trim($("#txtShopProportion").val()) != 1 && !/^0\.\d*[0-9]$/.test($.trim($("#txtShopProportion").val()))) strErrorMsg += "<li>提成比例只能为0到1之间的数字</li>";
        }
           strSettlementInterval = $.trim($("#txtSettlementInterval").val());
 
     if (shopType == 3)
     {
         if ($.trim($("#txtRechargeMaxMoney").val()) == "" && $("input[name='radRechargeYesOrNo']:checked").val()=="1") strErrorMsg += "<li>最大充值金额不能为空</li>";         

            if ($.trim($("#txtTotalRate").val()) == "") strErrorMsg += "<li>返利总比例系数不能为空</li>";           
            if ($.trim($("#txtTotalRate").val()) != 0 && $.trim($("#txtTotalRate").val()) != 1 && !/^0\.\d*[0-9]$/.test($.trim($("#txtTotalRate").val()))) strErrorMsg += "<li>返利比例只能为0到1之间的数字</li>";
            if ($("#sltShopList").val() == "0" && $("#txtShopID").val()!="1" && $("input[name='radMainShopYesOrNo']:checked").val()=="0")
             {
                strErrorMsg += "<li>请选择所属联盟商;</li>";
            }
              strTotalRate = $.trim($("#txtTotalRate").val());
       }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: "将要" + (type == "ShopAdd" ? "增加" : "编辑") + strShop+" [" + $.trim($("#txtShopName").val()) + "]，\n确定操作吗？" + (type == "ShopAdd" ? "("+strShop+"增加成功，则不可删除，请仔细核对信息)" : ""),
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
         type,
        {
            "RechargeProportion": $.trim($("#txtRechargeProportion").val()),
            "shopID": $.trim($("#txtShopID").val()),
            "shopName": $.trim($("#txtShopName").val()),
            "shopContactMan": $.trim($("#txtShopContactMan").val()),
            "shopTelephone": $.trim($("#txtShopTelephone").val()),
            //"shopAreaID": $("#sltShopAreaID").val(),
            "shopAddress": $("#txtShopAddress").val(),
            "shopRemark": $("#txtShopRemark").val(),
            "shopTitle": $("#txtShopPrintTitle").val(),
            "shopFoot": $("#txtShopPrintFoot").val(),
            "shopSmsName": $("#txtShopSmsName").val(),
            "isChoose": $("input[name='radChooseYesOrNo']:checked").val(),
            "txtSettlementInterval": strSettlementInterval,
            "txtShopProportion": $.trim($("#txtShopProportion").val()),
            "isAllianceProgram": $("input[name='isLms']:checked").val(),
            "fatherShopID": $("#sltShopList").val(),
            "PointType": $("input[name='PointType']:checked").val(),
            "SmsType": $("input[name='SmsType']:checked").val(),
            "shopType": $("#txtShopType").val(),
            "totalRate": strTotalRate,
            "shopProvince": $("#sltProvince").val(),
            "shopCity": $("#sltCity").val(),
            "shopCounty": $("#sltCounty").val(),
            "shopImageUrl": $("#txtShopPhoto").val(),
               "IsRecharge": $("input[name='radRechargeYesOrNo']:checked").val(),
                "RechargeMaxMoney":  $("#txtRechargeMaxMoney").val(),
                 "IsMainShop": $("input[name='radMainShopYesOrNo']:checked").val(),
        },
        "json",
         function (json) {
             switch (json) {
                 case 0:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统异常，未保存数据，请再次点击保存！"),
                              lock: true
                          });
                     break;
                 case -3:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
                          });
                     break;
                 case -2:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("操作员不允许锁定当前正在操作的"+strShop+"！"),
                              lock: true
                          });
                     break;
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此商家已经在系统中存在，请重新输入商家，然后重试！"),
                              lock: true
                          });
                     break;
                 case -5:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("积分不足时或短信不足时权限设置无效，请重新选择！"),
                              lock: true
                          });
                     break;
                 default:
                     art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '保存成功！',
                                close: function () { location.href =locationUrl; },
                                lock: true
                            });
                     break;
             }
         });
        },
        cancelVal: '取消',
        cancel: true
    });
}

/*****************************************************************************************************
*重置按钮响应函数
*****************************************************************************************************/
function ShopReset() {
    if ($("#ShopAddOrEdit").val() == 0) {
        $("#txtShopName").val("");
        $("#txtShopID").val("");
        $("#txtShopContactMan").val("");
        $("#txtShopTelephone").val("");
        $("#txtShopSmsName").val("");
        $("#txtShopPrintTitle").val("");
        $("#txtShopPrintFoot").val("");
        $("#radChooseNo").attr("checked", true);
        $("#txtShopAddress").val("");
        $("#txtShopRemark").val("");
        $("#txtShopProportion").val("");
        $("#txtRechargeProportion").val("");
        $("#txtTotalRate").val("");
        $("#txtSettlementInterval").val("");
        $("#rdisnotlms").attr("checked", "checked");
        if (parseInt($("#hdShopID").val()) == 1) {
            $("#sltShopList").val("0");
        }
        else {
            $("#sltShopList").val($("#hdShopID").val());
            $("#sltShopList").attr("disabled", "true");
        }
    }
    else if ($("#ShopAddOrEdit").val() == 1) {
        ShopEdit($("#txtShopName").val(), $("#txtShopID").val());
    }
}

/*****************************************************************************************************
*新增按钮响应函数
*****************************************************************************************************/
function btnShopAdd() {
    $("#radChooseNo").attr("checked", true);
      $("#radRechargeYes").attr("checked", true);
    if (parseInt($("#hdShopID").val()) == 1) {
        $("#sltShopList").val("0");
        $("#sltShopList").attr("disabled", "");
    }
    else {
        $("#rdislms").attr("disabled", "disabled");
        $("#rdisnotlms").attr("disabled", "disabled");
        $("#sltShopList").val($("#hdShopID").val());
        $("#sltShopList").attr("disabled", "disabled");
    }
    art.dialog({
        title: strShop+'新增',
        content: document.getElementById('ShopInfo'),
        id: 'ShopInfo',
        lock: true,
        close: function () {
            ShopReset(); 
         }
    });
    $("#ShopAddOrEdit").val(0);
    $("#txtShopName").focus();
}

/*****************************************************************************************************
*编辑按钮响应函数
*****************************************************************************************************/
function ShopEdit(shopname, shopid) {

    $("#ShopAddOrEdit").val(1);
    $("#sltShopList").attr("disabled", "disabled");
    $("#rdislms").attr("disabled", "disabled");
    $("#rdisnotlms").attr("disabled", "disabled");
    art.dialog({
        lock: true,
        title: strShop+'编辑',
        content: document.getElementById('ShopInfo'),
        id: 'ShopInfo',
        close: function () {

            $("#ShopAddOrEdit").val(0); ShopReset();
            $("#sltShopList").attr("disabled", "");
            $("#rdislms").attr("disabled", "");
            $("#rdisnotlms").attr("disabled", "");
            $("#sltShopList").val(0);
       
        }
    });
    doAjax("../",
             'GetShopInfo', { "shopID": shopid }, "json",
                 function (json) {
             
                   $("#sltProvince").val(json.ShopProvince);
                     var provinceID = $("#sltProvince").val();
                     GetNextName(provinceID, "sltCity");
                     $("#sltCity").val(json.ShopCity);

                     var CityID = $("#sltCity").val();
                     GetNextName(CityID, "sltCounty");
                     $("#sltCounty").val(json.ShopCounty);
                     if(json.ShopType==3)
                     { 
                         if (json.IsRecharge == "True") {
                         $("#radRechargeYes").attr("checked", true);
                            $("#trShopRecharge").show();
                     }
                     else {
                         $("#radRechargeNo").attr("checked", true);
                            $("#trShopRecharge").hide();
                     }
                   
                       if (json.IsMain == "True") {
                         $("#radMainShopYes").attr("checked", true);
                     }
                     else {
                         $("#radMainShopNo").attr("checked", true);
                     }

                      $("#txtRechargeMaxMoney").val(parseFloat(json.RechargeMaxMoney).toFixed(2));
                     $("#imgShopPhoto").attr('src', json.ShopImageUrl);
                     $("#txtShopPhoto").val(json.ShopImageUrl);
                     }
                       $("#txtShopAddress").val(json.ShopAddress);
                     $("#txtShopName").val(json.ShopName);
                     $("#txtShopID").val(json.ShopID);
                 
                     $("#txtShopContactMan").val(json.ShopContactMan);
                     $("#txtShopTelephone").val(json.ShopTelephone);
                     $("#txtShopPrintTitle").val(json.ShopPrintTitle);
                     $("#txtShopPrintFoot").val(json.ShopPrintFoot);
                     $("#txtShopSmsName").val(json.ShopSmsName);
                     $("#txtShopRemark").val(json.ShopRemark);
                     $("#txtShopProportion").val(json.ShopProportion);
                     $("#txtRechargeProportion").val(json.RechargeProportion);
                     $("#txtSettlementInterval").val(json.SettlementInterval);
                    
                      $("#txtTotalRate").val(parseFloat(json.TotalRate).toFixed(2));
                   
                     if (json.ShopState == "True") {
                         $("#radChooseYes").attr("checked", true);
                     }
                     else {
                         $("#radChooseNo").attr("checked", true);
                     }
                     if (json.IsAllianceProgram == "True") {
                         $("#rdislms").attr("checked", "checked");
                         $("#sltShopList").val("0");
                     }
                     else {
                         $("#rdisnotlms").attr("checked", "checked");
                         $("#sltShopList").val(json.FatherShopID.toString());
                     }
                     switch (json.PointType.toString()) {
                         case "1":
                             $("#rdbzxf").attr("checked", true);
                             break;
                         case "2":
                             $("#rdjfgl").attr("checked", true);
                             break;
                         case "3":
                             $("#rdtz").attr("checked", true);
                             break;
                         default:
                             $("#rdjfgl").attr("checked", true);
                             break;
                     }
                     switch (json.SmsType.toString()) {
                         case "1":
                             $("#rbbzfs").attr("checked", true);
                             break;
                         case "2":
                             $("#rbkytz").attr("checked", true);
                             break;
                         default:
                             $("#rbbzfs").attr("checked", true);
                             break;
                     }
                 });
}

function MainInfoBind(shopname, shopid) {

    $("#ShopAddOrEdit").val(1);
    $("#sltShopList").attr("disabled", "disabled");
    $("#rdislms").attr("disabled", "disabled");
    $("#rdisnotlms").attr("disabled", "disabled");
 
    doAjax("../",
             'GetShopInfo', { "shopID": shopid }, "json",
                 function (json) {
              
                   $("#sltProvince").val(json.ShopProvince);
                     var provinceID = $("#sltProvince").val();
                     GetNextName(provinceID, "sltCity");
                     $("#sltCity").val(json.ShopCity);

                     var CityID = $("#sltCity").val();
                     GetNextName(CityID, "sltCounty");
                     $("#sltCounty").val(json.ShopCounty);
                        $("#txtShopAddress").val(json.ShopAddress);
                     if(json.ShopType==3)
                     {
                     $("#imgShopPhoto").attr('src', json.ShopImageUrl);
                     $("#txtShopPhoto").val(json.ShopImageUrl);
                     }
                 
                     $("#txtShopName").val(json.ShopName);
                     $("#txtShopID").val(json.ShopID);
                 
                     $("#txtShopContactMan").val(json.ShopContactMan);
                     $("#txtShopTelephone").val(json.ShopTelephone);
                     $("#txtShopPrintTitle").val(json.ShopPrintTitle);
                     $("#txtShopPrintFoot").val(json.ShopPrintFoot);
                     $("#txtShopSmsName").val(json.ShopSmsName);
                     $("#txtShopRemark").val(json.ShopRemark);
                     $("#txtShopProportion").val(json.ShopProportion);
                     $("#txtRechargeProportion").val(json.RechargeProportion);
                     $("#txtSettlementInterval").val(json.SettlementInterval);
             
                      $("#txtTotalRate").val(parseFloat(json.TotalRate).toFixed(2));
                 
                     if (json.ShopState == "True") {
                         $("#radChooseYes").attr("checked", true);
                     }
                     else {
                         $("#radChooseNo").attr("checked", true);
                     }
                           
                    
                     if (json.IsAllianceProgram == "True") {
                         $("#rdislms").attr("checked", "checked");
                         $("#sltShopList").val("0");
                     }
                     else {
                         $("#rdisnotlms").attr("checked", "checked");
                         $("#sltShopList").val(json.FatherShopID.toString());
                     }
              
                     switch (json.PointType.toString()) {
                         case "1":
                             $("#rdbzxf").attr("checked", true);
                             break;
                         case "2":
                             $("#rdjfgl").attr("checked", true);
                             break;
                         case "3":
                             $("#rdtz").attr("checked", true);
                             break;
                         default:
                             $("#rdjfgl").attr("checked", true);
                             break;
                     }
                     switch (json.SmsType.toString()) {
                         case "1":
                             $("#rbbzfs").attr("checked", true);
                             break;
                         case "2":
                             $("#rbkytz").attr("checked", true);
                             break;
                         default:
                             $("#rbbzfs").attr("checked", true);
                             break;
                     }
                     
                 });
}
/*****************************************************************************************************
*商家短信管理
*****************************************************************************************************/
function ShopEditSms(shopname, shopid) {
    $("#txtSmsShopName").html(shopname);
    art.dialog({
        lock: true,
        title: strShop+'短信管理',
        content: document.getElementById('ShopSms'),
        id: 'ShopSms',
        close: function () { }
    });
    doAjax("../",
             'GetShopInfo', { "shopID": shopid }, "json",
                 function (json) {
                     $("#hdSmsShopID").val(json.ShopID);
                     $("#txtSmsNumber").html(json.SmsCount);
                 });
    $("#txtSmsCount").focus();
    $("#txtSmsCount").val("");
}
function btSaveSms() {
    if (!isIntPositive($("#txtSmsCount").val())) {
        art.dialog
            ({
                time: 4,
                title: '系统提醒',
                content: '请输入正确的短信数量！',
                lock: true
            });
        $("#txtSmsCount").focus();
        return;
    }
    else {
        doAjax("../",
             'SetShopSms',
             {
                 "shopID": $("#hdSmsShopID").val(),
                 "count": $("#txtSmsCount").val(),
                 "type": $('input[name="radSmsType"]:checked').val(),
                  "shopType": $("#txtShopType").val()
             },
              "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("系统异常 未保存，请重试！"),
                                lock: true
                            });
                        break;
                    case 1:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("该商家拥有的短信量不足，不能扣除！"),
                                lock: true
                            });
                        break;
                    case -5:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("联盟商短信不足，无法增加该数量的短信！"),
                                lock: true
                            });
                        break;
                    default:
                        art.dialog
                            ({
                                title: '系统提示',
                                time: 1,
                                content: '保存成功！',
                                close: function () { location.href = locationUrl;},
                                lock: true
                            });
                        break;
                }
            });
    }
}

/*****************************************************************************************************
*商家积分管理
*****************************************************************************************************/
function ShopEditPoint(shopname, shopid,shoptype) {
    $("#txtShopPointName").html(shopname);
    var strTitle;
    if(shoptype=="2") {
    strTitle="联盟商积分管理";
    }
     else  if(shoptype=="3") {
    strTitle="商家积分管理";
    }
    art.dialog({
        lock: true,
        title: strTitle,
        content: document.getElementById('ShopPoint'),
        id: 'ShopPoint',
        close: function () { }
    });
    doAjax("../",
             'GetShopInfo', { "shopID": shopid }, "json",
                 function (json) {
                     $("#hdPointShopID").val(json.ShopID);
                      $("#txtShopPointName").html(json.ShopName);
                     $("#txtShopPointNumber").html(json.PointCount);
                 });
    $("#txtcount").focus();
    $("#txtcount").val("");
}
function btSavePoint() {
    if (!isIntPositive($("#txtcount").val())) {
        art.dialog
            ({
                time: 4,
                title: '系统提醒',
                content: '请输入正确的积分数量！',
                lock: true
            });
        $("#txtcount").focus();
        return;
    }
    else {
        doAjax("../",
             'SetShopPonit',
             {
                 "shopID": $("#hdPointShopID").val(),
                  "shopName": $("#txtShopPointName").html(),
                 "count": $("#txtcount").val(),
                 "type": $('input[name="radPointType"]:checked').val(),
                 "shopType": $("#txtShopType").val(),
             },
              "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("系统异常 未保存，请重试！"),
                                lock: true
                            });
                        break;
                    case 1:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("该商家拥有的积分不足，不能扣除！"),
                                lock: true
                            });
                        break;
                    case -5:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("联盟商积分不足，无法增加该数量的积分！"),
                                lock: true
                            });
                        break;
                    default:
                        art.dialog
                            ({
                                title: '系统提示',
                                time: 2,
                                content: '保存成功！',
                                close: function () { location.href = locationUrl; },
                                lock: true
                            });
                        break;
                }
            });
    }
    }

/******************************************************************************************************
商家购卡
******************************************************************************************************/
function btnShopBuyCards(shopname, shopid, IsAllianceProgram,IsMain) {
    $("#lblShopName").html(shopname);
    $("#hdCardShopID").val(shopid);
    $("#hdCardShopType").val(IsAllianceProgram)
    if(IsMain=="True")
    {
     $("#trBuyType").css("display","");
      
  }
  else
 {   
    $("#trBuyType").css("display","none");
  }
    

    art.dialog({
        title: '商家购卡',
        content: document.getElementById('ShopBuyCard'),
        id: 'ShopBuyCardID',
        lock: true,
        close: function () { } //ShopReset();
    });
    doAjax("../",
             'GetShopInfo',
             { "shopID": shopid }, 
                 function (json) {
                     //$("#hdCardShopID").val(json[0].ShopID);                                          
                     $("#hdCardShopID").val(shopid);
                     $("#hdCardShopType").val(IsAllianceProgram)
                 },"json");
}
function btSaveCardLog() {    
    var strErrorMsg = "";
    if ($("#txtStartCardNumber").val() == "") {
        strErrorMsg += "<li>请输入卡片起始号;</li>";
    }
    if ($("#txtEndCardNumber").val() == "") {
        strErrorMsg += "<li>请输入卡片截止号;</li>";
    }
    if (!isIntPositive($("#txtStartCardNumber").val())) {
        strErrorMsg += "<li>卡片起始号只能输入数字;</li>";
    }
    else {
        if ($("#txtStartCardNumber").val().toString().length < 4 || $("#txtStartCardNumber").val().toString().length > 19) {
            strErrorMsg += "<li>卡片起始号必须是4~19位;</li>";
        }
    }
    if (!isIntPositive($("#txtEndCardNumber").val())) {
        strErrorMsg += "<li>卡片截止号只能输入数字;</li>";
    }
    else {
        if ($("#txtEndCardNumber").val().toString().length < 4 || $("#txtEndCardNumber").val().toString().length > 19) {
            strErrorMsg += "<li>卡片截止号必须是4~19位;</li>"
        }
    }
    if ($("#txtStartCardNumber").val().toString().length != $("#txtEndCardNumber").val().toString().length) {
        strErrorMsg += "<li>卡片起始号和卡片截止号长度必须一致;</li>"
    }
    if ($("#txtStartCardNumber").val().toString() > $("#txtEndCardNumber").val().toString()) {
        strErrorMsg += "<li>截止号必须大于起始号;</li>"
    }
    if ($("#txtBuyCardMoney").val() != "") {
        if (!$("#txtBuyCardMoney").val().IsDecimal()) {
            strErrorMsg += "<li>购卡总金额输入格式错误;</li>"
        }
    } var  BuyType="0";
     var shoptype = $("#txtShopType").val();
if(shoptype=="3")
{  BuyType=$('input[name="radBuyType"]:checked').val();
}
   
    
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    doAjax("../",
             'ShopBuyCard',
             {
                 "ShopID": $("#hdCardShopID").val(),
                 "StartCardNumber": $("#txtStartCardNumber").val(),
                 "EndCardNumber": $("#txtEndCardNumber").val(),
                 "BuyCardMoney": $("#txtBuyCardMoney").val(),
                 "Remark": $("#txtRemark").val(),
                 "IsAllianceProgram": $("#hdCardShopType").val(),
                  "BuyType": BuyType,
             },
              "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("系统异常 未保存，请重试！"),
                                lock: true
                            });
                        break;
                    case -1:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("卡号重复，请检查之后重新输入！"),
                                lock: true
                            });
                        break;
                    case -2:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("卡号不在该商家所属联盟商购卡范围内，请重新输入起始卡号"),
                                lock: true
                            });
                        break;
                    default:
                        art.dialog
                            ({
                                title: '系统提示',
                                time: 1,
                                content: '保存成功！',
                                close: function () {
                                    //location.href = "ShopSendCard.aspx?PID=134";
                                    window.location.reload();
                                    $("#hdCardShopID").val("");
                                    $("#hdCardShopType").val("");
                                },
                                lock: true
                            });
                        break;
                }
            });
}

//function ShopDel(shopname,shopid) {
//    art.dialog({
//        title: '系统提示',
//        lock: true,
//        content: '确定要删除商家【' + shopname + '】吗? 此操作不可恢复',
//        yesFn: function () {
//            this.close();
//            doAjax("../",
//             'ShopDel', { "shopID": shopid }, "json",
//                 function (json) {
//                     switch (json) {
//                         case 0:
//                             art.dialog
//                                   ({
//                                       title: '系统提示',
//                                       content: ("系统异常 记录未能删除，请重试！"),
//                                       lock: true
//                                   });
//                             break;
//                         case -3:
//                             art.dialog
//                                   ({
//                                       title: '系统提示',
//                                       content: ("系统错误 请与系统管理员联系！"),
//                                       lock: true
//                                   });
//                             break;
//                         case -2:
//                             art.dialog
//                                   ({
//                                       title: '系统提示',
//                                       content: ("此属性已经在系统中存在记录,不能删除！"),
//                                       lock: true
//                                   });
//                             break;
//                         default:
//                             art.dialog
//                                   ({
//                                       time: 1,
//                                       content: '删除成功！',
//                                       closeFn: function () { location.href = "CustomField.aspx?PID=34"; }
//                                   });
//                             break;
//                     }
//                 });
//            return false;
//        },
//        noText: '取消',
//        noFn: true //为true等价于function(){}
//    });
//    return false;
//}