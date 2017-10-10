var userObject = null;
var objectFb = { getLoginStatus: null };
var fbUserId = null;

function changeFBPet(value) {
    var newVal = '<fb:comments href="http://www.mapadopet.com.br/home/pet/' + value + '" num_posts="20" width="500"></fb:comments><div id="fb-root"></div>';
    $('#comments').html(newVal);
    $('.fb-like').html('<fb:like href="http://www.mapadopet.com.br/home/pet/' + value + '"  send="true" width="450" show_faces="true"></fb:like>');
    FB.XFBML.parse();
}

function atualizaIconUserFb(userid) {
    setTimeout('', 5000);
    $("img[src$='images/userM.png']").attr("src", "http://graph.facebook.com/" + userid + "/picture?type=normal");
    $("img[src$='images/userM.png']").addClass("img-circle");

}

function autorizaLoginFb() {
    FB.login(function (response) {
        if (response.authResponse) {


            //$.postJSON("api/facebook/user", response );



            console.log(response);

        } else {
            alert('O usuário cancelou o login ou não autorizou.');
        }
        objectFb.getLoginStatus = response.status;
    });
};


//Facebook
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/pt_BR/sdk.js#xfbml=1&version=v2.10&appId=1006198022832693";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

