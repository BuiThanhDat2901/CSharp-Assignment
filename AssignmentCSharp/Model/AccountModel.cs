using System.Collections.Generic;
using AssignmentCSharp.Entity;
using AssignmentCSharp.Util;
using MySql.Data.MySqlClient;

namespace AssignmentCSharp.Model
{
    public class AccountModel : IAccountModel
    {
        public Account Save(Account account)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "insert into accounts (account_number, username, password_hash,salt,balance,phone,status,createAt, updateAt, deleteAt) values (@account_number, @username, @password_hash, @salt,@balance,@phone,@status,@createAt,@updateAt,@deleteAt)";
                cmd.Parameters.AddWithValue("@account_number", account.AccountNumber);
                cmd.Parameters.AddWithValue("@username", account.UserName);
                cmd.Parameters.AddWithValue("@password_hash", account.PasswordHash);
                cmd.Parameters.AddWithValue("@salt", account.Salt);
                cmd.Parameters.AddWithValue("@balance", account.Balance);
                cmd.Parameters.AddWithValue("@phone", account.Phone);
                cmd.Parameters.AddWithValue("@status", account.Status);
                cmd.Parameters.AddWithValue("@createAt", account.CreateAt);
                cmd.Parameters.AddWithValue("@updateAt", account.UpdateAt);
                cmd.Parameters.AddWithValue("@deleteAt", account.DeleteAt);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return account;
                }
            }

            return null;
        }

        public TransactionHistory Deposit(TransactionHistory deposit)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "insert into transaction_history (id, senderAccountNumber, receiverAccountNumber, amount, type, createAt, updateAt, deleteAt) values (@id, @senderAccountNumber, @receiverAccountNumber, @amount, @type, @createAt , @updateAt, @deleteAt)";
                cmd.Parameters.AddWithValue("@id", deposit.Id);
                cmd.Parameters.AddWithValue("@senderAccountNumber", deposit.SenderAccountNumber);
                cmd.Parameters.AddWithValue("@receiverAccountNumber", deposit.ReceiverAccountNumber);
                cmd.Parameters.AddWithValue("@amount", deposit.Amount);
                cmd.Parameters.AddWithValue("@type", deposit.Type);
                cmd.Parameters.AddWithValue("@createAt", deposit.CreateAt);
                cmd.Parameters.AddWithValue("@updateAt", deposit.UpdateAt);
                cmd.Parameters.AddWithValue("@deleteAt", deposit.DeleteAt);
                cmd.Prepare();

                MySqlCommand cmdAccount = cnn.CreateCommand();
                cmdAccount.CommandText =
                    "update accounts set balance = balance + @amount where account_number = @account_number";
                cmdAccount.Parameters.AddWithValue("@amount", deposit.Amount);
                cmdAccount.Parameters.AddWithValue("@account_number", deposit.SenderAccountNumber);
                cmdAccount.Prepare();

                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                cmdAccount.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return deposit;
                }
            }

            return null;
        }

        public TransactionHistory Withdraw(TransactionHistory WithDraw)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "insert into transaction_history (id, senderAccountNumber, receiverAccountNumber, amount, type, createAt, updateAt, deleteAt) values (@id, @senderAccountNumber, @receiverAccountNumber, @amount, @type, @createAt , @updateAt, @deleteAt)";
                cmd.Parameters.AddWithValue("@id", WithDraw.Id);
                cmd.Parameters.AddWithValue("@senderAccountNumber", WithDraw.SenderAccountNumber);
                cmd.Parameters.AddWithValue("@receiverAccountNumber", WithDraw.ReceiverAccountNumber);
                cmd.Parameters.AddWithValue("@amount", WithDraw.Amount);
                cmd.Parameters.AddWithValue("@type", WithDraw.Type);
                cmd.Parameters.AddWithValue("@createAt", WithDraw.CreateAt);
                cmd.Parameters.AddWithValue("@updateAt", WithDraw.UpdateAt);
                cmd.Parameters.AddWithValue("@deleteAt", WithDraw.DeleteAt);
                cmd.Prepare();

                MySqlCommand cmdAccount = cnn.CreateCommand();
                cmdAccount.CommandText =
                    "update accounts set balance = balance - @amount where account_number = @account_number";
                cmdAccount.Parameters.AddWithValue("@amount", WithDraw.Amount);
                cmdAccount.Parameters.AddWithValue("@account_number", WithDraw.SenderAccountNumber);
                cmdAccount.Prepare();

                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                cmdAccount.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return WithDraw;
                }
            }

            return null;
        }

        public TransactionHistory Transfer(TransactionHistory transfer)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "insert into transaction_history (id, senderAccountNumber, receiverAccountNumber, amount, type, createAt, updateAt, deleteAt) values (@id, @senderAccountNumber, @receiverAccountNumber, @amount, @type, @createAt , @updateAt, @deleteAt)";
                cmd.Parameters.AddWithValue("@id", transfer.Id);
                cmd.Parameters.AddWithValue("@senderAccountNumber", transfer.SenderAccountNumber);
                cmd.Parameters.AddWithValue("@receiverAccountNumber", transfer.ReceiverAccountNumber);
                cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                cmd.Parameters.AddWithValue("@type", transfer.Type);
                cmd.Parameters.AddWithValue("@createAt", transfer.CreateAt);
                cmd.Parameters.AddWithValue("@updateAt", transfer.UpdateAt);
                cmd.Parameters.AddWithValue("@deleteAt", transfer.DeleteAt);
                cmd.Prepare();

                MySqlCommand cmdAccount = cnn.CreateCommand();
                cmdAccount.CommandText =
                    "update accounts set balance = balance - @amount where account_number = @account_number";
                cmdAccount.Parameters.AddWithValue("@amount", transfer.Amount);
                cmdAccount.Parameters.AddWithValue("@account_number", transfer.SenderAccountNumber);
                cmdAccount.Prepare();

                MySqlCommand cmdTranfer = cnn.CreateCommand();
                cmdTranfer.CommandText =
                    "update accounts set balance = balance + @amount where account_number = @account_number";
                cmdTranfer.Parameters.AddWithValue("@amount", transfer.Amount);
                cmdTranfer.Parameters.AddWithValue("@account_number", transfer.ReceiverAccountNumber);
                cmdTranfer.Prepare();

                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                cmdAccount.ExecuteNonQuery(); // Số bản ghi trả về
                cmdTranfer.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return transfer;
                }
            }

            return null;
        }

        public Account FindByAccountNumber(string accountNumber)
        {
            Account account = null;
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select * from accounts where account_number = @account_number";
                cmd.Parameters.AddWithValue("@account_number", accountNumber);
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account = new Account()
                    {
                        AccountNumber = reader.GetString("account_number"),
                        UserName = reader.GetString("username"),
                        PasswordHash = reader.GetString("password_hash"),
                        Salt = reader.GetString("salt"),
                        Phone = reader.GetString("phone"),
                        Status = reader.GetInt32("status"),
                        Balance = reader.GetDouble("balance"),
                        CreateAt = reader.GetDateTime("createAt"),
                        UpdateAt = reader.GetDateTime("updateAt"),
                        DeleteAt = reader.GetDateTime("deleteAt")
                    };
                }
            }

            return account;
        }

        public Account FindByUserName(string username)
        {
            Account account = null;
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select * from accounts where username = @username";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account = new Account()
                    {
                        AccountNumber = reader.GetString("account_number"),
                        UserName = reader.GetString("username"),
                        PasswordHash = reader.GetString("password_hash"),
                        Salt = reader.GetString("salt"),
                        Phone = reader.GetString("phone"),
                        Status = reader.GetInt32("status"),
                        Balance = reader.GetDouble("balance"),
                        CreateAt = reader.GetDateTime("createAt"),
                        UpdateAt = reader.GetDateTime("updateAt"),
                        DeleteAt = reader.GetDateTime("deleteAt")
                    };
                }
            }

            return account;
        }

        public Account FindByPhoneNumber(string phone)
        {
            Account account = null;
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select * from accounts where phone = @phone";
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account = new Account()
                    {
                        AccountNumber = reader.GetString("account_number"),
                        UserName = reader.GetString("username"),
                        PasswordHash = reader.GetString("password_hash"),
                        Salt = reader.GetString("salt"),
                        Phone = reader.GetString("phone"),
                        Status = reader.GetInt32("status"),
                        Balance = reader.GetDouble("balance"),
                        CreateAt = reader.GetDateTime("createAt"),
                        UpdateAt = reader.GetDateTime("updateAt"),
                        DeleteAt = reader.GetDateTime("deleteAt")
                    };
                }
            }

            return account;
        }

        public Account UpdateInfo(string id, Account updateAccount)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "update accounts set username = @username, phone = @phone, updateAt = @updateAt where account_number = @account_number";
                cmd.Parameters.AddWithValue("@username", updateAccount.UserName);
                cmd.Parameters.AddWithValue("@phone", updateAccount.Phone);
                cmd.Parameters.AddWithValue("@updateAt", updateAccount.UpdateAt);
                cmd.Parameters.AddWithValue("@account_number", id);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return updateAccount;
                }
            }

            return null;
        }

        public Account UpdatePassword(string id, Account updateAccount)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText =
                    "update accounts set password_hash = @password_hash, salt = @salt, updateAt = @updateAt where account_number = @account_number";
                cmd.Parameters.AddWithValue("@password_hash", updateAccount.PasswordHash);
                cmd.Parameters.AddWithValue("@salt", updateAccount.Salt);
                cmd.Parameters.AddWithValue("@updateAt", updateAccount.UpdateAt);
                cmd.Parameters.AddWithValue("@account_number", id);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                if (result > 0)
                {
                    return updateAccount;
                }
            }

            return null;
        }

    }
}