using System;
using System.IO;
using System.Linq;

using System.Text;
using System.Collections.Generic;

namespace WordLibrary
{

    public class WordList
    {
       public List<Word> wordCollection = new List<Word>();
        public string Name { get; }
        public string[] Languages { get; }

        public static string targetDirectory = Path.Combine(Environment.GetFolderPath(
         Environment.SpecialFolder.LocalApplicationData), @"WordList\");

        public WordList(string name, params string[] languages)
        {
            this.Name = name;
            this.Languages = languages;

        }

        public static void CreateIfMissing()
        {
            bool folderExists = Directory.Exists(targetDirectory);
            if (!folderExists)
                Directory.CreateDirectory(targetDirectory);
        }
        public static string[] GetLists()
        {
            string[] fileArray = Directory.GetFiles(targetDirectory, "").Select(Path.GetFileNameWithoutExtension).Select(p => p.Substring(0)).ToArray();

            return fileArray;
        }

        public static WordList LoadList(string name)
        {
            var enumLines = File.ReadLines(targetDirectory + name + ".dat", Encoding.UTF8);
            var languages = enumLines.First().Split(";");
            WordList wordList = new WordList(name, languages);

            foreach (var line in enumLines.Skip(1))
            {
                wordList.Add(line.Split(";"));
            }

            return wordList;
        }

        private string CreateCsvLine(string[] name)
        {
            string phaseOne = "";
            foreach (var item in name)
            {
                phaseOne += item + ";";
            }
            phaseOne = phaseOne.Remove(phaseOne.Length - 1);
            return phaseOne;
        }
        public Boolean WordlistExists()
        {
            return File.Exists(targetDirectory + Name + ".dat");
        }

        public void Save()
        {
            try
            {
                string[] lines = new string[this.Count() + 1];
                lines[0] = CreateCsvLine(Languages);
                for (int i = 0; i < wordCollection.Count(); i++)
                {
                    lines[i + 1] = CreateCsvLine(wordCollection[i].Translations);

                }
                Console.WriteLine(Languages.Count());
                File.WriteAllLines(targetDirectory + Name + ".dat", lines);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Add(params string[] translations)
        {
            if (translations.Length != this.Languages.Length)
            {
                throw new ArgumentException("Wrong number of translations");
            }
            Word word = new Word(translations);
            this.wordCollection.Add(word);

        }

        public bool Remove(int translation, string word)
        {
            Word removeWord = null;
            foreach (var item in wordCollection)
            {
                if (item.Translations[translation] == word)
                {
                    removeWord = item;
                }
            }

            return wordCollection.Remove(removeWord);

        }

        public int Count()
        {
            return wordCollection.Count();

        }

        public void List(int sortByTranslation, Action<string[]> showTranslations)
        {
            //ska sortera efter ett språk enligt languages 0 lr 1
            if (sortByTranslation < 0 || sortByTranslation >= Languages.Count())
            {
                throw new ArgumentOutOfRangeException("sortByTranslation argument out of range.");
            }
            foreach (Word word in wordCollection.OrderBy(w => w.Translations[sortByTranslation]).ToList())
            {
                showTranslations?.Invoke(word.Translations);
            }
        }

        public Word GetWordToPractice()
        {

            var ran = new Random();
            int index = ran.Next(wordCollection.Count());
            Word word = wordCollection[index];

            return word;
        }
    }



}




