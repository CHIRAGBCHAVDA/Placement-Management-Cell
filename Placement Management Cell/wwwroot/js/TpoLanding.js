$(document).ready(function () {
    var compUpload = $("#company-img");
    var compImg = $("#compImg");

    compUpload.on('change', function () {
        const file = this.files[0];
        const reader = new FileReader();

        reader.onload =  function () {
            compImg.attr('src', reader.result);
            console.log(reader.result);
        };
        if (file) {
            reader.readAsDataURL(file);
        }
    });
    compImg.on('click', function (e) {
        e.preventDefault();
        compUpload.click();
    });

    $("#newCompanyForm").submit(function (e) {
        e.preventDefault();
        var name = $("#");
        var technology = $("#");
        var title = $("#");
        var package = $("#");
        var briefdesc = $("#");
        var longdesc = $("#");
        var fromdate = $("#");
        var todate = $("#");
        var vacancy = $("#");
        var deadline = $("#");
        var address = $("#");
        var traininginfo = $("#");
        var benefits = $("#");
        var driveLink = $("#");
        var city = $("#");

    })

})