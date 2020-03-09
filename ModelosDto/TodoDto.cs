using Modelos;
using System;
using System.Collections.Generic;

namespace ModelosDto
{
    #region Queries
    public class TodoDto
    {
        #region [Atributos]
        public Guid Id { get; set; }
        public DateTime RegistradoAt { get; set; }
        public DateTime ActualizadoAt { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public UsuarioDto Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        #endregion

        #region [Metodos]
        public static TodoDto ProyectarDto(Todo modelo)
        {
            var modeloDto = new TodoDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre,
                Activo = modelo.Activo,
                Usuario = UsuarioDto.ProyectarDto(modelo.Usuario),
                UsuarioId = modelo.UsuarioId
            };

            return modeloDto;
        }

        public static Func<Todo, TodoDto> ProyectarDto()
        {
            return modelo => new TodoDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre,
                Activo = modelo.Activo,
                Usuario = UsuarioDto.ProyectarDto(modelo.Usuario),
                UsuarioId = modelo.UsuarioId
            };
        }
        #endregion
    }

    public class TodoViewModel
    {
        public TodoDto Item { get; set; }

        public bool HabilitarEditar { get; set; }

        public bool HabilitarBorrar { get; set; }
    }

    public class TodoListViewModel
    {
        public List<TodoDto> Items { get; set; }

        public bool HabilitarCrear { get; set; }
    }
    #endregion

    #region Setters
    public class TodoAgregarCommand
    {
        public string Nombre { get; set; }
        public Guid UsuarioId { get; set; }
    }

    public class TodoEditarCommand
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Guid UsuarioId { get; set; }
    }
    #endregion
}
