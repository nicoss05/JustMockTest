using System.Collections.Generic;

namespace JustMockTest
{
    public abstract class Cine
    {
        public abstract int ButacasLibres(string pelicula);
        public abstract List<string> Descargar(string pelicula,int cantidadEntradas);
    }
}
