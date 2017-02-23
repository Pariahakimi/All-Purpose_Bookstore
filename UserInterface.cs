/*
CSS 475 Final Project
TEAM APR: Andre Jenkins, Paria Hakimi, & Rosie Gu
Spring 2016 | Dr. Erika Parsons
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace APR_CS
{
    enum menuItems
    {
        SEARCH_AUTHOR = 1,
        SEARCH_BY_PUBLISHER,
        SEARCH_BY_TITLE,
        SEARCH_BY_YEAR,
        VIEW_ALL_BOOKS,
        QUIT = 9
    }

    class UserInterface
    {
        public int num = 0;

        // DB connect would go here
        public void runDriver()
        {
            int choice;
            
            do
            {
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("%          Your options:                                                    %");
                Console.WriteLine("%                  {0} : Search by Author                                    %", (int)menuItems.SEARCH_AUTHOR);
                Console.WriteLine("%                  {0} : Search by Publisher                                 %", (int)menuItems.SEARCH_BY_PUBLISHER);
                Console.WriteLine("%                  {0} : Search by Title                                     %", (int)menuItems.SEARCH_BY_TITLE);
                Console.WriteLine("%                  {0} : Search by Year                                      %", (int)menuItems.SEARCH_BY_YEAR);
                Console.WriteLine("%                  {0} : View ALL Books                                      %", (int)menuItems.VIEW_ALL_BOOKS);
                Console.WriteLine("%                  {0} : End the program                                     %", (int)menuItems.QUIT);
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");


                if (!Int32.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("You need to type in a valid, whole number!");
                    continue;
                }
                switch ((menuItems)choice)
                {
                    case menuItems.SEARCH_BY_PUBLISHER:
                        Console.WriteLine("Please enter the Publisher's name and press Enter: ");
                        string pname = Console.ReadLine();
                        Console.WriteLine();
                        this.searchByPublisher(pname);
                        break;
                    case menuItems.SEARCH_BY_TITLE:
                        Console.WriteLine("Please enter the Book title and press Enter: ");
                        string tname = Console.ReadLine();
                        Console.WriteLine();
                        this.searchByTitle(tname);
                        break;
                    case menuItems.SEARCH_BY_YEAR:
                        Console.WriteLine("Please enter the Book title and press Enter: ");

                        int byear = Int32.Parse(Console.ReadLine());
                        Console.WriteLine();
                        this.searchByYear(byear);
                        break;

                    case menuItems.VIEW_ALL_BOOKS:
                        this.viewAllBooks();
                        Console.WriteLine("Okay, you want to remove a book");
                        break;
                    case menuItems.SEARCH_AUTHOR:
                        string first, last;
                        Console.WriteLine("Please enter the Author's last name and press Enter: ");
                        last = Console.ReadLine();
                        Console.WriteLine("Please enter the Author's first name and press Enter: ");
                        first = Console.ReadLine();
                        Console.WriteLine();
                        this.searchbyAuthor(last, first);
                        break;
                    case menuItems.QUIT:
                        Console.WriteLine("Thank you for visiting All-Purpose Books!");
                        break;
                    default:
                        Console.WriteLine("I'm sorry, but that wasn't a valid menu option");
                        break;
                }

            } while (choice != (int)menuItems.QUIT);
        }

        public void viewAllBooks()
        {
            SqlConnection connect;
            string str;
            string query;
            try
            {
                str = @"";
                connect = new SqlConnection(str);
                connect.Open();

                Console.WriteLine(".... database connected");

                query = "SELECT * FROM BOOK;";
                SqlCommand view = new SqlCommand(query, connect);
                SqlDataReader dr = view.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ISBN :\t\t" + dr.GetValue(0).ToString());
                    Console.WriteLine("Book_title : \t" + dr.GetValue(1).ToString());
                    Console.WriteLine("Price :\t\t" + dr.GetValue(2).ToString());
                    Console.WriteLine("A_ID :\t\t" + dr.GetValue(3).ToString());
                    Console.WriteLine("book_year :\t" + dr.GetValue(4).ToString());
                    Console.WriteLine("Qty_stock :\t" + dr.GetValue(5).ToString());
                    Console.WriteLine("Copies_sold :\t" + dr.GetValue(6).ToString());
                    Console.WriteLine("P_ID :\t\t" + dr.GetValue(7).ToString());
                    //Console.ReadKey();
                }
                connect.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }

        }

        public void searchbyAuthor(string first, string last)
        {
            //--Select ISBN, Book_title
            //--From Book, Author
            //--Where A_ID = Author_ID AND lname = 'Peters' AND fname = 'Maureen';
            SqlConnection connect;
            string str;
            string query;
            try
            {
                str = @"";
                connect = new SqlConnection(str);
                connect.Open();
                query = "SELECT ISBN, Book_title FROM BOOK, AUTHOR WHERE A_ID = Author_ID AND lname = '" + first + "' AND fname = '" + last + "';";

                SqlCommand view = new SqlCommand(query, connect);
                SqlDataReader dr = view.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ISBN :\t\t" + dr.GetValue(0).ToString());
                    Console.WriteLine("Book title : \t" + dr.GetValue(1).ToString());
                    Console.ReadKey();
                    Console.Clear();
                }
                connect.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
        }

        public void searchByPublisher(string pubName)
        {
            //--SELECT Book_Title
            //--FROM BOOK, PUBLISHER
            //--WHERE P_ID = Publisher_ID AND P_name = 'Chivers Press';
            SqlConnection connect;
            string str;
            string query;
            try
            {
                str = @""; // string removed
                connect = new SqlConnection(str);
                connect.Open();
                query = "SELECT Book_title, P_name FROM BOOK, PUBLISHER WHERE P_ID = Publisher_ID AND P_name = '" + pubName + "';";

                SqlCommand view = new SqlCommand(query, connect);
                SqlDataReader dr = view.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("Book title :\t\t" + dr.GetValue(0).ToString());
                    Console.WriteLine("Publisher name : \t" + dr.GetValue(1).ToString());
                    Console.ReadKey();
                    Console.Clear();
                }
                connect.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
        }

        public void searchByTitle(string tName)
        {
            //SELECT Book_Title, lname, fname
            //FROM BOOK, AUTHOR
            //WHERE A_ID = Author_ID AND Book_title = ' '; 
            SqlConnection connect;
            string str;
            string query;
            try
            {
                str = @"Data Source =(LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\Andre\Documents\Visual Studio 2015\Projects\APR_CS\APR_CS\APR_DB.mdf;Integrated Security = True";
                connect = new SqlConnection(str);
                connect.Open();
                query = "SELECT Book_title, lname, fname FROM BOOK, AUTHOR WHERE A_ID = Author_ID AND Book_title = '" + tName + "';";

                SqlCommand view = new SqlCommand(query, connect);
                SqlDataReader dr = view.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("Book title : \t\t" + dr.GetValue(0).ToString());
                    Console.Write("Author name : \t\t" + dr.GetValue(1).ToString());
                    Console.WriteLine(",  " + dr.GetValue(2).ToString());
                    Console.ReadKey();
                    Console.Clear();
                }
                connect.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
        }

        public void searchByYear(int year)
        {
            //--SELECT ISBN, Book_Title
            //--FROM BOOK
            //--WHERE book_year = 1993; 
            SqlConnection connect;
            string str;
            string query;
            try
            {
                str = @"";
                connect = new SqlConnection(str);
                connect.Open();
                query = "SELECT ISBN, Book_title, book_year FROM BOOK WHERE book_year = " + year + ";";

                SqlCommand view = new SqlCommand(query, connect);
                SqlDataReader dr = view.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine("ISBN :\t\t" + dr.GetValue(0).ToString());
                    Console.WriteLine("Book title :\t\t" + dr.GetValue(1).ToString());
                    Console.WriteLine("Year : \t" + dr.GetValue(2).ToString());
                    Console.ReadKey();
                    Console.Clear();
                }
                connect.Close();
            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
        }



    }
}
    
    
