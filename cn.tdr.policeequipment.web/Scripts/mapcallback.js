/// <reference path="easyui/jquery.min.js" />
// 地图交互中间处理层
// 与底层地图相关交互的业务应该放在此处

// 矩形区间选择器
function rectangleSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts)

    //开启地图绘制矩形模式
    pgisMap.DrawRect(function (points) {
        var pointsArry = points.split(',');
        var data = {
            x1: pointsArry[0],
            y1: pointsArry[1],
            x2: pointsArry[2],
            y2: pointsArry[3]
        }; // 后台业务处理参数，矩形对角线的两个顶点坐标
        if (defaults.callback instanceof Function) {
            defaults.callback(data);
            pgisMap.myEzMap.changeDragMode("");
        }
    }, true);
}

// 圆形区间选择器
function circleSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts);

    //开启地图绘制圆模式
    pgisMap.DrawCircle(function (points) {
        var pointsArry = points.split(',');
        var data = {
            x: pointsArry[0],
            y: pointsArry[1],
            r: pointsArry[2]
        }; // 后台业务处理参数，矩形对角线的两个顶点坐标
        if (defaults.callback instanceof Function) {
            defaults.callback(data);
            pgisMap.myEzMap.changeDragMode("");
        }
    });
}

// 不规则区间选择器
function irregularSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts);

    //开启地图绘制多边形模式
    pgisMap.DrawPolygon(function (points) {
        var pointsArry = points.split(',');
        //获取多边形的点位
        var data = [];
        for (var i = 0; i < pointsArry.length; i++) {
            if (i % 2 != 0) {
                continue;
            }
            data.push({
                x: pointsArry[i],
                y: pointsArry[i + 1]
            });
        }
        if (defaults.callback instanceof Function) {
            defaults.callback(data);
            pgisMap.myEzMap.changeDragMode("");
        }
    });
}

// 轨迹移动
function traceMove(opts) {
    var defaults = {
        items: [{ x: 0, y: 0 }], // 移动轨迹个节点位置
        onStartMove: function (x, y) { }, // 开始移动回调函数
        onStopMove: function (x, y) { }, // 停止移动回调函数
        onInputPoint: function (x, y) { }, // 进入节点回调函数
        onOutputPoint: function (x, y) { } // 离开节点回调函数
    };
    $.extend(defaults, opts);

    // 此处业务处理
}

// 定位
function location(opts) {
    var defaults = {
        x: 0, // 节点 x 坐标
        y: 0, // 节点 y 坐标
        html: '', // HTML 标记内容，
        isCenter: false,//是否移动到当前位置
        callback: function () { } // 处理完成回调函数
    };
    $.extend(defaults, opts);
    var vPoint = new Point(defaults.x, defaults.y);
    var marker = new Marker(vPoint, pgisMap.MarkerIcon.BaseStation);
    marker.addListener("click", function () {
        marker.openInfoWindowHtml(defaults.html);
    });
    pgisMap.AddMarker(marker, defaults.isCenter);
}

//移动到指定位置
function move(opts) {
    var defaults = {
        x: 0, // 节点 x 坐标
        y: 0, // 节点 y 坐标
        html: '', // HTML 标记内容，
        isCenter: false,//是否移动到当前位置
        callback: function () { } // 处理完成回调函数
    };
    $.extend(defaults, opts);
    var vPoint = new Point(defaults.x, defaults.y);
    pgisMap.myEzMap.centerAtLatLng(vPoint);
}