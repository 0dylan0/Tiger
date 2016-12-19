(function () {
    $.fn.extend({
        cascadeDropdownListFor: function (option) {
            // 全局设置
            // $.cascadeDropdownListSetup = {}
            // 属性与object一致
            //
            //// option 模型
            //var optionModel = {
            //    // 数据源
            //    dataSource: {
            //        // API URL
            //        url: "",
            //        // 可选 请求时要传递的对象（由调用方直接确定对象）
            //        data: {},
            //        // 当data为空时必填 请求时要传递的对象的名称，值会被设置为级联对象的 val()
            //        paramName: {},
            //        // 可选 请求方式，默认GET
            //        method: "",
            //        // 可选 API 返回数据的名称，默认无
            //        dataName: ""
            //    },
            //    // 级联对象 HTML或jQuery对象
            //    cascadeFrom: {},
            //    // 可选 默认选中值，页面第一次加载时使用的默认值，会在Ajax请求完成后设置
            //    defaultSelect: "",
            //    // 可选 显示初始值或提示信息
            //    defaultDisplay: {
            //        // 若defaultDisplay不为空必填 显示的内容
            //        text: "",
            //        // 可选 选项的值，默认为""
            //        value: ""
            //    },
            //    // 保持原有选项，如果设为 true，初次载入时将不会设置 defaultSelect 以及 defaultDisplay
            //    keepOriginalOptions: bool,
            //    // 可选 API 返回数据中表示显示内容的字段名称，默认为 text
            //    textField: "",
            //    // 可选 API 返回数据中表示选项值的内容的字段名称，默认为 value
            //    valueField: "",
            //    // 自定义 option 显示的内容
            //    displayText: "function(value, text)",
            //    // 可选 当级联的控件发生改变触发向API请求数据时，需要执行的函数
            //    beforeChange: "function",
            //    // 可选 填充完成后所调用的方法
            //    onChanged: "function"
            //
            //    // 已公开事件：cascadeStatusChange，在状态改变时触发
            //}

            var setup = $.cascadeDropdownListSetup || {};
            setup.dataSource = setup.dataSource || {};

            option.dataSource = option.dataSource || setup.dataSource || {};
            option.keepOriginalOptions = option.keepOriginalOptions || false;

            var cascadeElement = $(option.cascadeFrom),
                currentElement = $(this);

            // ele is $(this)
            function _init(ele) {
                ele.empty();

                if (option.defaultDisplay) {
                    ele.append(new Option(option.defaultDisplay.text, option.defaultDisplay.value || ""));
                }
            }

            // 获取和填充下拉菜单的内容
            function _getData(ele, defaultValue) {
                // 
                currentElement.cascadeStatus = 1;
                currentElement.trigger("cascadeStatusChange", 1);

                _init(ele);

                var dataName = option.dataSource.dataName,
                    paramObject = _getParamObject(),
                    valueField = option.valueField || setup.valueField || "value",
                    textField = option.textField || setup.textField || "text",
                    groupField = option.groupField || setup.groupField || "group",
                    displayText = option.displayText || setup.displayText;

                if (paramObject !== undefined) {
                    $.ajax({
                        url: option.dataSource.url,
                        method: option.dataSource.method || "GET",
                        data: paramObject,
                        cache: false,
                        beforeSend: function () {
                            // 尚未发送ajax请求
                            currentElement.cascadeStatus = 2;
                            currentElement.trigger("cascadeStatusChange", 2);

                            _callOnChange(option.beforeChange);
                        },
                        success: function (data) {
                            //
                            currentElement.cascadeStatus = 3;
                            currentElement.trigger("cascadeStatusChange", 3);

                            var trueData = data[dataName] || data;
                            if (trueData) {
                                var lastGroup = "";
                                $.each(trueData, function () {
                                    var text = this[textField] || "",
                                        value = this[valueField],
                                        group = this[groupField];
                                    //列表格式为code-name[code]
                                    if (displayText && typeof (displayText) == "function") {
                                        var returnValue = displayText(value, text);
                                        if (returnValue) {
                                            text = returnValue;
                                        }
                                        else {
                                            throw "missing return value";
                                        }
                                    }

                                    if (text != null && value) {
                                        if (group != null) {
                                            if (group.name != null) {
                                                if (lastGroup != group.name) {

                                                    var newGroup = document.createElement('OPTGROUP');
                                                    newGroup.label = group.name;
                                                    newGroup.innerText = " ";
                                                    ele.append(newGroup);
                                                    // ele.find("OPTGROUP").last().append(new Option(text, value));

                                                }
                                                else if (lastGroup == group.name && lastGroup != "") {
                                                    ele.find("OPTGROUP").last().append(new Option(text, value));
                                                }
                                                lastGroup = group.name;
                                            }
                                            else {
                                                ele.append(new Option(text, value));
                                            }
                                        } else {
                                            ele.append(new Option(text, value));
                                        }
                                        if (value == defaultValue) {
                                            ele.find(":last")[0].selected = true;
                                        }
                                    }
                                    else {
                                        throw "textField, valueField error, or return value is null or empty";
                                    }
                                })
                            }
                            else {
                                throw "Ajax Data Error";
                            }

                            //
                            currentElement.cascadeStatus = 4;
                            currentElement.trigger("cascadeStatusChange", 4);

                            _callOnChange(option.onChanged);
                        }
                    })
                }
                else {
                    _callOnChange(option.onChanged);
                }
            }

            // 获取需要进行级联的目标的值
            function _getCascadeElementValue() {
                if (cascadeElement) {
                    return cascadeElement.val();
                }
            }

            // 获取发送给服务器端的对象
            function _getParamObject() {
                if (option.dataSource.data && typeof (option.dataSource.data) == "object") {
                    return option.dataSource.data;
                }

                var objectName = option.dataSource.paramName,
                    dataObject = new Object();

                if (objectName) {
                    dataObject[objectName] = _getCascadeElementValue();
                    return dataObject;
                }
                else {
                    throw "Missing paramName";
                }
            }

            // 检测并调用指定的方法
            function _callOnChange(func) {
                if (func && typeof (func) == "function") {
                    // 在自定义的方法中，this 将会代表select控件的jQuery对象
                    func.call(currentElement)
                }
            }

            // 绑定级联的对象
            if (cascadeElement) {
                // Init
                currentElement.cascadeStatus = 0;
                currentElement.trigger("cascadeStatusChange", 0);

                cascadeElement.bind("change", function () {
                    _getData(currentElement);
                })

                if (!option.keepOriginalOptions) {
                    _getData(currentElement, option.defaultValue);
                }
            }
            else {
                throw "Missing cascadeElement";
            }

            return currentElement;
        }
    })
})()


