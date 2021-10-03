using System;
using AssignmentCSharp.Controllers;
using AssignmentCSharp.Entity;
using AssignmentCSharp.View;

namespace AssignmentCSharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("—— Ngân Hàng Spring Hero Bank ——");
                Console.WriteLine("1. Đăng ký tài khoản.");
                Console.WriteLine("2. Đăng nhập");
                Console.WriteLine("3. Exit");
                Console.WriteLine("==========================");
                Console.WriteLine("Vui lòng chọn (từ 1 đến 3):");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        while (true)
                        {
                            Console.WriteLine("Đăng ký tài khoản: 1. Admin 2.User 0.Back");
                            var choiceRegister = Convert.ToInt32(Console.ReadLine());
                            switch (choiceRegister)
                            {
                                case 1:
                                    Console.WriteLine("Đăng ký tài khoản Admin");
                                    AdminController adminController = new AdminController();
                                    adminController.CreateAdmin();
                                    break;
                                case 2:
                                    Console.WriteLine("Đăng ký tài khoản User");   
                                    UserController userController = new UserController();
                                    userController.Register();
                                    break;
                            }
                            if (choiceRegister == 0)
                            {
                                Console.WriteLine("Back");    
                                break;
                            }
                        }
                        break;
                    case 2:
                        while (true)
                        {
                            Console.WriteLine("Đăng nhập tài khoản: 1. Admin 2.User 0.Back");
                            var choiceRegister = Convert.ToInt32(Console.ReadLine());
                            switch (choiceRegister)
                            {
                                case 1:
                                    Console.WriteLine("Đăng nhập tài khoản Admin");   
                                    AdminView adminView = new AdminView();
                                    adminView.GenerateAdminMenu();
                                    break;
                                case 2:
                                    Console.WriteLine("Đăng nhập tài khoản User");    
                                    UserView userView = new UserView();
                                    userView.GenerateUserMenu();
                                    break;
                            }
                            if (choiceRegister == 0)
                            {
                                Console.WriteLine("Back");    
                                break;
                            }
                        }
                        break;
                }

                if (choice == 3)
                {
                    Console.WriteLine("Finish!\n");
                    break;
                }
            }
        }
    }
}