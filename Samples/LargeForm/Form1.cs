using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using WAConverter;

namespace WinFormsSampleApp1
{
    public partial class Form1 : WAForm {
        private string SerialPortWasOpen = "";
        private bool SerialPortState = false;
        private int ByteCounter = 0;
        private int ByteCounterOld = 0;
        private int FailCounter = 0;
        private string FileForSavingData = "";
        private int StartFramesQuantity = 0;
        private int NewBaudrateForFastCommunication = 0;
        private string DefaultBaudrate = "960";
        private string DefaultBaudrateH = "03C0";
        private int FlagReconfigBaudrate = 0;
        private int TimerDefaultInterval = 200;
        private int TimerHighSpeedInterval = 100;
        private int FlagToAnswerMain = 10;
        private int TimerSpeedReconfigInterval = 2000;

        byte[] DumpReceiveDataBuffer; //dump
        int DumpReceiveDataCounter = 0;
        MemoryStream DumpMemoryStream;

        private void FailProcess() {
            FlagReconfigBaudrate = 0;
            SetProgressBar(100, Color.Red);
            Timer.Stop();
        }
        private bool EndOfMessageDetector(ref bool StateOp) {
            if(ByteCounter > 3) {
                if(FlagToAnswerMain < 100) {
                    if(ByteCounter == ByteCounterOld) {
                        StateOp = false;
                        return false;
                    } else {
                        ByteCounterOld = ByteCounter;
                    }
                } else {
                    ByteCounterOld = ByteCounter;
                    if(ByteCounter == 8454 || ByteCounter == 8455) {
                        StateOp = false;
                        return false;
                    }
                }
            }
            if(--FailCounter == 0) {
                FailProcess();
                StateOp = true;
                return true;
            } else {
                StateOp = true;
                return false;
            }
        }
        AlgorithmType Algorithm = new AlgorithmType();
        private void SerialConfig(string PName, int BaudR = 9600, StopBits Stb = StopBits.Two, Parity Prt = Parity.None) {
            SerialPort.BaudRate = BaudR;
            SerialPort.PortName = PName;
            SerialPort.Parity = Prt;
            SerialPort.StopBits = Stb;
        }
        private void SerialClearAndSend(int FCounter = 30) // default 3 sec timeout
        {
            SerialPort.DiscardOutBuffer();
            SerialPort.DiscardInBuffer();
            SerialPort.Write(Algorithm.TransmitData, 0, Algorithm.TransmitDataLength);
            Algorithm.ReceiveDataLength = 0;
            FailCounter = FCounter;
        }
        private void SetStateOfConnect(string s, Color cl) {
            StateOfConnect.ForeColor = cl;
            StateOfConnect.Text = s;
        }
        private void SetProgressBar(byte ProgressLevel, Color ProgressColor) {
            DataManagerBar.ForeColor = ProgressColor;
            DataManagerBar.Value = ProgressLevel;
        }
        private void InitCommand() {
            if(!SerialPortState) return;
            Algorithm.SetAddress((byte)ModbusAddr.Value);
            Algorithm.ClearNextCommandAndUsefulData();
            Algorithm.SetStep("NEED_TO_TRANSMIT");
            SetProgressBar(20, Color.Green);
            Algorithm.CommandSequencePointer = 0;
            Timer.Interval = TimerDefaultInterval;
            Timer.Start();
        }
        private int ToIntConverter(string s, ref int Index, int digits = 4) {
            string tmp = "";
            for(int i = 0; i < digits; i++) {
                tmp += s[Index++];
            }
            return Convert.ToInt32(tmp);
        }
        private void GuiSetParams(string HeaderString, string info) {
            int indx = 0;
            string s1 = "";

            int day = 0;
            int dayOfWeek = 0;
            int month = 0;
            int year = 0;

            switch(HeaderString) {
                case "GETTING_DATA_CONTROL":
                    BittQuantity.Value = ToIntConverter(info, ref indx);
                    Modbus_1.Value = ToIntConverter(info, ref indx);
                    Modbus_2.Value = ToIntConverter(info, ref indx);
                    Modbus_3.Value = ToIntConverter(info, ref indx);
                    Modbus_4.Value = ToIntConverter(info, ref indx);
                    Modbus_5.Value = ToIntConverter(info, ref indx);
                    Modbus_6.Value = ToIntConverter(info, ref indx);
                    break;
                case "GETTING_DATE_TIME":
                    Hour.Value = ToIntConverter(info, ref indx, 2);
                    Minute.Value = ToIntConverter(info, ref indx, 2);
                    Second.Value = ToIntConverter(info, ref indx, 2);
                    day = ToIntConverter(info, ref indx, 2);
                    dayOfWeek = ToIntConverter(info, ref indx, 2) - 1;
                    month = ToIntConverter(info, ref indx, 2);
                    year = 2000 + ToIntConverter(info, ref indx, 2);
                    DateTime projectStart = new DateTime(year, month, day);
                    DateTime projectEnd = new DateTime(year, month, day);
                    Calendar.SelectionRange = new SelectionRange(projectStart, projectEnd);
                    
                    break;
                case "COMMON_INFO":
                    Serial.Text = ToIntConverter(info, ref indx, 5).ToString();
                    SoftVersion.Text = ToIntConverter(info, ref indx, 4).ToString() + ".";
                    SoftVersion.Text += ToIntConverter(info, ref indx, 1).ToString();
                    UPower.Text = ToIntConverter(info, ref indx, 4).ToString() + ",";
                    UPower.Text += ToIntConverter(info, ref indx, 1).ToString();
                    InternalTemp.Text = ToIntConverter(info, ref indx, 5).ToString();
                    U_USB.Text = ToIntConverter(info, ref indx, 4).ToString() + ",";
                    U_USB.Text += ToIntConverter(info, ref indx, 1).ToString();
                    s1 = (65536 * ToIntConverter(info, ref indx, 5)).ToString();
                    TimeOfService.Text = "";
                    if(s1 != "0") TimeOfService.Text = s1;
                    TimeOfService.Text += ToIntConverter(info, ref indx, 4).ToString() + ",";
                    TimeOfService.Text += ToIntConverter(info, ref indx, 1).ToString();
                    StateSetting.Text = ToIntConverter(info, ref indx, 5).ToString();
                    break;
                case "RECORD_INFO":
                    PagePositionInFlash.Text = ToIntConverter(info, ref indx, 5).ToString();
                    RecPositionInFlash.Text = ToIntConverter(info, ref indx, 5).ToString();
                    break;
                case "GETTING_FIRST_FRAME_INDEX":
                    FirstFrame.Value = ToIntConverter(info, ref indx, 5);
                    break;
            }
        }

