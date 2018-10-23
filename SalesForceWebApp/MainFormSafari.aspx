<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFormSafari.aspx.cs" Inherits="SalesForceWebApp.MainFormSafari" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="smRegister" runat="server"></asp:ScriptManager>

    <div>
    
    <style type="text/css">
                .tabSubTitle {
                    font-size: medium;
                    font-weight: normal;
                }

                .pnlLabel {
                    font-size: small;
                    font-weight: normal;
                    float: left;
                    margin-bottom: 0px;
                }

                .pnlLabelSpouseInfo {
                    font-size: small;
                    font-weight: normal;
                    float: left;
                    margin-top: 5px;
                    margin-bottom: 10px;
                }

                .pnlLabelChildrenInfo {
                    font-size: small;
                    font-weight: normal;
                    float: left;
                    margin-top: 5px;
                    margin-bottom: 10px;
                }

                .lblChildrenName {
                    font-size: 14px;
                    float: left;
                    height: 25px;
                }

                .lblChildrenDateOfBirth {
                    font-size: 14px;
                    float: left;
                    height: 25px;
                }

                .lblChildrenStartDate {
                    font-size: 14px;
                    float: left;
                    height: 25px;
                }

                .inlineBlock {
                    display: inline-block;
                }

                .floatLeft {
                    float: left;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .TabContainer {
                    position: relative;
                    width: 1120px;
                    height: 1000px;
                }

                .TopTabPanel {
                    position: relative;
                    width: 1100px;
                    height: 1000px;
                }


                .topLeftPanel {
                    position: absolute;
                    top: 0;
                    left: 0;
                    /*float: left;*/
                    width: 535px;
                    height: 500px;
                    font-size: medium;
                    font-weight: bold;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 0px;
                    margin-bottom: 15px;
                }

                .topRightPanel {
                    position: absolute;
                    top: 0;
                    right: 0;
                    /*float: right;*/
                    width: 535px;
                    height: 500px;
                    font-size: medium;
                    font-weight: bold;
                    margin-top: 3px;
                    margin-left: 0px;
                    margin-right: 10px;
                    margin-bottom: 15px;
                }

                .downLeftPanel {
                    position: absolute;
                    bottom: 0;
                    left: 0;
                    width: 535px;
                    height: 550px;
                    /*width: 544px;
                    height: 445px;*/
                    font-size: medium;
                    font-weight: bold;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 0px;
                    margin-bottom: 10px;
                }

                .downRightPanel {
                    position: absolute;
                    bottom: 0;
                    right: 0;
                    width: 535px;
                    height: 550px;
                    font-size: medium;
                    font-weight: bold;
                    margin-top: 3px;
                    margin-left: 0px;
                    margin-right: 10px;
                    margin-bottom: 10px;
                }

                .GeneralInfoLabelPanel {
                    position: absolute;
                    bottom: 0;
                    left: 0;
                    margin-top: 5px;
                    margin-left: 5px;
                    margin-right: 5px;
                    margin-bottom: 10px;
                }

                .GeneralInfoButtonPanel {
                    /*font-size: medium;
                         font-weight: bold;*/
                    position: absolute;
                    bottom: 0;
                    right: 0;
                    margin-top: 5px;
                    margin-left: 5px;
                    margin-right: 5px;
                    margin-bottom: 10px;
                }

                .GeneralInfoButtonWithBillingAddress {
                    position: absolute;
                    bottom: -180px;
                    right: 0;
                    margin-top: 5px;
                    margin-left: 5px;
                    margin-right: 5px;
                    margin-bottom: 10px;

                }

                .floatLeftPrograms {
                    float: left;
                    height: 50px;
                    width: 163px;
                    font-weight: normal;
                    margin-top: 20px;
                    margin-left: 2px;
                    margin-right: 2px;
                    margin-bottom: 8px;
                }

                .floatLeftChurchName {
                    float: left;
                    height: 40px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 10px;
                }

                .floatLeftSeniorPastor {
                    float: left;
                    height: 40px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 10px;
                }

                .floatLeftMedicare {
                    float: left;
                    height: 44px;
                    width: 180px;
                    font-size: small;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftPaymentMethod {
                    float: left;
                    height: 52px;
                    font-size: small;
                    font-weight: normal;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftPaymentFrequency {
                    float: left;
                    height: 52px;
                    font-size: small;
                    font-weight: normal;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .NotifyByContainer {
                    float: left;
                    margin-top: 10px;
                }

                .floatLeftNotifyBy {
                    float: left;
                    height: 52px;
                    width: 240px;
                    font-weight: normal;
                    font-size: small;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftJoinMailing {
                    float: left;
                    height: 52px;
                    font-weight: normal;
                    font-size: small;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftAllowMessages {
                    float: left;
                    height: 52px;
                    font-weight: normal;
                    font-size: small;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftChurchAddress {
                    float: left;
                    font-weight: normal;
                    height: 50px;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatLeftChurchTelephone {
                    float: left;
                    height: 40px;
                    font-weight: normal;
                    margin-top: 14px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatSubPanel {
                    float: left;
                    height: 70px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatSubPanelSpouse {
                    float: left;
                    height: 73px;
                    font-weight: normal;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatSubPanelChildren {
                    float: left;
                    height: 195px;
                    font-weight: normal;
                    margin-top: 10px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 15px;
                }

                .floatTelephone {
                    float: left;
                    height: 45px;
                    width: 164px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatTitle {
                    float: left;
                    font-weight: normal;
                    height: 30px;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }



                .floatLeftPersonalInfoBottom {
                    float: left;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 15px;
                }

                .floatLeftCity {
                    float: left;
                    height: 25px;
                    width: 250px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 30px;
                }

                .floatLeftState {
                    float: left;
                    height: 25px;
                    width: 82px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 30px;
                }

                .floatZipCode {
                    float: left;
                    height: 25px;
                    width: 120px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 30px;
                }

                .floatLeftBillingAddress {
                    float: left;
                    font-weight: normal;
                    margin-top: 0px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 0px;
                }

                .floatZipCodeBillingAddress {
                    float: left;
                    height: 38px;
                    width: 120px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 15px;
                }

                .floatLeftStateBillingAddress {
                    float: left;
                    height: 38px;
                    width: 82px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 15px;
                }

                .floatLeftCityBillingAddress {
                    float: left;
                    height: 38px;
                    width: 250px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 15px;
                }

                .floatLeftCheckBox {
                    float: left;
                    font-weight: normal;
                    margin-top: 5px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 5px;
                }

                .floatLeftRadioButton {
                    float: left;
                    font-weight: normal;
                    margin-top: 5px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 5px;
                }

                .floatLeftGenderRadioButton {
                    float: left;
                    font-weight: normal;
                    margin-top: 4px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatPanelGender {
                    float: left;
                    font-size: small;
                    font-weight: normal;
                    height: 44px;
                    width: 180px;
                    margin-top: 6px;
                    margin-bottom: 0px;
                    margin-left: 1px;
                    margin-right: 5px;
                }

                .floatDateOfBirth {
                    float: left;
                    height: 40px;
                    width: 154px;
                    font-weight: normal;
                    margin-top: 4px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatSSN {
                    float: left;
                    height: 48px;
                    width: 120px;
                    font-weight: normal;
                    margin-top: 5px;
                    margin-left: 1px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .floatSpouse {
                    float: left;
                    height: 120px;
                    width: 506px;
                    font-weight: normal;
                    margin-top: 5px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 0px;
                }

                .floatChildren {
                    float: left;
                    height: 254px;
                    width: 506px;
                    font-weight: normal;
                    margin-top: 12px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 10px;
                }

                .listboxChildren {
                    float: left;
                    font-weight: normal;
                    min-height: 200px;
                    max-height: 200px;
                    border-width: 0px;
                }

                .ddlTitle {
                    height: 29px;
                    float: left;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 0px;
                    margin-right: 5px;
                    margin-bottom: 0px;
                }

                .DropDownList {
                    height: 29px;
                    float: left;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 5px;
                    margin-bottom: 0px;
                }

                .DropDownListState {
                    height: 29px;
                    width: 80px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .DropDownListCity {
                    height: 29px;
                    width: 320px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .ddlBillingState {
                    height: 29px;
                    width: 80px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .ddlBillingStateBillingAddress {
                    height: 29px;
                    width: 80px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .ddlBillingCity {
                    height: 29px;
                    width: 280px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .ddlBillingCityBillingAddress {
                    height: 29px;
                    width: 280px;
                    font-weight: normal;
                    margin-top: 3px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 3px;
                }

                .TextBox {
                    font-weight: normal;
                    height: 23px;
                    margin-top: 3px;
                    margin-left: 0px;
                    margin-bottom: 5px;
                }

                .atLeft {
                    clear: left;
                    font-weight: normal;
                }

                .BillingAddressDifferent {
                    float: left;
                    font-weight: normal;
                    height: 40px;
                    margin-top: 20px;
                    margin-left: 3px;
                    margin-right: 3px;
                    margin-bottom: 4px;
                }

                .ChildrenAddRemoveButton {
                    margin-bottom: 10px;
                }

                .pnlBillingAddressSaveButton {
                    margin-top: 10px;
                    margin-bottom: 10px;
                }

                .BillingAddressSaveButton {
                    float: right;
                    height: 25px;
                    width: 100px;
                    margin-top: 20px;
                    margin-left: 5px;
                    margin-right: 8px;
                    margin-bottom: 10px;
                }

                .GeneralInfoUpdateCancelButton {
                    float: right;
                    height: 25px;
                    width: 100px;
                    margin-top: 5px;
                    margin-left: 5px;
                    margin-right: 5px;
                    margin-bottom: 10px;
                }

                /*.Validators
            {
                        color: red;
                        margin-right: 0px;

            }*/

                .modalBackground {
                    background-color: gray;
                    filter: alpha(opacity=50);
                    opacity: 0.7;
                }

                .pnlBackGround {
                    position: fixed;
                    font-size: small;
                    font-weight: normal;
                    top: 10%;
                    left: 10px;
                    width: 320px;
                    height: 150px;
                    text-align: center;
                    background-color: white;
                    border: solid 1px black;
                }

                .pnlPageValidationFailure {
                    height: 280px;
                    width: 320px;
                    position: fixed;
                    font-size: small;
                    font-weight: normal;
                    top: 10%;
                    left: 10px;
                    text-align: center;
                    background-color: white;
                    border: solid 1px black;
                }
            </style>

            <script type="text/javascript">




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

                    if (textbox.value == message && textbox.style.borderColor == 'red' && textbox.style.borderWidth == '1px') {
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
                    if (document.getElementById('<% = chkBillingAddress.ClientID %>').checked) {
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

                if (keycode == 27) {
                    textBox.value = hidden.value;
                }

            }

            </script>

            <div style="text-align: left; font-size: large; color:blue; margin-left: 100px;">Please do not close the window. After you finish the updating, please click Log Out button at the bottom right side of window.</div><br />
            <ajaxToolkit:TabContainer ID="tabContainerRegister" runat="server" CssClass="TabContainer">
                
                <ajaxToolkit:TabPanel ID="tpnlGeneralInfo" runat="server" HeaderText="General Information" CssClass="TopTabPanel" >
                    <ContentTemplate>


                        <asp:Panel ID="pnlPersonalInfo" runat="server" GroupingText="Personal Information" CssClass="topLeftPanel">
                            <asp:Panel ID="pnlPrimaryID" runat="server" Width="505px" Height="50px" CssClass="floatLeft">
                                <asp:Label ID="lblPrimaryID" runat="server" Text="Primary ID" Width="100px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPrimaryID" runat="server" Width="500px" ReadOnly="True" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>
                            <asp:Panel ID="pnlEmail" runat="server" Width="505px" Height="50px" CssClass="floatLeft">
                                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtEmail" runat="server" Width="500px" CssClass="TextBox"
                                    onblur="txt_validate_Required_and_Email (this, 'Email address required!', 'Email address invalid!', hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);"
                                    onfocus="txt_got_Focus_Required_and_Email (this, 'Email address required!', 'Email address invalid!', hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);"
                                    onkeypress="OnValidateEmailOnBlur(event, this, 24);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnEmail);" TabIndex="1"></asp:TextBox>
                                <asp:HiddenField ID="hdnEmail" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailFontColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="rfvMemberEmail" runat="server" EnableClientScript="False" ControlToValidate="txtEmail" InitialValue="Email address required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMemberEmail" runat="server" EnableClientScript="False" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTitle" runat="server" Width="70px" Height="50px" CssClass="floatTitle">

                                <asp:Label ID="lblTitle" runat="server" Text="Title" Width="60px" CssClass="pnlLabel"></asp:Label>
                                <asp:DropDownList ID="ddlTitle" runat="server" Width="60px" CssClass="ddlTitle">
                                    <asp:ListItem Text="Jr." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Mr." Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Mrs." Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Ms." Value="3"></asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlLastName" runat="server" Width="150px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name" Width="145px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtLastName" runat="server" Width="140px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Last name required!', hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'Last name required!', hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnLastName);" TabIndex="2"></asp:TextBox>

                                <asp:HiddenField ID="hdnLastName" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameFontColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" EnableClientScript="False" InitialValue="Last name required!" ></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlFirstName" runat="server" Width="150px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="145px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtFirstName" runat="server" Width="140px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'First name required!', hdnFirstNameBorderWidth, hdnFirstNameBorderColor, hdnFirstNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'First name required!', hdnFirstNameBorderWidth, hdnFirstNameMiddleNameBorderColor, hdnFirstNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnFirstName);" TabIndex="3"></asp:TextBox>
                                <asp:HiddenField ID="hdnFirstName" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameFontColor" runat="server" Value="" ClientIDMode="Static" />
                               
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" EnableClientScript="False" InitialValue="First name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlMiddleName" runat="server" Width="115px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" Width="100px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtMiddleName" runat="server" Width="112px" CssClass="TextBox"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnMiddleName);" TabIndex="4"></asp:TextBox>
                                <asp:HiddenField ID="hdnMiddleName" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <%--<asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" EnableClientScript="False" ControlToValidate="txtMiddleName"></asp:RequiredFieldValidator>--%>
                            </asp:Panel>


                            <asp:Panel ID="pnlDateOfBirth" runat="server" Height="50px" CssClass="floatDateOfBirth">
                                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth" Width="120px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtDateOfBirth" runat="server" Width="142px" ReadOnly="True" TextMode="DateTime" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>

                            <asp:Panel ID="pnlGender" runat="server" GroupingText="Gender" CssClass="floatPanelGender">

                                <asp:RadioButton ID="rbMale" runat="server" Width="60px" Text="Male" Enabled="False" GroupName="Gender" Font-Size="Small" CssClass="floatLeftGenderRadioButton" />
                                <asp:RadioButton ID="rbFemale" runat="server" Width="78px" Text="Female" Enabled="False" GroupName="Gender" Font-Size="Small" CssClass="floatLeftGenderRadioButton" />
                            </asp:Panel>

                            <asp:Panel ID="pnlSSN" runat="server" Height="50px" CssClass="floatSSN">
                                <asp:Label ID="lblSSN" runat="server" Text="SSN" Width="100px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtSSN" runat="server" Width="156px" ReadOnly="True" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>

                            <br />

                            <asp:Panel ID="pnlTelephone1" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone1" runat="server" Text="Telephone" Width="150px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtTelephone1" runat="server" Width="150px" CssClass="TextBox"
                                    onblur="txt_validate_Required_and_Telephone (this, 'Phone number required!', 'Invalid phone number!', hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);"
                                    onfocus="txt_got_Focus_Required_and_Telephone (this, 'Phone number required!', 'Invalid phone number!', hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnTelephone);" TabIndex="5"></asp:TextBox>
                                <asp:HiddenField ID="hdnTelephone" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone1" InitialValue="Phone number required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone1" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTelephone2" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone2" runat="server" Text="Mobile Phone" Width="150px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTelephone2" runat="server" Width="158px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnMobilePhone);" TabIndex="6"></asp:TextBox>

                                <asp:HiddenField ID="hdnMobilePhone" runat="server" Value="" ClientIDMode ="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RegularExpressionValidator ID="revMobilePhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone2" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTelephone3" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone3" runat="server" Text="Other Phone" Width="165px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTelephone3" runat="server" Width="160px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnOtherPhone);" TabIndex="7"></asp:TextBox>
                                <asp:HiddenField ID="hdnOtherPhone" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RegularExpressionValidator ID="revOtherPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone3" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>


                            <asp:Panel ID="pnlAddress" runat="server" Height="50px" Width="500px" CssClass="floatLeft">
                                <asp:Label ID="lblAddress" runat="server" Width="500px" Text="Address" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAddress" runat="server" Width="500px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Your address required!', hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor)"
                                    onfocus="txt_got_Focus(this, 'Your address required!', hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnAddress);" TabIndex="8"></asp:TextBox>
                                <asp:HiddenField ID="hdnAddress" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvStreetAddress" runat="server" ErrorMessage="Street address required" EnableClientScript="False" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>

                            </asp:Panel>


                            <asp:Panel ID="pnlZipCode" runat="server" CssClass="floatZipCode" DefaultButton="btnZipCodeHidden">
                                <asp:Label ID="lblZipCode" runat="server" Width="113px" Text="Zip Code" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtZipCode" runat="server" Width="113px" CssClass="TextBox"
                                    OnTextChanged="txtZipCode_TextChanged"
                                    onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);"
                                    onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnZipCode);"
                                    TabIndex="9"></asp:TextBox>
                                <asp:HiddenField ID="hdnZipCode" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:Button ID="btnZipCodeHidden" runat="server" OnClick="btnZipCodeHidden_Click" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtZipCode" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtZipCode" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlState" runat="server" CssClass="floatLeftState">
                                <asp:Label ID="lblState" runat="server" Text="State" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtState" runat="server" Width="75px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'State required!', hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);"
                                    onfocus="txt_got_Focus(this, 'State required!', hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnState);" TabIndex="10"></asp:TextBox>
                                <asp:HiddenField ID="hdnState" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" EnableClientScript="False" InitialValue="State required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlCity" runat="server" CssClass="floatLeftCity">
                                <asp:Label ID="lblCity" runat="server" Width="290px" Text="City" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtCity" runat="server" Width="286px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'City name required!', hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor)"
                                    onfocus="txt_got_Focus(this, 'City name required!', hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);"
                                    onkeypress="OnMoveFocusToChurchNameOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnCity);" TabIndex="11"></asp:TextBox>
                                <asp:HiddenField ID="hdnCity" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" EnableClientScript="False" ControlToValidate="txtCity" InitialValue="City name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </asp:Panel>

                        

                        <asp:Panel ID="pnlMemberInformation" runat="server" GroupingText="Member Information" CssClass="topRightPanel">

                            <asp:Panel ID="pnlQualifyForMedicare" runat="server" GroupingText="Qualify for Medicare" CssClass="floatLeftMedicare">
                                <asp:RadioButton ID="rbYesQualifyForMedicare" runat="server" GroupName="QualifyMedicare" Enabled="False" Font-Size="Small" Text="Yes" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbNoQualifyForMedicare" runat="server" GroupName="QualifyMedicare" Enabled="False" Font-Size="Small" Text="No" CssClass="floatLeftRadioButton" />
                            </asp:Panel>
                            <asp:Panel ID="pnlMedicareAB" runat="server" GroupingText="Medicare A and B" CssClass="floatLeftMedicare">
                                <asp:RadioButton ID="rbMedicareABYes" runat="server" GroupName="MedicareAB" Enabled="False" Font-Size="Small" Text="Yes" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbMedicareABNo" runat="server" GroupName="MedicareAB" Enabled="False" Font-Size="Small" Text="No" CssClass="floatLeftRadioButton" />
                            </asp:Panel>

                            <asp:Panel ID="pnlParticipantProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblParticipantProgram" runat="server" Text="Parcitipant's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlPartipantsProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>
                            </asp:Panel>
                            <asp:Panel ID="pnlSpouseProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblSpouseProgram" runat="server" Text="Spouse's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlSpouseProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>

                            </asp:Panel>

                            <asp:Panel ID="pnlChildrenProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblChildrenProgram" runat="server" Text="Children's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlChildrenProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchName" runat="server" Width="334px" CssClass="floatLeftChurchName">
                                <asp:Label ID="lblChurchName" runat="server" Text="Church Name" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtChurchName" runat="server" Width="327px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Name of church required!', hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'Name of church required!', hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchName);" TabIndex="12"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchName" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchName" runat="server" EnableClientScript="False" ControlToValidate="txtChurchName" InitialValue="Name of church required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlSeniorPastor" runat="server" Width="158px" CssClass="floatLeftSeniorPastor">
                                <asp:Label ID="lblSeniorPastor" runat="server" Text="Senior Pastor" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtSeniorPastor" runat="server" Width="157px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Pastor name required!', hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);"
                                    onfocus="txt_got_Focus(this, 'Pastor name required!', hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnSeniorPastor);" TabIndex="13"></asp:TextBox>
                                <asp:HiddenField ID="hdnSeniorPastor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvSeniorPastor" runat="server" EnableClientScript="False" ControlToValidate="txtSeniorPastor" InitialValue="Pastor name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchStreet" runat="server" Width="500px" CssClass="floatLeftChurchAddress">
                                <asp:Label ID="lblChurchStreet" runat="server" Width="497px" Text="Address" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtChurchStreet" runat="server" Width="497px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Church address required!', hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);"
                                    onfocus="txt_got_Focus(this, 'Church address required!', hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchStreet);" TabIndex="14"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchStreet" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchStreet" runat="server" EnableClientScript="False" ControlToValidate="txtChurchStreet" InitialValue="Church address required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchZip" runat="server" CssClass="floatZipCode" DefaultButton="btnChurchZipCodeHidden">
                                <asp:Label ID="lblChurchZip" runat="server" Width="113px" Text="Zip Code" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchZip" runat="server" Width="113px" CssClass="TextBox"
                                    OnTextChanged="txtChurchZipCode_TextChanged"
                                    onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);"
                                    onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchZip);"
                                    TabIndex="15"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchZip" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:Button ID="btnChurchZipCodeHidden" runat="server" OnClick="btnChurchZipCodeHidden_Click" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvChurchZip" runat="server" EnableClientScript="False" ControlToValidate="txtChurchZip" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revChurchZip" runat="server" EnableClientScript="False" ControlToValidate="txtChurchZip" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchState" runat="server" CssClass="floatLeftState">
                                <asp:Label ID="lblChurchState" runat="server" Text="State" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchState" runat="server" Width="75px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'State required!', hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);"
                                    onfocus="txt_got_Focus(this, 'State required!', hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchState);" TabIndex="16"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchState" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchState" runat="server" EnableClientScript="False" ControlToValidate="txtChurchState" InitialValue="State required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchCity" runat="server" CssClass="floatLeftCity">
                                <asp:Label ID="lblChurchCity" runat="server" Width="283px" Text="City" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchCity" runat="server" Width="283px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Name of city required!', hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);" 
                                    onfocus="txt_got_Focus(this, 'Name of city required!', hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchCity);" TabIndex="17"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchCity" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchCity" runat="server" EnableClientScript="False" ControlToValidate="txtChurchCity" InitialValue="Name of city required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchTelephone" runat="server" Width="160px" CssClass="floatLeftChurchTelephone">
                                <asp:Label ID="lblChurchTelephone" runat="server" Text="Church Telephone" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtChurchTelephone" runat="server" Width="154px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);"
                                    onkeypress="OnMoveFocusToChkBillingAddrTelephoneOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchTelephone);" TabIndex="18"></asp:TextBox>
                                
                                <asp:HiddenField ID="hdnChurchTelephone" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneFontColor" runat="server" Value="" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchPhone" runat="server" EnableClientScript="False" ControlToValidate="txtChurchTelephone" InitialValue="Invalid phone number!"></asp:RequiredFieldValidator>
                            </asp:Panel>
                            <asp:Panel ID="pnlMembershipInfoPlaceHolder" runat="server" Width="495px" Height="31px" CssClass="floatLeft">
                            </asp:Panel>


                        </asp:Panel>

                        <br />

                        <asp:Panel ID="pnlFamilyInformation" runat="server" GroupingText="Family Information" CssClass="downLeftPanel">
                            <asp:Panel ID="pnlFamilyInfoSpouse" runat="server" GroupingText="Spouse" CssClass="floatSpouse">
                                <asp:Panel ID="pnlSpouseInfo" runat="server" Width="230px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseNameLabel" runat="server" Width="225px" Text="Name" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseName" runat="server" Width="225px" CssClass="SpouseName" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                                <asp:Panel ID="pnlSpouseDateOfBirth" runat="server" Width="110px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseDateOfBirthLabel" runat="server" Width="100px" Text="Date of Birth" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseDateOfBirth" runat="server" Width="100px" CssClass="SpouseDateOfBirth" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                                <asp:Panel ID="pnlSpouseStartDate" runat="server" Width="110px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseStartDateLabel" runat="server" Width="100px" Text="Start Date" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseStartDate" runat="server" Width="100px" CssClass="SpouseStartDate" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                            </asp:Panel>

                            <asp:Panel ID="pnlFamilyInfoChildren" runat="server" GroupingText="Children" CssClass="floatChildren">
                                <asp:Panel ID="pnlChildrenName" runat="server" Width="230px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenName" runat="server" Width="225px" CssClass="pnlLabelChildrenInfo" Text="Name" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName1" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName2" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName3" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName4" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName5" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName6" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName7" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName8" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName9" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName10" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="pnlChildrenDateOfBirth" runat="server" Width="110px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenDateOfBirth" runat="server" Width="100px" CssClass="pnlLabelChildrenInfo" Text="Date of Birth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth1" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth2" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth3" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth4" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth5" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth6" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth7" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth8" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth9" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth10" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>

                                </asp:Panel>
                                <asp:Panel ID="pnlChildrenStartDate" runat="server" Width="110px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenStartDate" runat="server" Width="100px" CssClass="pnlLabelChildrenInfo" Text="Start Date" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate1" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate2" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate3" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate4" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate5" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate6" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate7" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate8" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate9" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate10" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>

                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>


                        <asp:Panel ID="pnlGiftSendingInformation" runat="server" GroupingText="Gift Sending Information" CssClass="downRightPanel">
                            <asp:Panel ID="pnlPaymentMethod" runat="server" Width="500px" GroupingText="Method:" CssClass="floatLeftPaymentMethod">
                                <asp:RadioButton ID="rbCheck" runat="server" Width="100px" Text="Check" GroupName="PaymentMethod" Font-Size="Small" TabIndex="19" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbBankACH" runat="server" Width="100px" Text="Bank ACH" GroupName="PaymentMethod" Font-Size="Small" TabIndex="20" CssClass="floatLeftRadioButton"
                                    OnCheckedChanged="OnRBtnBankACH_Selected" />

                                <ajaxToolkit:ModalPopupExtender ID="mpePaymentBankACHDialogBox" runat="server" TargetControlID="btnPaymentBankACHHidden" PopupControlID="pnlPaymentBankACHDialogBox" DynamicServicePath="" BehaviorID="_content_mpePaymentBankACHDialogBox"></ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlPaymentBankACHDialogBox" runat="server" style="height: 250px; width: 250px;" CssClass="pnlBackGround">
                                        <div style="max-height: 150px; text-align:left; margin-top:30px; margin-left: 50px; margin-right: 20px; ">
                                            
                                            <br />
                                            <asp:Label ID="lblRoutingNumber" runat="server" Text="Bank Routing Number" ></asp:Label><br />
                                            <asp:TextBox ID="txtRoutingNumber" runat="server" ></asp:TextBox>
                                            <br /><br />
                                            <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number" ></asp:Label><br />
                                            <asp:TextBox ID="txtAccountNumber" runat="server" ></asp:TextBox>
                                            <br />
                                            <br /><br />
                                        </div>
                                    <asp:Button ID="btnPaymentBankACHOk" runat="server" Text="Ok" Width="80px" OnClick="btnPaymentBankACH_Save" />
                                    <asp:Button ID="btnPaymentBankACHCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnPaymentBankACH_Cancel" />
                                    </asp:Panel>
                                <asp:Button ID="btnPaymentBankACHHidden" runat="server" Text="Button" style="display: none;" />

                                <asp:RadioButton ID="rbCreditCard" runat="server" Width="100px" Text="Credit Card" GroupName="PaymentMethod" Font-Size="Small" TabIndex="21" CssClass="floatLeftRadioButton"
                                    OnCheckedChanged="OnRBtnCreditCard_Selected" />
                                <ajaxToolkit:ModalPopupExtender ID="mpePaymentCreditCardDialogBox" runat="server" TargetControlID="btnPaymentCreditCardHidden" PopupControlID="pnlPaymentCreditCardInfoDialogBox" DynamicServicePath="" BehaviorID="_content_mpePaymentCreditCardDialogBox"></ajaxToolkit:ModalPopupExtender>

                                    <asp:Panel ID="pnlPaymentCreditCardInfoDialogBox" runat="server" CssClass="pnlCreditCardInfoDialogBoxBackground">
                                        <div style="height: 20px; width: 420px; text-align: center; margin-top: 20px; font-size: medium; font-weight: bold;">
                                            Please enter your credit card information
                                        </div>
                                        
                                        <div style="height: 240px; width: 280px; text-align:left; margin-top: 20px; margin-left: 50px; margin-right: 20px; ">

                                            <br />
                                            <asp:Label ID="lblCreditCardNumber" runat="server" Text="Credit Card Number" ></asp:Label><br />
                                            <asp:TextBox ID="txtCreditCardNumber" runat="server"></asp:TextBox>
                                            <br /><br />
                                            <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date" ></asp:Label><br />
                                            <asp:TextBox ID="txtExpirationDate" runat="server"></asp:TextBox>
                                            <br /><br />
                                            <asp:Label ID="lblSecurityCode" runat="server" Text="Security Code" ></asp:Label><br />
                                            <asp:TextBox ID="txtSecurityCode" runat="server" ></asp:TextBox>
                                            <br /><br />
                                            <asp:Label ID="lblNameOnCreditCard" runat="server" Text="Name On The Credit Card" ></asp:Label><br />
                                            <asp:TextBox ID="txtNameOfCreditCard" runat="server"></asp:TextBox>
                                        </div>
                                    <asp:Button ID="btnPaymentCreditCardOk" runat="server" Text="Ok" style="width: 80px;" OnClick="btnPaymentCreditCardInfo_Save" />
                                    <asp:Button ID="btnPaymentCreditCardCancel" runat="server" Text="Cancel" style="width: 80px;" OnClick="btnPaymentCreditCardInfo_Cancel" />

                                    </asp:Panel>
                                <asp:Button ID="btnPaymentCreditCardHidden" runat="server" Text="Button" style="display: none;" />


                            </asp:Panel>
                            <asp:Panel ID="pnlFrequency" runat="server" Width="500px" GroupingText="Frequency" CssClass="floatLeftPaymentFrequency">
                                <asp:RadioButton ID="rbRecurring" runat="server" Width="100px" Text="Recurring" Font-Size="Small" GroupName="Frequency" TabIndex="22" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbOneTime" runat="server" Width="100px" Text="One Time" Font-Size="Small" GroupName="Frequency" TabIndex="23" CssClass="floatLeftRadioButton" />
                            </asp:Panel>
                            <asp:CheckBox ID="chkBillingAddress" runat="server" TabIndex="24" Text="If your billing address is DIFFERENT from the one you provided, please click this box." Font-Size="Small" CssClass="BillingAddressDifferent"
                                onkeypress="MoveFromChkBillingOnEnter(event, this);" OnCheckedChanged="chkBillingAddress_CheckedChanged" AutoPostBack="True" />

                            <asp:Panel ID="pnlBillingAddress" Visible="False" runat="server">
                                <asp:Panel ID="pnlBillingStreet" Visible="False" runat="server" Width="490px" CssClass="floatLeftBillingAddress">
                                    <asp:Label ID="lblBillingStreet" runat="server" Width="490px" Visible="False" CssClass="pnlLabel" Text="Address"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingStreet" runat="server" Width="490px" Visible="False" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing street required!', hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing street required!', hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingStreetAddress);" TabIndex="25"></asp:TextBox>

                                    <asp:HiddenField ID="hdnBillingStreetAddress" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetFontColor" runat="server" Value="" ClientIDMode="Static" />

                                    <asp:RequiredFieldValidator ID="rfvBillingStreet" runat="server" EnableClientScript="False" ControlToValidate="txtBillingStreet" InitialValue="Billing street required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingZipCode" runat="server" Visible="False" CssClass="floatZipCodeBillingAddress" DefaultButton="btnBillingZipCodeHidden">
                                    <asp:Label ID="lblBillingZipCode" runat="server" Visible="False" Width="115px" CssClass="pnlLabel" Text="Zip Code"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingZipCode" runat="server" CssClass="TextBox" Width="115px"
                                        onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);"
                                        onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);"
                                        OnTextChanged="txtBillingZipCode_TextChanged"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingZipCode);" TabIndex="26"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingZipCode" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeFontColor" runat="server" Value="" ClientIDMode="Static" />

                                    <asp:Button ID="btnBillingZipCodeHidden" runat="server" OnClick="btnBillingZipCodeHidden_Click" Style="display: none" />
                                    <asp:RequiredFieldValidator ID="rfvBillingZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtBillingZipCode" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revBillingZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtBillingZipCode" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingState" runat="server" Visible="False" CssClass="floatLeftStateBillingAddress">
                                    <asp:Label ID="lblBillingState" runat="server" Visible="False" CssClass="pnlLabel" Text="State"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingState" runat="server" Visible="False" Width="75px" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing state required!', hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing state required!', hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingState);" TabIndex="27"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingState" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateFontColor" runat="server" Value="" ClientIDMode="Static" />
                                    
                                    <asp:RequiredFieldValidator ID="rfvBillingState" runat="server" EnableClientScript="False" ControlToValidate="txtBillingState" InitialValue="Billing state required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingCity" runat="server" Visible="False" CssClass="floatLeftCityBillingAddress">
                                    <asp:Label ID="lblBillingCity" runat="server" Width="285px" Visible="False" CssClass="pnlLabel" Text="City"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingCity" runat="server" Visible="False" Width="277px" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing city required!', hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing city required!', hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingCity);" TabIndex="28"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingCity" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityBorderWidth" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityBorderColor" runat="server" Value="" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityFontColor" runat="server" Value="" ClientIDMode="Static" />

                                    <asp:RequiredFieldValidator ID="rfvBillingCity" runat="server" EnableClientScript="False" ControlToValidate="txtBillingCity" InitialValue="Billing city required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingSaveButton" runat="server" Visible="False" CssClass="pnlBillingAddressSaveButton">
                                    <asp:Button ID="btnBillingAddressSave" runat="server" Visible="False" Text="Save" CssClass="BillingAddressSaveButton" OnClick="btnBillingAddressSave_Click" UseSubmitBehavior="False" TabIndex="29" />
                                </asp:Panel>
                            </asp:Panel>

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressSaveConfirmation" runat="server" TargetControlID="btnBillingAddressSaveHidden" PopupControlID="pnlBillingAddressSaveConfirmation" BehaviorID="_content_mpeBillingAddressSaveConfirmation" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlBillingAddressSaveConfirmation" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address is saved successfully!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrSaveConfirmation" runat="server" Text="Ok" OnClick="btnBillingAddressSaveConfirmation_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressSaveHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressSaveFailure" runat="server" TargetControlID="btnBillingAddressSaveFailureHidden" PopupControlID="pnlBillingAddressSaveFailure" BehaviorID="_content_mpeBillingAddressSaveFailure" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlBillingAddressSaveFailure" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address saving failed!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrSavingFailure" runat="server" Text="Ok" OnClick="btnBillingAddrSavingFailure_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressSaveFailureHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressDeletionConfirmation" runat="server" TargetControlID="btnBillingAddressDeletionConfirmationHidden"
                                PopupControlID="pnlBillingAddressDeletionConfirmation" BehaviorID="_content_mpeBillingAddressDeletionConfirmation" DynamicServicePath="">
                            </ajaxToolkit:ModalPopupExtender>

                            <asp:Panel ID="pnlBillingAddressDeletionConfirmation" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address is deleted successfully!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrDeletionConfirmation" runat="server" Text="Ok" OnClick="btnBillingAddrDeletionConfirmation_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressDeletionConfirmationHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressDeletionFailed" runat="server" TargetControlID="btnBillingAddressDeletionFailedHidden"
                                PopupControlID="pnlBillingAddressDeletionFailed" BehaviorID="_content_mpeBillingAddressDeletionFailed" DynamicServicePath="">
                            </ajaxToolkit:ModalPopupExtender>

                            <asp:Panel ID="pnlBillingAddressDeletionFailed" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address deletion failed!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrDeletionFailed" runat="server" Text="Ok" OnClick="btnBillingAddrDeletionFailed_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressDeletionFailedHidden" runat="server" Text="Button" Style="display: none;" />

                            <asp:Panel ID="pnlReferredBy" runat="server" Width="500px" CssClass="floatLeft">
                                <asp:Label ID="lblReferredBy" runat="server" Width="100px" Text="Referred By" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlReferredBy" runat="server" Enabled="False" Height="28px" Width="200px" CssClass="DropDownList">
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlNotifyByContainer" runat="server" Width="500px" CssClass="NotifyByContainer">
                                <asp:Panel ID="pnlNotifyBy" runat="server" CssClass="floatLeftNotifyBy" GroupingText="Notify By:">
                                    <asp:CheckBox ID="chkEmail" runat="server" Width="60px" Text="Email" Font-Size="Small" CssClass="floatLeftCheckBox" TabIndex="30"
                                        onkeypress="MoveToChkPostalOnEnter(event, this);" />
                                    <asp:CheckBox ID="chkPostal" runat="server" Width="60px" Text="Postal" Font-Size="Small" CssClass="floatLeftCheckBox" TabIndex="31"
                                        onkeypress="MoveToEmailOnEnter(event, this);" />
                                </asp:Panel>
                            </asp:Panel>

                            <asp:Panel ID="pnlJoinMailing" runat="server" Width="180px" CssClass="floatLeftJoinMailing" GroupingText="Join Mailing:">
                                <asp:RadioButton ID="rbYesJoinMailing" runat="server" Width="60px" Text="Yes" Font-Size="Small" GroupName="JoinMailing" CssClass="floatLeftRadioButton" TabIndex="32" />
                                <asp:RadioButton ID="rbNoJoinMailing" runat="server" Width="60px" Text="No" Font-Size="Small" GroupName="JoinMailing" CssClass="floatLeftRadioButton" TabIndex="33" />
                            </asp:Panel>

                            <asp:Panel ID="pnlAllowMessages" runat="server" Width="180px" CssClass="floatLeftAllowMessages" GroupingText="Allow Messages">
                                <asp:RadioButton ID="rbYesAllowMessages" runat="server" Width="60px" Text="Yes" Font-Size="Small" GroupName="AllowMessages" CssClass="floatLeftRadioButton" TabIndex="34"
                                    onkeydown="MoveToEmailOnTab(event, this);"
                                    onkeypress="MoveToEmailOnEnter(event, this);" />
                                <asp:RadioButton ID="rbNoAllowMessages" runat="server" Width="60px" Text="No" Font-Size="Small" GroupName="AllowMessages" CssClass="floatLeftRadioButton" TabIndex="35"
                                    onkeydown="MoveToEmailOnTab(event, this);"
                                    onkeypress="MoveToEmailOnEnter(event, this);" />
                            </asp:Panel>

                            <asp:Panel ID="pnlGiftSendingInfoBottom" runat="server" Width="500px" CssClass="floatLeft" Height="21px"></asp:Panel>

                        </asp:Panel>
                        <asp:Panel ID="pnlMessageLabel" runat="server" CssClass="GeneralInfoLabelPanel">
                            <asp:Label ID="lblPersonalInfoUpdateMessage" runat="server" Text="Update Result: "></asp:Label><br />
                            <asp:Label ID="lblPaymentInfoUpdateMessage" runat="server" Text="Update Payment Result: "></asp:Label>
                        </asp:Panel>

                        <asp:Panel ID="pnlUpdateCancel" runat="server" Width="400px" CssClass="GeneralInfoButtonPanel">
<%--                            <asp:Button ID="btnCancel" runat="server" Width="80px" CssClass="GeneralInfoUpdateCancelButton" Text="Cancel" OnClientClick="btnCancel_ClientClick();"  UseSubmitBehavior="False" TabIndex="38"
                                onkeypress="MoveToEmailOnTab(event, this);" />--%>
                            <asp:Button ID="btnLogout" runat="server" Width="80px" CssClass="GeneralInfoUpdateCancelButton" Text="Log Out" OnClick="btnLogout_Click" UseSubmitBehavior="False" />
                            <asp:Button ID="btnUpdate" runat="server" Width="80px" CssClass="GeneralInfoUpdateCancelButton" Text="Update" OnClick="btnUpdate_Click"
                                UseSubmitBehavior="False" />

                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="mpeSucceeded" runat="server" PopupControlID="pnlConfirmation"
                            BackgroundCssClass="modalBackground" TargetControlID="btnHiddenSuccess" BehaviorID="_content_mpeSucceeded" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlConfirmation" runat="server" CssClass="pnlBackGround">
                            <br />
                            <br />
                            <span>Your information updated successfully!
                                <br />
                                <br />
                            </span>

                            <asp:Button ID="btnModalOk" runat="server" Text="Ok" OnClick="btnModalOk_Click" />
                        </asp:Panel>
                        <asp:Button ID="btnHiddenSuccess" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpeFailed" runat="server" TargetControlID="btnHiddenFailure" PopupControlID="pnlFailure"
                            BackgroundCssClass="modalBackground" BehaviorID="_content_mpeFailed" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlFailure" runat="server" CssClass="pnlBackGround">
                            <br />
                            <br />
                            <span>The update failed!
                                <br />
                                <br />
                            </span>
                            <asp:Button ID="btnModalFailure" runat="server" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnHiddenFailure" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpePageValidationFailed" runat="server" PopupControlID="pnlPageValidationFailed"
                            BackgroundCssClass="modalBackground" TargetControlID="btnPageValidationFailureHidden" ></ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlPageValidationFailed" runat="server" CssClass="pnlPageValidationFailure">
                            <br />
                            <asp:Label ID="lblPageValidationFailedMessage" runat="server" Text="Error: Please correct following (highlighted) fields." ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            <br /><br />
                            <div id="PageValidationFailed" runat="server" style="max-height: 150px; overflow: auto; text-align:left; margin-left: 30px; margin-right: 20px; border: 1px solid black; ">
                                <span style="width: 260px; border-width: medium; border-color: black;">
                                    <asp:Label ID="lblPageValidationFailure" runat="server" Text="" CssClass="pnlLabelChildrenInfo" ></asp:Label>                               
                                </span>
                            </div>
                            <br />
                            <asp:Button ID="btnModalPageValidationFailed" runat="server" Width="100px" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnPageValidationFailureHidden" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpePartialUpdateFailure" runat="server" PopupControlID="pnlPartialUpdateFailure"
                            BackgroundCssClass="modalBackground" TargetControlID="btnPartialUpdateFailureHidden" ></ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlPartialUpdateFailure" runat="server" CssClass="pnlPageValidationFailure">
                            <br />
                            <asp:Label ID="lblPartialUpdateFailure" runat="server" Text="Error: Partial update failure" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            <br /><br />
                            <div id="PartialUpdateFailure" runat="server" style="max-height: 150px; overflow: auto; text-align:left; margin-left: 30px; margin-right: 20px; border: 1px solid black; ">
                                <span style=""width: 260px; border-width: medium; border-color: black;">
                                    <asp:Label ID="lblPartialUpdateFailureMessage" runat="server" Text="" CssClass="pnlLabelChildrenInfo" ></asp:Label>
                                </span>
                            </div>
                            <br />
                            <asp:Button ID="btnModalPartialUpdateFailure" runat="server" Width="100px" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnPartialUpdateFailureHidden" runat="server" Text="Button" Style="display: none; " />

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnHealthHistory" runat="server" HeaderText="Health History" Height="500">
                    <ContentTemplate>
                        <div style="font-size: larger; font-weight: bold; text-align: center"> Health History: 건강 확인서 </div>
                        <asp:Table ID="tblHealthHistory" runat="server">
                            <asp:TableHeaderRow runat="server" style="font-size: small; font-weight: normal; text-align: center;">
                                <asp:TableHeaderCell>
                                    Has any person listed received medical attention and/or had surgery done in any hospital or similar institution?
                                    Please check below, and if answer to any of the listed is YES, explain it in the box below: 
                                    아래에 기재된 사항에 이상이 있거나 있었다면 (V)하시고, 그 내용을 아래칸에 기록하여 주시기 바랍니다.
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>

                        </asp:Table>


                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlAgreement" runat="server" HeaderText="Agreement" Height="500">
                    <ContentTemplate>
                        Agreement:
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlGiftHistory" runat="server" HeaderText="Gift History" Height="500">
                    <ContentTemplate>
                        Gift History:
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlNeedsProcessing" runat="server" HeaderText="Needs Processing" Height="500">
                    <ContentTemplate>
                        Needs Processing:
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlFiles" runat="server" HeaderText="Files" Height="500">
                    <ContentTemplate>
                        Files:
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlPassword" runat="server" HeaderText="Password" Height="500">
                    <ContentTemplate>
                        Password:
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>


    </div>
    </form>
</body>
</html>
