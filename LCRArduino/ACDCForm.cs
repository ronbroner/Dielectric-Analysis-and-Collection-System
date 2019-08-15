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
    public partial class ACDCForm : Form
    {


        // Made these all public to save myself from a thousand getters
        public decimal acFreq = 0;
        public String acVoltageMode = "";
        public decimal acVoltageVal = 0;
        public String acLimMode = "";
        public decimal acLimVal = 0;
        public String acRangeMode = "";
        public String acRangeVal = "";
        public String acLowZ = "";
        public String acTrigger = "";
        public String acDCBiasMode = "";
        public decimal acDCBiasVal = 0;
        public String acSpeed = "";
        public int acAvg = 0;
        public decimal acDelay = 0;
        public String acSyncMode = "";
        public decimal acSyncVal = 0;


        public ACDCForm()
        {
            InitializeComponent();
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox5.Text,out acLimVal);

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            acLimMode = (string)comboBox2.SelectedItem;

            if (comboBox2.SelectedItem.Equals("OFF"))
            {
                textBox5.Enabled = false;
            }

            if (comboBox2.SelectedItem.Equals("ON"))
            {
                textBox5.Enabled = true;
            }

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            acRangeMode = (string)comboBox3.SelectedItem;

            if (comboBox3.SelectedItem.Equals("AUTO"))
            {
                comboBox4.Enabled = false;
            }

            if (comboBox3.SelectedItem.Equals("HOLD"))
            {
                comboBox4.Enabled = true;
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            acRangeVal = (string)comboBox4.SelectedItem;
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            acDCBiasMode = (string)comboBox7.SelectedItem;

            if (comboBox7.SelectedItem.Equals("OFF"))
            {
                textBox6.Enabled = false;
            }

            if (comboBox7.SelectedItem.Equals("ON"))
            {
                textBox6.Enabled = true;
            }
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox6.Text, out acDCBiasVal);
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            acSyncMode = (string)comboBox9.SelectedItem;

            if (comboBox9.SelectedItem.Equals("OFF"))
            {
                textBox8.Enabled = false;
            }

            if (comboBox9.SelectedItem.Equals("ON"))
            {
                textBox8.Enabled = true;
            }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox8.Text, out acSyncVal);
        }

        internal void setACPopupLeft(decimal acFreq, string acVoltageMode, decimal acVoltageVal, string acLimMode, decimal acLimVal, string acRangeMode, string acRangeVal, string acLowZ)
        {
   
            this.acFreq = acFreq;
            this.acVoltageMode = acVoltageMode;
            this.acVoltageVal = acVoltageVal;
            this.acLimMode = acLimMode;
            this.acLimVal = acLimVal;
            this.acRangeMode = acRangeMode;
            this.acRangeVal = acRangeVal;
            this.acLowZ = acLowZ;

            textBox3.Text = acFreq.ToString() ;
            textBox4.Text = acVoltageVal.ToString();
            textBox5.Text = acLimVal.ToString();
            comboBox1.SelectedItem = acVoltageMode;
            comboBox2.SelectedItem = acLimMode;
            comboBox3.SelectedItem = acRangeMode;
            comboBox4.SelectedItem = acRangeVal;
            comboBox5.SelectedItem = acLowZ;

        }

        internal void setACPopupRight(string acTrigger, string acDCBiasMode, decimal acDCBiasVal, string acSpeed, int acAvg, decimal acDelay, string acSyncMode, decimal acSyncVal)
        {

            this.acTrigger = acTrigger;
            this.acDCBiasMode = acDCBiasMode;
            this.acDCBiasVal = acDCBiasVal;
            this.acSpeed = acSpeed;
            this.acAvg = acAvg;
            this.acDelay = acDelay;
            this.acSyncMode = acSyncMode;
            this.acSyncVal = acSyncVal;


            comboBox6.SelectedItem = acTrigger;
            comboBox7.SelectedItem = acDCBiasMode;
            comboBox8.SelectedItem = acSpeed;
            comboBox9.SelectedItem = acSyncMode;
            textBox6.Text = acDCBiasVal.ToString();
            textBox7.Text = acDelay.ToString();
            textBox8.Text = acSyncVal.ToString();
            numericUpDown1.Value = acAvg;




        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox3.Text,out acFreq);
        }

        private void ACDCForm_Load(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            acVoltageMode = (string)comboBox1.SelectedItem;
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox4.Text, out acVoltageVal);
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            acLowZ = (string)comboBox5.SelectedItem;
        }

        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            acSpeed = (string)comboBox8.SelectedItem;
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {
            decimal.TryParse(textBox7.Text, out acDelay);
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            acAvg = (int)numericUpDown1.Value;
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            acTrigger = comboBox6.Text;
        }
    }
}
