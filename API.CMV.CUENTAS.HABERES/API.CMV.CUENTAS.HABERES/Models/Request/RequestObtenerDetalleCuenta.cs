using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Request
{
    public class RequestObtenerDetalleCuenta
    {
        public string NumeroSocio { get; set; }
        public string ClabeCorresponsalias { get; set; }        
        public string NumeroContrato { get; set; }
        
    }
}