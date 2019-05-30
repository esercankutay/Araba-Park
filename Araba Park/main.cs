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

namespace Araba_Park
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (String item in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(item);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
            }
            comboBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            pictureBox1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label2.Text = "Bağlı cihaz yok";

        }
        
        
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("Bağlanacak cihaz yok!");
            }
            else
            {
                sp.PortName = comboBox1.Text;
                try
                {
                    sp.Open();
                    label2.Text = "Bağlandı";
                    comboBox1.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    pictureBox1.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show("Bağlantı kurulamadı! \nAracın veya Bluetooth bağlantısının açık olduğundan emin olun...");
                    Console.WriteLine("Bağlantı hatası : " + ex.ToString());
                    throw ex;
                }
            }
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                label2.Text = "Bağlantı kesildi";
                
                comboBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                pictureBox1.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
        }

        public void key_down(object sender, KeyEventArgs e)
        {
            if (sp.IsOpen)
            {
                if (e.KeyCode == Keys.W)
                {
                    button3.BackColor = Color.Red;
                    sp.Write("w");
                }
                if (e.KeyCode == Keys.A)
                {
                    button4.BackColor = Color.Red;
                    sp.Write("a");
                }
                if (e.KeyCode == Keys.D)
                {
                    button6.BackColor = Color.Red;
                    sp.Write("d");
                }
                if (e.KeyCode == Keys.S)
                {
                    button5.BackColor = Color.Red;
                    sp.Write("s");
                }
            }
        }
        public void key_up(object sender, KeyEventArgs e)
        {
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            button5.BackColor = SystemColors.Control;
            button6.BackColor = SystemColors.Control;

            sp.Write("z");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sp.Close();
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sp.IsOpen)
            {
                String[] mesafe = sp.ReadLine().Split(',');
                int onMesafe, arkaMesafe;
                onMesafe = Convert.ToInt32(mesafe[0]);
                arkaMesafe = Convert.ToInt32(mesafe[1]);
                if (onMesafe<7)
                {
                    label4.Invoke(new Action(() => label4.ForeColor = Color.Red));
                }
                else if (onMesafe < 12)
                {
                    label4.Invoke(new Action(() => label4.ForeColor = Color.Orange));
                }
                else
                {
                    label4.Invoke(new Action(() => label4.ForeColor = Color.Green));
                }
                if (arkaMesafe < 7)
                {
                    label3.Invoke(new Action(() => label3.ForeColor = Color.Red));
                }
                else if (arkaMesafe < 12)
                {
                    label3.Invoke(new Action(() => label3.ForeColor = Color.Orange));
                }
                else
                {
                    label3.Invoke(new Action(() => label3.ForeColor = Color.Green));
                }
                label3.Invoke(new Action(() => label3.Text = mesafe[1]));
                label4.Invoke(new Action(() => label4.Text = mesafe[0]));
                

            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
