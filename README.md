[![License: MIT](https://img.shields.io/badge/license-MIT-blue)](https://github.com/kkuba91/uGESRTP/blob/master/LICENSE)
[![Language: Platform](https://img.shields.io/badge/platform-win--32%20%7C%20win--64-lightgrey)](https://github.com/kkuba91/uGESRTP)
[![.NET: Version](https://img.shields.io/badge/.NET-%3E%3D4.5-brightgreen)](https://github.com/kkuba91/uGESRTP)

# uGESRTP
#### Micro GE Inteligent PLCs communication protocol.
#### Used by PLC family: 90-30, 90-70, RX3i, RX7i.
#### Class tested with RX3i CPU and general functions for data exchange with a PLC.
#### Protocol was design mainly to communicate Machine Edition Development Environment with PLC. Unfortunately this is the native protocol also for: diagnostic, start/stop, program dwnld/upld and so on. That is so the data exchange is high aviable this way and protocol allows to take a full access to PLC (like by PLC IDE). The structure is closed and explored at observations. Somehow restricted(parametrized) access to tags was given to other protocols like Modbus, tcp.OPC. That is why preffered use for commisioning is a principle practice strongly recommended here.

Metodology of the protocol:

- Set/Reset value (like in Modbus - no realtime ethernet - no watchdog, but still it is quite fast)

- According observations[2] SCADA drivers request for every memory type (%R, %AI, &M, %Q..) separately (not possible to pack two requests inside one tcp/ip transaction)

- According observations[2] SCADA drivers always request for not single data register (in my case it was 7 registers data exchange, but only one was read - requested one SCADA). In generał it is because of required structure of ACK,PSH. The answer packet has required length (56 bytes). This mechanism forces PLC to send next packet with bare data only (requested data values listed inside a ACK, PSH packet. It is convenient for larger data rangers read from the PLC. 

- Use of Ethernet Service Requests for data manipulation (something similar like SRV_REQ function block in PLC code, but it has other coding)

- It uses TCP/IP communication with initialization (handshake) at the begining

### How to:

#### Warning: Used here address value should be Uint16 type and has its value range. Larger will be cut-off (overflowed) to Uint16.

For init and start to communicate:

 ```csharp
uGESRTP GE_driver(192.168.0.10);    /* Init object with PLC IPv4 address */ 
int status = GE_driver.initConnection();    /* status variale should return 0 with success */
 ```
Than it is dependent of purpose of application and could be used read/write functions.

#### For REGISTERS (%R):
 ```csharp
/*    Read register (Ex. R15 word)
 *    return := INTEGER16BIT value    */
int address = 15;
Int16 value = read_R_WORD(address);
         
/*    Write to register (Ex. R15 word)
 *    return := INTEGER32BIT status    */
int value = 9876;    /* Value to write */
int status = write_R_WORD(address, value);
         
/*    Read register (Ex. R16-R17 dword)
 *    return := INTEGER32BIT value   */
address = 16;
Int32 DW_value = read_R_DWORD(address);
          
/*    Write to register (Ex. R16-R17 dword)
 *    return := INTEGER32BIT status    */
address = 16;
val = 64999;
status = write_R_DWORD(address, val);
           
/*    Read register (Ex. R16-R17 float)
 *    return := SINGLE PRECISION FLOAT value    */
address = 16;
float Fvalue =  read_R_FLOAT(address);
          
/*    Write to register (Ex. R16-R17 float)
 *    return := INTEGER32BIT status    */
address = 16;
Fvalue = 12.556f
status = write_R_FLOAT(address, Fvalue);
 ```
 
 #### For INPUTS (%I / %AI):
  ```csharp
/*    Read single input (Ex. Address 8 gives: I8)
 *    return := BIT value    */
int address = 8;
Boolean bit_value = read_I_BIT(address);
        
/*    Read Input byte (Ex. Address 8 gives: I8-I15)
 *    return := BYTE value    */
address = 8;
byte byte_value = read_I_BYTE(address);
         
/*    Read Input word (Ex. Address 8 gives: I8-I23)
 *    return := WORD value    */
address = 8;
Int16 value = read_I_WORD(address);
         
/*    Read Analog Input (Ex. AI1 word)
 *    return := INTEGER16BIT value    */
address = 1;    /* Analog Inputs given from different process image hardware inputs data than descrete inputs */
value = read_AI_WORD(address);
 ```
 
  #### For MARKERS (%M):
  ```csharp
/*    Read single marker (Ex. M15)
 *    return := BIT value    */
int address = 15;
Boolean Bit_Value = read_M_BIT(address);
        
/*    Write to marker bit (Ex. M15 bit)
 *    return := INTEGER32BIT status    */
address = 15;
Bit_Value = true;
int status = write_M_BIT(address, Bit_Value);
        
/*    Read byte of markers (Ex. M8-M15)
 *    return := BYTE value    */
address = 8;
byte B_val = read_M_BYTE(address);   
   ```
 
  #### For OUTPUTS (%Q / %AQ):
  ```csharp
/*    Read single Output (Ex. Address 8 gives: Q8)
 *    return := BIT value    */
int address = 8;
Boolean Bit_Value = read_Q_BIT(address);
       
/*    Read Output byte (Ex. Address 8 gives: Q8-Q15)
 *    return := BYTE value    */
address = 8;
byte Byte_Value = read_Q_BYTE(address);
        
/*    Read Output word (Ex. Address 8 gives: Q8-Q23)
 *    return := WORD value    */
 address = 8;
 Int16 value = read_Q_WORD(address);
       
/*    Read Analog Output (Ex. AQ1 word)
 *    return := INTEGER16BIT value    */
address = 1;    /* Analog Outputs given from different process image hardware outputs data than descrete outputs */
value = read_AQ_WORD(address);
   ```
 
 For deinit and close communicate on both sides:

 ```csharp
int status = GE_driver.closeConnection();    /* status variale should return 0 with success */
 ```
 
 ## Check out an example:
[LINK](https://github.com/kkuba91/uGESRTP/tree/main/Example%20-%20Rich)

 ## Trademarks and symbols mentioned here
 
 GE Inteligent (PLC, Machine Edition environment) are used and distributed by Emerson - ©2020 Emerson Electric Co. All rights reserved.
 
 ## Reference:
 [1] "Leveraging the SRTP protocol for over-the-network memory acquisition of a GE Fanuc Series 90-30"; George Denton, Filip Karpisek, Frank Breitinger, Ibrahim Baggili; DFRWS 2017 USA d Proceedings of the Seventeenth Annual DFRWS USA
 
 [2] Hardware testing - Read 8 Registers in the same request ([LINK](https://github.com/kkuba91/uGESRTP/blob/main/ReadExamplePackets.png))
 
