function closeWebPage() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {
        if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
            window.opener = null;
            window.close();
        } else {
            window.open('', '_top');
            window.top.close();
        }
    }
    else if (navigator.userAgent.indexOf("Firefox") > 0) {
        window.location.href = 'about:blank ';
    } else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}

function closeSelfDialog() {
    $.iframeDialogClose();
}

String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
                else {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, '');
                }
            }
        }
    }
    return result;
}

//如果设置调试状态
BUI.setDebug(true);

var dfAlert = function (msg) {
    if (typeof (msg) == "string" && msg.indexOf('DF_SCRIPT') >= 0) {
        msg = msg.replace(/DF_SCRIPT/g, '');
        eval(msg);
    }
    else {
        BUI.use('bui/overlay', function (overlay) {
            BUI.Message.Show({
                buttons: [{
                    text: 'OK',
                    elCls: 'button button-primary',
                    handler: function () { this.hide(); }
                }],
                'icon': 'success',
                'msg': msg
            });
        });
        //alert(msg);
    }
};

var dfMessage = function (msg) {
    BUI.use('bui/overlay', function (Overlay) {
        return BUI.Message.Show({
            closeable: false,
            buttons: [],
            width: 400,
            height: 85,
            msg: msg,
            icon: 'info'
        });
    });
};

var dfLog = function (msg) {
    if (console && console.log) {
        console.log(msg);
    }
};

/* 这个是调用示例
dfDialog(800, 600, '百度', 'http://www.baidu.com');
*/
var dfDialog = function (width, height, title, url) {
    // window.showModalDialog(src, '', 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;resizable=yes;status=yes;');
    var iTop = (window.screen.availHeight - 30 - height) / 2; //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth - 10 - width) / 2; //获得窗口的水平位置;
    if (!width) {
        width = window.screen.availWidth - 20;
        iLeft = 0;
    }
    if (!height) {
        height = window.screen.availHeight - 20;
        iTop = 0;
    }
    window.open(url, "_blank", 'height=' + height + ',innerHeight=' + height + ',width=' + width + ',innerWidth=' + width + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');
};

var dfDialogFullScreen = function (url) {
    var popup = window.open(url, "popup", "fullscreen");
    if (popup.outerWidth < screen.availWidth || popup.outerHeight < screen.availHeight) {
        popup.moveTo(0, 0);
        popup.resizeTo(screen.availWidth, screen.availHeight);
    }
    //var win = window.open(url, "_blank", 'width=' + (window.screen.availWidth) + ',height=' + (window.screen.availHeight - 60) + ',top=0,left=0,resizable=yes,status=yes,menubar=no,scrollbars=yes');
};


var dfChangeHandlerToClick = function (buttons) {
    if (buttons) {
        $.each(buttons, function () {
            if (this.handler) {
                this.click = this.handler;
                delete this.handler;
            }
        });
    }
}

var dfOverlayTopFullDialog = function (width, height, title, src, buttons) {
    dfChangeHandlerToClick(buttons);
    var dialog = window.top.$.iframeDialog({
        width: $(window.top).width(),
        height: $(window.top).height(),
        title: title,
        url: src,
        buttons: buttons
    });
    return dialog;
};

var dfOverlayFullDialog = function (width, height, title, src, buttons) {
    dfChangeHandlerToClick(buttons);
    var dialog = window.$.iframeDialog({
        width: $(window).width(),
        height: $(window).height(),
        title: title,
        url: src,
        buttons: buttons
    });
    return dialog;
};

var dfOverlayDialog = function (width, height, title, src, buttons) {
    dfChangeHandlerToClick(buttons);
    var dialog = $.iframeDialog({
        width: width,
        height: height,
        title: title,
        url: src,
        buttons: buttons
    });
    return dialog;
};

var dfOverlayTopDialog = function (width, height, title, src, buttons) {
    dfChangeHandlerToClick(buttons);
    var dialog;
    dialog = window.top.$.iframeDialog({
        width: width,
        height: height,
        title: title,
        url: src,
        buttons: buttons
    });
    return dialog;
};


var updateQueryStringParameter = function (uri, key, value) {
    var re = new RegExp("([?|&])" + key + "=.*?(&|$)", "i");
    separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    } else {
        return uri + separator + key + "=" + value;
    }
};

