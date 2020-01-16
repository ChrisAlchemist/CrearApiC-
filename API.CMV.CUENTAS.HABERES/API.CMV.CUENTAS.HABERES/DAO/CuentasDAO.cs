using AccesoDatos;
using API.CMV.CUENTAS.HABERES.Models.Entidades;
using API.CMV.CUENTAS.HABERES.Models.Request;
using API.CMV.CUENTAS.HABERES.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using static API.CMV.CUENTAS.HABERES.Models.Entidades.Enumeraciones;

namespace API.CMV.CUENTAS.HABERES.DAO
{
    public class CuentasDAO
    {
        private DBManager db = null;

        public _Response<List<Haber>> ObtenerCuentas( RequestObtenerCuentas request)
        {
            _Response<List<Haber>> response = new _Response<List<Haber>> ();
            List<Haber> haberes = new List<Haber>();
            Haber haber;

            try
            {
                using (db = new DBManager("Server=" + ConfigurationSettings.AppSettings["servidorBD"] + "; Database=banca;User Id=" + ConfigurationSettings.AppSettings["usuarioBase"] + ";Password=" + ConfigurationSettings.AppSettings["password"]))
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "NUMERO", request.NumeroSocio);
                    db.AddParameters(1, "tipo_cuenta", request.TipoCuenta);

                    db.ExecuteReader(System.Data.CommandType.StoredProcedure, "SP_BANCA_OBTENER_CUENTAS");//Modificar SP para nuevos parametros
                    //response = new ResponseObtenerCuentas();
                    //response.Cuentas = new List<Haber>();

                    while (db.DataReader.Read())
                    {
                        if (db.DataReader["ESTATUS"].ToString().Equals("200"))
                        {
                            haber = new Haber();
                            if (Convert.ToInt16(db.DataReader["IdMov"].ToString()) == 112)
                            {
                                haber.EstadoTarjeta = db.DataReader["estado_tarjeta"] == DBNull.Value ? EstadoTarjeta.Desbloqueada : (EstadoTarjeta)db.DataReader["estado_tarjeta"];
                                haber.TipoBloqueoTarjeta = db.DataReader["tipo_bloqueo_tarjeta"] == DBNull.Value ? TipoBloqueoTarjeta.Ninguno : (TipoBloqueoTarjeta)db.DataReader["tipo_bloqueo_tarjeta"];
                            }

                            haber.TipoEsquema = string.IsNullOrEmpty(db.DataReader["idEsquema"].ToString()) ? TipoEsquema.Ninguno : ((TipoEsquema)Convert.ToInt16(db.DataReader["idEsquema"].ToString()));
                            haber.IdMov = Convert.ToInt16(db.DataReader["IdMov"].ToString());
                            haber.NombreCuenta = db.DataReader["NombreCuenta"].ToString();
                            haber.Saldo = Convert.ToDecimal(string.IsNullOrEmpty(db.DataReader["Saldo"].ToString()) ? "0" : db.DataReader["Saldo"].ToString());
                            haber.FechaUltimoAbono = string.IsNullOrEmpty(db.DataReader["FechaUltimoAbono"].ToString()) ? "N/A" : db.DataReader["FechaUltimoAbono"].ToString();
                            haber.TipoCuenta = (TipoCuenta)Enum.Parse(typeof(TipoCuenta), db.DataReader["TipoCuenta"].ToString());
                            haber.NumeroContrato = db.DataReader["NumeroContrato"].ToString();
                            haber.ClabeCorresponsalias = db.DataReader["clabe_corresponsalias"].ToString();
                            haber.ClabeSpei = db.DataReader["clabe_spei"].ToString();
                            haberes.Add(haber);

                            response.Estatus = 200;
                            response.Mensaje =  "Consulta realizada con exito";
                        }

                        else
                        {
                            response.Estatus = db.DataReader["estatus"] == DBNull.Value ? 1000 : Convert.ToInt32(db.DataReader["estatus"]); ;
                            response.Mensaje = db.DataReader["mensaje"].ToString();
                        }

                    }
                    response.Data = haberes;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;

        }