        public Form1() {
            InitializeComponent();
            this.NewBaudrate.SelectedItem = "57600";
            NewBaudrateForFastCommunication = 5760;
            string[] ports = SerialPort.GetPortNames();
            IEnumerable<string> PortsSort = from port in ports
                                            orderby port
                                            select port;
            if(ports.Length == 0) {
                SetStateOfConnect("Порты отсутствуют!", Color.Red);
                StateOfConnect.Enabled = false;
            } else {
                foreach(string s in PortsSort) {
                    COMPortSelector.Items.Add(s);
                }
            }
            this.EnableEthernet.Enabled = true;
            this.COMPortSelector.Enabled = true;
            this.IpAddress.Enabled = false;
            this.EthernetPort.Enabled = false;
            this.CreateConnection.Enabled = false;
            DumpReceiveDataBuffer = new byte[400000];

        }

        private void COMPortSelector_SelectedIndexChanged(object sender, EventArgs e) {
            string cmpsel = COMPortSelector.Items[COMPortSelector.SelectedIndex].ToString();

            if(SerialPortState)
                try {
                    SerialPort.Close();
                } catch(Exception) {
                    SetStateOfConnect("Откройте порт еще раз!", Color.Red);
                    SerialPortWasOpen = "";
                    SerialPortState = false;
                    return;
                }

            SerialConfig(cmpsel);
            try {
                SerialPort.Open();
            } catch(Exception) {
                SetStateOfConnect("Невозможно открыть порт!", Color.Red);
                SerialPortWasOpen = "";
                SerialPortState = false;
                return;
            }
            SetStateOfConnect("Порт открыт!", Color.Green);
            SerialPortState = true;
            SerialPortWasOpen = cmpsel;
            SerialPort.DiscardOutBuffer();
            SerialPort.DiscardInBuffer();

            SerialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            if(Algorithm.CommandSequence[Algorithm.CommandSequencePointer] != "GETTING_MEMORY_DUMP") return;
            int bytesReceived = SerialPort.BytesToRead;
            SerialPort.Read(DumpReceiveDataBuffer, 0, bytesReceived);
            DumpReceiveDataCounter += bytesReceived;
            DumpMemoryStream.Write(DumpReceiveDataBuffer, 0, bytesReceived);
        }
        private void CreateConnection_Click(object sender, EventArgs e) {
            //List<char> address = (this.IpAddress.Text + ".").ToList();
            //List<byte> byteAddress = new List<byte>();
            //string s = "";
            //try {
            //    foreach(char symbol in address) {
            //        if(symbol != '.') {
            //            s += symbol;
            //        } else {
            //            byteAddress.Add(Convert.ToByte(s));
            //            s = "";
            //        }
            //    }
            //    System.Net.IPAddress ipAddress = new System.Net.IPAddress(byteAddress.ToArray());
            //    int port = Convert.ToInt32(this.EthernetPort.Text);
            //    TcpClient = new TcpClient();
            //    TcpClient.Connect(ipAddress, port);
            //    TcpClient.ReceiveBufferSize = 16384;
            //    TcpClient.SendBufferSize = 1024;
            //    //TcpClient.Connect("www.rambler.ru", 80);
            //    int i = 0;
            //    do {
            //        Thread.Sleep(100);
            //    } while(TcpClient.Connected || ++i >= 15);
            //    if(i <= 15) {
            //        TcpNetworkStream = TcpClient.GetStream();
            //        TcpTemporaryData = new List<byte>();
            //        SetStateOfConnect("Соединение установлено!", Color.Green);
            //        return;
            //    }
            //} catch(Exception) { }
            //SetStateOfConnect("Ошибка соединения!", Color.Red);
        }
        private void EnableEthernet_CheckedChanged(object sender, EventArgs e) {
            //if(this.EnableEthernet.Checked == true) {
            //    this.COMPortSelector.Enabled = false;
            //    this.IpAddress.Enabled = true;
            //    this.EthernetPort.Enabled = true;
            //    this.CreateConnection.Enabled = true;
            //} else {
            //    this.COMPortSelector.Enabled = true;
            //    this.IpAddress.Enabled = false;
            //    this.EthernetPort.Enabled = false;
            //    this.CreateConnection.Enabled = false;
            //}
        }
        private void SerialSpeedReconfig(string newSpeed) {
            int speed = Convert.ToInt32(newSpeed);

            string cmpsel = COMPortSelector.Items[COMPortSelector.SelectedIndex].ToString();
            try {
                SerialPort.BaudRate = 10 * speed;
            } catch(Exception) {
                SetStateOfConnect("Ошибка изменения скорости!", Color.Red);
                SerialPortWasOpen = "";
                SerialPortState = false;
                return;
            }
        }
        private void CreateFile_Click(object sender, EventArgs e) {
            string Path = FileName.Text;
            if(Path == "") {
                DateTime DTNow = DateTime.Now;
                Path = "Data_MB" + ((int)ModbusAddr.Value).ToString() + "_" + ((int)DTNow.Hour).ToString("D2")
                    + "_" + ((int)DTNow.Minute).ToString("D2") + "_" + ((int)DTNow.Second).ToString("D2") + ".txt";
                FileName.Text = FileForSavingData = Path;
            }
            try {
                if(!File.Exists(@Path)) {
                    var FileTmp = File.Create(@Path);
                    FileTmp.Close();
                    SetProgressBar(100, Color.Green);
                } else {
                    SetProgressBar(100, Color.Red);
                }
            } catch(Exception) {
                SetProgressBar(100, Color.Red);
                FileForSavingData = FileName.Text = "";
            }
        }
        private void ReadConfig_Click(object sender, EventArgs e) {
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("GETTING_DATA_CONTROL");

        }
        private void WriteConfig_Click(object sender, EventArgs e) {
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("SETTING_CODE_PROTECT", "0fed");
            Algorithm.AddNextCommandAndUsefulData("SETTING_DATA_CONTROL", ((int)BittQuantity.Value).ToString("X4") + ((int)Modbus_1.Value).ToString("X4")
                        + ((int)Modbus_2.Value).ToString("X4") + ((int)Modbus_3.Value).ToString("X4") + ((int)Modbus_4.Value).ToString("X4")
                        + ((int)Modbus_5.Value).ToString("X4") + ((int)Modbus_6.Value).ToString("X4"));
        }
        private void ReadOClock_Click(object sender, EventArgs e) {
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("GETTING_DATE_TIME");
        }
        private void WriteOClock_Click(object sender, EventArgs e) {
            if(!SetSystemTimeCheckbox.Checked) {
                DateTime date = Calendar.SelectionStart;
                Algorithm.GetGuiDateTime((int)Hour.Value, (int)Minute.Value, (int)Second.Value,
                    date.Day, Algorithm.DayOfWeekGetIndex(date), date.Month, (date.Year > 2013) ? (date.Year - 2000) : 14);
            } else {
                Algorithm.GetSystemDateTime();
                Hour.Value = Algorithm.MomentDateTime.Hour;
                Minute.Value = Algorithm.MomentDateTime.Minute;
                Second.Value = Algorithm.MomentDateTime.Second;
                DateTime projectStart = new DateTime(Algorithm.MomentDateTime.Year, Algorithm.MomentDateTime.Month,
                    Algorithm.MomentDateTime.Day);
                DateTime projectEnd = new DateTime(Algorithm.MomentDateTime.Year, Algorithm.MomentDateTime.Month,
                    Algorithm.MomentDateTime.Day);
                Calendar.SelectionRange = new SelectionRange(projectStart, projectEnd);
            }

            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("SETTING_CODE_PROTECT", "0fed");
            Algorithm.AddNextCommandAndUsefulData("SETTING_DATE_TIME", Algorithm.DateTimeToString());
        }
        private void ReadDiag_Click(object sender, EventArgs e) {
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("COMMON_INFO");
            Algorithm.AddNextCommandAndUsefulData("RECORD_INFO");
        }
        private void NewBaudrate_SelectedIndexChanged(object sender, EventArgs e) {
            NewBaudrateForFastCommunication = Convert.ToInt32((string)NewBaudrate.Items[NewBaudrate.SelectedIndex]) / 10;
        }
        private void FileWriteData(string HeaderString) {
            if(HeaderString != "GETTING_FRAME") return;
            try {
                using(StreamWriter sw = File.AppendText(FileName.Text)) {
                    sw.Write(Algorithm.ReceiveDataStr);
                }

            } catch(Exception) {
                SetProgressBar(100, Color.Red);
                return;
            }
        }
        private void ReadData_Click(object sender, EventArgs e) {
            if(FramesQuantity.Value == 0) FramesQuantity.Value = 1;
            StartFramesQuantity = (int)FramesQuantity.Value;
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("SETTING_FIRST_FRAME_INDEX", ((int)FirstFrame.Value).ToString("X4"));
            Algorithm.AddNextCommandAndUsefulData("SETTING_NEW_BAUDRATE", NewBaudrateForFastCommunication.ToString("X4"));
            Algorithm.AddNextCommandAndUsefulData("GETTING_FRAME");
            Algorithm.AddNextCommandAndUsefulData("SETTING_NEW_BAUDRATE", DefaultBaudrateH);
            FlagReconfigBaudrate = 1;
        }