var getQueryStringByName = function (name) {
    try {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }
    catch (ex) {
    }
}

/* 调用示例
<div id="tab" style="margin: 20px;">
</div>
dfTab('tab', [{
	title: '百度',
	selected: true,
	src: 'http://www.baidu.com'
}, {
	title: '163',
	selected: false,
	src: 'http://www.163.com'
}, {
	title: '搜狐',
	selected: false,
	src: 'http://www.sohu.com'
}]);
*/
var dfTab = function (tabId, height, children, wfCallback, initFunction) {
    BUI.use(['bui/tab', 'bui/mask'], function (Tab) {
        var c = $.map(children, function (val, i) {
            var frameId = BUI.guid('frame');
            var value = '<iframe id="{0}" name="{0}" width="100%" height="{1}px" class="dfTab-iframe" frameborder="0" src="{2}"></iframe>'.format(frameId, height, val.src);
            return {
                title: val.title,
                value: value,
                selected: val.selected,
                panelContent: val.selected ? value : ''
            };
        });
        var tab = new Tab.TabPanel({
            render: '#' + tabId,
            elCls: 'nav-tabs',
            autoRender: true,
            children: c
        });
        tab.on('selectedchange', function (ev) {
            var item = ev.item;
            item.set('panelContent', item.get('value'));
            if (typeof (wfCallback) == 'function') {
                wfCallback(tab, item);
            }
        });
        if (typeof (wfCallback) == 'function') {
            wfCallback(tab, tab.getSelected());
        }
        if (typeof (initFunction) == 'function') {
            initFunction();
        }
    });
};

var dfIframe = function (jqueryEl, src) {
    var frameId = BUI.guid('frame');
    src += "&IFRAME_ID={0}".format(frameId);
    var value = '<iframe id="{0}" name="{0}" width="{1}px" frameborder="0" src="{2}"></iframe>'.format(frameId, $(document).width(), src);
    jqueryEl.append(value);
}

// 计算页面的实际高度，iframe自适应会用到
var calcPageHeight = function (doc) {
    var cHeight = Math.max(doc.body.clientHeight, doc.documentElement.clientHeight);
    var sHeight = Math.max(doc.body.scrollHeight, doc.documentElement.scrollHeight)
    var height = Math.max(cHeight, sHeight);
    return height;
};

var resizeIframeHeight = function () {
    var frameId = getQueryStringByName("IFRAME_ID");
    var height = calcPageHeight(document);
    if (parent && parent.document && frameId && parent.document.getElementById(frameId)) {
        parent.document.getElementById(frameId).style.height = height + 'px'
    }
};

// 模拟链接点击，在本页打开
var dfOpenLink = function (url) {
    if ($('#dfOpenLinkId').length == 0) {
        $(getRes('<a id="dfOpenLinkId" href="{0}">自动转向...</a>').format(url)).appendTo('body');
    }
    $('#dfOpenLinkId').attr('href', url);
    $('#dfOpenLinkId')[0].click();
};

// 模拟链接点击，在新窗口打开
var dfOpenLinkBlank = function (url) {
    if ($('#dfOpenLinkBlankId').length == 0) {
        $('<a id="dfOpenLinkBlankId" target="_blank"  href="{0}"></a>'.format(url)).appendTo('body');
    }
    $('#dfOpenLinkBlankId').attr('href', url);
    $('#dfOpenLinkBlankId')[0].click();
};

// 刷新父窗体
var dfRefreshParentFromWindowOpen = function () {
    if (window.opener) {
        window.opener.location.reload();
    }
};

