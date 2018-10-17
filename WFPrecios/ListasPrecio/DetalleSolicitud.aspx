<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleSolicitud.aspx.cs" Inherits="WFPrecios.ListasPrecio.DetalleSolicitud" %>

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
                        <%--<a href="Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">

            <div class="row">
                <div class="col s12 m12 l12 center">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr>
                            <td class="cell03">Folio</td>
                            <td>
                                <input type="text" runat="server" id="txtFolio" class="cell05" disabled="disabled" /></td>
                        </tr>
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
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />
    </form>
</body>
</html>
