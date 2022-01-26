//#cSpell:disable

using System.Threading.Tasks;
using System.Collections.Generic;
using ApiPeliculas.Domain.Entities;

namespace ApiPeliculas.Domain.Interfaces
{
    public interface IPelisRepository
    {
        Task<IEnumerable<Pelicula>> AllMovies();
        Task<Pelicula> GetByID(int id);
        Task<int> CreateMovie(Pelicula movie);
        Task<bool> UpdateMovie(int id, Pelicula movie);

        Task<bool> DeleteMovie(int id, Pelicula movie);
        

    }
}