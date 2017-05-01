
//重新刷新页面
function reloadPage() {
    location.reload();
}

//加载部分视图
function loadPartialHtml(ele, url) {
    $.get(url,
        {},
        function (result) {
            ele.html(result);
        });
}

function timeoutRefresh() {
    loadPartialHtml($('#baseInfoRaw'), "/home/BaseInfoRaw");
    loadPartialHtml($('#detailInfos'), "/home/detailInfos");
    loadPartialHtml($('#clientInfos'), "/home/clientInfos");
}

timeoutRefresh();

$('#btnReload').click(function () {
    reloadPage();
});

setInterval("timeoutRefresh()",
    $("#sltSecond").val() * 1000);

