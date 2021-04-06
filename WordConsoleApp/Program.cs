using System;
using WordLibrary;

namespace WordConsoleApp
{
    class Program
    {


        static void Main(string[] args)
        {


            WordList.CreateIfMissing();
            string input = args[0];

            switch (args[0])
            {

                case "-lists":
                    string[] array = WordList.GetLists();
                    foreach (var item in array)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case "-new":
                    if (args.Length < 3)
                    { 
                        Console.WriteLine("Too few arguments");
                        break;
                    }
                    WordList saveList = new WordList(args[1], args[2..]);
                    if (saveList.WordlistExists())
                    {
                        Console.WriteLine("Wordlist with this name exists already");
                        break;
                    }
                    Adder(saveList);
                    saveList.Save();
                    break;

                case "-add":

                    WordList addList = WordList.LoadList(args[1]);
                    Console.WriteLine(args[1]);
                    Adder(addList);
                    break;



                case "-remove":
                    WordList remove = WordList.LoadList(args[1]);
                    string[] removeWords = args[3..];
                    foreach (var removeWord in removeWords)
                    {
                        remove.Remove(int.Parse(args[2]), removeWord);
                    }
                    remove.Save();
                    break;


                case "-words":

                    WordList loadList = WordList.LoadList(args[1]);
                    Action<string[]> print = (x) =>
                    {
                        foreach (var y in x)
                        {
                            Console.WriteLine(y);

                        }
                    };
                    int translationOrder = args.Length == 3 ? int.Parse(args[2]) : 0;
                    loadList.List(translationOrder, print);
                    break;
                case "-count":
                    WordList countList = WordList.LoadList(args[1]);
                    Console.WriteLine(countList.Count());
                    break;
                case "-practice":

                    WordList randomList = WordList.LoadList(args[1]);
                    int correctCount = 0;


                    while (true)
                    {

                        Word word = randomList.GetWordToPractice();
                        string randomWord = word.Translations[word.FromLanguage];
                        string matchWord = word.Translations[word.ToLanguage];


                        Console.WriteLine("Translate word: " + randomWord);
                        string userInput = Console.ReadLine();

                        if (userInput.Equals(matchWord))
                        {

                            correctCount++;
                            continue;
                        }

                        else
                        {
                            Console.WriteLine("You had: " + correctCount + ", correct answers");
                            break;
                        }

                    }
                    break;


                default:
                    Console.WriteLine("Use any of the following parameters:");
                    Console.WriteLine("-lists\n-new <list name> <language 1> <language 2> .. <language n>");
                    Console.WriteLine("-add <list name>\n-remove <list name> <language> <word 1> <word 2> .. <word n>");
                    Console.WriteLine("-words <listname> <sortByLanguage>\n-count <listname>\n-practice <listname>");

                    break;





            }


            void Adder(WordList addList)
            {
                while (true)
                {
                    Console.WriteLine("Enter first base word");

                    string baseWord = Console.ReadLine();
                    if (baseWord == " " || baseWord == "")

                        break;
                    Console.WriteLine("Enter translation");
                    string translatedWord = Console.ReadLine();
                    addList.Add(baseWord, translatedWord);
                    addList.Save();

                }
            }
        }


    }
}
