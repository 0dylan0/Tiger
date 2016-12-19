/// <reference path="kunlun.data.js" />
/// <reference path="kunlun.util.js" />
/// <reference path="kunlun.modal.js" />

(function () {
    var defaultOptions = {
        showCategory: true,
        // 当需要显示大类但设置默认选中项时无法以大类code来初始化时，请设置为 true
        // 如果默认选中项中传入了大类code，需要设为 false
        ignoreCheckCategory: true,
        showQuery: true,
        showQueryButton: true,
        multiSelect: false,
        rowCells: 3,
        autoClose: true,
        // 模态窗打开后 focus 的元素，设为 null 表示不 focus
        // 值设置为 selectorContent 对象的属性名
        focus: "queryElement",
        data: {
            category: {
                url: null,
                data: null,
                method: "GET",
                // 自动选择大类仅在单选下可用
                defaultValue: null
            },
            query: null
        },
        // 对没有值的大类进行处理
        unhandledCategory: {
            handleNull: false,
            handleEmpty: false,
            code: null,
            // 本地化时使用
            name: null
        },
        format: {
            // TODO 未来考虑服务器端返回的大类数据只包含 code 和 name，由客户端来进行格式化
            categoryDropDownList: "[{0}] {1}"
        },

        onSave: null,

        title: null,
        selectedCodes: []
    },
    state = {
        isMultiSelect: false,
        ignoreCheckCategory: true
    },
    SelectedItem = kunlun.data.Selector.Item;

    var selectorContent = {
        categoryContainer: "#CategoryCodeContainer",
        categoryElement: "#CategoryCode",
        queryContainer: "#QueryStrContainer",
        queryElement: "#QueryStr",
        queryButtonContainer: "#kunlunSearchContainer",
        queryButtonElement: "#kunlunSearch",
        resultContainer: "#searchResult",
        resultTable: "#searchResult table",
        resultCategory: ".kl-modal-selector-category",
        okButtonElement: "#kunlunOk",
        selectAllContainer: "#selectAllContainer",
        selectAllElement: "#selectAll"
    }

    var selectorHandler = {
        categoryInitHandler: function (kendoDropDownListOptions) {
            var url = kendoDropDownListOptions.url,
                data = kendoDropDownListOptions.data || {},
                param = $.param(data),
                ajaxOptions = {
                    url: url,
                    method: kendoDropDownListOptions.method || "GET"
                };

            if (ajaxOptions.method == "GET") {
                if (param) {
                    ajaxOptions.url += "?" + param;
                }
            }
            else {
                ajaxOptions.data = data;
            }

            var dataSource = new kendo.data.DataSource({
                serverFiltering: false,
                type: "json",
                transport: {
                    read: function (options) {
                        ajaxOptions.success = function (result) {
                            options.success(result);

                            if ($.isFunction(kendoDropDownListOptions.queryCallback)) {
                                kendoDropDownListOptions.queryCallback();
                            }
                        };

                        $.ajax(ajaxOptions);
                    }
                }
            }),
            defaultValue = privateFunction.isMultiSelect() ? [] : (kendoDropDownListOptions.defaultValue == undefined ? [] : kendoDropDownListOptions.defaultValue);

            return {
                filter: "contains",
                optionLabel: kunlun.language.selector.optionLabel,
                dataTextField: "text",
                dataValueField: "value",
                value: defaultValue,
                dataSource: dataSource
            };
        },

        queryButtonClickHandler: function (e) {
            var ajaxObject = e.data,
                queryData = new kunlun.data.Selector.Query(klSelector.getQueryStrValue(), klSelector.getCategoryValue());

            ajaxObject.data = $.extend(ajaxObject.data, queryData);

            $.ajax(ajaxObject);

            return false;
        },

        okButtonClickHandler: function (originalHandler) {
            return function () {
                var selectedData = klSelector.getSelectedData();

                if ($.isFunction(originalHandler)) {
                    originalHandler({
                        codesArray: selectedData.selectedCodes,
                        codes: selectedData.selectedCodes.join(","),
                        data: selectedData.data,
                        item: selectedData.data.length > 0 ? selectedData.data[0] : null
                    });
                }

                if (klSelector.getOptions()["autoClose"]) {
                    $.klModal.closeModal();
                }
            }
        },

        queryBeforeHandler: function () {
            $("#searchResult").hide(0, function () {
                $.klModal.displayModalLoadingInfo();
            });
        },

        querySuccessHandler: function (originalHandler) {
            return function (data, textStatus, jqXHR) {
                // 处理默认选中，为默认选中的项赋 name
                privateFunction.updateSelectedCode(data);

                // isNoQuery 表示当前结果，不是通过关键字查询得到的
                var isNoQuery = klSelector.getQueryStrValue() === "" || klSelector.getQueryStrValue() === null;

                if (!klSelector.isExistsCategory()) {
                    // 无大类的情况
                    var rowTemplate = kendo.template($("#modalSelectorPartialRowTemplate").html()),
                        rows = rowTemplate(privateFunction.grouping(data));

                    if (isNoQuery) {
                        privateFunction.saveAllCodes(data);
                    }

                    privateFunction.setResultTable(rows);
                }
                else {
                    var options = $.klSelector.getOptions(),
                        unhandledCategory = options.unhandledCategory,
                        shouldHandleCategory = unhandledCategory && (unhandledCategory.handleNull || unhandledCategory.handleEmpty),
                        t = {};
                    Object.defineProperty(t, "__length__", { value: 0, enumerable: false, writable: true });

                    for (var i = 0; i < data.length; i++) {
                        var item = data[i],
                            categoryCode = item.categoryCode,
                            categoryName = item.categoryName;

                        if (shouldHandleCategory) {
                            if ((categoryCode === null && unhandledCategory.handleNull) || (categoryCode === "" && unhandledCategory.handleEmpty)) {
                                categoryCode = unhandledCategory.code != undefined ? unhandledCategory.code : null;
                                categoryName = unhandledCategory.name;
                            }
                        }

                        if (t[categoryCode] !== undefined) {
                            t[categoryCode].value.push(item);
                        }
                        else {
                            t[categoryCode] = {
                                key: { categoryCode: categoryCode, categoryName: categoryName },
                                value: [item]
                            };

                            t.__length__++;
                        }
                    }

                    for (var group in t) {
                        t[group].value = privateFunction.grouping(t[group].value);
                    }

                    // CategoryValue 为 "" 表示当前选择的大类为 ALL
                    // QueryStrValue 为 "" 或 null 表示当前不含查询条件
                    // 综上：只有显示全部的时候，才缓存 data 数据
                    if (isNoQuery && (klSelector.isMultiSelect() == false || klSelector.getCategoryValue() == "")) {
                        privateFunction.saveAllCodes(data);
                    }

                    var rowTemplate = kendo.template($("#modalSelectorPartialRowWithCategoryTemplate").html());
                    privateFunction.setResultTable(rowTemplate(t));
                }

                // 处理大类自动选中
                if (privateFunction.isMultiSelect() && klSelector.isExistsCategory()) {
                    var table = privateFunction.getResultTable(),
                        categoryCodes = table.find(selectorContent.resultCategory).toArray().map(function (item) { return item.value; })

                    $.each(categoryCodes, function (index, item) {
                        if (klSelector.isSelectedAll(item)) {
                            table.find(".kl-modal-selector-category[data-code='" + item + "']").prop("checked", true);
                        }
                    });
                }

                privateFunction.setSelectAll();

                if ($.isFunction(originalHandler)) {
                    originalHandler(data, textStatus, jqXHR);
                }

                $("#searchResult").show(0, function () {
                    $.klModal.hideModalLoadingInfo();
                });
            }
        },

        itemSelectedHandler: function () {
            var selectedClass = "kl-selected";
            var current = $(this),
                data = current.data(),
                table = privateFunction.getResultTable();

            if (current.hasClass(selectedClass)) {
                // 已选中的，取消选中
                current.removeClass(selectedClass);
                // 并从已选中的列表中移除
                privateFunction.removeSelectedCode(data.code, data.categoryCode);

                // 多选，并且存在大类，取消当前选中的项的大类复选框的勾选
                if (!isSingle && klSelector.isExistsCategory()) {
                    var currentCategoryCode = current.data("categoryCode"),
                        category = table.find(".kl-modal-selector-category[data-code='" + currentCategoryCode + "']");

                    category.prop("checked", false);
                }
            }
            else {
                // 对于未选中的，多选状态下可以任意选择，如果是单选，则只能选择一个
                var isSingle = !privateFunction.isMultiSelect();
                if (isSingle) {
                    table.find(".kl-selected").removeClass(selectedClass);
                    privateFunction.clearSelectedCode();
                }

                current.addClass(selectedClass)

                // 并添加到已选中列表中
                var customerData = privateFunction.getCustomerData(data);
                privateFunction.addSelectedCode(new SelectedItem(data.code, data.name, data.categoryCode, data.categoryName, customerData));

                // 多选，并且存在大类
                if (!isSingle && klSelector.isExistsCategory()) {
                    var currentCategoryCode = current.data("categoryCode"),
                        category = table.find(".kl-modal-selector-category[data-code='" + currentCategoryCode + "']");

                    // 如果同一个大类的所有小类都被选中了，则为大类的全选打上勾
                    if (privateFunction.isCurrentCategoryItemsSelectedAll(currentCategoryCode)) {
                        category.prop("checked", true);
                    }
                }
            }

            privateFunction.setSelectAll();
        },

        resultCategoryCheckBoxChangeHandler: function () {
            var categoryCode = $(this).val();

            selectorHandler.selectByCategoryCode(categoryCode);
            privateFunction.setSelectAll();
        },

        selectAllHandler: function () {
            var isSelectAll = $(this).is(":checked"),
                categoryCodes = privateFunction.getResultTable().find(".kl-modal-selector-category");

            if (categoryCodes.length > 0) {
                $.each(categoryCodes, function (index, item) {
                    $(item).prop("checked", isSelectAll);
                    selectorHandler.selectByCategoryCode(item.value);
                });
            }
            else {
                var items = privateFunction.getResultTable().find(".kl-can-select");

                if (isSelectAll) {
                    var newItems = [];

                    $.each(items.not(".kl-selected"), function (index, item) {
                        $(item).addClass("kl-selected");

                        var data = $(item).data(),
                            customerData = privateFunction.getCustomerData(data);
                        newItems.push(new SelectedItem(data.code, data.name, null, null, customerData));
                    });

                    privateFunction.addSelectedCode(newItems);
                }
                else {
                    $.each(items.filter(".kl-selected"), function (index, item) {
                        $(item).removeClass("kl-selected");

                        var data = $(item).data();
                        privateFunction.removeSelectedCode(data.code, data.categoryCode);
                    });
                }
            }
        },

        selectByCategoryCode: function (categoryCode) {
            var table = privateFunction.getResultTable(),
                categoryCheckbox = table.find(".kl-modal-selector-category[data-code='" + categoryCode + "']"),
                isChecked = categoryCheckbox.is(":checked"),
                items = table.find("td[data-category-code='" + categoryCode + "']");

            if (isChecked) {
                items = items.not(".kl-selected");

                var newItems = [];

                $.each(items, function (index, item) {
                    var data = $(item).data(),
                        customerData = privateFunction.getCustomerData(data);

                    items.addClass("kl-selected");
                    newItems.push(new SelectedItem(data.code, data.name, data.categoryCode, data.categoryName, customerData));
                });

                privateFunction.addSelectedCode(newItems);
            }
            else {
                // 取消选中
                items.removeClass("kl-selected");
                $.each(items, function (index, item) {
                    privateFunction.removeSelectedCode($(item).data("code"), $(item).data("categoryCode"));
                });
            }
        }
    };

    var privateFunction = {
        getIndex: function (arr, item) {
            /// <summary>获取给定对象在数组中的索引</summary>
            /// <param name="arr" type="Array" elementType="kunlun.data.Selector.Item">要查找的数组</param>
            /// <param name="item" type="kunlun.data.Selector.Item">要查找的对象</param>
            /// <returns type="Number">索引，如果未查到将返回 -1</returns>

            var isMultiSelect = state.isMultiSelect;
            for (var i = 0; i < arr.length; i++) {
                var e = arr[i];
                if (e.code == item.code && (isMultiSelect == false || state.ignoreCheckCategory || (isMultiSelect && e.categoryCode == item.categoryCode))) {
                    return i;
                }
            }

            return -1;
        },

        addSelectedCode: function (item) {
            var codes = klSelector.getSelectedData().data;

            if ($.isArray(item)) {
                codes = codes.concat(item);
            }
            else {
                codes.push(item);
            }

            privateFunction.setSelectedCodes(codes);
        },

        clearSelectedCode: function () {
            privateFunction.setSelectedCodes([]);
        },

        removeSelectedCode: function (code, categoryCode) {
            if (code === undefined || code === null) {
                throw "code 值无效";
            }

            var codes = klSelector.getSelectedData(),
                selectedData = codes.data,
                index = privateFunction.getIndex(selectedData, new kunlun.data.Selector.Item(code, null, categoryCode));

            if (index >= 0) {
                selectedData.splice(index, 1);
            }

            privateFunction.setSelectedCodes(selectedData);
        },

        updateSelectedCode: function (data) {
            /// <summary></summary>
            /// <param name="data" type="Array" elementType="kunlun.data.Selector.Item"></param>

            var selectedData = klSelector.getSelectedData().data;

            $.each(selectedData, function (index, item) {
                var i = privateFunction.getIndex(data, item);
                if (i != -1) {
                    selectedData[index].name = data[i].name;
                    selectedData[index].categoryName = data[i].categoryName;
                    selectedData[index].data = data[i].data;

                    if (selectedData[index].categoryCode == null) {
                        selectedData[index].categoryCode = data[i].categoryCode;
                    }
                }
            });

            privateFunction.setSelectedCodes(selectedData);
        },

        setSelectedCodes: function (codes) {
            var data = privateFunction.getData("options");
            data.selectedCodes = codes;

            $.klModal.getCurrentModalBody().children().data("options", data);
        },

        saveAllCodes: function (data) {
            var allCodes = {};
            if (klSelector.isExistsCategory()) {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];

                    if (allCodes[item.categoryCode] === undefined) {
                        allCodes[item.categoryCode] = {};
                    }

                    allCodes[item.categoryCode][item.code] = item;
                }
            }
            else {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    allCodes[item.code] = item;
                }
            }

            $.data(privateFunction.getDomSelector(), "allCodes", allCodes);
        },

        getAllCodes: function () {
            /// <summary>获取选择器中所有的选项（若是单选，则获取当前分类下所有的选项）</summary>
            /// <returns type="Array" elementType="kunlun.data.Selector.Item">选项</returns>

            return privateFunction.getData("allCodes");
        },

        isInAllCodes: function (item) {
            var allCodes = this.getAllCodes();

            if (klSelector.isExistsCategory()) {
                return allCodes[item.categoryCode] != undefined && allCodes[item.categoryCode][item.code] != undefined;
            }
            else {
                return allCodes[item.code] != undefined;
            }
        },

        getResultTable: function () {
            return privateFunction.getSelector().find(selectorContent.resultTable);
        },

        setResultTable: function (content) {
            var table = privateFunction.getResultTable();

            table.empty();
            table.append(content);
        },

        // 处理自定义的数据内容（在data attribute上以 data-data-* 表示）
        getCustomerData: function (data) {
            var allCodes = this.getAllCodes();

            if (klSelector.isExistsCategory()) {
                if (allCodes[data.categoryCode] != undefined && allCodes[data.categoryCode][data.code] != undefined) {
                    return allCodes[data.categoryCode][data.code].data;
                }
            }
            else {
                if (allCodes[data.code] != undefined) {
                    return allCodes[data.code].data;
                }
            }

            return undefined;
        },

        grouping: function (data) {
            var rowCells = klSelector.getOptions().rowCells,
                group = [],
                row = [];

            for (var i = 0; i < data.length; i++) {
                row.push(data[i]);

                // 到最后一个元素了，需要补全空位
                if ((i == data.length - 1) && (row.length % 3 != 0)) {
                    for (var j = 0; j <= rowCells - row.length; j++) {
                        row.push(null);
                    }
                }

                if (row.length == rowCells) {
                    group.push(row);
                    row = [];
                }
            }

            return group;
        },

        // 判断 body 上是否已经绑定了某个事件
        isBind: function (domElement, eventName, selector) {
            var events = $._data(domElement, "events");
            if (events) {
                if (events[eventName]) {
                    return events[eventName].some(function (e) { return e.selector == selector; })
                }
            }

            return false;
        },

        isMultiSelect: function () {
            //return privateFunction.getData("options")["multiSelect"];
            return state.isMultiSelect;
        },

        // 用于判断当前小类是否被全部选中了
        isCurrentCategoryItemsSelectedAll: function (categoryCode) {
            var table = privateFunction.getResultTable(),
                selectedItemsCount = table.find("td.kl-selected[data-category-code='" + categoryCode + "']").length,
                allItemsCount = table.find("td[data-category-code='" + categoryCode + "']").length;

            return allItemsCount > 0 && selectedItemsCount == allItemsCount;
        },

        // 判断是否所有的小类都被选中了
        isItemsSelectedAll: function () {
            var canSelectItemCount = privateFunction.getResultTable().find("td.kl-can-select").length,
                selectedItemCount = privateFunction.getResultTable().find("td.kl-selected").length;

            return (canSelectItemCount > 0) && (canSelectItemCount == selectedItemCount);
        },

        // 自动判断是否需要勾选“全选”
        setSelectAll: function () {
            $.klModal.getCurrentModalBody().find("#selectAll").prop("checked", privateFunction.isItemsSelectedAll());
        },

        getSelector: function () {
            return $("#KunlunModalSelector");
        },

        getDomSelector: function () {
            return document.getElementById("KunlunModalSelector");
        },

        getData: function (name) {
            if (name != undefined) {
                return privateFunction.getSelector().data(name);
            }

            return privateFunction.getSelector().data();
        },

        // 设置光标焦点位置
        focus: function () {
            var elementName = privateFunction.getData("options")["focus"];

            if (elementName) {
                $.klModal.getCurrentModalBody().find(selectorContent[elementName]).focus();
            }
        }
    };

    var klSelector = {
        show: function (options) {
            $.klModal.showModal(options.title);
            $.klModal.getCurrentModalBody().parents(".modal").on("shown.bs.modal", function () {
                privateFunction.focus();
            });

            var defaultOpt = $.extend(true, {}, defaultOptions);
            options = $.extend(defaultOpt, options);

            var template = kendo.template($("#modalSelectorPartialTemplate").html()),
				templateHtml = $(template({}));

            var categoryContainer = templateHtml.find(selectorContent.categoryContainer),
				categoryElement = templateHtml.find(selectorContent.categoryElement),
				queryContainer = templateHtml.find(selectorContent.queryContainer),
				queryElement = templateHtml.find(selectorContent.queryElement),
				queryButtonContainer = templateHtml.find(selectorContent.queryButtonContainer),
				queryButtonElement = templateHtml.find(selectorContent.queryButtonElement),
                okButtonElement = templateHtml.find(selectorContent.okButtonElement),
                selectAllContainer = templateHtml.find(selectorContent.selectAllContainer),
                selectAllElement = templateHtml.find(selectorContent.selectAllElement);

            if (options.showCategory) {
                if (options.data.category.url == null) {
                    throw "需要显示大类，但未设置大类的数据源";
                }
            }
            else {
                categoryContainer.addClass("hide");
            }

            if (!options.showQuery) {
                queryContainer.addClass("hide");
            }

            if (options.showQueryButton) {
                if (options.data.query == null) {
                    throw "未指定查询数据源";
                }
            }
            else {
                queryButtonContainer.addClass("hide");
            }

            if (!$.isFunction(options.onSave)) {
                throw "未指定 onSave，或传入对象不是一个 Function";
            }

            // 非多选模式，隐藏全选功能
            if (!options.multiSelect) {
                state.isMultiSelect = false;
                selectAllContainer.addClass("hide");
            }
            else {
                state.isMultiSelect = true;
            }

            state.ignoreCheckCategory = !!options.ignoreCheckCategory;

            // 初始化已选中的Codes
            if (options.selectedCodes) {
                var codes = options.selectedCodes,
                    codesArray = [];

                // 支持的格式：
                // 1. 单一的 code
                // "test"
                // 2. code 数组
                // ["test1", "test2"]
                // 3. 单一的 Item（需要符合 SelectedItem 类型的属性）
                // { categoryCode: "testCate", categoryName: "testName", code: "test", name: "name" }
                // 4. Item 数组
                // [{}, {}]
                if (typeof codes == "string") {
                    // 1
                    var code = new SelectedItem(codes);
                    codesArray.push(code);
                }
                else if ($.isArray(codes)) {
                    if (codes[0] !== undefined) {
                        // 2
                        if (typeof codes[0] == "string") {
                            $.each(codes, function (index, item) {
                                var code = new SelectedItem(item);
                                codesArray.push(code);
                            });
                        }

                        // 4
                        if (codes[0] instanceof kunlun.data.Selector.Item) {
                            $.each(codes, function (index, item) {
                                codesArray.push(item);
                            });
                        }
                    }
                }
                else if (codes instanceof kunlun.data.Selector.Item) {
                    // 3
                    codesArray.push(codes);
                }
                else {
                    throw "错误的默认值类型";
                }

                options.selectedCodes = codesArray;
            }
            else {
                options.selectedCodes = [];
            }

            // 绑定查询按钮
            var ajaxObject = options.data.query;
            ajaxObject.beforeSend = selectorHandler.queryBeforeHandler;
            ajaxObject.success = selectorHandler.querySuccessHandler(ajaxObject.success);

            queryButtonElement.on("click", ajaxObject, selectorHandler.queryButtonClickHandler);

            templateHtml.data("options", options);

            // 绑定确定按钮
            options.onSave = selectorHandler.okButtonClickHandler(options.onSave);
            okButtonElement.on("click", options.onSave);

            // 大类的复选框事件
            if (!privateFunction.isBind(document.body, "change", selectorContent.resultCategory)) {
                $("body").on("change", selectorContent.resultCategory, selectorHandler.resultCategoryCheckBoxChangeHandler);
            }

            // 全选事件
            selectAllElement.on("change", selectorHandler.selectAllHandler);

            // 小类选中事件（对未来生成的元素，只有在未绑定的时候才绑定，否则会造成多次绑定）
            var canSelectSelector = ".kl-modal-selector .kl-can-select";
            if (!privateFunction.isBind(document.body, "click", canSelectSelector)) {
                $("body").on("click", canSelectSelector, selectorHandler.itemSelectedHandler);
            }

            // 根据每行显示的单元格数调整模板中的提示语
            templateHtml.find(".kl-modal-selector-default-info").attr("colspan", options.rowCells);

            $.klModal.addToModalBody(templateHtml);
            $.klModal.hideModalLoadingInfoAndDisplayBody();

            // 自动查询
            var autoQuery = function () {
                selectorHandler.queryButtonClickHandler({ data: ajaxObject });
            };

            if (options.showCategory == false) {
                autoQuery();
            }
            else {
                var categoryOptions = options.data.category;
                categoryOptions.queryCallback = autoQuery;

                // 初始化大类下拉
                categoryElement.kendoDropDownList(selectorHandler.categoryInitHandler(categoryOptions));
            }
        },

        isExistsCategory: function () {
            return this.getCategory() != null;
        },

        getCategory: function () {
            if (privateFunction.getData("options")["showCategory"]) {
                return $.klModal.getCurrentModalBody().find(selectorContent.categoryElement);
            }

            return null;
        },

        getCategoryValue: function () {
            var categoryElement = klSelector.getCategory();
            if (categoryElement) {
                return categoryElement.getKendoDropDownList().value();
            }

            return null;
        },

        getQueryStr: function () {
            if (privateFunction.getData("options")["showQuery"]) {
                return $.klModal.getCurrentModalBody().find(selectorContent.queryElement);
            }

            return null;
        },

        getQueryStrValue: function () {
            var queryStrElement = klSelector.getQueryStr();
            if (queryStrElement) {
                return queryStrElement.val();
            }

            return null;
        },

        getSelectedCodes: function () {
            return klSelector.getSelectedData().selectedCodes;
        },

        getSelectedData: function () {
            var currentSelectedCodes = privateFunction.getData("options")["selectedCodes"],
                allCodes = privateFunction.getAllCodes();

            // 剔除不可选的 code
            if (currentSelectedCodes.length > 0 && allCodes != undefined) {
                for (var i = currentSelectedCodes.length - 1; i >= 0; i--) {
                    if (!privateFunction.isInAllCodes(currentSelectedCodes[i])) {
                        currentSelectedCodes.splice(i, 1);
                    }
                }
            }

            return {
                selectedCodes: currentSelectedCodes.map(function (c) { return c.code; }),
                data: currentSelectedCodes
            };
        },

        getOptions: function () {
            return privateFunction.getData("options");
        },

        isMultiSelect: function () {
            return privateFunction.isMultiSelect();
        },

        isSelectedAll: function (categoryCode) {
            return privateFunction.isCurrentCategoryItemsSelectedAll(categoryCode);
        },

        isSelected: function (item) {
            return privateFunction.getIndex(klSelector.getSelectedData().data, item) != -1;
        }
    };

    jQuery.extend({
        klSelector: klSelector
    });
})();