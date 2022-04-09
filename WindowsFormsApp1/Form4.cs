using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
     
    public partial class Form4 : Form
    {
        

        public Form4()
        {
            InitializeComponent();
        }
        string connStr = "server=chuc.caseum.ru;port=33333;user=st_2_19_13;database=is_2_19_st13_KURS;password=73015211;";
        
        public Boolean CheckUser()
        {
            //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
            string sql = "SELECT * FROM Pacienti WHERE Telefon = @un";
            //Открытие соединения
            conn.Open();
            //Объявляем таблицу
            DataTable table = new DataTable();
            //Объявляем адаптер
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            //Объявляем команду
            MySqlCommand command = new MySqlCommand(sql, conn);
            command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
            command.Parameters["@un"].Value = textBox1.Text;
            //Заносим команду в адаптер
            adapter.SelectCommand = command;
            //Заполняем таблицу
            adapter.Fill(table);
            conn.Close();
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой номер телефона уже есть, введите другой номер телефона или авторизайтесь !");
                return true;
            }
            else
            {
                return false;
            }



        }
        static string sha256(string randomString)
        {
            //Тут происходит криптографическая магия. Смысл данного метода заключается в том, что строка залетает в метод
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        //Объявляем соединения с БД
        MySqlConnection conn;

        private void Form4_Load(object sender, EventArgs e)
        {
            // строка подключения к БД
            string connStr = "server=chuc.caseum.ru;port=33333;user=st_2_19_1;database=is_2_19_st1_KURS;password=58458103;";
            // создаём объект для подключения к БД
            conn = new MySqlConnection(connStr);
            //Вызов метода обновления списка преподавателей с передачей в качестве параметра ListBox
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                if (CheckUser())
                {
                    return;
                }
                //Открываем соединение
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO client1 (Fio, Kont_nomer, Parol)" +
                   "VALUES (@fio, @kont_nomer, @parol)", conn))
                    {
                        //Условная конструкция
                        if (textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == "" || textBox3.Text == "")
                        {
                            MessageBox.Show("Заполните все поля !");
                        }
                        else
                        {
                            //Использование параметров в запросах. Это повышает безопасность работы программы
                            cmd.Parameters.Add("@fio", MySqlDbType.VarChar).Value = textBox1.Text;
                            cmd.Parameters.Add("@parol", MySqlDbType.VarChar).Value = sha256(textBox3.Text);
                            cmd.Parameters.Add("@kont_nomer", MySqlDbType.VarChar).Value = textBox2.Text;
                            int insertedRows = cmd.ExecuteNonQuery();
                            // закрываем подключение  БД
                            conn.Close();
                            MessageBox.Show("Регистрация прошла успешно !");
                            this.Close();
                        }
                    }
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox3.PasswordChar = '*';
            }
        }
    }

       
}
