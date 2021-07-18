﻿$(() => {
    LoadComData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadComment", function () {
        LoadComData();
    })
    LoadComData();
    function LoadComData() {
        var tr = '';
        $.ajax({
            url: '/Comment/GetComment',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                            <td>${v.UserId}</td>
                            <td>${v.IdTranslation}</td>
                            <td>${v.Message}</td>
                            <td>${v.DateComment}</td>
                            <td>
                            <a href='../Product/Edit?id=${v.Id}'>Edit</a>
                            <a href='../Product/Details?id=${v.Id}'>Details</a>
                            <a href='../Product/Delete?id=${v.Id}'>Delete</a>
                            </td>
                       </tr>`
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
    LoadChaData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatsignlr").build();
    connection.start();
    connection.on("LoadChat", function () {
        LoadChaData();
    })
    LoadChaData();
    function LoadChaData() {
        var tr = '';
        $.ajax({
            url: '/Chat/GetChat',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                            <td>${v.UserIdSender}</td>
                            <td>${v.UserIdReceiver}</td>
                            <td>${v.Message}</td>
                            <td>${v.DateComment}</td>
                            <td>
                            <a href='../Product/Edit?id=${v.Id}'>Edit</a>
                            <a href='../Product/Details?id=${v.Id}'>Details</a>
                            <a href='../Product/Delete?id=${v.Id}'>Delete</a>
                            </td>
                       </tr>`
                })
                $("#tableBodyChat").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})