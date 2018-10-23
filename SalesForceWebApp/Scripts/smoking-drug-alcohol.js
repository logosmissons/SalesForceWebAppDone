function toggleYes(button) {

    var btnYes = document.getElementById(button.id);

    var siblings = btnYes.parentElement.childNodes;

    var hdnYes = siblings[1];
    var btnNo = siblings[2];
    var hdnNo = siblings[3];

    if (btnYes.style.backgroundColor == 'lightgrey') {
        btnYes.style.backgroundColor = 'red';
        btnYes.style.color = 'white';
        hdnYes.value = 'red';
        btnNo.style.backgroundColor = 'lightgrey';
        btnNo.style.color = 'black';
        hdnNo.value = 'lightgrey';
    }
    else if (btnYes.style.backgroundColor == 'red') {
        btnYes.style.backgroundColor = 'lightgrey';
        btnYes.style.color = 'black';
        hdnYes.value = 'lightgrey';
        btnNo.style.backgroundColor = 'blue';
        btnNo.style.color = 'white';
        hdnNo.value = 'blue';
    }
}

function toggleNo(button) {
    var btnNo = document.getElementById(button.id);

    var siblings = btnNo.parentElement.childNodes;

    var btnYes = siblings[0];
    var hdnYes = siblings[1];
    var hdnNo = siblings[3];

    if (btnNo.style.backgroundColor == 'blue') {
        btnNo.style.backgroundColor = 'lightgrey';
        btnNo.style.color = 'black';
        hdnNo.value = 'lightgrey';
        btnYes.style.backgroundColor = 'red';
        btnYes.style.color = 'white';
        hdnYes.value = 'red';
    }
    else if (btnNo.style.backgroundColor == 'lightgrey') {
        btnNo.style.backgroundColor = 'blue';
        btnNo.style.color = 'white';
        hdnNo.value = 'blue';
        btnYes.style.backgroundColor = 'lightgrey';
        btnYes.style.color = 'black';
        hdnYes.value = 'lightgrey';
    }
}

function toggleFormerYes(button) {

    var btnYes = document.getElementById(button.id);

    var siblings = btnYes.parentElement.childNodes;

    var hdnYes = siblings[1];
    var btnNo = siblings[2];
    var hdnNo = siblings[3];

    if (btnYes.style.backgroundColor == 'lightgrey') {
        btnYes.style.backgroundColor = 'green';
        btnYes.style.color = 'white';
        hdnYes.value = 'green';
        btnNo.style.backgroundColor = 'lightgrey';
        btnNo.style.color = 'black';
        hdnNo.value = 'lightgrey';
    }
    else if (btnYes.style.backgroundColor == 'green') {
        btnYes.style.backgroundColor = 'lightgrey';
        btnYes.style.color = 'black';
        hdnYes.value = 'lightgrey';
        btnNo.style.backgroundColor = 'blue';
        btnNo.style.color = 'white';
        hdnNo.value = 'blue';
    }
}

function toggleFormerNo(button) {
    var btnNo = document.getElementById(button.id);

    var siblings = btnNo.parentElement.childNodes;

    var btnYes = siblings[0];
    var hdnYes = siblings[1];
    var hdnNo = siblings[3];

    if (btnNo.style.backgroundColor == 'green') {
        btnNo.style.backgroundColor = 'lightgrey';
        btnNo.style.color = 'black';
        hdnNo.value = 'lightgrey';
        btnYes.style.backgroundColor = 'green';
        btnYes.style.color = 'white';
        hdnYes.value = 'green';
    }
    else if (btnNo.style.backgroundColor == 'lightgrey') {
        btnNo.style.backgroundColor = 'blue';
        btnNo.style.color = 'white';
        hdnNo.value = 'blue';
        btnYes.style.backgroundColor = 'lightgrey';
        btnYes.style.color = 'black';
        hdnYes.value = 'lightgrey';
    }
}