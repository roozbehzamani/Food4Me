var w=0,h=0;
var cart_top = new Array(0,0);
var search_url = "";

$(document).ready(function(){
	win_size(0);
	$(".button").append("<div><span></span></div>");
	
	$(".sub_menu").hover(function() {
		w_d = $(this).outerWidth();
		$(this).find(">div").css({left:-Math.abs(250-w_d)/2});
		$(this).find(">div").fadeIn(50,function(){ $(this).animate({top:'55px',opacity:1},200); });
	},function(){
		$(this).find(">div").stop();
		$(this).find(">div").fadeOut(100).delay(100).animate({top:'150px',opacity:0},1);
	});
	
	$(window).scroll( function() {
		if(w>900) {
			scrollTop = $(window).scrollTop();
			distance = (scrollTop - cart_top[0] + 50);
			if(cart_top[1]<distance) distance = cart_top[1];
			if(distance>0) {
				$('#cart_win').css({position:'absolute',top:distance});
			}
			else $('#cart_win').css({position:'relative',top:0});
		}
		
		if($("#cart_win").length > 0) {
    		cart_h = $("#cart_win").offset().top;
    		scroll_top_1 = $(window).scrollTop() +h;
    		if(scroll_top_1>cart_h) $("#goto_cart").css({opacity:0});
    		else $("#goto_cart").css({opacity:1});
		}
	});
	
	$("#search_find_item").click(function(e) {
		e.stopPropagation();
	});
});

$(document).click(function(){
	$("#search_find_item > div:nth-child(3)").empty().css({display:'none'});
});

function goto_cart() {
    $('html, body').animate({
        scrollTop: ($("#cart_win").offset().top - 100)
    }, 200);
}

function win_size(input){
	w=$(window).outerWidth();
	h=$(window).outerHeight();
	
	$(".all_h").css({height:h});
	
	left_dis = (w - parseInt($(".over_win_large").outerWidth()))/2;
	$(".over_win_large").css({left:left_dis});
	left_dis = (w - parseInt($(".over_win_small").outerWidth()))/2;
	$(".over_win_small").css({left:left_dis});
	if(h>=600) {
		$(".over_win").css({top:'90px'});
		$(".over_win > div:nth-child(2)").css({maxHeight:(h-180)});
	}
	else {
		$(".over_win").css({top:'20px'});
		$(".over_win > div:nth-child(2)").css({maxHeight:(h-40)});
	}
	
	main_h = h - parseInt($("#top_menu_bar").outerHeight()) - parseInt($("#footer_spc").outerHeight()) - 30;
	$("#min_inner").css({minHeight:main_h});
	
	hp = $("#search_background").outerHeight();
	if((w/hp)>=(1400/749)) $("#search_background").css({backgroundSize:'100% auto'});
	else $("#search_background").css({backgroundSize:'auto 100%'});
	
	step_h = $("#steps_holder").outerWidth()*150/1200;
	$("#steps_holder").outerHeight(step_h);
	
	if($('#cart_win').length!=0 && w>900) {
		$('#cart_win > div:nth-child(2)').css({maxHeight:h-145});
		cart_top[0] = $('#restaurant_cols > div:nth-child(2)').offset().top-30;
		cart_top[1] = $('#restaurant_cols > div:nth-child(2)').outerHeight() - $('#cart_win').outerHeight();
	}
	else $('#cart_win').css({position:'relative',top:0});
	
	if(input<2) setTimeout(function(){win_size(++input)},200);
}

var selected_win = "";
var close_outer_win = 0;
var after_win_success = "";
var win_top_dis = 0;
function show_over_win(id,close_mode,onsuccessid) {
    close_mode = (typeof close_mode !== 'undefined') ?  close_mode : 0;
    onsuccessid = (typeof onsuccessid !== 'undefined') ?  onsuccessid : "";
	close_right_menu();
	$(id).css({top:'300px',opacity:0});
	$("#bg_win").fadeIn(300);
	if(h>=500) $(id).fadeIn(10).animate({top:'90px',opacity:1},500);
	else $(id).fadeIn(10).animate({top:'20px',opacity:1},500);
	selected_win = id;
	close_outer_win = close_mode;
	after_win_success = onsuccessid;
	
	win_top_dis = $(window).scrollTop();
	$("body").css({position:'fixed',width:'100%',top:-win_top_dis+"px"});
}

function close_over_win(mode) {
    mode = (typeof mode !== 'undefined') ?  mode : 0;
	if(mode==0 || (mode==1 && close_outer_win==0)) {
		$("#bg_win").fadeOut(300);
		$(selected_win).animate({top:'300',opacity:0},300).delay(300).fadeOut(100);
		selected_win = "";
		close_outer_win = 0;
		after_win_success = "";
	}
	$("body").css({position:'relative',top:0});
	$('html, body').animate({ scrollTop:win_top_dis }, 0);
}

