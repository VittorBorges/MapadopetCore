let posUser;
let posPin = new L.LatLng(-10.185, -48.328);

var markers = new L.MarkerClusterGroup();
var markersList = [];
var Gmap;
var Gmarkers = [];


function showCoordinates(e) {
    alert(e.latlng);
}

function centerGMap(e) {
    Gmap.setCenter(e);
    Gmap.setZoom(17);
    clearGMarkers();
    addGMarker(e);
}

function zoomIn(e) {
    map.zoomIn();
}

function zoomOut(e) {
    map.zoomOut();
}

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        alert('Você precisa autorizar o navegador para ser redirecionado no mapa automaticamente.');
    }
}

function atualizaMapa() {
    markers.clearLayers();
    markers = new L.MarkerClusterGroup();
    markersList = [];
    populate();
    map.addLayer(markers);
}

function posicionarMapa(lat, Lng) {
    map.flyTo(new L.LatLng(lat, Lng), 14);
}

function showPosition(position) {
    var marker = L.marker(new L.LatLng(position.coords.latitude, position.coords.longitude), { title: 'Você', icon: userIcon });
    markers.addLayer(marker);
    //map.flyTo(new L.LatLng(position.coords.latitude, position.coords.longitude), 13);
}

function populate() {
    $.get(varHost + "/api/marca", function (data) {
        let marcaPet;
        marcaPet = data;
        console.log(marcaPet);
        for (var i = 0; i < marcaPet.length; i++) {
            console.log
            let markerIcon = 0;
            switch (marcaPet[i].tipo) {
                case 0:
                    markerIcon = petLostIcon;
                    break;
                case 1:
                    markerIcon = petAdocaoIcon;
                    break;
                case 2:
                    markerIcon = petAbandonadoIcon;
                    break;
            }
            let marker = L.marker(new L.LatLng(marcaPet[i].cord[0], marcaPet[i].cord[1]), { title: marcaPet[i].nome, icon: markerIcon });
            marker.attribution = marcaPet[i].id;
            marker.on('click', function (a) {
                loadPet(this.attribution);
            });
            markers.addLayer(marker);
        }
    });


    $.get(varHost + "/api/google/GetPlaces", { "lat": map.getCenter().lat, "lng": map.getCenter().lng }).done(function (data) {
        let marcaGooglePlace;
        marcaGooglePlace = data;
        for (var i = 0; i < marcaGooglePlace.length; i++) {
            var marker = L.marker(new L.LatLng(marcaGooglePlace[i].cord[0], marcaGooglePlace[i].cord[1]), { title: marcaGooglePlace[i].name, icon: petShopIcon });
            let place_id = marcaGooglePlace[i].place_id;
            marker.on('click', function (a) {
                loadGooglePlace(place_id);
            });
            markers.addLayer(marker);
        }
    });

    return false;
}

function initializeGMap() {

    var GinitialPoint = new google.maps.LatLng(-10.185, -48.328);
    var GmapOptions = {
        zoom: 18,
        center: GinitialPoint,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    Gmap = new google.maps.Map(document.getElementById('Gmap-canvas'), GmapOptions);

    google.maps.event.addListener(Gmap, 'click', function (event) {
        addGMarker(event.latLng);
        saveData(Gmap, event);
    });

    addGMarker(GinitialPoint);

    google.maps.event.addDomListener(window, "resize", function () {
        var center = Gmap.getCenter();
        google.maps.event.trigger(map, "resize");
        Gmap.setCenter(center);
    });

}

function saveData(Gmap, event) {
    var zoomLevel = Gmap.getZoom();
    var pos = (event.latLng).toString();
    pos = pos.replace('(', '');
    pos = pos.replace(')', '');
    pos = pos.replace(' ', '');
    $('#gmapPetPosition').val(pos);
}

function addGMarker(location) {
    clearGMarkers();
    var Gmarker = new google.maps.Marker({
        position: location,
        map: Gmap,
        draggable: true
    });

    google.maps.event.addListener(Gmarker, 'dragend', function (event) {
        saveData(Gmap, event);
    });

    Gmarkers.push(Gmarker);
}

function setAllMap(Gmap) {
    for (var i = 0; i < Gmarkers.length; i++) {
        Gmarkers[i].setMap(Gmap);
    }
}

function clearGMarkers() {
    setAllMap(null);
}

function showGMarkers() {
    setAllMap(Gmap);
}

function deleteGMarkers() {
    clearGMarkers();
    Gmarkers = [];
    $('#gmapPetPosition').val('0,0');
}


var tiles = L.tileLayer('https://api.mapbox.com/styles/v1/mapbox/streets-v10/tiles/256/{z}/{x}/{y}?access_token=pk.eyJ1Ijoidml0dG9yIiwiYSI6ImNqMGRzc2l1MDAwazEycXVpYWg1NHpnM2YifQ.e2d9NQgefD9vl6B1lNiePA', {
    maxZoom: 18,
    attribution: 'Mapa do Pet',
    id: 'vittor.cj0dt4o6j00nv32qlx13bnn6u-1kkgx',
    accessToken: 'pk.eyJ1Ijoidml0dG9yIiwiYSI6ImNqMGRzc2l1MDAwazEycXVpYWg1NHpnM2YifQ.e2d9NQgefD9vl6B1lNiePA'
});

var map = new L.Map('map', {
    center: posPin, zoom: 16, layers: [tiles], contextmenu: true,
    contextmenuWidth: 200,
    contextmenuItems: [{
        text: 'Perdi Meu Pet',
        callback: menuLostPet,
        icon: varHost + '/images/contextMenuPet.png',
    }, '-', {
        text: 'Vi um Pet sozinho',
        callback: menuPetAbandonado,
        icon: varHost + '/images/contextMenuVisto.png',
    }, '-', {
        text: 'Pet para adoção',
        callback: menuPetAdocao,
        icon: varHost + '/images/contextMenuAdocao.png',
    },
        '-', {
        text: 'Centralizar',
        callback: menuCentralizar,
        icon: varHost + '/images/contextMenuCentro.png',
    }
        , {
        text: 'Zoom in',
        icon: varHost + '/images/zoom-in.png',
        callback: zoomIn
    }, {
        text: 'Zoom out',
        icon: varHost + '/images/zoom-out.png',
        callback: zoomOut
    }]
});