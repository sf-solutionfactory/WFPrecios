<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelLP.aspx.cs" Inherits="WFPrecios.ListasPrecio.ExcelLP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Cargar Excel</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
    <link href="../css/datepicker.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        //$('form1').attr('action', 'baz'); //this fails silently
        function enviar() {
            $('form').get(0).setAttribute('action', './EnviaSolicitud.aspx'); //this works
            document.forms["form1"].submit();
        }
        function cargar() {
            document.getElementById("modal1").style.visibility = "visible";
            $('form').get(0).setAttribute('action', './ExcelLP.aspx'); //this works
            document.forms["form1"].submit();
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
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Listas de precios / Nueva Solicitud
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <a href="../files/LAYOUT%20LISTAS%20PRECIOS.xlsx">Descargar Layout</a>&nbsp;|&nbsp;<a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container">
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col s12 m12 l12">
                    <div class="center">
                        Ruta:
                    <input type="file" runat="server" id="xlsFile" name="xlsFile" class="cell07 center" style="width: 500px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="button" value="Cargar" class="btn right" runat="server" onclick="cargar()" />
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
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
                    <input type="button" runat="server" onclick="enviar()" value="Enviar" id="btnSubmit" disabled="disabled" class="btn right" />
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
