using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace RecuperationDeDonnees
{
    public partial class Form1 : Form
    {
        SaveFileDialog SFD1 = new SaveFileDialog();
        String Coord;
        String File;
        String Xs;
        String Ys;
        String Zs;
        Bitmap bmp = new Bitmap(300, 360);
        Bitmap bmp2 = new Bitmap(300, 360);
        Bitmap bmp3 = new Bitmap(300, 360);
        Bitmap bmp4 = new Bitmap(360, 360);
        int Timy;
        int Xd;
        int Yd;
        int Zd;


        public Form1()
        {
            InitializeComponent();
            getAvailabePorts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void getAvailabePorts ()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(comboBox1.Text == "" || comboBox3.Text == "")
                {
                    textBox2.Text = "Please select Port settings";
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox3.Text);
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    textBox1.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;


                }
            }
            catch(UnauthorizedAccessException)
            {
                textBox2.Text = "Unauthorized Access";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = true;
            textBox1.Enabled = false;
            timer1.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(textBox1.Text);
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = serialPort1.ReadLine();
                Coord = serialPort1.ReadLine();
            }
            catch (TimeoutException)
            {
                textBox2.Text = "TimeoutExecption";
            }
            //Initialisation du timer

            File = File + Coord + "\n";
            // Enregistrement des coordonnées

            String[] Spliter = Regex.Split(Coord.Substring(0, Coord.Length - 0), " ");
            //Split String

            
            Xs = Spliter[1].ToString();             
            Xs = Xs.Replace('.', ',');
            float Xf = float.Parse(Xs);
            int X = Convert.ToInt32(Xf);
            /*if (Xd >= 180 || Xd <= -180)
            {
                Xd = 0;
            }*/
          

            Ys = Convert.ToString(Spliter[2]);
            Ys = Ys.Replace('.', ',');
            float Yf = float.Parse(Ys);
            int Y = Convert.ToInt32(Yf);
            /*if (Yd >= 180 || Yd <= -180)
            {
                Yd = 0;
            }*/

            /*Zs = Convert.ToString(Spliter[2]);
            Zs = Zs.Replace('.', ',');
            float Zf = float.Parse(Zs);
            int Z = Convert.ToInt32(Zf);
            /*if (Zd >= 180 || Zd <= -180)
            {
                Zd = 0;
            }*/
            //Calcul des coordonées


            XBox.Text = "X " + X;
            YBox.Text = "Y " + Y;
            ZBox.Text = "Z " + Zd;
            //Text Box
            
            
            Graphics g = Graphics.FromImage(bmp);
            Graphics g2 = Graphics.FromImage(bmp2);
            Graphics g3 = Graphics.FromImage(bmp3);
            Graphics g4 = Graphics.FromImage(bmp4);
            //Assignation des graphiques

            g.DrawLine(new Pen(Color.Gray), 100, 0, 100, 359);
            g.DrawLine(new Pen(Color.Gray), 200, 0, 200, 359);
            g.DrawLine(new Pen(Color.Gray), 0, 0, 0, 359);
            g.DrawLine(new Pen(Color.Gray), 299, 0, 299, 359);
            g.DrawLine(new Pen(Color.Gray), 0, 180, 299, 180);

            g2.DrawLine(new Pen(Color.Gray), 100, 0, 100, 359);
            g2.DrawLine(new Pen(Color.Gray), 200, 0, 200, 359);
            g2.DrawLine(new Pen(Color.Gray), 0, 0, 0, 359);
            g2.DrawLine(new Pen(Color.Gray), 299, 0, 299, 359);
            g2.DrawLine(new Pen(Color.Gray), 0, 180, 299, 180);

            g3.DrawLine(new Pen(Color.Gray), 100, 0, 100, 359);
            g3.DrawLine(new Pen(Color.Gray), 200, 0, 200, 359);
            g3.DrawLine(new Pen(Color.Gray), 0, 0, 0, 359);
            g3.DrawLine(new Pen(Color.Gray), 299, 0, 299, 359);
            g3.DrawLine(new Pen(Color.Gray), 0, 180, 299, 180);
            //Repères


            Timy = Timy+1;
            if (Timy == 300)
            {
                Timy = 1;
                g.Clear(Color.Empty);
                g2.Clear(Color.Empty);             
                g3.Clear(Color.Empty);  
            }
            //Reset

            g.DrawLine(new Pen(Color.LightSteelBlue), 0 + Timy, 180, 0 + Timy, 180 + X);
            g2.DrawLine(new Pen(Color.LightSteelBlue), 0 + Timy, 180, 0 + Timy, 180 + Y);
            g3.DrawLine(new Pen(Color.LightSteelBlue), 0 + Timy, 180, 0 + Timy, 180 + Zd);
            //Dessin des coordonées


            pictureBox1.Image = bmp;
            pictureBox2.Image = bmp2;
            pictureBox3.Image = bmp3;
            //Mise dans la picture box

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SFD1.Filter = "Text file|*.txt";
            SFD1.Title = "Save file";
            SFD1.ShowDialog();

            System.IO.File.WriteAllText(@SFD1.FileName, File);
        }
    }
}
