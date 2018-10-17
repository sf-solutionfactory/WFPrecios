<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFPrecios.Precios.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>WF Precios</title>
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
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Precios
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href="Solicitud.aspx">Cambio en Precios</a>&nbsp;|&nbsp;<a href="SolicitudPC.aspx">Precio para pedido</a>&nbsp;|&nbsp;<a href="SolicitudLO.aspx">Precio por Lote</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="Solicitud.aspx">Cambio en Precios</a>&nbsp;|&nbsp;<a href="SolicitudPC.aspx">Precio para pedido</a>&nbsp;|&nbsp;<a href="SolicitudLO.aspx">Precio por Lote</a>&nbsp;|&nbsp;<a href="../Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <div class="row">
                <br />
                <div runat="server" id="delegar">
                    <div class="row">
                        <div class="col l2 m2 s12"></div>
                        <div class="col center-align l8 m8 s8" style="color: White; font-family: Oswald; font-size: 18pt; background-color: rgb(57, 59, 64);">
                            Selección de Usuario
                        </div>
                    </div>

                    <div class="center-align">
                        Usuario
                            
                <%--<select class="cell03" name="txtDELEGA" runat="server" id="txtDELEGA" onchange="eligeUSER(this.value)" style="width: 400px;">
                </select>--%>
                        <asp:DropDownList Style="width: 400px;" CssClass="cell03" ID="txtDELEGAR" runat="server" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="txtDELEGAR_SelectedIndexChanged" OnTextChanged="txtDELEGAR_TextChanged"></asp:DropDownList>

                    </div>
                </div>
                <br />
                <a href="Busqueda.aspx" class="btn right">Historial</a>
                <table id="Table9" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                    <tbody>
                        <tr>
                            <td class="cell08">Resumen de solicitudes</td>
                        </tr>
                    </tbody>
                </table>
                <table style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                    <tr>
                        <th class="tablahead">ID</th>
                        <%--<th class="tablahead">Usuario</th>--%>
                        <th class="tablahead">Fecha</th>
                        <th class="tablahead">Tipo</th>
                        <th class="tablahead">Estatus</th>
                        <th class="tablahead">Por autorizar</th>
                        <th class="tablahead">Ver</th>
                    </tr>

                    <label runat="server" id="lblTabla"></label>
                </table>
            </div>
        </div>
        <input type="hidden" runat="server" id="hidNumEmp" />
        <input type="hidden" runat="server" id="hidUsuario" />
        <input type="hidden" runat="server" id="hidTipoEmp" />
        <input type="hidden" runat="server" id="hidDescUsuario" />
    </form>
</body>
</html>
