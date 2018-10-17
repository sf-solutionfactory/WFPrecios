<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudLP.aspx.cs" Inherits="WFPrecios.ListasPrecio.SolicitudLP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Nueva Solicitud</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.js"></script>
    <script src="../js/jquery-ui-1.11.4.js"></script>
    <link href="../css/datepicker.css" rel="stylesheet" />
    <%-- <script type="text/javascript">
        var j11_4 = $.noConflict(true);
    </script>--%>
    <script type="text/javascript">
        function autocompleta_c(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var vko = $('#txtVKORG').val();
                    var vtw = $('#txtVTWEG').val();
                    var spa = $('#txtSPART').val();
                    var emp = $('#hidNumEmp').val();
                    var tip = $('#hidTipoEmp').val();
                    var param = { vk: vko, vt: vtw, sp: spa, ku: para.toUpperCase(), em: emp, tp: tip };
                    $.ajax({
                        url: "../catalogos.asmx/auto_clientes",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = $.parseJSON(data.d)
                            response($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {

                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        var val = ui.item.value;
                        var arr = val.split(' - ');
                        $('#txtKUNNR').text = arr[0];
                        llena_cliente(arr[0]);
                    }
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }

        function llena_cliente(cliente) {
            traerDato(cliente, "NAME1", "txtNAME1");
            traerDato(cliente, "STRAS", "txtADDR");
            traerEmail(cliente, "txtEMAIL");
            traerDato(cliente, "STCD1", "txtSTCD1");
            traerLP(cliente, "txtPLTYP");
            traerLPS(cliente);
        }

        function traerDato(cliente, columna, campo) {
            var ct = cliente;
            var col = columna;
            var vko = $('#txtVKORG').val();
            var vtw = $('#txtVTWEG').val();
            var spa = $('#txtSPART').val();
            var param = { vk: vko, vt: vtw, sp: spa, kunnr: ct, column: col };
            $.ajax({
                url: "../catalogos.asmx/getColumnaCliente",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    document.getElementById(campo).value = data.d;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });

        }
        function traerEmail(cliente, campo) {
            var ct = cliente;
            var vko = $('#txtVKORG').val();
            var vtw = $('#txtVTWEG').val();
            var spa = $('#txtSPART').val();
            var param = { vk: vko, vt: vtw, sp: spa, kunnr: ct };
            $.ajax({
                url: "../catalogos.asmx/getEmailCliente",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {

                    document.getElementById(campo).value = data.d;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });

        }
        function traerLP(cliente, campo) {
            var ct = cliente;
            var vko = $('#txtVKORG').val();
            var vtw = $('#txtVTWEG').val();
            var spa = $('#txtSPART').val();
            var param = { vk: vko, vt: vtw, sp: spa, kunnr: ct };
            $.ajax({
                url: "../catalogos.asmx/getLPCliente",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var res = "";
                    if (data.d == "")
                        res = "No tiene lista de precios";
                    else
                        res = data.d;

                    document.getElementById(campo).value = res;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        }

        function traerLPS(cliente) {
            var ct = cliente;
            var vko = $('#txtVKORG').val();
            var vtw = $('#txtVTWEG').val();
            var spa = $('#txtSPART').val(); 
            var emp = $('#hidNumEmp').val();
            var param = { sp: spa, kunnr: ct, em:emp  };
            $.ajax({
                url: "../catalogos.asmx/getLPSCliente",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var embeddedJsonObj = $.parseJSON(data.d);
                    var oo = ($.map(embeddedJsonObj, function (item) {
                        return {
                            value: item
                        }
                    }))
                    var pl = document.getElementById("txtPLTYP_N");
                    for (var i = 0; i < pl.length; i++) {
                        pl.remove(0);
                        i--;
                    }
                    for (var i = 0; i < oo.length; i++) {
                        var option = document.createElement("option");
                        option.text = oo[i].value;
                        var valor = oo[i].value + "";;
                        option.value = valor.split(" ")[0];
                        pl.add(option);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server" action="EnviaSolicitud.aspx">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Listas de precios / Nueva Solicitud
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="ExcelLP.aspx">Carga Excel</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="ExcelLP.aspx">Carga Excel</a>&nbsp;|&nbsp;<a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <div class="row">
                <div class="col s12 m12 l12">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr>
                            <td class="cell03">Org. de ventas</td>
                            <td>
                                <select class="cell03" name="txtVKORG" runat="server" id="txtVKORG" onchange="eligeVKORG(this.value)">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Canal Dist.</td>
                            <td>
                                <select class="cell03" name="txtVTWEG" runat="server" id="txtVTWEG" onchange="eligeVKORG(this.value)">
                                </select></td>
                        </tr>
                        <tr>
                            <td class="cell03">Sector</td>
                            <td>
                                <select class="cell03" name="txtSPART" runat="server" id="txtSPART" onchange="eligeSPART(this.value)">
                                </select></td>
                        </tr>
                        <tr>
                            <td class="cell03">Cliente</td>
                            <td>
                                <input class="cell07" type="text" name="txtKUNNR" id="txtKUNNR" runat="server" />
                                <script type="text/javascript">autocompleta_c("txtKUNNR");</script>
                            </td>
                        </tr>



                        <tr>
                            <td class="cell03">Nombre</td>
                            <td>
                                <input class="cell05" type="text" id="txtNAME1" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Dirección</td>
                            <td>
                                <input class="cell05" type="text" id="txtADDR" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Datos de contacto</td>
                            <td>
                                <input class="cell05" type="text" id="txtEMAIL" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">RFC</td>
                            <td>
                                <input class="cell05" type="text" id="txtSTCD1" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Lista de precios</td>
                            <td>
                                <input class="cell05" type="text" id="txtPLTYP" disabled="disabled" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="cell03">Nueva Lista de precios</td>
                            <td>
                                <select class="cell03" runat="server" id="txtPLTYP_N"></select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Fecha vigencia</td>
                            <td>
                                <input class="cell03 datepicker" runat="server" id="txtDATE" type="text" onchange="cambiaFecha(this.value)" />
                            </td>
                        </tr>
                    </table>
                    <link href="../css/jquery-ui2.css" rel="stylesheet" />
                    <script src="../js/jquery-ui.js"></script>
                    <script>
                        $(function () {
                            var input = '<%=fecha_limite %>';
                            var date = new Date();
                            if (input != '') {
                                var parts = input.match(/(\d+)/g);
                                date = new Date(parts[0], parts[1] - 1, parts[2]);
                            }

                            $(".datepicker").datepicker({
                                dateFormat: "dd/mm/yy",
                                minDate: new Date(),
                                maxDate: date
                            });
                        });
                        function cambiaFecha(valor) {
                            var input = '<%=fecha_limite %>';
                            var date = new Date();
                            if (input != '') {
                                var parts = input.match(/(\d+)/g);
                                date = new Date(parts[0], parts[1] - 1, parts[2]);
                            }
                            var fecha = $("#txtDATE").datepicker("getDate");
                            if (fecha > date) {
                                document.getElementById("txtDATE").style.backgroundColor = "#ff6666";
                                return true;
                            } else {
                                document.getElementById("txtDATE").style.backgroundColor = "#ffffff";
                                return false;
                            }
                        }
                    </script>

                </div>



            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="button" value="Agregar" class="btn right" onclick="addLine()" />
                </div>
            </div>
            <script type="text/javascript">
                function eligeVKORG(value) {
                    document.getElementById("txtKUNNR").value = "";
                    document.getElementById("txtNAME1").value = "";
                    document.getElementById("txtADDR").value = "";
                    document.getElementById("txtEMAIL").value = "";
                    document.getElementById("txtSTCD1").value = "";
                    document.getElementById("txtPLTYP").value = "";
                    //document.getElementById("txtDATE").value = "";
                }

                function eligeSPART(value) {
                    var sp = document.getElementById("txtSPART").value;
                    var pr = document.getElementById("hidNumEmp").value;
                    var param = { pr: pr, sp: sp };
                    $.ajax({
                        url: "../catalogos.asmx/llenaPltyp",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = $.parseJSON(data.d);
                            var oo = ($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                            var pl = document.getElementById("txtPLTYP_N");
                            for (var i = 0; i < pl.length; i++) {
                                pl.remove(0);
                                i--;
                            }
                            for (var i = 0; i < oo.length; i++) {
                                var option = document.createElement("option");
                                option.text = oo[i].value;
                                var valor = oo[i].value + "";;
                                option.value = valor.split(" ")[0];
                                pl.add(option);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });

                }


                var lines = 0;
                var tab = [];
                function addLine() {
                    var kunnr = document.getElementById("txtKUNNR").value.split("-")[0].trim();
                    var pltyp_n = document.getElementById("txtPLTYP_N").value;

                    var vkorg = document.getElementById("txtVKORG").value;
                    var vtweg = document.getElementById("txtVTWEG").value;
                    var spart = document.getElementById("txtSPART").value;
                    var name1 = document.getElementById("txtNAME1").value;
                    var pltyp = document.getElementById("txtPLTYP").value;
                    var date = document.getElementById("txtDATE").value;

                    if (pltyp != "" & kunnr != "" & date != "") {

                        var table = document.getElementById("txtTabla").value;

                        if (tab.length == 0) {
                            table = "";
                        }
                        var ban = false;
                        for (var i = 0; i < tab.length; i++) {
                            var sol = tab[i];

                            if (vkorg == sol.vkorg) {
                                if (vtweg == sol.vtweg) {
                                    if (spart == sol.spart) {
                                        if (kunnr.trim() == sol.kunnr.trim()) {
                                            ban = true;
                                        }
                                    }
                                }
                            }
                        }

                        var ban2 = cambiaFecha(date);
                        if (!ban2) {
                            if (!ban) {
                                var param = { lp: pltyp_n, vk: vkorg, vt: vtweg, sp: spart, ku: kunnr };
                                $.ajax({
                                    url: "../catalogos.asmx/getDescLP",
                                    data: JSON.stringify(param),
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    dataFilter: function (data) { return data; },
                                    success: function (data) {
                                        pltyp_n = data.d;

                                        if (pltyp_n != "" & kunnr != "") {

                                            if (lines < 1) {
                                                document.getElementById("lblTabla").innerHTML = "";
                                                $("#lblTabla").append("<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody></tbody></table>");
                                                $("#lblTabla tbody").append("<tr><td class='tablahead'>Org</td><td class='tablahead'>Canal</td><td class='tablahead'>Sector</td>" +
                                                                                "<td class='tablahead'>Cliente</td><td class='tablahead'>Nombre</td><td class='tablahead'>Lista Ant</td>" +
                                                                                "<td class='tablahead'>Nueva lista</td><td class='tablahead'>Vigencia</td><td class='tablahead'></td></tr>");

                                                //-----END INSERT 28/04/2017 
                                            }
                                            lines++;
                                            var body = "";
                                            body += "<tr id=tr-" + lines + ">";
                                            body += "<td class='tablaCent'>" + vkorg;
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + vtweg;
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + spart;
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + kunnr;
                                            body += "</td>";
                                            body += "<td class='tablaIzq'>" + name1;
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + pltyp;
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + pltyp_n;
                                            body += "</td>";
                                            body += "</td>";
                                            body += "<td class='tablaCent'>" + date;
                                            body += "</td>";
                                            body += "</td>";
                                            //-----BEGIN INSERT 28/04/2017 
                                            body += "<td class='tablaCent'><input type='button' id='chk-" + lines + "' value='-' class='btn2' onclick='elimina(this.id)' /></td>";
                                            //-----END INSERT 28/04/2017 
                                            body += "</tr>";

                                            $("#lblTabla tbody").append(body);


                                            var solicitud = {
                                                vkorg: document.getElementById("txtVKORG").value,
                                                vtweg: document.getElementById("txtVTWEG").value,
                                                spart: document.getElementById("txtSPART").value,
                                                kunnr: document.getElementById("txtKUNNR").value.split("-")[0].trim(),
                                                pltyp: document.getElementById("txtPLTYP").value,
                                                pltyp_n: pltyp_n,
                                                date: document.getElementById("txtDATE").value,
                                                name1: name1, //RSG 02.05.2017
                                                id: lines
                                            }

                                            tab.push(solicitud);
                                            table += solicitud.vkorg + "|" + solicitud.vtweg + "|" + solicitud.spart + "|" +
                                                     solicitud.kunnr + "|" + solicitud.pltyp.split(" ")[0] + "|" + solicitud.pltyp_n.split(" ")[0] + "|" + solicitud.date + "|" + solicitud.id + "|";

                                            document.getElementById("txtTabla").value = table;
                                            document.getElementById("btnSubmit").disabled = false;
                                            document.getElementById("btnSubmit").className = "btn right";
                                        }

                                        document.getElementById("txtKUNNR").value = "";
                                        document.getElementById("txtNAME1").value = "";
                                        document.getElementById("txtADDR").value = "";
                                        document.getElementById("txtEMAIL").value = "";
                                        document.getElementById("txtSTCD1").value = "";
                                        document.getElementById("txtPLTYP").value = "";
                                        //document.getElementById("txtDATE").value = "";
                                    },
                                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                                        //alert(textStatus);
                                    }
                                });
                            } else {
                                alert("Ya ha agregado ese cliente");
                            }
                        } else {
                            alert("Cambie la fecha por una válida.");
                        }
                    }
                }
                function enviar() {
                    document.getElementById("modal1").style.visibility = "visible";
                }

                //-----BEGIN INSERT 28/04/2017 
                var b = false;
                function elimina(id) {
                    //if (!b) {
                    b = true;
                    var num = id.split('-')[1];
                    var tabla = document.getElementById("txtTabla").value;
                    var arr = tabla.split('|');
                    var temp = "";
                    //tab = [];
                    var tab2 = [];
                    for (var i = 1; i <= tab.length ; i++) {
                        if ((tab[i - 1].id) != num) {
                            temp += tab[i - 1].vkorg + '|';
                            temp += tab[i - 1].vtweg + '|';
                            temp += tab[i - 1].spart + '|';
                            temp += tab[i - 1].kunnr.split("-")[0].trim() + '|';
                            temp += tab[i - 1].pltyp.split(" ")[0] + '|';
                            temp += tab[i - 1].pltyp_n.split(" ")[0] + '|';
                            temp += tab[i - 1].date + '|';
                            temp += tab[i - 1].id + '|';

                            var solicitud = {
                                vkorg: tab[i - 1].vkorg,
                                vtweg: tab[i - 1].vtweg,
                                spart: tab[i - 1].spart,
                                kunnr: tab[i - 1].kunnr,
                                pltyp: tab[i - 1].pltyp,
                                pltyp_n: tab[i - 1].pltyp_n,
                                date: tab[i - 1].date,
                                name1: tab[i - 1].name1,
                                id: tab[i - 1].id
                            }

                            tab2.push(solicitud);
                        } else {

                        }
                    }
                    document.getElementById("txtTabla").value = temp;
                    tab = tab2;

                    //document.getElementById("lblTabla").innerHTML = "";
                    document.getElementById("tr-" + num).style.display = "none";

                    ////$("#lblTabla").append("<table border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody></tbody></table>");
                    ////$("#lblTabla tbody").append("<tr><td class='tablahead'>Org</td><td class='tablahead'>Canal</td><td class='tablahead'>Sector</td>" +
                    ////                                "<td class='tablahead'>Cliente</td><td class='tablahead'>Nombre</td><td class='tablahead'>Lista Ant</td>" +
                    ////                                "<td class='tablahead'>Nueva lista</td><td class='tablahead'>Vigencia</td><td class='tablahead'></td></tr>");

                    ////var body = "";
                    ////for (var i = 0; i < tab.length; i++) {
                    ////    body += "<tr id=tr-" + tab[i].id + ">";
                    ////    body += "<td class='tablaCent'>" + tab[i].vkorg;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].vtweg;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].spart;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].kunnr;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaIzq'>" + tab[i].name1;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].pltyp;
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].pltyp_n;
                    ////    body += "</td>";
                    ////    body += "</td>";
                    ////    body += "<td class='tablaCent'>" + tab[i].date;
                    ////    body += "</td>";
                    ////    body += "</td>";
                    ////    //-----BEGIN INSERT 28/04/2017 
                    ////    body += "<td class='tablaCent'><input type='button' id='chk-" + tab[i].id + "' value='-' class='btn2' onclick='elimina(this.id);' /></td>";
                    ////    //-----END INSERT 28/04/2017 
                    ////    body += "</tr>";
                    ////    $("#lblTabla tbody").append(body);
                    ////    body = "";
                    ////}

                    //for (var i = 0; i < tab.length; i++) {
                    //    $("chk-" + (i + 1)).unbind('click').click(function () {
                    //        alert("bob");
                    //        //addToCart($(this).attr("id"));
                    //    });
                    //}
                    ////lines--;
                    //} else {
                    //    b = false;
                    //}
                }
                //-----END INSERT 28/04/2017 
            </script>

            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table9" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tbody>
                            <tr>
                                <td class="cell08">Nueva solicitud</td>
                            </tr>
                        </tbody>
                    </table>
                    <label runat="server" id="lblTabla"></label>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    Comentarios:<br />
                    <%--<textarea runat="server" id="txtCOMM" style="width: 100%" rows="6"></textarea>--%>
                    <script src="../js/materialize.js"></script>
                    <div class="input-field col s12">
                        <textarea runat="server" id="txtCOMM" class="materialize-textarea" data-length="255" style="width: 100%; font-family: Oswald" rows="2"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="hidden" runat="server" id="txtTabla" required="required" />
                    <input type="submit" runat="server" value="Enviar" id="btnSubmit" disabled="disabled" class="btn right disabled" onclick="enviar()" />
                    <div class="right" style="width: 10px;">&nbsp;</div>
                    <a href="Default.aspx" runat="server" class="btn right" target="_parent">Cancelar</a>
                </div>
            </div>
            <div id="modal1" style="visibility: hidden">
                <div class="modal-overlay" id="materialize-modal-overlay-1" style="z-index: 1002; display: block; opacity: 0.5;">
                    <div style="position: absolute; top: 50%; left: 50%; width: 300px; margin-left: -150px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;">
                        Procesando...
                <%--<img src="images/ajax_loader_blue_512.gif" style="top: 40%; left: 40%; position: inherit; width: 20%;" />--%>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />
    </form>
</body>
</html>
