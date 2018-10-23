function txt_validate_RequiredField(element, message, hdnBorderWidth, hdnBorderColor, hdnForeColor) {
    var textbox = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenForeColor = document.getElementById(hdnForeColor.id);

    //alert('The hiddend field: ' + hidden.id);

    if (textbox.value == '') {
        textbox.style.height = '25px';
        textbox.style.borderWidth = '1px';
        textbox.style.borderColor = 'red';
        textbox.style.color = 'red';
        textbox.value = message;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenForeColor.value = 'red';
    }
}

function txt_validate_RequiredField_LastName(element, message, hdn) {
    alert('txt_validate_RequiredField_LastName');

    var textbox = document.getElementById(element.id);
    var hidden = document.getElementById(hdn.id);

    if (textbox.value == '') {
        textbox.style.borderWidth = '1px';
        textbox.style.borderColor = 'red';
        textbox.style.color = 'red';
        textbox.value = message;

        hidden.value = 'red';


    }
}

function txt_got_Focus(element, message, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var textbox = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    if (textbox.value == message && textbox.style.borderColor == 'red' && textbox.style.borderWidth == '1px' && textbox.style.color == 'red') {
        textbox.style.height = '25px';
        textbox.style.borderWidth = '1px';
        textbox.style.borderColor = 'black';
        textbox.style.color = 'black';
        textbox.value = '';

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'black';
        hiddenFontColor.value = 'black';
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////


function txt_validate_Required_and_Email(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtEmail = document.getElementById(element.id);
    var regexEmail = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    if (txtEmail.value == '') {
        txtEmail.style.height = '25px';
        txtEmail.style.borderWidth = '1px';
        txtEmail.style.borderColor = 'red';
        txtEmail.style.color = 'red';
        txtEmail.value = empty_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }
    else if (regexEmail.test(txtEmail.value) == false) {
        txtEmail.style.height = '25px';
        txtEmail.style.borderWidth = '1px';
        txtEmail.style.borderColor = 'red';
        txtEmail.style.color = 'red';
        txtEmail.value = invalid_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }

}

function txt_got_Focus_Required_and_Email(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtEmail = document.getElementById(element.id);

    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    if (txtEmail.style.borderWidth == '1px' &&
        txtEmail.style.borderColor == 'red' &&
        txtEmail.style.color == 'red' &&
        (txtEmail.value == empty_warning || txtEmail.value == invalid_warning)) {

        txtEmail.style.borderColor = 'black';
        txtEmail.style.color = 'black';
        txtEmail.value = '';

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'black';
        hiddenFontColor = 'black';

    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////

function txt_validate_Telephone(element, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtTelephone = document.getElementById(element.id);

    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    var regexPhone = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;

    if ((txtTelephone.value != '') && (regexPhone.test(txtTelephone.value) == false)) {
        txtTelephone.style.borderWidth = '1px';
        txtTelephone.style.borderColor = 'red';
        txtTelephone.style.color = 'red';

        txtTelephone.value = invalid_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }
}

function txt_got_Focus_Telephone(element, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtTelephone = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    if (txtTelephone.style.borderWidth == '1px' &&
        txtTelephone.style.borderColor == 'red' &&
        txtTelephone.style.color == 'red' &&
        txtTelephone.value == invalid_warning) {

        txtTelephone.style.borderColor = 'black';
        txtTelephone.style.color = 'black';
        txtTelephone.value = '';

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'black';
        hiddenFontColor.value = 'black';


    }
}

/////////////////////////////////////////////////////////////////////////////////////////////////////

function txt_validate_Required_and_Telephone(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtTelephone = document.getElementById(element.id);

    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);


    var regexPhone = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;

    if (txtTelephone.value == '') {

        txtTelephone.style.borderWidth = '1px';
        txtTelephone.style.borderColor = 'red';
        txtTelephone.style.color = 'red';
        txtTelephone.value = empty_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }
    else if (regexPhone.test(txtTelephone.value) == false) {

        txtTelephone.style.borderWidth = '1px';
        txtTelephone.style.borderColor = 'red';
        txtTelephone.style.color = 'red';

        txtTelephone.value = invalid_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }
}

function txt_got_Focus_Required_and_Telephone(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {

    var txtTelephone = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    var lblHiddenBorderWidth = document.getElementById('<%= lblHdnTelephoneBorderWidth.ClientID %>');
    var lblHiddenBorderColor = document.getElementById('<%= lblHdnTelephoneBorderColor.ClientID %>');
    var lblHiddenForeColor = document.getElementById('<%= lblHdnTelephoneForeColor.ClientID %>');

    if (txtTelephone.style.borderWidth == '1px' &&
        txtTelephone.style.borderColor == 'red' &&
        txtTelephone.style.color == 'red' &&
        (txtTelephone.value == empty_warning || txtTelephone.value == invalid_warning)) {

            txtTelephone.style.borderWidth = '1px';
            txtTelephone.style.borderColor = 'black';
            txtTelephone.style.color = 'black';

            txtTelephone.value = '';

            hiddenBorderWidth.value = '1';
            hiddenBorderColor.value = 'black';
            hiddenFontColor.value = 'black';

            //lblHiddenBorderWidth.innerHTML = hiddenBorderWidth.value;
            //lblHiddenBorderColor.innerHTML = hiddenBorderColor.value;
            //lblHiddenForeColor.innerHTML = hiddenFontColor.value;

        }


}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////

function txt_validate_Required_and_ZipCode(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {

    var txtZipCode = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    var regexZipCode = /\d{5}(-\d{4})?/;

    if (txtZipCode.value == '') {
        txtZipCode.style.borderWidth = '1px';
        txtZipCode.style.borderColor = 'red';
        txtZipCode.style.color = 'red';
        txtZipCode.style.height = '25px';

        txtZipCode.value = empty_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';
    }
    else if (regexZipCode.test(element.value) == false) {
        txtZipCode.style.borderWidth = '1px';
        txtZipCode.style.borderColor = 'red';
        txtZipCode.style.color = 'red';
        txtZipCode.style.height = '25px';

        txtZipCode.value = invalid_warning;

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'red';
        hiddenFontColor.value = 'red';

    }
}

function txt_got_Focus_Required_and_ZipCode(element, empty_warning, invalid_warning, hdnBorderWidth, hdnBorderColor, hdnFontColor) {
    var txtZipCode = document.getElementById(element.id);
    var hiddenBorderWidth = document.getElementById(hdnBorderWidth.id);
    var hiddenBorderColor = document.getElementById(hdnBorderColor.id);
    var hiddenFontColor = document.getElementById(hdnFontColor.id);

    if (txtZipCode.style.borderWidth == '1px' &&
        txtZipCode.style.borderColor == 'red' &&
        txtZipCode.style.color == 'red' &&
        (txtZipCode.value == empty_warning || txtZipCode.value == invalid_warning)) {

        txtZipCode.style.borderWidth = '1px';
        txtZipCode.style.borderColor = 'black';
        txtZipCode.style.color = 'black'

        txtZipCode.value = '';

        hiddenBorderWidth.value = '1';
        hiddenBorderColor.value = 'black';
        hdnFontColor.value = 'black';

    }
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////////

function trigger_onblur(e, element) {
    var code = (e.keyCode ? e.keyCode : e.which);

    var target = document.getElementById(element.id);

    if (code == 13) {
        //element.dispatchEvent(onblur);
        //alert('You pressed ENTER KEY.');

        target.onblur();
    }
}

//function getNextControl(sender)
//{
//    var waitOne = false;

//    for (i = 0; i<=form1.elements.length; i++)
//    {
//        if (waitOne && form1.elements[i].type == 'text') return form1.elements[i].id;
//        waitOne = sender.id == form1.elements[i].id;
//    }
//}

//function OnFocusOut(e, element, lastTabIndex)
//{
//    var code = (e.keyCode ? e.keyCode : e.which);
//    var currentElement = document.getElementById(element.id);
//    var currentTabIndex = currentElement.tabIndex;

//    if (currentTabIndex == lastTabIndex)
//    {
//        currentTabIndex = 0;
//    }

//    if (code == 13) {
//        var tabTextBoxes = document.querySelectorAll('.TextBox:not([readonly])');

//        for (var i = 0; i < tabTextBoxes.length; i++) {

//            if (tabTextBoxes[i].tabIndex == (currentTabIndex + 1)) {
//                alert(tabTextBoxes[i].tabIndex);
//                tabTextBoxes[i].focus();
//                return (e.keyCode != 13);
//            }
//        }
//    }
//}

function getNextControl(sender) {
    var waitOne = false;
    for (var i = 0; i <= form1.elements.length; i++) {
        if (waitOne && form1.elements[i].type == 'text') return form1.elements[i].id;
        waitOne = sender.id == form1.elements[i].id;
    }
}

function getNextControlNextTab(sender, tab) {
    var waitOne = false;

    var index = 0;
    var senderTabIndex = 0;

    for (var i = 0; i <= form1.elements.length; i++) {
        if (sender.id == form1.elements[i].id) {
            index = i;
            senderTabIndex = sender.tabIndex;
            break;
        }

    }
    for (var j = index; j <= form1.elements.length; j++) {
        if (tab == form1.elements[j].tabIndex) {
            return form1.elements[j].id;
        }
    }
}

function setFocus(sender, args) {
    document.getElementById(getNextControl(sender)).focus();
}

function setFocusToNextTab(sender, args, nextTab) {
    document.getElementById(getNextControlNextTab(sender, nextTab)).focus();
}

function OnFocusOut(e, element, lastTabIndex) {

    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }
    else if ((keycode === 13) && (currentText == '')) {
        e.preventDefault();
        setFocus(currentElement, null);
        return false;
    }
}

function OnMoveFocusOnEnter(e, element, lastTabIndex) {

    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    var nextTab = currentElement.tabIndex;
    nextTab++;

    //alert("The Tab index: " + nextTab);

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        //setFocus(currentElement, null, nextTab);
        setFocusToNextTab(currentElement, null, nextTab);
        return false;
    }
}

function OnMoveFocusToTelephoneOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= txtTelephone1.ClientID %>').focus();
        false;
    }
}

function OnMoveFocusToChurchNameOnEnter(e, element, lastTabIndex) {

    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= txtChurchName.ClientID %>').focus();
        return false;
    }
}

function OnMoveFocusToChkBillingAddrTelephoneOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= chkBillingAddress.ClientID %>').focus();
        return false;
    }

}

function OnMoveFocusToBankACHOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= rbBankACH.ClientID %>').focus();
        return false;
    }
}

function OnMoveFocusToCreditCardOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= rbCreditCard.ClientID %>').focus();
        return false;
    }

}

function OnMoveFocusToFrequencyRecurringOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= rbRecurring.ClientID %>').focus();
        return false;
    }

}

function OnMoveFocusToFrequencyOneTimeOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= rbOneTime.ClientID %>').focus();
        return false;
    }

}

function OnMoveFocusToChkBillingAddressOnEnter(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var currentText = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= chkBillingAddress.ClientID %>').focus();
        return false;
    }

}

function MoveFromChkBillingOnEnter(e, element) {
    var keycode = (e.keyCode ? e.keyCode : e.which);

    if (keycode === 13) {
        if (document.getElementById('<%= chkBillingAddress.ClientID %>').checked) {
            e.preventDefault();
            document.getElementById('<%= txtBillingStreet.ClientID %>').focus();
            return false;
        }
        else {
            e.preventDefault();
            document.getElementById('<%= chkEmail.ClientID %>').focus();
            return false;
        }
    }
}

function MoveToChkPostalOnEnter(e, element) {
    var keycode = (e.keyCode ? e.keyCode : e.which);

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= chkPostal.ClientID %>').focus();
        return false;
    }
}

