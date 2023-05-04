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

    var imgOfStudent = "";

    $(document).on('change', "#user-image", function () {
        const file = this.files[0];
        readAsDataURL(file)
            .then(function (imgSrc) {
                $("#student-image-tag").attr("src", imgSrc);
                imgOfStudent = imgSrc;
                console.log(imgSrc);
            });
    });

    $(document).on('submit', "#student-profile-edit-form", function () {
        var form = document.getElementById("student-profile-edit-form");
        var formData = new FormData(form);
        formData.set('avatar', imgOfStudent);

        $.ajax({
            type: "POST",
            url: "/Student/EditStudentProfile",
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                window.location.reload();
            },
            error: function (xhr, status, error) {
                console.log("IN error function");
                console.log(error);
                return;
            }
        });
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
            $('body').html(result);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function StudentProfileEdit() {
    $.ajax({
        type: "GET",
        url: "/Student/GetStudentProfile",
        success: function (result) {
            console.log(result);
            $('#Student-Edit-Modal-Wrapper').html(result);
            $("#hiddenbtn-for-student-editmodal").click();
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
};


function readAsDataURL(file) {
    if (!(file instanceof Blob)) {
        return Promise.resolve(null);
    }
    return new Promise(function (resolve, reject) {
        var reader = new FileReader();
        reader.onload = function (event) {
            resolve(event.target.result);
        };
        reader.onerror = function (event) {
            reject(event.target.error);
        };
        reader.readAsDataURL(file);
    });
}
