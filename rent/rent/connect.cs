using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Connect
{
    public class connect
    {
        public System.IO.Ports.SerialPort serialPort1;
        //MicroRWD RFID指令
        private byte RWD_CMD_READ_TAG_UID = 0x55;

        public connect() { }

        //設定COMO PORT初始值
        public connect(System.IO.Ports.SerialPort sp, String port_number)
        {
            sp.PortName = port_number;
            sp.BaudRate = 9600;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Handshake = Handshake.RequestToSend;
            serialPort1 = sp;
        }

        //開啟COM PORT
        public void open_port()
        {
            try
            {
                if (!serialPort1.IsOpen)
                    serialPort1.Open();
            }
            catch (IOException)
            {
                MessageBox.Show("Port error", "");
            }
        }

        //關閉COM PORT
        public void close_port()
        {
            try
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Port error", "");
            }
        }
        
        //讀取卡片的UID
        public String read_UID()
        {
            //傳送指令來讀取卡片UID
            byte[] send = new byte[1];
            send[0] = RWD_CMD_READ_TAG_UID;
            serialPort1.Write(send, 0, 1);
            //延遲接收資料，避免未接收到回傳資料
            System.Threading.Thread.Sleep(200);

            //接收回傳資料
            int bytes = serialPort1.BytesToRead;
            byte[] buffer = new byte[bytes];
            serialPort1.Read(buffer, 0, bytes);
            String temp2 = "", data = "", TAG_UID = "";
            for (int i = 0; i < bytes; i++) //將資料轉成16進位
            {
                temp2 = buffer[i].ToString("x").ToUpper();
                if (temp2.Length == 1)
                    data += "0" + temp2;
                else
                    data += temp2;
            }

            //判斷接收資料長度是否正確，若正確就回傳
            if (data.StartsWith("86") && data.Length == 16)
            {
                //判斷卡片編號為7個位元組或4個位元組
                if (data.Substring(10, 2).Equals("00") & data.Substring(12, 2).Equals("00") & data.Substring(14, 2).Equals("00"))
                    TAG_UID = data.Substring(2, 8);
                else
                    TAG_UID = data.Substring(2, 14);
            }
            if (data.StartsWith("80"))
                TAG_UID = "";

            return TAG_UID;
        }
    }
}
