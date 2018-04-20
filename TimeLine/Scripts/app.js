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
    var tmp = {};
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

        $("#submitUpdate").bind("click", function () {
            var data = {};
            data.data = $("form").serialize();
            data.url = "/Admin/Update";
            data.type = "POST",
            data.successCallBack = $this.successUpdateCallBack;
            data.errorCallBack = $this.errorCallBack;
            //发送请求
            common.ajax(data.url, data.data, data.type, data.successCallBack, data.errorCallBack);
        });

        //加载列表数据并绑定操作事件
        layui.use('table', function () {
            var table = layui.table;
            table.render({
                elem: '#dataTable'
              , height: 'full-100'
              , skin: 'nob'
              , even: true
              , url: '/Admin/AjaxLineList'
              , loading: true
              , page: true
              , cols: [[
                  { field: 'Id', title: 'Id', sort: true, width: 80, fixed: 'left' }
                , { field: 'TitleYear', title: '年',  width: 80,  sort: true}
                , { field: 'TitleMonth', title: '月', width: 50}
                , { field: 'TitleDay', title: '日',width: 50 }
                , { field: 'Copy', title: '背景' }
                , { field: 'Images', title: '地址', minWidth: 380 }
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
                    field: 'IsDeleted', title: '有效', width: 60,
                    templet: function (d) {
                        if (d.IsDeleted == true) {
                            return '无效'
                        } else {
                            return '有效'
                        }
                    }, style: "color:#5FB878 ;"
                },
                { fixed: 'right', title: "操作", align: 'center', toolbar: '#toolbar', width: 190 }
              ]]
            });
            table.on('tool(dataTb)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
                var data = obj.data; //获得当前行数据
                var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
                var tr = obj.tr; //获得当前行 tr 的DOM对象

                if (layEvent === 'detail') { //查看
                    //layer.msg("detail");
                    layer.open({
                        type: 2,
                        title: false,
                        skin: 'layui-layer-rim',
                        area: ['503px', '332px'],
                        shade: 0.5,
                        closeBtn: 1,
                        content: [data.Images,'no']
                    });

                } else if (layEvent === 'del') { //删除
                    layer.confirm('确定删除当前记录么？', function (index) {
                        obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                        layer.close(index);
                        //向服务端发送删除指令
                        $this.delete(data.Id);
                    });
                } else if (layEvent === 'edit') { //编辑
                    var idx = layer.open({
                        type: 2,
                        title: "修改快照",
                        skin: 'layui-layer-rim',
                        area: ['1240px', '693px'],
                        shade: 0.5,
                        fixed:false,
                        maxmin:true,
                        content: ['/Admin/Edit?id='+data.Id, 'no']
                    });
                    
                    //同步更新缓存对应的值
                    //obj.update({
                    //    TitleYear:  '2222',
                    //    TitleMonth:  tmp.TitleMonth,
                    //    TitleDay:  tmp.TitleDay,
                    //    Copy:  tmp.Copy,
                    //    Images:  tmp.Images,
                    //    CreateTime: tmp.CreateTime,
                    //    UpdateTime: tmp.UpdateTime
                    //});
                    //还原对象
                    tmp = {};
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

    //更新成功回调
    app.prototype.successUpdateCallBack = function (result) {
        var $this = this;
        if (result.Code == 200) {
            //tmp.TitleYear  = result.Data.TitleYear;
            //tmp.TitleMonth = result.Data.TitleMonth;
            //tmp.TitleDay = result.Data.TitleDay;
            //tmp.Images = result.Data.Images;
            //tmp.CreateTime = result.Data.CreateTime;
            //tmp.Copy = result.Data.Copy;
            //tmp.UpdateTime = result.Data.UpdateTime;
            $this.layer.msg("修改成功", { time: 3000, icon: 6 });
            setTimeout(function () {
                var idx = parent.layer.getFrameIndex(window.name);
                parent.layer.close(idx);
            }, 3000);
            setTimeout(function () {
                parent.window.location = "/Admin/LineList";
            },400);
        }
        else {
            $this.layer.msg("修改失败", { time: 3000, icon: 5 });
        }
    },

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

    //载入页面
    app.prototype.pageLoad();
});