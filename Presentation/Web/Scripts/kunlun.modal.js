(function () {
    var klModalSelector = {
        body: ".kl-modal-body",
        title: ".kl-modal-title",
        loadingInfo: ".kl-modal-loading-info",
        saveButton: ".kl-modal-save",
        closeButton: ".kl-modal-close",
        template: ".kl-modal-template"
    };

    var currentModalIndex = 0;

    function getCurrentModalId() {
        return "KunlunModal" + currentModalIndex;
    }

    function getCurrentModalSelector() {
        return "#" + getCurrentModalId();
    }

    function getCurrentModal() {
        return $(getCurrentModalSelector());
    }

    function getCurrentModalClassSelector(className) {
        return getCurrentModalSelector() + " " + className;
    }

    function getCurrentModalClass(className) {
        return $(getCurrentModalClassSelector(className));
    }

    function createNewModal() {
        var templateHtml = $(klModalSelector.template).prop("outerHTML"),
            newModal = $(templateHtml);

        currentModalIndex++;

        newModal.attr("id", getCurrentModalId())
                .removeClass("kl-modal-template");

        $("body").append(newModal);
    }

    function removeModalFromPage(modalIndex) {
        if (getCurrentModal().is(":hidden") || (getCurrentModal().is(":visible") && currentModalIndex == 1)) {
            getCurrentModal().remove();
            currentModalIndex--;
        }
        else if (currentModalIndex > 1) {
            var lastModal = $("#KunlunModal" + (--currentModalIndex));
            if (lastModal.length == 1 && lastModal.is(":hidden")) {
                lastModal.remove();
            }
        }
    }

    var klModal = {
        // 显示模态窗口
        showModal: function (title, showSaveButton, showCloseButton) {
            createNewModal();

            getCurrentModalClass(klModalSelector.title).html(title);
            showSaveButton || getCurrentModalClass(klModalSelector.saveButton).hide();
            showCloseButton || getCurrentModalClass(klModalSelector.closeButton).hide();

            getCurrentModal().modal({ backdrop: false });

            //解决模态框上有kendo的filter下拉菜单时，点击下拉菜单，下拉菜单出现后立马收回的问题，因为bootstrap的模态框不允许其以外的元素获得焦点
            $('.modal').on('shown.bs.modal', function () {
                $(document).off('focusin.modal');
            });
        },

        // 显示信息模态窗
        showInfoModal: function (displayContent, title, showCloseButton) {
            klModal.showModal(title, false, showCloseButton);
            klModal.hideModalLoadingInfoAndDisplayBody();
            klModal.addDataToModalBody({ content: displayContent });
        },

        // 关闭模态窗口
        closeModal: function () {
            getCurrentModal().modal("hide");
        },

        removeModal: function () {
            removeModalFromPage();
        },

        getCurrentModalBody: function () {
            return getCurrentModalClass(klModalSelector.body);
        },

        getData: function (name) {
            var data = klModal.getCurrentModalBody().children().data();

            if (data && name) {
                return data[name];
            }

            return data;
        },

        addToModalBody: function (obj) {
            if (obj) {
                getCurrentModalClass(klModalSelector.body).html(obj);
            }
        },

        addDataToModalBody: function (data) {
            if (data) {
                klModal.addToModalBody(data.content);
            }
        },

        displayModalLoadingInfo: function () {
            getCurrentModalClass(klModalSelector.loadingInfo).show();
        },

        disabledModalSaveButton: function () {
            getCurrentModalClass(klModalSelector.saveButton).prop("disabled", true);
        },

        enableModalSaveButton: function () {
            getCurrentModalClass(klModalSelector.saveButton).prop("disabled", false);
        },

        disabledModalSaveButtonAndDisplayLoadingInfo: function () {
            klModal.disabledModalSaveButton();
            klModal.displayModalLoadingInfo();
        },

        displayModalBody: function () {
            getCurrentModalClass(klModalSelector.body).fadeIn(200);
        },

        hideModalLoadingInfo: function () {
            getCurrentModalClass(klModalSelector.loadingInfo).hide();
        },

        hideModalLoadingInfoAndDisplayBody: function () {
            klModal.hideModalLoadingInfo();
            klModal.displayModalBody();
        },

        bindModalSaveButtonClick: function (callback) {
            if (callback && typeof (callback) == "function") {
                getCurrentModalClass(klModalSelector.saveButton).prop("disabled", false);
                getCurrentModalClass(klModalSelector.saveButton).on("click", callback);
            }
        },

        bindModalLoadedSuccess: function (successCallback) {
            if (successCallback && typeof (successCallback) == "function") {
                successCallback();
            }
        },

        // 重置模态窗口
        resetModal: function () {
            getCurrentModalClass(klModalSelector.body).hide();
            getCurrentModalClass(klModalSelector.title).html("");
            getCurrentModalClass(klModalSelector.body).html("");
            getCurrentModalClass(klModalSelector.saveButton).off("click");
            getCurrentModalClass(klModalSelector.saveButton).show();
            getCurrentModalClass(klModalSelector.closeButton).show();
        }
    };

    jQuery.extend({
        klModal: klModal
    });

    $(function () {
        $("body").on("hidden.bs.modal", function () {
            removeModalFromPage();
        });
    });
})();