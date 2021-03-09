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
        int counter_data = 0;

        static string Folder_Path = ConfigurationManager.AppSettings["Folder_Path"] ?? "Not Found";
        static string application_name = ConfigurationManager.AppSettings["application_name"] ?? "Not Found";

        int counter_interval;
        int JumlahInterval;
        int TimeInterval;
        int NumberGrain;
        int NumberMeasure;
        int NumberGrain_Frekuensi = -1;

        bool StatusListen = false;

        List<data_measure_2> data_finals_update = new List<data_measure_2> { };
        List<data_measure_2> data_finals_update_2 = new List<data_measure_2> { };
        List<data_measure_2> temp_data_finals_2 = new List<data_measure_2> { };

        List<data_measure_2> data_Grid_1_list_a = new List<data_measure_2> { };
        List<data_measure_2> data_Grid_1_list_b = new List<data_measure_2> { };

        List<data_measure_2> data_Grid_2_list_a = new List<data_measure_2> { };
        List<data_measure_2> data_Grid_2_list_b = new List<data_measure_2> { };

        List<data_measure_2> data_Grid_3_list_a = new List<data_measure_2> { };
        List<data_measure_2> data_Grid_3_list_b = new List<data_measure_2> { };
        
        List<data_measure_2> data_Grid_4_list_a = new List<data_measure_2> { };
        List<data_measure_2> data_Grid_4_list_b = new List<data_measure_2> { };

        List<data_measure_2> data_Grid_5_list_a = new List<data_measure_2> { };
        List<data_measure_2> data_Grid_5_list_b = new List<data_measure_2> { };

        List<data_measure_2> data_Average = new List<data_measure_2> { };


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
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(0, "30 sec"));
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(1, "60 sec "));
            ComboBox_TimeInterval.Items.Add(new KeyValuePair<int, string>(2, "90 sec"));


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

           
            int NumberGrain = ComboBox_NumberGrain.SelectedIndex;
            int NumberMeasure = ComboBox_NumberMeasure.SelectedIndex;


            int TimeInterval = ComboBox_TimeInterval.SelectedIndex;
            int delay;
            switch (TimeInterval)
            {
                default:
                    delay = 30000;
                    break;
                case 0:
                    delay = 30000;
                    break;
                case 1:
                    delay = 60000;
                    break;
                case 2:
                    delay = 90000;
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

            if (counter_interval > 0)
            {

                
                counter_interval = counter_interval - 1;

                if (counter_interval == 0)
                {
                    MessageBox.Show("All Measurement finish", application_name);
                    /*
                    if (temp_data_finals_2.Count > 0)
                    {
                        //data_finals_ori.AddRange(temp_data_finals);

                        //data_finals_update.AddRange(temp_data_finals_2);
                        data_finals_update_2.AddRange(temp_data_finals_2);

                        temp_data_finals_2.Clear();

                    }
                    */
                    //OpenCon_Port_local(mySerialPort, BaudRate);

                    //mySerialPort.Close();
                }
                else
                {
                    Task.Delay(delay).ContinueWith(_ =>
                    {
                        //OpenCon_Port_local(mySerialPort, BaudRate);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);
                        //StatusListen = true;

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

            //TimeInterval = ComboBox_TimeInterval.SelectedIndex;
            int delay;
            switch (TimeInterval)
            {
                default:
                    delay = 3000;
                    break;
                case 0:
                    delay = 30000;
                    break;
                case 1:
                    delay = 60000;
                    break;
                case 2:
                    delay = 90000;
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
            //Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
            //Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);

            /*
            while (data_finals_update.Count() <= (JumlahInterval * NumberGrain_Frekuensi) && data_Average.Count() <= JumlahInterval )
            {

                if (data_finals_update.Count % NumberGrain_Frekuensi == 0 && data_finals_update_2.Count()> 0)
                {
                    Task.Delay(delay).ContinueWith(_ =>
                    {
                        //OpenCon_Port_local(mySerialPort, BaudRate);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultGrain);
                        Sensor_input_Helper.Command_Write(mySerialPort, ResultMeasure);
                        StatusListen = true;

                        MessageBox.Show("Start Next Sequence", application_name);
                    });

                }

            }
            */
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
            //mySerialPort.Encoding = ASCIIEncoding.ASCII;
            mySerialPort.Encoding = ASCIIEncoding.UTF8;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(ProcessSensorData_local);

            mySerialPort.Open();
            Application.Current.Dispatcher.Invoke(new Action(() => {
                MessageBox.Show("Port is opened.", application_name);
            }));
            //Application.Run();

        }
        private List<string> GetWords(string text)
        {
            Regex reg = new Regex("[a-zA-Z0-9]");
            string Word = "";
            char[] ca = text.ToCharArray();
            List<string> characters = new List<string>();
            for (int i = 0; i < ca.Length; i++)
            {
                char c = ca[i];
                if (c > 65535)
                {
                    continue;
                }
                if (char.IsHighSurrogate(c))
                {
                    i++;
                    characters.Add(new string(new[] { c, ca[i] }));
                }
                else
                {
                    if (reg.Match(c.ToString()).Success || c.ToString() == "/")
                    {
                        Word = Word + c.ToString();
                        //characters.Add(new string(new[] { c }));
                    }
                    else if (c.ToString() == " ")
                    {
                        if (Word.Length > 0)
                            characters.Add(Word);
                        Word = "";
                    }
                    else
                    {
                        if (Word.Length > 0)
                            characters.Add(Word);
                        Word = "";
                    }

                }

            }
            return characters;
        }

        public void ProcessSensorData_local(object sender, SerialDataReceivedEventArgs args)
        {
            try
            {
                /*
                 * "\u0002129\u0003A1"
                 * \u0002128\u0003A0\r\u0002122\u00039A\r
                 * \u0002129\u0003A1\r\u0002116\u00039D\r
                 * \u0002125\u00039D\r\u0002123\u00039B"
                 * 1. Split character by Y
                 */
                Thread.Sleep(1000);// this solves the problem
                byte[] readBuffer = new byte[mySerialPort.ReadBufferSize];
                int readLen = mySerialPort.Read(readBuffer, 0, readBuffer.Length);
                string readStr = string.Empty;

                readStr = Encoding.UTF8.GetString(readBuffer, 0, readLen);
                //readStr = Encoding.UTF8.GetString(readBuffer,0,readLen);
                readStr = readStr.Trim();
                Console.WriteLine("ReadStr adalah: " + readStr);

                char[] delimiter_r = { '\r' };
                string[] Measures_With_U = readStr.Split(delimiter_r);

                List<string> Measure_Results = new List<string>();
                List<string> Test_Isengs = new List<string>();

                foreach (var Measure in Measures_With_U)
                {
                    Test_Isengs = GetWords(Measure);
                    string Result_Parsing = GetWords(Measure).FirstOrDefault();
                    Console.WriteLine("Result Parsing adalah: " + Result_Parsing);
                    //Measure_Results.Add(Result_Parsing);

                    string[] charactersToReplace = new string[] { @"\t", @"\n", @"\r", " ", "<CR>", "<LF>" };

                    if (Result_Parsing != "" && Result_Parsing != null)
                    {
                        foreach (string s in charactersToReplace)
                        {
                            Result_Parsing = Result_Parsing.Replace(s, "");
                        }
                    }


                    if (Result_Parsing != "" && Result_Parsing != null && !Result_Parsing.Trim().ToLower().Contains("r"))
                    {

                        Console.WriteLine("Nilai Measure adalah: " + Result_Parsing);
                        
                        
                        data_measure_2 data_final_update = new data_measure_2(counter_data + 1, Result_Parsing, (DateTime.Now).ToString());
                        if (Result_Parsing.Contains("-") || (Result_Parsing.Length) >4)
                        {
                            data_Average.Add(data_final_update);
                        }
                        else
                        {
                            data_finals_update.Add(data_final_update);
                            data_finals_update_2.Add(data_final_update);

                            //var result = from db.MyTable.Where(d => (double)d.Price >= minValue && (double)d.Price <= maxValue)

                            var data_Grid_1 = data_finals_update.Where(p => p.Id > 0 && p.Id <= 1 * NumberGrain_Frekuensi);
                            data_Grid_1_list_a = data_Grid_1.ToList();
                            data_Grid_1_list_b = data_Grid_1.ToList();

                            var data_Grid_2 = data_finals_update.Where(p => p.Id > 1 * NumberGrain_Frekuensi && p.Id <= 2 * NumberGrain_Frekuensi);
                            data_Grid_2_list_a = data_Grid_2.ToList();
                            data_Grid_2_list_b = data_Grid_2.ToList();

                            var data_Grid_3 = data_finals_update.Where(p => p.Id > 2 * NumberGrain_Frekuensi && p.Id <= 3 * NumberGrain_Frekuensi);
                            data_Grid_3_list_a = data_Grid_3.ToList();
                            data_Grid_3_list_b = data_Grid_3.ToList();

                            var data_Grid_4 = data_finals_update.Where(p => p.Id > 3 * NumberGrain_Frekuensi && p.Id <= 4 * NumberGrain_Frekuensi);
                            data_Grid_4_list_a = data_Grid_4.ToList();
                            data_Grid_4_list_b = data_Grid_4.ToList();

                            var data_Grid_5 = data_finals_update.Where(p => p.Id > 4 * NumberGrain_Frekuensi && p.Id <= 5 * NumberGrain_Frekuensi);
                            data_Grid_5_list_a = data_Grid_5.ToList();
                            data_Grid_5_list_b = data_Grid_5.ToList();

                            //data_pdfhistories = (data_pdfhistories_var.OrderBy(p => p.Id)).ToList();

                            //data_finals_ori.Add(data_final_ori);
                            //Data_Receive_Grid.ItemsSource = data_finals_ori;
                            
                        }
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            //MessageBox.Show("Port is opened. Start Collecting Data", application_name);
                            //this.DataContext = this;
                            Data_Receive_Grid_1.ItemsSource = data_Grid_1_list_a;
                            Data_Receive_Grid_1.ItemsSource = data_Grid_1_list_b;

                            Data_Receive_Grid_2.ItemsSource = data_Grid_2_list_a;
                            Data_Receive_Grid_2.ItemsSource = data_Grid_2_list_b;

                            Data_Receive_Grid_3.ItemsSource = data_Grid_3_list_a;
                            Data_Receive_Grid_3.ItemsSource = data_Grid_3_list_b;

                            //var x = data_Average[0] != null ? 12 : (int?)null;
                            //if (val % 2 == 1) { output = “Number is odd”; } else { output = “Number is even”; }
                            if (data_Average.Count() == 1) { Average_1.Text = data_Average[0].Measures; } else {; }
                            if (data_Average.Count() == 2) { Average_2.Text = data_Average[1].Measures; } else {; }
                            if (data_Average.Count() == 3) { Average_3.Text = data_Average[2].Measures; } else {; }
                            if (data_Average.Count() == 4) { Average_4.Text = data_Average[3].Measures; } else {; }

                        }));
                        counter_data = counter_data + 1;
                    }

                    
                    else if (data_finals_update.Count % NumberGrain_Frekuensi == 0 && data_finals_update.Count > 0)
                    {
                        Sensor_input_Helper.Command_MoisturAggregate(mySerialPort);
                        //mySerialPort.Close();
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            //MessageBox.Show("Port is opened. Start Collecting Data", application_name);
                            //this.DataContext = this;
                            RunSensor();
                        }));
                        
                        Console.WriteLine("Nilai Else If adalah: " + Result_Parsing);
                    }
                    
                    else
                    {
                        Console.WriteLine("Nilai Else adalah: " + Result_Parsing);
                    }


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
            //Data_Receive_Grid.ItemsSource = data_finals_ori;

        }
    }
}
