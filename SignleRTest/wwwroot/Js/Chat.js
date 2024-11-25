document.addEventListener("DOMContentLoaded", function ()
{
    var USERNAME = prompt("Please Enter Your name");
    var message = document.getElementById("messageInp");
    var GrupName = document.getElementById("groupNameInp");
    var GroupMessage = document.getElementById("messagetoGroup");

    message.focus();
    //Connection to the Hub
   var Proxyconnection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

    Proxyconnection.start().then(function () {
        document.getElementById("sendmessagebtn").addEventListener("click", function (e) {
            e.preventDefault();
            Proxyconnection.invoke("Send", USERNAME, message.value);

        });
        document.getElementById("JoinGroupbtn").addEventListener("click", function (e) {
            e.preventDefault();
            Proxyconnection.invoke("JoinGroup", GrupName.value, USERNAME);

        })
        document.getElementById("sendmessageGroupbtn").addEventListener("click", function (e) {
            e.preventDefault();
            Proxyconnection.invoke("SendToGroup", GrupName.value, GroupMessage.value, USERNAME);

        })
    }).catch(function (error) {
        console.log(error.toString());
    });
    Proxyconnection.on("Resiviemessage", function (username, message)
    {
        var listelement = document.createElement("li")
        listelement.innerHTML = `<strong>"${username}"</strong>:${message}`;
        document.getElementById("messagesList").appendChild(listelement);
    }
    );
    Proxyconnection.on("JoinGroup", function (Username, groupName) {
        var listelement = document.createElement("li")
        listelement.innerHTML = `<strong>"${Username}"Has join to${groupName}</strong>`;
        document.getElementById("messagesGroupList").appendChild(listelement)
    });
    Proxyconnection.on("ResiviemessageFromGroup", function (message, sender) {
      var listelement = document.createElement("li")
        listelement.innerHTML = `<strong>"${sender}":</strong>${message}`;
        document.getElementById("messagesGroupList").appendChild(listelement)
    });

});