namespace Compartida.Compartido
{
    public abstract class RespuestaAuxBase
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }

    public class RespuestaAux<T> : RespuestaAuxBase
    {
        public T Result { get; set; }
    }

    public class RespuestaAux : RespuestaAuxBase
    {

    }
}
