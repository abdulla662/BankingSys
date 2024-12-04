using System.Security.Principal;

namespace BankSys
{
    internal class Program
    {
        public class Account
        {
            public string Name { get; set; }
            public double Balance { get; set; }

            public Account(string Name = "Unnamed Account", double Balance = 0.0)
            {
                this.Name = Name;
                this.Balance = Balance;
            }

            public bool Deposit(double amount)
            {
                if (amount > 0)
                {
                    Balance += amount;
                    return true;
                }
                return false;
            }

            virtual public bool Withdraw(double amount)
            {
                if (Balance - amount >= 0)
                {
                    Balance -= amount;
                    return true;
                }
                return false;
            }
            public static Account operator +(Account a1, Account a2)
            {
              Account Result = new Account();
                Account  names = new Account();
               
                Result.Balance = a1.Balance + a2.Balance;
                Result.Name = a1.Name + " and " + a2.Name;
                return Result;
              
            }

            public override string ToString()
            {
                return $"Name is {Name} And Balance is ${Balance} ";
            }
          
            public static class AccountUtil
            {

                public static void Display(List<Account> accounts)
                {
                    Console.WriteLine("\n=== Accounts ==========================================");
                    foreach (var acc in accounts)
                    {
                        Console.WriteLine(acc);
                    }
                }

                public static void Deposit(List<Account> accounts, double amount)
                {
                    Console.WriteLine("\n=== Depositing to Accounts =================================");
                    foreach (var acc in accounts)
                    {
                        if (acc.Deposit(amount))
                            Console.WriteLine($"Deposited {amount} to {acc}");
                        else
                            Console.WriteLine($"Failed Deposit of {amount} to {acc}");
                    }
                }

                public static void Withdraw(List<Account> accounts, double amount)
                {
                    Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
                    foreach (var acc in accounts)
                    {
                        if (acc.Withdraw(amount))
                            Console.WriteLine($"Withdrew {amount} from {acc}");
                        else
                            Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
                    }
                }

            }
            public class SavingsAccount : Account
            {
                public SavingsAccount(String Name = "default", double Balance = 0.0, double interestrate = 0.0) : base(Name, Balance)
                {
                    this.interestrate = interestrate;
                }

                public double interestrate { get; set; }
                public bool Deposit(double amount)
                {
                    return base.Deposit(amount);
                }

            }
            public class CheckingAccount : Account
            {
                public CheckingAccount(string name = "", double balance = 0.0) : base(name, balance)
                {
                    Name = name;
                    Balance = balance;
                }


                public string Name { get; set; }
                public double Balance { get; set; }
                public bool Withdraw(double amount)
                {
                    return base.Withdraw(amount - 1.50);
                }
                public override string ToString()
                {
                    return base.ToString();
                }

            }
            public class TrustAccount : SavingsAccount
            {
                public int withdrawalCount = 0;
                public int maxWithdrawals = 3;
                public TrustAccount(string name = "default", double balance = 0.0, double interestrate = 0.0) : base(name, balance, interestrate)
                {
                    Name = name;
                    Balance = balance;
                    this.interestrate = interestrate;
                }


                public string Name { get; set; }
                public double Balance { get; set; }
                public double interestrate { get; set; }
                public bool Deposit(double amount)
                {
                    if (amount >= 5000.00)
                    {
                        return base.Deposit(amount + 50.0);

                    }
                    else return base.Deposit(amount);
                }
                public override bool Withdraw(double amount)
                {
                    if (withdrawalCount >= maxWithdrawals)
                    {
                        Console.WriteLine($"Withdrawal failed for {Name}: Maximum number of withdrawals reached.");
                        return false;
                    }
                    if (amount > 0.2 * Balance)
                    {
                        Console.WriteLine($"Withdrawal failed for {Name}: Amount exceeds 20% of the account balance.");
                        return false;
                    }
                    if (base.Withdraw(amount))
                    {
                        withdrawalCount++;
                        Console.WriteLine($"Withdraw {amount} from {Name}. Remaining balance: {Balance}");
                        return true;
                    }

                    Console.WriteLine($"Withdrawal failed for {Name}: Insufficient balance.");
                    return false;
                }
                public override string ToString()
                {
                    { return $"{base.ToString}.withdrawals this  year is{withdrawalCount}"; };
                }
            }







            static void Main()
            {
                var accounts = new List<Account>();
                accounts.Add(new Account());
                accounts.Add(new Account("Larry"));
                accounts.Add(new Account("Moe", 2000));
                accounts.Add(new Account("Curly", 5000));
                Account account1 = new Account("abdulla", 3000.0);
                Account account2 = new Account("ali", 3000.0);
               Console.WriteLine(account1 + account2);


                AccountUtil.Display(accounts);
                AccountUtil.Deposit(accounts, 1000);
                AccountUtil.Withdraw(accounts, 2000);

                // Savings
                var savAccounts = new List<Account>();
                savAccounts.Add(new SavingsAccount());
                savAccounts.Add(new SavingsAccount("Superman"));
                savAccounts.Add(new SavingsAccount("Batman", 2000));
                savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

                AccountUtil.Display(savAccounts);
                AccountUtil.Deposit(savAccounts, 1000);
                AccountUtil.Withdraw(savAccounts, 2000);

                // Checking
                var checAccounts = new List<Account>();
                checAccounts.Add(new CheckingAccount());
                checAccounts.Add(new CheckingAccount("Larry2"));
                checAccounts.Add(new CheckingAccount("Moe2", 2000));
                checAccounts.Add(new CheckingAccount("Curly2", 5000));

                AccountUtil.Display(checAccounts);
                AccountUtil.Deposit(checAccounts, 1000);
                AccountUtil.Withdraw(checAccounts, 2000);
                AccountUtil.Withdraw(checAccounts, 2000);

                // Trust
                var trustAccounts = new List<Account>();
                trustAccounts.Add(new TrustAccount());
                trustAccounts.Add(new TrustAccount("Superman2"));
                trustAccounts.Add(new TrustAccount("Batman2", 2000));
                trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

                AccountUtil.Display(trustAccounts);
                AccountUtil.Deposit(trustAccounts, 1000);
                AccountUtil.Deposit(trustAccounts, 6000);
                AccountUtil.Withdraw(trustAccounts, 2000);
                AccountUtil.Withdraw(trustAccounts, 3000);
                AccountUtil.Withdraw(trustAccounts, 500);

                Console.WriteLine();
            }
        }
    }
}

