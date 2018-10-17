<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WFPrecios.ListasPrecio.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>WF Listas de precios</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
    <script type="text/javascript">
       <%--<%-- function eligeUSER(pernr) {
            alert(pernr);
            if (pernr == "-") {
                <%Session["Usuario"] = Session["UsuarioP"]; %>;
                <%Session["NumEmp"] = Session["NumEmpP"]; %>;
                <%Session["TipoEmp"] = Session["TipoEmpP"]; %>;
            } else {
                var param = { pr: pernr };
                $.ajax({
                    url: "Default.aspx/setSession",
                    data: JSON.stringify(param),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                    }
                });
            }
            document.getElementById("form1").submit();
        }--%>--%>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Listas de precios
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <a href="SolicitudLP.aspx">Nueva solicitud</a>&nbsp;|&nbsp;<a href="../Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
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
