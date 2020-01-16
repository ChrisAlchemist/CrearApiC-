using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.CMV.CUENTAS.HABERES.Models.Response
{
    public class _Response<T> where T : class
    {
        public string Mensaje { get; set; }
        public int Estatus { get; set; }
        public T Data { get; set; }
    }
}