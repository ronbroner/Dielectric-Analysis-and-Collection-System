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
    public partial class ParameterForm : Form
    {
        Parameter param1;
        Parameter param2;
        Parameter param3;
        Parameter param4;
        public ParameterForm(Parameter p1, Parameter p2, Parameter p3, Parameter p4)
        {
            param1 = p1;
            param2 = p2;
            param3 = p3;
            param4 = p4;
            InitializeComponent();
            comboBox1.Text = param1.getSymbol();
            comboBox2.Text = param2.getSymbol();
            comboBox3.Text = param3.getSymbol();
            comboBox4.Text = param4.getSymbol();
            checkBox1.Checked = p1.getReportNeg();
            checkBox2.Checked = p2.getReportNeg();
            checkBox3.Checked = p3.getReportNeg();
            checkBox4.Checked = p4.getReportNeg();
            //  comboBox1.SelectedItem = param1;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Cancel
            //Console.WriteLine("You clicked Cancel (in popupForm)");

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Ok
            param1.changeParameter((string)(comboBox1.SelectedItem));
            param2.changeParameter((string)(comboBox2.SelectedItem));
            param3.changeParameter((string)(comboBox3.SelectedItem));
            param4.changeParameter((string)(comboBox4.SelectedItem));
            param1.setReportNeg(checkBox1.Checked);
            param2.setReportNeg(checkBox2.Checked);
            param3.setReportNeg(checkBox3.Checked);
            param4.setReportNeg(checkBox4.Checked);

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        public Parameter getParam1()
        {
            return param1;
        }

        public Parameter getParam2()
        {
            return param2;
        }

        public Parameter getParam3()
        {
            return param3;
        }

        public Parameter getParam4()
        {
            return param4;
        }

        private void ParameterForm_Load(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
