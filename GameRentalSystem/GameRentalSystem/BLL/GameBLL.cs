using GameRentalSystem.DAL;
using GameRentalSystem.Models;
using System;
using System.Collections.Generic;

namespace GameRentalSystem.BLL
{
    public class GameBLL
    {
        private readonly GameDAL _gameDAL = new GameDAL();
        private readonly VendorDAL _vendorDAL = new VendorDAL();
        private readonly RentalDAL _rentalDAL = new RentalDAL(); 

     
        public bool AddGame(Game game, int submittingUserId, string userRole)
        {
           
            if (string.IsNullOrWhiteSpace(game.Title) || game.ReleaseYear <= 0 || game.Price <= 0 || game.CategoryID <= 0)
            {
                
                return false;
            }

            if (userRole == "Vendor")
            {
                int vendorId = _vendorDAL.GetVendorIdByUserId(submittingUserId);
                if (vendorId == -1) return false;

                game.VendorID = vendorId;
                game.IsApproved = false;
                game.AdminApproverID = null;
                game.ApprovalDate = null;
            }
            else if (userRole == "Admin")
            {
              
                if (game.IsApproved)
                {
                    game.AdminApproverID = submittingUserId;
                    game.ApprovalDate = System.DateTime.Now;
                }
                else
                {
                    game.AdminApproverID = null;
                    game.ApprovalDate = null;
                }
             
                if (game.VendorID <= 0)
                {
                   
                    return false;
                }
            }
            else
            {
                return false; 
            }

            try
            {
                int newGameId = _gameDAL.InsertGame(game);
                return newGameId > 0;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error adding game: " + ex.Message);
                return false;
            }
        }

        public bool UpdateGame(Game game, int updatingUserId, string userRole)
        {
        
            if (game.GameID <= 0 || string.IsNullOrWhiteSpace(game.Title) || game.ReleaseYear <= 0 || game.Price <= 0 || game.CategoryID <= 0 || game.VendorID <= 0)
            {
                return false;
            }

            if (userRole == "Vendor")
            {
                int vendorId = _vendorDAL.GetVendorIdByUserId(updatingUserId);
               
                var existingGame = GetGameById(game.GameID); 
                if (existingGame == null || existingGame.VendorID != vendorId)
                {
                    return false;
                }
                
                game.IsApproved = existingGame.IsApproved;
                game.AdminApproverID = existingGame.AdminApproverID;
                game.ApprovalDate = existingGame.ApprovalDate;

            }
            else if (userRole != "Admin")
            {
                return false;
            }

            if (userRole == "Admin" && game.IsApproved && game.AdminApproverID == null)
            {
                game.AdminApproverID = updatingUserId;
                game.ApprovalDate = System.DateTime.Now;
            }

            else if (userRole == "Admin" && !game.IsApproved)
            {
                game.AdminApproverID = null;
                game.ApprovalDate = null;
            }
           
            try
            {
                return _gameDAL.UpdateGame(game);
            }
            catch (Exception ex)
            {
              
                Console.WriteLine("Error updating game: " + ex.Message);
                return false;
            }
        }

        // For Admin
        public bool DeleteGame(int gameId)
        {
           
            if (_rentalDAL.HasActiveRentalsForGame(gameId))
            {
                return false; 
            }

            try
            {
                return _gameDAL.DeleteGame(gameId); 
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error deleting game: " + ex.Message);
                return false;
            }
        }

        // For Admin
        public bool ApproveGame(int gameId, int adminUserId)
        {
            try
            {
               
                var game = GetGameById(gameId);
                if (game == null || game.IsApproved)
                {
                    return false;
                }

                return _gameDAL.ApproveGame(gameId, adminUserId);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error approving game: " + ex.Message);
                return false;
            }
        }

        // For Client
        public List<Game> GetApprovedGamesForClient()
        {
            try
            {
                return _gameDAL.GetApprovedGamesWithDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting approved games: " + ex.Message);
                return new List<Game>();
            }
        }

        // For Admin
        public List<Game> GetAllGamesForAdmin()
        {
            try
            {
                return _gameDAL.GetAllGamesWithDetails();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error getting all games for admin: " + ex.Message);
                return new List<Game>();
            }
        }

      
        public List<Game> GetGamesForVendor(int vendorUserId)
        {
            try
            {
                int vendorId = _vendorDAL.GetVendorIdByUserId(vendorUserId);
                if (vendorId == -1) return new List<Game>(); 

                return _gameDAL.GetGamesByVendorId(vendorId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting games for vendor: " + ex.Message);
                return new List<Game>();
            }
        }

      
        public Game GetGameById(int gameId)
        {
            if (gameId <= 0) return null;

            try
            {
                return _gameDAL.GetGameById(gameId);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error getting game by ID: " + ex.Message);
                return null; 
            }
        }

        public bool RejectGame(int gameId, int adminUserId)
        {
            if (gameId <= 0) return false; 

            try
            {
                return _gameDAL.RejectGame(gameId); 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error rejecting game: " + ex.Message);
                return false;
            }
        }
    }
}