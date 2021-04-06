using System;
using System.IO;
using System.Windows.Forms;
using WordLibrary;

namespace WordUI
{
    public partial class StartPage : Form
    {
        public string targetDirectory = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), @"WordList\");
        public static string fileName { get; set; }
        string inputDirectory { get; set; }
        public StartPage()
        {
            InitializeComponent();
            WordList.CreateIfMissing();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckIfFileExist();
            string[] array = WordList.GetLists();
            foreach (var item in array)
            {
                listBox1.Items.Add(item);
            }
        }


        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {

                fileName = listBox1.SelectedItem.ToString();
                inputDirectory = targetDirectory + fileName;
                LoadedList loaded = new LoadedList();
                loaded.Show();
                listBox1.Items.Clear();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PracticeWord practiceForm = new PracticeWord();
            practiceForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewList newList = new NewList();
            newList.Show();
        }
        public void CheckIfFileExist()
        {
            if (Directory.GetFileSystemEntries(targetDirectory).Length == 0)
            {
                MessageBox.Show("No files found, please use \"Add new list\"");
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}