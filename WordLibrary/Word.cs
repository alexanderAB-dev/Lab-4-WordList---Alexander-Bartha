using System;
using System.Collections.Generic;
using System.Text;

namespace WordLibrary
{
    public class Word
    {
        public string[] Translations { get; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }


         public Word(params string[] translations)
        {
            this.FromLanguage = 0; // set to default on start
            this.ToLanguage = 1; // set to default on start
            Translations = translations;
        }

        public Word(int fromLanguage, int toLanguage, params string[] translations)
        {

            this.FromLanguage = fromLanguage;
            this.ToLanguage = toLanguage;
            this.Translations = translations;

        }
    }
}

