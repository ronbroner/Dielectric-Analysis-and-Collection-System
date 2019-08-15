using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCRArduino
{
    public partial class SweepSetup : Form
    {
        decimal freqStart = 1;
        decimal freqEnd = 1000;
        int freqCount = 200;
        String freqScale = "Linear";

        decimal tempStart = 1;
        decimal tempEnd = 1000;
        int tempCount = 200;
        String tempScale = "Linear";

        public SweepSetup(int fStart, int fEnd, int fCount, String fScale, int tStart, int tEnd, int tCount, String tScale)
        {
            InitializeComponent();
            comboBox1.SelectedItem = "Linear";
            comboBox2.SelectedItem = "Linear";
            freqStart = fStart;
            freqEnd = fEnd;
            freqCount = fCount;
            freqScale = fScale;
            tempStart = tStart;
            tempEnd = tEnd;
            tempCount = tCount;
            tempScale = tScale;



            textBox3.Text = freqStart.ToString();
            textBox4.Text = freqEnd.ToString();
            numericUpDown1.Value = freqCount;
            comboBox1.SelectedItem = freqScale;

            textBox5.Text = tempStart.ToString();
            textBox6.Text = tempEnd.ToString();
            numericUpDown2.Value = tempCount;
            comboBox2.SelectedItem = tempScale;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        public int getFreqStart()
        {
           // freqStart = Convert.ToInt32(textBox3.Text);
            return (int)freqStart;
        }

        public int getFreqEnd()
        {
           // freqEnd = Convert.ToInt32(textBox4.Text);
            return (int)freqEnd;
        }

        public int getFreqCount()
        {
           // freqCount = (int)numericUpDown1.Value;
            return freqCount;
        }

        public String getFreqScale()
        {
            //freqScale = comboBox1.Text;
            return freqScale;
        }

        public int getTempStart()
        {
            //tempStart = Convert.ToInt32(textBox5.Text);
            return (int)tempStart;
        }

        public int getTempEnd()
        {
           // tempEnd = Convert.ToInt32(textBox6.Text);
            return (int)tempEnd;
        }

        public int getTempCount()
        {
           // tempCount = (int)numericUpDown2.Value;
            return tempCount;
        }

        public String getTempScale()
        {
            //tempScale = comboBox2.Text;
            return tempScale;
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
             Decimal.TryParse(textBox3.Text,out freqStart);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            Decimal.TryParse(textBox4.Text, out freqEnd);
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            Decimal.TryParse(textBox5.Text, out tempStart);
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            Decimal.TryParse(textBox6.Text, out tempEnd);
        }
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            freqCount = (int)numericUpDown1.Value;
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            tempCount = (int)numericUpDown2.Value;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            freqScale = comboBox1.Text;
            if (freqScale.Equals("Log"))
            {
                numericUpDown1.Enabled = false;
            }
            else if (freqScale.Equals("Linear"))
            {
                numericUpDown1.Enabled = true;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempScale = comboBox2.Text;
            if (tempScale.Equals("Log"))
            {
                numericUpDown2.Enabled = false;
            }
            else if (tempScale.Equals("Linear"))
            {
                numericUpDown2.Enabled = true;
            }
        }
    }
}
