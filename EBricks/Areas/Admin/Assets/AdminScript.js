//Showing Edit Modal For Category
function editCategory(id, name) {
    $("#editModal").modal('show');
    $("#editId").val(id);
    $("#editCategoryName").val(name);
}
//Deleting Category
function deleteCategory(id) {
    var errorDiv = $("#divMessage");

    $.ajax({
        url: "/Admin/Category/DeleteCategory/" + id,
        method: "post",
        success: function () {

            errorDiv.empty().show();
            errorDiv.html(`<div class="alert alert-success"> Category Deleted Successfully</div>`)
            getCategories();
            $("#deleteModal").modal("hide");

        },
        error: function (xhr) {
        }
    });
}
//Showing Delete Modal For Ctaegory
function showDeleteModal(id, name) {
    $("#deleteModal").modal('show');
    $("#DelCategoryDiv").html(`<h4>${name}</h4>
                        <button type="button" id="deleteCategory" onclick="deleteCategory(${id})" class="btn btn-danger btn-block">Delete</button>
                                `);
}

//Getting All Categories
function getCategories() {
    var tableBody = $("#tableBody");
    tableBody.empty();
    $.ajax({
        url: "/Admin/Category/GetCategories",
        type: "get",
        dataType: "json",
        success: function (categories) {
            $.each(categories, function (index, category) {
                tableBody.append(`<tr><td>
                         ${category.CategoryName}</td ><td>
                        <a name="btnEdit"  href="#" onclick="editCategory(${category.Id},'${category.CategoryName}')" class="btn btn-primary">Edit</a>
                        <a name="btnDelete" href="#" onclick="showDeleteModal(${category.Id},'${category.CategoryName}')" class="btn btn-danger">Delete</a></td></tr>`
                );
            });

        },
        error: function (xhr) {

            alert(xhr.responseText);
        }
    });

}

$(document).ready(function () {


    //Adding Category
    $("#btnAddCategory").click(function () {
        debugger
        var formData = $("#categoryForm").serialize();
        $.ajax({
            url: "/Admin/Category/AddCategory",
            method: "post",
            data: formData,
            success: function () {
                $("#divMessage").empty().html(`<div class="alert alert-success">Category Added Successfully</div>`).show("fade");
                getCategories();
            },
            error: function (xhr) {
                $("#divMessage").empty().html(`<div class="alert alert-danger">Category Already Exists</div>`).show("fade");;
            }
        });
    });
    //Editing Category
    $("#btnEditCategory").click(function () {
        var id = $("#editId").val();
        var formData = $("#editCategoryForm").serialize();
        $.ajax({
            url: "/Admin/Category/EditCategory",
            method: "post",
            data: formData,
            success: function () {
                $("#divMessage").empty().html(`<div class="alert alert-success"> Category Updated Successfully</div>`).show("fade");
                getCategories();
                $("#editModal").modal("hide");
            },
            error: function (xhr) {
                $("#divMessage").empty().html(`<div class="alert alert-danger">Category Already Exists</div>`).show("fade");
                $("#editModal").modal("hide");
            }
        });
    });
});