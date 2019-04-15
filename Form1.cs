using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Auto_Galery
{
    public partial class Form1 : Form
    {
        private void listRegistry()
        {
            try
            {
                connecion.Open();
                OleDbDataAdapter list = new OleDbDataAdapter("select * from vehicles", connecion);
                //-----// Sorgumuzun oluşturulmasını sağlıyoruz //-----// 
                DataSet dsmemory = new DataSet(); //-----// data set ile sonuçları hafızaya saklıyoruz //-----//
                list.Fill(dsmemory);
                dataGridView1.DataSource = dsmemory.Tables[0]; //-----// Datagridview in veri kaynağı olan ilk tabloyu aldık, datagridview'e aktardık //-----//
                connecion.Close();
            }
            catch (Exception explenation)
            {

                MessageBox.Show(explenation.Message);
                connecion.Close();
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listRegistry();
        }
        OleDbConnection connecion = new OleDbConnection("Provider=Microsoft.JET.OleDb.4.0; Data Source=" +
            Application.StartupPath+"\\Auto_Galery.mdb");

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();   //-----// veriler alt alta yığılmasın diye her seferinde temizliyoruz //-----//
            string brand = comboBox1.SelectedItem.ToString();
            if (brand == "Toyota")
            {
                string[] model = { "Auris", "Yaris", "Corolla" };
                comboBox2.Items.AddRange(model);   //-----// Elemanların eklenmesini sağlıyoruz //-----//
            }
            if (brand == "Honda")
            {
                string[] model = { "Civic", "Accord" };
                comboBox2.Items.AddRange(model);
            }
            if(brand == "Opel")
            {
                string[] model = { "Astra", "Vectra", "Corsa" };
                comboBox2.Items.AddRange(model);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connecion.Open();
                //------//----------------------------------------------------------//------//
                OleDbDataAdapter addcommand = new OleDbDataAdapter
                    ("insert into vehicles (licenceno,brand,model,fuel_type,back_type,kilometer,price) values " +
                    "('" + textBox1.Text + "','" + comboBox1.SelectedItem.ToString()
                    + "','" + comboBox2.SelectedItem.ToString() + "','" + comboBox3.SelectedItem.ToString() + "','" + comboBox4.SelectedItem.ToString()
                    + "','" + textBox2.Text + "','" + textBox3.Text + " ')", connecion);
                //------//Araçlar tablosuna kayıt ekle (alanar verildi) //------//
                //------//Valuesten sonra sırasıyla bu alanlara değer veriyoruz //------//
                DataSet dsmemory = new DataSet();        //------//sorgunun sonuçlarının bellekte geçici olarak depolanması için------------//------//
                addcommand.Fill(dsmemory);              //------//sonuçların dsmemory'e aktarılmasını sağladık ve access tablosuna işledik.//------//
                connecion.Close();
                MessageBox.Show("Vehicle has been added to DataBase");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                listRegistry();             //------//Kullanıcı anlık olarak verileri görsün diye bu metodu çağırdık//------//
            }
            catch (Exception explenation)
            {
                MessageBox.Show(explenation.Message);
                connecion.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connecion.Open();
                OleDbDataAdapter deletecommand = new OleDbDataAdapter("delete from vehicles where licenceno='"+textBox1.Text+"'",connecion);
                //------//------------------------------------Ruhsat no'su tb1 ile eşleşenleri sil----------------------------//------//
                DataSet dsmemory = new DataSet();
                deletecommand.Fill(dsmemory);
                connecion.Close();
                MessageBox.Show("Vehicle has been deleted from DataBase");
                listRegistry();
            }
            catch  (Exception explenation)
            {
                MessageBox.Show(explenation.Message);
                connecion.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                connecion.Open();
                OleDbDataAdapter update = new OleDbDataAdapter("update vehicles set price='" + textBox3.Text + "'where licenceno='" + textBox1.Text + "'", connecion);
                DataSet dsmemory = new DataSet();
                update.Fill(dsmemory);
                connecion.Close();
                MessageBox.Show("Vehicle's price has been updated");
                listRegistry();

            }
            catch (Exception explanation)
            {
                MessageBox.Show(explanation.Message);
                connecion.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[selected].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[selected].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[selected].Cells[2].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[selected].Cells[3].Value.ToString();
            comboBox4.Text = dataGridView1.Rows[selected].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[selected].Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.Rows[selected].Cells[6].Value.ToString();
        }
    }
}