function MoveToEmailOnEnter(e, element) {
    var keycode = (e.keyCode ? e.keyCode : e.which);

    if (keycode === 13) {
        e.preventDefault();
        document.getElementById('<%= txtEmail.ClientID %>').focus();
        return false;
    }
}

function MoveToEmailOnTab(e, element) {
    var keycode = (e.keyCode ? e.keyCode : e.which);

    if (keycode === 9) {
        e.preventDefault()
        document.getElementById('<%= txtEmail.ClientID %>').focus();
        return false;
    }
}

function OnValidateZipCodeOnBlur(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var ZipCode = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;
    var regexZipCode = /\d{5}(-\d{4})?/;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if ((keycode === 13) && (ZipCode == '')) {
        e.preventDefault();
        setFocus(currentElement, null);
        return false;
    }
    else if ((keycode === 13) && (regexZipCode.test(ZipCode) == false)) {
        e.preventDefault();
        setFocus(currentElement, null);
        return false;
    }
}

function OnValidateZipCodeAndMoveFocus(e, element, lastTabIndex, target) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    var currentElement = document.getElementById(element.id);
    var targetElement = document.getElementById(target.id);
    var ZipCode = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;
    var regexZipCode = /\d{5}(-\d{4})?/;

    alert('OnValidateZipCodeAndMoveFocus');

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if ((keycode === 13) && (ZipCode == '')) {
        e.preventDefault();
        setFocus(currentElement, null);
        return false;
    }
    else if ((keycode === 13) && (regexZipCode.test(ZipCode) == false)) {
        e.preventDefault();
        setFocus(currentElement, null);
        return false;
    }
    else if ((keycode === 13) && (regexZipCode.test(ZipCode) == true)) {
        alert('OnValidateZipCodeAndMoveFocus');
        //e.preventDefault();
        targetElement.focus();
        return false;
    }

}

