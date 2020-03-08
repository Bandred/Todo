using Compartida.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compartida.Compartido
{
    public class TipoRhEnum : EnumeracionAux
    {
        public TipoRhEnum(int id, string nombre) : base(id, nombre) { }

        public static TipoRhEnum ONegativo = new TipoRhEnum(1, "O -");
        public static TipoRhEnum OPositivo = new TipoRhEnum(2, "O +");
        public static TipoRhEnum ANegativo = new TipoRhEnum(3, "A -");
        public static TipoRhEnum APositivo = new TipoRhEnum(4, "A +");
        public static TipoRhEnum BNegativo = new TipoRhEnum(5, "B -");
        public static TipoRhEnum BPositivo = new TipoRhEnum(6, "B +");
        public static TipoRhEnum ABNegativo = new TipoRhEnum(7, "AB -");
        public static TipoRhEnum ABPositivo = new TipoRhEnum(8, "AB +");

        public static IList<TipoRhEnum> List() => new[] {
            ONegativo,
            OPositivo,
            ANegativo,
            APositivo,
            BNegativo,
            BPositivo,
            ABNegativo,
            ABPositivo
        };

        public static TipoRhEnum FiltrarPorNombre(string value)
        {
            var result = List().Single(x => String.Equals(x.Nombre, value, StringComparison.OrdinalIgnoreCase));

            return result;
        }

        public static TipoRhEnum FiltrarporId(int value)
        {
            var result = List().Single(x => x.Id == value);

            return result;
        }

    }
}