        public _Response<Haber> ObtenerDetalleCuenta(RequestObtenerDetalleCuenta request)
        {
            _Response<Haber> response = new _Response<Haber>();            
            Haber haber;
            try
            {
                using (db = new DBManager("Server=" + ConfigurationSettings.AppSettings["servidorBD"] + "; Database=banca;User Id=" + ConfigurationSettings.AppSettings["usuarioBase"] + ";Password=" + ConfigurationSettings.AppSettings["password"]))
                {
                    db.Open();
                    db.CreateParameters(3);
                    db.AddParameters(0, "NUMERO", request.NumeroSocio);
                    db.AddParameters(1, "clabe_corresponsalias", request.ClabeCorresponsalias);
                    db.AddParameters(2, "numero_contrato", request.NumeroContrato);
                    db.ExecuteReader(System.Data.CommandType.StoredProcedure, "BANCA.DBO.SP_BANCA_OBTENER_DETALLE_CUENTA");//Modificar SP para nuevos parametros
                    if (db.DataReader.Read())
                    {
                        haber = new Haber();
                        if (Convert.ToInt32(db.DataReader["estatus"].ToString()) == 200)
                        {
                            /*if (Enum.Parse(typeof(TipoCuenta), db.DataReader["TipoCuenta"].ToString()).Equals(TipoCuenta.HABERES))
                            {*/
                                //Haber cuentaHaber = new Haber();
                            haber.IdMov = db.DataReader["IdMov"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["IdMov"].ToString());
                            haber.NombreCuenta = db.DataReader["NombreCuenta"] == DBNull.Value ? "" : db.DataReader["NombreCuenta"].ToString();
                            haber.Saldo = db.DataReader["Saldo"] == DBNull.Value ? 0 : Convert.ToDecimal(db.DataReader["Saldo"].ToString());
                            haber.TipoCuenta = (TipoCuenta)Convert.ToInt32(db.DataReader["TipoCuenta"].ToString());
                            haber.NumeroTarjeta = db.DataReader["NumeroTarjeta"] == DBNull.Value ? "" : db.DataReader["NumeroTarjeta"].ToString();
                            haber.UltimoAbono = string.IsNullOrEmpty(db.DataReader["FechaUltimoAbono"].ToString()) ? "N/A" : db.DataReader["FechaUltimoAbono"].ToString();
                            haber.EstadoTarjeta = db.DataReader["EstadoTarjeta"] == DBNull.Value ? EstadoTarjeta.Desbloqueada : (EstadoTarjeta)db.DataReader["EstadoTarjeta"];
                            haber.TipoBloqueoTarjeta = db.DataReader["TipoBloqueoTarjeta"] == DBNull.Value ? TipoBloqueoTarjeta.Ninguno : (TipoBloqueoTarjeta)db.DataReader["TipoBloqueoTarjeta"];
                            haber.MontoRetiros = string.IsNullOrEmpty(db.DataReader["NumeroRetiros"].ToString()) ? 0 : Convert.ToDecimal(db.DataReader["NumeroRetiros"].ToString());
                            haber.MontoDepositos = string.IsNullOrEmpty(db.DataReader["NumeroDepositos"].ToString()) ? 0 : Convert.ToDecimal(db.DataReader["NumeroDepositos"].ToString());
                                //cuenta = cuentaHaber;
                            //}

                            /*
                            if (Enum.Parse(typeof(TipoCuenta), db.DataReader["TipoCuenta"].ToString()).Equals(TipoCuenta.PRESTAMOS))
                            {
                                //Credito cuentaCredito = new Credito();
                                haber.IdMov = db.DataReader["IdMov"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["IdMov"].ToString());
                                haber.NombreCuenta = db.DataReader["NombreCuenta"] == DBNull.Value ? "" : db.DataReader["NombreCuenta"].ToString();
                                haber.Saldo = db.DataReader["Saldo"] == DBNull.Value ? 0 : Convert.ToDecimal(db.DataReader["Saldo"].ToString());
                                haber.TipoCuenta = (TipoCuenta)Convert.ToInt32(db.DataReader["TipoCuenta"].ToString());
                                //-----------
                                haber.DiasVencidos = db.DataReader["DiasVencidos"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["DiasVencidos"].ToString());
                                haber.EstatusCredito = db.DataReader["EstatusCredito"] == DBNull.Value ? "" : db.DataReader["EstatusCredito"].ToString();
                                haber.PagoHoy = db.DataReader["PagoHoy"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["PagoHoy"].ToString());//PagoAlCorriente
                                haber.PeriodosAtrasados = db.DataReader["PeriodosAtrasados"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["PeriodosAtrasados"].ToString());
                                haber.MontoInicial = db.DataReader["MontoInicial"] == DBNull.Value ? 0 : Convert.ToDecimal(db.DataReader["MontoInicial"].ToString());
                                haber.FechaPrestamo = db.DataReader["FechaPrestamo"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(db.DataReader["FechaPrestamo"].ToString());

                                DateTime fCorte;
                                if (DateTime.TryParse(db.DataReader["FechaCorte"].ToString(), out fCorte))
                                    haber.FechaCorte = fCorte.ToShortDateString();
                                else
                                    haber.FechaCorte = db.DataReader["FechaCorte"] == DBNull.Value ? "" : db.DataReader["FechaCorte"].ToString();

                                DateTime fLimitePago;
                                if (DateTime.TryParse(db.DataReader["FechaLimitePago"].ToString(), out fLimitePago))
                                    haber.FechaLimitePago = fLimitePago.ToShortDateString();
                                else
                                    haber.FechaLimitePago = db.DataReader["FechaLimitePago"] == DBNull.Value ? "" : db.DataReader["FechaLimitePago"].ToString();

                                DateTime fUltimoPago;
                                if (DateTime.TryParse(db.DataReader["FechaUltimoPago"].ToString(), out fUltimoPago))
                                    haber.FechaUltimoPago = fUltimoPago.ToShortDateString();
                                else
                                    haber.FechaUltimoPago = string.IsNullOrEmpty(db.DataReader["FechaUltimoPago"].ToString()) ? "N/A" : db.DataReader["FechaUltimoPago"].ToString();

                                haber.MontoDisponible = db.DataReader["MontoDisponible"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["MontoDisponible"].ToString());
                                haber.LimiteCredito = db.DataReader["LimiteCredito"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["LimiteCredito"].ToString());

                                haber.SaldoAdelantado = string.IsNullOrEmpty(db.DataReader["SaldoAdelantado"].ToString()) ? 0 : Convert.ToDecimal(db.DataReader["SaldoAdelantado"].ToString());
                                //cuentaCredito.ReferenciaCorresponsales = db.DataReader["ReferenciaCorresponsales"] == DBNull.Value ? "" : db.DataReader["ReferenciaCorresponsales"].ToString();
                                //cuenta = cuentaCredito;
                                haber.TipoEsquema = (TipoEsquema)Convert.ToInt16(db.DataReader["idEsquema"].ToString());

                            }
                            if (Enum.Parse(typeof(TipoCuenta), db.DataReader["TipoCuenta"].ToString()).Equals(TipoCuenta.INVERSIONES))
                            {
                                //Inversion inversion = new Inversion();
                                haber.IdMov = db.DataReader["IdMov"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["IdMov"].ToString());
                                haber.NombreCuenta = db.DataReader["NombreCuenta"] == DBNull.Value ? "" : db.DataReader["NombreCuenta"].ToString();
                                haber.FechaApertura = db.DataReader["FechaApertura"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(db.DataReader["FechaApertura"].ToString());
                                haber.NumeroContrato = db.DataReader["NoContrato"] == DBNull.Value ? "" : db.DataReader["NoContrato"].ToString();
                                haber.FechaVencimiento = db.DataReader["FechaVencimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(db.DataReader["FechaVencimiento"].ToString());
                                haber.Plazo = db.DataReader["plazo"] == DBNull.Value ? 0 : Convert.ToInt32(db.DataReader["plazo"].ToString());
                                haber.Tasa = db.DataReader["Tasa"] == DBNull.Value ? 0 : Convert.ToDouble(db.DataReader["Tasa"].ToString());
                                haber.TipoCuenta = (TipoCuenta)Convert.ToInt32(db.DataReader["TipoCuenta"].ToString());
                                haber.Saldo = db.DataReader["Saldo"] == DBNull.Value ? 0 : Convert.ToDecimal(db.DataReader["Saldo"].ToString());
                                //cuenta = inversion;
                            }

                            */

                            haber.ClabeCorresponsalias = string.IsNullOrEmpty(db.DataReader["ClabeCorresponsalias"].ToString()) ? "" : db.DataReader["ClabeCorresponsalias"].ToString();
                            haber.ClabeSpei = string.IsNullOrEmpty(db.DataReader["ClabeSpei"].ToString()) ? "" : db.DataReader["ClabeSpei"].ToString();

                            response.Estatus = 200;
                            response.Mensaje = "Consulta realizada con exito";
                        }
                        else
                        {
                            response.Estatus = db.DataReader["estatus"] == DBNull.Value ? 1000 : Convert.ToInt32(db.DataReader["estatus"]); ;
                            response.Mensaje = db.DataReader["mensaje"].ToString();
                        }

                        response.Data = haber;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
            //return cuenta;
        }

    }
}