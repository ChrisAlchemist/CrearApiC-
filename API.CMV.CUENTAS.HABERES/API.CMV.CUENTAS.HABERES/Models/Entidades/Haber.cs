using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static API.CMV.CUENTAS.HABERES.Models.Entidades.Enumeraciones;

namespace API.CMV.CUENTAS.HABERES.Models.Entidades
{
    public class Haber
    {
        public int IdMov { get; set; }

        public String NombreCuenta { get; set; }

        public Decimal Saldo { get; set; }

        public TipoCuenta TipoCuenta { get; set; }

        public String NumeroContrato { get; set; }

        public String ClabeCorresponsalias { get; set; }

        public String ClabeSpei { get; set; }

        public String FechaUltimoAbono { get; set; }

        public TipoEsquema TipoEsquema { get; set; }

        public int IdCuenta { get; set; }
        
        public string UltimoAbono { get; set; }
        
        public string NumeroTarjeta { get; set; }
        
        public EstadoTarjeta EstadoTarjeta { get; set; }
        
        public TipoBloqueoTarjeta TipoBloqueoTarjeta { get; set; }
        
        public Decimal MontoDepositos { get; set; }
        
        public Decimal MontoRetiros { get; set; }
    }
}