using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FirstBankOfSuncoast
{
    class Transactions
    {
        public string name { get; set; }
        public int checking { get; set; }
        public int savings { get; set; }

        public string checkingHistory { get; set; }
        public string savingsHistory { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var account = new List<Transactions>();

            if (File.Exists("FBoSC.csv"))
            {
                var fileReader = new StreamReader("FBoSC.csv");
                var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
                account = csvReader.GetRecords<Transactions>().ToList();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------WELCOME TO FIRST BANK OF SUNCOAST--------");
            Console.WriteLine();
            Console.WriteLine();

            var quitApplication = false;
            while (quitApplication == false)
            {

                Console.WriteLine("What would you like to do?");
                Console.WriteLine("Deposit");
                Console.WriteLine("Withdraw");
                Console.WriteLine("Transaction History");
                Console.WriteLine("Check Balance");
                Console.WriteLine("Quit");
                Console.WriteLine();
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                if (choice == "Deposit")
                {
                    Console.WriteLine("Would you like to deposit into checking or savings?");
                    Console.Write("Choice: ");
                    var depositChoice = Console.ReadLine();
                    if (depositChoice == "checking")
                    {
                        Console.Write("Amount: ");
                        var foundUser = account.FirstOrDefault(account => account.name == "User");
                        var transactionAmount = int.Parse(Console.ReadLine());
                        if (transactionAmount < 0)
                        {
                            Console.WriteLine("Please enter valid amount");
                        }
                        else
                        {
                            var newTransaction = account.Sum(account => account.checking + transactionAmount);
                            foundUser.checking = newTransaction;
                            var history = $"Deposited ${transactionAmount}. New Funds: ${newTransaction}.///";
                            Console.WriteLine(history);
                            var newHistory = foundUser.checkingHistory + history.ToString();
                            foundUser.checkingHistory = newHistory;
                        }
                    }
                    if (depositChoice == "savings")
                    {
                        Console.Write("Amount: ");
                        var foundUser = account.FirstOrDefault(account => account.name == "User");
                        var transactionAmount = int.Parse(Console.ReadLine());
                        if (transactionAmount < 1)
                        {
                            Console.WriteLine("Please enter valid amount");
                        }
                        else
                        {
                            var newTransaction = account.Sum(account => account.savings + transactionAmount);
                            foundUser.savings = newTransaction;
                            var history = $"Deposited ${transactionAmount}. New Funds: ${newTransaction}.///";
                            Console.WriteLine(history);
                            var newHistory = foundUser.savingsHistory + history.ToString();
                            foundUser.savingsHistory = newHistory;
                        }
                    }
                }
                if (choice == "Withdraw")
                {
                    Console.WriteLine("Would you like to withdraw from checking or savings");
                    Console.Write("Choice: ");
                    var withdrawChoice = Console.ReadLine();
                    if (withdrawChoice == "checking")
                    {
                        Console.Write("Amount: ");
                        var foundUser = account.FirstOrDefault(account => account.name == "User");
                        var transactionAmount = int.Parse(Console.ReadLine());
                        if (transactionAmount < 0)
                        {
                            Console.WriteLine("Please enter valid amount");
                        }
                        else
                        {
                            var newTransaction = account.Sum(account => account.checking - transactionAmount);
                            if (newTransaction < 0)
                            {
                                Console.WriteLine("Insufficient funds.");
                            }
                            else
                            {
                                foundUser.checking = newTransaction;
                                var history = $"Withdrew ${transactionAmount}. New Funds: ${newTransaction}.///";
                                Console.WriteLine(history);
                                var newHistory = foundUser.checkingHistory + history.ToString();
                                foundUser.checkingHistory = newHistory;


                            }
                        }

                    }
                    if (withdrawChoice == "savings")
                    {
                        Console.Write("Amount: ");
                        var foundUser = account.FirstOrDefault(account => account.name == "User");
                        var transactionAmount = int.Parse(Console.ReadLine());
                        if (transactionAmount < 1)
                        {
                            Console.WriteLine("Please enter valid amount");
                        }
                        else
                        {
                            var newTransaction = account.Sum(account => account.savings - transactionAmount);
                            if (newTransaction < 0)
                            {
                                Console.WriteLine("Insufficient funds.");
                            }
                            else
                            {
                                foundUser.savings = newTransaction;
                                var history = $"Withdrew ${transactionAmount}. New Funds: ${newTransaction}.///";
                                Console.WriteLine(history);
                                var newHistory = foundUser.savingsHistory + history.ToString();
                                foundUser.savingsHistory = newHistory;


                            }
                        }
                    }

                }
                if (choice == "Transaction History")
                {
                    Console.WriteLine("Which would you like to see, checking or savings?");
                    Console.Write("Choice: ");
                    var historyChoice = Console.ReadLine();
                    if (historyChoice == "checking")
                    {
                        var foundUser = account.FirstOrDefault(account => account.name == "User");

                        Console.WriteLine($"{foundUser.checkingHistory}");
                    }
                    if (historyChoice == "savings")
                    {
                        var foundUser = account.FirstOrDefault(account => account.name == "User");


                        Console.WriteLine($"{foundUser.savingsHistory}");
                    }
                }
                if (choice == "Check Balance")
                {


                    Console.WriteLine("Which would you like to see, checking or savings?");
                    Console.Write("Choice: ");
                    var balanceChoice = Console.ReadLine();
                    if (balanceChoice == "checking")
                    {
                        var checkingBalance = account.Max(account => account.checking);
                        Console.WriteLine($"Your checking account's balance is ${checkingBalance}.");
                    }
                    if (balanceChoice == "savings")
                    {
                        var savingsBalance = account.Max(account => account.savings);
                        Console.WriteLine($"Your savings account's balance is ${savingsBalance}.");
                    }
                }
                if (choice == "Quit")
                {
                    quitApplication = true;
                }
                var fileWriter = new StreamWriter("FBoSC.csv");
                var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                csvWriter.WriteRecords(account);
                fileWriter.Close();

            }
            Console.WriteLine("--------THANK YOU FOR BANKING WITH FBoSC--------");
        }

    }
}