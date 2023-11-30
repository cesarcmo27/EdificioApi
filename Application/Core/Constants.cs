using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Constants
    {
        public enum Estados
        {
            Activo = 1,
            Suspendido = 2
        }
        public static string ObtenerEstadoDepartamento(int estado)
        {
            switch (estado)
            {
                case (int)Estados.Activo:
                    return "activo";
                case (int)Estados.Suspendido:
                    return "suspendido";
                default:
                    return "desconocido";
            }
        }
    }
}