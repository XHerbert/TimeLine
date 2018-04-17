//模块化
define(["jquery"],function (jquery) {
    var module = {};
    var moduleName = "common";
    var moduleVersion = "1.0.1";
    var $ = jquery;
    var ajax = function (url,data,type,successCallBack,errorCallBack) {
        $.ajax({
            async:true,
            url: url,
            data: data,
            dataType:"json",
            type:type,
            success: function (result) {
                if (result && successCallBack!=null) {
                    successCallBack();
                }
            },
            error: function (error) {
                if (errorCallBack != null) {
                    errorCallBack();
                }
            }
        });
    };
    module.ajax = ajax;
    module.version = moduleVersion;
    module.name = moduleName;
    module.$ = $;
    return module;
});