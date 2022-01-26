using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.Interfaces;

namespace ApiPeliculas.Application.Services
{
    public class ServicesMovie : MovieServices
    {
        public bool PeliValidated(Pelicula movie)
        {
            if(string.IsNullOrEmpty(movie.Titulo))
                return false;

            if(string.IsNullOrEmpty(movie.Director))
                return false;

            if(string.IsNullOrEmpty(movie.Genero))
                return false;

            if(string.IsNullOrEmpty(movie.FechaPublicacion))
                return false;

            return true;
        }
        public bool UpdateMovie_Validated (Pelicula movie)
        {
            if(string.IsNullOrEmpty(movie.Titulo))
                return false;

            if(string.IsNullOrEmpty(movie.Director))
                return false;

            if(string.IsNullOrEmpty(movie.Genero))
                return false;

            if(string.IsNullOrEmpty(movie.FechaPublicacion))
                return false;

            return true;
        }

    
    }
}