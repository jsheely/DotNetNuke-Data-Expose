(function (name, definition) {
    var theModule = definition(),
    // this is considered "safe":
      hasDefine = typeof define === 'function' && define.amd,
    // hasDefine = typeof define === 'function',
      hasExports = typeof module !== 'undefined' && module.exports;

    if (hasDefine) { // AMD Module
        define(theModule);
    } else if (hasExports) { // Node.js Module
        module.exports = theModule;
    } else { // Assign to common namespaces or simply the global object (window)
        (this.jQuery || this.ender || this.$ || this)[name] = theModule;
    }
})('DataExpose', function () {
    var module = this;
    module.plugins = [];
    module.options = {};
    module.dataModels = [];



    module.Utils = function () {
        jQuery.extend(this, {
            GetParamByName: function (name) {
                var match = new RegExp('\/' + name + '\/([0-9]+)/', 'gi').exec(window.location.href);
                if (match != null) {
                    return match[1];
                }
                return null;
            },
            ShowLoading: function () {
                $("<div />").addClass("dnnLoading").css({backgroundColor:'#ddd', width: $(".DataExpose").width()+8, height: $(".DataExpose").height(), opacity: .7 }).prependTo($(".DataExpose"));
            },
            HideLoading: function () {
                $(".dnnLoading").remove();
            },
            ShowMessage: function (level, msg) {
                $(".DataExpose .dnnFormMessage").remove();
                var className;
                switch(level) {
                    case "success":
                        className = "dnnFormSuccess";
                        break;
                    case "warning":
                        className = "dnnFormWarning";
                        break;
                    case "info":
                        className = "dnnFormInfo";
                        break;
                    case "error":
                        className = "dnnFormValidationSummary";
                        break;
                    default:
                        className = "dnnFormSuccess";
                        break;
                        
                }
                $("<div />").addClass("dnnFormMessage").addClass(className).text(msg).prependTo($(".DataExpose"));
            }
        });
        return this;
    };


    return this;

});