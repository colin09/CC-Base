$(function(){
    slidesJs();
	newsTab();
	scrollToTop();
    contestTabsFixed();
});
// index top slidesjs
function slidesJs(){
    $(function() {
        $('#homeSlidesJs').slidesjs({
            width: 1920,
            height: 1080,
            start: 1,
            navigation: {
                active: true, //previous & next button
                effect: "slide"
            },
            pagination: {
                active: true, //page dot
                effect: "slide"
            },
            play: {
                effect: "slide",
                active: false, //play & stop button
                auto: true, // auto play
                interval: 4000,
                pauseOnHover: true,
                swap: true
                //restartDelay: 2500 重新启动延迟无效幻灯片
            },
            effect: { //animation speed
                slide: {
                    speed: 1000
                },
                fade: {
                    speed: 1000,
                    crossfade: true
                }
            }
        });
    })
}
// media news tabs hover
function newsTab(){
    var btnNewsList = $(".tabs");
    $(btnNewsList).hide();
    $(btnNewsList).first().show();
    var btnNewsTab = $(".btn-tabs");
    btnNewsTab.click(function () {
      btnNewsTab.removeClass("active");
      $(this).addClass("active");
      var indexThisBtn = $(this).index();
      $(btnNewsList).hide();
      $(btnNewsList).eq(indexThisBtn).show();
    });
}
// scroll to top
function scrollToTop(){
    var btnScrollToTop = $(".btn-scrolltop");
    $(btnScrollToTop).hide();
    $(window).scroll(function () {
        var scrollTopPixel = $(window).scrollTop();
        if ( scrollTopPixel > 600 ){
            $(btnScrollToTop).fadeIn();
        } else {
            $(btnScrollToTop).fadeOut();
        }
    });
    $(btnScrollToTop).click(function () {
        $("body,html").animate({scrollTop:0},800);
    })
}
// contest news fixed
function contestTabsFixed(){
    var contestTabs = $(".tabs-news-list");
    var naviH = $(".header-wrap").height();
    var topBannerH = $(".top-banner").height();
    var titleWrapH = $(".title-wrap").height();
    var tabsScrollTop = naviH + topBannerH +titleWrapH;
    $(window).scroll(function () {
        var tabsTopPixed = $(window).scrollTop();
        if ( tabsTopPixed > tabsScrollTop ){
            contestTabs.addClass("tabsfixed");
        } else {
            contestTabs.removeClass("tabsfixed");
        }
    })
}
