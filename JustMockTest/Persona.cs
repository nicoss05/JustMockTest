using System.Collections.Generic;
using System.Linq;

namespace JustMockTest

{
    internal class Persona
    {
        public bool ObtuvoEntradas { get { return Entradas != null && Entradas.Any(); } }
        public List<string> Entradas { get; internal set; }

        internal void CompraEntradas(Cine cine, int cantidadEntradas, string pelicula)
        {
            var butacasLibres = cine.ButacasLibres(pelicula);
            if (butacasLibres >= cantidadEntradas)
            {
                Entradas = cine.Descargar(pelicula, cantidadEntradas);
            }
            else
            {
                Entradas = cine.Descargar(pelicula, butacasLibres);
            }
        }
    }
}