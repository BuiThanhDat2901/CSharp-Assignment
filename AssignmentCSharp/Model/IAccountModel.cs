using System.Collections.Generic;
using AssignmentCSharp.Entity;


namespace AssignmentCSharp.Model
{
    public interface IAccountModel
    {
        Account Save(Account account); //  đăng ký tài khoản
        TransactionHistory Deposit(TransactionHistory deposit); // thực hiện gửi tiền
        TransactionHistory Withdraw(TransactionHistory WithDraw); //  thực hiện rút tiền
        TransactionHistory Transfer(TransactionHistory WithDraw); //  thực hiện chuyển tiền
        Account FindByAccountNumber(string accountNumber); //  tìm tài khoản theo số tài khoản, truy vấn số dư
        Account FindByUserName(string username); // tìm tài khoản theo username
        Account FindByPhoneNumber(string phone); // tìm tài khoản theo phone
        Account UpdateInfo(string id, Account updateAccount);  // 5. thay đổi thông tin cá nhân
        Account UpdatePassword(string id, Account updateAccount); // 6. thay đổi mật khẩu
    }
}