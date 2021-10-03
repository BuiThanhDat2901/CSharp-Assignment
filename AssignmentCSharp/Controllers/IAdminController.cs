using AssignmentCSharp.Entity;

namespace AssignmentCSharp.Controllers
{
    public interface IAdminController
    {
        //   đăng ký Admin
        Admin CreateAdmin();
        //  đăng nhập Admin
        Admin Login();
        // 1. danh sách người dùng
        void ShowListUser(); 
        // 2. danh sách lịch sử giao dịch
        void ShowListTransactionHistory();

        // 3. tìm kiếm người dùng theo tên
        Account FindUserByUserName();
        // 4. tìm kiếm người dùng theo Account Number
        Account FindUserByAccountNumber();
        // 5. tìm kiếm người dùng theo Phone Number
        Account FindUserByPhoneNumber(); 
        Account CreateNewAccount();
        void LockUnlockUser(); 
        // 8. Tìm kiếm lịch sử giao dịch theo số tài khoản
        void SearchTransactionHistoryByAccountNumber(); 
        Admin UpdateInformation(Admin login); // 9. cập nhật thông tin cá nhân admin
        Admin UpdatePassword(Admin login); // 10. cập nhật thông tin cá nhân Admin
    }
}