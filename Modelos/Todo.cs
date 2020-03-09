using Compartida.Compartido;
using System;

namespace Modelos
{
    public class Todo
    {
        #region [Atributos]
        public Guid Id { get; private set; }
        public DateTime RegistradoAt { get; private set; }
        public DateTime ActualizadoAt { get; private set; }
        public string Nombre { get; private set; }
        public bool Activo { get; private set; }

        public Usuario Usuario { get; private set; }
        public Guid UsuarioId { get; private set; }
        #endregion [Atributos]

        #region [Constructor]
        public Todo() { }
        #endregion [Constructor]

        #region [ Metodos ]

        public static RespuestaAux<Todo> AgregarTodo(string nombre, Guid usuarioId)
        {
            var result = new RespuestaAux<Todo>();

            if (string.IsNullOrEmpty(nombre))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            if (string.IsNullOrEmpty(usuarioId.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Todo modelo = new Todo()
            {
                Id = Guid.NewGuid(),
                RegistradoAt = DateTime.UtcNow,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre,
                Activo = true,
                UsuarioId = usuarioId
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }

        public static RespuestaAux<Todo> EditarTodo(Guid id, string nombre, bool activo, Guid usuarioId)
        {
            var result = new RespuestaAux<Todo>();

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

            if (string.IsNullOrEmpty(usuarioId.ToString()))
            {
                result.Exitoso = false;
                result.Mensaje = "Modelo no es válido";

                return result;
            }

            Todo modelo = new Todo()
            {
                Id = id,
                ActualizadoAt = DateTime.UtcNow,
                Nombre = nombre,
                Activo = activo,
                UsuarioId = usuarioId,
            };

            result.Exitoso = true;
            result.Result = modelo;

            return result;
        }
        #endregion
    }
}