function close_show_over_win(id,close_mode) {
    close_mode = (typeof close_mode !== 'undefined') ?  close_mode : 0;
	$(selected_win).animate({top:'300',opacity:0},300).delay(300).fadeOut(100);
	selected_win = id;
	$(id).css({top:'-200px',opacity:0});
	if(h>=500)  $(id).fadeIn(10).animate({top:'90px',opacity:1},300);
	else  $(id).fadeIn(10).animate({top:'20px',opacity:1},300);
	close_outer_win = close_mode;
}

function show_right_menu() {
	$("#right_menu").animate({right:0},300);
	$("#right_menu_back").fadeIn(100);
}

function close_right_menu() {
	$("#right_menu").animate({right:'-320px'},300);
	$("#right_menu_back").fadeOut(200);
}

function hide_alarm() {
	$("#down_alarm").fadeOut(100);
}

function show_alarm(txt) {
	$("#down_alarm").fadeIn(100);
	$("#down_alarm").removeClass("alarm_0").removeClass("alarm_1");
	txt = txt.split("|");
	if(txt[0]==0 || txt[0]=="0") $("#down_alarm").addClass("alarm_0");
	else $("#down_alarm").addClass("alarm_1");
	$("#down_alarm > div:nth-child(2)").html(txt[1]);
}

function show_loading() {
	$("#loading").fadeIn(100);
}

function hide_loading() {
	$("#loading").fadeOut(5);
}

var js_folder = "http://www.jeteroni.com/script/";
var reload_mode = 0;

function register(btn,reload) {
    reload = (typeof reload !== 'undefined') ?  reload : 0;
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "register.php",
		data: { 
			f1:$("#register_name").val(),
			f2:$("#register_mobile").val(),
			f3:$("#register_pass_1").val(),
			f4:$("#register_pass_2").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$(".before_login").css({display:"none"});
				$(".after_login").css({display:"block"});
				$(".account_label").html(res[2]);
				result = res[0] + "|" + res[1];
				if(reload==1) location.reload();
				if(after_win_success!="") close_show_over_win(after_win_success,1);
				else close_over_win();
			}
			show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}
function login(btn,reload) {
    reload = (typeof reload !== 'undefined') ?  reload : 0;
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "login.php",
		data: { 
			f1:$("#login_mobile").val(),
			f2:$("#login_pass").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$(".before_login").css({display:"none"});
				$(".after_login").css({display:"block"});
				$(".account_label").html(res[2]);
				result = res[0] + "|" + res[1];
				if(reload==1) location.reload();
				if(after_win_success!="") close_show_over_win(after_win_success,1);
				else close_over_win();
			}
			show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}
