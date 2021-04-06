using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordLibrary;

namespace WordUI
{
    public partial class PracticeWord : Form
    {
        private WordList words;
        public string inputString;
        public string answerString;
        public string fromLanguage;
        public string toLanguage;
        public string originalWord;
        public string matchWord;
        int wordCount;


        private void GetPracticeWord()
        {

            Word word = words.GetWordToPractice();

            fromLanguage = words.Languages[word.FromLanguage];
            toLanguage = words.Languages[word.ToLanguage];
            originalWord = word.Translations[word.FromLanguage];
            matchWord = word.Translations[word.ToLanguage];


        }
        public PracticeWord()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            inputString = textBox1.Text;
            if (e.KeyChar == (char)13)
            {
                words = WordList.LoadList(inputString);
                GetPracticeWord();
                textBox2.Text = originalWord;
                textBox1.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
        }

        private void textBox3_TextChanged(object sender, KeyPressEventArgs e)
        {
            answerString = textBox3.Text;
            if (e.KeyChar == (char)13 && answerString.Equals(matchWord))
            {

                wordCount++;
                textBox3.Clear();
                GetPracticeWord();
                textBox2.Text = originalWord;
            }
            else if (e.KeyChar == (char)13 && !answerString.Equals(toLanguage))
            {
                DialogResult res = MessageBox.Show("You had " + wordCount + " correct answers.");

                textBox3.Clear();
                textBox2.Clear();
                wordCount = 0;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}