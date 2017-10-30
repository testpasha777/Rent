(function () {
    $(function () {
        var AccountManager = {};
        AccountManager.AjaxLogin = function () {
            var form = $("#form_login");
            $.ajax({
                url: "/Account/Login",
                type: "POST",
                data: form.serialize(),
                //dataType: "json",
                //contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.rez == 1) {
                        location.reload();
                    }
                }
            });
        }

        AccountManager.AjaxRegister = function () {
            var form = $("#form_register");
            $.ajax({
                url: "/Account/Register",
                type: "POST",
                data: form.serialize(),
                //dataType: "json",
                //contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.res == 1) {
                        location.reload();
                    }
                }
            });
        }

        var aMgr = AccountManager;

        $("#btnLoginPopup").on("click", function () {
            aMgr.AjaxLogin();
        });

        $("#btnRegisterPopup").on("click", function () {
            aMgr.AjaxRegister();
        });
    });
})();