(function () {
    if (alertify) {
        //bootstrap theme
        alertify.defaults.transition = "slide";
        alertify.defaults.theme.ok = "btn btn-primary";
        alertify.defaults.theme.cancel = "btn btn-default";
        alertify.defaults.theme.input = "form-control";

        // Error
        alertify.klErrorAlert || alertify.dialog('klErrorAlert', function factory() {
            return {
                main: function (message, header, onclose) {
                    this.message = message;
                    this.header = header;
                    this.onclose = onclose || function () { };
                },
                setup: function () {
                    return {
                        buttons: [{
                            text: alertify.defaults.glossary.ok,
                            key: 27/*Esc*/,
                            className: alertify.defaults.theme.ok
                        }],
                        focus: { element: 0 },
                        options: {
                            resizable: false,
                            maximizable: false,
                            movable: false,
                            transition: 'flipx',
                            closableByDimmer: false
                        }
                    };
                },
                build: function () {
                    $(this.elements.header).addClass("kl-alert-error");
                },
                prepare: function () {
                    this.setContent(this.message);
                    this.setHeader("<span>{0}</span>".formate(this.header || alertify.defaults.kunlun.errorTitle));
                    this.set({
                        onclose: this.onclose
                    })
                }
            }
        });

        // Warning
        alertify.klWarningAlert || alertify.dialog('klWarningAlert', function factory() {
            return {
                main: function (message, header, onclose) {
                    this.message = message;
                    this.header = header;
                    this.onclose = onclose || function () { };
                },
                setup: function () {
                    return {
                        buttons: [{
                            text: alertify.defaults.glossary.ok,
                            key: 27/*Esc*/,
                            className: alertify.defaults.theme.ok
                        }],
                        focus: { element: 0 },
                        options: {
                            resizable: false,
                            maximizable: false,
                            movable: false,
                            transition: 'flipx',
                            closableByDimmer: false
                        }
                    };
                },
                build: function () {
                    $(this.elements.header).addClass("kl-alert-warning");
                },
                prepare: function () {
                    this.setContent(this.message);
                    this.setHeader("<span>{0}</span>".formate(this.header || alertify.defaults.kunlun.warningTitle));
                    this.set({
                        onclose: this.onclose
                    })
                }
            }
        });

        // Confirm
        alertify.klConfirm || alertify.dialog('klConfirm', function factory() {
            return {
                main: function (message, onok, oncancel, onclose) {
                    this.header = message;
                    this.onok = onok || function () { };
                    this.oncancel = oncancel || function () { };
                    this.onclose = onclose || function () { };
                },
                setup: function () {
                    return {
                        buttons: [{
                            text: alertify.defaults.glossary.ok,
                            className: alertify.defaults.theme.ok
                        }, {
                            text: alertify.defaults.glossary.cancel,
                            key: 27/*Esc*/,
                            className: alertify.defaults.theme.cancel,
                            invokeOnClose: true,
                        }],
                        focus: { element: 0 },
                        options: {
                            resizable: false,
                            maximizable: false,
                            movable: false,
                            transition: 'flipx',
                            closableByDimmer: false
                        }
                    };
                },
                prepare: function () {
                    this.setContent(this.message);
                    this.setHeader("<span>{0}</span>".formate(this.header || ""));
                    this.set({
                        onclose: this.onclose
                    });
                },
                build: function () {
                    $(this.elements.header).addClass("kl-confirm-info");
                    $(this.elements.header).css("border-bottom", "none");
                    $(this.elements.body).remove();
                },
                callback: function (closeEvent) {
                    switch (closeEvent.index) {
                        case 0:
                            this.onok();
                            break;
                        case 1:
                            this.oncancel();
                            break;
                    }
                }
            }
        });

        alertify.klAlert || alertify.dialog('klAlert', function factory() {
            return {
                main: function (message, header) {
                    this.message = message;
                    this.header = header;
                },
                setup: function () {
                    return {
                        buttons: [{
                            text: alertify.defaults.glossary.ok,
                            key: 27/*Esc*/,
                            className: alertify.defaults.theme.ok
                        }],
                        focus: { element: 0 },
                        options: {
                            resizable: false,
                            maximizable: false,
                            movable: false,
                            transition: 'flipx',
                            closableByDimmer: false
                        }
                    };
                },
                prepare: function () {
                    this.setContent(this.message);
                    this.setHeader("<span>{0}</span>".formate(this.header || "prompt"));
                }
            }
        });
    }
    else {
        throw "alertify is undefined";
    }
})()