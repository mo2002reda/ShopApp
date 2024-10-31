console.log("Products.js loaded");
var dtble;
$(document).ready(function () {
    loaddata();
});
function loaddata() {
    dtble = $("#mytable").DataTable(
        {
            ajax: {
                /* url: '/Admin/Product/GetAll',*/
                url: '@Url.Action("GetAll", "Product", new { area = "Admin" })',
                dataSrc: 'data'
            },
            columns:
                [
                    { "data": "name" },
                    { "data": "description" },
                    { "data": "img" },
                    { "data": "price" },
                    { "data": "Category.name" },
                    {
                        "data": "id",
                        "render": function (data) {
                            return
                            `
                      <a href="Admin/Product/Edit/${data}" class="btn btn-success">Edit</a> ||
                      <a href="Admin/Product/Delete/${data}" class="btn btn-danger">Delete</a> 
                                    `;


                        }
                    }
                ]
        })
};
