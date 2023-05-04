var searchKeyword = "";
var selectedOption = 1;

$(document).ready(function () {
    var imgOfCompany = "";
    var compUpload = $("#company-img");
    var compImg = $("#compImg");

    compUpload.on('change', function () {
        const file = this.files[0];
        const reader = new FileReader();

        reader.onload = function () {
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
        var companyId = $("#company-id-tpo-addedit").val();

        console.log(BranchId)
        console.log(MinCgpa)
        console.log(MaxBacklog)

        $.ajax({
            type: 'POST',
            url: '/TPO/NewCompany',
            data: {
                "CompanyId": companyId,
                "avatar": avatar,
                "name": name,
                "technology": technology,
                "title": title,
                "BID": parseInt(BranchId),
                "package": package,
                "briefdesc": briefdesc,
                "longdesc": longdesc,
                "maxBacklog": MaxBacklog,
                "minCgpa": MinCgpa,  
                "fromdate": fromdate,
                "todate": todate,
                "vacancy": vacancy,
                "deadline": deadline,
                "address": address,
                "traininginfo": traininginfo,
                "benefits": benefits,
                "driveLink": driveLink,
                "city": city,
            },
            beforeSend: function () {
                $('#loader').removeClass('d-none');     
            },
            success: function (result) {
                console.log(result);
                if (result.success) {
                    setTimeout(function () {
                        $('#loader').addClass('d-none');
                    }, 2000);
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

    });

    $("#1").addClass('active');
    $('#TPOCompDashboard-wrapper').on('click', '.page-item', function () {
        $('.pagination .page-item').removeClass('active');
        $(this).addClass('active');
        getFilter($(this).attr('id'));
    });

    $("#search-query").keyup(function () {
        searchMissions();
    });

    $('#sortingCompany').change(function () {
        selectedOption = $(this).val();
        console.log("The value of sorting is : " + selectedOption);// Get the selected option value
        getFilter(); // Call the sortMissions function with the selected option
    });

    $('#sortingCompany').change(function () {
        selectedOption = $(this).val();
        console.log("The value of sorting is : " + selectedOption);// Get the selected option value
        getFilter(); // Call the sortMissions function with the selected option
    });


    $(document).on('click', ".tpo-comp-xlsheet", function () {
        //var companyId = $(this).data("companyid");
        //$.ajax({
        //    type: "post",
        //    url: "/TPO/DownloadStudentDetails",
        //    data: {
        //        companyId:companyId
        //    },
        //    success: function (result) {
        //        var toast = new bootstrap.Toast(document.querySelector('.toast-container'));
        //        toast.show({
        //            autohide: true,
        //            delay: 5000,
        //            body: 'Data Downloaded successfully....!!',
        //            header: 'Bootstrap Toast'
        //        });
        //        console.log(typeof(result));
        //    },
        //    error: function (xhr, status, error) {
        //        toastr.error("Could not download the data....!");
        //        console.log(error);
        //    }
        //});

        var companyId = $(this).data("companyid");
        toastr.success("Data will be downloaded shortly...!!")
        setTimeout(function () {
            window.location.href = "/TPO/DownloadStudentDetails?companyId=" + companyId;
        }, 2000);
    });

    $(document).on('click', ".tpo-comp-mail", function () {
        var companyId = $(this).data("companyid");

        $.ajax({
            type: "get",
            url: "/TPO/getChartData",
            data: {
                companyId:companyId
            },
            success: function (result) {
                console.log(result);
                var IT = result.it;
                var Computer = result.computer;
                var EC = result.ec;
                var Mech = result.mech;
                var Civil = result.civil;
                var Prod = result.prod;

                if (window.myChart) {
                    window.myChart.destroy();
                }
                
                

                var ctx = document.getElementById('chart_canvas').getContext('2d');

                window.myChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: ['IT', 'Computer', 'EC','Mechanical','Civil','Production'],
                        datasets: [{
                            label: 'Interested Students',
                            data: [IT,Computer,EC,Mech,Civil,Prod],
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.5)',
                                'rgba(54, 162, 235, 0.5)',
                                'rgba(255, 206, 86, 0.5)',
                                'rgba(75, 192, 192, 0.5)',
                                'rgba(153, 102, 255, 0.5)',
                                'rgba(255, 159, 64, 0.5)'
                            ],
                            borderColor: [
                                'rgba(255, 99, 132, 1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                                'rgba(75, 192, 192, 1)',
                                'rgba(153, 102, 255, 1)',
                                'rgba(255, 159, 64, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: {
                            animateRotate: true,
                            animateScale: true
                        }
                    }
                });


                if (IT + Computer + EC + Mech + Civil + Prod == 0) {
                    $('.chart-message').removeClass("d-none");
                    return;
                } else {
                    $('.chart-message').addClass("d-none");
                }
                // Open the modal
                $('#chartModal').modal('show');
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });

    });

    //$(document).on('click', ".tpo-comp-edit", function () {
    //    var companyId = $(this).attr('data-companyid');
    //    $.ajax({
    //        type: "POST",
    //        url: "/TPO/EditCompany",
    //        data: { companyId: companyId },
    //        success: function (result) {
    //            debugger;
    //            $("body").html(result); 
    //        },
    //        error: function (error) {
    //            console.log(error);
    //        }

    //    });
    //});

});


function searchMissions() {
    searchKeyword = document.getElementById("search-query").value;
    console.log(searchKeyword)
    getFilter(1);
}


function getFilter(pg) {
    $.ajax({
        type: "POST",
        url: "/TPO/TPOCompCardFilter",
        data: {
            "searchKeyword": searchKeyword,
            "pageNum": pg,
            "sortBy": selectedOption
        },
        success: function (result) {
            $("#TPOCompDashboard-wrapper").html(result);
            $(`.page-item#${pg}`).addClass("active");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}