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
    public partial class Form3 : Form
    {
        //Объявляем соединение
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = "0";
        //ID выбранного клиента
        string id_selected_clients = "0";
        //Переменная которая хранить имя товара 
        string titleItems_selected_rows = "";
        //Переменная которая хранит стоимость товара
        string priceItems_selected_rows = "";
        //Перемененная отвечающая за понимание, создан ли заказ
        bool issetOrder = false;
        //Переменная для подсчёта предварительной суммы заказа
        double prSumOrder = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = false;
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 70;
            dataGridView1.Columns[2].FillWeight = 20;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Индекс добавленной строки
            int rowNumber = dataGridView2.Rows.Add();
            //Распихивание данных по полям грида
            dataGridView2.Rows[rowNumber].Cells[0].Value = id_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[1].Value = titleItems_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[2].Value = "1";
            dataGridView2.Rows[rowNumber].Cells[3].Value = priceItems_selected_rows;
            dataGridView2.Rows[rowNumber].Cells[4].Value = priceItems_selected_rows;
            //Обновление итоговой суммы заказа
            prSumOrder += Convert.ToDouble(dataGridView2.Rows[rowNumber].Cells[4].Value) * Convert.ToDouble(dataGridView2.Rows[rowNumber].Cells[2].Value);
            //Вывод предварительной итоговой суммы заказа
            
        }
    }
}
