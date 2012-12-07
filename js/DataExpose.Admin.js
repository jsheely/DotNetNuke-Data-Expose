(function (name, definition) {
    var theModule = definition(),
        hasDefine = typeof define === 'function',
        hasExports = typeof module !== 'undefined' && module.exports;

    if (hasDefine) { // AMD Module
        define(theModule);
    } else if (hasExports) { // Node.js Module
        module.exports = theModule;
    } else { // Assign to common namespaces or simply the global object (window)


        // account for for flat-file/global module extensions
        var obj = null;
        var namespaces = name.split(".");
        var scope = (this.jQuery || this.ender || this.$ || this);
        for (var i = 0; i < namespaces.length; i++) {
            var packageName = namespaces[i];
            if (obj && i == namespaces.length - 1) {
                obj[packageName] = theModule;
            } else if (typeof scope[packageName] === "undefined") {
                scope[packageName] = {};
            }
            obj = scope[packageName];
        }

    }
})('DataExpose.Admin', function () {

    var plugin = this;

    plugin.defaultOptions = {};
    plugin.opts = {};
    plugin.tabs = {};
    plugin.Feed = {
        ID: -1,
        Name: "",
        Description: "",
        SQL: "",
        ProcName: "",
        Roles: ""
    };

    plugin.testTab = null;

    plugin.ClearForm = function() {
        $("#iit_feedName").val("");
        $("#iit_feedDesc").val("");
        $("#iit_SQL").val("");
        $("#iit_ProcName").val("");
        $("#iit_ID").val("");
        $("#iit_Roles").val("");
        $("#iit_feedUrl").val("");
        $("#iit_feedResults").children().remove();
        $("#iit_TestFeed").hide();
        $("#iit_LnkDelete").hide();
        
        tabs.tabs('select', 0);
        plugin.testTab.hide();
    };

    this.loadFeedDetails = function (feed, switchTabs) {
        $("#iit_LnkDelete").show();
        $("#iit_feedName").val(feed.Name);
        $("#iit_feedDesc").val(feed.Description);
        $("#iit_SQL").val(feed.SQL);
        $("#iit_ProcName").val(feed.ProcName);
        $("#iit_ID").val(feed.ID);
        $("#iit_Roles").val(feed.Roles);
        $("#iit_TestFeed").attr("target", "_blank").attr("href", opts.servicesFramework.getServiceRoot("DataExpose") + "Services/Execute?feed=" + feed.Name + "&feedParams=").show();
        $("#iit_feedUrl").val(opts.servicesFramework.getServiceRoot("DataExpose") + "Services/Execute?feed=" + feed.Name + "&feedParams=");
        if(switchTabs==null || switchTabs) {
            tabs.tabs('select', 0);
        }
        
        plugin.testTab.show();
    };
    

    return {
        Init: function (options) {
            tabs = $("#iit_tabs").tabs({ select: function (event, ui) { if (ui.index == 5) return false; } });
            plugin.testTab = $("a[href='#tabs-4']");
            plugin.testTab.hide();
            opts = $.extend({}, defaultOptions, options);

            $("#iit_BtnTestFeed").button();

            $("#iit_TestFeed").hide();

            $("#iit_NewFeed").click(function (e) {
                $.DataExpose.Admin.NewFeed();
            });

            $("#iit_LnkSave").click(function (e) {
                e.preventDefault();
                $.DataExpose.Admin.SaveFeed();
            });

            $("#iit_BtnTestFeed").click(function(e) {
                e.preventDefault();
                $.DataExpose.Admin.TestFeed();
            });

            $("#iit_LnkDelete").click(function (e) {
                e.preventDefault();
                $.DataExpose.Admin.DeleteFeed();
            }).hide();

            this.GetFeeds();

            return this;
        },
        GetFeeds: function () {
            $.DataExpose.Utils().ShowLoading();
            
            $.ajax({
                type: "POST",
                url: opts.servicesFramework.getServiceRoot("DataExpose") + 'Services/GetFeedList ',
                dataType:'json',
                beforeSend: opts.servicesFramework.setModuleHeaders,
                success: function(data) {
                    $(".iit_feedList ul").empty();

                    $(data).each(function () {
                        var $li = $("<li />");
                        var $a = $("<a />");
                        var $wrench = $("<div />").addClass("ui-icon-wrench").addClass("ui-icon").addClass("iit_listWrench");
                        $a.attr("href", "#").attr("feedId", this.ID).text(this.Name);
                        $a.click($.DataExpose.Admin.GetFeedDetails);
                        $(".iit_feedList ul").append($li.append($wrench).append($a));
                        
                    });
                    $(".iit_feedList li:even").css("backgroundColor", "#FAFAFA");

                },
                error: function (xhr, status, error) {
                    $.DataExpose.Utils().ShowMessage("error", error);
                }

            }).complete(function () {
                $.DataExpose.Utils().HideLoading();
            });

            return this;
        },
        GetFeedDetails: function (e) {
            $.DataExpose.Utils().ShowLoading();
            plugin.ClearForm();
            e.preventDefault();
            $(".iit_FeedSelected").removeClass("iit_FeedSelected");

            $(this).parent().addClass("iit_FeedSelected");

            $.ajax({
                type: "POST",
                url: opts.servicesFramework.getServiceRoot("DataExpose") + "Services/GetFeedDetails",
                dataType: 'json',
                beforeSend: opts.servicesFramework.setModuleHeaders,
                data: {feedId: parseInt($(this).attr("feedId"))},
                success: function (data) {
                    plugin.loadFeedDetails(data);
                },
                error: function (xhr, status, error) {
                    $.DataExpose.Utils().ShowMessage("error", error);
                }
            }).complete(function () {
                $.DataExpose.Utils().HideLoading();
            });

            return this;
        },
        NewFeed: function () {
            $("#iit_LnkDelete").hide();
            plugin.ClearForm();
            return this;
        },
        SaveFeed: function () {
            $.DataExpose.Utils().ShowLoading();
            var feed = $.extend({ }, plugin.Feed, null);
            if ($("#iit_ID").val() == "") {
                feed.ID = -1;
            }
            else {
                feed.ID = $("#iit_ID").val();
            }
            
            feed.Name = $("#iit_feedName").val();
            feed.ProcName = $("#iit_ProcName").val();
            feed.Description = $("#iit_feedDesc").val();
            feed.SQL = $("#iit_SQL").val();
            feed.Roles = $("#iit_Roles").val();


            $.ajax({
                type: "POST",
                url: opts.servicesFramework.getServiceRoot("DataExpose") + "Services/SaveFeed",
                dataType: 'json',
                beforeSend: opts.servicesFramework.setModuleHeaders,
                data: feed,
                success: function (data) {
                    plugin.loadFeedDetails(data,false);
                    $.DataExpose.Admin.GetFeeds();
                    $("#iit_LnkDelete").show();
                },
                error: function (xhr, status, error) {
                    $.DataExpose.Utils().ShowMessage("error", error);
                }
            }).complete(function () {
                $.DataExpose.Utils().HideLoading();
            });
        },
        DeleteFeed: function () {
            if (confirm("Are you sure you want to delete this feed?")) {
                if ($("#iit_ID").val() != "") {
                    $.ajax({
                        type: "POST",
                        url: opts.servicesFramework.getServiceRoot("DataExpose") + "Services/DeleteFeed",
                        dataType: 'json',
                        beforeSend: opts.servicesFramework.setModuleHeaders,
                        data: { feedId: parseInt($("#iit_ID").val()) },
                        success: function(data) {
                            $.DataExpose.Admin.GetFeeds();
                            plugin.ClearForm();
                        },
                        error: function(xhr, status, error) {
                            $.DataExpose.Utils().ShowMessage("error", error);
                        }
                    });
                }
            }
        },
        TestFeed: function () {
            $.DataExpose.Utils().ShowLoading();

            $.ajax({
                type: 'GET',
                dataType: $("#iit_ContentType").val(),
                url: $('#iit_feedUrl').val(),
                success: function (data) {
                    myData = data;
                    $("#iit_feedResults").children().remove();
                    if ($('#iit_ContentType').val() == "json") {
                        CodeMirror(document.getElementById('iit_feedResults'), {
                            lineWrapping: true,
                            value: JSON.stringify(data,null,2),
                            mode: "javascript"
                        });
                    }
                    else {
                        CodeMirror(document.getElementById('iit_feedResults'), {
                            lineWrapping: true,
                            value:new XMLSerializer().serializeToString(data),
                            mode: "xml"
                        });
                    }
                    
                },
                error: function(xhr, status, error) {
                $.DataExpose.Utils().ShowMessage("error", error);
            }

            }).complete(function () {
                $.DataExpose.Utils().HideLoading();
            });

            return this;
        }
        
    }

});