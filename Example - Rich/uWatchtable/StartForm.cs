using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Linq;
using System.Globalization;
using GEwatch;
using System.Threading.Tasks;

namespace uWatchtable
{
    public partial class StartForm : Form
    {
        /* Dynamic Libraries include */
        /* void ReleaseCapture() and void SendMessage() Ex. used for drag the window on the screen (custom topbars/AppHolders)
         */
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        /* 
         * PUBLIC DECLARATIONS 
         */
        uGESRTP GE_driver;
        WatchRowData[] WatchData;
        WatchRowData temp;
        public int readIndex = 0;
        public int rowIndex = 0;

        /* StartForm - First Opening form Constructor function
         * Icludes: Dictionary of Ethernet adapter types
         */
        public StartForm()
        {
            this.Visible = false;
            InitializeComponent();

            GE_driver = new uGESRTP("192.168.0.100");
            WatchData = new WatchRowData[100];
            for(int i = 0; i<100;i++)
                WatchData[i] = new WatchRowData();

            temp = new WatchRowData();

            label_min.Text = trackBar_Refresh.Minimum.ToString()+"ms";
            label_max.Text = trackBar_Refresh.Maximum.ToString()+"ms";
            this.Visible = true;
        }

        /* WinForms GUI Action Functions (self-generated from properties toolbox)
         */

        /**
         * \brief           Close Application Action triggered by right up corner Close Button
         * \return          Sum of input values
         */
        private void buttonClose_Click(object sender, EventArgs e)
        {
            timerRW.Enabled = false;
            GE_driver.closeConnection();
            Application.Exit();
        }

        /**
         * \brief           Minimize Application Action triggered by right up corner Minimize Button
         */
        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /**
         * \brief           Drag Application form on the screen Actions
         * \note            Depending which graphical object is pressed by mouse cursor there are 3 functions for:
         *                  - TopBar
         *                  - label located in TopBar
         *                  - logo (pictureBox) located in TopBar on the Left
         */
        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void pictureBoxLogo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }


        /* Other functions
         */


        private void button_GE_Connect_Click(object sender, EventArgs e)
        {
            GE_driver.sIP = textBox_GE_IP.Text;
            int status = GE_driver.initConnection();
            if(status == 0)
            {
                label_GE_Status.Text = "OK";
                label_GE_Status.ForeColor = System.Drawing.Color.Green;
                label_GE_Status.Visible = true;
                button_GE_Connect.Visible = false;
                button_GE_Disconnect.Visible = true;
                timerRW.Enabled = true;
            }
            else
            {
                label_GE_Status.Text = "NOK";
                label_GE_Status.ForeColor = System.Drawing.Color.Red;
                label_GE_Status.Visible = true;
                timerRW.Enabled = false;
            }
        }

        private void button_GE_Disconnect_Click(object sender, EventArgs e)
        {
            timerRW.Enabled = false;
            int status = GE_driver.closeConnection();
            if (status == 0)
            {
                label_GE_Status.Visible = false;
                button_GE_Connect.Visible = true;
                button_GE_Disconnect.Visible = false;
            }
            else
            {
                label_GE_Status.Text = "OK?";
            }
        }

        private void textBox_GE_IP_TextChanged(object sender, EventArgs e)
        {
            IPAddress ip;
            string stempIP = textBox_GE_IP.Text;
            string ipaddress = textBox_GE_IP.Text;
            bool ValidateIP = IPAddress.TryParse(ipaddress, out ip);
            if (stempIP != "")
            {
                label_GE_valid.Visible = true;

                int count = 0;
                foreach (char c in stempIP)
                    if (c == '.') count++;

                if (ValidateIP && count==4)
                    label_GE_valid.Text = "Valid";
                else
                    label_GE_valid.Text = "Not valid";
            }
        }

