<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SolicitudLO.aspx.cs" Inherits="WFPrecios.Precios.SolicitudLO" %>

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
    <%--<script src="../../js/materialize.js"></script>--%>
    <script type="text/javascript">

        var ban = false;
        function autocompleta_m(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    //var vko = $('#txtVKORG').val();
                    //var vtw = $('#txtVTWEG').val();
                    var spa = $('#txtSPART').val();
                    if (spa != null) {
                        var param = { ma: para.toUpperCase(), sp: spa };
                        $.ajax({
                            url: "../catalogos.asmx/auto_materiales",
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
                    }
                },
                select: function (event, ui) {
                    ban = true;
                    document.getElementById("btnCargar").disabled = false;
                    document.getElementById("btnAgregar").disabled = false;
                    //if (ui.item) {
                    //    var val = ui.item.value;
                    //    var arr = val.split(' - ');
                    //    $('#txtMATNR').text = arr[0];
                    //    llena_cliente(arr[0]);
                    //}
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }
        function cantidades(id) { //Verifica que el contenido sea numérico
            var patron = /^[0-9]*\.?[0-9]+$/;
            //var id = evt.target.id;
            var valor = $("#" + id).val();
            if (valor.length > 0) {
                if (patron.test(valor))
                    valor = valor;
                else {
                    document.getElementById(id).value = "";
                }
            }
        }
        function autocompleta_l(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    //var vko = $('#txtVKORG').val();
                    //var vtw = $('#txtVTWEG').val();
                    var mat = $('#txtMATNR').val().split(' ')[0];
                    if (mat != null) {
                        var param = { ch: para.toUpperCase(), ma: mat };
                        $.ajax({
                            url: "../catalogos.asmx/auto_lote",
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
                    }
                },
                select: function (event, ui) {
                    //if (ui.item) {
                    //    var val = ui.item.value;
                    //    var arr = val.split(' - ');
                    //    $('#txtMATNR').text = arr[0];
                    //    llena_cliente(arr[0]);
                    //}
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }

        function existe(id, lote) {
            var re = "";
            var para = lote;
            var mat = $('#txtMATNR').val().split(' ')[0];
            var param = { ch: para.toUpperCase(), ma: mat };
            $.ajax({
                url: "../catalogos.asmx/existe_lote",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    document.getElementById("MATNR-"+id).value = data.d;
                    re = data.d;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            });
            return re;
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
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Precios / Nueva Solicitud
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="ExcelSP.aspx?Tipo=2">Carga Excel</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="ExcelSP.aspx?Tipo=2">Carga Excel</a>&nbsp;|&nbsp;<a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <table id="Table9" style="border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td class="cell08">Nueva solicitud</td>
                    </tr>
                </tbody>
            </table>
            <div class="row">
                <div class="col s12 m12 l12">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <%--                        <tr>
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
                        </tr>--%>
                        <tr>
                            <td class="cell03">Sector</td>
                            <td>
                                <select class="cell03" name="txtSPART" runat="server" id="txtSPART" onchange="eligeVKORG(this.value)">
                                </select></td>
                        </tr>
                        <tr>
                            <td class="cell03">Material</td>
                            <td>
                                <input class="cell07" type="text" name="txtMATNR" id="txtMATNR" runat="server" onchange="cambiaMATNR()" />
                                <script type="text/javascript">autocompleta_m("txtMATNR");</script>
                            </td>
                        </tr>



                        <tr>
                            <td class="cell03">% Incremento</td>
                            <td>
                                <input class="cell03" type="text" id="P_inc" onchange="porcentajes(this)" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">% Decremento</td>
                            <td>
                                <input class="cell03" type="text" id="P_dec" onchange="porcentajes(this)" runat="server" />
                            </td>
                        </tr>

                        <tr>
                            <td class="cell03">
                                <input id="chkCalidad" type="checkbox" runat="server" />
                                <label for="chkCalidad">Calidad degradada</label>
                            </td>
                            <td></td>
                        </tr>
                    </table>

                </div>



            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="button" value="Cargar" class="btn right" onclick="cargar()" id="btnCargar" runat="server" disabled="disabled" />
                    <div class="right" style="width: 10px;">&nbsp;</div>
                    <input type="button" value="Agregar" class="btn right" onclick="add()" id="btnAgregar" runat="server" disabled="disabled" />
                </div>
            </div>
            <script>
                function cambiaMATNR() {
                    //document.getElementById("txtKUNNR").value = document.getElementById("txtKUNNR").value.split(" -")[0];
                    if (ban)
                        ban = false;
                    else {
                        document.getElementById("btnCargar").disabled = true;
                        document.getElementById("btnAgregar").disabled = true;
                    }

                    document.getElementById("txtTabla").value = "";
                    document.getElementById("txtPos").value = "0";
                    document.getElementById("lblTabla").innerHTML = "";
                }
                function porcentajes(input) {
                    if (input.value < 0) input.value = 0;
                }

                function cargar() {
                    //document.getElementById("txtVKORG").disabled = false;
                    //document.getElementById("txtVTWEG").disabled = false;
                    document.getElementById("txtSPART").disabled = false;
                    $('form').get(0).setAttribute('action', './SolicitudLO.aspx'); //this works
                    document.forms["form1"].submit();
                }
            </script>
            <script type="text/javascript">

                function eligeVKORG(value) {
                    document.getElementById("txtMATNR").value = "";
                    document.getElementById("lblTabla").innerHTML = "";
                    document.getElementById("txtTabla").value = "";
                    document.getElementById("btnCargar").disabled = true;
                    document.getElementById("btnAgregar").disabled = true;
                    document.getElementById("btnSubmit").disabled = true;
                }
                function cambiaPed(campo) {
                    var valor = campo.value;
                    var id = campo.id.split('-')[1];
                    existe(id, valor);
                    var valores = document.getElementById("txtTabla").value;

                    valor = campo.value;

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        if (pos == id) {
                            nueva += valor.split(' ')[0] + "|";
                        } else {
                            nueva += tabla[i] + "|";
                        }
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        nueva += tabla[i + 4] + "|";
                        nueva += tabla[i + 5] + "|";
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        nueva += tabla[i + 9] + "|";
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }
                function cambiaCant(id) {
                    cantidades(id);

                    var valor = document.getElementById(id).value;
                    var id = id.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 3] + "|";
                        }
                        nueva += tabla[i + 4] + "|";
                        nueva += tabla[i + 5] + "|";
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        nueva += tabla[i + 9] + "|";
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }
                function cambiaMone(campo) {
                    var valor = campo.value;
                    var id = campo.id.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 4] + "|";
                        }
                        nueva += tabla[i + 5] + "|";
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        nueva += tabla[i + 9] + "|";
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }
                //function cambiaFechA(valor, ida) {
                //    //var valor = campo.value;
                //    var id = ida.split('-')[1];
                //    var valores = document.getElementById("txtTabla").value;

                //    revisaFechaA(ida);

                //    var tabla = valores.split('|');
                //    var nueva = "";
                //    var pos = 0;
                //    for (var i = 0; i < tabla.length - 1; i += 11) {
                //        pos++;
                //        nueva += tabla[i] + "|";
                //        nueva += tabla[i + 1] + "|";
                //        nueva += tabla[i + 2] + "|";
                //        nueva += tabla[i + 3] + "|";
                //        nueva += tabla[i + 4] + "|";
                //        if (pos == id) {
                //            nueva += valor + "|";
                //        } else {
                //            nueva += tabla[i + 5] + "|";
                //        }
                //        nueva += tabla[i + 6] + "|";
                //        nueva += tabla[i + 7] + "|";
                //        nueva += tabla[i + 8] + "|";
                //        nueva += tabla[i + 9] + "|";
                //    }

                //    document.getElementById("txtTabla").value = nueva;
                //}
                function cambiaFechA(valor, ida) {
                    //var valor = campo.value;
                    var id = ida.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    revisaFechaA(ida);

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        nueva += tabla[i + 4] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 5] + "|";
                        }
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";

                        nueva += tabla[i + 9] + "|";
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }
                function cambiaFechB(valor, ida) {
                    //var valor = campo.value;
                    var id = ida.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    var ban = revisaFechaB(ida);

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        nueva += tabla[i + 4] + "|";
                        nueva += tabla[i + 5] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 6] + "|";
                        }
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        nueva += tabla[i + 9] + "|";
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }

                function cambiaCome(campo) {
                    var valor = campo.value;
                    var id = campo.id.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        nueva += tabla[i + 4] + "|";
                        nueva += tabla[i + 5] + "|";
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 9] + "|";
                        }
                        nueva += tabla[i + 10] + "|";
                    }

                    document.getElementById("txtTabla").value = nueva;
                }

                function cambiaMeins(ida, value) {
                    var valor = value;
                    var id = ida.split('-')[1];
                    var valores = document.getElementById("txtTabla").value;

                    var tabla = valores.split('|');
                    var nueva = "";
                    var pos = 0;
                    for (var i = 0; i < tabla.length - 1; i += 11) {
                        pos++;
                        nueva += tabla[i] + "|";
                        nueva += tabla[i + 1] + "|";
                        nueva += tabla[i + 2] + "|";
                        nueva += tabla[i + 3] + "|";
                        nueva += tabla[i + 4] + "|";
                        nueva += tabla[i + 5] + "|";
                        nueva += tabla[i + 6] + "|";
                        nueva += tabla[i + 7] + "|";
                        nueva += tabla[i + 8] + "|";
                        nueva += tabla[i + 9] + "|";
                        if (pos == id) {
                            nueva += valor + "|";
                        } else {
                            nueva += tabla[i + 10] + "|";
                        }
                    }
                    document.getElementById("txtTabla").value = nueva;
                }

                function revisaFechaA(ida) {
                    var id = ida.split('-')[1];

                    var posi = document.getElementById("txtPos").value;

                    var ban = false;
                    //for (var i = 1 ; i <= posi; i++) {

                    var dateA = $("#DATAB-" + id).datepicker('getDate');
                    var dateB = $("#DATBI-" + id).datepicker('getDate');

                    if (dateA != null & dateB != null) {
                        if (dateA > dateB) {
                            document.getElementById("DATAB-" + id).style.backgroundColor = "#ff6666";
                            document.getElementById("DATBI-" + id).style.backgroundColor = "#ff6666";
                            ban = true;
                        } else {
                            document.getElementById("DATAB-" + id).style.backgroundColor = "#ffffff";
                            document.getElementById("DATBI-" + id).style.backgroundColor = "#ffffff";
                        }
                    }
                    //}
                    document.getElementById("btnSubmit").disabled = ban;
                    //return ban;
                    if (!ban) {
                        var input = '<%=fecha_limite %>';
                            var date_l = new Date();
                            if (input != '') {
                                var parts = input.match(/(\d+)/g);
                                date_l = new Date(parts[0], parts[1] - 1, parts[2]);
                            }
                            var dateA = $("#" + ida).datepicker('getDate');
                            if (dateA > date_l) {
                                document.getElementById(ida).style.backgroundColor = "#ff6666";
                                ban = true;
                            } else {
                                document.getElementById(ida).style.backgroundColor = "#ffffff";
                            }
                            var dateB = $("#DATBI-" + id).datepicker('getDate');
                            if (dateB > date_l) {
                                document.getElementById("DATBI-" + id).style.backgroundColor = "#ff6666";
                                ban = true;
                            } else {
                                document.getElementById("DATBI-" + id).style.backgroundColor = "#ffffff";
                            }
                        }
                        return ban;
                    }
                    function revisaFechaB(ida) {
                        var id = ida.split('-')[1];

                        var posi = document.getElementById("txtPos").value;

                        var ban = false;
                        //for (var i = 1 ; i <= posi; i++) {

                        var dateA = $("#DATAB-" + id).datepicker('getDate');
                        var dateB = $("#DATBI-" + id).datepicker('getDate');

                        if (dateA != null & dateB != null) {
                            if (dateA > dateB) {
                                document.getElementById("DATAB-" + id).style.backgroundColor = "#ff6666";
                                document.getElementById("DATBI-" + id).style.backgroundColor = "#ff6666";
                                ban = true;
                            } else {
                                document.getElementById("DATAB-" + id).style.backgroundColor = "#ffffff";
                                document.getElementById("DATBI-" + id).style.backgroundColor = "#ffffff";
                            }
                        }
                        //}
                        document.getElementById("btnSubmit").disabled = ban;
                        //return ban;
                        if (!ban) {
                            var input = '<%=fecha_limite %>';
                            var date_l = new Date();
                            if (input != '') {
                                var parts = input.match(/(\d+)/g);
                                date_l = new Date(parts[0], parts[1] - 1, parts[2]);
                            }
                            var dateA = $("#" + ida).datepicker('getDate');
                            if (dateA > date_l) {
                                document.getElementById(ida).style.backgroundColor = "#ff6666";
                                ban = true;
                            } else {
                                document.getElementById(ida).style.backgroundColor = "#ffffff";
                            }
                            var dateB = $("#DATAB-" + id).datepicker('getDate');
                            if (dateB > date_l) {
                                document.getElementById("DATAB-" + id).style.backgroundColor = "#ff6666";
                                ban = true;
                            } else {
                                document.getElementById("DATAB-" + id).style.backgroundColor = "#ffffff";
                            }
                        }
                        return ban;
                    }

                    function add() {
                        document.getElementById("btnSubmit").disabled = false;
                        var linea = "";
                        var posi = document.getElementById("txtPos").value;
                        posi++;
                        document.getElementById("txtPos").value = posi;
                        if (posi == 1) {
                            document.getElementById("lblTabla").innerHTML = "";
                            linea = "<table id='tblTabla' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'><tbody>";
                            //linea += "<tr><td class='tablahead'>Pedido del cliente</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td></tr></tbody></table>";
                            linea += "<tr><td class='tablahead'>Lote</td><td class='tablahead'>Unidad</td>" +
                                "<td class='tablahead'>Precio actual</td><td class='tablahead'>Moneda</td>" +
                                "<td class='tablahead'>Precio nuevo</td><td class='tablahead'>Moneda</td>" +
                                "<td class='tablahead'>Válido de</td><td class='tablahead'>Válido a</td><td class='tablahead'>Comentario</td><td class='tablahead'>E</td><td class='tablahead'></td></tr>";
                            $("#lblTabla").append(linea);
                        }
                        linea = "";
                        linea += "<tr id='tr-" + posi + "'>";

                        linea += "<td class='tablaCent'><input class='cell031' type='text' id='MATNR-" + posi + "' onchange='cambiaPed(this)' onblur='borraMatnr(this.id, this.value)' ondblclick='escalas(this.id)'  /></td>";
                        //linea += "<td><input class='cell03' type='text' id='NAME1-" + posi + "' disabled='disabled' /></td>";
                        //linea += "<td><input class='cell06' type='text' id='MEINS-" + posi + "' disabled='disabled' /></td>";
                        linea += "<td id='trMEINS-" + posi + "' ondblclick='escalas(this.id)'><select class='cell031' id='MEINS-" + posi + "' style='width:50px;' onchange='cambiaMeins(this.id, this.value)'>";
                        linea += "<%=um_meins %>" + "</select></td>";
                        linea += "<td>0.00</td><td></td>";
                        linea += "<td class='tablaCent'><input class='cell031' type='text' onchange='cambiaCant(this.id)' id='KBETR-" + posi + "' /></td>";
                        //linea += "<td class='tablaCent'><input class='cell031' type='text' onchange='cambiaMone(this)' id='KONWA-" + posi + "' /></td>";
                        linea += "<td class='tablaCent'><select class='cell031' onchange='cambiaMone(this)' id='KONWA-" + posi + "'>";
                        linea += "<%=monedas %>";
                        linea += "</select></td>";
                        linea += "<td class='tablaCent'><input class='cell031 datepicker' type='text' onchange='cambiaFechA(this.value, this.id);' ondblclick='copiaA(this.value)' onblur='revisaFechaA(this.id);' id='DATAB-" + posi + "' /></td>";
                        linea += "<td class='tablaCent'><input class='cell031 datepicker' type='text' onchange='cambiaFechB(this.value, this.id)' ondblclick='copiaB(this.value)' onblur='revisaFechaA(this.id);' id='DATBI-" + posi + "' /></td>";
                        //linea += "<td class='tablaCent'><input class='cell031' type='text'  id='DATBI-" + posi + "' /></td>";
                        linea += "<td><input class='cell031' type='text' onchange='cambiaCome(this)' id='COMM-" + posi + "' /></td>";

                        //-----BEGIN INSERT 28/04/2017 
                        linea += "<td class='tablaCent'><input type='checkbox' id='chk-" + posi + "' disabled='disabled' /><input type='hidden' id='knumh-" + posi + "' value='' /></td>";
                        linea += "<td class='tablaCent'><input type='button' id='btn-" + posi + "' value='-' class='btn2' onclick='elimina(this.id)' /></td>";
                        //-----END INSERT 28/04/2017 

                        linea += "</tr>";

                        $("#tblTabla tbody").append(linea);
                        //document.getElementById("txtTabla").value += "||||||||X|";
                        //document.getElementById("txtTabla").value += "|0.00||0.00|MXN||||X||M|";
                        document.getElementById("txtTabla").value += "|0.00||0.00|MXN||||X||M2|";//ADD RSG 13.09.2018


                        var input = '<%=fecha_limite %>';
                        var date = new Date();
                        if (input != '') {
                            var parts = input.match(/(\d+)/g);
                            date = new Date(parts[0], parts[1] - 1, parts[2]);
                        }

                        $("#DATBI-" + posi).datepicker({
                            dateFormat: "dd/mm/yy",
                            maxDate: date
                        });
                        $("#DATAB-" + posi).datepicker({
                            dateFormat: "dd/mm/yy",
                            maxDate: date
                        });
                        autocompleta_l("MATNR-" + posi);
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
                        for (var i = 0; i < arr.length - 1; i += 11) {
                            if ((i / 11 + 1) == num) {
                                temp += arr[i] + '|';
                                temp += arr[i + 1] + '|';
                                temp += arr[i + 2] + '|';
                                temp += arr[i + 3] + '|';
                                temp += arr[i + 4] + '|';
                                temp += arr[i + 5] + '|';
                                temp += arr[i + 6] + '|';
                                temp += arr[i + 7] + '|';
                                temp += ' |';
                                temp += arr[i + 9] + '|';
                                temp += arr[i + 10] + '|';
                            } else {

                                temp += arr[i] + '|';
                                temp += arr[i + 1] + '|';
                                temp += arr[i + 2] + '|';
                                temp += arr[i + 3] + '|';
                                temp += arr[i + 4] + '|';
                                temp += arr[i + 5] + '|';
                                temp += arr[i + 6] + '|';
                                temp += arr[i + 7] + '|';
                                temp += arr[i + 8] + '|';
                                temp += arr[i + 9] + '|';
                                temp += arr[i + 10] + '|';
                            }
                        }
                        document.getElementById("txtTabla").value = temp;

                        document.getElementById("tr-" + num).style.display = "none";


                        //} else {
                        //    b = false;
                        //}
                    }
                var escala = [];
                //var escalas = [];
                function escalas(id) {
                    var num = id.split("-")[1];
                    document.getElementById("txtID").value = num;
                    var checked = document.getElementById("chk-" + num).checked;
                    var knumh = document.getElementById("knumh-" + num).value;

                    document.getElementById("KBETR").value = document.getElementById("KBETR-" + num).value;
                    document.getElementById("KONWA").value = document.getElementById("KONWA-" + num).value;
                    document.getElementById("MEINS").value = document.getElementById("MEINS-" + num).value;

                    var kbetr = document.getElementById("KBETR").value;
                    var konwa = document.getElementById("KONWA").value;
                    var meins = document.getElementById("MEINS").value;
                    var matnr = document.getElementById("MATNR-" + num).value;

                    if (matnr.trim() != "" & meins.trim() != "") {
                        document.getElementById("modal").style.display = "block";
                        document.getElementById("materialize-modal-overlay-2").style.display = "block";

                        var table = "<table id='tblTabla1' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'>" +
                            //"<tr><td class='tablahead'>Clase escala</td><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>por</td><td class='tablahead'>UM</td></tr>";
                        "<tr><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>UM</td><td class='tablahead'></td></tr>";
                        table += "<table>";
                        document.getElementById("lblEscala").innerHTML = table;
                    }

                    if (checked && knumh != '' && knumh != undefined) {
                        generaEscalas(knumh, num);

                    } else {

                        var tabla = "";
                        for (var i = 0; i < escala.length; i++) {
                            if (escala[i].id == num && escala[i].activo) {
                                //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                                //$("#tblTabla1 tbody").append("<tr><td><input type='text' class='cell031' id='MENGE-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].cantidad + "' onchange='cambiaMENGE(this.id)'/></td><td class='center'>"
                                //    + meins + "</td><td><input type='text' class='cell031' id='KBETR-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].importe + "' onchange='cambiaKBETR(this.id)' /></td><td class='center'>"
                                //    + konwa + "</td><td class='center'>" + meins + "</td></tr>");
                                //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                                var tabb = "";
                                tabb += "<tr><td><input type='text' class='cell031' id='MENGE-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].cantidad + "' onchange='cambiaMENGE(this.id)'/></td>";
                                tabb += "<td class='center'>";
                                tabb += "<select  class='cell031' id='KONMS-" + escala[i].id + "-" + escala[i].pos + "' onchange='cambiaKONMS(this.id)' style='width: 100px;' ";
                                if (escala[i].pos > 1)
                                    tabb += " disabled='disabled' ";
                                tabb += ">"
                                tabb += "<%=um_meins %>";
                                tabb += "</select></td>";
                                tabb += "<td><input type='text' class='cell031' id='KBETR-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].importe + "' onchange='cambiaKBETR(this.id)' /></td>";
                                tabb += "<td class='center'>" + konwa + "</td>";
                                tabb += "<td class='center'>" + meins + "</td>"
                                tabb += "<td class='center'><input type='button' id='btn-" + escala[i].id + "-" + escala[i].pos + "' value='-' class='btn2' onclick='eliminaEsc(this.id)' /></td>"
                                tabb += "</tr>";

                                $("#tblTabla1 tbody").append(tabb);
                                document.getElementById("KONMS-" + escala[i].id + "-" + escala[i].pos).value = escala[i].meins;
                            }
                        }

                    }
                }
                function cerrar() {
                    document.getElementById("modal").style.display = "none";
                    document.getElementById("materialize-modal-overlay-2").style.display = "none";
                }

                function addEscala() {
                    var importe = document.getElementById("KBETR").value;
                    var moneda = document.getElementById("KONWA").value;
                    var um = document.getElementById("MEINS").value;
                    //var cantidad = document.getElementById("CANT-0").value;
                    //var um2 = document.getElementById("UM2-0").value;
                    //var imp = document.getElementById("IMPO-0").value;

                    var nva_esc = {
                        id: document.getElementById("txtID").value,
                        pos: 0,
                        //cantidad: parseFloat(cantidad),
                        cantidad: 0.0,
                        //importe: parseFloat(imp),
                        importe: 0.0,
                        activo: true,
                        meins: "M2"
                    };

                    var pos_n = 0;
                    for (var i = 0; i < escala.length; i++) {
                        if (escala[i].id == nva_esc.id) {
                            if (escala[i].pos > pos_n) {
                                pos_n = escala[i].pos;
                            }
                            //if (nva_esc.cantidad <= escala[i].cantidad) {
                            //    nva_esc.activo = false;
                            //}
                        }
                    }
                    nva_esc.pos = pos_n + 1;

                    if (nva_esc.activo == true) {
                        escala.push(nva_esc);


                        //$("#tblTabla1 tbody").append("<tr><td></td><td>" + cantidad + "</td><td>" + um + "</td><td>" + imp + "</td><td>" + moneda + "</td><td>1</td><td>" + um + "</td></tr>");
                        //$("#tblTabla1 tbody").append("<tr><td><input type='text' class='cell031' id='MENGE-" + nva_esc.id + "-" + nva_esc.pos + "' onchange='cambiaMENGE(this.id)'/></td><td class='center'>"
                        //    + um + "</td><td><input type='text' class='cell031' id='KBETR-" + nva_esc.id + "-" + nva_esc.pos + "' onchange='cambiaKBETR(this.id)'/></td><td class='center'>"
                        //    + moneda + "</td><td class='center'>" + um + "</td></tr>");
                        var tabb = "";
                        tabb += "<tr><td><input type='text' class='cell031' id='MENGE-" + nva_esc.id + "-" + nva_esc.pos + "' value='" + nva_esc.cantidad + "' onchange='cambiaMENGE(this.id)'/></td>";
                        tabb += "<td class='center'>";
                        tabb += "<select  class='cell031' id='KONMS-" + nva_esc.id + "-" + nva_esc.pos + "' onchange='cambiaKONMS(this.id)' style='width: 100px;' ";
                        if (nva_esc.pos > 1)
                            tabb += " disabled='disabled' ";
                        tabb += ">"
                        tabb += "<%=um_meins %>";
                        tabb += "</select></td>";
                        tabb += "<td><input type='text' class='cell031' id='KBETR-" + nva_esc.id + "-" + nva_esc.pos + "' value='" + nva_esc.importe + "' onchange='cambiaKBETR(this.id)' /></td>";
                        tabb += "<td class='center'>" + moneda + "</td>";
                        tabb += "<td class='center'>" + um + "</td>"
                        tabb += "<td class='center'><input type='button' id='btn-" + nva_esc.id + "-" + nva_esc.pos + "' value='-' class='btn2' onclick='eliminaEsc(this.id)' /></td>"
                        tabb += "</tr>";

                        $("#tblTabla1 tbody").append(tabb);
                        if (nva_esc.pos > 1)
                            nva_esc.meins = document.getElementById("KONMS-" + nva_esc.id + "-1").value;

                        document.getElementById("KONMS-" + nva_esc.id + "-" + nva_esc.pos).value = nva_esc.meins;
                        document.getElementById("chk-" + nva_esc.id).checked = true;
                    }
                }

                function cambiaMENGE(id) {
                    var n_id = id.split('-')[1];
                    var n_pos = id.split('-')[2];
                    var value = parseFloat(document.getElementById(id).value);
                    //if (n_pos == 1) {
                    //    document.getElementById("KBETR-" + n_id).value = value;
                    //    cambiaCant("KBETR-" + n_id);
                    //}
                    var temp = [];
                    var temp2 = [];
                    for (var i = 0; i < escala.length; i++) {
                        if (escala[i].id == n_id) {
                            temp.push(escala[i]);
                        } else {
                            temp2.push(escala[i]);
                        }
                    }
                    for (var i = 0; i < temp.length; i++) {
                        if (temp[i].id == n_id && temp[i].pos == n_pos) {
                            if (temp[i].pos > 1) {
                                if (value > temp[i - 1].cantidad | temp[i - 1].cantidad == 0) {
                                    temp[i].cantidad = value;
                                } else {
                                    temp[i].cantidad = 0.0;
                                    document.getElementById(id).value = "";
                                    value = 0.0;
                                }
                            } else {
                                temp[i].cantidad = value;
                            }
                        }
                    }

                    for (var i = 0; i < temp.length; i++) {
                        if (temp[i].id == n_id && temp[i].pos == n_pos) {
                            if ((i + 1) != temp.length) {
                                if ((i + 1) != temp.length) {
                                    if (value < temp[i + 1].cantidad | temp[i + 1].cantidad == 0) {
                                        temp[i].cantidad = value;
                                    } else {
                                        temp[i].cantidad = 0.0;
                                        document.getElementById(id).value = "";
                                        value = 0.0;
                                    }
                                } else {
                                    temp[i].cantidad = value;
                                }
                            }
                        }
                    }
                    escala = [];
                    for (var i = 0; i < temp.length; i++) {
                        escala.push(temp[i]);
                    }
                    for (var i = 0; i < temp2.length; i++) {
                        escala.push(temp2[i]);
                    }
                }

                    function cambiaKBETR(id) {
                        var n_id = id.split('-')[1];
                        var n_pos = id.split('-')[2];
                        var value = document.getElementById(id).value;
                        if (n_pos == 1) {
                            document.getElementById("KBETR-" + n_id).value = value;
                            cambiaCant("KBETR-" + n_id);
                        }

                        for (var i = 0; i < escala.length; i++) {
                            if (escala[i].id == n_id & escala[i].pos == n_pos) {
                                escala[i].importe = value;
                            }
                        }
                    }

                    function cambiaKONMS(id) {
                        var n_id = id.split('-')[1];
                        var n_pos = id.split('-')[2];
                        var value = document.getElementById(id).value;
                        if (n_pos == 1) {
                            //document.getElementById("KBETR-" + n_id).value = value;
                            //cambiaCant("KBETR-" + n_id);
                        }

                        for (var i = 0; i < escala.length; i++) {
                            if (escala[i].id == n_id) {
                                escala[i].meins = value;
                                document.getElementById("KONMS-" + n_id + "-" + escala[i].pos).value = value;
                            }
                        }
                    }

                    function generaEscalas(knumh, num) {
                        var temp = document.getElementById("P_inc").value;
                        if (temp != "")
                            var porc1 = parseFloat(temp);
                        temp = document.getElementById("P_dec").value;
                        if (temp != "")
                            var porc2 = parseFloat(temp);
                        temp = document.getElementById("C_inc").value;
                        if (temp != "")
                            var cant1 = parseFloat(temp);
                        temp = document.getElementById("C_dec").value;
                        if (temp != "")
                            var cant2 = parseFloat(temp);

                        var porc = 0;
                        var cant = 0;
                        if (porc1 != 0 && porc1 != undefined)
                            porc = porc1
                        if (porc2 != 0 && porc2 != undefined)
                            porc = porc2 * -1;
                        if (cant1 != 0 && cant1 != undefined)
                            cant = cant1
                        if (cant2 != 0 && cant2 != undefined)
                            cant = cant2 * -1;


                        var param = { kn: knumh };
                        $.ajax({
                            url: "../../catalogos.asmx/tablaEscalas",
                            data: JSON.stringify(param),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                var tabEsc = data.d.split('|');
                                for (var i = 0; i < tabEsc.length - 1; i += 4) {
                                    if (porc != 0) {
                                        var nva_esc = {
                                            id: num,
                                            pos: i + 1,
                                            cantidad: parseFloat(tabEsc[i + 2]),
                                            importe: nvoPrecio(parseFloat(tabEsc[i + 3]), porc),
                                            activo: true,
                                            meins: tabEsc[i]
                                        };
                                    } else {
                                        var nva_esc = {
                                            id: num,
                                            pos: i + 1,
                                            cantidad: parseFloat(tabEsc[i + 2]),
                                            importe: parseFloat(tabEsc[i + 3]) + cant,
                                            activo: true,
                                            meins: tabEsc[i]
                                        };
                                    }
                                    escala.push(nva_esc);

                                }
                                var moneda = document.getElementById("KONWA").value;
                                var um = document.getElementById("MEINS").value;

                                var tabla = "";
                                for (var i = 0; i < escala.length; i++) {
                                    if (escala[i].id == num) {
                                        //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                                        var tabb = "";
                                        tabb += "<tr><td><input type='text' class='cell031' id='MENGE-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].cantidad + "' onchange='cambiaMENGE(this.id)'/></td>";
                                        tabb += "<td class='center'>";
                                        tabb += "<input type='text' class='cell031' id='KONMS-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].meins + "' onchange='cambiaMENGE(this.id)'/>";
                                        tabb += "</td>";
                                        tabb += "<td><input type='text' class='cell031' id='KBETR-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].importe + "' onchange='cambiaKBETR(this.id)' /></td>";
                                        tabb += "<td class='center'>" + moneda + "</td>";
                                        tabb += "<td class='center'>" + um + "</td></tr>";

                                        $("#tblTabla1 tbody").append(tabb);
                                    }
                                }
                                document.getElementById("knumh-" + num).value = "";
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {

                            }
                        });
                    }

                    function nvoPrecio(prec, p) {
                        if (p != 0) {
                            var num = prec * p / 100;
                            prec = prec + num;

                            //var num2 = Math.trunc(prec * 10);
                            var num2 = (prec * 10).toString().split('.')[0]
                            num2 = (prec * 10) - num2;
                            prec = (prec * 10).toString().split('.')[0]
                            if (num2 != 0)
                                prec++;
                            prec = prec / 10;
                            //prec = Math.round(prec * 100) / 100;
                            prec = (prec * 100) / 100;

                            //return string.Format("{0:0.00}", prec);
                            return prec;
                        }
                    }

                    function eliminaEsc(id) {
                        var n_id = id.split('-')[1];
                        var n_pos = id.split('-')[2];
                        for (var i = 0; i < escala.length; i++) {
                            if (escala[i].id == n_id && escala[i].pos == n_pos) {
                                escala[i].activo = false;
                            }
                        }
                        var temp = [];
                        var poss = 0;
                        for (var i = 0; i < escala.length; i++) {
                            if (escala[i].id == n_id) {
                                if (escala[i].activo) {
                                    if (escala[i].pos == 1) {
                                        poss = 1;
                                    } else {
                                        escala[i].pos = poss + 1;
                                    }
                                    poss++;
                                    temp.push(escala[i]);
                                }
                            } else {
                                temp.push(escala[i]);
                            }
                        }
                        escala = temp;
                        escalas(id);
                        //alert(id);
                    }
                    //-----END INSERT 28/04/2017 
            </script>

            <div class="row">
                <div class="col s12 m12 l12">
                    <label runat="server" id="lblTabla"></label>
                </div>
            </div>
            <link href="../css/jquery-ui2.css" rel="stylesheet" />
            <script src="../js/jquery-ui.js"></script>
            <script>
                $(function () {
                    var input = '<%=fecha_limite %>';
                    var date = new Date();
                    if (input != '') {
                        var parts = input.match(/(\d+)/g);
                        date = new Date(parts[0], parts[1] - 1, parts[2]);
                        //$("#datepicker").datepicker();
                        $(".datepicker").datepicker({
                            dateFormat: "dd/mm/yy",
                            maxDate: date
                        });
                    }

                });

                function borraMatnr(id, valor) {
                    var valores = valor.split(' ');
                    document.getElementById(id).value = valores[0];
                }

                function copiaA(valor) {
                    var pos = document.getElementById("txtPos").value;
                    for (var i = 1; i <= pos; i++) {
                        document.getElementById("DATAB-" + i).value = valor;
                        cambiaFechA(valor, "DATAB-" + i);
                    }
                }
                function copiaB(valor) {
                    var pos = document.getElementById("txtPos").value;
                    for (var i = 1; i <= pos; i++) {
                        document.getElementById("DATBI-" + i).value = valor;
                        cambiaFechB(valor, "DATBI-" + i);
                    }
                }
            </script>
            <div class="row">
                <div class="col s12 m12 l12">
                    Comentarios:<br />
                    <%--<textarea runat="server" id="txtCOMM" style="width: 100%" rows="6"></textarea>--%>
                    <script src="../js/materialize.js"></script>
                    <div class="input-field col s12">
                        <textarea runat="server" id="txtCOMM" class="materialize-textarea" data-length="120" style="width: 100%; font-family: Oswald" rows="2"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="hidden" runat="server" id="txtTabla" />
                    <input type="hidden" runat="server" id="txtPos" value="0" />
                    <input type="hidden" runat="server" id="txtTipo" value="2" />
                    <input type="button" runat="server" value="Enviar" id="btnSubmit" disabled="disabled" class="btn right" onclick="enviar();" />&nbsp;&nbsp;&nbsp;&nbsp;
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
        <script>
            function enviar() {
                var error = false;
                var error2 = false;
                var cont = 0;
                var tabla = document.getElementById("txtTabla").value.split('|');
                var escalasTab = "";
                for (var i = 0; i < tabla.length - 1; i += 11) {
                    if (tabla[i + 8] == "X") {
                        cont++;
                        if (tabla[i].trim() == "")
                            error = true;
                        if (tabla[i + 3].trim() == "")
                            error = true;
                        if (tabla[i + 5].trim() == "")
                            error = true;
                        else {
                            revisaFechaA("DATAB-" + ((i / 11) + 1));
                            if (!error2)
                                error2 = revisaFechaA("DATAB-" + ((i / 11) + 1));
                        }
                        if (tabla[i + 6].trim() == "")
                            error = true;
                        else {

                            revisaFechaA("DATAB-" + ((i / 11) + 1));
                            if (!error2)
                                error2 = revisaFechaA("DATBI-" + ((i / 11) + 1));
                        }
                        for (var j = 0; j < escala.length; j++) {
                            if (escala[j].id == (i / 11) + 1) {
                                escalasTab += tabla[i] + "|" + escala[j].id + "|" + escala[j].pos + "|" + escala[j].cantidad + "|" + escala[j].importe + "|" + escala[j].meins + "|";
                            }
                        }
                    }
                }
                if (cont > 0) {
                    if (!error) {
                        if (!error2) {
                            document.getElementById("txtEscalas").value = escalasTab;
                            document.getElementById("modal1").style.visibility = "visible";
                            $('form').get(0).setAttribute('action', './EnviaSolicitud.aspx'); //this works
                            document.forms["form1"].submit();
                        } else {
                            alert("Favor de corregir fechas");
                        }
                    } else {
                        alert("LLene los datos necesarios");
                    }
                } else {
                    alert("Agregue por lo menos una posición");
                }
            }
        </script>
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />

        <div id="modal" class="modal modal-fixed-footer open" style="z-index: 1003; opacity: 1; transform: scaleX(1); top: 10%;">
            <div class="modal-content">
                <label style="font-size: 20px;">Escalas</label>
                <div class="row">
                    <input type="button" value="Agregar" class="btn right" onclick="addEscala();" />
                </div>

                <label id="lblEscala" runat="server"></label>
                <input type="hidden" id="txtEscalas" runat="server" />
                <input type="hidden" id="KBETR" runat="server" />
                <input type="hidden" id="KONWA" runat="server" />
                <input type="hidden" id="MEINS" runat="server" />

            </div>
            <div class="modal-footer" style="width: 100%;">
                <input type="button" class="btn right" value="Aceptar" onclick="cerrar()" />
                <input type="hidden" runat="server" id="txtID" />
            </div>
        </div>
        <div class="modal-overlay" id="materialize-modal-overlay-2" style="z-index: 1002; display: none; opacity: 0.5;"></div>
    </form>
</body>
</html>
