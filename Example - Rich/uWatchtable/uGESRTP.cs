using System;
using System.Net.Sockets;


namespace uWatchtable
{
    class uGESRTP
    {
        /*    First send 56 bytes, to the PLC all:       0x00 0x00 ... 
         *    before real message. It will respond with: 0x01 0x00 ...
         *    */
        private byte[] msg_init = new byte[56];

        /*    This is standard message format, you need to update 
         *    some fields before sending.
         *    PC sends request starting with: 0x02 0x00 ...
         *    PLC respond with message like:  0x03 0x00 ...
         *    */
        private byte[] msg_base = { 
            0x02,        // 00 - Type (x03:=ReceiveOK, x02:=Transmit, x08:=Something just after msg_init - maybe kind of exception interruption?)
            0x00,        // 01 - Unknown
            0x06,        // 02 - Seq Number (x06 is ok, x05 also according referenced document)
            0x00,        // 03 - Unknown
            0x00,        // 04 - Text Length
            0x00,        // 05 - Unknown / Text character?
            0x00,        // 06 - Unknown / Text character?
            0x00,        // 07 - Unknown / Text character?
            0x00,        // 08 - Unknown / Text character?
            0x01,        // 09 - Unknown / Text character?
            0x00,        // 10 - Unknown / Text character?
            0x00,        // 11 - Unknown / Text character?
            0x00,        // 12 - Unknown / Text character?
            0x00,        // 13 - Unknown / Text character?
            0x00,        // 14 - Unknown / Text character?
            0x00,        // 15 - Unknown / Text character?
            0x00,        // 16 - Unknown / Text character?
            0x01,        // 17 - Unknown / Always x01
            0x00,        // 18 - Unknown
            0x00,        // 19 - Unknown
            0x00,        // 20 - Unknown
            0x00,        // 21 - Unknown
            0x00,        // 22 - Unknown
            0x00,        // 23 - Unknown
            0x00,        // 24 - Unknown
            0x00,        // 25 - Unknown
            0x00,        // 26 - Time Seconds
            0x00,        // 27 - Time Minutes
            0x00,        // 28 - Time Hours
            0x00,        // 29 - Reserved / Always x01
            0x06,        // 30 - Seq Number (Repeated) (0x06) - meaning in responces from PLC
            0xc0,        // 31 - Message Type
            0x00,        // 32 - Mailbox Source
            0x00,        // 33 - Mailbox Source
            0x00,        // 34 - Mailbox Source
            0x00,        // 35 - Mailbox Source
            0x10,        // 36 - Mailbox Destination // dec: 3600
            0x0e,        // 37 - Mailbox Destination
            0x00,        // 38 - Mailbox Destination
            0x00,        // 39 - Mailbox Destination
            0x01,        // 40 - Packet Number // to check
            0x01,        // 41 - Total Packet Number
            0x00,        // 42 - Service Request Code - (Operation Type SERVICE_REQUEST_CODE)
            0x00,        // 43 - Request Dependent Space (Ex. MEMORY_TYPE_CODE)
            0x00,        // 44 - Request Dependent Space (Ex. Address:LSB)
            0x00,        // 45 - Request Dependent Space (Ex. Address:MSB)
            0x00,        // 46 - Request Dependent Space (Ex. Data Size Words:LSB)
            0x00,        // 47 - Request Dependent Space (Ex. Data Size Words:MSB)
            0x00,        // 48 - Request Dependent Space (Ex. Write Value:LSB)
            0x00,        // 49 - Request Dependent Space (Ex. Write Value:MSB)
            0x00,        // 50 - Request Dependent Space (Ex. Write Value Part 2 for LONG:LSB)
            0x00,        // 51 - Request Dependent Space (Ex. Write Value Part 2 for LONG:MSB)
            0x00,        // 52 - Dependent of "Data Size" - byte 46, 47 / PLC status in other Service Request
            0x00,        // 53 - Dependent of "Data Size" - byte 46, 47 / PLC status in other Service Request
            0x00,        // 54 - Dependent of "Data Size" - byte 46, 47 / PLC status in other Service Request
            0x00         // 55 - Dependent of "Data Size" - byte 46, 47 / PLC status in other Service Request
        };

