<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelTab.aspx.cs" Inherits="WFPrecios.Precios.tabs.ExcelTab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Cargar Excel</title>
    <link href="../../css/style.css" rel="stylesheet" />
    <link href="../../css/materialize.css" rel="stylesheet" />
    <%--<link href="../../css/datepicker.css" rel="stylesheet" />--%>
    <script src="../../js/jquery-1.9.1.js"></script>
    <script type="text/javascript">

        addEventListener("load", inicio, false);
       
        function inicio() {
            var id = <%=tipo%> ;
            document.getElementById("txtTipo").value = id;
            for (var i = 1; i < 7; i++) {
                document.getElementById(i).className = "white-text";
            }
            document.getElementById(id).className = "active white-text";
            
            $(document).ready(function(){
                $('ul.tabs').tabs();
            });
        }

        function enviar() {
            $('form').get(0).setAttribute('action', './EnviaExcel.aspx'); //this works
            document.forms["form1"].submit();
        }
        function cargar() {
            document.getElementById("modal1").style.visibility = "visible";
            $('form').get(0).setAttribute('action', './ExcelTab.aspx'); //this works
            document.forms["form1"].submit();
        }
        function cambiaMenu(id) {
            var tipo = "";
            var link = "";
            switch (id) {
                case "1":
                    tipo = "Sector/Cliente/Material";
                    link = "<a href='../../files/LAYOUT%20CLIENTE-MATERIAL.xlsx'>Descargar Layout</a>";
                    break;
                case "2":
                    tipo = "Sector/Material";
                    link = "<a href='../../files/LAYOUT%20SECTOR-MATERIAL.xlsx'>Descargar Layout</a>";
                    break;
                case "3":
                    tipo = "Sector/Cliente/Grupo de artículos";
                    link = "<a href='../../files/LAYOUT%20CLIENTE-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                    break;
                case "4":
                    tipo = "Sector/Lista de precios/Material";
                    link = "<a href='../../files/LAYOUT%20LP-MATERIAL.xlsx'>Descargar Layout</a>";
                    break;
                case "5":
                    tipo = "Sector/Lista de precios/Grupo de artículos";
                    link = "<a href='../../files/LAYOUT%20LP-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                    break;
                case "6":
                    tipo = "Sector/Grupo de artículos";
                    link = "<a href='../../files/LAYOUT%20SECTOR-GPO%20ARTICULOS.xlsx'>Descargar Layout</a>";
                    break;
            }
            
            document.getElementById("lblTipo").innerText = tipo;
            document.getElementById("lblTabla").innerText = "";
            document.getElementById("txtTipo").value = id;
            document.getElementById("btnSubmit").disabled = true;
            document.getElementById("lblLink").innerHTML = link;
            
            document.getElementById("tr1").style.visibility = "hidden";
            document.getElementById("tr1").style.display = "none";
            document.getElementById("tr2").style.visibility = "hidden";
            document.getElementById("tr2").style.display = "none";
            document.getElementById("tr3").style.visibility =  "hidden";
            document.getElementById("tr3").style.display = "none";
            document.getElementById("tr4").style.visibility =  "hidden";
            document.getElementById("tr4").style.display = "none";
            document.getElementById("tr5").style.visibility = "hidden";
            document.getElementById("tr5").style.display = "none";
        }

        
        var escala = [];
        //var escalas = [];
        function escalas(id) {
            var num = id.split("-")[1];
            document.getElementById("txtID").value = num;
            var checked = document.getElementById("chk-" + num).checked;
            //var knumh = document.getElementById("knumh-" + num).value;

            //document.getElementById("KBETR").value = document.getElementById("KBETR-" + num).value;
            document.getElementById("KONWA").value = document.getElementById("KONWA-" + num).value;
            //document.getElementById("MEINS").value = document.getElementById("MEINS-" + num).value;
            //ADD RSG 27.11.2017
            document.getElementById("MEINS").value = document.getElementById("MEINSE-" + num).value;

            //var kbetr = document.getElementById("KBETR").value;
            var konwa = document.getElementById("KONWA").value;
            var meins = document.getElementById("MEINS").value;
            var matnr = document.getElementById("MATNR-" + num).value;

            if (matnr.trim() != "" && meins.trim() != "" && checked) {
                document.getElementById("modal").style.display = "block";
                document.getElementById("materialize-modal-overlay-2").style.display = "block";

                var table = "<table id='tblTabla1' border='0' style='border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;'>" +
                    //"<tr><td class='tablahead'>Clase escala</td><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>por</td><td class='tablahead'>UM</td></tr>";
                "<tr><td class='tablahead'>Cantidad de escala</td><td class='tablahead'>UM</td><td class='tablahead'>Importe</td><td class='tablahead'>Moneda</td><td class='tablahead'>UM</td></tr>";
                table += "<table>";
                document.getElementById("lblEscala").innerHTML = table;

            //if (checked && knumh != '' && knumh != undefined) {
            //    generaEscalas(knumh, num);

            //} else {

            var tabla = "";
            for (var i = 0; i < escala.length; i++) {
                if (escala[i].id == num && escala[i].activo) {
                    //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                    //$("#tblTabla1 tbody").append("<tr><td><input type='text' class='cell031' id='MENGE-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].cantidad + "' onchange='cambiaMENGE(this.id)'/></td><td class='center'>"
                    //    + meins + "</td><td><input type='text' class='cell031' id='KBETR-" + escala[i].id + "-" + escala[i].pos + "' value='" + escala[i].importe + "' onchange='cambiaKBETR(this.id)' /></td><td class='center'>"
                    //    + konwa + "</td><td class='center'>" + meins + "</td></tr>");
                    //$("#tblTabla1 tbody").append("<tr><td>" + escala[i].cantidad + "</td><td>" + meins + "</td><td>" + escala[i].importe + "</td><td>" + konwa + "</td><td>1</td><td>" + meins + "</td></tr>");
                    var tabb = "";
                    tabb += "<tr><td>" + escala[i].cantidad + "</td>";
                    tabb += "<td class='center'>";
                    tabb += escala[i].meins;
                    tabb += "</td>";
                    tabb += "<td>" + escala[i].importe + "</td>";
                    tabb += "<td class='center'>" + konwa + "</td>";
                    tabb += "<td class='center'>" + meins + "</td>"
                    tabb += "</tr>";

                    $("#tblTabla1 tbody").append(tabb);
                    //document.getElementById("KONMS-" + escala[i].id + "-" + escala[i].pos).value = escala[i].meins;
                }
            }
                
            }
            //}
        }
        function cerrar() {
            document.getElementById("modal").style.display = "none";
            document.getElementById("materialize-modal-overlay-2").style.display = "none";
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

        function generaEscalas() {
            var tab = document.getElementById("txtEscalas").value;
            var tabEsc = tab.split('|');
            var poss = 0;
            for (var i = 0; i < tabEsc.length - 1; i += 6) {
                poss++;
                var nva_esc = {
                    id: tabEsc[i+1],
                    pos: poss,
                    cantidad: parseFloat(tabEsc[i + 3]),
                    importe: parseFloat(tabEsc[i + 4]),
                    activo: true,
                    meins: tabEsc[i+5]
                };
                escala.push(nva_esc);

            }
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
                        <%--<a href="../Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="https://www.terzaonline.com/nworkflow/login/">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <label runat="server" id="lblLink"></label>
                        &nbsp;|&nbsp;<a href="../Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="grey">
            <ul class="tabs grey center-align">
                <li class="tab grey"><a id="1" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/Cte/Mat</a></li>
                <li class="tab grey"><a id="2" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/Mat</a></li>
                <li class="tab grey"><a id="3" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/Cte/Gpo Art</a></li>
                <li class="tab grey"><a id="4" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/List Prec/Mat</a></li>
                <li class="tab grey"><a id="5" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/List Prec/Gpo Art</a></li>
                <li class="tab grey"><a id="6" href="#" onclick="cambiaMenu(this.id)" class="white-text">Sector/Gpo Art</a></li>
            </ul>
        </div>
        <div class="container">
            <br />
            <div class="row">
                <div class="col s12 m12 l12">
                    <div class="center">
                        <label id="lblTipo" runat="server" class="cell03 black-text">Sector/Cte/Mat</label>
                        <br />
                        <br />
                    </div>
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
                <div class="col s12 m12 l12 center">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr id="tr1" runat="server" style="visibility: hidden; display: none;">
                            <td class="cell03">Org. de ventas</td>
                            <td>
                                <input type="text" runat="server" id="txtVKORG1" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr2" runat="server" style="visibility: hidden; display: none;">
                            <td class="cell03">Canal de distribución</td>
                            <td>
                                <input type="text" runat="server" id="txtVTWEG1" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr3" runat="server" style="visibility: hidden; display: none;">
                            <td class="cell03">Sector</td>
                            <td>
                                <input type="text" runat="server" id="txtSPART1" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr4" runat="server" style="visibility: hidden; display: none;">
                            <td class="cell03">Cliente</td>
                            <td>
                                <input type="text" runat="server" id="txtKUNNR1" class="cell05" disabled="disabled" /></td>
                        </tr>
                        <tr id="tr5" runat="server" style="visibility: hidden; display: none;">
                            <td class="cell03">Lista de precio</td>
                            <td>
                                <input type="text" runat="server" id="txtPLTYP1" class="cell05" disabled="disabled" /></td>
                        </tr>

                    </table>
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
                    <script src="../../js/materialize.js"></script>
                    <div class="input-field col s12">
                        <textarea runat="server" id="txtCOMM" class="materialize-textarea" data-length="255" style="width: 100%; font-family: Oswald" rows="2"></textarea>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <input type="hidden" runat="server" id="txtTabla" />
                    <input type="hidden" runat="server" id="txtTipo" />
                    <input type="button" runat="server" onclick="enviar()" value="Enviar" id="btnSubmit" disabled="disabled" class="btn right" />
                    <div class="right" style="width: 10px;">&nbsp;</div>
                    <a href="../Default.aspx" runat="server" class="btn right" target="_parent">Cancelar</a>
                </div>

                <input type="hidden" runat="server" id="txtVKORG" />
                <input type="hidden" runat="server" id="txtVTWEG" />
                <input type="hidden" runat="server" id="txtSPART" />
                <input type="hidden" runat="server" id="txtKUNNR" />
                <input type="hidden" runat="server" id="txtPLTYP" />
                <input type="hidden" runat="server" id="P_inc" />
                <input type="hidden" runat="server" id="P_dec" />
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

        <div id="modal" class="modal modal-fixed-footer open" style="z-index: 1003; opacity: 1; transform: scaleX(1); top: 10%;">
            <div class="modal-content">
                <label style="font-size: 20px;">Escalas</label>
                <div class="row">
                    <%--<input type="button" value="Agregar" class="btn right" onclick="addEscala();" />--%>
                </div>

                <input type="hidden" id="txtEscalas" runat="server" />
                <label id="lblEscala" runat="server"></label>
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
