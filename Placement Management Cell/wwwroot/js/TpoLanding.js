$(document).ready(function () {
    var imgOfCompany = "";
    var compUpload = $("#company-img");
    var compImg = $("#compImg");

    compUpload.on('change', function () {
        const file = this.files[0];
        const reader = new FileReader();

        reader.onload =  function () {
            compImg.attr('src', reader.result);
            imgOfCompany = reader.result;
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
        var avatar = imgOfCompany;
        var name = $("#company-name").val();
        var technology = $("#company-technology").val();
        var title = $("#company-title").val();
        var BranchId = $("#Company-branch option:selected").val();
        var MaxBacklog = $("#company-maxbacklog").val();
        var MinCgpa = $("#company-mincgpa").val();
        var package = $("#company-package").val();
        var briefdesc = $("#company-brief-desc").val();
        var longdesc = CKEDITOR.instances['ck-editor-company-longdesc'].getData();
        var fromdate = $("#company-from-date").val();
        var todate = $("#company-to-date").val();
        var vacancy = $("#no-of-vacancy").val();
        var deadline = $("#deadline").val();
        var address = $("#company-address").val();
        var traininginfo = $("#company-traininginfo").val();
        var benefits = $("#company-benefits").val();
        var driveLink = $("#company-drive").val();
        var city = $("#company-city").val();

        console.log(BranchId)
        console.log(MinCgpa)
        console.log(MaxBacklog)

        $.ajax({
            type: 'POST',
            url: '/TPO/NewCompany',
            data: {
                "avatar" : avatar,
                "name" : name,
                "technology" : technology,
                "title": title,
                "BID":parseInt(BranchId),
                "package" : package,
                "briefdesc" : briefdesc,
                "longdesc": longdesc,
                "maxBacklog": MaxBacklog,
                "minCgpa":MinCgpa,
                "fromdate" : fromdate,
                "todate" : todate,
                "vacancy" : vacancy,
                "deadline" : deadline,
                "address" : address,
                "traininginfo" : traininginfo,
                "benefits" : benefits,
                "driveLink" : driveLink,
                "city" : city
            },
            success: function (result) {
                console.log(result);
                if (result.success) {
                    toastr.success(result.message);
                }
                else {

                toastr.error(result.message);
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

        console.log(
            avatar,
            name ,
            technology ,
            title ,
            package, 
            briefdesc, 
            longdesc ,
            fromdate ,
            todate ,
            vacancy ,
            deadline ,
            address ,
            traininginfo ,
            benefits ,
            driveLink ,
            city 

        )
    })

})