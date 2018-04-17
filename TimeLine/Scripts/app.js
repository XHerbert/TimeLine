//////加载JS
require.config({
    //baseUrl: 'http://localhost:810/Scripts',
    paths: {
        "jquery": "http://localhost:810/Scripts/jquery-1.10.2",
        "layui": "http://localhost:810/Scripts/layui/layui",
        "layer": "http://localhost:810/Scripts/layui/lay/modules/layer",
        "common": "http://localhost:810/Scripts/lib/common",
        "deleterecord": "http://localhost:810/Scripts/lib/deleterecord"
    }, shim: {
        "layui": {
            exports: "layui"
        },
        "layer": {
            exports: "layer"  //之前一直报undefined，是因为这个lib 不是标准的AMD规范
        },
        "deleterecord": {
            deps: ["jquery", "common"]
        }
    }
});

//////核心类库
require(["jquery", "layui", "layer", "common", "deleterecord"], function ($, layui, layer, common, _) {
    var layui = layui;
    var layer = layer;
    var common = common;
    var $ = $ || {};
    var del = _;
    var app = function () { };
    //定义函数功能
    app.prototype.pageLoad = function () {
        var $this = this;
        //common.ajax();
        //开始绑定事件

        $("#submitBtn").bind("click", function () {
            var data = {};
            data.data = $("form").serialize();
            data.url = "/Admin/Create";
            data.type = "POST",
            data.successCallBack = $this.successCallBack;
            data.errorCallBack = $this.errorCallBack;
            //发送请求
            common.ajax(data.url, data.data, data.type, data.successCallBack, data.errorCallBack);

        });

        //加载列表数据并绑定操作事件
        layui.use('table', function () {
            var table = layui.table;
            table.render({
                elem: '#dataTable'
              , height: 'full-50'
              , skin: 'nob'
              , even: true
              , url: '/Admin/AjaxLineList'
              , loading: true
              , page: true
              , cols: [[
                { field: 'Id', title: 'Id', sort: true, width: 80, templet: function (d) { return d.Id }, fixed: 'left' }
                , { field: 'TitleYear', title: '年', width: 80, sort: true }
                , { field: 'TitleMonth', title: '月', width: 80 }
                , { field: 'TitleDay', title: '日', width: 80 }
                , { field: 'Copy', title: '背景' }
                , { field: 'Images', title: '地址', width: 400 }
                , {
                    field: 'CreateTime', title: '创建时间', width: 180,
                    templet: function (d) {
                        var stamp = d.CreateTime.replace(/[^0-9]/ig, "");
                        var date = new Date();
                        date.setTime(stamp);
                        return date.Format("yyyy-MM-dd hh:mm:ss");
                    }
                }
                , {
                    field: 'UpdateTime', title: '修改时间', width: 180,
                    templet: function (d) {
                        var stamp = d.UpdateTime.replace(/[^0-9]/ig, "");
                        var date = new Date();
                        date.setTime(stamp);
                        return date.Format("yyyy-MM-dd hh:mm:ss");
                    }
                }
                , {
                    field: 'IsDeleted', title: '有效', width: 80,
                    templet: function (d) {
                        if (d.IsDeleted == true) {
                            return '无效'
                        } else {
                            return '有效'
                        }
                    }, style: "color:#5FB878 ;"
                },
                { fixed: 'right', title: "操作", width: 250, align: 'center', toolbar: '#toolbar' }
              ]]
            });
            table.on('tool(dataTb)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
                var data = obj.data; //获得当前行数据
                var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
                var tr = obj.tr; //获得当前行 tr 的DOM对象

                if (layEvent === 'detail') { //查看
                    layer.msg("detail");
                } else if (layEvent === 'del') { //删除
                    layer.confirm('真的删除行么', function (index) {
                        obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                        layer.close(index);
                        //向服务端发送删除指令
                        $this.delete(data.Id);
                    });
                } else if (layEvent === 'edit') { //编辑

                    layer.msg("edit");
                    //同步更新缓存对应的值
                    obj.update({
                        username: '123'
                      , title: 'xxx'
                    });
                }
            });
        });

        //上传文件
        layui.use('upload', function () {
            var upld = layui.upload;
            var uploadInst = upld.render({
                elem: '#upld', //绑定元素
                loading: true,
                url: '/Admin/UploadImage',//上传接口
                done: function (res, index, upload) {
                    //上传完毕回调
                    $("#img").val(res.Data);
                },
                accept: 'jpg|png|gif|bmp|jpeg',
                size: 500,
                error: function (err) {
                    //请求异常回调
                }
            });
        });

        //日期选择
        layui.use('laydate', function () {
            var laydate = layui.laydate;
            laydate.render({
                elem: "#date",
                theme: 'grid',
                done: function (value, date) {
                    $("input[name='TitleYear']").val(date.year);
                    $("input[name='TitleMonth']").val(date.month);
                    $("input[name='TitleDay']").val(date.date);
                }
            });
        });
    };

    //发布成功回调
    app.prototype.successCallBack = function () {
        var $this = this;
        $this.layer.msg("发布成功", { time: 3000, icon: 6 });
        setTimeout(function () {
            window.location = "/Admin/LineList";
        }, 3000);
    },

    //删除成功回调
    app.prototype.successDeleteCallBack = function () {
        layer.msg("删除成功", { time: 3000, icon: 6 });
    }

    //发生错误回调
    app.prototype.errorCallBack = function (err) {

    },

    //删除记录
    app.prototype.delete = function (id) {
        var $this = this;
        var data = {};
        data.data = { id: id };
        data.url = "/Admin/Delete";
        data.type = "POST",
        data.successCallBack = $this.successDeleteCallBack;
        //发送请求
        common.ajax(data.url, data.data, data.type, data.successCallBack, null);
    }

    app.prototype.pageLoad();
});