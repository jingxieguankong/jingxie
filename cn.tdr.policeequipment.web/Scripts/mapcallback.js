/// <reference path="easyui/jquery.min.js" />
// 地图交互中间处理层
// 与底层地图相关交互的业务应该放在此处

// 矩形区间选择器
function rectangleSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts)

    // 此处获取选择区间各节点信息
    var data = {
        x1: 0,
        y1: 0,
        x2: 0,
        y2: 0
    }; // 后台业务处理参数，矩形对角线的两个顶点坐标
    if (defaults.callback instanceof Function) {
        defaults.callback(data);
    }
}

// 圆形区间选择器
function circleSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts)

    // 此处获取选择区间各节点信息
    var data = {
        x: 0, // 原点 x 坐标
        y: 0, // 原点 y 坐标
        r: 100 // 半径
    }; // 后台业务处理参数
    if (defaults.callback instanceof Function) {
        defaults.callback(data);
    }
}

// 不规则区间选择器
function irregularSelector(opts) {
    var defaults = {
        callback: function (d) { } // 获取到各区间节点之后的回调函数
    };
    $.extend(defaults, opts)

    // 此处获取选择区间各节点信息
    var data = [{
        x: 0, // 节点 x 坐标 
        y: 0 // 节点 y 坐标
    }, {
        x: 1, // 节点 x 坐标 
        y: 1 // 节点 y 坐标
    }, {
        x: 2, // 节点 x 坐标 
        y: 2 // 节点 y 坐标
    }]; // 后台业务处理参数
    if (defaults.callback instanceof Function) {
        defaults.callback(data);
    }
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
        callback: function () { } // 处理完成回调函数
    };
    $.extend(defaults, opts);

    // 此处业务处理
}