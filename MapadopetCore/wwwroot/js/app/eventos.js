$(document).on('hide.bs.modal', '#mdShowPet', function () {
    window.history.pushState("", "", "/");
});

Object.observe(objectFb, function (changes) {

    if (objectFb.getLoginStatus === 'connected') {

        FB.api('/me', {
            fields: ['id',
                'birthday',
                'email',
                'first_name',
                'gender',
                'hometown',
                'languages',
                'last_name',
                'locale',
                'name',
                'relationship_status']
        }, function (response) {
            userObject = response
            console.log(response);
            $(".fbUser").html(response.name)
            $(".fbUserImg").attr("src", "http://graph.facebook.com/" + response.id + "/picture?type=normal");
            atualizaIconUserFb(response.id)
        });

        $(".fbEntrar").hide();
        $(".fbLogado").show();
    } else {

        $(".fbEntrar").show();
        $(".fbLogado").hide();
    }
    // comentar em produção
    //console.log("Changes: ", changes);
});
