using EF04.Data;
using EF04.Enums;
using EF04.Models;
using Microsoft.EntityFrameworkCore;

namespace EF04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using BankDbContext context = new BankDbContext();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("     National Bank Management");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Add New Customer");
                Console.WriteLine("2. Open Account for Customer");
                Console.WriteLine("3. Update Account Status");
                Console.WriteLine("4. Remove Account from Customer");
                Console.WriteLine("5. List All Customers with Accounts");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==================================");
                Console.Write("Enter your choice: ");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    Pause();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddCustomer();
                        break;

                    case 2:
                        OpenAccount();
                        break;

                    case 3:
                        UpdateAccountStatus();
                        break;

                    case 4:
                        RemoveAccountFromCustomer();
                        break;

                    case 5:
                        ListAllCustomersWithAccounts();
                        break;

                    case 0:
                        Console.WriteLine("Thank you for using our system!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please select from 0 to 5.");
                        break;
                }

                Pause();
            }
        }




        static void AddCustomer()
        {
            using BankDbContext context = new BankDbContext();

            Console.WriteLine("\n========== Add New Customer ==========");

            string fullName = ReadRequired("Enter Full Name: ");

            string nationalId = ReadNationalId(context);

            DateTime dateOfBirth = ReadDateOfBirth();

            string email = ReadEmail();

            string phoneNumber = ReadPhoneNumber();

            string address = ReadRequired("Enter Address: ");

            CustomerType customerType = ReadCustomerType();

            Customer customer = new Customer
            {
                FullName = fullName,
                NationalId = nationalId,
                DateOfBirth = dateOfBirth,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                CustomerType = customerType
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine("\nCustomer added successfully!");
            Console.WriteLine($"Customer ID: {customer.Id}");
        }

        // 1. Required Fields
        static string ReadRequired(string message)
        {
            while (true)
            {
                Console.Write(message);
                string value = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(value))
                    return value;

                Console.WriteLine("This field is required! Please try again.");
            }
        }

        // 2. National ID Validation
        static string ReadNationalId(BankDbContext context)
        {
            while (true)
            {
                Console.Write("Enter National ID: ");
                string nationalId = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(nationalId) ||
                    !nationalId.All(char.IsDigit))
                {
                    Console.WriteLine("National ID must contain digits only!");
                    continue;
                }

                bool exists = context.Customers
                    .Any(c => c.NationalId == nationalId);

                if (exists)
                {
                    Console.WriteLine("This National ID already exists!");
                    continue;
                }

                return nationalId;
            }
        }

        // 3. Date of Birth Validation
        static DateTime ReadDateOfBirth()
        {
            while (true)
            {
                Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
                string input = Console.ReadLine() ?? "";

                if (!DateTime.TryParseExact(
                    input,
                    "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime dateOfBirth))
                {
                    Console.WriteLine("Invalid date format! Please try again.");
                    continue;
                }

                if (dateOfBirth.Date >= DateTime.Today)
                {
                    Console.WriteLine("Date of birth must be in the past!");
                    continue;
                }

                return dateOfBirth;
            }
        }

        // 4. Email Validation
        static string ReadEmail()
        {
            while (true)
            {
                Console.Write("Enter Email: ");
                string email = Console.ReadLine()?.Trim() ?? "";

                if (System.Net.Mail.MailAddress.TryCreate(email, out var address)
                    && address.Address == email)
                {
                    return email;
                }

                Console.WriteLine("Invalid email format! Please try again.");
            }
        }

        // 5. Phone Number Validation
        static string ReadPhoneNumber()
        {
            while (true)
            {
                Console.Write("Enter Phone Number: ");
                string phone = Console.ReadLine()?.Trim() ?? "";

                bool validCharacters = phone.All(c =>
                    char.IsDigit(c) || "+- ()".Contains(c));

                int digitCount = phone.Count(char.IsDigit);

                if (validCharacters && digitCount >= 7 && digitCount <= 15)
                {
                    return phone;
                }

                Console.WriteLine("Invalid phone number! Please try again.");
            }
        }

        // 6. Customer Type Validation
        static CustomerType ReadCustomerType()
        {
            while (true)
            {
                Console.WriteLine("\nCustomer Types:");
                Console.WriteLine("1. Individual");
                Console.WriteLine("2. Business");
                Console.Write("Choose Customer Type: ");

                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int type))
                {
                    if (type == 1)
                        return CustomerType.Individual;

                    if (type == 2)
                        return CustomerType.Business;
                }

                Console.WriteLine("Invalid choice! Enter 1 or 2.");
            }
        }



        static void OpenAccount()
        {
            using BankDbContext context = new BankDbContext();

            Console.WriteLine("\n========== Open New Account ==========");

            // 1. Account Number
            string accountNumber;

            while (true)
            {
                accountNumber = ReadRequired("Enter Account Number: ");

                bool exists = context.Accounts
                    .Any(a => a.AccountNumber == accountNumber);

                if (!exists)
                    break;

                Console.WriteLine("Account number already exists! Try another one.");
            }

            // 2. Account Type
            AccountType accountType;

            while (true)
            {
                Console.WriteLine("\nAccount Types:");
                Console.WriteLine("1. Savings");
                Console.WriteLine("2. Current");
                Console.WriteLine("3. Business");
                Console.Write("Choose Account Type: ");

                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int type) &&
                    Enum.IsDefined(typeof(AccountType), type))
                {
                    accountType = (AccountType)type;
                    break;
                }

                Console.WriteLine("Invalid account type! Choose 1, 2, or 3.");
            }

            // 3. Branch Code
            Branch branch;

            while (true)
            {
                string branchCode = ReadRequired("Enter Branch Code: ");

                branch = context.Branches
                    .FirstOrDefault(b => b.Code == branchCode)!;

                if (branch != null)
                    break;

                Console.WriteLine("Branch not found! Please enter a valid branch code.");
            }

            // 4. Customer ID
            Customer customer;

            while (true)
            {
                Console.Write("Enter Customer ID: ");
                string input = Console.ReadLine() ?? "";

                if (!int.TryParse(input, out int customerId))
                {
                    Console.WriteLine("Invalid ID! Please enter a number.");
                    continue;
                }

                customer = context.Customers
                    .FirstOrDefault(c => c.Id == customerId)!;

                if (customer != null)
                    break;

                Console.WriteLine("Customer not found! Please try again.");
            }

            // 5. Ownership Type
            OwnershipType ownershipType;

            while (true)
            {
                Console.WriteLine("\nOwnership Types:");
                Console.WriteLine("1. Primary");
                Console.WriteLine("2. CoHolder");
                Console.Write("Choose Ownership Type: ");

                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int type) &&
                    Enum.IsDefined(typeof(OwnershipType), type))
                {
                    ownershipType = (OwnershipType)type;
                    break;
                }

                Console.WriteLine("Invalid ownership type! Choose 1 or 2.");
            }

            // 6. Create Account
            Account account = new Account
            {
                AccountNumber = accountNumber,
                AccountType = accountType,
                BranchCode = branch.Code,
                CurrentBalance = 0,
                OpeningDate = DateTime.Today
            };

            // 7. Create CustomerAccount
            CustomerAccount customerAccount = new CustomerAccount
            {
                AccountNumber = accountNumber,
                CustomerId = customer.Id,
                OwnershipType = ownershipType,
                AccountStatus = AccountStatus.Active,
                OwnershipStartDate = DateTime.Today
            };

            context.Accounts.Add(account);
            context.CustomerAccounts.Add(customerAccount);

            context.SaveChanges();

            Console.WriteLine("\nAccount opened successfully!");
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Customer: {customer.FullName}");
            Console.WriteLine($"Branch: {branch.Name}");
            Console.WriteLine($"Account Type: {account.AccountType}");
            Console.WriteLine($"Ownership: {customerAccount.OwnershipType}");
            Console.WriteLine($"Initial Balance: {account.CurrentBalance}");
        }






        static void UpdateAccountStatus()
        {
            using BankDbContext context = new BankDbContext();

            Console.WriteLine("\n========== Update Account Status ==========");

            // 1. Get Account Number
            string accountNumber = ReadRequired("Enter Account Number: ");

            // 2. Get Customer ID
            int customerId;

            while (true)
            {
                Console.Write("Enter Customer ID: ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out customerId) && customerId > 0)
                    break;

                Console.WriteLine("Invalid Customer ID! Please enter a valid number.");
            }

            // 3. Find CustomerAccount
            CustomerAccount? customerAccount = context.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.AccountNumber == accountNumber &&
                    ca.CustomerId == customerId);

            if (customerAccount == null)
            {
                Console.WriteLine("No account found for this customer!");
                return;
            }

            // 4. Toggle Account Status
            customerAccount.AccountStatus =
                customerAccount.AccountStatus == AccountStatus.Active
                ? AccountStatus.Closed
                : AccountStatus.Active;

            // 5. Save Changes
            context.SaveChanges();

            Console.WriteLine("\nAccount status updated successfully!");
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"New Status: {customerAccount.AccountStatus}");
        }




        static void RemoveAccountFromCustomer()
        {
            using BankDbContext context = new BankDbContext();

            Console.WriteLine("\n========== Remove Account from Customer ==========");

            // 1. Get Account Number
            string accountNumber = ReadRequired("Enter Account Number: ");

            // 2. Get Customer ID
            int customerId;

            while (true)
            {
                Console.Write("Enter Customer ID: ");
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out customerId) && customerId > 0)
                    break;

                Console.WriteLine("Invalid Customer ID! Please enter a valid number.");
            }

            // 3. Find CustomerAccount
            CustomerAccount? customerAccount = context.CustomerAccounts
                .FirstOrDefault(ca =>
                    ca.AccountNumber == accountNumber &&
                    ca.CustomerId == customerId);

            if (customerAccount == null)
            {
                Console.WriteLine("No account found for this customer!");
                return;
            }

            // 4. Remove CustomerAccount
            context.CustomerAccounts.Remove(customerAccount);

            // 5. Save Changes
            context.SaveChanges();

            Console.WriteLine("\nAccount removed from customer successfully!");
        }




        static void ListAllCustomersWithAccounts()
        {
            using BankDbContext context = new BankDbContext();

            Console.WriteLine("\n========== All Customers with Accounts ==========");

            // Eager Loading
            var customers = context.Customers
                .Include(c => c.CustomerAccounts)
                .ThenInclude(ca => ca.Account)
                .ToList();

            if (!customers.Any())
            {
                Console.WriteLine("No customers found!");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine("\n--------------------------------------");
                Console.WriteLine($"Customer ID   : {customer.Id}");
                Console.WriteLine($"Full Name     : {customer.FullName}");
                Console.WriteLine($"National ID   : {customer.NationalId}");
                Console.WriteLine($"Email         : {customer.Email}");
                Console.WriteLine($"Phone Number  : {customer.PhoneNumber}");
                Console.WriteLine($"Customer Type : {customer.CustomerType}");

                if (!customer.CustomerAccounts.Any())
                {
                    Console.WriteLine("No accounts linked to this customer.");
                    continue;
                }

                Console.WriteLine("\nAccounts:");

                foreach (var customerAccount in customer.CustomerAccounts)
                {
                    Console.WriteLine($"  Account Number : {customerAccount.AccountNumber}");
                    Console.WriteLine($"  Account Type   : {customerAccount.Account.AccountType}");
                    Console.WriteLine($"  Balance        : {customerAccount.Account.CurrentBalance}");
                    Console.WriteLine($"  Ownership      : {customerAccount.OwnershipType}");
                    Console.WriteLine($"  Status         : {customerAccount.AccountStatus}");
                    Console.WriteLine("  -----------------------------");
                }
            }

            Console.WriteLine("\n--------------------------------------");
        }



        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
