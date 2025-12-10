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
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileProject
{

    public partial class Room : Form
    {
        public Room()
        {
            InitializeComponent();
        }
        FileStream fs;
        StreamReader sr;
        StreamWriter sw;
        string filename = "Rooms.txt";
        private void button6_Click(object sender, EventArgs e)
        {// open file
            fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs);
            sr = new StreamReader(fs);
            MessageBox.Show("File has been opened successfully!");
        }
        private void button1_Click(object sender, EventArgs e)
        { // add room
            fs.Seek(0, SeekOrigin.End);
            string roomData = textBox1.Text + "|" + textBox5.Text + "|" + textBox3.Text;
            sw.WriteLine(roomData);
            sw.Flush();
            button5_Click(sender, e);
            MessageBox.Show("Room has been added successfully!");
        }
        private void button5_Click(object sender, EventArgs e)
        {// clear
            textBox1.Clear();
            textBox5.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox4.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {// display rooms
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            textBox4.Text = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] != '*')
                    textBox4.Text += line + "\r\n";
            }
            sw.Flush();
            MessageBox.Show("End of the File!");
        }
        private void button3_Click(object sender, EventArgs e)
        {// delete room
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            string[] field;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                field = line.Split('|');
                if (field[0] == textBox2.Text)
                {
                    fs.Seek(count, SeekOrigin.Begin);
                    sw.Write('*');
                    sw.Flush();
                    fs.Flush();
                    MessageBox.Show("Room has been deleted successfully!");
                    textBox2.Text = null;
                    return;
                }
                count += line.Length + 2;
                fs.Seek(0, SeekOrigin.End);

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {// search room
            int roomnumber = int.Parse(textBox2.Text);
            string line;
            string[] field;
            while ((line = sr.ReadLine()) != null)
            {
                field = line.Split('|');

                if (int.Parse(field[0]) == roomnumber)
                {
                    textBox1.Text = field[0];
                    textBox5.Text = field[1];
                    textBox3.Text = field[2];
                    MessageBox.Show("Room found!");
                    return;
                }
            }
            MessageBox.Show("Room not found!");
            textBox1.Text = null;
            textBox5.Text = null;
            textBox3.Text = null;
        }
        FileStream SQfile;
        StreamReader SQreader;
        StreamWriter SQwriter;
        string SQfilename = "SQRooms.txt";
        private void button7_Click_1(object sender, EventArgs e)
        {//sqeueeze 
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
            SQfile.Close();
            MessageBox.Show("File has been copied successfully!");
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {// room number
            fs.Seek(0, SeekOrigin.Begin);
            string line;
            string[] field;
            string roomNumber = textBox1.Text;
            if (e.KeyCode == Keys.Enter)
            {
                while ((line = sr.ReadLine()) != null)
                {
                    field = line.Split('|');
                    if (field[0] == textBox1.Text)
                    {
                        MessageBox.Show("Room is available!");
                        textBox1.Clear();
                        return;
                    }
                }
                if (int.Parse(roomNumber) <= 0 || int.Parse(roomNumber) > 300)
                {
                    MessageBox.Show("Room number mus be valid 0 to 300");
                    textBox1.Clear();
                }
                else
                {
                    for (int i = 0; i < 300; i++)
                    {
                        if (int.Parse(roomNumber) < 100)
                        {
                            textBox3.Text = "1000";
                            textBox5.Text = "Economy";
                            break;
                        }
                        else if (int.Parse(roomNumber) < 200)
                        {
                            textBox3.Text = "1500";
                            textBox5.Text = "Standard";
                            break;
                        }
                        else if (int.Parse(roomNumber) < 300)
                        {
                            textBox3.Text = "2500";
                            textBox5.Text = "Luxury";
                            break;
                        }
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {// Exit
            this.Close();

        }

        private void button9_Click(object sender, EventArgs e)
        {// Main Menu
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        private void Room_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
