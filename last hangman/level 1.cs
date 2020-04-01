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
        string word = "";
        List<Label> labels = new List<Label>();
        int guess = 0;

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
         
         
        void drawbodyparts(bodyparts bp)
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
           

        }

        
       

        string getrandonword()
        {
            StreamReader a = new StreamReader("level 1.txt");
            string wordlist = a.ReadToEnd();
            string[] words = wordlist.Split('\n');
            Random ran=new Random();
            return words[ran.Next(0,words.Length-1)];
            
        }
        void addlabel()
        {
            word = getrandonword();
            char[] chars = word.ToCharArray();
            int between = 461 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                Label l = new Label();
                l.Text = "_";
                l.ForeColor = System.Drawing.Color.Red;
                l.Location = new Point(10 + i * between, groupBox1.Height - 30);
                l.Parent = groupBox1;
                l.BringToFront();
                labels.Add(l);
                l.CreateControl();              
            }

            label2.Text = "WORD LENGTH:" + (chars.Length - 1).ToString();

        }
        void reset()
        {
            Graphics g = panel1.CreateGraphics();            
            g.Clear(panel1.BackColor);
            addlabel();
            Drawhangpost();
            getrandonword();            
            label1.Text = "MISSED:";
            lblinfo.Text = "";
            textBox1.Text = "";
            start.Enabled = true;

        }


        private void menu_Click(object sender, EventArgs e)
        {
            Form1 b = new Form1();
            b.Show();
            this.Hide();
        }

        private void level_1_Load(object sender, EventArgs e)
        {
            hint.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("submit letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            char letter = textBox1.Text.ToLower().ToCharArray()[0];          
             if (!char.IsLetter(letter))
            {
                MessageBox.Show("submit letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (l.Text == "_") return;

                lblinfo.Text = "YOU WON";
                lblinfo.ForeColor = Color.Green;
                button1.Enabled = false;

            }
            else
            {
                lblinfo.Text = "WRONG";
                lblinfo.ForeColor = Color.Red;
                label1.Text += " " + letter.ToString() + ",";
                drawbodyparts((bodyparts)guess);
                guess++;



                if (guess == 9)
                {
                    lblinfo.Text = "YOU LOST";
                    lblinfo.ForeColor = Color.Red;
                    button1.Enabled = false;
                    label1.Text = "CORRECT WORD: " + word;
                }
            }
        }

        private void start_CLick(object sender, EventArgs e)
        {
            reset();           
            button1.Enabled = true;
            start.Enabled = false;
            hint.Enabled = true;
            heading.Text = "GUESS COUNTRY NAME";
        }

        private void next_Click(object sender, EventArgs e)
        {
            level_1 a = new level_1();
            a.Show();
            this.Hide();
        }

        private void hint_Click(object sender, EventArgs e)
        {
            char[] chars = word.ToCharArray();
            labels[0].Text = chars[0].ToString();
            hint.Enabled = false;
        }

       
    }
}
