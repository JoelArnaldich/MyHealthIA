using MyHealthAI.Commands;
using System;
using MyHealthAI.ViewModels;
using MyHealthAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Forms;

namespace MyHealthAI.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly AppDbContext _dbContext; 
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        public HomeViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            SetWelcomeMessage();
        }

    
        private void SetWelcomeMessage()
        {
            String currentUser = GetCurrentUser(); 

  
                Message = $"Welcome, {currentUser}!"; 
         
            
        }

        private String GetCurrentUser()
        {

            int userId = CurrentUser.LoggedInUserId;


            if (userId != 0)
            {
                var username = _dbContext.Users
                               .Where(u => u.ID == userId)
                               .Select(u => u.Username)
                               .FirstOrDefault(); 

                return username; 
            }


            return null;
        }
    }
}
