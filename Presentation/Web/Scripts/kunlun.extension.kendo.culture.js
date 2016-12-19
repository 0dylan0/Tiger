(function () {
    var enUSCulture = kendo.cultures["en-US"];
    if (enUSCulture && enUSCulture.calendars.standard) {
        var standardCalendar = enUSCulture.calendars.standard;

        standardCalendar.rangePad = {
            left: "From ",
            middle: " To ",
            right: ""
        };
    }

    var zhHansCulture = kendo.cultures["zh-Hans"];
    if (zhHansCulture && zhHansCulture.calendars.standard) {
        var standardCalendar = zhHansCulture.calendars.standard;

        standardCalendar.rangePad = {
            left: "",
            middle: " ~ ",
            right: ""
        };

        standardCalendar.patterns.d = "yyyy-MM-dd";
    }
})();