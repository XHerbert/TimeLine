///////
require.config({
    baseUrl: './Scripts',
    paths: {
        "jquery": "jquery-1.10.2",
        "layer": "layui/lay/modules/layer",
        "common": "lib/common",
        "layui": "layui/layui",
    }, shim: {
        "layui": {
            exports:"layui"
        }
    }
});

//require(["lib/alert"]);

//require(["common"], function (common) {
//    var c = common;
//    alert(c.version);
//});
//require(["layui/lay/modules/upload"], ["layui"],function (upld) {
//    var l = upld;
//});
//require(["layui"],function (upld)  {
//    //执行实例
//    var upld = upload;
//    $("#upld").bind("click", function () {
//        var uploadInst = upld.render({
//            elem: '#upld', //绑定元素
//            url: '/Admin/UploadImage',//上传接口
//            done: function (res) {
//                //上传完毕回调
//                $("#img").val("1234");
//            },
//            error: function () {
//                //请求异常回调
//            }
//        });
//    });
//})


require(["layer", "jquery","common"], function (layer, $,common) {

    var layer = layer;
    var common = common;
    var upload = upld;
    var $ = $ || {};
    var app = function () { };
    
    //定义函数功能
    app.prototype.pageLoad = function () {
        //layer.msg("yyy");
        //开始绑定事件
        $("#upld").bind("click", function () {
            //require(["alert"]);
            require(["layui"], function (layui) {
                //执行实例
                layui.use('upload', function () {
                    var upld = layui.upload;
                    var uploadInst = upld.render({
                        elem: '#upld', //绑定元素
                        url: '/Admin/UploadImage',//上传接口
                        done: function (res,index,upload) {
                            //上传完毕回调
                            $("#img").val("1234");
                        },
                        accept: 'jpg',
                        size:50,
                        error: function () {
                            //请求异常回调
                        }
                    });
                })              
            })
        });
    };
    app.prototype.upload = function () {
        //执行实例
        var uploadInst = upload.render({
            elem: '#upld', //绑定元素
            url: '/Admin/UploadImage',//上传接口
            done: function (res) {
                //上传完毕回调
                $("#img").val("1234");
            },
            error: function () {
                //请求异常回调
            }
        });
    };

    app.prototype.pageLoad();
});


