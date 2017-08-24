var petLostIcon = L.icon({
    iconUrl: varHost + '/images/petIcon.png',
    //shadowUrl: 'images/sombrapet.png',
    iconSize: [40, 40], // size of the icon
    //shadowSize:   [64, 70], // size of the shadow
    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
    //shadowAnchor: [9, 66],  // the same for the shadow
    popupAnchor: [0, -20] // point from which the popup should open relative to the iconAnchor
});

var userIcon = L.icon({
    iconUrl: varHost + '/images/userM.png',
    iconSize: [40, 40], 
    iconAnchor: [20, 20],
    popupAnchor: [0, -20] 
});

var petShopIcon = L.icon({
    iconUrl: varHost + '/images/petShopIcon.png',
    iconSize: [40, 40],
    iconAnchor: [20, 20],
    popupAnchor: [0, -20]
});

var petAdocaoIcon = L.icon({
    iconUrl: varHost + '/images/petAdocaoIcon.png',
    iconSize: [40, 40],
    iconAnchor: [20, 20],
    popupAnchor: [0, -20]
});

var petAbandonadoIcon = L.icon({
    iconUrl: varHost + '/images/petVistoIcon.png',
    iconSize: [40, 40],
    iconAnchor: [20, 20],
    popupAnchor: [0, -20]
});