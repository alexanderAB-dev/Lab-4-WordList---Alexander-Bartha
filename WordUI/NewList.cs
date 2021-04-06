using System;

using System.Windows.Forms;
using WordLibrary;

namespace WordUI
{
    public partial class NewList : Form
    {

        public NewList()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartPage.fileName = textBox3.Text;
            WordList saveList = new WordList(textBox3.Text, textBox1.Text, textBox2.Text);
            if (saveList.WordlistExists())
            {
                DialogResult res = MessageBox.Show("File already exists.");
            }
            else
            {
                saveList.Save();
                LoadedList loaded = new LoadedList();
                loaded.Show();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}