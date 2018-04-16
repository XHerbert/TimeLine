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
            success:function(result){
                successCallBack();
            },
            error:function(error){
                errorCallBack();
            }
        });
    };
    module.ajax = ajax;
    module.version = moduleVersion;
    module.name = moduleName;
    return module;
});