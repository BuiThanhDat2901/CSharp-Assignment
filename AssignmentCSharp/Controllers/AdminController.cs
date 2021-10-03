using System;
using System.Collections.Generic;
using AssignmentCSharp.Entity;

namespace AssignmentCSharp.Controllers
{
    public class AdminController:IAdminController
    {
        public Admin CreateAdmin()
        {
            throw new NotImplementedException();
        }
        public Admin Login()
        {
            throw new System.NotImplementedException();
        }

        public  void ShowListUser()
        {
            throw new System.NotImplementedException();
        }

        public  void ShowListTransactionHistory()
        {
            throw new System.NotImplementedException();
        }

        public  Account FindUserByUserName()
        {
            throw new System.NotImplementedException();
        }

        public Account FindUserByAccountNumber()
        {
            throw new System.NotImplementedException();
        }

        public  Account FindUserByPhoneNumber()
        {
            throw new System.NotImplementedException();
        }

        public  Account CreateNewAccount()
        {
            throw new System.NotImplementedException();
        }

        public  void LockUnlockUser()
        {
            throw new System.NotImplementedException();
        }

        public  void SearchTransactionHistoryByAccountNumber()
        {
            throw new System.NotImplementedException();
        }

        public  Admin UpdateInformation(Admin login)
        {
            throw new System.NotImplementedException();

        }

        public  Admin UpdatePassword(Admin login)
        {
            var admin = new Admin();
            Console.WriteLine("Please enter new password: ");
            admin.Password = Console.ReadLine();
            Console.WriteLine("Please confirm new password: ");
            admin.PasswordConfirm = Console.ReadLine();
            return admin;

        }
    }
}