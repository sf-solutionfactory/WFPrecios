using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFPrecios.Models
{
    public class Fechas
    {
        public DateTime fecha(string date) //YYYY-MM-DD a datetime
        {
            string anio = date.Substring(0, 4);
            string mes = date.Substring(5, 2);
            string dia = date.Substring(8, 2);

            int year = int.Parse(anio);
            int month = int.Parse(mes);
            int day = int.Parse(dia);

            return new DateTime(year, month, day);
        }
        public DateTime fechaD(string date) //DD-MM-YYYY a datetime
        {
            string anio = date.Substring(6, 4);
            string mes = date.Substring(3, 2);
            string dia = date.Substring(0, 2);

            int year = int.Parse(anio);
            int month = int.Parse(mes);
            int day = int.Parse(dia);

            return new DateTime(year, month, day);
        }
        public string fechaToSAP(DateTime date) //datetime a YYYYMMDD
        {
            int anio = date.Year;
            int mes = date.Month;
            int dia = date.Day;

            string year = anio + ""; ;
            string month = "" + mes;
            if (mes < 10)
                month = "0" + mes;
            string day = "" + dia;
            if (dia < 10)
                day = "0" + dia;

            return year + month + day;
        }
        public string fechaToOUT(DateTime date) //datetime a DD/MM/YYYY
        {
            int anio = date.Year;
            int mes = date.Month;
            int dia = date.Day;

            string year = anio + ""; ;
            string month = "" + mes;
            if (mes < 10)
                month = "0" + mes;
            string day = "" + dia;
            if (dia < 10)
                day = "0" + dia;

            return day + "/" + month + "/" + year;
        }

        public string fechatoCHROME(DateTime date)//datetime a YYYY-MM-DD
        {
            int anio = date.Year;
            int mes = date.Month;
            int dia = date.Day;

            string year = anio + ""; ;
            string month = "" + mes;
            if (mes < 10)
                month = "0" + mes;
            string day = "" + dia;
            if (dia < 10)
                day = "0" + dia;

            return year + "-" + month + "-" + day;
        }

        public string hora(DateTime date) //datetime a HHMMSS
        {
            int hora = date.Hour;
            int min = date.Minute;
            int seg = date.Second;

            string hour = hora + "";
            if (hora < 10)
                hour = "0" + hora;
            string minute = "" + min;
            if (min < 10)
                minute = "0" + min;
            string sec = "" + seg;
            if (seg < 10)
                sec = "0" + seg;

            return hour + minute + sec;
        }

        public DateTime fechafromSAP(string date) //YYYYMMDD a datetime
        {
            string anio = date.Substring(0, 4);
            string mes = date.Substring(4, 2);
            string dia = date.Substring(6, 2);

            int year = int.Parse(anio);
            int month = int.Parse(mes);
            int day = int.Parse(dia);

            return new DateTime(year, month, day);
        }
    }
}