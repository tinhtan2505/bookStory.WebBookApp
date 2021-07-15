$(() => {
    LoadCommentData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadComment", function () {
        LoadCommentData();
    })
    LoadCommentData();
    function LoadCommentData() {
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
//$(() => {
//    LoadProdData();
//    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
//    connection.start();
//    connection.on("LoadChat", function () {
//        LoadProdData();
//    })
//    LoadProdData();
//    function LoadProdData() {
//        var tr = '';
//        $.ajax({
//            url: '/vi/Chat/GetChat',
//            method: 'GET',
//            success: (result) => {
//                $.each(result, (k, v) => {
//                    tr += `<tr>
//                            <td>${v.UserIdSender}</td>
//                            <td>${v.UserIdReceiver}</td>
//                            <td>${v.Message}</td>
//                            <td>${v.DateComment}</td>
//                            <td>${v.UserName1}</td>
//                            <td>
//                            <a href='../Product/Edit?id=${v.Id}'>Edit</a>
//                            <a href='../Product/Details?id=${v.Id}'>Details</a>
//                            <a href='../Product/Delete?id=${v.Id}'>Delete</a>
//                            </td>
//                       </tr>`
//                })
//                $("#tableBodyChat").html(tr);
//            },
//            error: (error) => {
//                console.log(error)
//            }
//        })
//    }
//})
$(() => {
    LoadChatData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadChat", function () {
        LoadChatData();
    })
    LoadChatData();
    function LoadChatData() {
        var li = '';
        var unsender = document.getElementById("unsender").value.toString();
        $.ajax({
            url: '/vi/Chat/GetChat',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    if (v.UserName1 == unsender && v.UserName2 == "hoangha") {
                        li += `
                           <div class="chat__bubble chat__bubble--you">
                                        ${v.Message}
                           </div>
                            `
                    }
                    if (v.UserName1 == "hoangha" && v.UserName2 == unsender) {
                        li += `
                           <div class="chat__bubble chat__bubble--me">
                                ${v.Message}
                            </div>
                            `
                    }
                })
                $("#chatsender").html(li);
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})