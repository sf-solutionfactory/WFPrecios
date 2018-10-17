<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnviaSolicitud.aspx.cs" Inherits="WFPrecios.Precios.tabs.EnviaSolicitud" %>

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