        // Used at byte locaiton 42:
        enum SERVICE_REQUEST : byte
        {
            PLC_STATUS = 0x00,
            RETURN_PROG_NAME = 0x03,
            READ_SYS_MEMORY = 0x04,    // Used to read general memory (Example: %R123)
            READ_TASK_MEMORY = 0x05,
            READ_PROG_MEMORY = 0x06,
            WRITE_SYS_MEMORY = 0x07,   // Used to write general memory
            WRITE_TASK_MEMORY = 0x08,
            WRITE_PROG_MEMORY = 0x09,
            RETURN_DATETIME = 0x25,
            RETURN_CONTROLLER_TYPE = 0x43
        }

        // Used at byte locaiton 43:
        enum MEMORY_TYPE : byte
        {
            /* Word selector - address */
            R = 0x08,    // Register (Word)
            AI = 0x0a,   // Analog Input (Word)
            AQ = 0x0c,   // Analog Output (Word)

            /* Byte selector - address */
            I_BYTE = 0x10,    // Descrete Inputs (Byte)
            Q_BYTE = 0x12,    // Descrete Outputs (Byte)
            T_BYTE = 0x14,    // Descrete Temporary Bits (Byte)
            M_BYTE = 0x16,    // Descrete Markers (Byte)
            SA_BYTE = 0x18,   // System Bits A-part (Byte)
            SB_BYTE = 0x20,   // System Bits B-part (Byte)
            SC_BYTE = 0x22,   // System Bits C-part (Byte)
            G_BYTE = 0x38,    // Genius Global (Byte)

            /* Binary selector - address */
            I_BIT = 0x46,    // Descrete Input (Bit)
            Q_BIT = 0x48,    // Descrete Output (Bit)
            T_BIT = 0x4a,    // Descrete Temporary (Bit)
            M_BIT = 0x4c,    // Descrete Marker (Bit)
            SA_BIT = 0x4e,   // System Bit A-part (Bit)
            SB_BIT = 0x50,   // System Bit B-part (Bit)
            SC_BIT = 0x52,   // System Bit C-part (Bit)
            G_BIT = 0x56,    // Genius Global (Bit)
        }

        public Int32 PLC_PORT;
        public string sIP;
        private TcpClient client;
        private NetworkStream stream;
        public Boolean Connected;

    public uGESRTP(string ip)
        {
            this.Connected = false;
            this.PLC_PORT = 18245;
            this.sIP = ip;
        }

    ~uGESRTP()
        {
            this.Connected = false;
            // Close everything without handshaking "Close command" with PLC
            this.stream.Close();
            this.client.Close();
        }

