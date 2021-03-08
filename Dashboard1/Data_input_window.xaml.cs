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
using Dashboard1.Helper;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Dashboard1
{
    /// <summary>
    /// Interaction logic for Data_input_window.xaml
    /// </summary>
    /// 

    public partial class Data_input_window : Window
    {
        static string comport = ((MainWindow)Application.Current.MainWindow).ComboBox_input_data.Text;

        static int BaudRate = int.Parse(((MainWindow)Application.Current.MainWindow).ComboBox_Input_Baud.Text);
        SerialPort mySerialPort = new SerialPort(comport);
        int counter = 0;

        static string Folder_Path = ConfigurationManager.AppSettings["Folder_Path"] ?? "Not Found";
        static string application_name = ConfigurationManager.AppSettings["application_name"] ?? "Not Found";

        int counter_interval;
        int JumlahInterval;
        int TimeInterval;
        int NumberGrain;
        int NumberMeasure;
        int NumberGrain_Frekuensi;

        bool StatusListen = false;

        List<data_measure_2> data_finals_update = new List<data_measure_2> { };
        List<data_measure_2> data_finals_update_2 = new List<data_measure_2> { };
        List<data_measure_2> temp_data_finals_2 = new List<data_measure_2> { };


        List<Data_Measure> data_finals_ori = new List<Data_Measure> { };
        List<Data_Measure> temp_data_finals = new List<Data_Measure> { };

        //public YourCollection MyObjects { get; } = new YourCollection();
        //observe_datameasure data_finals_test { get; } = new observe_datameasure();


        public Data_input_window()
        {
            InitializeComponent();
            OpenCon_Port_local(mySerialPort, BaudRate);
            this.DataContext = this;
            //data_finals_update = SensorHelper_2.Test_DataMeasure_2();

            //Data_Receive_Grid.ItemsSource = data_finals_ori;
            data_initiation_input();
        }

        private void data_initiation_input()
        {
            ComboBox_TimeInterval.SelectedValuePath = "Key";
            ComboBox_TimeInterval.DisplayMemberPath = "Value";
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(0, "3 sec"));
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(1, "6 sec "));
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(2, "9 sec"));


            //
            ComboBox_NumberInterval.SelectedValuePath = "Key";
            ComboBox_NumberInterval.DisplayMemberPath = "Value";
            ComboBox_NumberInterval.Items.Add(new KeyValuePair<int, string>(0, "1"));
            ComboBox_NumberInterval.Items.Add(new KeyValuePair<int, string>(1, "2"));
            ComboBox_NumberInterval.Items.Add(new KeyValuePair<int, string>(2, "3"));

            //
            ComboBox_NumberGrain.SelectedValuePath = "Key";
            ComboBox_NumberGrain.DisplayMemberPath = "Value";
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(0, "10"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(1, "20"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(2, "30"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(3, "40"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(4, "50"));
            /*
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(5, "6"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(6, "7"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(7, "8"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(8, "9"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(9, "10"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(10, "11"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(11, "12"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(12, "13"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(13, "14"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(14, "15"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(15, "16"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(16, "17"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(17, "18"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(18, "19"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(19, "20"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(20, "21"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(21, "22"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(22, "23"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(23, "24"));
            ComboBox_NumberGrain.Items.Add(new KeyValuePair<int, string>(24, "25"));
            */

            ComboBox_NumberMeasure.SelectedValuePath = "Key";
            ComboBox_NumberMeasure.DisplayMemberPath = "Value";
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(0, "Short Paddy"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(1, "Long Paddy"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(2, "Jasmine Paddy"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(3, "Long Sticky Paddy"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(4, "Long Parboiled Rice"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(5, "Peak A/D count value"));
            ComboBox_NumberMeasure.Items.Add(new KeyValuePair<int, string>(6, "Wheat"));

        }

        public void RunSensor()
        {

            int TimeInterval = ComboBox_TimeInterval.SelectedIndex;
            int NumberGrain = ComboBox_NumberGrain.SelectedIndex;
            int NumberMeasure = ComboBox_NumberMeasure.SelectedIndex;

            int delay;
            switch (TimeInterval)
            {
                default:
                    delay = 3000;
                    break;
                case 0:
                    delay = 3000;
                    break;
                case 1:
                    delay = 6000;
                    break;
                case 2:
                    delay = 9000;
                    break;
            }

            string ResultGrain;
            switch (NumberGrain)
            {
                case 0:
                    ResultGrain = "10192\r";
                    NumberGrain_Frekuensi = 10;
                    break;
                case 1:
                    ResultGrain = "10293\r";
                    NumberGrain_Frekuensi = 20;
                    break;
                case 2:
                    ResultGrain = "10394\r";
                    NumberGrain_Frekuensi = 30;
                    break;
                case 3:
                    ResultGrain = "10495\r";
                    NumberGrain_Frekuensi = 40;
                    break;
                case 4:
                    ResultGrain = "10596\r";
                    NumberGrain_Frekuensi = 50;
                    break;
                default:
                    ResultGrain = "10697\r";
                    NumberGrain_Frekuensi = 60;
                    break;

            }

            string ResultMeasure = "";
            switch (NumberMeasure)
            {
                case -1:
                    ResultMeasure = "22094\r";
                    break;
                case 0:
                    ResultMeasure = "22094\r";
                    break;
                case 1:
                    ResultMeasure = "32095\r";
                    break;
                case 2:
                    ResultMeasure = "42096\r";
                    break;
                case 3:
                    ResultMeasure = "52097\r";
                    break;
                case 4:
                    ResultMeasure = "62098\r";
                    break;
                case 5:
                    ResultMeasure = "72094\r";
                    break;
                case 6:
                    ResultMeasure = "8209A\r";
                    break;
                default:
                    ResultMeasure = "22094\r";
                    break;
            }

            if (counter_interval > 0 && StatusListen == false)
            {

                
                counter_interval = counter_interval - 1;

                if (counter_interval == 0)
                {
                    MessageBox.Show("All Measurement finish", application_name);
                    if (temp_data_finals_2.Count > 0)
                    {
                        //data_finals_ori.AddRange(temp_data_finals);

                        data_finals_update.AddRange(temp_data_finals_2);
                        data_finals_update_2.AddRange(temp_data_finals_2);

                        temp_data_finals_2.Clear();

                    }
                    //OpenCon_Port_local(mySerialPort, BaudRate);

                    //mySerialPort.Close();
                }
                else
                {
                    Task.Delay(delay).ContinueWith(_ =>
                    {
                        OpenCon_Port_local(mySerialPort, BaudRate);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);
                        StatusListen = true;

                        MessageBox.Show("Start Next Sequence", application_name);
                    }
);

                    //Thread.Sleep(delay);
                    

                }

            }



        }
        private void btn_Check_click(object sender, RoutedEventArgs e)
        {
            Sensor_input_Helper.Command_Check(mySerialPort);
        }

        private void btn_CheckData_click(object sender, RoutedEventArgs e)
        {
            Sensor_input_Helper.Command_CheckData(mySerialPort);
        }

        private void btn_Stop_click(object sender, RoutedEventArgs e)
        {
            Sensor_input_Helper.Command_Stop(mySerialPort);
        }
        private void btn_NumberGrain_click(object sender, RoutedEventArgs e)
        {
            int NumberofGrain = ComboBox_NumberGrain.SelectedIndex;
            //int test = ((MainWindow)Application.).ComboBox_Port1.Text;
            //int NumberMeasure = ComboBox_NumberMeasure.SelectedIndex;

            string result = "";


            //Sensor_input_Helper.Command_NumberofGrain(mySerialPort, result);
            // Sensor_input_Helper.Command_MoistureMeasure(mySerialPort, result);

        }
        private void btn_MoistureAgg_click(object sender, RoutedEventArgs e)
        {
            if (!mySerialPort.IsOpen)
            {
                OpenCon_Port_local(mySerialPort, BaudRate);
            }
            Sensor_input_Helper.Command_MoisturAggregate(mySerialPort);

        }

        private void btn_MoistureMeasure_click(object sender, RoutedEventArgs e)
        {
            JumlahInterval = ComboBox_NumberInterval.SelectedIndex + 1;
            counter_interval = JumlahInterval;

            TimeInterval = ComboBox_TimeInterval.SelectedIndex;
            NumberGrain = ComboBox_NumberGrain.SelectedIndex;
            NumberMeasure = ComboBox_NumberMeasure.SelectedIndex;


            string ResultGrain;
            switch (NumberGrain)
            {
                case 0:
                    ResultGrain = "10192\r";
                    NumberGrain_Frekuensi = 10;
                    break;
                case 1:
                    ResultGrain = "10293\r";
                    NumberGrain_Frekuensi = 20;
                    break;
                case 2:
                    ResultGrain = "10394\r";
                    NumberGrain_Frekuensi = 30;
                    break;
                case 3:
                    ResultGrain = "10495\r";
                    NumberGrain_Frekuensi = 40;
                    break;
                case 4:
                    ResultGrain = "10596\r";
                    NumberGrain_Frekuensi = 50;
                    break;
                default:
                    ResultGrain = "10697\r";
                    NumberGrain_Frekuensi = 60;
                    break;

            }

            string ResultMeasure = "";
            switch (NumberMeasure)
            {
                case -1:
                    ResultMeasure = "22094\r";
                    break;
                case 0:
                    ResultMeasure = "22094\r";
                    break;
                case 1:
                    ResultMeasure = "32095\r";
                    break;
                case 2:
                    ResultMeasure = "42096\r";
                    break;
                case 3:
                    ResultMeasure = "52097\r";
                    break;
                case 4:
                    ResultMeasure = "62098\r";
                    break;
                case 5:
                    ResultMeasure = "72094\r";
                    break;
                case 6:
                    ResultMeasure = "8209A\r";
                    break;
                default:
                    ResultMeasure = "22094\r";
                    break;
            }

            if (JumlahInterval < 0 || TimeInterval < 0 || NumberGrain < 0 || NumberMeasure < 0)
            {
                MessageBox.Show("Please fill All mandatory value", application_name);
            }

            else
            {
                ComboBox_NumberInterval.IsEnabled = false;
                ComboBox_TimeInterval.IsEnabled = false;
                ComboBox_NumberGrain.IsEnabled = false;
                ComboBox_NumberMeasure.IsEnabled = false;

                Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                //mySerialPort.DiscardOutBuffer();
                //mySerialPort.DiscardInBuffer();

                Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);
                MessageBox.Show("Sensor Start Collecting Data", application_name);

                StatusListen = true;
            }
            //RunSensor();

        }
        public void OpenCon_Port_local(SerialPort mySerialPort, int BaudRate)
        {
            //SerialPort SerialPort = new SerialPort(PortName);
            mySerialPort.BaudRate = BaudRate;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.ReadBufferSize = 2000000;
            mySerialPort.Encoding = ASCIIEncoding.ASCII;
            mySerialPort.Encoding = ASCIIEncoding.UTF8;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(ProcessSensorData_local);

            mySerialPort.Open();
            Application.Current.Dispatcher.Invoke(new Action(() => {
                MessageBox.Show("Port is opened.", application_name);
            }));
            //Application.Run();

        }

        public void ProcessSensorData_local(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                Thread.Sleep(100);// this solves the problem
                byte[] readBuffer = new byte[mySerialPort.ReadBufferSize];
                int readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                string readStr = string.Empty;

                readStr = Encoding.Default.GetString(readBuffer, 0, readLen);
                readStr = readStr.Trim();
                string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", "<CR>", "<LF>" };
                foreach (string s in charactersToReplace)
                {
                    readStr = readStr.Replace(s, "");
                }
                //readStr = Regex.Replace(readStr, "[^0-9.]", "");
                readStr = String.Concat(readStr.Substring(0, readStr.Length - 1), ".", readStr.Substring(readStr.Length - 1, 1));
                //MyString.Substring(MyString.Length-6);
                //return readStr;number 

                if (readStr != "" && readStr != null && !readStr.Trim().ToLower().Contains("r") && StatusListen == true)
                {
                    if (temp_data_finals_2.Count > 0)
                    {
                        //data_finals_ori.AddRange(temp_data_finals);

                        data_finals_update.AddRange(temp_data_finals_2);
                        data_finals_update_2.AddRange(temp_data_finals_2);

                        temp_data_finals_2.Clear();
                        Data_Receive_Grid.ItemsSource = data_finals_update;
                        Data_Receive_Grid.ItemsSource = data_finals_update_2;
                    }
                    Console.WriteLine("Else if: " + readStr);

                    data_measure_2 data_final_update = new data_measure_2(counter + 1, readStr, (DateTime.Now).ToString());
                    // data_measure_2 data_final_update2 = new data_measure_2(counter + 1, readStr, (DateTime.Now).ToString());

                    Data_Measure data_final_ori = new Data_Measure(counter + 1, readStr, (DateTime.Now));

                    data_finals_update.Add(data_final_update);
                    data_finals_update_2.Add(data_final_update);

                    data_finals_ori.Add(data_final_ori);
                    //Data_Receive_Grid.ItemsSource = data_finals_ori;
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        //MessageBox.Show("Port is opened. Start Collecting Data", application_name);
                        //this.DataContext = this;
                        Data_Receive_Grid.ItemsSource = data_finals_update;
                        Data_Receive_Grid.ItemsSource = data_finals_update_2;

                    }));


                }
                else if (readStr.Trim().ToLower().Contains("r") && data_finals_update.Count() > 0 && StatusListen == true)//&& data_finals_ori.Count() % NumberGrain_Frekuensi == 0
                {

                    Sensor_input_Helper.Command_MoisturAggregate(mySerialPort);
                    Thread.Sleep(3000);

                    mySerialPort.Close();

                    //temp_data_finals = data_finals_ori.;
                    temp_data_finals_2.AddRange(data_finals_update);


                    data_finals_ori.Clear();
                    data_finals_update.Clear();
                    data_finals_update_2.Clear();

                    Console.WriteLine("Else if: " + readStr);

                    StatusListen = false;

                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MessageBox.Show("please wait for interval", application_name);
                        //mySerialPort.DataReceived -= ProcessSensorData_local;
                        //mySerialPort.DataReceived = null;


                        RunSensor();
                        //mySerialPort.DiscardOutBuffer();
                        //mySerialPort.DiscardInBuffer();

                        //mySerialPort.DataReceived += new SerialDataReceivedEventHandler(ProcessSensorData_local);

                    }));

                }
                else
                {
                    Console.WriteLine("Else: " + readStr);

                }

            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Console.WriteLine(ex);
                //return "";
            }

        }

        private class YourCollection : ObservableCollection<MyObject>
        {
            // some wrapper functions for example:
            public void Add(string title)
            {
                this.Add(new MyObject { Title = title });
            }
        }
        private class YourCollection_data : ObservableCollection<MyObject>
        {
            // some wrapper functions for example:
            public void Add(string title)
            {
                this.Add(new MyObject { Title = title });
            }
        }

        private void btn_GridPrint_click(object sender, RoutedEventArgs e)
        {
            Data_Receive_Grid.ItemsSource = data_finals_ori;

        }
    }
}
