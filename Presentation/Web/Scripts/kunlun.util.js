(function () {
    jQuery.extend(String.prototype, {
        formate: function () {
            var content = String(this);
            for (var i = 0; i < arguments.length; i++) {
                eval("var re = /\\{" + i + "\\}/g;");
                content = content.replace(re, arguments[i]);
            }

            return content;
        }
    });

    jQuery.extend({
        // 通过 Ctrl + Enter 来提交表单
        ctrlEnterSubmit: function () {
            $(document).keydown(function (e) {
                if (e.ctrlKey && e.which == 13) {
                    $("form").submit();
                }
            });
        },

        scrollTo: function (element) {
            var top = 0;

            if (element.is(":hidden")) {
                var firstVisibleParent = element.parents(":visible").first();
                top = firstVisibleParent.offset().top;
            }
            else {
                top = element.offset().top;
            }

            // IE 不支持对 body 添加 scrollTop； Chrome 不支持对 html 添加 scrollTop
            $("html, body").animate({
                scrollTop: top
            });
        },

        // 重置下拉菜单
        resetDropdownList: function (control) {
            if (control) {
                var option = document.createElement("option");
                option.value = "";
                option.text = "";

                control.empty();
                control.append(option);

                if (control.kendoDropDownList) {
                    control.kendoDropDownList();
                }
            }
        },

        // 清空 input 的内容
        clearInput: function (e) {
            if (e && e.data) {
                if (typeof (e.data) == "function") {
                    $(e).val("");
                }
                else {
                    $(e.data).val("");
                }
            }
            else {
                throw new Error("对象不存在或不支持此操作");
            }
        },

        // 创建 input hidden
        createHidden: function (name, value, setId) {
            var element = document.createElement("input");
            element.type = "hidden";

            if ((typeof (setId) == "boolean" && setId === true) || (typeof (setId) == "undefined")) {
                element.id = name;
            }
            else if (typeof (setId) == "string") {
                element.id = setId;
            }

            element.name = name;
            element.value = value;

            return element;
        },

        createHiddenStr: function (name, value, setId) {
            var element = $.createHidden(name, value, setId)

            return element.outerHTML;
        },

        // 获取指定行的数据对象
        getRowData: function (index, selector, properties) {
            var rowData = {};

            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = "{0}\\\[{1}\\\]\\\.{2}".formate(selector, index, propertyName);

                // 应对使用属性选择器的情况
                if (/\[/.test(selector)) {
                    propertySelector += "]";
                }

                rowData[propertyName] = $(propertySelector).val()
            }

            return rowData;
        },

        // 设置指定行的数据
        setRowData: function (index, selector, dataObject, properties) {
            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = "{0}\\\[{1}\\\]\\\.{2}".formate(selector, index, propertyName);

                $(propertySelector).val(dataObject[propertyName]);
            }
        },

        // 向指定元素追加行数据
        insertRowData: function (elementSelector, index, selector, dataObject, properties) {
            var element = $(elementSelector);
            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = "{0}[{1}].{2}".formate(selector, index, propertyName);

                element.append($.createHidden(propertySelector, dataObject[propertyName]));
            }
        },

        // 向指定元素插入隐藏域
        insertHiddenData: function (elementSelector, selector, dataObject, properties) {
            var element = $(elementSelector);
            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = "{0}.{1}".formate(selector, propertyName);

                element.append($.createHidden(propertySelector, dataObject[propertyName]));
            }
        },

        // 获取给定元素中的数据对象
        getDataFromParent: function (parentElement, selector, isSelectorContainFormate, properties) {
            var data = {};

            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = undefined;

                if (isSelectorContainFormate) {
                    propertySelector = selector.formate(propertyName);
                }
                else {
                    propertySelector = selector + propertyName;
                }

                data[propertyName] = parentElement.find(propertySelector).val();
            }

            return data;
        },

        // 为给定选择器指定的元素赋值
        setDataToElement: function (selector, dataObject, properties, camel) {
            for (var i = 0; i < properties.length; i++) {
                var propertyName = properties[i],
                    propertySelector = "#{0}{1}".formate(selector, propertyName);
                if (camel) {
                    propertyName = propertyName.substring(0, 1).toLowerCase() + propertyName.substring(1);
                }
                $(propertySelector).val(dataObject[propertyName]);
            }
        },

        // 禁用给定选择器内，所有的表单元素
        disableElements: function (selector) {
            $(selector).find(":input, a.btn").each(function (i, ele) {
                ele = $(ele);

                if (ele.is("select")) {
                    if (ele.getKendoDropDownList()) {
                        ele.data("kendoDropDownList").enable(false);
                    }
                }
                else if (ele.is("input")) {
                    if (ele.getKendoDatePicker()) {
                        ele.data("kendoDatePicker").enable(false);
                    }
                }

                if (ele.attr("data-widget") === undefined) {
                    ele.prop("disabled", true);
                }

                if (ele.hasClass("cro-do-not-disable")) {
                    ele.prop("disabled", false);
                }
            });
        },

        handleNull: function (obj) {
            return obj === null ? "" : obj;
        },

        htmlEncode: function (str) {
            var s = "";
            if (!str || str.length == 0) return "";
            s = str.replace(/&/g, "&amp;");
            s = s.replace(/</g, "&lt;");
            s = s.replace(/>/g, "&gt;");
            s = s.replace(/ /g, "&nbsp;");
            s = s.replace(/\'/g, "&#39;");
            s = s.replace(/\"/g, "&quot;");
            return s;
        },

        htmlDecode: function (str) {
            var s = "";
            if (!str || str.length == 0) return "";
            s = str.replace(/&amp;/g, "&");
            s = s.replace(/&lt;/g, "<");
            s = s.replace(/&gt;/g, ">");
            s = s.replace(/&nbsp;/g, " ");
            s = s.replace(/&#39;/g, "\'");
            s = s.replace(/&quot;/g, "\"");
            return s;
        },

        parseBoolen: function (str) {
            if (!str) {
                return !!str;
            }

            return str.toLowerCase() == "true";
        },

        getEchartsInstance: function (id) {
            return echarts.getInstanceByDom(document.getElementById(id));
        },

        getEchartsDataUri: function (id) {
            return this.getEchartsInstance(id).getDataURL({ backgroundColor: "#FFF" });
        },

        onKendoNumericTextBoxChangeToNull: function () {
            if (this.value() == null) {
                this.value(this.min() || 0);
            }
        }
    });

    jQuery.extend(Date.prototype, {
        // datetime format
        format: function (fmt) {
            return kendo.toString(this, fmt);
        }
    });
})();

// 重写jquery验证实现包含全角字符数据最大长度校验
jQuery.validator.addMethod("maxlength",
		function (value, element, params) {
		    var maxLength = value.replace(/[^\x00-\xff]/g, "aa").length;
		    return params >= maxLength;
		});

function FormatDate(originalDateStr, format) {
    format = format || "yyyy-MM-dd HH:mm:ss";
    var date = kendo.parseDate(originalDateStr);
    return kendo.toString(date, format);
}

//CSRF
function addAntiForgeryToken(data) {
    if (!data) {
        data = {};
    }
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};