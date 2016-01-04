//Turn off caching
$.ajaxSetup({ cache: false });

//URL Parameters


//Sort Parameters
var sort_1 = document.getElementById("sortbydate");
var sort_2 = document.getElementById("sortbyaward");
var sort_3 = document.getElementById("sortbyissuer");


//################ Content Load/Reload/Sort Functions

//Initial Page Load
$(function() {
    loadContent(window.loadContent_url);
});


function reloadBtnClick() {
    loadContent(window.loadContent_url);
}

function generatePortfolioBtnClick() {
    loadViewModel(window.addContent_url);
}

//Sort
function itemMenuBtnClick(action) {

    //Remove active css
    removeClass("active", sort_1);
    removeClass("active", sort_2);
    removeClass("active", sort_3);

    switch (action) {
    case "add":
        //alert("view");
        loadViewModel(window.addContent_url);
        break;
    case "reload":
        //alert(action);
        loadContent(window.loadContent_url);
        break;
    case "sortby1":
        //alert(action);
        addClass("active", sort_1);
        loadContent(window.loadContent_url + "?sort=" + action);
        break;
    case "sortby2":
        //alert(action);
        addClass("active", sort_2);
        loadContent(window.loadContent_url + "?sort=" + action);
        break;
    case "sortby3":
        //alert(action);
        addClass("active", sort_3);
        loadContent(window.loadContent_url + "?sort=" + action);
        break;
    case "":
        alert("empty");
        break;
    }
}

//Loading Function
function loadContent(url) {
    var content = $("#content");
    content.fadeOut(400, function() {
        $("#container").append("<div id=\"loading\">Loading..." +
            " <img src=\"../Content/Images/loading1.gif\" /></div>");
        var loading = $("#loading");
        loading.fadeIn(400, function() {
            content.load(url, function() {
                loading.fadeOut(400, function() {
                    $(this).remove();
                    content.fadeIn(400);
                });
            });
        });
    });
}

//################ Content Form Functions
//"?itemId=" + id
function itemBtnClick(action, param) {
    switch (action) {
    case "view":
        //alert("view");
        loadViewModel(window.viewContent_url + param);
        break;
    case "edit":
        //alert("edit");
        loadViewModel(window.editContent_url + param);
        break;
    case "remove":
        //alert("remove");
        loadViewModel(window.removeContent_url + param);
        break;
    case "addCV":
        //alert("remove");
        var value = $("input[name=optradio]:checked").val();
        alert(value);
        loadViewModel(window.addContent_url + "?type="+value);
        break;
    case "":
        //alert("empty");
        break;
    }
}



$(document).on("click", "#btn-content-add-submit", function() {
    var self = $(this);
    $.ajax({
        url: window.addContent_url,
        type: "POST",
        data: self.closest("form").serialize(),
        success: function(data) {
            if (data.success === true) {
                $("#popupModal").modal("hide");
                loadContent(window.loadContent_url);
                $.notify("Successfully Added!!", "success");
                return false;
            } else {
                $("#popupcontainer").html(data);
            }
            return false;
        }
    });
});

$(document).on("click", "#btn-content-edit-submit", function() {
    var self = $(this);
    $.ajax({
        url: window.editContent_url,
        type: "POST",
        data: self.closest("form").serialize(),
        success: function(data) {
            if (data.success === true) {
                $("#popupModal").modal("hide");
                loadContent(window.loadContent_url);
                $.notify("Successfully Edited!!", "success");
                return false;
            } else {
                $("#popupcontainer").html(data);
            }
            return false;
        }
    });
});

$(document).on("click", "#btn-content-remove-submit", function() {
    var self = $(this);
    $.ajax({
        url: window.removeContent_url,
        type: "POST",
        data: self.closest("form").serialize(),
        success: function(data) {
            if (data.success === true) {
                $("#popupModal").modal("hide");
                loadContent(window.loadContent_url);
                $.notify("Successfully Removed!!", "success");
                return false;
            } else {
                $("#popupcontainer").html(data);
            }
            return false;
        }
    });
});


//################ Model Function

function loadViewModel(url) {
    $("#popupcontainer").html("");
    $("#popupcontainer").load(url);
    $("#popupModal").modal("show");
}

//################ Helper Functions

function addClass(classname, element) {
    var cn = element.className;
    //test for existance
    if (cn.indexOf(classname) !== -1) {
        return;
    }
    //add a space if the element already has class
    if (cn !== "") {
        classname = " " + classname;
    }
    element.className = cn + classname;
}

function removeClass(classname, element) {
    var cn = element.className;
    var rxp = new RegExp("\\s?\\b" + classname + "\\b", "g");
    cn = cn.replace(rxp, "");
    element.className = cn;
}

function hasClass(classname, element) {
    return (" " + element.className + " ").indexOf(" " + classname + " ") > -1;
}


function getSelected(parameters) {
    
}