        private void ReadDump_Click(object sender, EventArgs e) { //dump
            if(DumpMemoryStream != null) {
                DumpMemoryStream.Close();
                DumpMemoryStream.Dispose();
            }
            DumpMemoryStream = new MemoryStream();
            DumpReceiveDataCounter = 0;
            InitCommand();
            Algorithm.AddNextCommandAndUsefulData("GETTING_MEMORY_DUMP");
        }

        private void Timer_Tick(object sender, EventArgs e) {
            bool StateOp = false;
            if(Algorithm.GetStep() == "NEED_TO_TRANSMIT") {
                Algorithm.ModbusRWMessageCreator();
                switch(Algorithm.CommandSequence[Algorithm.CommandSequencePointer]) {
                    case "GETTING_FRAME": SerialClearAndSend(150);
                        break;
                    case "GETTING_MEMORY_DUMP": SerialClearAndSend(350);
                        break;
                    default: SerialClearAndSend(30);
                        break;
                }
                Algorithm.SetStep("NEED_TO_RECEIVE");
                if(Algorithm.CommandSequence[Algorithm.CommandSequencePointer] == "GETTING_MEMORY_DUMP") { //dump
                    Timer.Interval = 500; // 500 ms timer for memory dump reading
                }
            } else if(Algorithm.GetStep() == "NEED_TO_RECEIVE") {
                ByteCounter = (Algorithm.CommandSequence[Algorithm.CommandSequencePointer] == "GETTING_MEMORY_DUMP") // dump
                    ? DumpReceiveDataCounter : SerialPort.BytesToRead;
                if(Algorithm.CommandSequence[Algorithm.CommandSequencePointer] == "GETTING_MEMORY_DUMP") // dump
                    DumpByteCounter.Value = DumpReceiveDataCounter;
                if(EndOfMessageDetector(ref StateOp)) {
                    if(StateOp) // Fail Process (timeout)!!
                    {
                        FailProcess();
                    }
                } else {
                    if(!StateOp) {
                        Timer.Stop(); // receive process
                        if(Algorithm.CommandSequence[Algorithm.CommandSequencePointer] == "GETTING_MEMORY_DUMP") { // dump
                            try {
                                using(StreamWriter sw = File.AppendText(FileName.Text)) {
                                    DumpMemoryStream.Seek(0, SeekOrigin.Begin);
                                    for(int i = 0; i < DumpMemoryStream.Length; i++) {
                                        sw.Write(DumpMemoryStream.ReadByte().ToString("X2").ToUpper());
                                    }
                                }
                            } catch(Exception) {
                                SetProgressBar(100, Color.Red);
                                return;
                            }
                            SetProgressBar(100, Color.Green);
                            return;
                        }
                        SerialPort.Read(Algorithm.ReceiveData, 0, ByteCounter);
                        Algorithm.ReceiveDataLength = ByteCounter;
                        if(Algorithm.TestCommand()) {
                            FailProcess();
                            return;
                        } else {
                            GuiSetParams(Algorithm.CommandSequence[Algorithm.CommandSequencePointer], Algorithm.ExecuteCommand());
                            FileWriteData(Algorithm.CommandSequence[Algorithm.CommandSequencePointer]);
                            switch(Algorithm.CommandSequence[Algorithm.CommandSequencePointer]) {
                                case "SETTING_NEW_BAUDRATE":
                                    switch(FlagReconfigBaudrate) {
                                        case 1:
                                            SerialSpeedReconfig(NewBaudrateForFastCommunication.ToString("D4"));
                                            FlagReconfigBaudrate = 2;
                                            break;
                                        case 2:
                                            SerialSpeedReconfig(DefaultBaudrate);
                                            FlagReconfigBaudrate = 0;
                                            break;
                                    }
                                    break;
                                case "GETTING_FRAME":
                                    if(FramesQuantity.Value > 0) {
                                        FramesQuantity.Value--;
                                        if(FramesQuantity.Value > 0) {
                                            SetProgressBar((byte)(100.0 * (1 - (float)FramesQuantity.Value / StartFramesQuantity)), Color.Green);
                                            Algorithm.SetStep("NEED_TO_TRANSMIT");
                                            FlagToAnswerMain = 8455;
                                            Timer.Interval = TimerHighSpeedInterval;
                                            Timer.Start();
                                            return;
                                        }
                                    }
                                    break;
                            }
                            if(Algorithm.CommandSequencePointer < (Algorithm.CommandSequence.Count() - 1)) {
                                int interval = (Algorithm.CommandSequence[Algorithm.CommandSequencePointer] == "SETTING_NEW_BAUDRATE") ? TimerSpeedReconfigInterval : TimerDefaultInterval;
                                Algorithm.CommandSequencePointer++;
                                SetProgressBar((byte)(100.0 * Algorithm.CommandSequencePointer / Algorithm.CommandSequence.Count()), Color.Green);
                                Algorithm.SetStep("NEED_TO_TRANSMIT");

                                FlagToAnswerMain = 10;

                                Timer.Interval = interval;
                                Timer.Start();
                                return;
                            }
                        }
                        SetProgressBar(100, Color.Green);
                        Timer.Interval = TimerDefaultInterval;
                    }
                }
            }
        }
    }
}