// 刷新父窗体
var dfRefreshParentFromIframe = function () {
    if (parent) {
        parent.location.reload();
    }
};


// 是否是有效日期
var dfIsValidDate = function (d) {
    if (Object.prototype.toString.call(d) !== "[object Date]")
        return false;
    return !isNaN(d.getTime());
};

var dfReplace = function (value) {
    return value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
};

// 添加隐藏控件
jQuery.fn.addHidden = function (name, value) {
    return this.each(function () {
        var input = $("<input>").attr("type", "hidden").attr("name", name).val(value);
        $(this).append($(input));
    });
};


/**
 * @author Kyle Florence <kyle[dot]florence[at]gmail[dot]com>
 * @website https://github.com/kflorence/jquery-deserialize/
 * @version 1.2.1
 *
 * Dual licensed under the MIT and GPLv2 licenses.
 */
(function (jQuery, undefined) {

    var push = Array.prototype.push,
        rcheck = /^(?:radio|checkbox)$/i,
        rplus = /\+/g,
        rselect = /^(?:option|select-one|select-multiple)$/i,
        rvalue = /^(?:button|color|date|datetime|datetime-local|email|hidden|month|number|password|range|reset|search|submit|tel|text|textarea|time|url|week)$/i;

    function getElements(elements) {
        return elements.map(function () {
            return this.elements ? jQuery.makeArray(this.elements) : this;
        }).filter(":input:not(:disabled)").get();
    }

    function getElementsByName(elements) {
        var current,
            elementsByName = {};

        jQuery.each(elements, function (i, element) {
            current = elementsByName[element.name];
            elementsByName[element.name] = current === undefined ? element :
                (jQuery.isArray(current) ? current.concat(element) : [current, element]);
        });

        return elementsByName;
    }

    jQuery.fn.deserialize = function (data, options) {
        var i, length,
            elements = getElements(this),
            normalized = [];

        if (!data || !elements.length) {
            return this;
        }

        if (jQuery.isArray(data)) {
            normalized = data;

        } else if (jQuery.isPlainObject(data)) {
            var key, value;

            for (key in data) {
                jQuery.isArray(value = data[key]) ?
                    push.apply(normalized, jQuery.map(value, function (v) {
                        return { name: key, value: v };
                    })) : push.call(normalized, { name: key, value: value });
            }

        } else if (typeof data === "string") {
            var parts;

            data = data.split("&");

            for (i = 0, length = data.length; i < length; i++) {
                parts = data[i].split("=");
                push.call(normalized, {
                    name: decodeURIComponent(parts[0].replace(rplus, "%20")),
                    value: decodeURIComponent(parts[1].replace(rplus, "%20"))
                });
            }
        }

        if (!(length = normalized.length)) {
            return this;
        }

        var current, element, j, len, name, property, type, value,
            change = jQuery.noop,
            complete = jQuery.noop,
            names = {};

        options = options || {};
        elements = getElementsByName(elements);

        // Backwards compatible with old arguments: data, callback
        if (jQuery.isFunction(options)) {
            complete = options;

        } else {
            change = jQuery.isFunction(options.change) ? options.change : change;
            complete = jQuery.isFunction(options.complete) ? options.complete : complete;
        }

        for (i = 0; i < length; i++) {
            current = normalized[i];

            name = current.name;
            value = current.value;

            if (!(element = elements[name])) {
                continue;
            }

            type = (len = element.length) ? element[0] : element;
            type = (type.type || type.nodeName).toLowerCase();
            property = null;

            if (rvalue.test(type)) {
                if (len) {
                    j = names[name];
                    element = element[names[name] = (j == undefined) ? 0 : ++j];
                }

                change.call(element, (element.value = value));

            } else if (rcheck.test(type)) {
                property = "checked";

            } else if (rselect.test(type)) {
                property = "selected";
            }

            if (property) {
                if (!len) {
                    element = [element];
                    len = 1;
                }

                for (j = 0; j < len; j++) {
                    current = element[j];

                    if (current.value == value) {
                        change.call(current, (current[property] = true) && value);
                    }
                }
            }
        }

        complete.call(this);

        return this;
    };

})(jQuery);


