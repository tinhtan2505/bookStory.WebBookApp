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

$(() => {
    LoadChatData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadChat", function () {
        LoadChatData();
    })
    LoadChatData();
    function LoadChatData() {
        var list = '';
        var unnn_array = new Array();
        var dem = 0;
        var un2first = '';
        $.ajax({
            url: '/vi/Chat/GetChat',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    if (v.UserName1 == unsender) {
                        var username2 = v.UserName2.toString();
                        var kt = false;
                        for (var i = 0; i < dem; i++) {
                            if (username2 == unnn_array[i]) {
                                kt = true;
                                break;
                            }
                        }
                        if (kt == false) {
                            unnn_array[dem] = username2;
                            dem++;
                            if (dem == 1) {
                                list += `
                                <li  class="messaging-member messaging-member--active">
                               <div class="messaging-member__wrapper">
                                    <div class="messaging-member__avatar">
                                        <img src="https://randomuser.me/api/portraits/thumb/women/17.jpg" alt="Courtney Simmons" loading="lazy">
                                        <div class="user-status"></div>
                                    </div>

                                    <span class="messaging-member__name">${v.FirstName2} ${v.LastName2}</span>
                                    <span class="messaging-member__message">${v.Message}</span>
                                </div>
                                </li>
                                `
                            } else {
                                list += `
                                <li  class="messaging-member">
                               <div class="messaging-member__wrapper">
                                    <div class="messaging-member__avatar">
                                        <img src="https://randomuser.me/api/portraits/thumb/women/17.jpg" alt="Courtney Simmons" loading="lazy">
                                        <div class="user-status"></div>
                                    </div>

                                    <span class="messaging-member__name">${v.FirstName2} ${v.LastName2}</span>
                                    <span class="messaging-member__message">${dem} ${v.Message}</span>
                                </div>
                                </li>
                                `
                            }
                        }
                    }
                })
                un2first = unnn_array[0];
                $("#listReceiver").html(list);
            },
            error: (error) => {
                console.log(error)
            }
        })
        var li = '';
        var unsender = document.getElementById("unsender").value.toString();
        $.ajax({
            url: '/vi/Chat/GetChat',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    if (v.UserName1 == unsender && v.UserName2 == un2first) {
                        li += `
                           <div class="chat__bubble chat__bubble--you">
                                        ${v.Message}
                           </div>
                            `
                    }
                    if (v.UserName1 == un2first && v.UserName2 == unsender) {
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