using Modelos;
using System;
using System.Collections.Generic;

namespace ModelosDto
{
    #region Queries
    public class EpsDto
    {
        #region [Atributos]
        public Guid Id { get; set; }
        public DateTime RegistradoAt { get; set; }
        public DateTime ActualizadoAt { get; set; }
        public string Nombre { get; set; }
        #endregion

        #region [Metodos]
        public static EpsDto ProyectarDto(Eps modelo)
        {
            var modeloDto = new EpsDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre
            };

            return modeloDto;
        }

        public static Func<Eps, EpsDto> ProyectarDto()
        {
            return modelo => new EpsDto
            {
                Id = modelo.Id,
                RegistradoAt = modelo.RegistradoAt,
                ActualizadoAt = modelo.ActualizadoAt,
                Nombre = modelo.Nombre
            };
        }
        #endregion
    }

    public class EpsViewModel
    {
        public EpsDto Item { get; set; }

        public bool HabilitarEditar { get; set; }

        public bool HabilitarBorrar { get; set; }
    }

    public class EpsListViewModel
    {
        public List<EpsDto> Items { get; set; }

        public bool HabilitarCrear { get; set; }
    }
    #endregion

    #region Setters
    public class EpsAgregarCommand
    {
        public string Nombre { get; set; }
    }

    public class EpsEditarCommand
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
    #endregion
}
