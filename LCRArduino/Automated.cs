
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace LCRArduino
{
    public partial class Automated : Form
    {

        int MR1, MR2, MR3;
        Parameter param1, param2, param3, param4;
        int[] paramOrder;

        int freqStart = 10;
        int freqEnd = 1000000;
        int freqCount = 6;
        String freqScale = "Log";

        int tempStart = 20;
        int tempEnd = 30;
        int tempCount = 5;
        String tempScale = "Linear";

        List<double> freqVals = new List<double>();
        List<double> tempVals = new List<double>();

        decimal freqDelay, tempDelay, freqNum, freqAvg, bulkDelay, tempError;

        string ext;
        bool singleFile = false;

        Standby StandbyPopup = new Standby();

        public Automated(int M1, int M2, int M3, Parameter p1, Parameter p2, Parameter p3, Parameter p4, int[] paramOrder)
        {
            InitializeComponent();
            MR1 = M1;
            MR2 = M2;
            MR3 = M3;
            param1 = p1;
            param2 = p2;
            param3 = p3;
            param4 = p4;
            this.paramOrder = paramOrder;
            StandbyPopup.setParameters(param1, param2, param3, param4);

            freqDelay = numericUpDown1.Value;
            freqNum = numericUpDown2.Value;
            bulkDelay = numericUpDown3.Value;
            freqAvg = numericUpDown4.Value;
            tempDelay = numericUpDown5.Value;
            tempError = numericUpDown6.Value;

            ext = ".txt";
        }

        public void setAverage(int val)
        {
            freqAvg = val;
            numericUpDown4.Value = val;
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            //StandbyPopup = new Standby();

            //  lock (locker)
            // {
            //StandbyPopup.Visible = true;
                //button3.Enabled = false;
               // StandbyPopup.FormClosed += new FormClosedEventHandler(StandbyPopup_FormClosed);

                DialogResult res = StandbyPopup.ShowDialog();

                if (res == DialogResult.Cancel)
                {
                    button3.Enabled = true;
                    StandbyPopup.Visible = false;
                }
                else if (res == DialogResult.Abort)
                {
                    // Console.WriteLine("ABORT");
                    if (serialPort1.IsOpen)
                        serialPort1.Close();
                    if (serialPort2.IsOpen)
                    serialPort2.Close();
                    System.Windows.Forms.Application.Exit();
                }

            // }*/

        }

        private void StandbyPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            button3.Enabled = true;
            StandbyPopup = new Standby();
        }




        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            freqDelay = numericUpDown1.Value;
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            freqNum = numericUpDown2.Value;
        }

        private void NumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            bulkDelay = numericUpDown3.Value;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            //  Pick Destination folder

            folderBrowserDialog1.ShowDialog();
            textBox8.Text = folderBrowserDialog1.SelectedPath;

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void FolderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Automated_Load(object sender, EventArgs e)
        {

        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            freqAvg = numericUpDown4.Value;
        }

        private void NumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            tempDelay = numericUpDown5.Value;
        }


        private void NumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            tempError = numericUpDown6.Value;
        }


        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ext = ".txt";
            singleFile = true;
        }


        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ext = ".txt";
            singleFile = false;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void Button2_Click(object sender, EventArgs e)
        {
            SweepSetup popup = new SweepSetup(freqStart, freqEnd, freqCount, freqScale, tempStart, tempEnd, tempCount, tempScale);
            DialogResult dialogresult = popup.ShowDialog();

            
            
            if (dialogresult == DialogResult.OK)
            {
                
                freqStart = popup.getFreqStart();
                freqEnd = popup.getFreqEnd();
                freqCount = popup.getFreqCount();
                freqScale = popup.getFreqScale();

                tempStart = popup.getTempStart();
                tempEnd = popup.getTempEnd();
                tempCount = popup.getTempCount();
                tempScale = popup.getTempScale();
                
                // Ensure Values are withing allowed limits
                while (dialogresult == DialogResult.OK && (freqStart <4 || freqEnd > 8000000 || tempStart < 0 || tempEnd > 1100))
                {
                    freqStart = popup.getFreqStart();
                    freqEnd = popup.getFreqEnd();
                    freqCount = popup.getFreqCount();
                    freqScale = popup.getFreqScale();

                    tempStart = popup.getTempStart();
                    tempEnd = popup.getTempEnd();
                    tempCount = popup.getTempCount();
                    tempScale = popup.getTempScale();


                    Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);
                    //popup.Visible = true;

                    SweepError error = new SweepError();
                    DialogResult errorResult = error.ShowDialog();

                    if (errorResult == DialogResult.OK)
                    {
                        Console.WriteLine("You clicked OK AGAIN");
                        error.Dispose();
                        popup = new SweepSetup(freqStart, freqEnd, freqCount, freqScale, tempStart, tempEnd, tempCount, tempScale);
                        dialogresult = popup.ShowDialog();
                    }
                    
                }

                //Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);

                // Populate Frequency Values
                textBox2.Text = "";
                freqVals.Clear();
                if ((popup.getFreqScale()).Equals("Linear"))
                {
                    double x = (freqEnd - freqStart) / ((double)freqCount - 1);
                    for (double i = (double)freqStart; i <= freqEnd; i = i + x)
                    {
                        //Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);
                        freqVals.Add(Math.Round(i, 3));
                        textBox2.AppendText(Math.Round(i,3).ToString().Replace(",", ""));
                        textBox2.AppendText(Environment.NewLine);
                    }
                }
                else if ((popup.getFreqScale()).Equals("Log"))
                {
                    for (double i = (double)freqStart; i <= freqEnd; i = i*10)
                    {
                        //Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);
                        freqVals.Add(Math.Round(i, 3));
                        textBox2.AppendText(Math.Round(i,3).ToString().Replace(",", ""));
                        textBox2.AppendText(Environment.NewLine);
                    }
                }
                else
                {
                    Console.WriteLine("Frequency Scale Error");
                }


                // Populate Temperature Values
                textBox3.Text = "";
                tempVals.Clear();

                if ((popup.getTempScale()).Equals("Linear"))
                {
                    double y = (tempEnd - tempStart) / ((double)tempCount - 1);
                    for (double j = (double)tempStart; j <= tempEnd; j = j + y)
                    {
                        //Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);
                        tempVals.Add(Math.Round(j,3));
                        textBox3.AppendText(Math.Round(j,3).ToString().Replace(",", ""));
                        textBox3.AppendText(Environment.NewLine);
                    }
                }
                else if ((popup.getTempScale()).Equals("Log"))
                {
                    for (double i = (double)tempStart; i <= tempEnd; i = i * 10)
                    {
                        //Console.WriteLine("freqStart:" + freqStart + "  freqEnd:" + freqEnd + "  tempStart:" + tempStart + "  tempEnd:" + tempEnd);
                        tempVals.Add(Math.Round(i, 3));
                        textBox3.AppendText(Math.Round(i,3).ToString().Replace(",", ""));
                        textBox3.AppendText(Environment.NewLine);
                    }
                }
                else
                {
                    Console.WriteLine("Temperature Scale Error");
                }

                /*for (int a =0; a<freqVals.Count; a++)
                {
                    Console.WriteLine(freqVals[a]);
                }

                for (int a = 0; a < tempVals.Count; a++)
                {
                    Console.WriteLine(tempVals[a]);
                }*/



                popup.Dispose();

            }
            else if (dialogresult == DialogResult.Cancel)
            {
               // popup.Visible = true;
               // Console.WriteLine("You clicked either Cancel or X button in the top right corner");
                popup.Dispose();
            }


           
        }


        private void Button1_Click(object sender, EventArgs e)
        {
           if (!System.IO.Directory.Exists(textBox8.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SweepError error = new SweepError();
                DialogResult errorResult = error.ShowDialog();

                if (errorResult == DialogResult.OK)
                {
                    Console.WriteLine("You clicked OK AGAIN");
                    error.Dispose();
                }
            }
           else
            {
                button1.Enabled = false;
                button3.Enabled = false;
                //StandbyPopup.Visible = true;
                ThreadPool.QueueUserWorkItem(state => initCycle(StandbyPopup, singleFile));

                DialogResult res = StandbyPopup.ShowDialog();

                if (res == DialogResult.Cancel)
                {
                    StandbyPopup.Visible = false;
                    button3.Enabled = true;
                }
                else if (res == DialogResult.Abort)
                {
                    // Console.WriteLine("ABORT");
                    if (serialPort1.IsOpen)
                        serialPort1.Close();
                    if (serialPort2.IsOpen)
                        serialPort2.Close();
                    System.Windows.Forms.Application.Exit();
                }

            }
            //System.Threading.Tasks.Task.Factory.StartNew(initCycle(StandbyPopup));
            // StandbyPopup.Visible = true;



        }



        public void initCycle(Standby sPop, bool oneFile)
        {
            try
            {
                File.Delete(folderBrowserDialog1.SelectedPath + "\\" + "Data" + ext); // Delete single results file (if previosuly done, because append is on)

                // BEGIN LCR SETUP
                serialPort1.NewLine = Environment.NewLine; // Sets the line feed code to CR+LF

                serialPort1.Open();                             // Opens serial communication port
                serialPort1.WriteLine(":BEEPer:KEY OFF");            // Turn Beep Off
                serialPort1.WriteLine(":BEEPer:JUDGment OFF");            // Turn Beep Off
                serialPort1.WriteLine(":MODE LCR");            // Mode: LCR
                serialPort1.WriteLine(":PARameter1 " + param1.getSymbol().ToUpper() + ";:PARameter2 " + param2.getSymbol().ToUpper() + ";:PARameter3 " + param3.getSymbol().ToUpper() + ";:PARameter4 " + param4.getSymbol().ToUpper() + "; ");// Displays measurement finished message
                Console.WriteLine(MR1.ToString() + "," + MR2.ToString() + "," + MR3.ToString());
                serialPort1.WriteLine(":MEAS:ITEM " + MR1.ToString() + "," + MR2.ToString() + "," + MR3.ToString());      //Define measurement parameters
                serialPort1.WriteLine(":HEAD OFF");            // Header: OFF
                serialPort1.WriteLine(":LEV V");           // Signal level: Open-circuit voltage
                //serialPort1.WriteLine(":LEV:VOLT 0.5");         // Signal level: 500 mV signal level
                //serialPort1.WriteLine(":FREQ 1E3");          // Measurement frequency: 1kHz
                serialPort1.WriteLine("TRIG EXT");             // Trigger: External trigger
                                                               // END LCR SETUP

                // BEGIN ARDUINO SETUP

                serialPort2.BaudRate = 9600;
                serialPort2.PortName = "COM4";
                serialPort2.Open();
                //serialPort2.WriteLine("O");
                //string a = serialPort2.ReadExisting();
                //Console.WriteLine(a);
                //serialPort2.Close();

                // END ARDUINO SETUP


                // BEGIN TEMPERATURE SWEEP

                decimal furnaceTemp = 0;
                string rd;
                decimal[] vals = new decimal[5];
                string[] correctOutput = new string[4]; // CHANGE IF YOU WANT MORE THAN 4 PARAMETERS

                this.BeginInvoke((MethodInvoker)delegate { sPop.setMinMaxTemp((decimal)tempVals.ElementAt<double>(tempVals.Count - 1), (decimal)tempVals.ElementAt<double>(0)); });
                //StandbyPopup.setMaxTemp((decimal)tempVals.ElementAt<double>(tempVals.Count-1));

                // These are to clear arduino reading
                serialPort2.ReadLine();
                serialPort2.ReadLine();


                while (tempVals.Count>0 && furnaceTemp <= tempEnd)
                {
                    //Console.WriteLine("1");
                    // ****update temp using arduino data****
                    decimal.TryParse(serialPort2.ReadLine(), out furnaceTemp);
                    //furnaceTemp = furnaceTemp;

                    this.BeginInvoke((MethodInvoker)delegate { sPop.setCurrentTemp(furnaceTemp, (decimal)tempVals[0]); });
                   // this.BeginInvoke((MethodInvoker)delegate { stopRun = sPop.getStopCommand(); });
                     //StandbyPopup.setCurrentTemp(furnaceTemp);

                     Console.WriteLine("temp: "+furnaceTemp);
                    // Console.WriteLine("2 -- " + ((decimal)Math.Abs((decimal)tempVals[0] - furnaceTemp)).ToString() + " < " + tempError.ToString());

                    // *** if you are within some error value of desired temperature start a sweep***
                    if ((decimal)Math.Abs((decimal)tempVals[0]-furnaceTemp) < tempError) 
                    {
                        // Console.WriteLine("2.5");
                        StreamWriter fp;
                        if (!oneFile) // not one file, put every temp in its own folder
                        {
                            fp = new System.IO.StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + tempVals[0].ToString() + ext, false, System.Text.Encoding.GetEncoding("shift_jis")); // File open
                        }
                        else // append to same file every time
                        {
                            fp = new System.IO.StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + "Data" + ext, true, System.Text.Encoding.GetEncoding("shift_jis")); // File open
                            fp.Write("Temperature: " + tempVals[0] + Environment.NewLine); // Write the temp for reference on single sheet
                        }

                        fp.Write("Frequency(Hz)" + "," + param1.getSymbol() + "," + param2.getSymbol() + "," + param3.getSymbol() + "," + param4.getSymbol() + Environment.NewLine);                // Outputs the header to file
                        
                        /*
                         *         TODO: freqAvg
                         * 
                         */

                        if (tempDelay>0) // delay after particular temperature reached
                            System.Threading.Thread.Sleep((int)(tempDelay * 1000)); // s

                       // Console.WriteLine("3");
                        for (int s = 0; s < freqNum; s++) // repeat measurements
                        {
                           // Console.WriteLine("4");
                            if (bulkDelay > 0) // delay between mearuements
                                System.Threading.Thread.Sleep((int)(bulkDelay * 1000));

                            for (int r = 0; r < freqVals.Count; r++)
                            {
                               // Console.WriteLine("5");
                                if (freqDelay > 0) // user specified sleep between each frequency measurement
                                    System.Threading.Thread.Sleep((int)(freqDelay * 1000)); // s
                                //Console.WriteLine(freqVals[r].ToString());
                                serialPort1.WriteLine(":FREQ "+ freqVals[r].ToString());          // Set Measurement frequency: 
                                string f = freqVals[r].ToString();
                                serialPort1.WriteLine("*TRG;:MEAS?");       // Reads trigger and measurement results
                                rd = serialPort1.ReadLine();                  // Acquires measurement results

                                // FIX ORDER OF OUTPUT START
                                string[] paramOutput = rd.Split(',');
                                correctOutput[0] = paramOutput[paramOrder[0]];
                                correctOutput[1] = paramOutput[paramOrder[1]];
                                correctOutput[2] = paramOutput[paramOrder[2]];
                                correctOutput[3] = paramOutput[paramOrder[3]];
                                string finalOutput = f + "," + correctOutput[0] + "," + correctOutput[1] + "," + correctOutput[2] + "," + correctOutput[3];
                                // FIX ORDER OF OUTPUT END 
                               // Console.WriteLine("6");
                                this.BeginInvoke((MethodInvoker)delegate { sPop.addPoint(furnaceTemp, f, (decimal)double.Parse(correctOutput[0]), (decimal)double.Parse(correctOutput[1]), (decimal)double.Parse(correctOutput[2]), (decimal)double.Parse(correctOutput[3]));});
                                fp.Write(finalOutput);                               // Outputs the measurement results to file
                                fp.Write(Environment.NewLine);                           // Outputs the line feed to file
                            }
                        }


                        fp.Close();




                        tempVals.RemoveAt(0); // if temp reached, remove it from list
                        //fp.Close();



                    }

                }



                serialPort1.Close();
                // serialPort2.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRPR");
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
