<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Procesa.aspx.cs" Inherits="WFPrecios.Precios.Procesa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorización de Solicitud</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="robots" content="noodp,noydir" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <link href="http://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" type="text/css" />
    <link href="http://cdnjs.cloudflare.com/ajax/libs/normalize/3.0.1/normalize.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" action="Autoriza.aspx">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="cell01">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="cell02">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">
                            <label id="txtFolio" runat="server"></label>
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="../../Default.aspx">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <label runat="server" id="lblLinks"></label></td>
                </tr>
            </table>
        </div>
         <div class="container" id="tablas">
            <br />
            <div class="row">
                <div class="col s12 m12 l12 center">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr>
                            <td class="cell03">Solicitante</td>
                            <td class="cell05">
                                <label runat="server" id="txtPERNR"></label>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell03">Fecha de solicitud</td>
                            <td class="cell05">
                                <label runat="server" id="txtDATE"></label>
                            </td>
                        </tr>
                    </table>
                    <label runat="server" id="lblFolio"></label>
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
            <br />
            <div class="row" style="text-align: left">
                <div class="col s12 m12 l12">
                    Comentarios:
                    <textarea runat="server" id="txtCOMM" disabled="disabled" class="materialize-textarea black-text" data-length="255"></textarea>
                    <%--<script src="../js/jquery-1.10.2.js"></script>--%>
                    <script>
                        var cont = document.getElementById("txtCOMM").value;
                        var lineas = cont.split('\n').length;
                        var style = "width: 100%; resize: none; font-family: Oswald; height:" + (lineas * 25) + "px;"
                        document.getElementById("txtCOMM").style = style;
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
        <div class="container" id="comentarios">
            Comentario:
            <textarea runat="server" id="txtCOMM2" class="materialize-textarea black-text" data-length="200" style="font-family:Oswald"></textarea>
            <input type="submit" runat="server" id="btnSubmit"  value="Enviar" class="btn right" />
        </div>
        <script>
            var Oper = '<%=oper%>';
            if (Oper == "N") {
                document.getElementById("comentarios").style.display = "none";
            }
            if (Oper == "A" | Oper == "R") {
                document.getElementById("tablas").style.display = "none";
            }
        </script>
        <div>
            <input type="hidden" runat="server" id="hidTipo" />
            <input type="hidden" runat="server" id="hidFolio" />
            <input type="hidden" runat="server" id="hidOper" />
            <input type="hidden" runat="server" id="hidPosi" />
        </div>
    </form>
</body>
</html>