function OnValidateEmailOnBlur(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which)
    var currentElement = document.getElementById(element.id);
    var nextTab = currentElement.tabIndex;
    nextTab++;
    var Email = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;
    var regexEmail = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }

    if (keycode === 13) {
        e.preventDefault();
        setFocusToNextTab(currentElement, null, nextTab);
        return false;
    }
}

function OnValidateTelephoneOnBlur(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which)
    var currentElement = document.getElementById(element.id);
    var nextTab = currentElement.tabIndex;
    nextTab++;
    var Telephone = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;
    var regexTelephone = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }
    if (keycode === 13) {
        e.preventDefault();
        //setFocus(currentElement, null);
        setFocusToNextTab(currentElement, null, nextTab);
        return false;
    }
    //if ((keycode === 13) && (Telephone == ''))
    //{
    //    e.preventDefault();
    //    setFocus(currentElement, null);
    //    return false;
    //}
    //else if ((keycode === 13) && (regexTelephone.test(Telephone)) == false)
    //{
    //    e.preventDefault();
    //    setFocus(currentElement, null);
    //    return false;
    //}
}

function OnValidateTelephoneOnBlurRegEx(e, element, lastTabIndex) {
    var keycode = (e.keyCode ? e.keyCode : e.which)
    var currentElement = document.getElementById(element.id);

    var nextTab = currentTabIndex + 1;
    var Telephone = currentElement.value;
    var currentTabIndex = currentElement.tabIndex;
    var regexTelephone = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;

    if (currentTabIndex == lastTabIndex) {
        currentTabIndex = 0;
    }
    //if ((keycode === 13) && (Telephone == '')) {
    //    e.preventDefault();
    //    setFocus(currentElement, null);
    //    return false;
    //}
    if ((keycode === 13) && (regexTelephone.test(Telephone)) == false) {
        e.preventDefault();
        //setFocus(currentElement, null);
        setFocusToNextTab(currentElement, null, nextTab);
        return false;
    }
}

