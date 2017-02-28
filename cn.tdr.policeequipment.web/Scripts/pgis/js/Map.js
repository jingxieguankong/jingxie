function PGisMap(vContainer) {
    if (!EzServerClient.GlobeFunction.isTypeRight(vContainer, "object")) {
        throw EzErrorFactory.createError("EzMap构造方法中arguments[0]类型不正确");
    }
    this.myEzMap = new EzMap(vContainer);

    //添加 ArcGis Online 图层
    var arcgisolineUrl = "http://www.arcgisonline.cn/ArcGIS/rest/services/ChinaOnlineCommunity/MapServer";
    var arcgisGroupLayer = new EzServerClient.GroupLayer("ArcGis Online", [new EzServerClient.Layer.ArcGISTileLayer("arcgisLayer", arcgisolineUrl)]);
    this.myEzMap.addGroupLayer(arcgisGroupLayer);

    //添加百度地图图层
    //var baiduUrl = "http://online1.map.bdimg.com/onlinelabel";
    //var baiduGroupLayer = new EzServerClient.GroupLayer("百度地图", [new EzServerClient.Layer.BaiduTileLayer("baiduLayer", arcgisolineUrl)]);
    //this.myEzMap.addGroupLayer(arcgisGroupLayer);

    this.MarkerIcon = (function () {
        var icons = {};
        //人员
        icons.Person = new Icon();
        icons.Person.image = "/Content/img/mapPosition/person.png";
        icons.Person.height = 28;
        icons.Person.width = 19;
        icons.Person.topOffset = -14;

        //基站
        icons.BaseStation = new Icon();
        icons.BaseStation.image = "/Content/img/mapPosition/baseStation.png";
        icons.BaseStation.height = 26;
        icons.BaseStation.width = 36;
        icons.BaseStation.topOffset = -13;

        return icons;
    })();
}

PGisMap.prototype.AddMarker = function (marker, isCenter) {
    this.myEzMap.addOverlay(marker);
    if (isCenter) {
        var point = marker.getPoint();
        this.myEzMap.centerAtLatLng(point);
    }
}

PGisMap.prototype.AddMarkers = function (markers) {
    if (!(arguments[0] instanceof Array)) {
        throw EzErrorFactory.createError("AddMarkers方法中arguments[0]类型必须为数组");
    }
    if (markers.length < 2) {
        throw EzErrorFactory.createError("AddMarkers方法中参数数组length必须大于或者等于2");
    }
    var mbr = null;
    for (var i = 0; i < markers.length; i++) {
        this.AddMarker(markers[i], false);
        if (i == 1) {
            var point1 = markers[0].getPoint();
            var point2 = markers[1].getPoint();
            mbr = new MBR(point1.getX(), point1.getY(), point2.getX(), point2.getY());
        } else if (i > 1) {
            mbr.extend(markers[i].getPoint());
        }
    }
    mbr.scale(2);
    this.myEzMap.centerAtMBR(mbr);
}

//绘制矩形框
PGisMap.prototype.DrawRect = function (callback, isRemoveDrawGeometry) {
    if (typeof callback != "function") {
        throw EzErrorFactory.createError("DrawRect方法中参数类型必须为function");
    }
    this.myEzMap.changeDragMode('drawRect', null, null, function () {
        callback(arguments[0]);
        if (isRemoveDrawGeometry && isRemoveDrawGeometry == true) {
            this.myEzMap.removeDrawGeometry()
        }
    });
}

//绘制圆
PGisMap.prototype.DrawCircle = function (callback, isRemoveDrawGeometry) {
    if (typeof callback != "function") {
        throw EzErrorFactory.createError("DrawRect方法中参数类型必须为function");
    }
    this.myEzMap.changeDragMode('drawCircle', null, null, function () {
        callback(arguments[0]);
        if (isRemoveDrawGeometry && isRemoveDrawGeometry == true) {
            this.myEzMap.removeDrawGeometry()
        }
    });
}

//绘制多边形
PGisMap.prototype.DrawPolygon = function (callback, isRemoveDrawGeometry) {
    if (typeof callback != "function") {
        throw EzErrorFactory.createError("DrawRect方法中参数类型必须为function");
    }
    this.myEzMap.changeDragMode('drawPolygon', null, null, function () {
        callback(arguments[0]);
        if (isRemoveDrawGeometry && isRemoveDrawGeometry == true) {
            this.myEzMap.removeDrawGeometry()
        }
    });
}

//绘制轨迹线路
PGisMap.prototype.AddTrack = function () {
    var thas = this;
    var strPoints = "120.67668, 27.8501, 120.67674, 27.84941, 120.67683, 27.84875, 120.6743, 27.84847, 120.67408, 27.84909, 120.67211, 27.84888, 120.67193, 27.84959, 120.6694, 27.84922, 120.6693, 27.85032, 120.66728, 27.84971, 120.66736, 27.85012, 120.66932, 27.8507, 120.66906, 27.8521, 120.66878, 27.85351, 120.67127, 27.85418, 120.67148, 27.85278, 120.67382, 27.85326, 120.67359, 27.85467, 120.67582, 27.85506, 120.67646, 27.85227, 120.67861, 27.85278, 120.67968, 27.85306";
    var a = strPoints.split(',');
    for (var i = 0; i < a.length; i++) {
        if (i < 2 || i % 2 != 0) {
            continue;
        }
        (function () {
            var pLine = new Polyline(a[i - 2] + "," + a[i - 1] + "," + a[i] + "," + a[i + 1], "#ff0000", 3, 0.8, 1);
            thas.myEzMap.addOverlay(pLine);
        })();
    }
}

//清除地图上的绘制和加入的对象
PGisMap.prototype.Clear = function () {
    this.myEzMap.clear();
}



