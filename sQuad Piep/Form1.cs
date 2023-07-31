namespace sQuad_Piep
{
    public partial class Form1 : Form
    {

        int duration = 125;
        public Form1()
        {
            InitializeComponent();
            preselectToene();

            //radioButton_type_x35.Checked = true;
            //comboBox_led_1.Text = "R (Rot)";
            //comboBox_led_2.Text = "G (Grün)";
            //comboBox_led_3.Text = "1 (Weiß)";
            //comboBox_led_4.Text = "0 (Aus)";
            //comboBox_led_5.Text = "Y (Gelb)";
            //comboBox_led_6.Text = "B (Blau)";
            //comboBox_led_7.Text = "M (Pink)";
            //comboBox_led_8.Text = "C (Cyan)";
            //textBox_tonmuster.Text = "ABC_ABC_";
        }


        private void button_Play_Click(object sender, EventArgs e)
        {
            if (!radioButton_type_x15.Checked & !radioButton_type_x35.Checked)
            {
                MessageBox.Show("Kein Modell gewählt", "Modell Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox_led_1.Text == "" || comboBox_led_2.Text == "" || comboBox_led_3.Text == "" || comboBox_led_4.Text == "" || comboBox_led_5.Text == "" || comboBox_led_6.Text == "" || comboBox_led_7.Text == "" || comboBox_led_8.Text == "")
            {
                MessageBox.Show("Mindestens eine LED-Farbe wurde nicht ausgewählt", "LED Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numericUpDown_dauer.Value == 0)
            {
                MessageBox.Show("Es kann kein Ton mit einer Dauer von 0 abgespielt werden", "Dauer Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox_tonmuster.Text.Length != 8)
            {
                MessageBox.Show("Es wurde kein gültiges Tonmuster eingegeben", "Tonmuster Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ton_A = Convert.ToInt32(comboBox_ton_A.Text.Split(' ')[0]);
            int ton_B = Convert.ToInt32(comboBox_ton_B.Text.Split(' ')[0]);
            int ton_C = Convert.ToInt32(comboBox_ton_C.Text.Split(' ')[0]);
            int ton_D = Convert.ToInt32(comboBox_ton_D.Text.Split(' ')[0]);
            int ton_E = Convert.ToInt32(comboBox_ton_E.Text.Split(' ')[0]);
            int ton_F = Convert.ToInt32(comboBox_ton_F.Text.Split(' ')[0]);
            int ton_G = Convert.ToInt32(comboBox_ton_G.Text.Split(' ')[0]);
            int ton_H = Convert.ToInt32(comboBox_ton_H.Text.Split(' ')[0]);

            char led_1 = Convert.ToChar(comboBox_led_1.Text.Split(' ')[0]);
            char led_2 = Convert.ToChar(comboBox_led_2.Text.Split(' ')[0]);
            char led_3 = Convert.ToChar(comboBox_led_3.Text.Split(' ')[0]);
            char led_4 = Convert.ToChar(comboBox_led_4.Text.Split(' ')[0]);
            char led_5 = Convert.ToChar(comboBox_led_5.Text.Split(' ')[0]);
            char led_6 = Convert.ToChar(comboBox_led_6.Text.Split(' ')[0]);
            char led_7 = Convert.ToChar(comboBox_led_7.Text.Split(' ')[0]);
            char led_8 = Convert.ToChar(comboBox_led_8.Text.Split(' ')[0]);


            Task task_sound = Task.Factory.StartNew(() => playSound(ton_A, ton_B, ton_C, ton_D, ton_E, ton_F, ton_G, ton_H));
            Task task_led = Task.Factory.StartNew(() => playLED(led_1, led_2, led_3, led_4, led_5, led_6, led_7, led_8));
            Task task_button = Task.Factory.StartNew(() => DisablePlayButton());

        }

        private void DisablePlayButton()
        {
            button_play.Invoke(new Action(() => button_play.Enabled = false));
            Thread.Sleep(Convert.ToInt32(numericUpDown_dauer.Value) * 8 * duration);
            button_play.Invoke(new Action(() => button_play.Enabled = true));
        }


        private void playSound(int ton_A, int ton_B, int ton_C, int ton_D, int ton_E, int ton_F, int ton_G, int ton_H)
        {
            for (int t = 0; t < numericUpDown_dauer.Value; t++)
            {
                foreach (char chr in textBox_tonmuster.Text.ToUpper())
                {
                    switch (chr)
                    {
                        case 'A':
                            Console.Beep(ton_A, duration);
                            break;
                        case 'B':
                            Console.Beep(ton_B, duration);
                            break;
                        case 'C':
                            Console.Beep(ton_C, duration);
                            break;
                        case 'D':
                            Console.Beep(ton_D, duration);
                            break;
                        case 'E':
                            Console.Beep(ton_E, duration);
                            break;
                        case 'F':
                            Console.Beep(ton_F, duration);
                            break;
                        case 'G':
                            Console.Beep(ton_G, duration);
                            break;
                        case 'H':
                            Console.Beep(ton_H, duration);
                            break;
                        case '_':
                        default:
                            Thread.Sleep(duration);
                            break;
                    }
                }
            }
        }

        private void playLED(char led_1, char led_2, char led_3, char led_4, char led_5, char led_6, char led_7, char led_8)
        {
            for (int t = 0; t < numericUpDown_dauer.Value; t++)
            {
                for (int i = 1; i <= 8; i++)
                {
                    switch (i)
                    {
                        case 1:
                            changeLEDColor(led_1);
                            break;
                        case 2:
                            changeLEDColor(led_2);
                            break;
                        case 3:
                            changeLEDColor(led_3);
                            break;
                        case 4:
                            changeLEDColor(led_4);
                            break;
                        case 5:
                            changeLEDColor(led_5);
                            break;
                        case 6:
                            changeLEDColor(led_6);
                            break;
                        case 7:
                            changeLEDColor(led_7);
                            break;
                        case 8:
                            changeLEDColor(led_8);
                            break;
                    }
                    Thread.Sleep(duration);
                }
            }
            changeLEDColor('0');
        }

        private void changeLEDColor(char color)
        {
            switch (color)
            {
                case '0':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = SystemColors.Control));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = ));                    
                    break;
                case '1':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.White));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.White));
                    break;
                case 'R':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Red));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Red));
                    break;
                case 'G':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Green));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Green));
                    break;
                case 'Y':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Yellow));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Yellow));
                    break;
                case 'B':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Blue));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Blue));
                    break;
                case 'M':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Magenta));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Magenta));
                    break;
                case 'C':
                    button_LED.Invoke(new Action(() => button_LED.BackColor = Color.Cyan));
                    //button_LED.BeginInvoke(new Action(() => button_LED.BackColor = Color.Cyan));
                    break;
            }
        }


        private void radioButton_type_CheckedChanged(object sender, EventArgs e)
        {
            object[] farbwerteX15 = new object[2] { "0 (Aus)", "1 (Weiß)" };
            object[] farbwerteX35 = new object[8] { "0 (Aus)", "1 (Weiß)", "R (Rot)", "G (Grün)", "Y (Gelb)", "B (Blau)", "M (Pink)", "C (Cyan)" };

            comboBox_led_1.Text = "";
            comboBox_led_2.Text = "";
            comboBox_led_3.Text = "";
            comboBox_led_4.Text = "";
            comboBox_led_5.Text = "";
            comboBox_led_6.Text = "";
            comboBox_led_7.Text = "";
            comboBox_led_8.Text = "";

            comboBox_led_1.Items.Clear();
            comboBox_led_2.Items.Clear();
            comboBox_led_3.Items.Clear();
            comboBox_led_4.Items.Clear();
            comboBox_led_5.Items.Clear();
            comboBox_led_6.Items.Clear();
            comboBox_led_7.Items.Clear();
            comboBox_led_8.Items.Clear();

            if (((RadioButton)sender).Text == "x15")
            {
                comboBox_led_1.Items.AddRange(farbwerteX15);
                comboBox_led_2.Items.AddRange(farbwerteX15);
                comboBox_led_3.Items.AddRange(farbwerteX15);
                comboBox_led_4.Items.AddRange(farbwerteX15);
                comboBox_led_5.Items.AddRange(farbwerteX15);
                comboBox_led_6.Items.AddRange(farbwerteX15);
                comboBox_led_7.Items.AddRange(farbwerteX15);
                comboBox_led_8.Items.AddRange(farbwerteX15);
            }
            else if (((RadioButton)sender).Text == "x35")
            {
                comboBox_led_1.Items.AddRange(farbwerteX35);
                comboBox_led_2.Items.AddRange(farbwerteX35);
                comboBox_led_3.Items.AddRange(farbwerteX35);
                comboBox_led_4.Items.AddRange(farbwerteX35);
                comboBox_led_5.Items.AddRange(farbwerteX35);
                comboBox_led_6.Items.AddRange(farbwerteX35);
                comboBox_led_7.Items.AddRange(farbwerteX35);
                comboBox_led_8.Items.AddRange(farbwerteX35);
            }

        }

        private void preselectToene()
        {
            object[] toene = new object[60] { "155 Hz (dis)", "164 Hz (e)", "174 Hz (f)", "185 Hz (fis)", "196 Hz (g)", "207 Hz (gis)", "220 Hz (a)", "233 Hz (ais)", "246 Hz (h)", "261 Hz (c1)", "277 Hz (cis1)", "293 Hz (d1)", "311 Hz (dis1)", "329 Hz (e1)", "349 Hz (f1)", "370 Hz (fis1)", "392 Hz (g1)", "415 Hz (gis1)", "440 Hz (a1)", "466 Hz (ais1)", "493 Hz (h1)", "523 Hz (c2)", "544 Hz (cis2)", "587 Hz (d2)", "622 Hz (dis2)", "659 Hz (e2)", "698 Hz (f2)", "740 Hz (fis2)", "784 Hz (g2)", "830 Hz (gis2)", "880 Hz (a2)", "923 Hz  (ais2)", "987 Hz (h2)", "1046 Hz (c3)", "1108 Hz (cis3)", "1174 Hz (d3)", "1244 Hz (dis3)", "1318 Hz (e3)", "1397 Hz (f3)", "1480 Hz (fis3)", "1568 Hz (g3)", "1661 Hz (gis3)", "1760 Hz (a3)", "1864 Hz (ais3)", "1975 Hz (h3)", "2093 Hz (c4)", "2217 Hz (cis4)", "2349 Hz (d4)", "2489 Hz (dis4)", "2637 Hz (e4)", "2793 Hz (f4)", "2960 Hz (fis4)", "3136 Hz (g4)", "3322 Hz (gis4)", "3520 Hz (a4)", "3729 Hz (ais4)", "3951 Hz (h4)", "1004 Hz", "2731 Hz", "3200 Hz" };
            comboBox_ton_A.Items.AddRange(toene);
            comboBox_ton_B.Items.AddRange(toene);
            comboBox_ton_C.Items.AddRange(toene);
            comboBox_ton_D.Items.AddRange(toene);
            comboBox_ton_E.Items.AddRange(toene);
            comboBox_ton_F.Items.AddRange(toene);
            comboBox_ton_G.Items.AddRange(toene);
            comboBox_ton_H.Items.AddRange(toene);

            comboBox_ton_A.SelectedItem = "1004 Hz";
            comboBox_ton_B.SelectedItem = "2731 Hz";
            comboBox_ton_C.SelectedItem = "3200 Hz";
            comboBox_ton_D.SelectedItem = "880 Hz (a2)";
            comboBox_ton_E.SelectedItem = "987 Hz (h2)";
            comboBox_ton_F.SelectedItem = "1046 Hz (c3)";
            comboBox_ton_G.SelectedItem = "1174 Hz (d3)";
            comboBox_ton_H.SelectedItem = "1318 Hz (e3)";
        }

        string tonmuster_oldText = string.Empty;
        private void textBox_tonmuster_TextChanged(object sender, EventArgs e)
        {
            char[] validTonMuster = new char[17] { '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            if (textBox_tonmuster.Text.All(chr => validTonMuster.Contains(chr) & (char.IsLetter(chr) || chr == '_')))
            {
                tonmuster_oldText = textBox_tonmuster.Text.ToUpper();
                textBox_tonmuster.Text = tonmuster_oldText;
                textBox_tonmuster.BackColor = Color.White;
                textBox_tonmuster.ForeColor = Color.Black;
            }
            else
            {
                textBox_tonmuster.Text = tonmuster_oldText;
                textBox_tonmuster.BackColor = Color.Red;
                textBox_tonmuster.ForeColor = Color.White;
            }
            textBox_tonmuster.SelectionStart = textBox_tonmuster.Text.Length;
        }
    }
}