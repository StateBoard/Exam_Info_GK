﻿var base_url = 'http://localhost/';
$(document).ready(function () {
    $("input:text,form").attr("autocomplete", "off");
})
function Success_Alert(MSG) {
    Swal.fire({
        icon: 'success',
        title: MSG,
        showConfirmButton: true
    })
}

function Error_Alert(MSG) {

    Swal.fire({
        icon: 'error',
        title: MSG,
        showConfirmButton: true
    })
}
function Confirmation_Before_Submit() {



}
function Showloader() {
    $(".loadertap2").show();
    $(".loadertap1").show();
}
function Hideloader() {
    $(".loadertap2").hide();
    $(".loadertap1").hide();
}
function Delete_Record(ID, Client_ID, Function_Name) {

    $.ajax({

        type: 'POST',
        url: Function_Name,
        autoUpload: true,
        dataType: 'json',
        data: {
            Client_ID: Client_ID,
            Delete_ID: ID
        },
        beforeSend: function () {
            Showloader();
        },
        complete: function () {
            Hideloader();
        },
        success: function (data) {
            if (data.Result == "Success") {
                Success_Alert(data.message);
            }
            else {
                Error_Alert(data.message);
            }
        },
        error: function (e1, e2, e3) {

        }
    });
}

function Get_State(Client_ID, ID) {
    $("#" + ID).empty();
    $.ajax({

        type: 'POST',
        url: '/Master/Get_States',
        autoUpload: true,
        dataType: 'json',
        data: { Client_ID: Client_ID },

        beforeSend: function () {
            Showloader();
        },
        complete: function () {
            Hideloader();
        },
        success: function (Accnos) {
            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select State--" }));
            $.each(Accnos, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.State_ID + '">' + Accno.State_Name + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });
}

function Get_District(Division_Code, ID) {

    $("#" + ID).empty();
    $.ajax({
        type: 'POST',
        url: '/Report/Get_District',
        autoUpload: true,
        dataType: 'json',
        data: {
            Division_Code: Division_Code
        },
        
        success: function (Accnos) {

            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select District--" }));
            $.each(Accnos.Responce, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.DISTRICT_CODE + '">' + Accno.DISTRICT_NAME + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });

}
function Get_Taluka(Division_Code, District_Code, ID) {

    $("#" + ID).empty();
    $.ajax({
        type: 'POST',
        url: '/Report/Get_Taluka',
        autoUpload: true,
        dataType: 'json',
        data: {
            Division_Code: Division_Code,
            District_Code: District_Code
        },
        success: function (Accnos) {
            console.log(Accnos.Responce);
            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select Taluka--" }));
            $.each(Accnos.Responce, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.TALUKA_CODE + '">' + Accno.TALUKA_NAME + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });

}
function Get_Center(Division_Code, ID) {

    $("#" + ID).empty();
    $.ajax({
        type: 'POST',
        url: '/Registration/Get_Center',
        autoUpload: true,
        dataType: 'json',
        data: {
            Division_Code: Division_Code
        },
        beforeSend: function () {
            Showloader();
        },
        complete: function () {
            Hideloader();
        },
        success: function (Accnos) {

            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select Center--" }));
            $.each(Accnos.Responce, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.Center_Code + '">' + Accno.Center_Code + " " + Accno.Center_Name + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });

}
function Get_Division(Client_ID, ID) {

    $("#" + ID).empty();

    $.ajax({

        type: 'POST',
        url: '/Division/Get_Division',
        autoUpload: true,
        dataType: 'json',
        data: { Client_ID: Client_ID },

        beforeSend: function () {
            Showloader();
        },
        complete: function () {
            Hideloader();
        },
        success: function (Accnos) {
            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select Division--" }));
            $.each(Accnos, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.Division_ID + '">' + Accno.Division_Name + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });
}
function Get_Cutodian(Division_Code, ID) {

    $("#" + ID).empty();
    $.ajax({
        type: 'POST',
        url: '/Registration/Get_Cutodian',
        autoUpload: true,
        dataType: 'json',
        data: {
            Division_Code: Division_Code
        },
        beforeSend: function () {
            Showloader();
        },
        complete: function () {
            Hideloader();
        },
        success: function (Accnos) {

            $("#" + ID).append($('<option/>', { Value: 0, text: "--Select Center--" }));
            $.each(Accnos.Responce, function (i, Accno) {
                $("#" + ID).append('<option value="' + Accno.Cutodian_Code + '">' + Accno.Cutodian_Code + '</option>');
            });
        },
        error: function (e1, e2, e3) {

        }
    });

}
function Bind_Table(data, ID) {
    var htmldata = "", j = 1, k = 1;
    var col = [];
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }
    htmldata += `<tr>`;
    for (var i = 0; i < col.length; i++) {
        for (var i = 0; i < col.length; i++) {
            htmldata += `<th>` + col[i] + `</th>`;

        }
    }
    htmldata += `</tr>`;
    for (var i = 0; i < data.length; i++) {
        htmldata += `<tr>`;
        for (var j = 0; j < col.length; j++) {
            var value = data[i][col[j]];
            if (value == null) { value = ""; }
            if (value == "null") { value = ""; }
            htmldata += `<td>` + value + `</td>`;
        }
        htmldata += `</tr>`;
    } $("#" + ID).append(htmldata);
}
function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}
function Get_Division_Name(Division_Code) {

    var Address = "";
    switch (Division_Code) {
        case "1":
            Address = " PUNE";
            break;
        case "2":
            Address = " NAGPUR";
            break;
        case "3":
            Address = " AURANGABAD";
            break;
        case "4":
            Address = " MUMBAI";
            break;
        case "5":
            Address = " KOLHAPUR";
            break;
        case "6":
            Address = " AMRAVATI";
            break;
        case "7":
            Address = " NASHIK";
            break;
        case "8":
            Address = " LATUR";
            break;
        case "9":
            Address = " KONKAN";
            break;

    }

    return Address;
}
function Get_Division_Name_Marathi(Division_Code) {

    var Address = "";
    switch (Division_Code) {
        case "1":
            Address = " पुणे";
            break;
        case "2":
            Address = " नागपूर";
            break;
        case "3":
            Address = " औरंगाबाद";
            break;
        case "4":
            Address = " मुंबई";
            break;
        case "5":
            Address = " कोल्हापूर";
            break;
        case "6":
            Address = " अमरावती";
            break;
        case "7":
            Address = " नाशिक";
            break;
        case "8":
            Address = " लातूर";
            break;
        case "9":
            Address = " कोकण";
            break;

    }

    return Address;
}
function Get_Payment_Head(Payment_Head) {

    var Address = "";
    switch (Payment_Head) {
        case "301":
            Address = "उत्तरपत्रिका गुणपडताळणी";
            break;
        case "302":
            Address = " उत्तरपत्रिका छायाप्रत";
            break;
        case "303":
            Address = "उत्तरपत्रिका पुनर्मूल्यांकन";
            break;


    }

    return Address;
}


function Get_Month(lang) {
    console.log(lang);
    if (lang == "9") {
        return `<select name="ddl_month" id="ddl_month">
                <option value="0">Select Month</option>
                <option value="1">February/March/April (Main Exam)</option>
                <option value="2">July/September/October /December (Supplementary Exam)</option>
               </select>`}
    else
        return "";
}


function Get_Year(lang) {

    if (lang == "9") {
        return `<select name="ddl_year" id="ddl_year">
                <option value="0">Select Year</option>
                         <option value="2020">2020 </option>
                         <option value="2019">2019 </option>
                         <option value="2018">2018 </option>
                         <option value="2017">2017 </option>
                         <option value="2016">2016 </option>
                         <option value="2015">2015 </option>
                         <option value="2014">2014 </option>
                         <option value="2013">2013 </option>
                         <option value="2012">2012 </option>
                         <option value="2011">2011 </option>
                         <option value="2010">2010 </option>
                         <option value="2009">2009 </option>
                         <option value="2008">2008 </option>
                         <option value="2007">2007 </option>
                         <option value="2006">2006 </option>
                         <option value="2005">2005 </option>
                         <option value="2004">2004 </option>
                         <option value="2003">2003 </option>
                         <option value="2002">2002 </option>
                         <option value="2001">2001 </option>
                         <option value="2000">2000 </option>
                         <option value="1999">1999 </option>
                         <option value="1998">1998 </option>
                         <option value="1997">1997 </option>
                         <option value="1996">1996 </option>
                         <option value="1995">1995 </option>
                         <option value="1994">1994 </option>
                         <option value="1993">1993 </option>
                         <option value="1992">1992 </option>
                         <option value="1991">1991 </option>
                         <option value="1990">1990 </option>
                         <option value="1989">1989 </option>
                         <option value="1988">1988 </option>
                         <option value="1987">1987 </option>
                         <option value="1986">1986 </option>
                         <option value="1985">1985 </option>
                         <option value="1984">1984 </option>
                         <option value="1983">1983 </option>
                         <option value="1982">1982 </option>
                         <option value="1981">1981 </option>
                         <option value="1980">1980 </option>

                 </select>`}
    else
        return "";
}

function Replace_Subjet_Change(value) {
    if (value == "MM") {
        return "-2";
    }
    else { return value}
}  
$(document).ready(function () {
    $('#tbl_last_std').on('keyup', 'input.txt_marks', function (e) {
        console.log(e.which + " " + this.value);
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57) && (e.which < 96 || e.which > 105) || (e.which == 77)) {
            if (this.value != '.') {
            this.value = this.value.replace(/[^0-9\.]/g, '');           
            return false;
        }
    }
        var Current_Row = $(this).closest("tr");
        if (parseFloat(Current_Row.find('input[id="txt_last_percent"]').val()) > parseFloat(100.00)) { Current_Row.find('input[id="txt_last_percent"]').val("") }
});
});















