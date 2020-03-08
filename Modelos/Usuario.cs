using Compartida.Compartido;
using System;
using System.Collections.Generic;

namespace Modelos
{
    public class Usuario
    {
        #region [Atributos]
        public Guid Id { get; private set; }
        public DateTime RegistradoAt { get; private set; }
        public DateTime ActualizadoAt { get; private set; }
        public string Nombre { get; private set; }
        public int TipoRhId { get; private set; }

        public Eps Eps { get; private set; }
        public Guid EpsId { get; private set; }

        public List<Todo> Todos { get; private set; }
        #endregion [Atributos]

        #region [Constructor]
        public Usuario() { }
        #endregion [Constructor]

        #region [ Metodos ]

        public static RespuestaAux<Usuario> AgregarUsuario(string nombre, int tipoRhId, Guid epsId)
        {

            var result = new RespuestaAux<Usuario>();

            if (!string.IsNullOrEmpty(nombre))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (TipoRhEnum.FiltrarporId(tipoRhId) == null)
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (!string.IsNullOrEmpty(epsId.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Usuario modelo = new Usuario()
            {
                Id = Guid.NewGuid(),
                RegistradoAt = DateTime.UtcNow,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre,
                TipoRhId = tipoRhId,
                EpsId = epsId
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }

        public static RespuestaAux<Usuario> EditarUsuario(Guid id, DateTime registradoAt, string nombre, int tipoRhId, Guid epsId)
        {
            var result = new RespuestaAux<Usuario>();

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (!string.IsNullOrEmpty(registradoAt.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (TipoRhEnum.FiltrarporId(tipoRhId) == null)
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (!string.IsNullOrEmpty(epsId.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Usuario modelo = new Usuario()
            {
                Id = id,
                RegistradoAt = registradoAt,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre,
                TipoRhId = tipoRhId,
                EpsId = epsId,
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }
        #endregion
    }
}
