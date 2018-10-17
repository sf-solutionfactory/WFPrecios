<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Busqueda.aspx.cs" Inherits="WFPrecios.ListasPrecio.Busqueda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Historial de cambios en Precios</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.js"></script>
    <script src="../js/jquery-ui-1.11.4.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script>
        var ban = false;
        function autocompleta_c(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var vko = $('#txtVKORG').val();
                    var vtw = $('#txtVTWEG').val();
                    var spa = $('#txtSPART').val();
                    var param = { vk: vko, vt: vtw, sp: spa, ku: para.toUpperCase() };
                    $.ajax({
                        url: "../catalogos.asmx/auto_clientesB",
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
                    document.getElementById("btnCargar").disabled = false;
                    ban = true;
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }
        function autocompleta_p(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { pr: para.toUpperCase() };
                    $.ajax({
                        url: "../catalogos.asmx/auto_pernr",
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
                    //document.getElementById("btnCargar").disabled = false;
                    //ban = true;
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }

        function autocompleta_l(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { pl: para.toUpperCase() };
                    $.ajax({
                        url: "../catalogos.asmx/auto_pltypB",
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
                    document.getElementById("btnCargar").disabled = false;
                    ban = true;
                },
                minLength: 1 //Longitud mínima para calcularse
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">Historial de cambios en Precios
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <div class="row">
                <div class="col s12 m12 l6">
                    <table style="">
                        <tr>
                            <td class="cell03">Org. de ventas</td>
                            <td>
                                <select class="cell03" name="txtVKORG" runat="server" id="txtVKORG" onchange="">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Canal Dist.</td>
                            <td>
                                <select class="cell03" name="txtVTWEG" runat="server" id="txtVTWEG" onchange="">
                                </select></td>
                        </tr>
                        <tr>
                            <td class="cell03">Sector</td>
                            <td>
                                <select class="cell03" name="txtSPART" runat="server" id="txtSPART" onchange="">
                                </select></td>
                        </tr>
                        <tr>
                            <td class="cell03">Solicitante</td>
                            <td>
                                <input type="text" class="cell07" name="txtPERNR" runat="server" id="txtPERNR" onchange="" />
                                <script type="text/javascript">autocompleta_p("txtPERNR");</script>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Lista de precios anterior</td>
                            <td>
                                <input class="cell07" type="text" name="txtPLTYP" id="txtPLTYP1" runat="server" onchange="" />
                                <script type="text/javascript">autocompleta_l("txtPLTYP1");</script>
                            </td>
                        </tr><tr>
                            <td class="cell03">Lista de precios nueva</td>
                            <td>
                                <input class="cell07" type="text" name="txtPLTYP" id="txtPLTYP2" runat="server" onchange="" />
                                <script type="text/javascript">autocompleta_l("txtPLTYP2");</script>
                            </td>
                        </tr>
                        
                       <%-- <tr>
                            <td class="cell03">Cliente</td>
                            <td>
                                <input class="cell07" type="text" name="txtKUNNR" id="txtKUNNR" runat="server" required="required" onchange="" />
                                <script type="text/javascript">autocompleta_c("txtKUNNR");</script>
                            </td>
                        </tr>--%>
                    </table>
                </div>
                <div class="col s12 m12 l6">
                    <table style="">
                        <tr>
                            <td class="cell03">Fecha de solicitud</td>
                            <td>
                                <input type="text" class="cell03 datepicker" name="txtDATEs" runat="server" id="txtDATEs" onchange="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Fecha de autorización</td>
                            <td>
                                <input type="text" class="cell03 datepicker" name="txtDATEa" runat="server" id="txtDATEa" onchange="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Cliente de</td>
                            <td>
                                <input class="cell07" type="text" name="txtKUNNR" id="txtKUNNR1" runat="server" required="required" onchange="" />
                                <script type="text/javascript">autocompleta_c("txtKUNNR1");</script>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Cliente a</td>
                            <td>
                                <input class="cell07" type="text" name="txtKUNNR" id="txtKUNNR2" runat="server" required="required" onchange="" />
                                <script type="text/javascript">autocompleta_c("txtKUNNR2");</script>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="button" value="Buscar" class="btn right" runat="server" onclick="buscar()" id="btnCargar" />
                    <input type="button" value="Limpiar" class="btn left" runat="server" id="btnLimpiar" onclick="limpiar()" />
                </div>
            </div>

            <div class="row">
                <div class="col s12 m12 l12">
                    <label runat="server" id="lblTabla"></label>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />
    </form>
    <link href="../css/jquery-ui2.css" rel="stylesheet" />
    <script src="../js/jquery-ui.js"></script>
    <script>
        $(function () {
            $(".datepicker").datepicker({
                dateFormat: "dd/mm/yy",
            });
        });

        function buscar() {
            $('form').get(0).setAttribute('action', './Busqueda.aspx'); //this works
            document.forms["form1"].submit();
        }
        function limpiar() {
            document.getElementById("txtVKORG").value = "";
            document.getElementById("txtVTWEG").value = "";
            document.getElementById("txtSPART").value = "";
            document.getElementById("txtPERNR").value = "";
            document.getElementById("txtPLTYP1").value = "";
            document.getElementById("txtPLTYP2").value = "";
            //document.getElementById("txtKUNNR").value = "";
            document.getElementById("txtDATEs").value = "";
            document.getElementById("txtDATEa").value = "";
            document.getElementById("txtKUNNR1").value = "";
            document.getElementById("txtKUNNR2").value = "";
        }
    </script>
</body>
</html>
