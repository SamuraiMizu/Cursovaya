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
    public partial class Menu : Form
    {
        // строка подключения к БД
        string connStr = "server=caseum.ru;port=33333;user=st_2_13_19;database=st_2_13_19;password=17295789;";
        //Переменная соединения
        MySqlConnection conn;
        //Логин и пароль к данной форме Вы сможете посмотреть в БД db_test таблице t_user
        public Menu()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                String login = textBox1.Text;
                String pass = textBox2.Text;
                //Запрос в БД на предмет того, если ли строка с подходящим логином и паролем
                string sql = "SELECT * FROM client WHERE Login = @un and password = @up";
                //Открытие соединения
                conn.Open();
                //Объявляем таблицу
                DataTable table = new DataTable();
                //Объявляем адаптер
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                //Объявляем команду
                MySqlCommand command = new MySqlCommand(sql, conn);
                //Определяем параметры
                command.Parameters.Add("@un", MySqlDbType.VarChar, 25);
                command.Parameters.Add("@up", MySqlDbType.VarChar, 25);
                //Присваиваем параметрам значение
                command.Parameters["@un"].Value = login;
                command.Parameters["@up"].Value = sha256(pass);
                //Заносим команду в адаптер
                adapter.SelectCommand = command;
                //Заполняем таблицу
                adapter.Fill(table);
                //Закрываем соединение
                conn.Close();
                //Если вернулась больше 0 строк, значит такой пользователь существует
                if (table.Rows.Count > 0)
                {
                    //Присваеваем глобальный признак авторизации
                    Auth.auth = true;
                    //Достаем данные пользователя в случае успеха
                    GetUserInfo(textBox1.Text);
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.ShowDialog();
                }
                else
                {
                    //Отобразить сообщение о том, что авторизаия неуспешна
                    MessageBox.Show("Логин или пароль не правильны");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4();
            Form4.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с подходящей строкой
            conn = new MySqlConnection(connStr);
        }
        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public void GetUserInfo(string Login_)
        {
            //Объявлем переменную для запроса в БД
            string selected_id_stud = textBox1.Text;
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT * FROM client WHERE Login='{Login_}'";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Auth.auth_fio = reader[1].ToString();
                Auth.auth_age = reader[2].ToString();
                Auth.auth_data = reader[3].ToString();
                Auth.auth_telef = reader[4].ToString();
                
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите номер телефона")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введите номер телефона";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Введите пароль")
            {
                textBox2.PasswordChar = '\0';
                textBox2.Text = "";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.PasswordChar = '\0';
                textBox2.Text = "Введите пароль";
                textBox2.ForeColor = Color.Gray;
            }
        }
    }
}
