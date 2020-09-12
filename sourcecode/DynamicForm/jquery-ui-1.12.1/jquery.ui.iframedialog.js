/*
* jQuery UI iframeDialog Plugin
* Copyright 2015, LiDecai
* jquery > 2.1.1
* jquery-ui > 1.11.1
* https://github.com/mydevin/jquery.ui.iframedialog
*/
(function ($) {
    $.fn.extend({
        iframeDialog: function (options) {
            var opts = {
                title: $(this).attr("title"),
                url: $(this).attr("href"),
                id: "iframeDialog",
                width: $(window).width() - 50,
                height: $(window).height() - 50,
                modal: true,
                closeText: "关闭窗口",
                beforeClose: function () {
                    $("body").removeClass("overflowhidden");
                    $(this).trigger("closing");
                    $(this).trigger("closeclick");
                }
            };
            $.extend(opts, options);
            $(this).click(function () {
                opts.sTop = $(window).scrollTop();
                var $tp = $(this).position().top + 35;
                var $top = 0;
                if (parseInt($(window).height() - ($tp - opts.sTop)) > opts.height) {
                    $top = $tp - opts.sTop + 35;
                } else {
                    $top = $(window).height() - opts.height + opts.sTop;
                }
                $.extend(opts, { position: { my: "center top", at: "center top+" + $top } });
                //if ($("#" + opts.id).length > 0) {
                //    $("#" + opts.id).html("").dialog("destroy");
                //}
                $("body").addClass("overflowhidden");
                if ($("#" + opts.id).length == 0) {
                    var $div = $("<div/>");
                    $div.attr("id", opts.id);
                    var $style = "<style type=\"text/css\">.overflowhidden { overflow:hidden !important;}.ui-dialog-content { padding: 0px !important; margin-top:3px; overflow: hidden !important; border:1px solid #efefef !important; border-radius:3px; }.ui-dialog-titlebar{ font-size:12px !important;}</style>";
                    $(document.body).append($style).append($div);
                }
                var $iframe = $('<iframe frameborder="0" width="100%" height="100%" marginwidth="0" hspace="0" vspace="0" scrolling="auto" allowtransparency="true" />');
                $iframe.attr("src", opts.url).load(function () {
                    $("#" + opts.id + " iframe").contents().find("body").attr("iframeDialogId", opts.id);
                });
                var dialog = $("#" + opts.id).html($iframe).dialog(opts).dialogExtend({
                    "closable": true,
                    "maximizable": true,
                    "minimizable": true,
                    "collapsable": true,
                    "dblclick": "collapse",
                    "titlebar": "transparent",
                    "minimizeLocation": "right"
                });

                // 这种方式缓存了 dialog，但带来了一个问题，就是调用方如果一直附加事件的话，事件会执行很多次
                // 所以这里需要把事件先清除
                dialog.off('closing');

                return dialog;
            });
        },
        iframeDialogClose: function (fn) {
            $(this).click(function () {
                setTimeout(function () {
                    if (window.parent) {
                        window.parent.$("#iframeDialog").dialog("close");
                        fn && fn.call(new Object());
                    }
                }, 0);
            });
        }
    });
    $.extend({
        iframeDialog: function (options) {
            if (!options.buttons) {
                $.extend(options, { buttons: [] });
            }
            var opts = {
                title: "对话框",
                url: "",
                id: "iframeDialog",
                width: $(window).width() - 50,
                height: $(window).height() - 50,
                modal: true,
                closeText: "关闭窗口",
                beforeClose: function () {
                    $("body").removeClass("overflowhidden");
                    $(this).trigger("closing");
                    $(this).trigger("closeclick");
                }
            };
            $.extend(opts, options);
            if (opts.width > $(window).width()) {
                opts.width = $(window).width();
            }
            if (opts.height > $(window).height()) {
                opts.height = $(window).height();
            }
            $("body").addClass("overflowhidden");
            if ($("#" + opts.id).length == 0) {
                var $div = $("<div/>");
                $div.attr("id", opts.id);
                var $style = "<style type=\"text/css\">.overflowhidden { overflow:hidden !important;}.ui-dialog-content { padding: 0px !important; margin-top:3px; overflow: hidden !important; border:1px solid #efefef !important; border-radius:3px; }.ui-dialog-titlebar{ font-size:12px !important;}</style>";
                $(document.body).append($style).append($div);
            }
            var $iframe = $('<iframe frameborder="0" width="100%" height="100%" marginwidth="0" hspace="0" vspace="0" scrolling="auto" allowtransparency="true" />');
            $iframe.attr("src", opts.url).load(function () {
                $("#" + opts.id + " iframe").contents().find("body").attr("iframeDialogId", opts.id);
            });
            var dialog = $("#" + opts.id).html($iframe).dialog(opts).dialogExtend({
                "closable": true,
                "maximizable": true,
                "minimizable": true,
                "collapsable": true,
                "dblclick": "collapse",
                "titlebar": "transparent",
                "minimizeLocation": "right"
            });
            
            // 这种方式缓存了 dialog，但带来了一个问题，就是调用方如果一直附加事件的话，事件会执行很多次
            // 所以这里需要把事件先清除
            dialog.off('closing');
            return dialog;
        },
        iframeDialogClose: function (fn) {
            setTimeout(function () {
                if (window.parent) {
                    window.parent.$("#iframeDialog").dialog("close");
                    fn && fn.call(new Object());
                }
            }, 0);
        }
    });
})(jQuery);