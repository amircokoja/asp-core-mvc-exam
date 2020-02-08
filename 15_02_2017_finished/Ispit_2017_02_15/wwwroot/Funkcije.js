function funkcije() {
    $("a[ajax-poziv='da']").click(function (event) {
        event.preventDefault();
        var btn = $(this);
        var id = btn.attr("odrzanicasDetaljiId");
        $.get("/ajaxtest/Uredi?odrzaniCasDetaljiId=" + id, function (data, status) {
            $("#divID").html(data);
        });
    });


    $("a[ajax-poziv='odsutan']").click(function (event) {
        event.preventDefault();
        var btn = $(this);
        var id = btn.attr("odrzanicasDetaljiId");
        $.get("/ajaxtest/UcenikJeOdsutan?odrzaniCasDetaljiId=" + id, function (data, status) {
            $("#divID").html(data);
        });
    });



    $("a[ajax-poziv='prisutan']").click(function (event) {
        event.preventDefault();
        var btn = $(this);
        var id = btn.attr("odrzanicasDetaljiId");
        $.get("/ajaxtest/UcenikJePrisutan?odrzaniCasDetaljiId=" + id, function (data, status) {
            $("#divID").html(data);
        });
    });
};





$(document).ready(function () {
    funkcije()
});

$(document).ajaxComplete(function () {
    funkcije();
});