using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCRArduino
{
    public class Parameter
    {

        String symbol = "";
        int val = 0;
        bool reportNeg = false;

        public Parameter(String p)
        {
            setSymbol(p);
            setValue(p);
        }


        public void changeParameter(String p)
        {
            setSymbol(p);
            setValue(p);
        }


        public String getSymbol()
        {
            return symbol;
        }

        public int getVal()
        {
            return val;
        }

        public void setSymbol(string p)
        {
            symbol = p;
        }

        public void setReportNeg(bool c)
        {
            reportNeg = c;
        }

        public bool getReportNeg()
        {
            return reportNeg;
        }

        public void setValue(string p)
        {
            if (p.Equals("Z"))
                val = 1;
            else if (p.Equals("Y"))
                val = 2;
            else if (p.Equals("PHASE"))
                val = 3;
            else if (p.Equals("Cs"))
                val = 4;
            else if (p.Equals("Cp"))
                val = 5;
            else if (p.Equals("D"))
                val = 6;
            else if (p.Equals("Ls"))
                val = 7;
            else if (p.Equals("Lp"))
                val = 8;
            else if (p.Equals("Q"))
                val = 9;
            else if (p.Equals("Rs"))
                val = 10;
            else if (p.Equals("G"))
                val = 11;
            else if (p.Equals("Rp"))
                val = 12;
            else if (p.Equals("X"))
                val = 13;
            else if (p.Equals("B"))
                val = 14;
            else if (p.Equals("RDC"))
                val = 15;
            else if (p.Equals("S"))
                val = 16;
            else if (p.Equals("E"))
                val = 17;
        }



        public int compare(Parameter other)
        {
            if (this.getVal() < other.getVal())
                return -1;
            else if (this.getVal() > other.getVal())
                return 1;
            else
                return 0;
        }

        public static int[] getCurrentOrder(Parameter[] arr)
        {
            int[] res = new int[arr.Length];

            for (int l = 0; l< arr.Length; l++)
            {
                res[l] = 0;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i].compare(arr[j]) == 1)
                        res[i]++;
                    else if (arr[i].compare(arr[j]) == -1)
                        res[j]++;
                }
            }
            return res;
        }


    }
}
