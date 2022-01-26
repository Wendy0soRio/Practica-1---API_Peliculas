//cSpell:disable
using System;

namespace ApiPeliculas.Domain.Dtos
{
    public record PelisCreate(string Titulo, string Director, string Genero, int Puntuación, decimal Rating, string FechaPublicacion);
}