function logout(reload) {
    reload = (typeof reload !== 'undefined') ?  reload : 0;
	hide_alarm();
	show_loading();
	$.ajax({
		type:"POST",
		url:js_folder + "logout.php",
		success:function(result){
			hide_loading();
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$(".before_login").css({display:"block"});
				$(".after_login").css({display:"none"});
				$(".account_label").html("حساب کاربری");
				if(reload==1) location.reload();
			}
			show_alarm(result);
		},
		error:function(){
			hide_loading();
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}
function forget(btn) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "forget.php",
		data: { 
			f1:$("#forget_mobile").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}
function search(btn) {
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(50);
	name = $("#search_name").val();
	area = $("#search_area").val();
	status = $("#search_status").val();
	order = $("#search_order").val();
	if(name=="" && area==0 && status==0 && order==0) show_alarm("0|لازم است حداقل یک مورد انتخاب شود");
	else {
		search_url = search_url+"&name="+name.split(" ").join("+")+"&area="+area+"&status="+status+"&order="+order;
		window.location.assign(search_url);
	}
	$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
}

function change_search_result(btn) {
	area = $("#search_area_list").val();
	status = $("#search_status_list").val();
	order = $("#search_order_list").val();
	search_url = search_url+"&area="+area+"&status="+status+"&order="+order;
	window.location.assign(search_url);
}

function search_head() {
	name = $("#search_head_name").val();
	area = $("#search_head_area").val();
	if(name=="" && area==0) show_alarm("0|لازم است حداقل یک مورد انتخاب شود");
	else {
		search_url = search_url+"&name="+name.split(" ").join("+")+"&area="+area;
		window.location.assign(search_url);
	}
}


function live_search(input,src_id,res_id) {
	hide_alarm();
	$(res_id).html('<div class="search_loading"><img src="temp/black_cir_loading.png" /></div>');
	$.ajax({
		type:"POST",
		url:js_folder + "live_search.php",
		data: {  f1:$(src_id).val() },
		success:function(result){
			$(res_id).fadeIn(10).html(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function set_search(ele) {
	$(ele).parents(".search_live").find("input").val($(ele).html());
}

function contact(btn) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "contact.php",
		data: { 
			f1:$("#contact_name").val(),
			f2:$("#contact_mobile").val(),
			f3:$("#contact_text").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#contact .font").val("");
			}
			show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function change_password(btn) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "change_password.php",
		data: { 
			f1:$("#pass_old").val(),
			f2:$("#pass_new_1").val(),
			f3:$("#pass_new_2").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#password .font").val("");
			}
			show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function show_week_time(btn,rest) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "get_rest_plan.php",
		data: { f1:rest },
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				show_over_win('#rest_week');
				$("#rest_week_info > div").html(res[2]);
				script = res[1].split("*");
				for(i=0;i<script.length;i++) {
					script_i = script[i].split(",");
					$(script_i[0]).css({width:script_i[1]+'%',right:script_i[2]+'%'});
				}
			}
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function add_cart(btn,rest,food) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "add_cart.php",
		data: {
			f1:rest,
			f2:food
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#null_cart").fadeOut(100);
				$("#list_cart").delay(100).fadeIn(10);
				$("#cart_item").html(res[2]);
				$("#cart_sum .price_sp").html(res[1]);
			}
			else show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function change_cart(rest,food,mode) {
	show_loading();
	$.ajax({
		type:"POST",
		url:js_folder + "change_cart.php",
		data: {
			f1:rest,
			f2:food,
			f3:mode
		},
		success:function(result) {
			hide_loading();
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#null_cart").fadeOut(100);
				$("#list_cart").delay(100).fadeIn(10);
				$("#cart_item").html(res[2]);
				$("#cart_sum .price_sp").html(res[1]);
			}
			else if(res[0]==2 || res[0]=="2") {
				$("#null_cart").fadeOut(100);
				$("#list_cart").delay(100).fadeIn(10);
				$("#cart_item").html(res[3]);
				$("#cart_sum .price_sp").html(res[2]);
				show_alarm("0|"+res[1]);
			}
			else if(res[0]==3 || res[0]=="3") {
				$("#list_cart").fadeOut(100);
				$("#list_cart_2").fadeOut(100);
				$("#null_cart").delay(100).fadeIn(10);
				$("#null_cart_2").delay(100).fadeIn(10);
			}
			else show_alarm(result);
		},
		error:function(){
			hide_loading();
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

var order_mode = 0;
var rest = 0;
function check_order(btn,rest) {
	rest_id = rest;
	order_mode = 0;
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "check_order.php",
		data: { f1:rest },
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") show_over_win("#order_mode",1);
			else if(res[0]==2 || res[0]=="2") show_over_win("#order_login",1,"#order_mode");
			else show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function set_order_mode(btn,mode) {
	order_mode = mode;
	if(order_mode==1) {
		show_final_cart(btn);
	}
	else { close_show_over_win("#order_location",1); }
}

var location_id = 0;
function get_area_price(ele,rest) {
	hide_alarm();
	$("#post_price").fadeOut(10);
	$("#post_loading").fadeIn(10);
	$.ajax({
		type:"POST",
		url:js_folder + "get_area_price.php",
		data: { f1:rest,f2:$(ele).val() },
		success:function(result){
			location_id = 0;
			res = result.split("|");
			$("#post_loading").fadeOut(10);
			if(res[0]==1 || res[0]=="1") {
				$("#post_price").fadeIn(10);
				$("#post_price").html(res[1]);
				location_id = $(ele).val();
			}
			else show_alarm(result);
		},
		error:function(){
			$("#post_loading").fadeOut(10);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function show_final_cart(btn) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "get_final_cart.php",
		data: { f1:rest_id,f2:order_mode,f3:location_id },
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#factor").html(res[1]);
				$("#order_info").html(res[2]);
				close_show_over_win("#order_factor",1);
				$("#order_factor > div:nth-child(2)").scrollTop(0);
			}
			else show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function back_order(mode) {
	if(mode==1) close_show_over_win("#order_mode",1);
	else close_show_over_win("#order_location",1);
}

function set_off(btn,rest) {
	hide_alarm();
	$(btn).find(">div").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "set_off.php",
		data: { 
			f1:rest,
			f2:$("#order_off_code").val(),
			f3:order_mode,
			f4:location_id
		},
		success:function(result){
			$(btn).find(">div").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				$("#final_price").html(res[1]);
			}
			else show_alarm(result);
		},
		error:function(){
			$(btn).find(">div").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}

function pay_order(btn,rest) {
	hide_alarm();
	$(btn).parents(".button").find(">div:nth-child(2)").fadeIn(100);
	$.ajax({
		type:"POST",
		url:js_folder + "pay_order.php",
		data: { 
			f1:rest,
			f2:$("#order_off_code").val(),
			f3:order_mode,
			f4:location_id,
			f5:$("#order_name").val(),
			f6:$("#order_mobile").val(),
			f7:$("#order_info_input").val(),
			f8:$("#order_address").val()
		},
		success:function(result){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			res = result.split("|");
			if(res[0]==1 || res[0]=="1") {
				show_alarm(result);
			}
			else if(res[0]==2 || res[0]=="2") {
				window.location.assign(res[1]);
			}
			else show_alarm(result);
		},
		error:function(){
			$(btn).parents(".button").find(">div:nth-child(2)").fadeOut(50);
			show_alarm("0|خطا در برقراری ارتباط با سرور");
		}
	});
}