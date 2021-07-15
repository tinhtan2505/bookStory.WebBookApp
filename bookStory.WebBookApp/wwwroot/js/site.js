$(() => {
    LoadProdData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadComment", function () {
        LoadProdData();
    })
    LoadProdData();
    function LoadProdData() {
        var tr = '';
        var idtran = document.getElementById("idtran").value;
        var username = document.getElementById("username").value.toString();
        $.ajax({
            url: '/vi/Book/GetComment',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    var currentdate = new Date(v.DateComment);
                    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                    if (idtran == v.IdTranslation) {
                        tr += `<span class="text-success">${v.FirstName} ${v.LastName}: </span>
                           <span class="text-dark ml-1">${v.Message}</span>
                           <span class="text-warning timecomment ml-2">${currentdate.toLocaleString('en-US', options)}</span>
                           `
                        if (v.UserName == username) {
                            var mce = "suacomment" + v.Id.toString();
                            var dce = "editcomment" + v.Id.toString();
                            tr +=
                                `
                                <a class="text-primary" data-toggle="modal" data-target="#${dce}" role="button" aria-expanded="false" aria-controls=${dce}>| Sửa</a >
                                <a class="text-danger" data-toggle="modal" data-target="#${mce}" role="button" aria-expanded="false" aria-controls=${mce}>| Xóa</a >

                                `
                        }
                        tr += `<hr class="mt-0 bg-info" />`
                    }
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})