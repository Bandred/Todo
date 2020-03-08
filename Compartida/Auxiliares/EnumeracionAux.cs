using System;

namespace Compartida.Auxiliares
{
    public abstract class EnumeracionAux : IComparable
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        protected EnumeracionAux() { }

        protected EnumeracionAux(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() => Nombre;

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((EnumeracionAux)obj).Id);
        }
    }
}