        /***
         *  AUTOCOMPLETE BINDED WITH CELL EDIT END EVENT 
         *  Validate after cell value changed + TODO TYPE definition not complete
         *  */
        private void dataTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sType = "";
            string sBuff = "";
            string sBuff2 = "";
            string sChar = "";
            string b = "";
            int ok_status = 0;
            // Address Column:
            // ADDRESS:
            if (e.ColumnIndex == 0)
            {

                int len = 0;
                int val_start = -1, val_stop = 0;

                //Validate first in general to not make exception
                if (dataTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    sBuff2 = dataTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }

                //Validate second that string is greater than 2
                if (sBuff2.Length >= 2)
                {
                    for (int i = 0; i < sBuff2.Length; i++)
                    {
                        if (Char.IsDigit(sBuff2[i]))
                        {
                            b += sBuff2[i];
                            if (val_start == -1)
                                val_start = i;
                            val_stop = i;
                        }
                    }

                    if (val_start != -1)
                        sType = sBuff2.Substring(0, val_start);

                    if (val_stop != 0 && val_start != -1)
                        sChar = sBuff2.Substring(val_stop + 1);

                    if (sType.Length < 1 || sType.Length > 2 || val_start == -1)
                        ok_status = 1;
                    if (sChar.Length > 2)
                        ok_status = 2;

                    if (ok_status == 0)
                    {
                        sBuff = sBuff2.Substring(sType.Length, sBuff2.Length - sChar.Length - 1);
                        len = sBuff.Length;
                    }

                    if (!sBuff.All(char.IsDigit))
                        ok_status = 3;
                }
                else ok_status = 4;

                // AUTOCOMPLETE - Validate ending type and change to capital letter:
                if (sChar.Length > 0 && ok_status == 0)
                {
                    if (sType == "R" || sType == "r")
                    {
                        if (sChar == "f")
                            sChar = "F";
                        if (sChar == "dw")
                            sChar = "DW";
                        if (sChar == "l")
                            sChar = "L";
                        if (sChar != "F" && sChar != "DW" && sChar != "L")
                        {
                            sChar = string.Empty;
                        }
                    }
                    else
                    {
                        sChar = string.Empty;
                    }
                }

                // AUTOCOMPLETE - Change to capital prefix:
                if (sType == "r" && ok_status == 0)
                    sType = "R";
                if (sType == "m" && ok_status == 0)
                    sType = "M";
                if (sType == "g" && ok_status == 0)
                    sType = "G";
                if (sType == "i" && ok_status == 0)
                    sType = "I";
                if (sType == "ai" && ok_status == 0)
                    sType = "AI";
                if (sType == "q" && ok_status == 0)
                    sType = "Q";
                if (sType == "aq" && ok_status == 0)
                    sType = "AQ";

                if (ok_status == 0)
                {
                    dataTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sType + sBuff + sChar;
                }
                else
                {
                    if (sBuff2 != "#")
                    {
                        dataTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
                    }
                    else
                    {
                        WatchData[rowIndex].comment = true;
                    }
                }             

                if (ok_status == 0 && WatchData[rowIndex].comment == false)
                {
                    // WRITE TAG: TYPE, ADDRESS VALUE, SPECIFIER TO THE ARRAY (used for R/W functions)
                    try
                    {
                        temp.Address.Type = sType;
                        temp.Address.Specifier = sChar;
                        temp.Address.Value = int.Parse(sBuff);
                        
                        //WatchData[rowIndex] = temp; - many troubles with array!!!
                        int.TryParse(sBuff, out WatchData[rowIndex].Address.Value);
                        WatchData[rowIndex].Address.Type = sType;
                        WatchData[rowIndex].Address.Specifier = sChar; //rowIndex
                    }
                    catch (Exception ee) { }

                    // AUTOCOMPLETE FOR TAG SPECIFICATION - BIN | DEC | FLOAT:
                    if (dataTable.Rows[rowIndex].Cells[3].Value == null && sType != "")
                    {
                        if (temp.Address.Type == "R" && temp.Address.Specifier != "F" || temp.Address.Type == "AI" || temp.Address.Type == "AQ")
                        {
                            dataTable.Rows[e.RowIndex].Cells[3].Value = "DEC";
                        }
                        if (temp.Address.Type == "R" && temp.Address.Specifier == "F")
                        {
                            dataTable.Rows[e.RowIndex].Cells[3].Value = "FLOAT";
                        }
                        if (temp.Address.Type == "M" ||
                            temp.Address.Type == "G" ||
                            temp.Address.Type == "T" ||
                            temp.Address.Type == "I" ||
                            temp.Address.Type == "Q")
                        {
                            dataTable.Rows[e.RowIndex].Cells[3].Value = "BIN";
                            WatchData[e.RowIndex].Address.Type = temp.Address.Type;
                        }
                        WatchData[e.RowIndex].Representation = dataTable.Rows[e.RowIndex].Cells[3].Value.ToString();
                    }

                }

            }

