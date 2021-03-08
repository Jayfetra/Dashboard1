using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dashboard1.Library;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.html;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel; // CancelEventArgs
using System.Configuration;

namespace Dashboard1.Helper
{
    class Sensor_input_Helper
    {

        public static void Command_Check(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "00191\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);

            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }

        public static void Command_CheckData(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "9119B\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }

        public static void Command_Stop(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }
        public static void Command_MoistureMeasure(SerialPort mySerialPort, string input)
        {
            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }
        public static void Command_NumberofGrain(SerialPort mySerialPort, string input)
        {

            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }

        public static void Command_Write(SerialPort mySerialPort, string input)
        {
            string data = input;
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }

        }

        public static void Command_MoisturAggregate(SerialPort mySerialPort)
        {
            //mySerialPort.Write("123");
            //public void Write (byte[] buffer, int offset, int count);
            string data = "9129C\r";
            byte[] hexstring = Encoding.ASCII.GetBytes(data);
            foreach (byte hexval in hexstring)
            {
                byte[] _hexval = new byte[] { hexval };     // need to convert byte 
                                                            // to byte[] to write
                mySerialPort.Write(_hexval, 0, 1);
                Thread.Sleep(10);
            }
        }

    }
}