function ChurchNameFocus() {
    document.getElementById('<%= txtChurchName.ClientID %>').focus();
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////
function validat_phone_number() {

    var textbox = document.getElementById('<%= txtTelephone1.ClientID %>');

    if (textbox.value == '') {
        textbox.style.borderWidth = '1px';
        textbox.style.borderColor = 'red';
        textbox.style.color = 'red';
        textbox.value = 'Enter phone number';
    }
    else {
        textbox.style.borderColor = 'black';
        textbox.style.color = 'black';
        //textbox.value = '';
    }

    alert(textbox.value);

    //var regex = new RegExp()

    var phone_number = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;
    var bIsPhone = phone_number.test(textbox.value);

    //alert(bIsPhone);

    if (bIsPhone == false) {
        textbox.style.borderWidth = '1px';
        textbox.style.borderColor = 'red';
        textbox.style.color = 'red';
        textbox.value = 'Enter valid phone number';
    }
    else {
        textbox.style.borderWi = '1px';
        textbox.style.borderColor = 'black';
        textbox.style.color = 'black';
    }
}

function btnCancel_ClientClick() {
    var hiddenEmail = document.getElementById('<%= hdnEmail.ClientID %>');
    var textEmail = document.getElementById('<%= txtEmail.ClientID %>');

    textEmail.value = hiddenEmail.value;


    var hiddenBillingAddress = document.getElementById('<%= hdnBillingStreetAddress.ClientID %>');
    var textBillingAddress = document.getElementById('<%= txtBillingStreet.ClientID %>');

    textBillingAddress.value = hiddenBillingAddress.value;

    return false;


}

function OnEscapeKeyPressed(e, element, hdnElement) {
    var keycode = (e.keyCode ? e.keyCode : e.which)
    var textBox = document.getElementById(element.id);
    var hidden = document.getElementById(hdnElement.id);

    //var hdnBorderWidth = document.getElementById(hdnBorderWidth.id);
    //var hdnBorderColor = document.getElementById(hdnBorderColor.id);
    //var hdnFontColor = document.getElementById(hdnFontColor.id);

    if (keycode == 27) {
        //hdnBorderWidth.value = '1';
        //hdnBorderColor.value = 'black';
        //hdnFontColor.value = 'black';

        textBox.value = hidden.value;
    }

}

function OnPasswordReset(e) {

    var newPassword = document.getElementById('<%=txtNewPassword.ClientID%>');
    var newPasswordConfirm = document.getElementById('<%=txtNewPasswordConfirmation.ClientID%>');
    var lblPasswordMismatch = document.getElementById('<%=lblPasswordMismatchError.ClientID%>');


    if (newPassword.value != newPasswordConfirm.value) {
        lblPasswordMismatch.innerHTML = "Passwords do not match!";
        e.preventDefault();
    }
}
/////////////////////////////////////////////////////////////
//Buttons used for Health history

//function toggleYes(button) {
//    var btnYes = document.getElementById(button.id);

//    var siblings = btnYes.parentElement.childNodes;

//    var hdnYes = siblings[1];
//    var btnNo = siblings[2];
//    var hdnNo = siblings[3];

//    if (btnYes.style.backgroundColor == 'lightgrey') {
//        btnYes.style.backgroundColor = 'red';
//        hdnYes.value = 'red';
//        btnYes.style.color = 'white';
//        btnNo.style.backgroundColor = 'lightgrey';
//        hdnNo.value = 'lightgrey';
//        btnNo.style.color = 'black';
//    }
//    else if (btnYes.style.backgroundColor == 'red') {
//        btnYes.style.backgroundColor = 'lightgrey';
//        hdnYes.value = 'lightgrey';
//        btnYes.style.color = 'black';
//        btnNo.style.backgroundColor = 'blue';
//        hdnNo.value = 'blue';
//        btnNo.style.color = 'white';
//    }
//}

//function toggleNo(button) {
//    var btnNo = document.getElementById(button.id);

//    var siblings = btnNo.parentElement.childNodes;

//    var btnYes = siblings[0];
//    var hdnYes = siblings[1];
//    var hdnNo = siblings[3];

//    if (btnNo.style.backgroundColor == 'blue') {
//        btnNo.style.backgroundColor = 'lightgrey';
//        hdnNo.value = 'lightgrey';
//        btnNo.style.color = 'black';
//        btnYes.style.backgroundColor = 'red';
//        hdnYes.value = 'red';
//        btnYes.style.color = 'white';
//    }
//    else if (btnNo.style.backgroundColor = 'lightgrey') {
//        btnNo.style.backgroundColor = 'blue';
//        hdnNo.value = 'blue';
//        btnNo.style.color = 'white';
//        btnYes.style.backgroundColor = 'lightgrey';
//        hdnYes.value = 'lightgrey';
//        btnYes.style.color = 'black';
//    }
//}