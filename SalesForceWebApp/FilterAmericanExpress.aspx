<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilterAmericanExpress.aspx.cs" Inherits="SalesForceWebApp.FilterAmericanExpress" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <style type="text/css">
    </style>

    <script type="text/javascript">

        function mouse_left_click(e, element) {
            var textbox = document.getElementById(element.id);
            if (textbox.value == '') {
                textbox.value = "XXXX-XXXX-XXXX-XXXX";
                textbox.setSelectionRange(0, 0);
            }
            else if (textbox.value == 'XXXX-XXXX-XXXX-XXXX') textbox.setSelectionRange(0, 0);
            else if (textbox.selectionStart > total_digits(textbox)) {
                if (credit_card_number_count(textbox.value) >= 0 && credit_card_number_count(textbox.value) <= 3)
                    textbox.setSelectionRange(credit_card_number_count(textbox.value), credit_card_number_count(textbox.value));
                if (credit_card_number_count(textbox.value) >= 4 && credit_card_number_count(textbox.value) <= 7)
                    textbox.setSelectionRange(credit_card_number_count(textbox.value) + 1, credit_card_number_count(textbox.value) + 1);
                if (credit_card_number_count(textbox.value) >= 8 && credit_card_number_count(textbox.value) <= 11)
                    textbox.setSelectionRange(credit_card_number_count(textbox.value) + 2, credit_card_number_count(textbox.value) + 2);
                if (credit_card_number_count(textbox.value) >= 12 && credit_card_number_count(textbox.value) <= 15)
                    textbox.setSelectionRange(credit_card_number_count(textbox.value) + 3, credit_card_number_count(textbox.value) + 3);
            }
        }

        function total_digits(textbox) {
            var counter = 0;

            for (var i = 0; i < textbox.value.length; i++) {
                if (textbox.value.charAt(i) != 'X') counter++;
            }

            return counter;
        }

        function credit_card_lost_focus(e, element) {
            var textbox = document.getElementById(element.id);

            if (textbox.value == 'XXXX-XXXX-XXXX-XXXX') textbox.value = '';
        }

        function accept_credit_card_number(e, element) {
            var keycode = (typeof e.which == "number") ? e.which : e.keyCode;
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            /////////////////////////////////////////////////////////////

            if (keycode >= 48 && keycode <= 57 && credit_card_number_count(textValue) < 16) {
                if ((textbox.selectionStart == 0) && (keycode == 51)) {
                    e.preventDefault();
                }
                else if (textbox.value.charAt(textbox.selectionStart) == 'X') {
                    if (credit_card_number_count(textValue) >= 0 && credit_card_number_count(textValue) <= 3) {
                        textbox.value = textbox.value.replace(textValue.charAt(textbox.selectionStart), String.fromCharCode(keycode));
                        if (credit_card_number_count(textValue) == 3) textbox.setSelectionRange(credit_card_number_count(textValue) + 2, credit_card_number_count(textValue) + 2);
                        else textbox.setSelectionRange(credit_card_number_count(textValue) + 1, credit_card_number_count(textValue) + 1);
                        e.preventDefault();
                    }
                    else if (credit_card_number_count(textValue) >= 4 && credit_card_number_count(textValue) <= 7) {
                        textbox.value = textbox.value.replace(textValue.charAt(textbox.selectionStart), String.fromCharCode(keycode));
                        if (credit_card_number_count(textValue) == 7) textbox.setSelectionRange(credit_card_number_count(textValue) + 3, credit_card_number_count(textValue) + 3);
                        else textbox.setSelectionRange(credit_card_number_count(textValue) + 2, credit_card_number_count(textValue) + 2);
                        e.preventDefault();
                    }
                    else if (credit_card_number_count(textValue) >= 8 && credit_card_number_count(textValue) <= 11) {
                        textbox.value = textbox.value.replace(textValue.charAt(textbox.selectionStart), String.fromCharCode(keycode));
                        if (credit_card_number_count(textValue) == 11) textbox.setSelectionRange(credit_card_number_count(textValue) + 4, credit_card_number_count(textValue) + 4);
                        else textbox.setSelectionRange(credit_card_number_count(textValue) + 3, credit_card_number_count(textValue) + 3);
                        e.preventDefault();
                    }
                    else if (credit_card_number_count(textValue) >= 12 && credit_card_number_count(textValue) <= 15) {
                        textbox.value = textbox.value.replace(textValue.charAt(textbox.selectionStart), String.fromCharCode(keycode));
                        if (credit_card_number_count(textValue) == 15) textbox.setSelectionRange(credit_card_number_count(textValue) + 5, credit_card_number_count(textValue) + 5);
                        else textbox.setSelectionRange(credit_card_number_count(textValue) + 4, credit_card_number_count(textValue) + 4);
                        e.preventDefault();
                    }
                }
                else {
                    if (textbox.selectionStart >= 0 && textbox.selectionStart <= 4) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_credit_card_number = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_credit_card_number += temp.charAt(i);
                        }

                        var left_side_of_caret = temp_credit_card_number.slice(0, pos);
                        var right_side_of_caret = temp_credit_card_number.slice(pos, temp_credit_card_number.length);
                        var temp_credit_card = '';

                        temp_credit_card = left_side_of_caret + String.fromCharCode(keycode) + right_side_of_caret.slice(0, right_side_of_caret.length);
                        var credit_card_number = '';

                        for (var i = 0; i < temp_credit_card.length; i++) {
                            if ((i % 4 == 0) && (i != 0) && (i < 16)) {
                                credit_card_number += '-';
                                credit_card_number += temp_credit_card.charAt(i);
                            }
                            else if (i < 16) credit_card_number += temp_credit_card.charAt(i);
                        }

                        textbox.value = credit_card_number;
                        if (original_caret_pos == 4) textbox.setSelectionRange(original_caret_pos + 2, original_caret_pos + 2);
                        else textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);

                        e.preventDefault();
                    }
                    else if (textbox.selectionStart >= 5 && textbox.selectionStart <= 9) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_credit_card_number = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_credit_card_number += temp.charAt(i);
                        }

                        pos--;

                        var left_side_of_caret = temp_credit_card_number.slice(0, pos);
                        var right_side_of_caret = temp_credit_card_number.slice(pos, temp_credit_card_number.length);
                        var temp_credit_card = '';

                        temp_credit_card = left_side_of_caret + String.fromCharCode(keycode) + right_side_of_caret.slice(0, right_side_of_caret.length);
                        var credit_card_number = '';

                        for (var i = 0; i < temp_credit_card.length; i++) {
                            if ((i % 4 == 0) && (i != 0) && (i < 16)) {
                                credit_card_number += '-';
                                credit_card_number += temp_credit_card.charAt(i);
                            }
                            else if (i < 16) credit_card_number += temp_credit_card.charAt(i);
                        }

                        textbox.value = credit_card_number;
                        if (original_caret_pos == 9) textbox.setSelectionRange(original_caret_pos + 2, original_caret_pos + 2);
                        else textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);

                        e.preventDefault();
                    }
                    else if (textbox.selectionStart >= 10 && textbox.selectionStart <= 14) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_credit_card_number = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_credit_card_number += temp.charAt(i);
                        }

                        pos = pos - 2;

                        var left_side_of_caret = temp_credit_card_number.slice(0, pos);
                        var right_side_of_caret = temp_credit_card_number.slice(pos, temp_credit_card_number.length);
                        var temp_credit_card = '';

                        temp_credit_card = left_side_of_caret + String.fromCharCode(keycode) + right_side_of_caret.slice(0, right_side_of_caret.length);
                        var credit_card_number = '';

                        for (var i = 0; i < temp_credit_card.length; i++) {
                            if ((i % 4 == 0) && (i != 0) && (i < 16)) {
                                credit_card_number += '-';
                                credit_card_number += temp_credit_card.charAt(i);
                            }
                            else if (i < 16) credit_card_number += temp_credit_card.charAt(i);
                        }

                        textbox.value = credit_card_number;
                        if (original_caret_pos == 14) textbox.setSelectionRange(original_caret_pos + 2, original_caret_pos + 2);
                        else textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);
                        e.preventDefault();
                    }
                    else if (textbox.selectionStart >= 15 && textbox.selectionStart <= 18) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_credit_card_number = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_credit_card_number += temp.charAt(i);
                        }

                        pos = pos - 3;

                        var left_side_of_caret = temp_credit_card_number.slice(0, pos);
                        var right_side_of_caret = temp_credit_card_number.slice(pos, temp_credit_card_number.length);
                        var temp_credit_card = '';

                        temp_credit_card = left_side_of_caret + String.fromCharCode(keycode) + right_side_of_caret.slice(0, right_side_of_caret.length);
                        var credit_card_number = '';

                        for (var i = 0; i < temp_credit_card.length; i++) {
                            if ((i % 4 == 0) && (i != 0) && (i < 16)) {
                                credit_card_number += '-';
                                credit_card_number += temp_credit_card.charAt(i);
                            }
                            else if (i < 16) credit_card_number += temp_credit_card.charAt(i);
                        }

                        textbox.value = credit_card_number;

                        textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);
                        e.preventDefault();
                    }
                }
            }
            else e.preventDefault();
        }

        function credit_card_number_count(text_value) {
            var count = 0;

            for (var i = 0; i < text_value.length; i++) {
                if (text_value.charCodeAt(i) >= 48 && text_value.charCodeAt(i) <= 57) {
                    count++;
                }
            }
            return count;
        }

        function credit_card_digits(numbers) {
            var credit_card_number = '';

            for (var i = 0; i < numbers.length; i++) {
                if (!isNaN(numbers.charAt(i))) credit_card_number += numbers.charAt(i);
            }

            return credit_card_number;
        }

        function filter_control_character_credit_card(e, element) {
            var keycode = (e.keyCode ? e.keyCode : e.which)
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            switch (keycode) {
                case 46:    // delete key pressed
                    if ((typeof textbox.selectionStart == "number") && (typeof textbox.selectionEnd == "number")) {
                        if (textbox.selectionStart != textbox.selectionEnd) {

                            // save the current caret position
                            var pos = textbox.selectionStart;
                            var left_side = textbox.value.slice(0, textbox.selectionStart);
                            var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);
                            var result = left_side + right_side;

                            var temp_credit_card_number = credit_card_digits(result, textbox);

                            for (var i = temp_credit_card_number.length; i < 16; i++) temp_credit_card_number += 'X';

                            var credit_card_number = '';

                            for (var i = 0; i < 16; i++) {
                                if ((i % 4 == 0) && (i != 0)) {
                                    credit_card_number += '-';
                                    credit_card_number += temp_credit_card_number.charAt(i);
                                }
                                else credit_card_number += temp_credit_card_number.charAt(i);
                            }

                            textbox.value = credit_card_number;
                            textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                        else {

                            var pos = textbox.selectionStart;
                            var dash_flag = false;
                            if (textbox.value.charAt(pos) == '-') {
                                pos++;
                                dash_flag = true;
                            }
                            // save the left side of caret
                            var credit_card_number = textbox.value.slice(0, pos);

                            for (var i = pos + 1; i < 19; i++) {
                                if (textbox.value.charAt(i) == '-') {
                                    i++;
                                    var tmp = textbox.value.charAt(i) + '-';
                                    credit_card_number += tmp;
                                }
                                else {
                                    var tmp = textbox.value.charAt(i);
                                    credit_card_number += tmp;
                                }
                            }

                            textbox.value = credit_card_number + 'X';

                            if (dash_flag) textbox.setSelectionRange(pos - 1, pos - 1);
                            else textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                    }
                    break;
                case 8:     // back space key pressed
                    if ((typeof textbox.selectionStart == "number") && (typeof textbox.selectionEnd == "number")) {
                        if ((textbox.selectionStart == textbox.selectionEnd) && (textbox.selectionStart != 0)) {

                            var pos = textbox.selectionStart;

                            if (textbox.value.charAt(pos - 1) == '-') {
                                var right_side = textbox.value.slice(pos + 1, 19);
                                var credit_card_number = textbox.value.slice(0, pos - 2) + textbox.value.charAt(pos) + '-';

                                for (var i = textbox.selectionStart + 1; i < 19; i++) {
                                    if (textbox.value.charAt(i) == '-') {
                                        i++;
                                        var tmp = textbox.value.charAt(i) + '-';
                                        credit_card_number += tmp;
                                    }
                                    else {
                                        var tmp = textbox.value.charAt(i);
                                        credit_card_number += tmp;
                                    }
                                }

                                textbox.value = credit_card_number + 'X';
                                textbox.setSelectionRange(pos - 2, pos - 2);
                            }
                            else {
                                var credit_card_number = textbox.value.slice(0, pos - 1);
                                for (var i = textbox.selectionStart; i < 19; i++) {
                                    if (textbox.value.charAt(i) == '-') {
                                        i++;
                                        var tmp = textbox.value.charAt(i) + '-';
                                        credit_card_number += tmp;
                                    }
                                    else {
                                        var tmp = textbox.value.charAt(i);
                                        credit_card_number += tmp;
                                    }
                                }
                                textbox.value = credit_card_number + 'X';
                                textbox.setSelectionRange(pos - 1, pos - 1);
                            }
                            e.preventDefault();
                        }
                        else {

                            var start = textbox.selectionStart;
                            var end = textbox.selectionEnd;

                            var pos = textbox.selectionStart;
                            var left_side = textbox.value.slice(0, textbox.selectionStart);
                            var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);
                            var result = left_side + right_side;
                            var temp_credit_card_number = credit_card_digits(result, textbox);

                            for (var i = temp_credit_card_number.length; i < 16; i++) temp_credit_card_number += 'X';

                            var credit_card_number = '';

                            for (var i = 0; i < 16; i++) {
                                if ((i % 4 == 0) && (i != 0)) {
                                    credit_card_number += '-';
                                    credit_card_number += temp_credit_card_number.charAt(i);
                                }
                                else credit_card_number += temp_credit_card_number.charAt(i);
                            }

                            textbox.value = credit_card_number;
                            textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        // Javascript functions for social security number
        function mouse_left_click_ssn(e, element) {
            var textbox = document.getElementById(element.id);
            if (textbox.value == '') {
                textbox.value = 'XXX-XX-XXXX';
                textbox.setSelectionRange(0, 0);
            }
            else if (textbox.value == 'XXX-XX-XXXX') textbox.setSelectionRange(0, 0);
            else if (textbox.selectionStart > total_digits(textbox)) {
                if (social_security_number_count(textbox.value) >= 0 && social_security_number_count(textbox.value) <= 2)
                    textbox.setSelectionRange(social_security_number_count(textbox.value), social_security_number_count(textbox.value));
                if (social_security_number_count(textbox.value) >= 3 && social_security_number_count(textbox.value) <= 4)
                    textbox.setSelectionRange(social_security_number_count(textbox.value) + 1, social_security_number_count(textbox.value) + 1);
                if (social_security_number_count(textbox.value) >= 5 && social_security_number_count(textbox.value) <= 8)
                    textbox.setSelectionRange(social_security_number_count(textbox.value) + 2, social_security_number_count(textbox.value) + 2);
            }
        }

        function ssn_lost_focus(e, element) {
            var textbox = document.getElementById(element.id);

            if (textbox.value == 'XXX-XX-XXXX') textbox.value = '';
        }

        function accept_social_security_number(e, element) {
            var keycode = (typeof e.which == "number") ? e.which : e.keyCode;
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            if (keycode >= 48 && keycode <= 57 && social_security_number_count(textValue) < 9) {
                if (textbox.value.charAt(textbox.selectionStart) == 'X') {
                    if (social_security_number_count(textValue) >= 0 && social_security_number_count(textValue) <= 2) {
                        textbox.value = textbox.value.replace(textValue.charAt(social_security_number_count(textValue)), String.fromCharCode(keycode));
                        if (social_security_number_count(textValue) == 2) textbox.setSelectionRange(social_security_number_count(textValue) + 2, social_security_number_count(textValue) + 2);
                        else textbox.setSelectionRange(social_security_number_count(textValue) + 1, social_security_number_count(textValue) + 1);
                        e.preventDefault();
                    }
                    else if (social_security_number_count(textValue) >= 3 && social_security_number_count(textValue) <= 4) {
                        textbox.value = textbox.value.replace(textValue.charAt(social_security_number_count(textValue) + 1), String.fromCharCode(keycode));
                        if (social_security_number_count(textValue) == 4) textbox.setSelectionRange(social_security_number_count(textValue) + 3, social_security_number_count(textValue) + 3);
                        else textbox.setSelectionRange(social_security_number_count(textValue) + 2, social_security_number_count(textValue) + 2);
                        e.preventDefault();
                    }
                    else if (social_security_number_count(textValue) >= 5 && social_security_number_count(textValue) <= 8) {
                        textbox.value = textbox.value.replace(textValue.charAt(social_security_number_count(textValue) + 2), String.fromCharCode(keycode));
                        if (social_security_number_count(textValue) == 8) textbox.setSelectionRange(social_security_number_count(textValue) + 4, social_security_number_count(textValue) + 4);
                        else textbox.setSelectionRange(social_security_number_count(textValue) + 3, social_security_number_count(textValue) + 3);
                        e.preventDefault();
                    }
                }
                else {
                    if (textbox.selectionStart >= 0 && textbox.selectionStart <= 3) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_ssn = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_ssn += temp.charAt(i);
                        }

                        var left_side = temp_ssn.slice(0, pos);
                        var right_side = temp_ssn.slice(pos, temp_ssn.length);

                        var temp_ssn_number = left_side + String.fromCharCode(keycode) + right_side.slice(0, right_side.length - 1);

                        var ssn = '';

                        for (var i = 0; i < temp_ssn_number.length; i++) {
                            if (((i == 3) || (i == 5)) && (i < 10)) {
                                ssn += '-';
                                ssn += temp_ssn_number.charAt(i);
                            }
                            else if (i < 10) ssn += temp_ssn_number.charAt(i);
                        }
                        textbox.value = ssn;

                        if (original_caret_pos == 3) textbox.setSelectionRange(original_caret_pos + 2, original_caret_pos + 2);
                        else textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);

                        e.preventDefault();

                    }
                    else if (textbox.selectionStart >= 4 && textbox.selectionStart <= 6) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_ssn = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_ssn += temp.charAt(i);
                        }

                        pos--;

                        var left_side = temp_ssn.slice(0, pos);
                        var right_side = temp_ssn.slice(pos, temp_ssn.length);

                        var temp_ssn_number = left_side + String.fromCharCode(keycode) + right_side.slice(0, right_side.length - 1);

                        var ssn = '';

                        for (var i = 0; i < temp_ssn_number.length; i++) {
                            if (((i == 3) || (i == 5)) && (i < 10)) {
                                ssn += '-';
                                ssn += temp_ssn_number.charAt(i);
                            }
                            else if (i < 10) ssn += temp_ssn_number.charAt(i);
                        }
                        textbox.value = ssn;

                        if (original_caret_pos == 6) textbox.setSelectionRange(original_caret_pos + 2, original_caret_pos + 2);
                        else textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);

                        e.preventDefault();
                    }
                    else if (textbox.selectionStart >= 7 && textbox.selectionStart <= 10) {
                        var pos = textbox.selectionStart;
                        var original_caret_pos = pos;

                        var temp = textbox.value;
                        var temp_ssn = '';

                        for (var i = 0; i < temp.length; i++) {
                            if (temp.charAt(i) != '-') temp_ssn += temp.charAt(i);
                        }

                        pos = pos - 2;

                        var left_side = temp_ssn.slice(0, pos);
                        var right_side = temp_ssn.slice(pos, temp_ssn.length);

                        var temp_ssn_number = left_side + String.fromCharCode(keycode) + right_side.slice(0, right_side.length - 1);

                        var ssn = '';

                        for (var i = 0; i < temp_ssn_number.length; i++) {
                            if (((i == 3) || (i == 5)) && (i < 10)) {
                                ssn += '-';
                                ssn += temp_ssn_number.charAt(i);
                            }
                            else if (i < 10) ssn += temp_ssn_number.charAt(i);
                        }
                        textbox.value = ssn;
                        textbox.setSelectionRange(original_caret_pos + 1, original_caret_pos + 1);

                        e.preventDefault();
                    }
                }
            }
            else e.preventDefault();
        }

        function social_security_number_count(text_value) {
            var count = 0;

            for (var i = 0; i < text_value.length; i++) {
                if (text_value.charCodeAt(i) >= 48 && text_value.charCodeAt(i) <= 57) {
                    count++;
                }
            }
            return count;
        }

        function ssn_digits(numbers) {
            var ssn = '';

            for (var i = 0; i < numbers.length; i++) {
                if (!isNaN(numbers.charAt(i))) ssn += numbers.charAt(i);
            }

            return ssn;
        }

        function filter_control_character_ssn(e, element) {
            var keycode = (e.keyCode ? e.keyCode : e.which)
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            switch (keycode) {
                case 46:    // delete key pressed
                    // save the current caret position
                    if ((typeof textbox.selectionStart == "number") && (typeof textbox.selectionEnd == "number")) {
                        if (textbox.selectionStart == textbox.selectionEnd) {
                            var pos = textbox.selectionStart;
                            var dash_flag = false;
                            if (textbox.value.charAt(pos) == '-') {
                                pos++;
                                dash_flag = true;
                            }
                            // save the left side of caret
                            //var credit_card_number = textbox.value.slice(0, pos);
                            var ssn = textbox.value.slice(0, pos);

                            for (var i = pos + 1; i < 11; i++) {
                                if (textbox.value.charAt(i) == '-') {
                                    i++;
                                    var tmp = textbox.value.charAt(i) + '-';
                                    ssn += tmp;
                                }
                                else {
                                    var tmp = textbox.value.charAt(i);
                                    ssn += tmp;
                                }
                            }

                            textbox.value = ssn + 'X';

                            if (dash_flag) textbox.setSelectionRange(pos - 1, pos - 1);
                            else textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                        else {
                            // save the current caret position
                            var pos = textbox.selectionStart;
                            var left_side = textbox.value.slice(0, textbox.selectionStart);
                            var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);
                            var result = left_side + right_side;

                            var temp_ssn = ssn_digits(result);

                            for (var i = temp_ssn.length; i < 9; i++) temp_ssn += 'X';

                            var ssn = '';

                            for (var i = 0; i < 9; i++) {
                                if ((i == 3) || (i == 5)) {
                                    ssn += '-';
                                    ssn += temp_ssn.charAt(i);
                                }
                                else ssn += temp_ssn.charAt(i);
                            }

                            textbox.value = ssn;
                            textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                    }
                    break;
                case 8:     // backspace key pressed
                    if ((typeof textbox.selectionStart == "number") && (typeof textbox.selectionEnd == "number")) {
                        if ((textbox.selectionStart == textbox.selectionEnd) && (textbox.selectionStart != 0)) {
                            var pos = textbox.selectionStart;

                            if (textbox.value.charAt(pos - 1) == '-') {
                                var right_side = textbox.value.slice(pos + 1, 11);
                                var ssn = textbox.value.slice(0, pos - 2) + textbox.value.charAt(pos) + '-';

                                for (var i = textbox.selectionStart + 1; i < 11; i++) {
                                    if (textbox.value.charAt(i) == '-') {
                                        i++;
                                        var tmp = textbox.value.charAt(i) + '-';
                                        ssn += tmp;
                                    }
                                    else {
                                        var tmp = textbox.value.charAt(i);
                                        ssn += tmp;
                                    }
                                }

                                textbox.value = ssn + 'X';
                                textbox.setSelectionRange(pos - 2, pos - 2);

                                e.preventDefault();
                            }
                            else {
                                var ssn = textbox.value.slice(0, pos - 1);
                                for (var i = textbox.selectionStart; i < 11; i++) {
                                    if (textbox.value.charAt(i) == '-') {
                                        i++;
                                        var tmp = textbox.value.charAt(i) + '-';
                                        ssn += tmp;
                                    }
                                    else {
                                        var tmp = textbox.value.charAt(i);
                                        ssn += tmp;
                                    }
                                }
                                textbox.value = ssn + 'X';
                                textbox.setSelectionRange(pos - 1, pos - 1);

                                e.preventDefault();
                            }
                        }
                        else {
                            // save the current caret position
                            var pos = textbox.selectionStart;
                            var left_side = textbox.value.slice(0, textbox.selectionStart);
                            var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);
                            var result = left_side + right_side;

                            var temp_ssn = ssn_digits(result);

                            for (var i = temp_ssn.length; i < 9; i++) temp_ssn += 'X';

                            var ssn = '';

                            for (var i = 0; i < 9; i++) {
                                if ((i == 3) || (i == 5)) {
                                    ssn += '-';
                                    ssn += temp_ssn.charAt(i);
                                }
                                else ssn += temp_ssn.charAt(i);
                            }

                            textbox.value = ssn;
                            textbox.setSelectionRange(pos, pos);

                            e.preventDefault();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        function mouse_left_click_phone(e, element) {
            var textbox = document.getElementById(element.id);

            if (textbox.value == '') {
                textbox.value = '(___)___-____';
                textbox.setSelectionRange(1, 1);
            }
            else if (textbox.value == '(___)___-____') {
                textbox.setSelectionRange(1, 1);
            }
            else if (textbox.selectionStart == 0) textbox.setSelectionRange(1, 1);
        }

        function phone_number_lost_focus(e, element) {
            var textbox = document.getElementById(element.id);
            var regex_phone_number = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/;

            if (!regex_phone_number.test(textbox.value)) textbox.value = '';
        }

        function phone_number_count(text_value) {
            var count = 0;

            for (var i = 0; i < text_value.length; i++) {
                if (text_value.charCodeAt(i) >= 48 && text_value.charCodeAt(i) <= 57) {
                    count++;
                }
            }
            return count;
        }

        function number_of_digits_phone_number(textbox) {
            var counter = 0;

            for (var i = 0; i < textbox.value.length; i++) {
                if (!isNaN(textbox.value.charAt(i))) counter++;
            }

            return counter;
        }

        function digits_phone(number) {
            var digits = '';

            for (var i = 0; i < number.length; i++) {
                if (!isNaN(number.charAt(i))) digits += number.charAt(i)
            }

            return digits;
        }

        function accept_phone_number(e, element) {
            var keycode = (typeof e.which == "number") ? e.which : e.keyCode;
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            if ((keycode >= 48 && keycode <= 57) && (number_of_digits_phone_number(textbox) <= 10)) {
                if (!isNaN(textbox.value.charAt(textbox.selectionStart))) {
                    var pos = textbox.selectionStart;

                    var left_side = textbox.value.slice(0, pos);
                    var right_side = textbox.value.slice(pos, textbox.value.length);

                    var digits_left = digits_phone(left_side);
                    var digits_right = digits_phone(right_side);

                    var temp_phone = digits_left + String.fromCharCode(keycode) + digits_right;

                    for (var i = temp_phone.length; i < 10; i++) temp_phone += '_';
                    var phone = '';

                    for (var i = 0; i <= 9; i++) {
                        if (i == 0) phone += '(';
                        if (i == 3) phone += ')';
                        if (i == 6) phone += '-';
                        phone += temp_phone.charAt(i);
                    }

                    textbox.value = phone;
                    textbox.setSelectionRange(pos + 1, pos + 1);

                    e.preventDefault();
                }
                else if (!isNaN(textbox.value.charAt(textbox.selectionStart + 1))) {
                    var pos = textbox.selectionStart + 1;

                    var left_side = textbox.value.slice(0, pos);
                    var right_side = textbox.value.slice(pos, textbox.value.length);

                    var digits_left = digits_phone(left_side);
                    var digits_right = digits_phone(right_side);

                    var temp_phone = digits_left + String.fromCharCode(keycode) + digits_right;

                    for (var i = temp_phone.length; i < 10; i++) temp_phone += '_';
                    var phone = '';

                    for (var i = 0; i <= 9; i++) {
                        if (i == 0) phone += '(';
                        if (i == 3) phone += ')';
                        if (i == 6) phone += '-';
                        phone += temp_phone.charAt(i);
                    }

                    textbox.value = phone;
                    textbox.setSelectionRange(pos + 1, pos + 1);

                    e.preventDefault();
                }
                else {
                    var temp_phone = '';
                    var empty_index = 0;
                    var number_index = 0;

                    for (var i = textbox.value.length; i >= 0; i--) {
                        if (isNaN(textbox.value.charAt(i))) {
                            empty_index++;
                        }
                    }

                    var i = 14 - empty_index;
                    if ((i >= 1) && (i <= 3)) {
                        textbox.value = textbox.value.slice(0, i) + String.fromCharCode(keycode) + textbox.value.slice(i + 1, 14);
                        if (i < 3) textbox.setSelectionRange(i + 1, i + 1);
                        else if (i == 3) textbox.setSelectionRange(i + 2, i + 2);
                        e.preventDefault();
                    }
                    else if ((i >= 4) && (i <= 6)) {
                        textbox.value = textbox.value.slice(0, i + 1) + String.fromCharCode(keycode) + textbox.value.slice(i + 2, 14);
                        if (i < 6) textbox.setSelectionRange(i + 2, i + 2);
                        else if (i == 6) textbox.setSelectionRange(i + 3, i + 3);
                        e.preventDefault();
                    }
                    else if ((i >= 7) && (i <= 10)) {
                        textbox.value = textbox.value.slice(0, i + 2) + String.fromCharCode(keycode) + textbox.value.slice(i + 3, 14);
                        if (i < 10) textbox.setSelectionRange(i + 3, i + 3);
                        e.preventDefault();
                    }
                    e.preventDefault();
                }
            } else e.preventDefault();
        }

        function filter_control_character_phone_number(e, element) {
            var keycode = (e.keyCode ? e.keyCode : e.which)
            var textbox = document.getElementById(element.id);

            switch (keycode) {
                case 36:    // home key pressed
                    textbox.setSelectionRange(1, 1);
                    e.preventDefault();
                    break;
                case 35:    // end key pressed
                    textbox.setSelectionRange(13, 13)
                    break;
                case 37:    // left arrow key pressed
                    if (textbox.selectionStart == 1) e.preventDefault();
                    break;
                case 46:    // delete key pressed
                    if ((textbox.selectionStart == 13) || ((textbox.selectionStart == 1) && ((number_of_digits_phone_number(textbox)) == 0))) {
                        e.preventDefault();
                        break;
                    }
                    if (textbox.selectionStart == textbox.selectionEnd) {
                        var pos = textbox.selectionStart;
                        var number_of_digits = number_of_digits_phone_number(textbox);

                        if ((pos >= 9) && (pos <= 13)) {
                            var left_of_caret = textbox.value.slice(0, pos);
                            var right_of_caret = textbox.value.slice(pos + 1, textbox.value.length) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos, pos);
                            e.preventDefault();
                        }
                        else if (pos == 8) {
                            var left_of_caret = textbox.value.slice(0, pos);
                            var right_of_caret = textbox.value.slice(pos + 2, textbox.value.length) + '_';

                            textbox.value = left_of_caret + '-' + right_of_caret;
                            textbox.setSelectionRange(pos, pos);
                            e.preventDefault();
                        }
                        else if ((pos >= 5) && (pos <= 7)) {
                            var left_of_caret = textbox.value.slice(0, pos);
                            var right_of_caret = textbox.value.slice(pos + 1, 8) +
                                                 textbox.value.charAt(9) + '-' +
                                                 textbox.value.slice(10, textbox.value.length) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos, pos);
                            e.preventDefault();
                        }
                        else if (pos == 4) {
                            var left_of_caret = textbox.value.slice(0, pos + 1);
                            var right_of_caret = textbox.value.slice(pos + 2, pos + 4) +
                                                 textbox.value.charAt(pos + 5) + '-' +
                                                 textbox.value.slice(pos + 6, textbox.value.length) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos, pos);
                            e.preventDefault();
                        }
                        else if ((pos >= 1) && (pos <= 3)) {
                            var left_of_caret = textbox.value.slice(0, pos);
                            var right_of_caret = textbox.value.slice(pos + 1, 4) +
                                                 textbox.value.charAt(5) + ')' +
                                                 textbox.value.slice(6, 8) +
                                                 textbox.value.charAt(9) + '-' +
                                                 textbox.value.slice(10, textbox.value.length) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos, pos);
                            e.preventDefault();
                        }
                    }
                    else {
                        var pos = textbox.selectionStart;

                        var left_side = textbox.value.slice(0, textbox.selectionStart);
                        var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);

                        var result = left_side + right_side;
                        var temp_phone = digits_phone(result);

                        for (var i = temp_phone.length; i < 10; i++) temp_phone += '_';

                        var phone = '';

                        for (var i = 0; i <= 10; i++) {
                            if (i == 0) phone += '(';
                            if (i == 3) phone += ')';
                            if (i == 6) phone += '-';
                            phone += temp_phone.charAt(i);
                        }

                        textbox.value = phone;
                        if (pos == 0) textbox.setSelectionRange(1, 1);
                        else textbox.setSelectionRange(pos, pos);

                        e.preventDefault();

                    }
                    break;
                case 8:     // backspace key pressed
                    if (textbox.selectionStart == 1) {
                        e.preventDefault();
                        break;
                    }

                    if (textbox.selectionStart == textbox.selectionEnd) {
                        var pos = textbox.selectionStart;
                        var number_of_digits = number_of_digits_phone_number(textbox);

                        if ((pos == 13) && ((number_of_digits_phone_number(textbox)) == 0)) {
                            textbox.setSelectionRange(1, 1);
                            e.preventDefault();
                            break;
                        }
                        if ((pos > 9) && (pos <= 13)) {
                            var left_of_caret = textbox.value.slice(0, textbox.selectionStart - 1);
                            var right_of_caret = textbox.value.slice(textbox.selectionStart, textbox.value.length);
                            textbox.value = left_of_caret + right_of_caret + '_';
                            textbox.setSelectionRange(pos - 1, pos - 1);
                            e.preventDefault();
                        }
                        else if (pos == 9) {
                            var left_of_caret = textbox.value.slice(0, textbox.selectionStart - 2);
                            var number_at_pos = textbox.value.charAt(pos);
                            var right_of_caret = textbox.value.slice(pos + 1, textbox.value.length);
                            textbox.value = left_of_caret + number_at_pos + '-' + right_of_caret + '_';
                            textbox.setSelectionRange(pos - 2, pos - 2);
                            e.preventDefault();
                        }
                        else if ((pos > 5) && (pos <= 8)) {
                            var left_of_caret = textbox.value.slice(0, textbox.selectionStart - 1);
                            var right_of_caret = textbox.value.slice(pos, 8) + textbox.value.charAt(9) + '-' + textbox.value.slice(10, textbox.value.length) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos - 1, pos - 1);
                            e.preventDefault();
                        }
                        else if (pos == 5) {
                            var left_of_caret = textbox.value.slice(0, pos - 2);
                            var right_of_caret = textbox.value.charAt(pos) + ')' +
                                                    textbox.value.slice(pos + 1, 8) + textbox.value.charAt(9) + '-' +
                                                    textbox.value.slice(10, textbox.value.length + 1) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos - 2, pos - 2);
                            e.preventDefault();
                        }
                        else if ((pos > 1) && (pos <= 4)) {
                            var left_of_caret = textbox.value.slice(0, pos - 1);
                            var right_of_caret = textbox.value.slice(pos, 4) + textbox.value.charAt(5) + ')' + textbox.value.slice(6, 8) +
                                                 textbox.value.charAt(9) + '-' +
                                                 textbox.value.slice(10, textbox.value.length + 1) + '_';

                            textbox.value = left_of_caret + right_of_caret;
                            textbox.setSelectionRange(pos - 1, pos - 1);
                            e.preventDefault();
                        }
                    }
                    else {
                        var pos = textbox.selectionStart;

                        var left_side = textbox.value.slice(0, textbox.selectionStart);
                        var right_side = textbox.value.slice(textbox.selectionEnd, textbox.value.length);

                        var result = left_side + right_side;
                        var temp_phone = digits_phone(result);

                        for (var i = temp_phone.length; i < 10; i++) temp_phone += '_';

                        var phone = '';

                        for (var i = 0; i <= 10; i++) {
                            if (i == 0) phone += '(';
                            if (i == 3) phone += ')';
                            if (i == 6) phone += '-';
                            phone += temp_phone.charAt(i);
                        }

                        textbox.value = phone;
                        if (pos == 0) textbox.setSelectionRange(1, 1);
                        else textbox.setSelectionRange(pos, pos);

                        e.preventDefault();

                    }
                    break;
                default:
                    break;
            }
        }

        function get_digits_from_phone_number(e, element) {
            var keycode = (typeof e.which == "number") ? e.which : e.keyCode;
            var textbox = document.getElementById(element.id);
            var textValue = textbox.value;

            var phone_number = '';

            for (var i = 0; i <= textbox.value.length; i++) {
                if (!isNaN(textbox.value.charAt(i))) phone_number += textbox.value.charAt(i);
            }

            return phone_number;
        }

        function accept_cvv(e, element) {
            var keycode = (typeof e.which == "number") ? e.which : e.keyCode;
            var textbox = document.getElementById(element.id);

            if (!isNaN(String.fromCharCode(keycode)) && textbox.value.length < 3) textbox.value += String.fromCharCode(keycode);
            e.preventDefault();
        }

    </script>
    <form id="form1" runat="server">

        <div>
            <asp:Label ID="lblCreditCardNumber" runat="server" Text="Credit Card Number"></asp:Label><br />
            <asp:TextBox ID="txtCreditCardNumber" runat="server" onclick="mouse_left_click(event, this);" onfocus="mouse_left_click(event, this);"
                onfocusout="credit_card_lost_focus(event, this)" onkeypress="accept_credit_card_number(event, this);" onkeydown="filter_control_character_credit_card(event, this);"></asp:TextBox>
        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="lblSocialSecurityNumber" runat="server" Text="Social Security Number"></asp:Label><br />
            <asp:TextBox ID="txtSocialSecurityNumber" runat="server" onclick="mouse_left_click_ssn(event, this);" onfocus="mouse_left_click_ssn(event, this);"
                onfocusout="ssn_lost_focus(event, this)" onkeypress="accept_social_security_number(event, this);" onkeydown="filter_control_character_ssn(event, this);"></asp:TextBox>
        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number"></asp:Label><br />
            <asp:TextBox ID="txtPhoneNumber" runat="server" onclick="mouse_left_click_phone(event, this);" onfocus="mouse_left_click_phone(event, this);"
                onfocusout="phone_number_lost_focus(event, this)" onkeypress="accept_phone_number(event, this);" onkeydown="filter_control_character_phone_number(event, this);"></asp:TextBox>
        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="lblCVV" runat="server" Text="CVV" ></asp:Label><br />
            <asp:TextBox ID="txtCVV" runat="server" onkeypress="accept_cvv(event, this);" ></asp:TextBox>
        </div>

    </form>
</body>
</html>
