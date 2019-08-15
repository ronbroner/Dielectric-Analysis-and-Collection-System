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
    public partial class MainForm : Form
    {
        //Bits used to tell LCR what parameters to use
        int MR1 = 0;
        int MR2 = 0;
        int MR3 = 0;


        // MEASUREMENT PARAMETERS
        Parameter param1 = new Parameter("Cp");
        Parameter param2 = new Parameter("D");
        Parameter param3 = new Parameter("Rs");
        Parameter param4 = new Parameter("X");
        Parameter[] paramList = new Parameter[4]; // if more parameters change this
        int[] paramOrder;
      

        // AC INFORMATION
        decimal acFreq = 1000000.00M;
        decimal acVoltageVal = 0.5M;
        String acVoltageMode = "V";
        String acLimMode = "OFF";
        decimal acLimVal = 0.1M;
        String acRangeMode = "AUTO";
        String acRangeVal = "100kohm";
        String acLowZ = "OFF";
        String acTrigger = "EXTERNAL";
        String acDCBiasMode = "OFF";
        decimal acDCBiasVal = 0;
        String acSpeed = "MEDIUM";
        int acAvg = 1;
        decimal acDelay = 0.00000M;
        String acSyncMode = "OFF";
        decimal acSyncVal = 0.00100M;

        // DC INFORMATION
        decimal dcVoltage = 1;
        String dcRange = "AUTO";
        String dcLowZ = "OFF";
        String dcSpeed = "MEDIUM";
        String dcAvg = "OFF";
        String dcAdj = "ON";
        decimal dcDelay = 0.00000M;
        decimal dcAdjDelay = 0.00300M;
        decimal dcLFreq = 60;


        public MainForm()
        {
            InitializeComponent();
            //Init Measreuemnt Parameters
            button1.Text = param1.getSymbol();
            button2.Text = param2.getSymbol();
            button3.Text = param3.getSymbol();
            button4.Text = param4.getSymbol();

            //Init AC info
            textBox7.Text = acFreq.ToString("N2").Replace(",", "");
            textBox8.Text = acVoltageVal.ToString();
            textBox9.Text = acLimMode;
            textBox10.Text = acRangeMode;
            textBox11.Text = acLowZ;
            textBox12.Text = acTrigger;
            textBox13.Text = acDCBiasMode;
            textBox14.Text = acSpeed;
            textBox15.Text = acAvg.ToString();
            textBox16.Text = acDelay.ToString("N5");
            textBox17.Text = acSyncMode;


            //Init DC info
            textBox18.Text = dcVoltage.ToString();
            textBox19.Text = dcRange;
            textBox20.Text = dcLowZ;
            textBox21.Text = dcSpeed;
            textBox22.Text = dcAvg;
            textBox23.Text = dcAdj;
            textBox24.Text = dcDelay.ToString("N5");
            textBox25.Text = dcAdjDelay.ToString("N5");
            textBox26.Text = dcLFreq.ToString("N5");

            paramList[0] = param1;
            paramList[1] = param2;
            paramList[2] = param3;
            paramList[3] = param4;
            paramOrder = Parameter.getCurrentOrder(paramList);
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            updateParams();
            button1.Text = param1.getSymbol();
            button2.Text = param2.getSymbol();
            button3.Text = param3.getSymbol();
            button4.Text = param4.getSymbol();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            updateParams();
            button1.Text = param1.getSymbol();
            button2.Text = param2.getSymbol();
            button3.Text = param3.getSymbol();
            button4.Text = param4.getSymbol();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            updateParams();
            button1.Text = param1.getSymbol();
            button2.Text = param2.getSymbol();
            button3.Text = param3.getSymbol();
            button4.Text = param4.getSymbol();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            updateParams();
            button1.Text = param1.getSymbol();
            button2.Text = param2.getSymbol();
            button3.Text = param3.getSymbol();
            button4.Text = param4.getSymbol();
        }

        private void updateParams()
        {
            ParameterForm popup = new ParameterForm(param1, param2, param3, param4);
            DialogResult dialogresult = popup.ShowDialog();



            if (dialogresult == DialogResult.OK)
            {
                // Console.WriteLine("You clicked OK");
                param1 = popup.getParam1();
                param2 = popup.getParam2();
                param3 = popup.getParam3();
                param4 = popup.getParam4();
                paramList[0] = param1;
                paramList[1] = param2;
                paramList[2] = param3;
                paramList[3] = param4;
                paramOrder = Parameter.getCurrentOrder(paramList);
            }
            else if (dialogresult == DialogResult.Cancel)
            {
                // Console.WriteLine("You clicked either Cancel or X button in the top right corner");
            }
            popup.Dispose();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void File_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ACDCForm popup = new ACDCForm();

            calcMRBits();

            popup.setACPopupLeft(acFreq, acVoltageMode, acVoltageVal, acLimMode, acLimVal, acRangeMode, acRangeVal, acLowZ);
            popup.setACPopupRight(acTrigger, acDCBiasMode, acDCBiasVal, acSpeed, acAvg, acDelay, acSyncMode, acSyncVal);


            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                //Console.WriteLine("You clicked OK");
                updateAC(popup);
                updateDC(popup);
            }
            else if (dialogresult == DialogResult.Abort)
            {
               // Console.WriteLine("You clicked either Cancel or X button in the top right corner");
            }


            popup.Dispose();
        }

        public void updateAC(ACDCForm popup)
        {
            //Console.WriteLine(popup.acFreq);
            acFreq = popup.acFreq;
            acVoltageMode = popup.acVoltageMode;
            acVoltageVal = popup.acVoltageVal;
            acLimMode = popup.acLimMode;
            acLimVal = popup.acLimVal;
            acRangeMode = popup.acRangeMode;
            acRangeVal = popup.acRangeVal;
            acLowZ = popup.acLowZ;
            acTrigger = popup.acTrigger;
            acDCBiasMode = popup.acDCBiasMode;
            acDCBiasVal = popup.acDCBiasVal;
            acSpeed = popup.acSpeed;
            acAvg = popup.acAvg;
            acDelay = popup.acDelay;
            acSyncMode = popup.acSyncMode;
            acSyncVal = popup.acSyncVal;

            serialPort1.NewLine = Environment.NewLine; // Sets the line feed code to CR+LF

            serialPort1.Open();
            serialPort1.WriteLine(":LEV " + acVoltageMode);           // Signal level: Open-circuit voltage
            serialPort1.WriteLine(":LEV:VOLT " + acVoltageVal);         // Signal level: 500 mV signal level
            serialPort1.WriteLine(":FREQ " + acFreq);          // Measurement frequency: 1kHz
            serialPort1.WriteLine(":LIM " + acLimMode);          // Set ACLimit Mode
            serialPort1.WriteLine(":LIM:VOLT " + acLimVal);           // Set ACLimit Value 
            // SKIP RANGE
            serialPort1.WriteLine(":RANG:LOWZ " + acLowZ);           // Set LOWZ mode
            //TRIGGER FIXED TO EXTERNAL
            serialPort1.WriteLine(":DCB " + acDCBiasMode);          // Set (AC) DC Bias Mode
            serialPort1.WriteLine(":DCB:LEV " + acDCBiasVal);           // Set (AC) DC Bias Value 
            serialPort1.WriteLine(":SPEE " + acSpeed);          // Set Speed of measurement
            serialPort1.WriteLine(":AVER " + acAvg);          // Set Speed of measurement
            serialPort1.Close();                                                 


            //Init AC info
            textBox7.Text = acFreq.ToString("N2").Replace(",", "");
            textBox8.Text = acVoltageVal.ToString();
            if (acLimMode.Equals("OFF"))
                textBox9.Text = acLimMode;
            else if (acLimMode.Equals("ON"))
                textBox9.Text = acLimVal.ToString();
            textBox10.Text = acRangeMode;
            textBox11.Text = acLowZ;
            textBox12.Text = acTrigger;
            if (acDCBiasMode.Equals("OFF"))
                textBox13.Text = acDCBiasMode;
            else if (acDCBiasMode.Equals("ON"))
                textBox13.Text = acDCBiasVal.ToString();
            textBox14.Text = acSpeed;
            textBox15.Text = acAvg.ToString();
            textBox16.Text = acDelay.ToString("N5");
            if (acSyncMode.Equals("ON"))
                textBox17.Text = acSyncVal.ToString("N5");
            else if (acSyncMode.Equals("OFF"))
                textBox17.Text = acSyncMode;




        }

        public void updateDC(ACDCForm popup)
        {

            /*Init DC info
            textBox18.Text = dcVoltage.ToString();
            textBox19.Text = dcRange;
            textBox20.Text = dcLowZ;
            textBox21.Text = dcSpeed;
            textBox22.Text = dcAvg;
            textBox23.Text = dcAdj;
            textBox24.Text = dcDelay.ToString("N5");
            textBox25.Text = dcAdjDelay.ToString("N5");
            textBox26.Text = dcLFreq.ToString("N5");
            */
        }

        private void Trig_Click(object sender, EventArgs e)
        {
            calcMRBits();
            Trig.Enabled = false;

            try {
                serialPort1.NewLine = Environment.NewLine; // Sets the line feed code to CR+LF

                serialPort1.Open();                             // Opens serial communication port
                serialPort1.WriteLine(":BEEPer:KEY OFF");            // Turn Beep Off
                serialPort1.WriteLine(":BEEPer:JUDGment OFF");            // Turn Beep Off
                serialPort1.WriteLine(":MODE LCR");            // Mode: LCR
                serialPort1.WriteLine(":PARameter1 " + button1.Text.ToUpper() + ";:PARameter2 " + button2.Text.ToUpper() + ";:PARameter3 " + button3.Text.ToUpper() + ";:PARameter4 " + button4.Text.ToUpper() + "; ");// Displays measurement finished message
                //Console.WriteLine(MR1.ToString()+","+MR2.ToString()+","+MR3.ToString());
                serialPort1.WriteLine(":MEAS:ITEM "+ MR1.ToString()+","+ MR2.ToString() + ","+ MR3.ToString());      // Measurement Parameter: Z,Phase,Cp,Lp
                serialPort1.WriteLine(":HEAD OFF");            // Header: OFF
                serialPort1.WriteLine(":LEV "+ acVoltageMode);           // Signal level: Open-circuit voltage
                serialPort1.WriteLine(":LEV:VOLT "+ acVoltageVal);         // Signal level: 500 mV signal level
                serialPort1.WriteLine(":FREQ "+acFreq);          // Measurement frequency: 1kHz
                serialPort1.WriteLine("TRIG EXT");             // Trigger: External trigger

                serialPort1.WriteLine("*TRG;:MEAS?");
                String rd = serialPort1.ReadLine();
                string[] values = rd.Split(',');
                textBox1.Text = values[paramOrder[0]];
                textBox2.Text = values[paramOrder[1]];
                textBox3.Text = values[paramOrder[2]];
                textBox4.Text = values[paramOrder[3]];

                //Console.WriteLine(rd);

                serialPort1.Close(); // Closes serial communication port
                //Console.WriteLine("End of measurement"); // Displays measurement finished messag
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRPR");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Trig.Enabled = true;
        }



        private void calcMRBits()
        {
            MR1 = 0;
            MR2 = 0;
            MR3 = 0;
            // MR1
            if (button1.Text.Equals("Z") || button2.Text.Equals("Z") || button3.Text.Equals("Z") || button4.Text.Equals("Z"))
            {
                MR1 += 1;
            }
            if (button1.Text.Equals("Y") || button2.Text.Equals("Y") || button3.Text.Equals("Y") || button4.Text.Equals("Y"))
            {
                MR1 += 2;
            }
            if (button1.Text.Equals("PHASE") || button2.Text.Equals("PHASE") || button3.Text.Equals("PHASE") || button4.Text.Equals("PHASE"))
            {
                MR1 += 4;
            }
            if (button1.Text.Equals("Cs") || button2.Text.Equals("Cs") || button3.Text.Equals("Cs") || button4.Text.Equals("Cs"))
            {
                MR1 += 8;
            }
            if (button1.Text.Equals("Cp") || button2.Text.Equals("Cp") || button3.Text.Equals("Cp") || button4.Text.Equals("Cp"))
            {
                MR1 += 16;
            }
            if (button1.Text.Equals("D") || button2.Text.Equals("D") || button3.Text.Equals("D") || button4.Text.Equals("D"))
            {
                MR1 += 32;
            }
            if (button1.Text.Equals("Ls") || button2.Text.Equals("Ls") || button3.Text.Equals("Ls") || button4.Text.Equals("Ls"))
            {
                MR1 += 64;
            }
            if (button1.Text.Equals("Lp") || button2.Text.Equals("Lp") || button3.Text.Equals("Lp") || button4.Text.Equals("Lp"))
            {
                MR1 += 128;
            }




            // MR2
            if (button1.Text.Equals("Q") || button2.Text.Equals("Q") || button3.Text.Equals("Q") || button4.Text.Equals("Q"))
            {
                MR2 += 1;
            }
            if (button1.Text.Equals("Rs") || button2.Text.Equals("Rs") || button3.Text.Equals("Rs") || button4.Text.Equals("Rs"))
            {
                MR2 += 2;
            }
            if (button1.Text.Equals("G") || button2.Text.Equals("G") || button3.Text.Equals("G") || button4.Text.Equals("G"))
            {
                MR2 += 4;
            }
            if (button1.Text.Equals("Rp") || button2.Text.Equals("Rp") || button3.Text.Equals("Rp") || button4.Text.Equals("Rp"))
            {
                MR2 += 8;
            }
            if (button1.Text.Equals("X") || button2.Text.Equals("X") || button3.Text.Equals("X") || button4.Text.Equals("X"))
            {
                MR2 += 16;
            }
            if (button1.Text.Equals("B") || button2.Text.Equals("B") || button3.Text.Equals("B") || button4.Text.Equals("B"))
            {
                MR2 += 32;
            }
            if (button1.Text.Equals("Rdc") || button2.Text.Equals("Rds") || button3.Text.Equals("Rdc") || button4.Text.Equals("Rdc"))
            {
                MR2 += 64;
            }
            // 128 (bit 7) is unused




            // MR3
            if (button1.Text.Equals("S") || button2.Text.Equals("S") || button3.Text.Equals("S") || button4.Text.Equals("S"))
            {
                MR3 += 1;
            }
            if (button1.Text.Equals("E") || button2.Text.Equals("E") || button3.Text.Equals("E") || button4.Text.Equals("E"))
            {
                MR3 += 2;
            }
            
            // The rest (from bit 3) are unused
        }

        private void MeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AutomatedMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Automated popup = new Automated(MR1, MR2, MR3, param1, param2, param3, param4, paramOrder);
            popup.setAverage(acAvg);
            popup.FormClosed += new FormClosedEventHandler(popup_FormClosed);
            this.Visible = false;
            DialogResult dialogresult = popup.ShowDialog();
        }

        private void popup_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
