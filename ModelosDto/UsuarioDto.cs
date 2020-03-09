using Compartida.Compartido;
using Modelos;
using System;
using System.Collections.Generic;

namespace ModelosDto
{

    #region Queries
    public class UsuarioDto
    {
        #region [Atributos]
        public Guid Id { get; set; }
        public DateTime RegistradoAt { get; set; }
        public DateTime ActualizadoAt { get; set; }
        public string Nombre { get; set; }

        public TipoRhEnum TipoRh { get; set; }
        public int TipoRhId { get; set; }

        public EpsDto Eps { get; set; }
        public Guid EpsId { get; set; }
        #endregion

        #region [Metodos]
        public static UsuarioDto ProyectarDto(Usuario modelo)
        {
            var modeloDto = new UsuarioDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre,
                TipoRh = TipoRhEnum.FiltrarporId(modelo.TipoRhId),
                TipoRhId = modelo.TipoRhId,
                Eps = EpsDto.ProyectarDto(modelo.Eps),
                EpsId = modelo.EpsId
            };

            return modeloDto;
        }

        public static Func<Usuario, UsuarioDto> ProyectarDto()
        {
            return modelo => new UsuarioDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre,
                TipoRh = TipoRhEnum.FiltrarporId(modelo.TipoRhId),
                TipoRhId = modelo.TipoRhId,
                Eps = EpsDto.ProyectarDto(modelo.Eps),
                EpsId = modelo.EpsId
            };
        }
        #endregion
    }

    public class UsuarioViewModel
    {
        public UsuarioDto Item { get; set; }

        public bool HabilitarEditar { get; set; }

        public bool HabilitarBorrar { get; set; }
    }

    public class UsuarioListViewModel
    {
        public List<UsuarioDto> Items { get; set; }

        public bool HabilitarCrear { get; set; }
    }
    #endregion

    #region Setters
    public class UsuarioAgregarCommand
    {
        public string Nombre { get; set; }
        public int TipoRhId { get; set; }
        public Guid EpsId { get; set; }
    }

    public class UsuarioEditarCommand
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int TipoRhId { get; set; }
        public Guid EpsId { get; set; }
    }
    #endregion
}
