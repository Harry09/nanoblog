$(document).ready(function () {

    var posts = $("#posts-container");

    var postTemplate = $("#entry-template");

    function updateList() {

        // clear childs
        posts.empty();


        $.get("api/entries", function (data, status) {
            if (status === "success") {

                for (var i = 0; i < data.length; i++) {
                    var template = Handlebars.compile(postTemplate.html());



                    var htmlCode = template({ id: data[i].id, author:"Harry", date:data[i].createTime, text: data[i].text });

                    posts.append(htmlCode);

                    /*var post = $(document.createElement("div"));
                    post.attr("class", "post");

                    // add header
                    var postHeader = $(document.createElement("div"));
                    postHeader.attr("class", "post-author");
                    {
                        // add author
                        var postAuthor = $(document.createElement("div"));
                        postAuthor.attr("class", "post-author");
                        postAuthor.text("Autor");

                        postHeader.append(postAuthor);

                        // add date
                        var postDate = $(document.createElement("div"));
                        postDate.attr("class", "post-date");
                        postDate.text(data[i].createTime);

                        postHeader.append(postDate);
                    }

                    post.append(postHeader);

                    // add text
                    var postText = $(document.createElement("div"));
                    postText.attr("class", "post-text");
                    postText.text(data[i].text);

                    post.append(postText);

                    posts.append(post);*/
                }
            }
        });


    }

    updateList();

    posts.on("click", ".post-delete-link", function () {
        var id = $(this).closest(".post").attr("post-id");

        if (id !== null) {
            $.ajax({
                url: "/api/entries/" + id,
                type: "DELETE",
                success: function () {
                    updateList();
                },
                error: function (err) {
                    console.error(err);
                }
            });
        }
    });

    $("#submit-post").submit(function (event) {
        event.preventDefault();

        var $form = $(this);

        $.post("api/entries",
            {
                text: $("#input-text-form").val()
            },
            function (data, status) {
                updateList();
            }
        );

        $("#input-text-form").val("");
    });

});
