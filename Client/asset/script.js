function Start(onStart) {
    localStorage.setItem("Delay", 180);
    if (localStorage.getItem("Time") == 0) {
        localStorage.setItem("Time", localStorage.getItem("Delay"));
    }
    if (onStart != "first") {
        localStorage.setItem("Time", localStorage.getItem("Delay"));
    }
}

function startInterval() {
    isHiddenLogin();
    localStorage.getItem("Time") = 0;
    setInterval(function () {
        if (localStorage.getItem("Time") != 0) {
            localStorage.setItem("Time", localStorage.getItem("Time") - 1);

            $("#tokenDetail").html("");
            $("#tokenDetail").append("Log out second " + localStorage.getItem("Time"));
        }
        else {
            $("#tokenDetail").html("");
            $("#tokenDetail").append("You need login");
        }
    }, 1000);
}

function GetToken() {
    var userName = $("#FormName").val();
    var password = $("#FormPassword").val();
    $.ajax({
        url: "http://localhost:1253/token",
        type: "POST",
        crossDomain: true,
        data: {
            username: userName,
            password: password,
            grant_type: "password"
        },
        dataType: "json",
        success: function (data) {
            localStorage.setItem("Token", JSON.stringify(data));
            alert("Token saved.");

            isHiddenLogin();
            Start("token");
        },
        error: function (xhr, status, error) {
            alert("Hata : " + error);
        }
    });
}

function GetNewsEntity() {
    $(".tableList").html("");
    $(".tableList").append("<tr><th>Title</th><th>Text</th><th>CreateDate</th></tr>");
    $.get("http://localhost:1253/api/entity/list", "", function (data) {
        $.each(data, function (i, data) {
            $(".tableList").append("<tr><td>" + data.Title + "</td><td>" + data.Text + "</td><td>" + data.CreateDate + "</td><tr>");
        });
    });
    isHiddenGet();
}

function GetNewsMongo() {
    $(".tableList").html("");
    $(".tableList").append("<tr><th>Title</th><th>Text</th><th>CreateDate</th></tr>");
    $.get("http://localhost:1253/api/mongo/list", "", function (data) {
        $.each(data, function (i, data) {
            $(".tableList").append("<tr><td>" + data.Title + "</td><td>" + data.Text + "</td><td>" + data.CreateDate + "</td><tr>");
        });
    });
    isHiddenGet();
}

function GetNewsId() {
    $(".tableList").html("");
    $(".tableList").append("<tr><th>Title</th><th>Text</th><th>CreateDate</th></tr>");
    $.get("http://localhost:1253/api/entity/list?id=" + $("#GetId").val(), "", function (data) {
        $(".tableList").append("<tr><td>" + data.Title + "</td><td>" + data.Text + "</td><td>" + data.CreateDate + "</td><tr>");
    });
    isHiddenGet();
}

function PostNews() {
    $(".tableList").html("");

    var Title = $("#PostName").val();
    var Text = $("#PostText").val();

    var token = $.parseJSON(localStorage.getItem("Token")).access_token;
    $.ajax({
        url: "http://localhost:1253/api/news/Post?Title=" + Title + "&Text=" + Text,
        type: "Post",
        crossDomain: true,
        dataType: "json",
        headers: {
            "accept": "application/json",
            "content-type": "application/json",
            "authorization": "Bearer " + token
        },
        success: function (data) {
            $(".tableList").append(data);
        },
        error: function (xhr, status, error) {
            alert("Hata : " + error);
        }
    });
    isHiddenGet();
}

function PutNews() {
    $(".tableList").html("");

    var Title = $("#PutName").val();
    var Text = $("#PutText").val();
    var Id = $("#PutId").val();

    var token = $.parseJSON(localStorage.getItem("Token")).access_token;
    $.ajax({
        url: "http://localhost:1253/api/news/Put?Title=" + Title + "&Text=" + Text + "&Id=" + Id,
        type: "Put",
        crossDomain: true,
        dataType: "json",
        headers: {
            "accept": "application/json",
            "content-type": "application/json",
            "authorization": "Bearer " + token
        },
        success: function (data) {
            $(".tableList").append(data);
        },
        error: function (xhr, status, error) {
            alert("Hata : " + error);
        }
    });
    isHiddenGet();
}

function DeleteNews() {
    $(".tableList").html("");

    var Id = $("#DeleteId").val();

    var token = $.parseJSON(localStorage.getItem("Token")).access_token;
    $.ajax({
        url: "http://localhost:1253/api/news/Delete?Id=" + Id,
        type: "Delete",
        crossDomain: true,
        dataType: "json",
        headers: {
            "accept": "application/json",
            "content-type": "application/json",
            "authorization": "Bearer " + token
        },
        success: function (data) {
            $(".tableList").append(data);
        },
        error: function (xhr, status, error) {
            alert("Hata : " + error);
        }
    });
    isHiddenGet();
}


function isHiddenLogin() {
    $(".contentLogin").toggle();
    $(".contentGetId").hide();
    $(".contentPost").hide();
    $(".contentPut").hide();
    $(".contentDelete").hide();
}
function isHiddenGet() {
    $(".contentLogin").hide();
    $(".contentGetId").hide();
    $(".contentPost").hide();
    $(".contentPut").hide();
    $(".contentDelete").hide();
}
function isHiddenGetId() {
    $(".contentLogin").hide();
    $(".contentGetId").toggle();
    $(".contentPost").hide();
    $(".contentPut").hide();
    $(".contentDelete").hide();
}
function isHiddenPost() {
    $(".contentLogin").hide();
    $(".contentGetId").hide();
    $(".contentPost").toggle();
    $(".contentPut").hide();
    $(".contentDelete").hide();
}
function isHiddenPut() {
    $(".contentLogin").hide();
    $(".contentGetId").hide();
    $(".contentPost").hide();
    $(".contentPut").toggle();
    $(".contentDelete").hide();

}
function isHiddenDelete() {
    $(".contentLogin").hide();
    $(".contentGetId").hide();
    $(".contentPost").hide();
    $(".contentPut").hide();
    $(".contentDelete").toggle();
}
