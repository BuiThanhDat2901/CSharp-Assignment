using System;
using AssignmentCSharp.Controllers;
using AssignmentCSharp.Entity;

namespace AssignmentCSharp.View
{
    public class AdminView:IAdminView
    {
        private IAdminController adminController;

        public AdminView()
        {
            adminController = new AdminController();
        }
        
        public void GenerateAdminMenu()
        {
            Admin login = adminController.Login();
            while (true) {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine("Chào mừng “Xuân Hùng” quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Danh sách người dùng.");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                Console.WriteLine("6. Thêm người dùng mới.");
                Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11. Thoát");
                Console.WriteLine("==========================");
                Console.WriteLine("Nhập lựa chọn của bạn (Từ 1 đến 11):");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice){
                    case 1:
                        adminController.ShowListUser(); // 1. Danh sách người dùng
                        break;
                    case 2:
                        adminController.ShowListTransactionHistory(); // 2. Danh sách lịch sử giao dịch
                        break;
                    case 3:
                        adminController.FindUserByUserName(); // 3. Tìm kiếm người dùng theo tên
                        break;
                    case 4:
                        adminController.FindUserByAccountNumber(); // 4. Tìm kiếm người dùng theo số tài khoản
                        break;
                    case 5:
                        adminController.FindUserByPhoneNumber(); // 5. Tìm kiếm người dùng theo số điện thoại
                        break;
                    case 6:
                        adminController.CreateNewAccount(); // 6. Thêm người dùng mới
                        break;
                    case 7:
                        adminController.LockUnlockUser(); // 7. Khóa và mở tài khoản người dùng
                        break;
                    case 8:
                        adminController.SearchTransactionHistoryByAccountNumber(); // 8. Tìm kiếm lịch sử giao dịch theo số tài khoản
                        break;
                    case 9:
                        adminController.UpdateInformation(login); // 9. Thay đổi thông tin tài khoản admin
                        break;
                    case 10:
                        adminController.UpdatePassword(login); // 10. Thay đổi thông tin mật khẩu admin
                        break;
                }
                if (choice == 11){ // Thoát
                    Console.WriteLine("Finish!\n"); 
                    Environment.Exit(0);
                }
            }
        }
    }
}