using API.CMV.CUENTAS.HABERES.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Response
{
    public class ResponseObtenerCuentas
    {
        public List<Cuenta> Cuentas { get; set; }
    }
}