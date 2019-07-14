var check = true;
$(document).keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode = '13') {

        if ($("#User_Name").val() = "") {
            $("#Notificationusername").text("Vui lòng nhập tên đăng nhập");
            check = check & false;
        }
        if ($("#Password").val() = "") {
            $("#Notificationpassword").text("Vui lòng nhập mật khẩu");
            check = check & false;
        }

        if (check)
            $("#Action").submit();
        check = true;
        // alert('Bạn vừa nhấn phím "enter" trên trang web');

    }
});
$('#btnlogin').click(function () {
    if ($("#User_Name").val() = "") {
        $("#Notificationusername").text("Vui lòng nhập tên đăng nhập");
        check = check & false;
    }

    if ($("#Password").val() = "") {
        $("#Notificationpassword").text("Vui lòng nhập mật khẩu");
        check = check & false;
    }

    if (check)
        $("#Action").submit();

    check = true;
})

