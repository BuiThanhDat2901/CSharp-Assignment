using System;
using AssignmentCSharp.Controllers;
using AssignmentCSharp.Entity;

namespace AssignmentCSharp.View
{
    public class UserView:IUserView
    {
        private IUserController userController;
        Account loggedInAccount;
        public UserView()
        {
            userController = new UserController();
        }
        public void GenerateUserMenu()
        {
            Account login = userController.Login();
            while (true) {
                Console.WriteLine("—— Ngân hàng Spring Hero Bank——");
                Console.WriteLine("Chào mừng “Xuân Hùng” quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Gửi tiền");
                Console.WriteLine("2. Rút tiền");
                Console.WriteLine("3. Chuyển khoản");
                Console.WriteLine("4. Truy vấn số dư");
                Console.WriteLine("5. Thay đổi thông tin cá nhân");
                Console.WriteLine("6. Thay Thay đổi mật khẩu");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch");
                Console.WriteLine("8. Thoát");
                Console.WriteLine("==========================");
                Console.WriteLine("Nhập lựa chọn của bạn: (Từ 1 đến 8) ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice){
                    case 1:
                        userController.Deposit(login); // 1. Gửi tiền
                        break;
                    case 2:
                        userController.WithDraw(login, userController.CheckBalance(login)); // 2. Rút tiền
                        break;
                    case 3:
                        userController.Transfer(login, userController.CheckBalance(login)); // 3. Chuyển tiền cho ai đó
                        break;
                    case 4:
                        userController.CheckBalance(login); // 4. Kiểm tra số dư trong tài khoản
                        break;
                    case 5:
                        userController.UpdateInformation(login); // 5. Thay đổi thông tin cá nhân
                        break;
                    case 6:
                        userController.UpdatePassword(login); // 6. Thay đổi thông tin mật khẩu
                        break;
                    case 7:
                        userController.CheckTransactionHistory(login); // 7. Truy vấn giao dịch (sao kê)
                        break;
                }
                if (choice == 8){ // Thoát
                    Console.WriteLine("Finish\n");
                    Environment.Exit(0);
                }
            }
        }
    }
}