function insertText(obj, str) {
    if (document.selection) {
        var sel = document.selection.createRange();
        sel.text = str;
    } else if (typeof obj.selectionStart === 'number' && typeof obj.selectionEnd === 'number') {
        var startPos = obj.selectionStart,
            endPos = obj.selectionEnd,
            cursorPos = startPos,
            tmpStr = obj.value;
        obj.value = tmpStr.substring(0, startPos) + str + tmpStr.substring(endPos, tmpStr.length);

        cursorPos += str.length;

        obj.selectionStart = obj.selectionEnd = cursorPos;

    } else {

        obj.value += str;

    }

}

function moveEnd(obj) {

    obj.focus();

    var len = obj.value.length;

    if (document.selection) {

        var sel = obj.createTextRange();

        sel.moveStart('character', len);

        sel.collapse();

        sel.select();

    } else if (typeof obj.selectionStart == 'number' && typeof obj.selectionEnd == 'number') {

        obj.selectionStart = obj.selectionEnd = len;

    }

}


/*
dfGetData('DynamicForm.DFHandler.Test', {'Age':'1','Name':'Test'}, function(data){ dfLog(data.rows.Age); });
*/
var dfGetData = function (serverFunctionName, p, callback, appendMsg) {
    BUI.use('bui/mask', function (Mask) {
        Mask.maskElement('body');
    });
    var jqxhr = $.post('DFHandler.ashx?action=' + serverFunctionName, p)
        .done(function (data) {
            BUI.use('bui/mask', function (Mask) {
                Mask.unmaskElement('body');
            });
            var ret = data;
            if (typeof (ret) == "string") {
                ret = BUI.JSON.parse(data);
            }
            // {"rows":null,"results":0,"hasError":false,"error":null,"data":null}
            if (appendMsg) {
                callback(ret);
            }
            else {
                if (ret.hasError) {
                    dfAlert(ret.error);
                }
                else {
                    if (ret.error) {
                        dfAlert(ret.error);
                    }
                    callback(ret);
                }
            }
        })
        .fail(function () {
            BUI.use('bui/mask', function (Mask) {
                Mask.unmaskElement('body');
            });
            dfAlert('网络错误或者远程处理发生错误');
        });
    return jqxhr;
};


function getSelectedTabPanelId(tabContainerId) {
    var id = $('#' + tabContainerId + ' div.active').attr('id');
    return id.replace(/DF_OUTERDIV_/g, '');
}

function stopBubble(e) {
    //如果提供了事件对象，则这是一个非IE浏览器 
    if (e && e.stopPropagation)
        //因此它支持W3C的stopPropagation()方法 
        e.stopPropagation();
    else
        //否则，我们需要使用IE的方式来取消事件冒泡 
        window.event.cancelBubble = true;
}

//阻止浏览器的默认行为 
function stopDefault(e) {
    //阻止默认浏览器动作(W3C) 
    if (e && e.preventDefault)
        e.preventDefault();
    //IE中阻止函数器默认动作的方式 
    else
        window.event.returnValue = false;
    return false;
}

var downloadFile = function (url) {
    $("body").append("<iframe src='{0}' style='display: none;' ></iframe>".format(url));
}

var dfAppendMsg = function (msg, msgType) {
    var span = "<span class='dynamic-form-core-msg-message'>{0}</span>".format(msg);
    if (msgType == 'Warning') {
        span = "<span class='dynamic-form-core-msg-warning'>{0}</span>".format(msg);
    }
    else if (msgType == 'Error') {
        span = "<span class='dynamic-form-core-msg-error'>{0}</span>".format(msg);
    }
    $('#DF_MSG_ROW').prepend(span);
    $('#DF_MSG').val(span + $('#DF_MSG').val());
}



