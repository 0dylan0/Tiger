﻿(function () {
    var ignoreElems = ".k-dropdown, .k-numerictextbox, .k-datepicker"
    $.validator.prototype.defaultShowErrors = function () {
        var i, elements, error;
        for (i = 0; this.errorList[i]; i++) {
            error = this.errorList[i];
            if (this.settings.highlight) {
                this.settings.highlight.call(this, error.element, this.settings.errorClass, this.settings.validClass);
            }
            this.showLabel(error.element, error.message);
        }
        if (this.errorList.length) {
            this.toShow = this.toShow.add(this.containers);
        }
        if (this.settings.success) {
            for (i = 0; this.successList[i]; i++) {
                this.showLabel(this.successList[i]);
            }
        }
        if (this.settings.unhighlight) {
            for (i = 0, elements = this.validElements() ; elements[i]; i++) {
                this.settings.unhighlight.call(this, elements[i], this.settings.errorClass, this.settings.validClass);
            }
        }
        //这行代码是唯一与jquery.validate.js中defaultShowErrors方法不同的代码
        //暂时采用此方式避免使用KendoNumericTextBox和jqueryValidator导致的表单提交时的数字控件隐藏问题
        //以后若更新jquery.validate.js，请务必更新此方法
        this.toHide = this.toHide.not(this.toShow).not(ignoreElems);
        this.hideErrors();
        this.addWrapper(this.toShow).show();
    }
})();