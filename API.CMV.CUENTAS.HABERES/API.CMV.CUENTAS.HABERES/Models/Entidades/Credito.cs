using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Entidades
{
    public class Credito
    {
        public int DiasVencidos { get; set; }
        public string EstatusCredito { get; set; }
        public double PagoHoy { get; set; }
        public int PeriodosAtrasados { get; set; }
        public decimal MontoInicial { get; set; }     
        public DateTime FechaPrestamo { get; set; }
        public string FechaCorte { get; set; }
        public string FechaLimitePago { get; set; }
        public double MontoDisponible { get; set; }
        public double LimiteCredito { get; set; }
        //public DateTime FechaUltimoPago { get; set; }
        public String FechaUltimoPago { get; set; }
        public string ReferenciaCorresponsales { get; set; } //este falta mapear en la bd        
        public decimal SaldoAdelantado { get; set; }
    }
}