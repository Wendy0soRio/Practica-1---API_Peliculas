//cSpell:disable

using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApiPeliculas.Infrastructure.Data;
using ApiPeliculas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ApiPeliculas.Domain.Interfaces;

#pragma warning restore format

namespace ApiPeliculas.Infrastructure.Repositories
{
    public class MovieSqlRepository : IPelisRepository
    {
        private readonly UTMPeliculasBDContext _context;

        public MovieSqlRepository(UTMPeliculasBDContext context)
        {
            _context = context;
        }

        #region Peticiones GET
        public async Task<IEnumerable<Pelicula>> AllMovies()
        {
            var movies = _context.Peliculas.Select(m => m);
        
            return await movies.ToListAsync();
        }

        public async Task<Pelicula> GetByID(int id)
        {
            var movies = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == id);
        
            return movies;
        }

        #endregion

        //Registrar una pelicula
        public async Task<int> CreateMovie(Pelicula movie)
        {
            var entity = movie;

            await _context.Peliculas.AddAsync(entity);

            var rows = await _context.SaveChangesAsync();

            if(rows <= 0)
            
                throw new Exception("¡ERROR!: No se pudo registrar la pelicula...Verifique su información.");
            
            
            return entity.Id;
        }

        //Actualizar pelicula

        public async Task<bool> UpdateMovie(int id, Pelicula movie)
        {
            if(id <= 0 || movie == null)
            {
                throw new ArgumentException("Falta informacion para poder realizar la modificacion");
            }
            var entity = await GetByID(id);

            entity.Titulo = movie.Titulo;
            entity.Director = movie.Director;
            entity.Genero = movie.Genero;
            entity.Puntuacion = movie.Puntuacion;
            entity.Rating = movie.Rating;
            entity.FechaPublicacion = movie.FechaPublicacion;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        //Eliminar 

        public async Task<bool> DeleteMovie(int id, Pelicula movie)
        {
            //var movie = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == id);

            var entity = await GetByID(id);

            if(entity==null)
            
                _context.Peliculas.Remove(entity);
                
                var rows = await _context.SaveChangesAsync();

                return rows > 0;
            
            
        }

        //Actualizar bloque de peliculas        

    }
}