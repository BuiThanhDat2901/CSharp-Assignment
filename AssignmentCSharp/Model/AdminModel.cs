using System.Collections.Generic;
using AssignmentCSharp.Entity;
using AssignmentCSharp.Util;
using MySql.Data.MySqlClient;

namespace AssignmentCSharp.Model
{
    public class AdminModel:IAdminModel
    {
        public Admin Save(Admin admin)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "insert into admins (accountNumber, userName, passwordHash, salt,status,createAt,updateAt,deleteAt) values (@accountNumber,@userName,@passwordHash,@salt,@status,@createAt,@updateAt,@deleteAt)";;
                cmd.Parameters.AddWithValue("@accountNumber", admin.AccountNumber);
                cmd.Parameters.AddWithValue("@userName", admin.UserName);
                cmd.Parameters.AddWithValue("@passwordHash", admin.PasswordHash);
                cmd.Parameters.AddWithValue("@salt", admin.Salt);
                cmd.Parameters.AddWithValue("@status", admin.Status);
                cmd.Parameters.AddWithValue("@createAt", admin.CreateAt);
                cmd.Parameters.AddWithValue("@updateAt", admin.UpdateAt);
                cmd.Parameters.AddWithValue("@deleteAt", admin.DeleteAt);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); 
                if (result>0)
                {
                    return admin;
                }    
            }
            return null;        }

        public List<TransactionHistory> ListTransactionHistory()
        {
            List<TransactionHistory> list = new List<TransactionHistory>();
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select * from transaction_history";
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var transaction = new TransactionHistory()
                    {
                        Id = reader.GetString("id"),
                        SenderAccountNumber = reader.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                        Amount = reader.GetDouble("amount"),
                        Type = reader.GetInt32("type"),
                        CreateAt = reader.GetDateTime("createAt"),
                        UpdateAt = reader.GetDateTime("updateAt"),
                        DeleteAt = reader.GetDateTime("deleteAt")
                    };
                    list.Add(transaction);
                }
                return list;
            }
        }

        public bool LockOrUnLock(string idAccount, int status)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "update accounts set status = @status where account_number = @account_number";
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@account_number", idAccount);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); // Số bản ghi trả về
                if (result>0)
                {
                    return true;
                }    
            }
            return false;        }

        public List<TransactionHistory> FindTransactionHistoryByAccountNumber(string accountNumber)
        {
            List<TransactionHistory> list = new List<TransactionHistory>();
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "select * from transaction_history where senderAccountNumber = @senderAccountNumber OR receiverAccountNumber = @senderAccountNumber";
                cmd.Parameters.AddWithValue("@senderAccountNumber", accountNumber);
                cmd.Parameters.AddWithValue("@receiverAccountNumber", accountNumber);
                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var transaction = new TransactionHistory()
                    {
                        Id = reader.GetString("id"),
                        SenderAccountNumber = reader.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                        Amount = reader.GetDouble("amount"),
                        Type = reader.GetInt32("type"),
                        CreateAt = reader.GetDateTime("createAt"),
                        UpdateAt = reader.GetDateTime("updateAt"),
                        DeleteAt = reader.GetDateTime("deleteAt")
                    };
                    list.Add(transaction);
                }
                return list;
            }        }

        public Admin UpdateInfo(string id, Admin updateAccount)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "update admins set userName = @userName, updateAt = @updateAt where accountNumber = @accountNumber";
                cmd.Parameters.AddWithValue("@userName", updateAccount.UserName);
                cmd.Parameters.AddWithValue("@updateAt", updateAccount.UpdateAt);
                cmd.Parameters.AddWithValue("@accountNumber", id);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery(); 
                {
                    return updateAccount;
                }    
            }
            return null;        }

        public Admin UpdatePassword(string id, Admin updateAccount)
        {
            using (var cnn = ConnectionHelper.GetConnection())
            {
                cnn.Open();
                MySqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "update admins set passwordHash = @passwordHash, salt = @salt, updateAt = @updateAt where accountNumber = @accountNumber";
                cmd.Parameters.AddWithValue("@passwordHash", updateAccount.PasswordHash);
                cmd.Parameters.AddWithValue("@salt", updateAccount.Salt);
                cmd.Parameters.AddWithValue("@updateAt", updateAccount.UpdateAt);
                cmd.Parameters.AddWithValue("@accountNumber", id);
                cmd.Prepare();
                int result = cmd.ExecuteNonQuery();
                if (result>0)
                {
                    return updateAccount;
                }    
            }
            return null;        }
    }
}