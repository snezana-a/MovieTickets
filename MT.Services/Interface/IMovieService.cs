using MT.Data.DTO;
using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Services.Interface
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetMovieDetails(Guid? id);
        void CreateNewMovie(Movie m);
        void UpdateMovie(Movie m);
        AddToCartDto GetCartInfo(Guid? id);
        void DeleteMovie(Guid id);
        bool AddToCart(AddToCartDto item, string userId);
        List<Movie> FilterMovies();
    }
}
