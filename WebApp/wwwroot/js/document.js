let arrayCheckedId = [];
function checkTick(id) {
    let currentStatus = document.getElementById(id).checked;
    if (currentStatus) {
        arrayCheckedId.push(id);
    } else {
        if (arrayCheckedId.indexOf(id) > -1) {
            arrayCheckedId.splice(arrayCheckedId.indexOf(id), 1);
        }
    }
    console.log(arrayCheckedId);

}

function redirectToCompare() {
    let url = "https://localhost:5001/Document/CompareDocument?doc1=" + arrayCheckedId[0] + "&doc2=" + arrayCheckedId[1];
    window.location.href = url;
}