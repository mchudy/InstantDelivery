(function () {
    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    $("#sidebar-toggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
    });
})();