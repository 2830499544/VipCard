/**
 * 登录二期js
 *
 **/
var i = oClientH = 0;
$(function(){
	//加载图片
	if(bgArr.length){
		loadImg();
	    var iPicIndex = 0 ;
	    var t = setInterval(function(){
	        //没有在加载一次
	        if('object' != typeof aLoadImg || 0 >= aLoadImg.length || aLoadImg.length != bgTotal - 1){
	            loadImg();    
	        }
	        //实在没有中断
	        $('#bg'+iPicIndex).fadeOut(1000, function() {
	        		if ($(this).hasClass('last')) {
						$("#bg-container li").css('z-index', 10);
					} else {
						$(this).css('z-index', 9)
					}
					$(this).show();
			});

	    	iPicIndex += 1;
	    	iPicIndex =	iPicIndex > aLoadImg.length ? 0 : iPicIndex ;
	    },5000)
	}

	if($("#txtAccount").val()!=''){
		$('#login').attr('disabled', 0);
		$("#AccountLabel").hide();
	}
	$("#txtAccount").keydown(function() {
		$('#login').attr('disabled', 0);
		$("#AccountLabel").hide();
	}).focusout(function() {
		var value = $(this).val().replace(/(^\s*)|(\s*$)/,"");
		if ('' == value) {
			$("#AccountLabel").show();
		}
	});

	if($("#txtPassword").val()!=''){
		$('#login').attr('disabled', 0);
		$("#passwordLabel").hide();
	}
	$("#txtPassword").keydown(function() {
		$('#login').attr('disabled', 0);
		$("#passwordLabel").hide();
	}).focusout(function() {
		var value = $(this).val().replace(/(^\s*)|(\s*$)/,"");
		if ('' == value) {
			$("#passwordLabel").show();
		}
	});
	
	$("#txtValCode").keydown(function() {
		$('#login').attr('disabled', 0);
		$("#valcodeLabel").hide();
	}).focusout(function() {
		var value = $(this).val().replace(/(^\s*)|(\s*$)/,"");
		if ('' == value) {
			$("#valcodeLabel").show();
		}
	});


})

//预加载图片
function loadImg(){
	if(undefined == bgArr[0]){
		return false;	
	}
    var oImg = new Image(); 
    oImg.src = bgArr[0].pic_url0;
    oImg.id  = "bg" + (i + 1);
    
    //oImg.height = oClientH;
    if(1 == bgArr.length){
    	oImg.className = 'last'
    }
	bgArr.shift();
	
    oImg.onload = function(){
    	var sImg = '<li id="'+oImg.id+'" class="'+oImg.className+'" style="background-image:url(\''+oImg.src+'\')" ></li>';
    	$('#bg-container').prepend(sImg);
		i += 1;
		loadImg()
    	//延迟放入数组，主要为兼容ie
    	setTimeout(function(){
			addImgArr(oImg)
    	},100)
    }
}

//加入数组内
function addImgArr(obj){
	if(!inArray(obj, aLoadImg)){
		if(obj.complete){
			aLoadImg.push(obj)
		}
	}
}

//判断数据是否存在 
function inArray(obj, arr){
    if('object' != typeof aLoadImg || 0 >= aLoadImg.length){
        aLoadImg = [];
        return false;
    }
    for(x in arr){
        if(undefined == arr[x] || 'object' != typeof arr[x]){
            continue;
        }
        if(arr[x].src == obj.src){
            return true;
        }
    }
    return false;
}

//加入收藏
function bookmark(){
var title=document.title
var url=document.location.href
if (window.sidebar) window.sidebar.addPanel(title, url,"");
else if( window.opera && window.print ){
var mbm = document.createElement('a');
mbm.setAttribute('rel','sidebar');
mbm.setAttribute('href',url);
mbm.setAttribute('title',title);
mbm.click();}
else if( document.all ) window.external.AddFavorite( url, title);
}