using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileProject
{
    public partial class Reservation : Form
    {
        public Reservation()
        {
            InitializeComponent();
        }
        FileStream fs;
        StreamReader sr;
        StreamWriter sw;
        string filename = "Reservation.txt";
        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {// Customer ID
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox6.TextLength != 14)
                {
                    MessageBox.Show("Customer ID cannot be empty!");
                    textBox6.Clear();
                    return;
                }

                textBox1.Focus();
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {// Customer Name
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {// Phone Number
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.TextLength != 11)
                {
                    MessageBox.Show("Phone number must be 11 digits!");
                    textBox2.Clear();
                    return;
                }
                textBox3.Focus();
            }
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {// Room Number
            if (e.KeyCode == Keys.Enter)
            {
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                {
                    MessageBox.Show("Check-out date must be after check-in date!");
                    return;
                }
                else if (int.Parse(textBox3.Text) >= 0 && int.Parse(textBox3.Text) <= 300)
                {
                    TimeSpan span = dateTimePicker2.Value - dateTimePicker1.Value;
                    int days = span.Days;
                    int price = 0;
                    if (int.Parse(textBox3.Text) <= 100)
                    {
                        price = 1000;
                        textBox7.Text = (days * price).ToString();
                    }
                    else if (int.Parse(textBox3.Text) <= 200)
                    {
                        price = 1500;
                        textBox7.Text = (days * price).ToString();
                    }
                    else if (int.Parse(textBox3.Text) <= 300)
                    {
                        price = 2500;
                        textBox7.Text = (days * price).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Room number must be valid 0 to 300!");
                }
                textBox4.Focus();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {// open file
            fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs);
            sr = new StreamReader(fs);
            MessageBox.Show("File has been opened successfully!");
        }
        private void button1_Click(object sender, EventArgs e)
        {// add reservation
            fs.Seek(0, SeekOrigin.End);
            sw.WriteLine(textBox6.Text + "|" + textBox1.Text + "|" + textBox2.Text + "|" + textBox3.Text + "|" + textBox7.Text);
            sw.Flush();
            button6_Click(sender, e);
            MessageBox.Show("Reservation has been added successfully!");
        }
        private void button4_Click(object sender, EventArgs e)
        {// display 
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            textBox5.Text = null; 
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] != '*')
                {
                    textBox5.Text += line + "\r\n";
                }
            }
            sw.Flush();
            MessageBox.Show("All Reservation are Showen");
        }

        private void button2_Click(object sender, EventArgs e)
        {// search reservation
            string customerID = textBox4.Text;
            string line;
            string[] field;
            while ((line = sr.ReadLine()) != null)
            {
                field = line.Split('|');
                if (field[0] == customerID)
                {
                    textBox6.Text = field[0];
                    textBox1.Text = field[1];
                    textBox2.Text = field[2];
                    textBox3.Text = field[3];
                    textBox7.Text = field[4];
                    MessageBox.Show("Room found!");
                    return;
                }
            }
            MessageBox.Show("Room not found!");
        }
        private void button3_Click(object sender, EventArgs e)
        {// delete reservation
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            string[] field;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                field = line.Split('|');

                if (field[0] == textBox4.Text)
                {
                    fs.Seek(count, SeekOrigin.Begin);
                    sw.Write("*");
                    sw.Flush();
                    fs.Flush();
                    MessageBox.Show("Reservation has been deleted successfully!");
                    textBox4.Text = null;
                    return;
                }
                count += line.Length + 2;
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {//clear
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = dateTimePicker1.Text = dateTimePicker2.Text = null;
        }
        FileStream SQfile;
        StreamReader SQreader;
        StreamWriter SQwriter;
        string SQfilename = "SQReservation.txt";
        private void button7_Click(object sender, EventArgs e)
        {// squeeze 

            SQfile = new FileStream(SQfilename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            SQwriter = new StreamWriter(SQfile);
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] != '*')
                {
                    SQwriter.WriteLine(line);
                    SQwriter.Flush();
                }
            }
            SQwriter.Close();
            MessageBox.Show("File has been copied successfully!");
        }
        private void button8_Click(object sender, EventArgs e)
        {// Exit
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {// Back to Main Menu
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string indexFile = "Index.txt";
            FileStream indexFs = new FileStream(indexFile, FileMode.Create, FileAccess.Write);
            StreamWriter indexWriter = new StreamWriter(indexFs);
            fs.Seek(0, SeekOrigin.Begin); // ارجع لبداية Reservation.txt
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] != '*')
                {
                    string[] field = line.Split('|');
                    string nationalID = field[0];
                    string roomNo = field[3];
                    indexWriter.WriteLine(nationalID + "|" + roomNo);
                    indexWriter.Flush();
                }
            }

            MessageBox.Show("Index.txt has been created successfully!");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
