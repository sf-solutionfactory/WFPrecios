<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleSolicitud.aspx.cs" Inherits="WFPrecios.Precios.DetalleSolicitud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Detalle</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">Gestión de precios
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <br />
            <div class="row">
                <div class="col s12 m12 l12 center">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr>
                            <td class="cell03">Folio</td>
                            <td>
                                <input type="text" runat="server" id="txtFolio" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr1">
                            <td class="cell03">Org. de ventas</td>
                            <td>
                                <input type="text" runat="server" id="txtVKORG" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr2">
                            <td class="cell03">Canal de distribución</td>
                            <td>
                                <input type="text" runat="server" id="txtVTWEG" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr>
                            <td class="cell03">Sector</td>
                            <td>
                                <input type="text" runat="server" id="txtSPART" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr3">
                            <td class="cell03">Cliente</td>
                            <td>
                                <input type="text" runat="server" id="txtKUNNR" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr4">
                            <td class="cell03">Lista de precio</td>
                            <td>
                                <input type="text" runat="server" id="txtPLTYP" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr5">
                            <td class="cell03">Material</td>
                            <td>
                                <input type="text" runat="server" id="txtMATNR" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr>
                            <td class="cell03">Incremento</td>
                            <td>
                                <input type="text" runat="server" id="txtPORC" class="cell05" disabled="disabled" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table9" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tbody>
                            <tr>
                                <td class="cell08">Detalle</td>
                            </tr>
                        </tbody>
                    </table>
                    <label runat="server" id="lblTabla"></label>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    Comentarios:
                    <textarea runat="server" id="txtCOMM" disabled="disabled" class="materialize-textarea black-text" data-length="255"></textarea>
                    <script src="../js/jquery-1.10.2.js"></script>
                    <script>
                        //BEGIN OF INSERT RSG 12/05/2017

                        //function escalas(id) {
                        //    var num = id.split('-')[1];
                        //    var ischk = document.getElementById("chk-" + num).checked;
                        //    if (ischk) {

                        //    }
                        //}

                        var escala = [];
                        function escalas(id) {
                            var num = id.split("-")[1];
                            document.getElementById("txtID").value = num;
                            var checked = document.getElementById("chk-" + num).checked;

                            if (checked) {
                                //document.getElementById("KBETR").value = document.getElementById("KBETR-" + num).value;
                                document.getElementById("KONWA").value = document.getElementById("KONWA-" + num).innerText;
                                document.getElementById("MEINS").value = document.getElementById("MEINS-" + num).innerText;

                                var kbetr = document.getElementById("KBETR").value;
                                var konwa = document.getElementById("KONWA").value;
                                var meins = document.getElementById("MEINS").value;
                                //var matnr = document.getElementById("MATNR-" + num).value;

                                document.getElementById("modal").style.display = "block";
                                document.getElementById("materialize-modal-overlay-1").style.display = "block";

                                var table = "<table id='tblTabla1' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'>" +
                                    //"<tr><td class='tablahead'>Clase escala</td><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>por</td><td class='tablahead'>UM</td></tr>";
                                "<tr><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>por</td><td class='tablahead'>UM</td></tr>";
                                table += "<table>";
                                document.getElementById("lblEscala").innerHTML = table;


                                generaEscalas(num);

                            } //else {

                            //    var tabla = "";
                            //    for (var i = 0; i < escala.length; i++) {
                            //        if (escala[i].id == num) {
                            //            //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                            //            $("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "/></td><td class='center'>"
                            //                + meins + "</td><td>" + escala[i].importe + " /></td><td class='center'>"
                            //                + konwa + "</td><td>1</td><td class='center'>" + meins + "</td></tr>");

                            //        }
                            //    }

                            //}
                        }
                        function cerrar() {
                            document.getElementById("modal").style.display = "none";
                            document.getElementById("materialize-modal-overlay-1").style.display = "none";
                        }

                        function getURLParameter(name) {
                            return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [null, ''])[1].replace(/\+/g, '%20')) || null;
                        }
                        function generaEscalas(num) {
                            var id = getURLParameter("Folio");
                            var param = { id: id, pos: num };
                            $.ajax({
                                url: "../catalogos.asmx/tablaEscalasSol",
                                data: JSON.stringify(param),
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                dataFilter: function (data) { return data; },
                                success: function (data) {
                                    var tabEsc = data.d.split('|');
                                    for (var i = 0; i < tabEsc.length - 1; i += 5) {
                                        var nva_esc = {
                                            id: num,
                                            pos: i + 1,
                                            cantidad: parseFloat(tabEsc[i + 2]),
                                            importe: parseFloat(tabEsc[i + 3]),
                                            activo: true,
                                            meins: tabEsc[i+4]
                                        };
                                        escala.push(nva_esc);

                                    }
                                    var moneda = document.getElementById("KONWA").value;
                                    var um = document.getElementById("MEINS").value;

                                    var tabla = "";
                                    for (var i = 0; i < escala.length; i++) {
                                        if (escala[i].id == num) {
                                            //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                                            $("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td class='center'>"
                                                + escala[i].meins + "</td><td>" + escala[i].importe + "</td><td class='center'>"
                                                + moneda + "</td><td>1</td><td class='center'>" + um + "</td></tr>");
                                        }
                                    }
                                    //document.getElementById("knumh-" + num).value = "";
                                    escala = [];
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {

                                }
                            });
                        }

                        //END OF INSERT RSG 12/05/2017

                        var cont = document.getElementById("txtCOMM").value;
                        var lineas = cont.split('\n').length;
                        var style = "width: 100%; resize: none; font-family: Oswald; height:" + (lineas * 50) + "px;"
                        document.getElementById("txtCOMM").style = style;

                        addEventListener("load", inicio, false);
                        function inicio() {
                            var tipo = '<%=tipo %>';
                            if (tipo.substring(0, 1) == "C") {
                                document.getElementById("tr4").className = "hide";
                                document.getElementById("tr5").className = "hide";
                            } else if (tipo.substring(0, 1) == "S") {
                                document.getElementById("tr1").className = "hide";
                                document.getElementById("tr2").className = "hide";
                                document.getElementById("tr3").className = "hide";
                                document.getElementById("tr4").className = "hide";
                                document.getElementById("tr5").className = "hide";
                            } else if (tipo.substring(0, 1) == "L") {
                                document.getElementById("tr1").className = "hide";
                                document.getElementById("tr2").className = "hide";
                                document.getElementById("tr3").className = "hide";
                                document.getElementById("tr5").className = "hide";
                            } else if (tipo.substring(0, 1) == "M") {
                                document.getElementById("tr1").className = "hide";
                                document.getElementById("tr2").className = "hide";
                                document.getElementById("tr3").className = "hide";
                                document.getElementById("tr4").className = "hide";
                            }
                        }
                    </script>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table10" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                        <tbody>
                            <tr>
                                <td class="cell08">Bitácora</td>
                            </tr>
                        </tbody>
                    </table>
                    <label runat="server" id="lblBItacora"></label>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />

        <div id="modal" class="modal modal-fixed-footer open" style="z-index: 1003; opacity: 1; transform: scaleX(1); top: 10%;">
            <div class="modal-content">
                <label style="font-size: 20px;">Escalas</label>
                <div class="row">
                    <%--<input type="button" value="Agregar" class="btn right" onclick="addEscala();" />--%>
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
        <div class="modal-overlay" id="materialize-modal-overlay-1" style="z-index: 1002; display: none; opacity: 0.5;"></div>
    </form>
</body>
</html>
