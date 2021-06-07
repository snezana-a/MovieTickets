using Microsoft.Extensions.Logging;
using MT.Data.DTO;
using MT.Data.Models;
using MT.Repository.Interface;
using MT.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<TicketsInCart> _ticketsInCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IRepository<Movie> movieRepository, IRepository<TicketsInCart> ticketsInCartRepository, IUserRepository userRepository, ILogger<MovieService> logger)
        {
            _movieRepository = movieRepository;
            _ticketsInCartRepository = ticketsInCartRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public bool AddToCart(AddToCartDto item, string userId)
        {
            var user = this._userRepository.Get(userId);
            var userCart = user.UserCart;

            if (item.TicketId != null && userCart != null)
            {
                var product = this.GetMovieDetails(item.TicketId);
                if (product != null)
                {
                    TicketsInCart addItem = new TicketsInCart
                    {
                        Movie = product,
                        TicketId = product.Id,
                        Cart = userCart,
                        CartId = userCart.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketsInCartRepository.Insert(addItem);
                    _logger.LogInformation("Ticket was successfully added to cart!");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something went wrong! ");
            return false;
        }

        public void CreateNewMovie(Movie m)
        {
            this._movieRepository.Insert(m);
        }

        public void DeleteMovie(Guid id)
        {
            var movie = this.GetMovieDetails(id);
            this._movieRepository.Delete(movie);
        }

        public List<Movie> GetAllMovies()
        {
            _logger.LogInformation("GetAllMovies was called!");
            return this._movieRepository.GetAll().ToList();
        }

        public AddToCartDto GetCartInfo(Guid? id)
        {
            var movie = this.GetMovieDetails(id);
            AddToCartDto model = new AddToCartDto
            {
                SelectedTicket = movie,
                TicketId = movie.Id,
                Quantity = 1
            };
            return model;
        }

        public Movie GetMovieDetails(Guid? id)
        {
            return this._movieRepository.Get(id);
        }

        public void UpdateMovie(Movie m)
        {
            this._movieRepository.Update(m);
        }

        public List<Movie> FilterMovies()
        {
            return this._movieRepository.GetAll().OrderBy(x => x.Showtime).ToList();
        }
    }
}
