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
        //MicroRWD RFID���O
        private byte RWD_CMD_READ_TAG_UID = 0x55;

        public connect() { }

        //�]�wCOMO PORT��l��
        public connect(System.IO.Ports.SerialPort sp, String port_number)
        {
            sp.PortName = port_number;
            sp.BaudRate = 9600;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Handshake = Handshake.RequestToSend;
            serialPort1 = sp;
        }

        //�}��COM PORT
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

        //����COM PORT
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
        
        //Ū���d����UID
        public String read_UID()
        {
            //�ǰe���O��Ū���d��UID
            byte[] send = new byte[1];
            send[0] = RWD_CMD_READ_TAG_UID;
            serialPort1.Write(send, 0, 1);
            //���𱵦���ơA�קK��������^�Ǹ��
            System.Threading.Thread.Sleep(200);

            //�����^�Ǹ��
            int bytes = serialPort1.BytesToRead;
            byte[] buffer = new byte[bytes];
            serialPort1.Read(buffer, 0, bytes);
            String temp2 = "", data = "", TAG_UID = "";
            for (int i = 0; i < bytes; i++) //�N����ন16�i��
            {
                temp2 = buffer[i].ToString("x").ToUpper();
                if (temp2.Length == 1)
                    data += "0" + temp2;
                else
                    data += temp2;
            }

            //�P�_������ƪ��׬O�_���T�A�Y���T�N�^��
            if (data.StartsWith("86") && data.Length == 16)
            {
                //�P�_�d���s����7�Ӧ줸�թ�4�Ӧ줸��
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
