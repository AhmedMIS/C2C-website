// -----------------------------------------------------------------------------------
// http://wowslider.com/
// JavaScript Wow Slider is a free software that helps you easily generate delicious 
// slideshows with gorgeous transition effects, in a few clicks without writing a single line of code.
// Generated by WOW Slider 6.4
//
//***********************************************
// Obfuscated by Javascript Obfuscator
// http://javascript-source.com
//***********************************************
function ws_parallax(k,g,a){var c=jQuery;var f=c(this);var b=k.parallax||0.25;var e=c("<div>").css({position:"absolute",top:0,left:0,width:"100%",height:"100%",overflow:"hidden"}).addClass("ws_effect").appendTo(a.parent());function j(l){return Math.round(l*1000)/1000}function d(n,o,p){var l=new Date()*1;var m=function(){var q=(new Date()*1-l)/o;if(q>=1){n(1);cancelAnimationFrame(m);if(p){p()}}else{n(q);requestAnimationFrame(m)}};m()}var i=c("<div>").css({position:"absolute",left:0,top:0,overflow:"hidden",width:"100%",height:"100%",transform:"translate3d(0,0,0)"}).append(c("<img>").css({position:"absolute",transform:"translate3d(0,0,0)"})).appendTo(e);var h=i.clone().appendTo(e);this.go=function(l,r,p){var s=c(g.get(n));s={width:s.width(),height:s.height(),marginTop:s.css("marginTop"),marginLeft:s.css("marginLeft")};p=p?1:-1;var n=i.find("img").attr("src",g.get(r).src).css(s);var o=h.find("img").attr("src",g.get(l).src).css(s);var m=a.width()||k.width;var q=a.height()||k.height;d(function(v){v=c.easing.swing(v);var x=j(p*v*m),u=j(p*(-m+v*m)),t=j(-p*m*b*v),w=j(p*m*b*(1-v));if(k.support.transform){i.css("transform","translate3d("+x+"px,0,0)");n.css("transform","translate3d("+t+"px,0,0)");h.css("transform","translate3d("+u+"px,0,0)");o.css("transform","translate3d("+w+"px,0,0)")}else{i.css("left",x);n.css("left",t);h.css("left",u);o.css("left",w)}},k.duration,function(){e.hide();f.trigger("effectEnd")})}};// -----------------------------------------------------------------------------------
// http://wowslider.com/
// JavaScript Wow Slider is a free software that helps you easily generate delicious 
// slideshows with gorgeous transition effects, in a few clicks without writing a single line of code.
// Generated by WOW Slider 6.4
//
//***********************************************
// Obfuscated by Javascript Obfuscator
// http://javascript-source.com
//***********************************************
jQuery("#wowslider-container1").wowSlider({effect:"parallax",prev:"",next:"",duration:20*100,delay:20*100,width:640,height:480,autoPlay:true,autoPlayVideo:false,playPause:true,stopOnHover:false,loop:false,bullets:true,caption:true,captionEffect:"parallax",controls:true,responsive:1,fullScreen:false,onBeforeStep:0,images:0});