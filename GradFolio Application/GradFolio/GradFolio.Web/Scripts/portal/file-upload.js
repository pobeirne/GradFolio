var nowTemp = new Date();
var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
var files;
var storedFiles = [];
var upc = 0;


$(function() {

    $(":file").attr("title", "  ");
    $("input[id^='fileToUpload']").change(function(e) {
        doReCreate(e);
    });

    window.selDiv = $("#selectedFiles");
});


function doReCreate(e) {
    upc = upc + 1;
    handleFileSelect(e);

    $("input[id^='fileToUpload']").hide();

    $("<input>").attr({
        type: "file",
        multiple: "multiple",
        id: "fileToUpload" + upc,
        "class": "fUpload",
        name: "fileUpload",
        style: "float: left",
        title: "  ",
        onchange: "doReCreate(event)"

    }).appendTo("#uploaders");
}


function handleFileSelect(e) {

    //selDiv.innerHTML = ""; storedFiles = []; 
    window.selDiv = document.querySelector("#selectedFiles");

    if (!e.target.files) return;

    //selDiv.innerHTML = "";
    files = e.target.files;

    for (var i = 0; i < files.length; i++) {
        //if (i == 0) { selDiv.innerHTML = ""; storedFiles = []; }
        var f = files[i];
        window.selDiv.innerHTML += "<div>" + f.name + "<a onclick='removeAtt(this)'> X </a></div>";
        storedFiles.push(f.name);
    }
    $("#@Html.IdFor(i => i.FilesToBeUploaded)").val(storedFiles);
}

function removeAtt(t) {
    var serEle = $(t).parent().text().slice(0, -3);
    var index = storedFiles.indexOf(serEle);
    if (index !== -1) {
        storedFiles.splice(index, 1);
    }
    $(t).parent().remove();

    $("#@Html.IdFor(i => i.FilesToBeUploaded)").val(storedFiles);

}