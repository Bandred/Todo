using Compartida.Compartido;
using System;
using System.Collections.Generic;

namespace Modelos
{
    public class Eps
    {
        #region [Atributos]
        public Guid Id { get; private set; }
        public DateTime RegistradoAt { get; private set; }
        public DateTime ActualizadoAt { get; private set; }
        public string Nombre { get; private set; }

        public List<Usuario> Usuarios { get; private set; }
        #endregion [Atributos]

        #region [Constructor]
        public Eps() { }
        #endregion [Constructor]

        #region [ Metodos ]

        public static RespuestaAux<Eps> AgregarEps(string nombre)
        {
            var result = new RespuestaAux<Eps>();

            if (string.IsNullOrEmpty(nombre))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Eps modelo = new Eps()
            {
                Id = Guid.NewGuid(),
                RegistradoAt = DateTime.UtcNow,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }

        public static RespuestaAux<Eps> EditarEps(Guid id, string nombre)
        {
            var result = new RespuestaAux<Eps>();

            if (string.IsNullOrEmpty(id.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Eps modelo = new Eps()
            {
                Id = id,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }
        #endregion
    }
}