        /*    Initialize communication with PLC
         *    return := 0 -> OK
         *    */
        public int initConnection()
        {
            byte[] data = new byte[1024];
            try
            {
                this.client = new TcpClient(this.sIP, this.PLC_PORT);
                this.client.ReceiveTimeout = 3000;
                this.client.SendTimeout = 3000;

                if (client.Connected)
                {
                    this.stream = client.GetStream();
                    this.stream.Write(this.msg_init, 0, this.msg_init.Length);
                    Int32 bytes = this.stream.Read(data, 0, data.Length);
                    if (data[0] == 0x01)
                    {
                        this.Connected = true;
                        return 0;
                    }
                    else
                    {
                        this.Connected = false;
                        this.stream.Close();
                        this.client.Close();
                        return -2;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /*    Close communication with PLC
         *    return := 0 -> OK (PLC also closed comm)
         *    */
        public int closeConnection()
        {
            byte[] data = new byte[1024];
            try
            {
                if (client.Connected)
                {
                    //this.msg_init[0] = 0x04; //Close connection for PLC TCP-ACK interruption
                    this.stream.Write(this.msg_init, 0, this.msg_init.Length);
                    Int32 stat = this.stream.Read(data, 0, data.Length);
                    this.Connected = false;
                    this.stream.Close();
                    this.client.Close();
                    return 0;
                }
                return -1;
            }
            catch (Exception ex)
            {
                this.Connected = false;
                return -1;
            }
        }


        /* REGISTERS MEMORY "%R" READ/WRITE (WORD/DWORD/FLOAT)*/
        #region REGISTERS

        /*    Read register (Ex. R15 word)
         *    return := INTEGER16BIT value
         *    */
        public Int16 read_R_WORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // 0 based register to read
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int16)BitConverter.ToInt16(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /*    Write to register (Ex. R15 word)
         *    return := INTEGER32BIT status
         *    */
        public int write_R_WORD(int address, Int16 val)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            byte[] byteArray = BitConverter.GetBytes(val);

            msg_send[42] = (byte)SERVICE_REQUEST.WRITE_SYS_MEMORY;

            // Update for type of register:
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // Register to read (two bytes):
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for data quantity (two bytes):
            msg_send[46] = 0x01;
            msg_send[47] = 0x00;
            // Value:
            msg_send[48] = byteArray[0];    // Get LSB of Word
            msg_send[49] = byteArray[1];    // Get MSB of Word
            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /*    Read register (Ex. R16-R17 dword)
         *    return := INTEGER32BIT value
         *    */
        public Int32 read_R_DWORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // 0 based register to read
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x02;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int32)BitConverter.ToInt32(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /*    Write to register (Ex. R16-R17 dword)
         *    return := INTEGER32BIT status
         *    */
        public int write_R_DWORD(int address, Int32 val)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            byte[] byteArray = BitConverter.GetBytes(val);

            msg_send[42] = (byte)SERVICE_REQUEST.WRITE_SYS_MEMORY;

            // Update for type of register:
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // Register to read (two bytes):
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for data quantity (two bytes):
            msg_send[46] = 0x02;
            msg_send[47] = 0x00;
            // Value:
            msg_send[48] = byteArray[0];    // Get LSB of Word
            msg_send[49] = byteArray[1];    // Get MSB of Word
            msg_send[50] = byteArray[2];    // Get LSB of Word
            msg_send[51] = byteArray[3];    // Get MSB of Word

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /*    Read register (Ex. R16-R17 float)
         *    return := SINGLE PRECISION FLOAT value
         *    */
        public float read_R_FLOAT(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // 0 based register to read
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x02;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (float)BitConverter.ToSingle(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /*    Write to register (Ex. R16-R17 float)
         *    return := INTEGER32BIT status
         *    */
        public int write_R_FLOAT(int address, float val)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            byte[] byteArray = BitConverter.GetBytes(val);

            msg_send[42] = (byte)SERVICE_REQUEST.WRITE_SYS_MEMORY;

            // Update for type of register:
            msg_send[43] = (byte)MEMORY_TYPE.R;
            // Register to read (two bytes):
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for data quantity (two bytes):
            msg_send[46] = 0x02;
            msg_send[47] = 0x00;
            // Value:
            msg_send[48] = byteArray[0];    // Get LSB of Word
            msg_send[49] = byteArray[1];    // Get MSB of Word
            msg_send[50] = byteArray[2];    // Get LSB of Word
            msg_send[51] = byteArray[3];    // Get MSB of Word
            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        /* INPUTS MEMORY "%I" / "%AI" READ (BIT/BYTE/WORD)*/
        #region INPUTS

        /*    Read single input (Ex. Address 8 gives: I8)
         *    return := BIT value
         *    */
        public Boolean read_I_BIT(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 word_val = 0;

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.I_BIT;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)address);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        word_val = (Int16)BitConverter.ToInt16(msg_read, 44);
                        return ((word_val) != 0);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        /*    Read Input byte (Ex. Address 8 gives: I8-I15)
         *    return := BYTE value
         *    */
        public byte read_I_BYTE(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 byteAddr = (Int16)(address / 8);
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.I_BYTE;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)byteAddr);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return msg_read[44];
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        /*    Read Input word (Ex. Address 8 gives: I8-I23)
         *    return := WORD value
         *    */
        public Int16 read_I_WORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 byteAddr = (Int16)(address / 8);
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.I_BYTE;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)byteAddr);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x02;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int16)BitConverter.ToInt16(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        /*    Read Analog Input (Ex. AI1 word)
        *    return := INTEGER16BIT value
        *    */
        public Int16 read_AI_WORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.AI;
            // 0 based register to read
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int16)BitConverter.ToInt16(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        #endregion

        /* MARKERS MEMORY "%M" READ/WRITE (BIT/BYTE)*/
        #region MARKERS

        /*    Read single marker (Ex. M15)
         *    return := BIT value
         *    */
        public Boolean read_M_BIT(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 word_val = 0;
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.M_BIT;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)address);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        word_val = (Int16)BitConverter.ToInt16(msg_read, 44);
                        return (word_val != 0);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        /*    Write to marker bit (Ex. M15 bit)
         *    return := INTEGER32BIT status
         *    */
        public int write_M_BIT(int address, Boolean val)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.WRITE_SYS_MEMORY;

            // Update for type of marker bit:
            msg_send[43] = (byte)MEMORY_TYPE.M_BIT;
            // Register to read (two bytes):
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for data quantity (two bytes):
            msg_send[46] = 0x01;
            msg_send[47] = 0x00;
            // Value:
            if(val) // Get LSB of Word
                msg_send[48] = 0xff;    
            else
                msg_send[48] = 0x00;
            msg_send[49] = 0x00;    // Get MSB of Word
            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return 0;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        /*    Read byte of markers (Ex. M8-M15)
         *    return := BYTE value
         *    */
        public byte read_M_BYTE(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 byteAddr = (Int16)(address / 8);
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.M_BYTE;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)byteAddr);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return msg_read[44];
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        #endregion

