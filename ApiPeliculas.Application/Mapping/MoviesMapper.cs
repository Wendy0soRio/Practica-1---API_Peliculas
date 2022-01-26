using AutoMapper;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Domain.Dtos.Request;
using ApiPeliculas.Domain.Dtos.Response;

namespace ApiPeliculas.Application.Mapping
{
    public class MoviesMapper : Profile
    {
        public MoviesMapper()
        {
            CreateMap<Pelicula, PelisResponsesMapper>()

            .ForMember(mv => mv.Titulo, opt => opt.MapFrom(src => src.Titulo))
            .ForMember(mv => mv.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(mv => mv.Director, opt => opt.MapFrom(src => src.Director))
            .ForMember(mv => mv.Critica, opt => opt.MapFrom(src => $"Puntuacion: {src.Puntuacion} estrellas,  Rating:  {src.Rating}%"))
            .ForMember(mv => mv.Año_De_Estreno, opt => opt.MapFrom(src => src.FechaPublicacion));

            CreateMap<PelisRequestMapper, Pelicula>();
        }
    }
}