class Account {
    private string name = "";
    private string password = "";
    private int balance;

    public string Name {
        get {return name;}
        set {name = value;}
    }

    public string Password {
        get {return password;}
        set {name = value;}
    }

    public int Balance {
        get {return balance;}
        set {balance = value;}
    }

    public Account(string Name, string Password) {
        name = Name;
        password = Password;
        balance = 0;
    }
}

namespace BankApp
{
    class Program 
    {
        public static int cnt = 0, logid = -1;
        public static Account[] accounts = new Account[10];

        static void Main() {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press 1 for login, 2 to create new account.");
            while(true) {
                int key = Convert.ToInt32(Console.ReadLine());
                if(key == 1) {
                    Login();
                    break;
                } else if(key == 2) {
                    SignUp();
                    break;
                } else {
                    Console.WriteLine("Invalid Selection! Try Again!");
                }
            }
            Console.WriteLine("Hi " + accounts[logid].Name + ", you are logged in.\n");

            while(true) {
                Console.WriteLine("Please select the functionality from the following:");
                Console.WriteLine("1 -> Check Balance");
                Console.WriteLine("2 -> Deposit Amount");
                Console.WriteLine("3 -> Withdraw Amount");
                Console.WriteLine("4 -> Change Password");
                Console.WriteLine("5 -> Logout of the account");
                Console.WriteLine("6 -> Delete Account\n");

                int cond = Convert.ToInt32(Console.ReadLine());
                switch(cond) {
                    case 1: 
                        int balance = accounts[logid].Balance;
                        Console.WriteLine("Your account balance is " + balance + "\n");
                        break;
                    case 2:
                        Console.WriteLine("Enter amount to be deposited: ");
                        int amt = Convert.ToInt32(Console.ReadLine());
                        accounts[logid].Balance += amt;
                        Console.WriteLine("Amount deposited successfully!\n");
                        break;
                    case 3:
                        Console.WriteLine("Enter amount to withdraw: ");
                        amt = Convert.ToInt32(Console.ReadLine());
                        if(amt > accounts[logid].Balance) {
                            Console.WriteLine("Transaction Failed! Insufficient Balance.\n");
                            break;
                        }
                        accounts[logid].Balance -= amt;
                        Console.WriteLine("Amount withdrawn successfully!\n");
                        break;
                    case 4:
                        ChangePassword();
                        break;
                    case 5:
                        Logout();
                        break;
                    case 6:
                        DeleteAccount();
                        break;
                    default:
                        break;
                }
                if(cond > 4) {
                    Main();
                }
            }
        }

        public static bool ValidatePassword(string s) {
            bool cap = false, spl = false, num = false;
            foreach(char x in s) {
                if(x >= 48 && x <= 57) {
                    num = true;
                } else if(x >= 65 && x <= 90) {
                    cap = true;
                } else if(x >= 97 && x <= 122) {

                } else {
                    spl = true;
                }
            } 
            if(spl && cap && num) {
                return true;
            }
            return false;
        }

        public static bool ValidateName(string s) {
            for(int id = 0; id < cnt; id++) {
                if(s == accounts[id].Name) {
                    return true;
                }
            }
            return false;
        }

        public static void ChangePassword() {
            Console.WriteLine("Please Enter the Old Password: ");
            string password = Console.ReadLine()!;
            while(password != accounts[logid].Password) {
                Console.WriteLine("Password did not match! Try again.");
            }
            while(true) {
                Console.Write("Enter New Password: ");
                password = Console.ReadLine()!;
                Console.Write("Confirm New Password: ");
                string ps2 = Console.ReadLine()!;
                if(ps2 == password) {
                    if(!ValidatePassword(ps2)) {
                        Console.WriteLine("Password did not match the requirements! Try Again.");
                    } else {
                        accounts[logid].Password = password;
                        Console.WriteLine("Password Updated Successfully!");
                        break;
                    }
                } else {
                    Console.WriteLine("Passwords did not match! Try Again.");
                }
            }
        }

        public static void SignUp() {
            Console.WriteLine("Please give the following details to Register");
            Console.Write("Full Name: ");
            string name = "", password = "";
            while(true) {
                name = Console.ReadLine()!;
                if(name == "") {
                    Console.WriteLine("Name cannot be null! Try Again.");
                } else if(ValidateName(name)) {
                    Console.WriteLine("An account already exists with this name.");
                } else {
                    break;
                }
            }
            Console.WriteLine("Create a New Password! It should contain, capital letter, special character and a number.");
            while(true) {
                Console.Write("New Password: ");
                string ps1 = Console.ReadLine()!;
                Console.Write("Confirm Password: ");
                string ps2 = Console.ReadLine()!;
                if(ps1 == ps2) {
                    if(ValidatePassword(ps1)) {
                        password = ps1;
                        break;
                    } else {
                        Console.WriteLine("Password did not meet the requirements! Try again.");
                    }
                } else {
                    Console.WriteLine("Passwords did not match! Try again.");
                }
            }
            Account newacc = new(name, password);
            if(cnt == 9) {
                Console.WriteLine("Maximum Accounts limit reached!");
            } else {
                accounts[cnt] = newacc;
                logid = cnt;
                cnt++;
            }
            Console.WriteLine("Account Created Successfully!");
        }

        public static void Login() {
            Console.Write("Please enter the name of the account holder: ");
            string name = Console.ReadLine()!;
            bool ok = false;
            for(int id = 0; id < cnt; ++id) {
                if(accounts[id].Name == name) {
                    Console.Write("Enter Password: ");
                    string x = Console.ReadLine()!;
                    if(accounts[id].Password == x) {
                        logid = id;
                        break;
                    } else {
                        ok = true;
                        Console.WriteLine("Either Name or Password is incorrect!");
                    }
                }
            }
            if(ok) {
                Main();
                return;
            }
            if(logid == -1) {
                Console.WriteLine("No account exists with this name.");
                Console.WriteLine("Press 1 to try again, 2 to create new account.");
                while(true) {
                    int key = Convert.ToInt32(Console.ReadLine());
                    if(key == 1) {
                        Login();
                        break;
                    } else if(key == 2) {
                        SignUp();
                        break;
                    } else {
                        Console.WriteLine("Invalid Selection! Try Again!");
                    }
                }
            }
        }

        public static void Logout() {
            logid = -1;
            Console.WriteLine("Logged out Successfully! New session starts now.");
            Main();
        }

        public static void DeleteAccount() {
            for(int id = logid; id < cnt - 1; ++id) {
                accounts[id] = accounts[id + 1];
            }
            cnt--;
            logid = -1;
            Console.WriteLine("Account deleted Succesfully!");
        }
    }
}