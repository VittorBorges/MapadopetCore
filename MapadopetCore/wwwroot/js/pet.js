var petIcon = L.icon({
    iconUrl: 'images/petIcon.png',
    //shadowUrl: 'images/sombrapet.png',
    iconSize: [40, 40], // size of the icon
    //shadowSize:   [64, 70], // size of the shadow
    iconAnchor: [20, 20], // point of the icon which will correspond to marker's location
    //shadowAnchor: [9, 66],  // the same for the shadow
    popupAnchor: [0, -20] // point from which the popup should open relative to the iconAnchor
});

var userIcon = L.icon({
    iconUrl: 'images/userM.png',
    iconSize: [40, 40], 
    iconAnchor: [20, 20],
    popupAnchor: [0, -20] 
});