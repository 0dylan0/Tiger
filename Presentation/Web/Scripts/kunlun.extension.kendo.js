(function ($, undefined) {
    var SELECTED = "kl-selected",
        SELECTED_CLASS = ".kl-selected",
        FIRST_SELECTED_DATE = "firstSelectedDate",
        SECOND_SELECTED_DATE = "secondSelectedDate",
        DATE_PICKER_BEGIN_DATE = "calendar-begin",
        DATE_PICKER_END_DATE = "calendar-end";

    var proxy = $.proxy,
        ui = kendo.ui,
        Widget = ui.Widget;

    // 无步进按钮的数字输入框
    var NumericTextBoxWithoutSpinner = ui.NumericTextBox.extend({
        init: function (element, options) {
            // base call to widget initialization
            ui.NumericTextBox.fn.init.call(this, element, options);

            this.wrapper
                .find(".k-numeric-wrap")//.css("padding-right", "0")
                .addClass("kl-no-padding-right")
                .find(".k-select").hide();

            this.wrapper.find(".k-link")
                .addClass("k-state-disabled")
                .unbind("mousedown");

            this.element.unbind("keydown");
        },
        options: {
            name: "NumericTextBoxWithoutSpinner"
        },
        readonly: function (readonly) {
            this._editable({
                readonly: readonly === undefined ? true : readonly,
                disable: false
            });

            var numericWrap = this.element.parents(".k-numeric-wrap");
            if (readonly) {
                numericWrap.addClass("k-state-disabled");
            }
            else {
                numericWrap.removeClass("k-state-disabled");
            }
        }
    });

    // 日期区间选择器
    var DateRange = Widget.extend({
        init: function (element, options) {
            var that = this;

            $(options.beginDateSelector).hide();
            $(options.endDateSelector).hide();

            Widget.fn.init.call(this, element, options);

            var icon = $("<span />")
                .addClass("k-icon k-i-calendar")
                .wrap($("<span />")
                .addClass("k-select"))
                .on("click", function () {
                    if (that._isOpen()) {
                        that._close();
                    }
                    else {
                        proxy(that._clickHandler, that)();
                    }
                })
                .parent();

            that._element = $(element).prop("readonly", true)
                    .on("focus", proxy(that._clickHandler, that))
                    .wrap("<span />").parent().addClass("k-picker-wrap k-state-default kl-date-range-wrap")
                    .append(icon)
                    .wrap("<span />").parent().addClass("k-widget k-datepicker k-header").addClass($(element).attr("class")).css("width", "20em")
                    .find("input[type='text']").addClass("k-input").css("width", "100%");

            var defaultValue = that._initDefaultValue(),
                guid = kendo.guid(),
                offset = $(element).offset();

            $(element).data("dateRangeId", guid);

            var panel = $($("#DemoTemplate").html());
            panel.attr("id", guid);

            var culture = kendo.getCulture().calendars.standard,
            rangePad = culture.rangePad;

            panel.find(".culture-pad-left").text(rangePad.left);
            panel.find(".culture-pad-middle").text(rangePad.middle);
            panel.find(".culture-pad-right").text(rangePad.right);

            that._createSingleCalendarWidge(panel.find(".calendar-1"));
            that._createSingleCalendarWidge(panel.find(".calendar-2"));
            that._createSingleCalendarWidge(panel.find(".calendar-3"));

            panel.find(".last-month").click(function (e) {
                that._dateOffset -= 1;
                proxy(that._refreshCalendarWidget, that)();
            });

            panel.find(".next-month").click(function () {
                that._dateOffset += 1;
                proxy(that._refreshCalendarWidget, that)();
            });

            panel.find(".last-year").click(function () {
                that._dateOffset -= 12;
                proxy(that._refreshCalendarWidget, that)();
            });

            panel.find(".next-year").click(function () {
                that._dateOffset += 12;
                proxy(that._refreshCalendarWidget, that)();
            });

            panel.find("button.date-range-cancel").click(function () {
                that._close();
            });

            panel.find("button.date-range-ok").click(function () {
                that._setRangeToDatePicker();

                var range = that._getDisplayRange(),
                    dateFormat = kendo.getCulture().calendars.standard.patterns.d,
                    beginDate = kendo.toString(range.beginDate, dateFormat),
                    endDate = kendo.toString(range.endDate, dateFormat);

                $(that.options.beginDateSelector).val(beginDate);
                $(that.options.endDateSelector).val(endDate);

                that._setRangeToDateElement();
                that._close();
            });

            var datePickerCommonOptions = {
                min: new Date(1950, 0, 1),
                max: kendo.date.today(),
                change: proxy(that._onDatePickerChange, that)
            };

            var calendarBegin = panel.find(".calendar-begin").kendoDatePicker($.extend(datePickerCommonOptions, { value: defaultValue.defaultBeginDate })),
                calendarEnd = panel.find(".calendar-end").kendoDatePicker($.extend(datePickerCommonOptions, { value: defaultValue.defaultEndDate }));

            var automaticOpenCalendar = function () {
                $(this).getKendoDatePicker().open();
            }

            $(calendarBegin).on("click", automaticOpenCalendar);
            $(calendarEnd).on("click", automaticOpenCalendar);

            that.popup = new ui.Popup(panel, { name: "Popup", anchor: that.element.parents(".k-widget") });
            that.popup.bind("close", proxy(this._initDefaultValue, that));

            that._area = $("#" + guid);

            that._initCalendarEventHandlar();
            that._setRangeToDateElement();
        },
        options: {
            name: "DateRange",
            beginDateSelector: null,
            endDateSelector: null
        },

        _firstSelectedDate: undefined,
        _secondSelectedDate: undefined,
        _element: undefined,

        _toDateObject: function (link) {
            var value = $(link).data("value").split("/");
            return new Date(value[0], value[1], value[2]);;
        },

        value: function () {
            return this.getRange();
        },

        getRange: function () {
            var beginDate = this._firstSelectedDate,
                endDate = this._secondSelectedDate;

            if (beginDate) {
                if (endDate) {
                    if (endDate < beginDate) {
                        var tmp = beginDate;
                        beginDate = endDate;
                        endDate = tmp;
                    }
                }
            }

            return {
                beginDate: beginDate,
                endDate: endDate,
                hasBeginAndEndDate: function () {
                    return this.beginDate && this.endDate;
                }
            };
        },

        setRange: function (range) {
            if (range && range.beginDate && range.endDate) {
                this._firstSelectedDate = kendo.parseDate(range.beginDate);
                this._secondSelectedDate = kendo.parseDate(range.endDate);

                $(this.options.beginDateSelector).val(range.beginDate);
                $(this.options.endDateSelector).val(range.endDate);

                this._setRangeToDateElement();
            }
            else {
                throw "必须设置 beginDate 和 endDate";
            }
        },

        _getDisplayRange: function () {
            var range = this.getRange();
            if (range.beginDate && !range.endDate) {
                range.endDate = range.beginDate;
            }

            return range;
        },

        _area: null,
        _dateOffset: 0,
        _getCalendarWidgetDate: function (calendarIndex) {
            var today = kendo.date.today(),
                year = today.getFullYear(),
                currentMonth = today.getMonth(),
                returnMonth = currentMonth + (calendarIndex - 3) + this._dateOffset;

            var returnDate = new Date(year, returnMonth, 1);
            return {
                min: returnDate,
                max: kendo.date.lastDayOfMonth(returnDate)
            };
        },
        _refreshCalendarWidget: function () {
            var that = this,
                area = that._area;

            for (var i = 1; i <= 3; i++) {
                var element = area.find(".calendar-" + i);
                element.getKendoCalendar().destroy();
                element.empty();

                that._createSingleCalendarWidge(element);
            }

            that._initCalendarEventHandlar();
        },
        _initCalendarEventHandlar: function () {
            var that = this;

            that._area.find("table tbody td:not(.k-state-disabled) a")
                .click(proxy(that._dayClickHandler, that))
                .hover(proxy(that._dayHoverHandler, that));
        },
        _createSingleCalendarWidge: function (element) {
            var that = this,
                date = that._getCalendarWidgetDate(element.data("index"));

            var kendoElement = element.kendoCalendar({
                start: "month",
                depth: "month",
                min: date.min,
                max: date.max,
                footer: false,
                disableDates: function (d) {
                    return d > kendo.date.today();
                }
            });

            kendoElement.getKendoCalendar()._click = function () { };
            kendoElement.wrap().find(".k-header a.k-nav-prev, .k-header a.k-nav-next").remove();
            kendoElement.wrap().find(".k-header a.k-nav-fast").off();
            kendoElement.wrap().find(".k-header a.k-nav-fast").click(function (e) {
                e.preventDefault();
                that._area.find(SELECTED_CLASS).removeClass(SELECTED);

                var parent = $(this).parents("div[data-index]"),
                    calendar = parent.getKendoCalendar();

                var a = parent.find("table tbody td:not(.k-state-disabled) a");
                a.each(function (index, element) {
                    if (index == 0) {
                        that._firstSelectedDate = that._toDateObject(this);
                    }

                    if (index == a.length - 1) {
                        that._secondSelectedDate = that._toDateObject(this);
                    }

                    $(this).parent().addClass(SELECTED);
                })

                that._setRangeToDatePicker();
            });

            kendoElement.wrap().find("table tbody td:not(.k-state-disabled) a").each(function () {
                var element = $(this),
                    date = that._toDateObject(element),
                    range = proxy(that.getRange, that)();

                if (range.hasBeginAndEndDate()) {
                    var beginDate = range.beginDate,
                        endDate = range.endDate;

                    if (beginDate <= date && date <= endDate) {
                        var parent = element.parent();
                        parent.addClass(SELECTED);
                    }
                }
            });

            return kendoElement;
        },

        _initDefaultValue: function () {
            var defaultBeginDate = kendo.parseDate($(this.options.beginDateSelector).val()),
                defaultEndDate = kendo.parseDate($(this.options.endDateSelector).val());

            this._firstSelectedDate = defaultBeginDate;
            this._secondSelectedDate = defaultEndDate;

            return {
                defaultBeginDate: defaultBeginDate,
                defaultEndDate: defaultEndDate
            };
        },

        _clickHandler: function (e) {
            var that = this,
                range = that.getRange();

            if (range.hasBeginAndEndDate()) {
                var today = kendo.date.today(),
                    beginDate = range.beginDate;

                var offset = (today.getFullYear() - beginDate.getFullYear()) * 12;
                offset += today.getMonth() - beginDate.getMonth();
                offset *= -1;       // 使小于今天的时间变为负数
                offset += 2;        // 使开始日期处于三个日历的第一个

                that._dateOffset = offset;
            }
            else {
                that._dateOffset = 0;
            }

            that._setRangeToDatePicker();
            that._refreshCalendarWidget();

            that.popup.open();
        },

        _onDatePickerChange: function (e) {
            var that = this,
                sender = e.sender,
                wrapper = sender.wrapper;

            if (wrapper.hasClass(DATE_PICKER_BEGIN_DATE)) {
                that._firstSelectedDate = sender.value();
            }
            else {
                that._secondSelectedDate = sender.value();
            }

            that._refreshCalendarWidget();
        },

        _close: function (e) {
            this.popup.close();
        },

        _isOpen: function () {
            if (this._area && !this._area.parent().is("body")) {
                return this._area.parent().is(":visible");
            }

            return false;
        },

        _dayClickHandler: function (e) {
            var that = this;
            var element = $(e.currentTarget);
            var currentDate = that._toDateObject(element);
            var calendar = element.parents("div.k-calendar");
            var container = calendar.parents(".date-range-container");
            var index = calendar.data("index");
            var selectedDate = that._firstSelectedDate;

            if (selectedDate) {
                // 已经选中第二日期，则清空原有的选择，将当前选中的日期设为第一日期
                if (that._secondSelectedDate) {
                    container.find("table tbody td" + SELECTED_CLASS).removeClass(SELECTED);
                    element.parent().addClass(SELECTED);
                    that._firstSelectedDate = currentDate;
                    that._secondSelectedDate = null;
                }
                else {
                    // 将当前选中的日期作为另一选中日期
                    that._secondSelectedDate = currentDate;
                    element.parent().addClass(SELECTED);
                }
            }
            else {
                // 尚未选中日期
                element.parent().addClass(SELECTED);

                that._firstSelectedDate = currentDate;
            }

            proxy(that._setRangeToDatePicker, that)();
        },
        _dayHoverHandler: function (e) {
            var that = this;
            var a = e.currentTarget;
            var calendar = $(a).parents("div.k-calendar");
            var container = calendar.parents(".date-range-container");
            var selectedDate = that._firstSelectedDate;

            // 只有当第二日期未被选中时，才进行 hover 相关操作
            if (!that._secondSelectedDate) {
                var hoverDate = that._toDateObject(a);

                if (selectedDate) {
                    container.find("div.calendar-area table tbody td a").each(function (index, element) {
                        var item = $(element),
                            parent = item.parent(),
                            date = that._toDateObject(item)

                        if (parent.hasClass("k-state-disabled")) {
                            return;
                        }

                        if ((date >= selectedDate && date <= hoverDate) || (date <= selectedDate && date >= hoverDate)) {
                            parent.addClass(SELECTED);
                        }
                        else {
                            parent.removeClass(SELECTED);
                        }
                    });
                }
            }
        },

        // 将日期区间设置到弹出的日期选择器上
        _setRangeToDatePicker: function () {
            var that = this;

            var beginDateObject = that._area.find("input.calendar-begin").getKendoDatePicker(),
                endDateObject = that._area.find("input.calendar-end").getKendoDatePicker(),
                dateRange = that._getDisplayRange();

            if (dateRange) {
                beginDateObject.value(dateRange.beginDate);
                endDateObject.value(dateRange.endDate);
            }
            else {
                beginDateObject.value(null);
                endDateObject.value(null);
            }
        },

        // 将日期区间设置到用户指定的展示控件上
        _setRangeToDateElement: function () {
            var culture = kendo.getCulture().calendars.standard,
                dateFormat = culture.patterns.d,
                rangePad = culture.rangePad,
                range = this._getDisplayRange();

            if (range.hasBeginAndEndDate()) {
                var beginDate = kendo.toString(range.beginDate, dateFormat),
                    endDate = kendo.toString(range.endDate, dateFormat);

                this._element.val(rangePad.left + beginDate + rangePad.middle + endDate + rangePad.right);
            }
            else {
                this._element.val("");
            }
        }
    });

    ui.plugin(NumericTextBoxWithoutSpinner);
    ui.plugin(DateRange);
})(jQuery);