<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editMemberData.aspx.cs" Inherits="ChainStock.mobile.member.editMemberData" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-编辑会员资料</title>
   
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">

	<link rel="stylesheet" type="text/css" href="css/style.css">
  
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
 
</head>
<body>
	<div class="section index" id="container">
		<div id="head" class="section">
			<div class="section header">
				<h1>编辑会员资料</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="#" class="sureBtnEditMemInfo" id="sureBtn">确定</a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section line_box">
				<!-- 获取会员信息并显示，可编辑 -->
				<div class="line_group">
					<p class="f-left">会员姓名</p>
					<div class="f-right" data-role="fieldcontain">
						<input type="text" placeholder="输入会员姓名" id="memname" data-role="datebox"   value="欧阳娜娜" runat="server" />
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">会员性别</p>
					<div class="f-right">
						<a href="javascript:void(0);" class="line_btn active" id="boy" runat="server">男</a>
						<a href="javascript:void(0);" class="line_btn" id="girl" runat="server">女</a>
					</div>
				</div>
               
				<div class="line_group divEditMemPhoto" >
					<p class="f-left">会员头像</p>
					<div class="f-right">
						<a href="javascript:void(0);" class="line_choice">
							<!-- 默认显示会员头像 -->
							<p id="imgDiv"><img src="images/headimg.jpg" id="imgShow"  runat="server"/></p>

						<span><img src="images/right_d.png"/></span>
                            	
						
						</a>
					</div>
				</div>
                
				<div class="line_group">
					<p class="f-left">电话号码</p>
					<div class="f-right">
						<input type="text" placeholder="输入电话号码" id="mobile" value="15899996666" runat="server" />
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">身份证</p>
					<div class="f-right">
						<input type="text" placeholder="请输入身份证号码" id="identityCard" value="5109211991****2630"  runat="server"/>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">生日</p>
					<div class="f-right">
						<%--	<input type="text" placeholder="请选择生日" id="birthday" disabled  runat="server"/>--%>
                            <p  id="birthday" runat="server"> </p>

							

						

					</div>
				</div>
				<div class="line_group">
					<p class="f-left">联系地址</p>
					<div class="f-right">
						<!-- 会员填写的地址显示在这里 -->
						<p class="show-address dis-n"></p>
						
						<a href="javascript:void(0);" class="line_choice queryCity" id="queryCity">
							<p id="address" runat="server">请选择</p>
							<span><img src="images/right_d.png" /></span>
						</a>
							<!-- 城市选择，地址填写 -->
						<div class="dis-n city-mode">
							<div class="city-query">
                                <h3>所在地区：</h3>
								<select id="sltProvince" runat="server">
									<option value="请选择">请选择</option>
								</select>
								<select id="sltCity"  runat="server">
									<option value="请选择">请选择</option>
								</select>
								<select id="sltCounty"  runat="server">
									<option value="请选择">请选择</option>
								</select>
                                	<select id="sltVillage"  runat="server">
									<option value="请选择">请选择</option>
								</select>
								<div class="section ad-detail">
									<input type="text" placeholder="请填写详细地址" id="detailAddress" runat="server" />
									<a href="##" class="ad-sure" id="ad-sure" >确定</a>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">电子邮箱</p>
					<div class="f-right">
						<input type="text" placeholder="请输入电子邮箱" value="" id="email" runat="server" />
					</div>
				</div>
			</div>
		</div>
		<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<!-- 返回主页 -->
			<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>我的会员</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="binding.aspx">会员卡绑定</a>
					<a href="myMember.aspx">我的会员卡</a>
					<a href="modifyPassword.aspx">修改密码</a>
				</div>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>会员服务</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="pointExchange.aspx">积分兑换</a>
					<a href="bill.aspx">消费记录</a>
				</div>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="rechargeOnline.aspx"><p>在线充值</p></a>
			</div>
		</div>
	</div>
     <input type="hidden" id="txtMemID" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script>
    function getPath(obj) {
        if (obj) {

            if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
                obj.select();

                return document.selection.createRange().text;
            }

            else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
                if (obj.files) {

                    return obj.files.item(0).getAsDataURL();
                }
                return obj.value;
            }
            return obj.value;
        }
    }  
</script>
<script type="text/javascript" src="scripts/script.js"></script>

<script>
    $("#sltProvince").bind("change", Province);
    $("#sltCity").bind("change", City);
    $("#sltCounty").bind("change", County);
</script>

</body>
</html>