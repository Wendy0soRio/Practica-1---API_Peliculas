//#cSpell:disable

using System;
using System.Collections.Generic;

#nullable disable

namespace ApiPeliculas.Domain.Entities
{
    public partial class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Genero { get; set; }
        public int Puntuacion { get; set; }
        public decimal Rating { get; set; }
        public string FechaPublicacion { get; set; }
    }
}
