using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace OOPCurs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
            // добавление данных на форму
        {
            if (textBoxName.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка");
            }
            else
            {
                int n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = textBoxName.Text;
                dataGridView.Rows[n].Cells[1].Value = dateTimePicker.Value.ToShortDateString();
                dataGridView.Rows[n].Cells[2].Value = comboBoxFixed.Text;
            }
        }
        
        private void buttonSaveAs_Click(object sender, EventArgs e)
            // сохранение данных из формы в файл XML
        {
            if (dataGridView.Rows.Count > 0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt.TableName = "Equipment";
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Fixed");
                    ds.Tables.Add(dt);

                    foreach (DataGridViewRow rows in dataGridView.Rows)
                    {
                        DataRow row = ds.Tables["Equipment"].NewRow();
                        row["Name"] = rows.Cells[0].Value;
                        row["Date"] = rows.Cells[1].Value;
                        row["Fixed"] = rows.Cells[2].Value;
                        ds.Tables["Equipment"].Rows.Add(row);
                    }
                    ds.WriteXml("Data.xml");
                    MessageBox.Show("XML файл успешно сохранен!", "Выполнено");
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить XML файл!", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Таблица пустая!", "Ошибка");
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e) 
            // загрузка данных из файла XML на форму
        {
            if (dataGridView.Rows.Count > 0)
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла!", "Ошибка");
            }
            else
            {
                if (File.Exists("Data.xml"))
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml("Data.xml");
                    
                    foreach (DataRow item in ds.Tables["Equipment"].Rows)
                    {
                        int n = dataGridView.Rows.Add();
                        dataGridView.Rows[n].Cells[0].Value = item["Name"];
                        dataGridView.Rows[n].Cells[1].Value = item["Date"];
                        dataGridView.Rows[n].Cells[2].Value = item["Fixed"];
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден!", "Ошибка");
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) 
            // очистить таблицу
        {
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица уже пустая!", "Ошибка");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e) 
            // удалить выбранную строку
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!", "Ошибка");
            }
        }
    }
}