var dfOverlayAutoHideDialog = function (width, height, title, src, buttons) {
    var dialog;
    BUI.use('bui/overlay', function (Overlay) {
        var btns = [];
        if (BUI.isArray(buttons)) {
            btns = buttons.concat(btns);
        }
        var frameId = BUI.guid('frame');
        dialog = new Overlay.Dialog({
            bodyStyle: {
                padding: '0px'
            },
            title: title,
            width: width,
            height: height,
            closeAction: 'destroy',
            bodyContent: '<iframe id="' + frameId + '" name="' + frameId + '" width="100%" height="100%" frameborder="0" src="' + src + '"></iframe>',
            buttons: btns,
            autoHide: true,
            autoHideDelay: 2000
        });
        dialog.show();
    });
    return dialog;
};

var dfGetClientReportUrl = function (formName, p) {
    var baseUrlFolder = location.href.substring(0, location.href.lastIndexOf('/'));
    var url = "mjetools:{0}?Url=".format(formName) + encodeURIComponent(baseUrlFolder + "/DFHandler.ashx?action=ClientReport&{0}".format(p));
    dfLog(url);
    return url;
}

// Prevents event bubble up or any usage after this is called.
var eventCancel = function (e) {
    if (!e)
        if (window.event) e = window.event;
        else return;
    if (e.cancelBubble != null) e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
    if (e.preventDefault) e.preventDefault();
    if (window.event) e.returnValue = false;
    if (e.cancel != null) e.cancel = true;
}

var loadSelectData = function (selectId, serverFunctionName, p) {
    return dfGetData(serverFunctionName, p, function (data) {
        // 先把选中的值保存下来
        var value = $("#" + selectId).val();
        $('#' + selectId + ' option').remove();
        var isSelected = false;
        if (data.rows) {
            $.each(data.rows, function (index, el) {
                if (el.Value == value) {
                    isSelected = true;
                }
                $('#' + selectId).append("<option value='" + el.Value + "'>" + el.Text + "</option>");
            });
        }
        //if (!isSelected && value) {
        //    $('#' + selectId).append("<option value='" + value + "'>" + value + "</option>");
        //}
        $('#' + selectId).val(value);
    });
};
var formatMoney = function (mVal, iAccuracy) {
    var fTmp = 0.00;//临时变量  
    var iFra = 0;//小数部分  
    var iInt = 0;//整数部分  
    var aBuf = new Array(); //输出缓存  
    var bPositive = true; //保存正负值标记(true:正数)  
    /** 
     * 输出定长字符串，不够补0 
     * <li>闭包函数</li> 
     * @param int iVal 值 
     * @param int iLen 输出的长度 
     */
    function funZero(iVal, iLen) {
        var sTmp = iVal.toString();
        var sBuf = new Array();
        for (var i = 0, iLoop = iLen - sTmp.length; i < iLoop; i++)
            sBuf.push('0');
        sBuf.push(sTmp);
        return sBuf.join('');
    };

    if (typeof (iAccuracy) === 'undefined')
        iAccuracy = 2;
    bPositive = (mVal >= 0);//取出正负号  
    fTmp = (isNaN(fTmp = parseFloat(mVal))) ? 0 : Math.abs(fTmp);//强制转换为绝对值数浮点  
    //所有内容用正数规则处理  
    iInt = parseInt(fTmp); //分离整数部分  
    iFra = parseInt((fTmp - iInt) * Math.pow(10, iAccuracy) + 0.5); //分离小数部分(四舍五入)  

    do {
        aBuf.unshift(funZero(iInt % 1000, 3));
    } while ((iInt = parseInt(iInt / 1000)));
    aBuf[0] = parseInt(aBuf[0]).toString();//最高段区去掉前导0  
    return ((bPositive) ? '' : '-') + aBuf.join(',') + '.' + ((0 === iFra) ? '00' : funZero(iFra, iAccuracy));
};


