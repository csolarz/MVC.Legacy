using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Legacy.Utils
{
    public class TicketSSO
    {
        public static string GetCookie()
        {
            var claims = new ClaimsSSO()
            {
                RutUsuario = "76116209-5",
                RutEmpresa = "11111111-1",
                URLDestino = "datosDeuda",
                EmailUsuario = "carlosz@lagash.com",
                NombreUsuario = "Carlos",
                APaternoUsuario = "Solar",
                AMaternoUsuario = "Zamorano",
                FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Isapre = "B"
            };

            var criptoUtil = new CryptoUtil();
            string ticket = criptoUtil.Encrypt(JsonConvert.SerializeObject(claims, Formatting.Indented));

            return ticket;
        }
    }

    public class ClaimsSSO
    {
        public string RutUsuario { get; set; }
        public string RutEmpresa { get; set; }
        public string URLDestino { get; set; }
        public string NombreUsuario { get; set; }
        public string APaternoUsuario { get; set; }
        public string AMaternoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string FechaCreacion { get; set; }
        public string Isapre { get; set; }
    }
}