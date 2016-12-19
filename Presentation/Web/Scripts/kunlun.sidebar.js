(function () {
    var sidebarCollapse = localStorage.sidebarCollapse,
        sidebarCollapseClassName = " sidebar-collapse",
        sidebarCollapseRegex = /\bsidebar-collapse\b/,
        body = document.body;

    if (sidebarCollapse == "true" && !sidebarCollapseRegex.test(body.className)) {
        body.className += sidebarCollapseClassName;
    }

    if (sidebarCollapse == "false" && sidebarCollapseRegex.test(body.className)) {
        body.className.replace(sidebarCollapseRegex, "");
    }
})();