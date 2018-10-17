<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mail.aspx.cs" Inherits="WFPrecios.Mail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Autorizaci&oacute;n de Solicitud</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EDGE" />
    <meta name="robots" content="noodp,noydir" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="icon" href="http://www.terza.com/img/Favicon_32x32-01.ico" type="image/x-icon" />
    <link href="http://fonts.googleapis.com/css?family=Roboto" rel="stylesheet" type="text/css" />
    <link href="http://cdnjs.cloudflare.com/ajax/libs/normalize/3.0.1/normalize.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
        }

        body {
            background-color: #FFFFFF;
            margin: 0px;
            padding: 0px;
            color: #404040;
            text-align: center;
            font-family: Arial;
        }

        a {
            color: #FFFFFF;
            text-decoration: none;
            cursor: pointer;
            font-family: Arial;
            font-size: 20px;
        }

            a:hover {
                color: rgb(184, 191, 13);
            }

        .row {
            width: 1150px;
            /*height: 27px;*/
            vertical-align: middle;
            text-align: center;
            border-bottom-style: solid;
            border-bottom-width: 3px;
            border-bottom-color: #FFFFFF;
        }

        .cell01 {
            background-color: rgb(57, 59, 64);
            width: 250px;
            height: 80px;
            vertical-align: middle;
            text-align: left;
            color: #FFFFFF;
            border-top-style: solid;
            border-top-width: 5px;
            border-top-color: rgb(184, 191, 13);
        }

        .cell02 {
            background-color: rgb(57, 59, 64);
            width: 100%;
            height: 80px;
            vertical-align: middle;
            text-align: right;
            color: #FFFFFF;
            border-top-style: solid;
            border-top-width: 5px;
            border-top-color: rgb(184, 191, 13);
            font-size: 30px;
            font-family: Arial;
            color: #FFFFFF;
        }

        .cell03 {
            width: 150px;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            font-size: 15px;
            font-family: Arial;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FFFFFF;
        }

        .cell04 {
            width: 750px;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            background-color: rgb(93, 96, 102);
            color: #FFFFFF;
            border-radius: 5px;
            font-size: 15px;
            font-family: Arial;
            color: #FFFFFF;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FFFFFF;
        }

        .cell05 {
            width: 295px;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            background-color: rgb(93, 96, 102);
            color: #FFFFFF;
            border-radius: 5px;
            font-size: 15px;
            font-family: Arial;
            color: #FFFFFF;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FFFFFF;
        }

        .cell06 {
            width: 40px;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FFFFFF;
        }

        .cell07 {
            width: 120px;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            font-size: 15px;
            font-family: Arial;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FFFFFF;
        }

        .cell08 {
            width: 100%;
            height: 27px;
            vertical-align: middle;
            text-align: left;
            font-size: 15px;
            font-family: Arial;
            color: #404040;
            border-bottom-style: solid;
            border-bottom-width: 4px;
            border-bottom-color: rgb(122, 129, 10);
        }

        .cell10 {
            width: 130px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell11 {
            width: 255px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell12 {
            /*width: auto;*/
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell12_importe {
            width: 130px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell13 {
            width: 50px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell14 {
            width: 50px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell15 {
            width: 100px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell16 {
            width: 130px;
            height: 20px;
            vertical-align: middle;
            text-align: left;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell17 {
            width: 255px;
            height: 20px;
            vertical-align: middle;
            text-align: left;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell18 {
            /*width: auto;*/
            height: 20px;
            vertical-align: middle;
            text-align: center;
            font-size: 12px;
            color: #454545;
        }

        .cell18_Importe {
            width: 130px;
            height: 20px;
            vertical-align: middle;
            text-align: right;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell19 {
            width: 50px;
            height: 20px;
            vertical-align: middle;
            text-align: left;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell20 {
            width: 50px;
            height: 20px;
            vertical-align: middle;
            text-align: center;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell21 {
            width: 100px;
            height: 20px;
            vertical-align: middle;
            text-align: left;
            font-size: 12px;
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell22a {
            width: 130px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell22b {
            width: 130px;
            height: 25px;
            vertical-align: middle;
            text-align: center;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
        }


        .cell23a {
            width: 130px;
            height: 20px;
            vertical-align: middle;
            text-align: right;
            font-size: 12px;
            color: #454545;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #FFFFFF;
        }

        .cell23b {
            width: 130px;
            height: 20px;
            vertical-align: middle;
            text-align: right;
            font-size: 12px;
            color: #454545;
        }


        .cell30 {
            width: 310px;
            height: 25px;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell31 {
            width: 70px;
            height: 25px;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell31_evento {
            width: 90px;
            height: 25px;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell32 {
            width: 140px;
            height: 25px;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #E2FF8A;
        }

        .cell33 {
            width: 400px;
            height: 25px;
            font-size: 14px;
            background-color: rgb(184, 191, 13);
            color: #FFFFCE;
        }

        .cell34 {
            width: 310px;
            height: 20px;
            font-size: 12px;
            color: #454545;
            text-align: left;
            vertical-align: middle;
        }

        .cell35 {
            width: 70px;
            height: 20px;
            font-size: 12px;
            color: #454545;
            text-align: center;
            vertical-align: middle;
        }

        .cell35_evento {
            width: 90px;
            height: 20px;
            font-size: 12px;
            color: #454545;
            text-align: center;
            vertical-align: middle;
        }

        .cell36 {
            width: 140px;
            height: 20px;
            font-size: 12px;
            color: #454545;
            text-align: center;
            vertical-align: middle;
        }

        .cell37 {
            width: 400px;
            height: 20px;
            font-size: 12px;
            color: #454545;
            text-align: left;
            vertical-align: middle;
        }

        .tituloencabezado {
            font-size: 30px;
            font-family: Arial;
            color: #FFFFFF;
        }

        .Cabecera {
            margin: 0px auto 0px auto;
            width: 900px;
            text-align: center;
            display: block;
            font-family: Arial;
            font-size: 14px;
            color: #404040;
            border-top-color: #FFFFFF;
            border-top-width: 5px;
            border-top-style: solid;
        }

        .textbox {
            border: 1px solid rgb(93, 96, 102);
            width: 100%;
            height: 100%;
            font-family: Arial;
            font-size: 14px;
            background-color: rgb(93, 96, 102);
            color: #FFFFFF;
            border-radius: 5px;
            padding-left: 0px;
        }

        .textbox2 {
            border: 1px solid rgb(93, 96, 102);
            width: 240px;
            height: 100%;
            font-family: Arial;
            font-size: 14px;
            background-color: rgb(93, 96, 102);
            color: #FFFFFF;
            border-radius: 5px;
            text-align: right;
        }

        .textbox3 {
            border: 1px solid rgb(93, 96, 102);
            width: 45px;
            height: 100%;
            font-family: Arial;
            font-size: 14px;
            background-color: rgb(93, 96, 102);
            color: #FFFFFF;
            border-radius: 5px;
            padding-left: 0px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table id="Table1" style="border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;">
                <tr>
                    <td class="cell01">&nbsp;<img src="http://www.terzaonline.com/nWorkflow/images/Terza_Logo-menu.png" /></td>

                    <td class="cell02">
                        <span id="lblTitulo" style="color: White; font-family: Arial; font-size: 18pt;">
                            <label id="txtFolio" runat="server"></label>
                        </span>&nbsp;&nbsp;
                        <br />
                        <%--<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=A&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Autorizar</a>&nbsp;|&nbsp;<a href='http://www.terzaonline.com/nworkflow/wf_pagos/procesa.aspx?pOper=R&pBURKS=4011&pBELNR=1900000634&pGJAHR=2017&pPOSI=00003'>Rechazar</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3862-7000094-3756296_7000094.PDF'>Ver PDF</a>&nbsp;|&nbsp;<a href='http://10.130.12.39/Boveda/Cargados/160-3'>Ver XML</a>&nbsp;&nbsp;</td>--%>
                        <%--<a href="Default.aspx">Regresar</a>&nbsp;|&nbsp;<a href="../../Default.aspx">Cerrar sesión</a>&nbsp;&nbsp;</td>--%>
                        <label runat="server" id="lblLinks"></label></td>
                </tr>
            </table>
        </div>
        <div class="container">
            <br />
            <div class="row">
                <div class="col s12 m12 l12 center">
                    <table style="position: relative; left: 50%; margin-left: -200px">
                        <tr>
                            <td class="cell03">Solicitante</td>
                            <td class="cell05"><label runat="server" id="txtPERNR" ></label></td>
                        </tr>
                        <tr>
                            <td class="cell03">Fecha de solicitud</td>
                            <td class="cell05"><label runat="server" id="txtDATE" ></label></td>
                        </tr>
                    </table>
                    <label runat="server" id="lblFolio"></label>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table9" style="border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;">
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
            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table09" style="border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;">
                        <tbody>
                            <tr>
                                <td class="cell08">Comentarios</td>
                            </tr>
                        </tbody>
                    </table>
                    <label runat="server" id="txtCOMM"></label>
                    <%--<textarea runat="server" id="txtCOMM" disabled="disabled" data-length="255"></textarea>--%>
                    <%--<script src="../js/jquery-1.10.2.js"></script>--%>
                    <%--<script>
                        var cont = document.getElementById("txtCOMM").value;
                        var lineas = cont.split('\n').length;
                        var style = "width: 100%; resize: none; font-family: Arial; height:" + (lineas * 25) + "px;"
                        document.getElementById("txtCOMM").style = style;
                    </script>--%>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col s12 m12 l12">
                    <table id="Table10" style="border-width: 0px; border-style: None; width: 1150px; border-collapse: collapse;">
                        <tbody>
                            <tr>
                                <td class="cell08">Bitacora</td>
                            </tr>
                        </tbody>
                    </table>
                    <label runat="server" id="lblBItacora"></label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
