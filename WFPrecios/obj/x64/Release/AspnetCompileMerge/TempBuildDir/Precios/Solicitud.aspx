<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Solicitud.aspx.cs" Inherits="WFPrecios.Precios.Solicitud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <title>Nueva Solicitud</title>
    <link href="../css/style.css" rel="stylesheet" />
    <link href="../css/materialize.css" rel="stylesheet" />
    <script src="../js/jquery-1.10.2.js"></script>
    <script src="../js/materialize.js"></script>
</head>
<body onresize="tamanio()">
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 100%; border-collapse: collapse;">
                <tr>
                    <td class="icons">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="linksC">
                        <span id="lblTitulo" style="color: White; font-family: Oswald; font-size: 18pt;">WF Precios / Nueva Solicitud
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <a id="lnkExcel" href="tabs/ExcelTab.aspx?tipo=1">Cargar Excel</a>&nbsp;|&nbsp;<a href="Default.aspx">Regresar</a>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="grey">
            <ul class="tabs grey center-align">
                <li class="tab grey"><a id="1" href="tabs/tab1.aspx" onclick="cambiaMenu(this.id)" class="active white-text">Sector/Cte/Mat</a></li>
                <li class="tab grey"><a id="2" href="tabs/tab2.aspx" onclick="cambiaMenu(this.id)" class="white-text">Sector/Mat</a></li>
                <li class="tab grey"><a id="3" href="tabs/tab3.aspx" onclick="cambiaMenu(this.id)" class="white-text">Sector/Cte/Gpo Art</a></li>
                <li class="tab grey"><a id="4" href="tabs/tab4.aspx" onclick="cambiaMenu(this.id)" class="white-text">Sector/List Prec/Mat</a></li>
                <li class="tab grey"><a id="5" href="tabs/tab5.aspx" onclick="cambiaMenu(this.id)" class="white-text">Sector/List Prec/Gpo Art</a></li>
                <li class="tab grey"><a id="6" href="tabs/tab6.aspx" onclick="cambiaMenu(this.id)" class="white-text">Sector/Gpo Art</a></li>
                <%--<li class="tab grey white-text" onclick="cambiaMenu(this.id)">Sector/Gpo Art</li>--%>
            </ul>
        </div>
        <%--<div class="container">
            <div class="row">
                <div class="col s12 m12 l12">
                    <div id="tab1" class="col s12">Test 1</div>
                    <div id="tab2" class="col s12">Test 2</div>
                    <div id="tab3" class="col s12">Test 3</div>
                    <div id="tab4" class="col s12">Test 4</div>
                    <div id="tab5" class="col s12">Test 5</div>
                    <div id="tab6" class="col s12">
                        Test 6
                        <a href="SolicitudPC.aspx" target="_self">SolicitudPC.aspx</a>
                    </div>
                </div>
            </div>
        </div>--%>
        <div style="margin: 0px; padding: 0px; overflow: hidden; height: 100%;">
            <iframe src="tabs/tab1.aspx" id="uno" name="uno" style="border: 0px; overflow: hidden; width: 100%" class="myIframe"></iframe>
        </div>
        <script>
            //var anchors = document.getElementsByTagName('a');
            //for (var i = 0; i < anchors.length; i++) {
            //    var anchor = anchors[i];
            //    anchor.addEventListener('click', function (event) {
            //        history.replaceState(null, null, anchor.href);
            //    }, false);
            //}

            var height = $(window).height() - 55 - 95;
            $('.myIframe').css('height', height + 'px');

            function tamanio() {
                var height = $(window).height() - 55 - 95;
                $('.myIframe').css('height', height + 'px');
            }
            var $this = $('ul.tabs'),
                    window_width = $(window).width();
            var $active, $content, $links = $this.find('li.tab a'),
                   $tabs_width = $this.width(),
                   $tabs_content = $(),
                   $tabs_wrapper,
                   $tab_width = Math.max($tabs_width, $this[0].scrollWidth) / $links.length,
                   $indicator,
                   index = prev_index = 0,
                   clicked = false,
                   clickedTimeout,
                   transition = 300;

            function cambiaMenu(id) {
                var href = document.getElementById(id).href;
                window.frames['uno'].location.replace(href);
                for (var i = 1; i < 7; i++) {
                    document.getElementById(i).className = "white-text";
                }
                document.getElementById(id).className = "active white-text";
                var tabs_width = $this.width();
                $tab_width = Math.max($tabs_width, $this[0].scrollWidth) / $links.length;
                prev_index = index;
                index = id;
                $indicator = $this.find('.indicator');
                $active = $($links.filter('[href="' + location.hash + '"]'));
                if ($active.length === 0) {
                    $active = $this.find('li.tab a.active').first();
                }
                if ($active.length === 0) {
                    $active = $this.find('li.tab a').first();
                }

                animateIndicator(prev_index);
                document.getElementById("lnkExcel").href = "tabs/ExcelTab.aspx?tipo=" + id;
            }

            function calcRightPos(el) {
                return $tabs_width - el.position().left - el.outerWidth() - $this.scrollLeft();
            }

            function calcLeftPos(el) {
                return el.position().left + $this.scrollLeft();
            }

            function animateIndicator(prev_index) {
                if ((index - prev_index) >= 0) {
                    $indicator.velocity({ "right": calcRightPos($active) }, { duration: transition, queue: false, easing: 'easeOutQuad' });
                    $indicator.velocity({ "left": calcLeftPos($active) }, { duration: transition, queue: false, easing: 'easeOutQuad', delay: 90 });

                } else {
                    $indicator.velocity({ "left": calcLeftPos($active) }, { duration: transition, queue: false, easing: 'easeOutQuad' });
                    $indicator.velocity({ "right": calcRightPos($active) }, { duration: transition, queue: false, easing: 'easeOutQuad', delay: 90 });
                }
            }
        </script>
    </form>
</body>
</html>