            // VALUE WRITE:
            if (e.ColumnIndex == 5) 
            {
                string representation = "";
                if(dataTable.Rows[e.RowIndex].Cells[3].Value != null)
                    representation = dataTable.Rows[e.RowIndex].Cells[3].Value.ToString();
                bool val_bool = false;
                Int16 val_i16 = 0;
                Int32 val_i32 = 0;
                float val_f = 0.0f;
                labelStat.Text = "Writing..";

                if (WatchData[e.RowIndex].Address.Type=="R")
                    switch(representation)
                    {
                        case "DEC":
                            labelStat.Text = "Writing R..";
                            if (WatchData[e.RowIndex].Address.Specifier=="")
                            {
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                {
                                    val_i16 = Convert.ToInt16(dataTable.Rows[e.RowIndex].Cells[5].Value.ToString(), new CultureInfo("en-US"));
                                    int stat = GE_driver.write_R_WORD(WatchData[e.RowIndex].Address.Value, val_i16);
                                    if(stat==0) labelStat.Text = "Writing R WORD ok"; else labelStat.Text = "Writing R WORD NOK";
                                }
                            }
                            if (WatchData[e.RowIndex].Address.Specifier == "F")
                            {
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                {
                                    val_f = Convert.ToSingle(dataTable.Rows[e.RowIndex].Cells[5].Value.ToString(), new CultureInfo("en-US"));
                                    val_f = (float)((int)val_f);
                                    int stat = GE_driver.write_R_FLOAT(WatchData[e.RowIndex].Address.Value, val_f);
                                    if (stat == 0) labelStat.Text = "Writing R FLOAT ok"; else labelStat.Text = "Writing R FLOAT NOK";
                                }
                            }
                            if (WatchData[e.RowIndex].Address.Specifier == "DW")
                            {
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                {
                                    val_i32 = Convert.ToInt32(dataTable.Rows[e.RowIndex].Cells[5].Value.ToString(), new CultureInfo("en-US"));
                                    int stat = GE_driver.write_R_DWORD(WatchData[e.RowIndex].Address.Value, val_i32);
                                    if (stat == 0) labelStat.Text = "Writing R DWORD ok"; else labelStat.Text = "Writing R DWORD NOK";
                                }
                            }
                            break;

                        case "FLOAT":
                            labelStat.Text = "Writing R ..";
                            if (WatchData[e.RowIndex].Address.Specifier == "F")
                            {
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                {
                                    val_f = Convert.ToSingle(dataTable.Rows[e.RowIndex].Cells[5].Value.ToString(), new CultureInfo("en-US"));
                                    val_f = (float)((int)val_f);
                                    int stat = GE_driver.write_R_FLOAT(WatchData[e.RowIndex].Address.Value, val_f);
                                    if (stat == 0) labelStat.Text = "Writing R FLOAT ok"; else labelStat.Text = "Writing R FLOAT NOK";
                                }
                            }
                            break;

                        default:
                            break;
                    }
                // Write single bits:
                if(dataTable.Rows[e.RowIndex].Cells[0].Value!=null)
                    if (dataTable.Rows[e.RowIndex].Cells[0].Value.ToString().Length>2)
                    {
                        string cell_val = "";
                        switch (dataTable.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 1))
                        {
                            case "M":
                                labelStat.Text = "Writing M BIT..";
                                cell_val = "";
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                    cell_val = dataTable.Rows[e.RowIndex].Cells[5].Value.ToString();

                                if (cell_val != "" && cell_val != " ")
                                {
                                    dataTable.Rows[e.RowIndex].Cells[5].Value = sValidateBool(cell_val);
                                    val_bool = Convert.ToBoolean(cell_val, new CultureInfo("en-US"));
                                    int stat = GE_driver.write_M_BIT(WatchData[e.RowIndex].Address.Value, val_bool);
                                    if (stat == 0) labelStat.Text = "Writing M BIT ok"; else labelStat.Text = "Writing M BIT NOK";
                                }
                                
                                break;

                            case "Q":
                                labelStat.Text = "Writing Q BIT..";
                                cell_val = "";
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                    cell_val = dataTable.Rows[e.RowIndex].Cells[5].Value.ToString();

                                if (cell_val != "" && cell_val != " ")
                                {
                                    dataTable.Rows[e.RowIndex].Cells[5].Value = sValidateBool(cell_val);
                                    val_bool = Convert.ToBoolean(cell_val, new CultureInfo("en-US"));
                                    int stat = GE_driver.write_Q_BIT(WatchData[e.RowIndex].Address.Value, val_bool);
                                    if (stat == 0) labelStat.Text = "Writing Q BIT ok"; else labelStat.Text = "Writing Q BIT NOK";
                                }
                                
                                break;

                            case "G":
                                cell_val = "";
                                if (dataTable.Rows[e.RowIndex].Cells[5].Value != null)
                                    cell_val = dataTable.Rows[e.RowIndex].Cells[5].Value.ToString();

                                if (cell_val != "" && cell_val != " ")
                                {
                                    dataTable.Rows[e.RowIndex].Cells[5].Value = sValidateBool(cell_val);
                                    val_bool = Convert.ToBoolean(cell_val, new CultureInfo("en-US"));
                                    //GE_driver.write_G_BIT(WatchData[e.RowIndex].Address.Value, val_bool);    NOT PREPARED
                                    labelStat.Text = "Zapis G BIT ok";
                                }
                                labelStat.Text = "Zapis G BIT..";
                                break;
                        }
                    }
                        
            }

        }

        /// ########################################
        /// VALIDATION FOR INPUTS:
        /// ########################################
        /// 
        /// <summary>
        /// AS SIMPLE AS POSIBLE - BOOLEAN VALIDATION WITH DECISION WHICH STATE "FALSE" XOR "TRUE"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string sValidateBool(string input)
        {
            string output = string.Empty;
            if (input == "0" || input == "00" || input == "000" || input == "0000" || input == "fa" || input == "fal" || input == "fals")
                output = "False";
            if (input == "false" || input == "f" || input == "F" || input == "FA" || input == "FAL" || input == "FALS")
                output = "False";
            if (output != "False")
                output = "True";
            return output;
        }

        private void timerRW_Tick(object sender, EventArgs e)
        {
            int max2 = dataTable.RowCount-1;
            if(dataTable.Rows[readIndex].Cells[0].Value!=null && !WatchData[readIndex].comment)
            {
                WatchData[readIndex] = readRow(WatchData[readIndex]).Result;
                dataTable.Rows[readIndex].Cells[4].Value = WatchData[readIndex].sValueRead;
                label_Odczyt.Text = WatchData[readIndex].Address.Type + WatchData[readIndex].Address.Value + WatchData[readIndex].Address.Specifier;
                readIndex++;
            }
                if (readIndex >= max2)
                    readIndex = 0;
        }



        /// <summary>
        /// brief:     read data according address
        /// </summary>
        /// <param name="rowData"></param>
        private async Task<WatchRowData> readRow(WatchRowData rowData)
        {
            WatchRowData t = rowData;
            if(!rowData.comment && GE_driver.Connected)
            {
                t.sValueRead = "0";
                switch(t.Address.Type)
                {
                    case "R":
                        if (t.Address.Specifier == string.Empty)
                            t.sValueRead = GE_driver.read_R_WORD(t.Address.Value).ToString();
                        if (t.Address.Specifier == "DW")
                            t.sValueRead = GE_driver.read_R_DWORD(t.Address.Value).ToString();
                        if (t.Address.Specifier == "F")
                            t.sValueRead = GE_driver.read_R_FLOAT(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    case "M":
                        t.sValueRead = GE_driver.read_M_BIT(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    case "I":
                        t.sValueRead = GE_driver.read_I_BIT(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    case "Q":
                        t.sValueRead = GE_driver.read_Q_BIT(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    case "AI":
                        t.sValueRead = GE_driver.read_AI_WORD(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    case "AQ":
                        t.sValueRead = GE_driver.read_AQ_WORD(t.Address.Value).ToString(new CultureInfo("en-US"));
                        break;
                    default:
                        break;
                }
            }
            return t;

        }

        private void dataTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                rowIndex = e.RowIndex;
            }
        }

        private void trackBar_Refresh_ValueChanged(object sender, EventArgs e)
        {
            timerRW.Interval = trackBar_Refresh.Value;
        }
    }
}
