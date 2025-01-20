namespace BankAccountApp
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    class Program
    {
        // Random number generator for creating random values
        private static readonly Random random = new Random();

        // Constants for the number of accounts, transactions, and various limits
        private const int NumberOfAccounts = 20;
        private const int NumberOfTransactions = 100;
        private const double MinInitialBalance = 10;
        private const double MaxInitialBalance = 50000;
        private const double MinTransactionAmount = -500;
        private const double MaxTransactionAmount = 500;
        private const int YearsBack = 10;

        /// <summary>
        /// The main method where the program starts execution.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Create a list of bank accounts
            List<BankAccount> accounts = CreateBankAccounts(NumberOfAccounts);

            // Simulate transactions for each account
            SimulateTransactions(accounts, NumberOfTransactions);

            // Simulate transfers between accounts
            SimulateTransfers(accounts);
        }

        /// <summary>
        /// Creates a specified number of bank accounts with random details.
        /// </summary>
        /// <param name="numberOfAccounts">The number of accounts to create.</param>
        /// <returns>A list of created bank accounts.</returns>
        static List<BankAccount> CreateBankAccounts(int numberOfAccounts)
        {
            List<BankAccount> accounts = new List<BankAccount>();
            for (int i = 0; i < numberOfAccounts; i++)
            {
                try
                {
                    // Create a random bank account and add it to the list
                    BankAccount account = CreateRandomBankAccount(i + 1);
                    accounts.Add(account);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions during account creation
                    Console.WriteLine($"Account creation failed: {ex.Message}");
                }
            }
            return accounts;
        }

        /// <summary>
        /// Creates a bank account with random details.
        /// </summary>
        /// <param name="accountNumber">The account number for the new account.</param>
        /// <returns>A new bank account with random details.</returns>
        static BankAccount CreateRandomBankAccount(int accountNumber)
        {
            // Generate random initial balance, account holder name, account type, and date opened
            double initialBalance = GenerateRandomBalance(MinInitialBalance, MaxInitialBalance);
            string accountHolderName = GenerateRandomAccountHolder();
            string accountType = GenerateRandomAccountType();
            DateTime dateOpened = GenerateRandomDateOpened();

            // Return a new bank account with the generated details
            return new BankAccount($"Account {accountNumber}", initialBalance, accountHolderName, accountType, dateOpened);
        }

        /// <summary>
        /// Simulates a specified number of transactions for each account.
        /// </summary>
        /// <param name="accounts">The list of bank accounts.</param>
        /// <param name="numberOfTransactions">The number of transactions to simulate for each account.</param>
        static void SimulateTransactions(List<BankAccount> accounts, int numberOfTransactions)
        {
            foreach (BankAccount account in accounts)
            {
                for (int i = 0; i < numberOfTransactions; i++)
                {
                    // Generate a random transaction amount
                    double transactionAmount = GenerateRandomBalance(MinTransactionAmount, MaxTransactionAmount);
                    try
                    {
                        // Process the transaction for the account
                        ProcessTransaction(account, transactionAmount);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions during transaction processing
                        Console.WriteLine($"Transaction failed: {ex.Message}");
                    }
                }
                // Print a summary of the account after transactions
                PrintAccountSummary(account);
            }
        }

        /// <summary>
        /// Processes a transaction for a bank account.
        /// </summary>
        /// <param name="account">The bank account to process the transaction for.</param>
        /// <param name="transactionAmount">The amount of the transaction.</param>
        static void ProcessTransaction(BankAccount account, double transactionAmount)
        {
            if (transactionAmount >= 0)
            {
                // Credit the account if the transaction amount is positive
                account.Credit(transactionAmount);
                Console.WriteLine($"Credit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
            }
            else
            {
                // Debit the account if the transaction amount is negative
                account.Debit(-transactionAmount);
                Console.WriteLine($"Debit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
            }
        }

        /// <summary>
        /// Prints a summary of a bank account.
        /// </summary>
        /// <param name="account">The bank account to print the summary for.</param>
        static void PrintAccountSummary(BankAccount account)
        {
            Console.WriteLine($"Account: {account.AccountNumber}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
        }

        /// <summary>
        /// Simulates transfers between all pairs of accounts.
        /// </summary>
        /// <param name="accounts">The list of bank accounts.</param>
        static void SimulateTransfers(List<BankAccount> accounts)
        {
            foreach (BankAccount fromAccount in accounts)
            {
                foreach (BankAccount toAccount in accounts)
                {
                    if (fromAccount != toAccount)
                    {
                        try
                        {
                            // Generate a random transfer amount and perform the transfer
                            double transferAmount = GenerateRandomBalance(0, fromAccount.Balance);
                            fromAccount.Transfer(toAccount, transferAmount);
                            Console.WriteLine($"Transfer: {transferAmount.ToString("C")} from {fromAccount.AccountNumber} ({fromAccount.AccountHolderName}, {fromAccount.AccountType}) to {toAccount.AccountNumber} ({toAccount.AccountHolderName}, {toAccount.AccountType})");
                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions during the transfer
                            Console.WriteLine($"Transfer failed: {ex.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates a random balance within a specified range.
        /// </summary>
        /// <param name="min">The minimum balance.</param>
        /// <param name="max">The maximum balance.</param>
        /// <returns>A random balance within the specified range.</returns>
        static double GenerateRandomBalance(double min, double max)
        {
            double balance = random.NextDouble() * (max - min) + min;
            
            return Math.Round(balance, 2);
        }

        /// <summary>
        /// Generates a random account holder name.
        /// </summary>
        /// <returns>A random account holder name.</returns>
        static string GenerateRandomAccountHolder()
        {
            string[] accountHolderNames = { "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois", "Liam Johnson", "Emma Martinez", "Noah Lee", "Olivia Kim", "William Chen", "Ava Wang", "James Brown", "Isabella Nguyen", "Benjamin Wilson", "Mia Li", "Lucas Anderson", "Charlotte Liu", "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim" };

            return accountHolderNames[random.Next(0, accountHolderNames.Length)];
        }

        /// <summary>
        /// Generates a random account type.
        /// </summary>
        /// <returns>A random account type.</returns>
        static string GenerateRandomAccountType()
        {
            string[] accountTypes = { "Savings", "Checking", "Money Market", "Certificate of Deposit", "Retirement" };

            return accountTypes[random.Next(0, accountTypes.Length)];
        }

        /// <summary>
        /// Generates a random date within the past specified number of years.
        /// </summary>
        /// <returns>A random date within the past specified number of years.</returns>
        static DateTime GenerateRandomDateOpened()
        {
            DateTime startDate = new DateTime(DateTime.Today.Year - YearsBack, 1, 1);
            int daysRange = (DateTime.Today - startDate).Days;
            DateTime randomDate = startDate.AddDays(random.Next(daysRange));

            if (randomDate.Year == DateTime.Today.Year && randomDate >= DateTime.Today)
            {
                randomDate = randomDate.AddDays(-1);
            }

            return randomDate;
        }
    }
}