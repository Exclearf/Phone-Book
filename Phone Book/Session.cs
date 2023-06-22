using Microsoft.EntityFrameworkCore;
using Phone_Book.Data;
using Phone_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Phone_Book
{
    public class Session
    {
        private ContactContext contactContext = new ContactContext();
        private void PrintLine(int v = 40)
        {
            Console.WriteLine(new string('-', v));
        }
        private void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public Session()
        {
            SessionStart();
        }

        private void SessionStart()
        {

            while (true)
            {
                Console.Clear();
                ShowMenu();
                string? choice = Console.ReadLine();
                if (choice != null)
                {
                    switch (choice)
                    {
                        case "1":
                            AddContact();
                            break;
                        case "2":
                            if (DisplayContacts())
                            {
                                RemoveContact();
                            }
                            else
                            {
                                Console.WriteLine("There is nothing to remove!");
                                PressAnyKey();
                            }
                            break;
                        case "3":
                            DisplayContacts();
                            PrintLine();
                            PressAnyKey();
                            break;
                        default:
                            SessionEnd();
                            break;
                    }
                }
                else
                {
                    SessionEnd();
                }
            }
        }

        private void RemoveContact()
        {
            PrintLine();
            Console.WriteLine("Type the ID of the record you wish to remove:");
            Console.Write("Input: ");
            var contactId = Console.ReadLine();
            Contact? contactToRemove = contactContext.Contacts.Find(Convert.ToInt32(contactId));
            if (contactToRemove != null)
            {
                contactContext.Contacts.Remove(contactToRemove);
                contactContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("This ID does not exist!");
                PressAnyKey();
            }
        }

        private void ShowMenu()
        {
            PrintLine();
            Console.WriteLine("Your Phone Book");
            Console.WriteLine("1. Add a new Contact");
            Console.WriteLine("2. Remove a Contact");
            Console.WriteLine("3. Display all Contacts");
            Console.WriteLine("4. Press any key to exit");
            Console.Write("Input: ");
        }

        private void SessionEnd()
        {
            Environment.Exit(0);
        }

        private void AddContact()
        {
            Console.Clear();
            PrintLine();
            Console.WriteLine("Adding a Contact");
            Console.Write("Enter Contacts Name (can be left empty): ");
            var name = Console.ReadLine();
            Console.Write("Enter Contacts Phone: ");
            var phoneNum = Console.ReadLine();
            contactContext.Add(new Contact
            {
                Name = name,
                PhoneNumber = phoneNum
            });
            contactContext.SaveChanges();
        }

        private bool DisplayContacts()
        {
            var contacts = contactContext.Contacts.ToList();
            Console.Clear();
            PrintLine();
            Console.WriteLine("ID     NAME          PHONE");
            if (contacts.Count > 0)
            {
                foreach (var contact in contacts)
                    Console.WriteLine($"{contact.ContactId}     {contact.Name}          {contact.PhoneNumber}");
                return true;
            }
            else {
                Console.WriteLine($"\nNo records found!\n");
                return false;
            }
        }
    }
}
