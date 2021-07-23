$(() => {
    LoadChatData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadListChat", function () {
        LoadChatData();
    })
    LoadChatData();
    function LoadChatData() {
        var unnn_array = new Array();
        var array_id = new Array();
        var userloged = document.getElementById("unsender").value.toString();
        var nguoinhan2 = document.getElementById("nguoinhan2").value.toString();
        var hoten2 = document.getElementById("hoten2").value.toString();
        unnn_array[0] = nguoinhan2;
        array_id[0] = 0;
        var dem = 1;
        $.ajax({
            url: '/vi/Chat/GetChat',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    if (v.UserName1 == userloged) {
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
                            array_id[dem] = v.Id;
                            dem++;
                        }
                    }
                })
                for (var j = 0; j < dem; j++) {
                    callchat(array_id[j]);
                }
            },
            error: (error) => {
                console.log(error)
            }
        })
        function callchat(id) {
            var tinnhan = "#tinnhan" + id.toString();
            var unsender = document.getElementById("unsender").value.toString();
            var un2 = "username2" + id.toString();
            var user2 = document.getElementById(un2).value.toString();
            var li = '';
            $.ajax({
                url: '/vi/Chat/GetChat',
                method: 'GET',
                success: (result) => {
                    $.each(result, (k, v) => {
                        if (v.UserName1 == unsender && v.UserName2 == user2) {
                            li += `
                               <div class="chat__bubble chat__bubble--you">
                                            ${v.Message}
                               </div>
                                `
                        }
                        if (v.UserName1 == user2 && v.UserName2 == unsender) {
                            li += `
                               <div class="chat__bubble chat__bubble--me">
                                    ${v.Message}
                                </div>
                                `
                        }
                    })
                    $(tinnhan).html(li);
                },
                error: (error) => {
                    console.log(error)
                }
            });
        }
    }
})