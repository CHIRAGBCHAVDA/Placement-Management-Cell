var searchKeyword = "";
var selectedOption = 1;

$(document).ready(function () {

    $("#1").addClass('active');
    $('.company-cards-wrapper-div').on('click', '.page-item', function () {
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
});

function getFilter(pg) {
    var fromApplications = false;
    if (window.location.href == 'https://localhost:44357/Student/StudentApplication') {
        fromApplications = true;
    }
    $.ajax({
        type: "POST",
        url: "/Student/CompanyFilter",
        data: {
            "searchKeyword": searchKeyword,
            "pageNum": pg,
            "sortBy": selectedOption,
            "fromApplications" : fromApplications
        },
        success: function (result) {
            $(".company-cards-wrapper-div").html(result);
            console.log(result);
            $(`.page-item#${pg}`).addClass("active");
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function searchMissions() {
    searchKeyword = document.getElementById("search-query").value;
    console.log(searchKeyword)
    getFilter(1);
}

function companyDetails(companyId) {
    window.location.href = `/Student/CompanyDetail?companyId=` + companyId;
}

function CompanyApply(companyId) {
    $.ajax({
        type: "POST",
        url: "/Student/ApplyCompany",
        data: {
            companyId: companyId
        },
        success: function (result) {
            console.log(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}