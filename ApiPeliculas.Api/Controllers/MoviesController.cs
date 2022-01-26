//cSpell:disable

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiPeliculas.Infrastructure.Repositories;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.Dtos;
using ApiPeliculas.Domain.Dtos.Request;
using ApiPeliculas.Domain.Dtos.Response;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using ApiPeliculas.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace ApiPeliculas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly MovieServices _service;
        private readonly IValidator<PelisRequestMapper> _createValidator;
        private readonly IPelisRepository _repository;
        public MoviesController(IPelisRepository repository, IHttpContextAccessor httpContext, IMapper mapper, MovieServices service, IValidator<PelisRequestMapper> createValidator)
        {
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._service = service;
            this._createValidator = createValidator;
        }

        [HttpGet]
        [Route("All")]
        public async  Task<IActionResult> AllMovies()
        {
            var movies = await _repository.AllMovies();
            var answerMovies = _mapper.Map<IEnumerable<Pelicula>,IEnumerable<PelisResponsesMapper>>(movies);
            return Ok(answerMovies);
        } 

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movies = await _repository.GetByID(id);

            if(movies == null)
                return NotFound("¡ERROR!: No se encontro la pelicula.");

            var answerMovie = _mapper.Map<Pelicula, PelisResponsesMapper>(movies);

            return Ok(answerMovie);
        }

        //Elimina pero aparece un error de servidor en el response (Más detalles en Infrastructure/Repositories/MovieSqlRepository.cs)
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id, Pelicula movie)
        {
            var entity = await _repository.GetByID(id);
            
            if(entity == null)
            {
                return NotFound("¡ERROR!: Eliminación cancelada - No existe la pelicula con el ID.");
            }

            var movies = _repository.DeleteMovie(id, entity);
            
            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> create(PelisRequestMapper movie)
        {
            var Val = await _createValidator.ValidateAsync(movie);

            if(!Val.IsValid)
            
                return UnprocessableEntity (Val.Errors.Select(d => $"{d.PropertyName} => Error: {d.ErrorMessage}"));

            var entity = _mapper.Map<PelisRequestMapper, Pelicula>(movie);

            var id = await _repository.CreateMovie(entity);
            
            if(id <= 0)
                return Conflict($"¡ERROR!: Ocurrio un conflicto con la información...Intentelo nuevamente.");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/Movies/{id}";
            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> Update (int id,[FromBody]Pelicula movie)
        {
            if(id <= 0)

                return NotFound("¡ERROR!: No se encontro la pelicula.");
            
            movie.Id = id;

            var MovieValidated = _service.UpdateMovie_Validated(movie);

            if(!MovieValidated)

                UnprocessableEntity($"¡ERROR!: No se pudo actualizar la informacion.");
            
            var updated = await _repository.UpdateMovie(id, movie);

            if(!updated)

                Conflict($"¡ERROR!: Ocurrio un conflicto al actualizar la información...Intentelo nuevamente.");
            
            return NoContent();
        }

        
        private Pelicula CreateObjectFromDto(PelisRequestMapper dto)
        {
            var movie = new Pelicula {
                Id = 0,
                Titulo = string.Empty,
                Director = string.Empty,
                Genero = string.Empty,
                FechaPublicacion = string.Empty
            
            };
            return movie;
        }
        
    }
}