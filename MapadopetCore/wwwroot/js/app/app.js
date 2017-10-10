
function menuLostPet(e) {
    centerGMap(e.latlng);
    var pos = (e.latlng).toString();
    pos = pos.replace('(', '');
    pos = pos.replace(')', '');
    pos = pos.replace(' ', '');
    pos = pos.replace('LatLng', '');
    $('#gmapPetPosition').val(pos);
    perdiMeuPet();
}

function menuPetAbandonado(e) {

    centerGMap(e.latlng);

    var pos = (e.latlng).toString();
    pos = pos.replace('(', '');
    pos = pos.replace(')', '');
    pos = pos.replace(' ', '');
    pos = pos.replace('LatLng', '');
    $('#gmapPetPosition').val(pos);
    petAbandonado();
}

function menuPetAdocao(e) {
    centerGMap(e.latlng);
    var pos = (e.latlng).toString();
    pos = pos.replace('(', '');
    pos = pos.replace(')', '');
    pos = pos.replace(' ', '');
    pos = pos.replace('LatLng', '');
    $('#gmapPetPosition').val(pos);
    petAdocao();
}

function menuCentralizar(e) {
    map.panTo(e.latlng);
}

function loadPet(id) {
    $.get(varHost + "/api/pet/" + id, function (data) {
        var tmpl = $.templates("#tmplmdShowPet");
        var html = tmpl.render(data);
        $("#mdShowPetContent").html(html);
        $('#mdShowPet').modal();
        //leva até o ponto
        map.flyTo(new L.LatLng(data.localizacao.split(",")[0], data.localizacao.split(",")[1], 13));
        window.history.pushState("", "", '/home/pet/' + id);
        changeFBPet(id);
    });
}

function loadGooglePlace(id) {
    $.get(varHost + "/api/google/get/" + id, function (data) {
        var tmpl = $.templates("#tmplmdShowGooglePlace");
        var html = tmpl.render(data);
        $("#mdShowGooglePlaceContent").html(html);
        $('#mdShowGooglePlace').modal()
    });
}

function perdiMeuPet() {
    if (objectFb.getLoginStatus === 'connected') {
        $("#hiddenuseremail").val(userObject.email);
        $("#hiddenusername").val(userObject.name);
        $("#hiddenuserid").val(userObject.id);
        $("#hiddenTipoPet").val(0);
        configuraModal("inserir");
        $('#mdInsertPet').modal();
    } else if (objectFb.getLoginStatus === 'not_authorized') {
        alert('Você ainda não Autorizou o nosso Sistema !!');
    } else {
        alert('Não Conectado ao Facebook');
    }
}

function petAbandonado() {
    if (objectFb.getLoginStatus === 'connected') {
        $("#hiddenuseremail").val(userObject.email);
        $("#hiddenusername").val(userObject.name);
        $("#hiddenuserid").val(userObject.id);
        $("#hiddenTipoPet").val(2);
        configuraModal("abandonado");
        $('#mdInsertPet').modal();
    } else if (objectFb.getLoginStatus === 'not_authorized') {
        alert('Você ainda não Autorizou o nosso Sistema !!');
    } else {
        alert('Não Conectado ao Facebook');
    }
}

function petAdocao() {
    if (objectFb.getLoginStatus === 'connected') {
        $("#hiddenuseremail").val(userObject.email);
        $("#hiddenusername").val(userObject.name);
        $("#hiddenuserid").val(userObject.id.id);
        $("#hiddenTipoPet").val(1);
        configuraModal("adocao");
        $('#mdInsertPet').modal();
    } else if (objectFb.getLoginStatus === 'not_authorized') {
        alert('Você ainda não Autorizou o nosso Sistema !!');
    } else {
        alert('Não Conectado ao Facebook');
    }
}

function salvaLostPet() {
    $.postJSON(varHost + "/api/pet", JSON.stringify($("#frmLostPet").serializeObject()));
    returnSalvaLostPet();
}

function salvaAbandonado() {
    $.postJSON(varHost + "/api/pet", JSON.stringify($("#frmLostPet").serializeObject()));
    returnSalvaLostPet();
}

function salvaAdocao() {
    $.postJSON(varHost + "/api/pet", JSON.stringify($("#frmLostPet").serializeObject()));
    returnSalvaLostPet();
}

function returnSalvaLostPet() {
    $('#mdInsertPet').modal('hide');
    alert('Seu pet vai ser adicionado ao mapa em instantes.');
    atualizaMapa();
    //Limpa o formulário de inserir pet
    document.getElementById("frmLostPet").reset();
    $("#upload-file-info").val("");
    $("#imgLostPetValue").val("");
    $("#upload-file-info").val("");
    $("#imgLostPet").hide();
    $("#imgLostPet").find("img").attr("src", "");
}

function atualizaFotoPet() {
    var form = new FormData();
    form.append("file", $("#mdLostpetFoto")[0].files[0]);
    jQuery.ajax('/api/imagem/postfotopet', {
        processData: false,
        contentType: false,
        method: "POST",
        data: form
    }).complete(function (data) {
        //seta valor no hidden
        $('#imgLostPetValue').val(data.responseText);
        $("#imgLostPet").show();
        $("#imgLostPet").find("img").attr("src", data.responseText);
    });

};

function configuraModal(nome) {

    var jsonConfig;

    switch (nome) {
        case "inserir":
            jsonConfig = mdInserirPerdiMeuPet;

            $("#mdInsertPetSalvar").unbind("click");
            $("#mdInsertPetSalvar").click(salvaLostPet);

            break;
        case "abandonado":
            jsonConfig = mdInserirAbandonado;

            $("#mdInsertPetSalvar").unbind("click");
            $("#mdInsertPetSalvar").click(salvaAbandonado);
            break;
        case "adocao":
            jsonConfig = mdInserirAdocao;

            $("#mdInsertPetSalvar").unbind("click");
            $("#mdInsertPetSalvar").click(salvaAdocao);
            break;

    }



    jQuery.each(jsonConfig.labels, function () {
        $("#" + this.n).text(this.v);
    });

    jQuery.each(jsonConfig.elementos, function () {
        if (!this.v) {
            $("#" + this.n).css("display", "none");
        }

    });


}

function btnEntrar() {
    if ($("#naoMostrarInicio").prop('checked')) {
        setCookie('nmi', 1, 10);
    }
}

