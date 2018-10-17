<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviaExcel.aspx.cs" Inherits="WFPrecios.Precios.tabs.EnviaExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Solicitud enviada</title>
    <link href="../../css/style.css" rel="stylesheet" />
    <link href="../../css/materialize.css" rel="stylesheet" />
    <%--<link href="../css/datepicker.css" rel="stylesheet" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Listas de precios/Nueva Solicitud
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <a href="../Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <br />
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col s12 m12 l12 center">
                    <%--La solicitud de modificación de Listas de precio ha sido recibida, y será procesada con el folio<br />--%>
                    <label id="lblFolio" runat="server"></label>
                </div>
            </div>

            <br />
            <br />
            <div class="row">
                <div class="col s12 m12 l12 center">
                    <a href="../Default.aspx" target="_parent" class="btn right">Regresar</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
