using API.CMV.CUENTAS.HABERES.DAO;
using API.CMV.CUENTAS.HABERES.Models.Entidades;
using API.CMV.CUENTAS.HABERES.Models.Request;
using API.CMV.CUENTAS.HABERES.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace API.CMV.CUENTAS.HABERES.Controllers
{
    public class CuentaHaberController : ApiController
    {
        //
        [HttpPost]
        public string cuentas()
        {
            return "Estas son las cuentas";
        }
        [HttpPost]
        public _Response<List<Haber>> ObtenerCuentas(RequestObtenerCuentas request)
        {
            _Response<List<Haber>> response = new _Response<List<Haber>>();
            //_Response<ResponseObtenerCuentas> _response = new _Response<ResponseObtenerCuentas>();
            try
            {
                CuentasDAO cuentasDAO = new CuentasDAO();
                response = cuentasDAO.ObtenerCuentas(request);
            }
            catch (Exception ex)
            {
                response.Estatus = -1;
                response.Mensaje = ex.Message;
            }
            return response;

            //public CuentasDAO cuentasDAO = new CuentasDAO();
            
            //responseObtenerCuentas.Cuentas = new List<string>() {"Inver", "Ahorro", "Debito"};
            //_response.Data = responseObtenerCuentas;
            //_response.Estatus = "200";
            //_response.Mensaje = "OK";
            
        }

        [HttpPost]
        public _Response<Haber> ObtenerDetalleCuenta (RequestObtenerDetalleCuenta request)
        {
            _Response<Haber> response = new _Response<Haber>();
            try
            {
                CuentasDAO cuentasDAO = new CuentasDAO();
                response = cuentasDAO.ObtenerDetalleCuenta(request);
            }
            catch (Exception ex)
            {

                response.Estatus = -1;
                response.Mensaje = ex.Message;
            }
            return response;
        }
    }
}
