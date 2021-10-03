using System.Collections.Generic;
using AssignmentCSharp.Entity;

namespace AssignmentCSharp.Model
{
    public interface IAdminModel
    {
        Admin Save(Admin admin); // đăng ký tài khoản Admin
        List<TransactionHistory> ListTransactionHistory(); //danh sách lịch sử giao dịch
        bool LockOrUnLock(string idAccount, int status); //  Khóa và mở khóa người dùng
        List<TransactionHistory> FindTransactionHistoryByAccountNumber(string accountNumber); // danh sách lịch sử giao dịch theo Account Number
        Admin UpdateInfo(string id, Admin updateAccount); //  thay đổi thông tin cá nhân Admin
        Admin UpdatePassword(string id, Admin updateAccount); // thay đổi mật khẩu Admin
    }
}