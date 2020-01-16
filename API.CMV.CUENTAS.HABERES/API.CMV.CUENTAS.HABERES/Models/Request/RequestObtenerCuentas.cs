using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Request
{
    public class RequestObtenerCuentas
    {
        public string NumeroSocio { get; set; }
        public int TipoCuenta { get; set; }
    }
}