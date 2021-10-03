using System;
using System.Collections.Generic;
using AssignmentCSharp.Entity;
using AssignmentCSharp.Model;
using AssignmentCSharp.Util;

namespace AssignmentCSharp.Controllers
{
    public class UserController:IUserController
    {
       private IAccountModel _accountModel;
        public UserController()
        {
            _accountModel = new AccountModel();
        }
        
        private static Account GetAccountInformation()
        {
            var account = new Account();
            Console.WriteLine("Please enter username: ");
            account.UserName = Console.ReadLine();
            Console.WriteLine("Please phone number: ");
            account.Phone = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            account.Password = Console.ReadLine();
            Console.WriteLine("Please enter password confirm: ");
            account.PasswordConfirm = Console.ReadLine();
            return account;
        }
       
        private static Account GetLoginInformation()
        {
            Account account = new Account();
            Console.WriteLine("Please enter username: ");
            account.UserName = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            account.Password = Console.ReadLine();
            return account;
        }
     
        private Account GetAccountUpdateInfo()
        {
            Account account = new Account();
            Console.WriteLine("Please enter username: ");
            account.UserName = Console.ReadLine();
            Console.WriteLine("Please phone number: ");
            account.Phone = Console.ReadLine();
            return account;
        }
     
        private Account GetAccountUpdatePassword()
        {
            Account account = new Account();
            Console.WriteLine("Please enter password: ");
            account.Password = Console.ReadLine();
            Console.WriteLine("Please enter password confirm: ");
            account.PasswordConfirm = Console.ReadLine();
            return account;
        }

        private static TransactionHistory GetTransactionInfo()
        {
            TransactionHistory transactionHistory = new TransactionHistory();
            Console.WriteLine("Please enter amount>=10000: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            while (true)
            {
                if (amount<10000)
                {
                    Console.WriteLine("Wrong, please enter amount >=10000: ");
                }
                else
                {
                    transactionHistory.Amount = amount;
                    break;
                }
            }
            return transactionHistory;
        }

        private TransactionHistory GetTransferTransactionInfo()
        {
            TransactionHistory transactionHistory = new TransactionHistory();
            Console.WriteLine("Please enter amount>=10000: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            while (true)
            {
                if (amount<10000)
                {
                    Console.WriteLine("Wrong, please enter amount >=10000: ");
                }
                else
                {
                    transactionHistory.Amount = amount;
                    break;
                }
            }
            Console.WriteLine("Please enter ReceiverAccountNumber: ");
            string receiverAccountNumber = Console.ReadLine();
            while (true)
            {
                if (!CheckExistAccountNumber(receiverAccountNumber)) //2.2 Check Account Number Existed
                {
                    Console.WriteLine("Receiver Account Number are not existed! ");
                }
                else
                {
                    Console.WriteLine("Found Receiver Account Number! ");
                    transactionHistory.ReceiverAccountNumber = receiverAccountNumber;
                    break;
                }
            }
            return transactionHistory;
        }
        
        private bool CheckExistUser(string username)
        {
            return _accountModel.FindByUserName(username) != null; // kiểm tra sự tồn tại của username
        }
        
        private bool CheckExistAccountNumber(string accountNumber)
        {
            return _accountModel.FindByAccountNumber(accountNumber) != null; // kiểm tra sự tồn tại của Account Number
        }
       
        private bool CheckExistPhoneNumber(string phone)
        {
            return _accountModel.FindByPhoneNumber(phone) != null; // kiểm tra sự tồn tại của Phone Number
        }
        
        private bool CheckBalanceIsOK(double amount,double loginBalance)
        {
            Console.WriteLine($"{loginBalance} - {amount} = {loginBalance - amount}");
            return (loginBalance - amount) > 30000;
        }
        
        public Account Register()
        {
            //1. Nhập dữ liệu
            Account account;
            bool isValid; // default false
            do
            {
                account = GetAccountInformation(); //1.1 Register Input
                //2. Validate dữ liệu
                Dictionary<string, string> errors = account.CheckValid();
                // Check unique username
                if (CheckExistUser(account.UserName)) // nếu user name tồn tại (//2.1 Check User Existed)
                {
                    errors.Add("username_duplicate","Duplicate username, please choose another!");
                }
                // Check phone number
                if (CheckExistPhoneNumber(account.Phone)) //2.3 Check Phone Number Existed
                {
                    errors.Add("phone","Duplicate phone, please choose another!");
                }
                isValid = errors.Count == 0;
                // Trong trường hợp nhập thông tin lỗi thì in ra thông báo
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Value);
                    }
                    Console.WriteLine("Please return account information!\n");
                }
            } while (!isValid); // chừng nào còn lỗi thì quay lại nhập
            // Check unique account number
            if (CheckExistAccountNumber(account.AccountNumber))
            {
                account.GenerateAccountNumber();
            }
            //2. Mã hóa
            account.EncryptPassword();
            //3. Save vào database
            var result = _accountModel.Save(account);
            if (result == null)
            {
                return null;
            }
            Console.WriteLine("Register success!");
            return result;
        }

