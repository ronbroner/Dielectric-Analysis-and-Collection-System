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
    public partial class Standby : Form
    {

        Parameter param1, param2, param3, param4;

        int errorCount, outlierCount;

        decimal maxTemp,minTemp, recordTemp;
        bool stop = false;

        public Standby()
        {
            InitializeComponent();
            errorCount = 0;
            outlierCount = 0;
            maxTemp = 1;
            recordTemp = 0;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Standby_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {


        }

        private void TextBox2_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }



        public void setParameters(Parameter p1, Parameter p2, Parameter p3, Parameter p4)
        {
            param1 = p1;
            param2 = p2;
            param3 = p3;
            param4 = p4;
        }

        public void setCurrentTemp(decimal currenT, decimal targetT)
        {
            Object locker = new object();

            lock (locker)
            {
                textBox6.Text = currenT.ToString();
                textBox2.Text = targetT.ToString();
                if (currenT <= maxTemp)
                {
                    //if (t > recordTemp) // so you only record increasing temperature
                    //  {
                        Console.WriteLine(currenT + " - " + minTemp +" / " + maxTemp + " - " + minTemp + " ------ " + (int)(((decimal)currenT - minTemp) / ((decimal)maxTemp - minTemp) * 100));
                         //progressBar1.Value = (int)(((decimal)currenT - minTemp) / ((decimal)maxTemp - minTemp) * 100);
                        //Console.WriteLine(progressBar1.Value);
                        //    recordTemp = t;
                        // }
                }
            }
            //textBox6.Text = t.ToString();
            //progressBar1.Increment((int)((t / maxTemp) * 100));
        }


        public void setMinMaxTemp(decimal maxT, decimal minT)
        {
            maxTemp = maxT;
            minTemp = minT;
        }

        public void addPoint(decimal t,string f, decimal p1, decimal p2, decimal p3, decimal p4)
        {
            string dataPoint = "Temp: " + t + "C     Freq: "+f+"Hz     " + param1.getSymbol()+ ": "+p1 + "  "+param2.getSymbol() + ": " + p2 + "  " +param3.getSymbol() + ": " + p3 + "  " + param4.getSymbol() + ": " + p4;
            // Negatice value, print to error list
            if ((p1 < 0 && param1.getReportNeg())|| (p2 < 0 && param2.getReportNeg()) || (p3 < 0 && param3.getReportNeg()) || (p4 < 0 && param4.getReportNeg()))
            {
                textBox5.AppendText(dataPoint);
                textBox5.AppendText(Environment.NewLine);
                errorCount++;
                label2.Text = "Error/Negative Values (" +errorCount+ ")";
            }



            // At to total data point list
            textBox1.AppendText(dataPoint);
            textBox1.AppendText(Environment.NewLine);
        }
    }
}
