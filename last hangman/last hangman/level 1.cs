using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace last_hangman
{
    public partial class level_1 : Form
    {
        public level_1()
        {
            InitializeComponent();
        }
        string word = null;
        List<Label> labels = new List<Label>();
        int guess = 9;

        enum bodyparts
        {
            head,
            right_eye,
            left_eye,
            mouth,
            right_arm,
            left_arm,
            body,
            right_leg,
            left_leg,
        }

        void Drawhangpost()
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Brown, 10);
            g.DrawLine(p, new Point(130, 218), new Point(130, 5));
            g.DrawLine(p, new Point(135, 5), new Point(65, 5));
            g.DrawLine(p, new Point(60, 0), new Point(60, 50));
        }
        void varybody()
        {
            drawbodyparts(bodyparts.head);
            drawbodyparts(bodyparts.left_eye);
            drawbodyparts(bodyparts.right_eye);
            drawbodyparts(bodyparts.mouth);
            drawbodyparts(bodyparts.body);
            drawbodyparts(bodyparts.left_arm);
            drawbodyparts(bodyparts.right_arm);
            drawbodyparts(bodyparts.left_leg);
            drawbodyparts(bodyparts.right_leg);
        }
        void drawbodyparts(int guess)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.White, 2);
            if (bp == bodyparts.head)
                g.DrawEllipse(p, 40, 50, 40, 40);
            else if (bp == bodyparts.left_eye)
            {
                SolidBrush c = new SolidBrush(Color.White);
                g.FillEllipse(c, 50, 60, 5, 5);
            }
            else if (bp == bodyparts.right_eye)
            {
                SolidBrush c = new SolidBrush(Color.White);
                g.FillEllipse(c, 63, 60, 5, 5);
            }
            else if (bp == bodyparts.mouth)
                g.DrawArc(p, 50, 60, 20, 20, 45, 90);
            else if ((bp == bodyparts.body))
                g.DrawLine(p, new Point(60, 90), new Point(60, 170));
            else if ((bp == bodyparts.left_arm))
                g.DrawLine(p, new Point(60, 100), new Point(30, 85));
            else if ((bp == bodyparts.right_arm))
                g.DrawLine(p, new Point(60, 100), new Point(90, 85));
            else if ((bp == bodyparts.left_leg))
                g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            else if ((bp == bodyparts.right_leg))
                g.DrawLine(p, new Point(60, 170), new Point(30, 190));

            if (guess == 7)
            {
                pictureBox1.Show();
            }
            else if (guess == 6)
            {
                pictureBox1.Hide();
                pictureBox2.Show();
            }
            else if (guess == 5)
            {
                pictureBox2.Hide();
                pictureBox3.Show();
            }
            else if (guess == 4)
            {
                pictureBox3.Hide();
                pictureBox4.Show();
            }
            else if (guess == 3)
            {
                pictureBox4.Hide();
                pictureBox5.Show();
            }
            else if (guess == 2)
            {
                pictureBox5.Hide();
                pictureBox6.Show();
            }
            else if (guess == 1)
            {
                pictureBox6.Hide();
                pictureBox7.Show();
            }
            else if (guess == 0)
            {
                pictureBox7.Hide();
                pictureBox8.Show();
            }
        }

        void addlabel()
        {
            word = getrandonword();
            char[] chars = word.ToCharArray();
            int between = 461/chars.Length-1;
            for (int i = 0; i < chars.Length-1; i++)
            {
                Label l = new Label();
                l.Text = "_";
                l.ForeColor = System.Drawing.Color.Red;
                l.Location = new Point(10 + i * between, groupBox2.Height - 30);
                l.Parent = groupBox2;
                l.BringToFront();
                labels.Add(l);
                l.CreateControl();
            }

            label2.Text = "WORD LENGTH:" + (chars.Length - 1).ToString();
           
        }

        void reset()
        {
           // Graphics g = panel1.CreateGraphics();
            guess = 9;
           // g.Clear(panel1.BackColor);
            addlabel();
            getrandonword();
            //Drawhangpost();
            label1.Text = "GUESS LEFT:";
            textBox1.Text = null;

        }

        string getrandonword()
        {
            StreamReader a = new StreamReader("level 1.txt");
            string wordlist = a.ReadToEnd();
            string[] words = wordlist.Split('\n');
            Random ran=new Random();
            return words[ran.Next(0,words.Length-1)];
            
        }


        private void menu_Click(object sender, EventArgs e)
        {
            Form1 b = new Form1();
            b.Show();
            this.Hide();
        }

        private void level_1_Load(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();
            pictureBox5.Hide();
            pictureBox6.Hide();
            pictureBox7.Hide();
            pictureBox8.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if (!char.IsLetter(letter))
            {
                MessageBox.Show("submit letter","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letter)
                    {
                        labels[i].Text = letter.ToString();
                        lblinfo.Text = "AWESOME";
                        lblinfo.ForeColor = Color.Green;
                    }
                }
                foreach (Label l in labels)              
                    if(l.Text=="_")return;
                lblinfo.Text = "YOU WON";
                lblinfo.ForeColor = Color.Green;
                
            }
            else
            {               
                lblinfo.Text = "WRONG";
                lblinfo.ForeColor = Color.Red;
                guess--;
                drawbodyparts(guess);
               

                label1.Text = "GUESS LEFT: " + guess;
                

                if (guess==0)
                {
                    lblinfo.Text = "YOU LOST";
                    lblinfo.ForeColor=Color.Red;
                    button1.Enabled = false;
                }
            }
        }

        private void start_CLick(object sender, EventArgs e)
        {
            reset();
            button1.Enabled = true;
            start.Enabled = false;
        }

        private void next_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }
}
