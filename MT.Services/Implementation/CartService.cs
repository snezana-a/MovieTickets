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
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderedTickets> _orderedTicketsRepository;
        private readonly IUserRepository _userRepository;

        public CartService(IRepository<Cart> cartRepository, IRepository<Order> orderRepository, IRepository<OrderedTickets> orderedTicketsRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _orderedTicketsRepository = orderedTicketsRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public bool deleteTicket(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;
                var ticketToDelete = userCart.CartTickets
                    .Where(z => z.TicketId == id)
                    .FirstOrDefault();
                userCart.CartTickets.Remove(ticketToDelete);
                this._cartRepository.Update(userCart);

                return true;
            }
            return false;
        }

        public CartDto getCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);
            var userCart = loggedInUser.UserCart;
            var tickets = userCart.CartTickets.ToList();
            var ticketPrice = tickets.Select(z => new
            {
                ticketPrice = z.Movie.TicketPrice,
                quantity = z.Quantity
            }).ToList();

            double totalPrice = 0;
            foreach(var item in ticketPrice)
            {
                totalPrice += item.ticketPrice * item.quantity;
            }

            CartDto cartDtoItem = new CartDto
            {
                TicketsInCart = tickets,
                TotalPrice = totalPrice
            };

            return cartDtoItem;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;

                /*EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Successfully created order!";
                message.Status = false;*/



                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<OrderedTickets> orderedTickets = new List<OrderedTickets>();

                var result = userCart.CartTickets.Select(z => new OrderedTickets
                {
                    Id = Guid.NewGuid(),
                    TicketId = z.Movie.Id,
                    SelectedMovie = z.Movie,
                    OrderId = order.Id,
                    UserOrder = order,
                    Quantity = z.Quantity
                }).ToList();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Your order is completed. The order contains: ");

                var totalPrice = 0.0;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];
                    totalPrice += item.Quantity * item.SelectedMovie.TicketPrice;
                    sb.AppendLine(i.ToString() + ". " + item.SelectedMovie.MovieName +
                        " with price of: $" + item.SelectedMovie.TicketPrice +
                        " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total price: $" + totalPrice.ToString());

                //message.Content = sb.ToString();

                orderedTickets.AddRange(result);

                foreach (var item in orderedTickets)
                {
                    this._orderedTicketsRepository.Insert(item);
                }

                loggedInUser.UserCart.CartTickets.Clear();

                //this._mailRepository.Insert(message);

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
    
}
