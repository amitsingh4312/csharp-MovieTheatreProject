using System;
using System.Collections.Generic;


namespace FinalGroupProject
{
    class Program
    {
        static void Main(string[] args)
        {
            void welcomeScreen()
            {
                Console.WriteLine("\t\t\t---------------------------------------------------------------");
                Console.WriteLine("\t\t\t-------------  - WELCOME TO MOVIEPLEX THEATRE -  --------------");
                Console.WriteLine("\t\t\t---------------------------------------------------------------\n\n");
                introScreen();              
            }

            void introScreen()                              //introduction Screen..
            {
                Console.WriteLine("Please select from the following:");
                Console.WriteLine("1: Administrator");
                Console.WriteLine("2: Guests\n");
                Console.Write("Selection: ");
                GlobalClass.selection = Console.ReadLine();
                selectionScreen();
            }

            void selectionScreen()
            {
                if(GlobalClass.selection == "1" || GlobalClass.selection == "2")
                {
                    if (GlobalClass.selection == "1")
                    {
                        //Console.WriteLine("One");
                        Console.Clear();
                        adminPortal();
                    }
                    else
                    {
                        Console.Clear();
                        guestPortal();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid Entry. Please Enter 1 or 2. Thankyou!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    introScreen();
                }
            }
            void adminPortal()
            {
                Console.WriteLine("Please enter the Admin Password");
                GlobalClass.pass = Console.ReadLine();

                if(GlobalClass.pass == "admin")
                {
                    //Console.WriteLine("Correct Password");
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  WELCOME ADMIN! \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    insideTheAdminPortal();
                }
                else if (GlobalClass.pass == "9")
                {
                    GlobalClass.passwordIncorrectCount = 0;
                    Console.Clear();
                    welcomeScreen();
                }
                else
                {
                    GlobalClass.passwordIncorrectCount++;
                    if(GlobalClass.passwordIncorrectCount < 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect Password, You have {0} more attempts or press \'9\' to go back to main menu", 5 - GlobalClass.passwordIncorrectCount);
                        Console.ForegroundColor = ConsoleColor.White;
                        adminPortal();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Limit Exceeded! Press Enter to move back to the main menu");
                        Console.ReadLine();
                        GlobalClass.passwordIncorrectCount = 0;
                        Console.ForegroundColor = ConsoleColor.White;
                        //Console.WriteLine(GlobalClass.passwordIncorrectCount);
                        Console.Clear();
                        welcomeScreen();
                    }
                }

            }


            var movies = new List<string>();
            var rating = new List<string>();
            var ratingAge = new List<int>();

            Dictionary<string, int> ratingValues = new Dictionary<string, int>
            {
                { "G", 0 },
                { "g", 0 },
                { "PG", 10 },
                { "pg", 10 },
                { "Pg", 10 },
                { "PG-13", 13 },
                { "pg-13", 13 },
                { "Pg-13", 13 },
                { "r", 15 },
                { "R", 15 },
                { "NC-17", 17 },
                { "Nc-17", 17 },
                { "nc-17", 17 }
            };


            void insideTheAdminPortal()
            {
                movies.Clear();
                rating.Clear();
                ratingAge.Clear();
                int numberOfMovies;
                Console.WriteLine("Enter the number of movies to be played : ");
                bool isNumeric = int.TryParse(Console.ReadLine(), out numberOfMovies);

                if (isNumeric == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You entered an invalid Value(Non-Numeric/Float/Empty). Please enter a valid INTEGER Value");
                    Console.ForegroundColor = ConsoleColor.White;
                    insideTheAdminPortal();
                }
                else if (numberOfMovies > 10 || numberOfMovies < 1){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Minimum movies allowed are 1 and Maximum Allowed movies are 10. Please Try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                    insideTheAdminPortal();
                }
                else
                {
                    for(int i = 0; i < numberOfMovies; i++)
                    {
                        bool movie_match;
                        //string pattern = "\\s+";
                        entername:  Console.WriteLine("Please enter movie {0} name: ", i + 1);
                        string tempName = Console.ReadLine();
                        movie_match = String.IsNullOrWhiteSpace(tempName);
                        if(movie_match == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Movie name cannot be empty, Please write the movie name!");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto entername;
                        }
                        else
                        {
                            movies.Add(tempName);
                        }

                        enterage:  Console.WriteLine("Please enter movie {0} rating or age ", i + 1);
                        int numericRating;
                        string tempRating = Console.ReadLine();
                        bool isRatingNumeric = int.TryParse(tempRating, out numericRating);
                        
                        if(isRatingNumeric == true)
                        {
                            if(numericRating > 0 && numericRating < 19)
                            {
                                ratingAge.Add(numericRating);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" Please Enter either Valid Age [0-18] OR valid Rating.\n Ratings you can use are :- \n G => General Audience, any age is good \n PG => 10 years or older \n PG-13 => 13 years or older \n R => 15 years or older. \n NC–17 => 17 years or older");
                                Console.ForegroundColor = ConsoleColor.White;
                                goto enterage;
                            }
                        }
                        else
                        {
                            if (ratingValues.ContainsKey(tempRating))
                            {
                                int tempRatingAge;
                                ratingValues.TryGetValue(tempRating, out tempRatingAge);
                                ratingAge.Add(tempRatingAge);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" Please Enter either Valid Age [0-18] OR valid Rating.\n Ratings you can use are :- \n G => General Audience, any age is good \n PG => 10 years or older \n PG-13 => 13 years or older \n R => 15 years or older. \n NC–17 => 17 years or older");
                                Console.ForegroundColor = ConsoleColor.White;
                                goto enterage;
                            }
                        }
                        
                        rating.Add(tempRating);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Movie {0} added Successfully",i+1);
                        Console.WriteLine("----------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    Console.Clear();
                    for(int i = 0; i < numberOfMovies; i++)
                    {
                        Console.WriteLine("{0}. {1} ----- [{2}] \n", i+1, movies[i], rating[i]);
                    }
                    confirmation();

                    /*foreach(string s in rating)
                    {
                        int temporary = 0;
                        if (ratingValues.TryGetValue(s, out temporary))
                        {
                            Console.WriteLine(temporary);
                        }
                        else
                        {
                            int.TryParse(s, out temporary);
                            Console.WriteLine(temporary);
                        }
                    }*/

                }

            }

            void confirmation()
            {
                Console.WriteLine("Your Movies Playing Today are listed above. Are you satisfied? (Y/N)?");

                string satisfaction = Console.ReadLine();
                if (satisfaction == "Y" || satisfaction == "y")
                {
                    Console.Clear();
                    welcomeScreen();
                }
                else if (satisfaction == "N" || satisfaction == "n")
                {
                    movies.Clear();
                    rating.Clear();
                    ratingAge.Clear();
                    Console.Clear();
                    insideTheAdminPortal();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect Value, Please try again!");
                    Console.ForegroundColor = ConsoleColor.White;
                    confirmation();
                }

            }

            void selection()
            {
                    string input = Console.ReadLine();
                    if (input == "M" || input == "m")
                    {
                        Console.Clear();
                        guestPortal();
                    }
                    else if (input == "S" || input == "s")
                    {
                           Console.Clear();
                        welcomeScreen();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Enter valid selection.");
                        Console.ForegroundColor = ConsoleColor.White;
                        selection();
                     }
             }

            void guestPortal()
            {
                String tempMovieNum, tempMovieAge;
                int tempMovieNum1, tempMovieAge1;
                int totalMovies = movies.Count;
                if (totalMovies == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There are no movies being played today. Please wait till we add movies. Press Enter to go back!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                    welcomeScreen();
                }
                else
                {
                    Console.WriteLine("There are {0} movies playing today. Please choose from the following movies.", totalMovies);

                    for (int i = 0; i < totalMovies; i++)
                    {
                        Console.WriteLine("{0}. {1} ----- [{2}] \n", i + 1, movies[i], rating[i]);
                    }

                    movieSelection:  Console.WriteLine("Which movie would you like to watch ?");
                    tempMovieNum = Console.ReadLine();

                    if (int.TryParse(tempMovieNum, out tempMovieNum1))
                    {
                        if (tempMovieNum1 > 0 && tempMovieNum1 <= totalMovies)
                        {
                            ageEntry:  Console.WriteLine("\nPlease enter your age for verification");
                            tempMovieAge = Console.ReadLine();

                            if (int.TryParse(tempMovieAge, out tempMovieAge1) == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Please Enter valid age");
                                Console.ForegroundColor = ConsoleColor.White;
                                goto ageEntry;
                            }
                            else
                            {
                                int.TryParse(tempMovieAge, out tempMovieAge1);
                                if(tempMovieAge1 > 0 && tempMovieAge1 < 100)
                                {
                                    if (tempMovieAge1 >= ratingAge[tempMovieNum1 - 1])
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("\nEnjoy the movie!!\n");
                                        Console.WriteLine("Press M to go back to guest main menu");
                                        Console.WriteLine("Press S to go back to start page");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        selection();
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Sorry you are too young for this movie.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("Press enter to make another selection");
                                        Console.ReadLine();
                                        Console.Clear();
                                        guestPortal();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please Enter a valid age");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto ageEntry;
                                }
                                
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Selection Please enter valid movie number.");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto movieSelection;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Selection Please enter valid movie number.");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto movieSelection;
                    }
                }
            }

           

            welcomeScreen();            //welcome Screen

            //Console.WriteLine(GlobalClass.selection);

        }
    }

    public class GlobalClass
    {
        public static string pass;              //password
        public static string selection;         //selection admin or guest
        public static int num;
        public static int watch;
        public static int verify;
        public static int passwordIncorrectCount = 0;
    }
}