        // 0.2 đăng nhập Account
        public Account Login()
        {
            bool isValid;
            Account account = null;
            do
            {
                do
                {
                    account = GetLoginInformation(); //1.2 Login Input
                    Dictionary<string, string> errors = account.CheckValidLogin();
                    // Check exist username
                    if (!CheckExistUser(account.UserName)) // nếu user name không tồn tại (//2.1 Check User Existed)
                    {
                        errors.Add("username_not_existed","Username are not existed, please check again!");
                    }
                    isValid = errors.Count == 0;
                    // Trong trường hợp nhập thông tin lỗi thì in ra thông báo
                    if (!isValid)
                    {
                        foreach (var error in errors)
                        {
                            Console.WriteLine(error.Value);
                        }
                        Console.WriteLine("Please return account information!\n");
                    }
                } while (!isValid);
                Account existingAccount = _accountModel.FindByUserName(account.UserName);
                if (existingAccount!=null && Hash.ComparePasswordHash(account.Password,existingAccount.Salt,existingAccount.PasswordHash) && existingAccount.Status != 0)
                {
                    Console.WriteLine("Login Success!\n");
                    return existingAccount;
                } else if (existingAccount.Status == 0)
                {
                    Console.WriteLine("This account has been lock, please call admin to help\n");
                }
                else
                {
                    Console.WriteLine("Login Fails!\n");
                }
            } while (true);
        }
        
        // 1. thực hiện gửi tiền
        public void Deposit(Account login)
        {
            //1. Nhập dữ liệu
            TransactionHistory transactionHistory= GetTransactionInfo(); //1.5 Get Deposit Transaction Info Input
            // Check unique id
            if (CheckExistAccountNumber(transactionHistory.Id))
            {
                transactionHistory.GenerateAccountNumber();
            }
            //2. Deposit type transaction History
            transactionHistory.Type = 1; // Deposit
            transactionHistory.SenderAccountNumber = login.AccountNumber;
            transactionHistory.ReceiverAccountNumber = login.AccountNumber;
            //3. Save vào database
            var result = _accountModel.Deposit(transactionHistory);
            if (result == null)
            {
                Console.WriteLine("Deposit is not success!");
            }
            else
            {
                Console.WriteLine("Deposit success!");
            }
        }

        // 2. thực hiện rút tiền
        public void WithDraw(Account login,double loginBalance)
        {
            //1. Nhập dữ liệu
            TransactionHistory transactionHistory;
            bool isValid; // default false
            do
            {
                transactionHistory = GetTransactionInfo(); //1.5 Get Deposit Transaction Info Input
                //2. Validate dữ liệu
                Dictionary<string, string> errors = new Dictionary<string, string>();
                // nếu tiền mà dưới 30.000 thì không được rút hoặc gửi. Để còn hoạt động tiền trong ngân hàn
                if (!CheckBalanceIsOK(transactionHistory.Amount,loginBalance)) //2.4 Check Check Balance Is OK Existed
                {
                    errors.Add("balanceIsNotEnough","Balance < 30000 => Is Not Enough, Please Deposit!");
                }
                isValid = errors.Count == 0;
                // Trong trường hợp nhập thông tin lỗi thì in ra thông báo
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Value);
                    }
                    Console.WriteLine("Please return Transaction information!\n");
                }
            } while (!isValid); // chừng nào còn lỗi thì quay lại nhập
            //2. Deposit type transaction History
            
            // Check unique id
            if (CheckExistAccountNumber(transactionHistory.Id))
            {
                transactionHistory.GenerateAccountNumber();
            }
            transactionHistory.Type = 2; // WithDraw
            transactionHistory.SenderAccountNumber = login.AccountNumber;
            transactionHistory.ReceiverAccountNumber = login.AccountNumber;
            //3. Save vào database
            var result = _accountModel.Withdraw(transactionHistory);
            if (result == null)
            {
                Console.WriteLine("Withdraw is not success!");
            }
            else
            {
                Console.WriteLine("Withdraw success!");    
            }
        }

        // 3. thực hiện chuyển tiền
        public void Transfer(Account login,double loginBalance)
        {
            //1. Nhập dữ liệu
            TransactionHistory transactionHistory;
            bool isValid; // default false
            do
            {
                transactionHistory = GetTransferTransactionInfo(); //1.6 Get Transfer Transaction Info Input
                //2. Validate dữ liệu
                Dictionary<string, string> errors = new Dictionary<string, string>();
                // nếu tiền mà dưới 30.000 thì không được rút hoặc gửi. Để còn hoạt động tiền trong ngân hàn
                if (!CheckBalanceIsOK(transactionHistory.Amount,loginBalance)) //2.4 Check Check Balance Is OK Existed
                {
                    errors.Add("balanceIsNotEnough","Balance < 30000 => Is Not Enough, Please Deposit!");
                }
                isValid = errors.Count == 0;
                // Trong trường hợp nhập thông tin lỗi thì in ra thông báo
                if (!isValid)
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Value);
                    }
                    Console.WriteLine("Please return Transaction information!\n");
                }
            } while (!isValid); // chừng nào còn lỗi thì quay lại nhập
            //2. Deposit type transaction History
            
            // Check unique id
            if (CheckExistAccountNumber(transactionHistory.Id))
            {
                transactionHistory.GenerateAccountNumber();
            }
            transactionHistory.Type = 3; // Transfer
            transactionHistory.SenderAccountNumber = login.AccountNumber;
            //3. Save vào database
            var result = _accountModel.Transfer(transactionHistory);
            if (result == null)
            {
                Console.WriteLine("Transfer is not success!");
            }
            else
            {
                Console.WriteLine("Transfer success!");    
            }
        }

        public double CheckBalance(Account login)
        {
            throw new NotImplementedException();
        }

        public Account UpdateInformation(Account login)
        {
            throw new NotImplementedException();
        }

        public Account UpdatePassword(Account login)
        {
            throw new NotImplementedException();
        }

        public void CheckTransactionHistory(Account login)
        {
            throw new NotImplementedException();
        }
    }
}