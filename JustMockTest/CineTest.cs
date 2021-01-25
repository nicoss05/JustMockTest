using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace JustMockTest
{
    [TestClass]
    public class CineTest
    {
        [TestMethod]
        public void SiLasButacasEstanLibresComproEntradasEntoncesReservoButacas()
        {
            string pelicula = "La vida es bella";
            int cantidadEntradas = 2;
            var cine = Mock.Create<Cine>();
            cine.Arrange(cine1 => cine1.ButacasLibres(pelicula)).Returns(20).Occurs(1);
            cine.Arrange(cine1 => cine1.Descargar(pelicula, cantidadEntradas)).Returns(new List<string> { "E1", "E2" });
            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntradas, pelicula);

            Assert.IsTrue(persona.ObtuvoEntradas);
            cine.Assert(cine1 => cine1.ButacasLibres(pelicula));
            cine.Assert(cine1 => cine1.Descargar(pelicula, cantidadEntradas));
        }
        [TestMethod]
        public void DadoQueNoHayButacasCuandoComproEntradasEntoncesNoReservoButacas()
        {
            string pelicula = "La vida es bella";
            int cantidadEntradas = 2;
            var cine = Mock.Create<Cine>();
            cine.Arrange(cineN => cineN.ButacasLibres(pelicula)).Returns(0);

            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntradas, pelicula);
            Assert.IsFalse(persona.ObtuvoEntradas);
            cine.Assert(cineN => cineN.ButacasLibres(pelicula));
        }

        [TestMethod]
        public void DadoQueNoHayTodasLasEntradasQueQuieroCuandoComproConsigoLasQueEstanDisponibles()
        {
            string pelicula = "la vida es bella";
            int cantidadEntradas = 6;
            int entradasDisponibles = 5;

            var cine = Mock.Create<Cine>();
            cine.Arrange(cineD => cineD.ButacasLibres(pelicula)).Returns(5);
            cine.Arrange(cineD => cineD.Descargar(pelicula, 5)).Returns(new List<string> { "E1", "E2", "E3", "E4", "E5" });
            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntradas, pelicula);
            Assert.IsTrue(persona.ObtuvoEntradas);
            Assert.AreEqual(persona.Entradas.Count, 5);
        }
        [TestMethod]
        public void DadoQueNoHayTodasLasEntradasQueQuieroCuandoComproConsigoSoloUna()
        {
            string pelicula = "La Vida Es Bella";
            int cantidadEntrada = 7;
            int entradasDisponible = 5;
            int entradasEsperadas = 1;

            var cine = Mock.Create<Cine>();
            cine.Arrange(c => c.ButacasLibres(pelicula)).Returns(entradasDisponible);
            cine.Arrange(c => c.Descargar(pelicula, 1)).Returns(new List<string> { "E1" });

            Persona persona = new Persona();
            persona.CompraEntradas(cine, cantidadEntrada, pelicula);

            Assert.IsTrue(persona.ObtuvoEntradas);
            Assert.AreEqual(entradasDisponible, persona.Entradas.Count);
            cine.Arrange(c => c.ButacasLibres(pelicula));
            cine.Arrange(c => c.Descargar(pelicula, entradasEsperadas));
        }
    }
}

