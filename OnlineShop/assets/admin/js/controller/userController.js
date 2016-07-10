// Thay doi trang thai user
var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khoá');
                    }
                }
            });
        });
    }
}
user.init();

// Thay doi trang thai bai viet
var content = {
    init: function () {
        content.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Content/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.html("<span class='text-success'><i class='glyphicon glyphicon-check'></i></span>");
                    }
                    else {
                        btn.html("<span class='text-danger'><i class='glyphicon glyphicon-unchecked'></i></span>");
                    }
                }
            });
        });
    }
}
content.init();

// Thay doi trang thai bai viet
var cat = {
    init: function () {
        cat.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Category/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.html("<span class='text-success'><i class='glyphicon glyphicon-check'></i></span>");
                    }
                    else {
                        btn.html("<span class='text-danger'><i class='glyphicon glyphicon-unchecked'></i></span>");
                    }
                }
            });
        });
    }
}
cat.init();