        /* OUTPUTS MEMORY "%Q" / "%AQ" READ/WRITE (BIT/BYTE/WORD)*/
        #region OUTPUTS

        /*    Read single Output (Ex. Address 8 gives: Q8)
         *    return := BIT value
         *    */
        public Boolean read_Q_BIT(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 word_val = 0;

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.Q_BIT;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)address);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        word_val = (Int16)BitConverter.ToInt16(msg_read, 44);
                        return ((word_val) != 0);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        /*    Read Output byte (Ex. Address 8 gives: Q8-Q15)
         *    return := BYTE value
         *    */
        public byte read_Q_BYTE(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 byteAddr = (Int16)(address / 8);
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.Q_BYTE;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)byteAddr);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return msg_read[44];
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        /*    Read Output word (Ex. Address 8 gives: Q8-Q23)
         *    return := WORD value
         *    */
        public Int16 read_Q_WORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 byteAddr = (Int16)(address / 8);
            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.Q_BYTE;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)byteAddr);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x02;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int16)BitConverter.ToInt16(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        /*    Read Analog Output (Ex. AQ1 word)
        *    return := INTEGER16BIT value
        *    */
        public Int16 read_AQ_WORD(int address)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);

            msg_send[42] = (byte)SERVICE_REQUEST.READ_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.AQ;
            // 0 based register to read
            address = address - 1;
            msg_send[44] = (byte)(address & 0xFF);    // Get LSB of Word
            msg_send[45] = (byte)(address >> 8);      // Get MSB of Word
            // Update for width
            msg_send[46] = 0x01;     // quantity of data

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return (Int16)BitConverter.ToInt16(msg_read, 44);
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        /*    Write single Output (Ex. For Address 8 gives: Q8)
         *    return := BIT value
         *    */
        public Int16 write_Q_BIT(int address, Boolean val)
        {
            byte[] msg_send = new byte[56];
            byte[] msg_read = new byte[512];
            this.msg_base.CopyTo(msg_send, 0);
            Int16 word_val = 0;

            msg_send[42] = (byte)SERVICE_REQUEST.WRITE_SYS_MEMORY;

            // Update for type of register
            msg_send[43] = (byte)MEMORY_TYPE.Q_BIT;
            // 0 based register to read
            address = address - 1;
            byte[] byteArrayAddress = BitConverter.GetBytes((Int16)address);

            msg_send[44] = byteArrayAddress[0];    // Get LSB of Word
            msg_send[45] = byteArrayAddress[1];      // Get MSB of Word
            // Quantity of data:
            msg_send[46] = 0x01; 
            msg_send[47] = 0x00;
            // Value:
            if (val) // Get LSB of Word
                msg_send[48] = 0xff;
            else
                msg_send[48] = 0x00;
            msg_send[49] = 0x00; // Get MSB of Word

            try
            {
                if (this.client.Connected)
                {
                    this.stream.Write(msg_send, 0, msg_send.Length);
                    Int32 bytes = this.stream.Read(msg_read, 0, msg_read.Length);
                    if (msg_read[0] == 0x03)
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return -2;
            }
            return -1;
        }

        #endregion

    }
}
