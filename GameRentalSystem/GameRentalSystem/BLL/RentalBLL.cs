using GameRentalSystem.DAL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;

namespace GameRentalSystem.BLL
{
    public class RentalBLL
    {
        private readonly RentalDAL _rentalDAL = new RentalDAL();
        private readonly GameDAL _gameDAL = new GameDAL(); 

        // For Client
        public bool RentGame(int gameId, int clientId)
        {
           
            var game = _gameDAL.GetGameById(gameId);
            if (game == null || !game.IsApproved)
            {
                return false;
            }

            
            if (_rentalDAL.IsGameCurrentlyRentedByUser(gameId, clientId))
            {
               
                return false;
            }

            try
            {
                DateTime rentalDate = DateTime.Now;
                DateTime dueDate = rentalDate.AddDays(7); 

                var rental = new Rental
                {
                    GameID = gameId,
                    ClientID = clientId,
                    RentalDate = rentalDate,
                    DueDate = dueDate,
                    Status = "Rented" 
                };

                int newRentalId = _rentalDAL.InsertRental(rental);
                return newRentalId > 0;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error renting game: " + ex.Message); 
                return false;
            }
        }

        // For Client
        public bool ReturnGame(int rentalId, int clientId)
        {
            try
            {
               
                var rental = GetRentalById(rentalId);
                if (rental == null || rental.ClientID != clientId || rental.Status != "Rented")
                {
                   
                    return false;
                }

              
                return _rentalDAL.ReturnRental(rentalId, DateTime.Now); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error returning game: " + ex.Message);
                return false;
            }
        }

       
        public List<Rental> GetClientRentals(int clientId)
        {
            try
            {
                return _rentalDAL.GetRentalsByClient(clientId); 
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("Error getting client rentals: " + ex.Message);
                return new List<Rental>(); 
            }
        }

        public Rental GetRentalById(int rentalId)
        {
            if (rentalId <= 0) return null;

            try
            {
                return _rentalDAL.GetRentalById(rentalId); 
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error getting rental by ID: " + ex.Message);
                return null;
            }
        }
    }
}