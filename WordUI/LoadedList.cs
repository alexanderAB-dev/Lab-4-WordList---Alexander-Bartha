using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WordLibrary;

namespace WordUI
{
    public partial class LoadedList : Form
    {
        WordList loadList = WordList.LoadList(StartPage.fileName);
        public List<string> list = new List<string>();
        public List<string> list2 = new List<string>();
        public LoadedList()
        {
            InitializeComponent();
            dataGridView1.Columns.Add($"language{0}", loadList.Languages[0]);
            dataGridView1.Columns.Add($"language{1}", loadList.Languages[1]);
            loadList.List(0, s =>
            {
                int rowNr = dataGridView1.Rows.Add(s);
                dataGridView1.Rows[rowNr].Resizable = DataGridViewTriState.False;
            });
            ChangeLabelText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            loadList.wordCollection.Clear();
            list = new List<string>();
            list2 = new List<string>();
            saveData();

            for (int i = 0; i < Math.Min(list.Count, list2.Count); i++)
            {
                loadList.Add(list[i], list2[i]);
            }
            ChangeLabelText();
            loadList.Save();
            MessageBox.Show("List updated");
        }

        private void saveData()
        {

            foreach (DataGridViewRow dataGridRow in dataGridView1.Rows)
            {

                if (!(dataGridRow.Cells[0].Value == null || dataGridRow.Cells[1].Value == null))
                {
                    list.Add(dataGridRow.Cells[0].Value.ToString());
                    list2.Add(dataGridRow.Cells[1].Value.ToString());
                }

            }
        }
        public void ChangeLabelText()
        {
            label3.Text = "Total words: " + loadList.Count();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadedList_Load(object sender, EventArgs e)
        {

        }




    }

}