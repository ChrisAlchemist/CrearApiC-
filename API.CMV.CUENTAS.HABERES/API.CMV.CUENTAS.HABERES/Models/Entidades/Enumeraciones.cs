using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Entidades
{
    public class Enumeraciones
    {
        public enum TipoCuenta
        {
            
            TODAS_LAS_CUENTAS = 0,            
            HABERES = 1,            
            INVERSIONES = 2,            
            PRESTAMOS = 3,
            //[EnumMember]
            //HABERES_MAS_INVERSIONES = 4
        }

        public enum TipoEsquema
        {            
            Ninguno = 0,            
            Decreciente = 1,            
            Nivelado = 2,            
            Nivelado_Quincenal = 3,            
            Revolvente = 4,            
        }

        public enum EstadoTarjeta
        {         
            Desbloqueada = 1,         
            Bloqueada = 2,         
            Todas
        }

        public enum TipoBloqueoTarjeta
        {
            Bloqueo_Temporal = 1,         
            Bloqueo_Robo = 2,            
            Bloqueo_Extravio = 3,            
            Ninguno = 4
        }
    }
}