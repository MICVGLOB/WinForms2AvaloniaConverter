using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsSampleApp1
{
    class MomentDateTimeType {
        public int Hour;
        public int Minute;
        public int Second;
        public int Day;
        public int DayOfWeek;
        public int Month;
        public int Year;
    };

    class AlgorithmType {
        private byte Address;
        public string Step; // NEED_TO_RECEIVE or NEED_TO_TRANSMIT
        public int CommandSequencePointer;

        public MomentDateTimeType MomentDateTime = new MomentDateTimeType();
        public byte[] TransmitData;
        public int TransmitDataLength;
        public byte[] ReceiveData;
        public string ReceiveDataStr;

        public int ReceiveDataLength;

        public struct CommandTemplate {
            public string HeaderString;
            public string DataToSend;
            public int ByteToAnswer;
            public string HeaderOfAnswer;
            public bool NeedToConvertUsefulData;

            public CommandTemplate(string s1, string s2, int BTA, string HOA, bool needToConvertUsefulData = false) {
                HeaderString = s1;
                DataToSend = s2;
                ByteToAnswer = BTA;
                HeaderOfAnswer = HOA;
                NeedToConvertUsefulData = needToConvertUsefulData;

            }
        }

        public List<CommandTemplate> CommandList = new List<CommandTemplate>();
        public List<string> CommandSequence = new List<string>();
        public List<string> UsefulData = new List<string>();

        public void CommandListInit() {
            CommandList.Add(new CommandTemplate("GETTING_DATA_CONTROL", "03000e0007", 19, "030e"));
            CommandList.Add(new CommandTemplate("SETTING_DATA_CONTROL", "10000e00070e", 8, "10000e0007"));
            CommandList.Add(new CommandTemplate("GETTING_DATE_TIME", "03000d0001", 20, "0302"));
            CommandList.Add(new CommandTemplate("SETTING_CODE_PROTECT", "10000b000102", 8, "10000b0001"));
            CommandList.Add(new CommandTemplate("SETTING_DATE_TIME", "10000d00010e", 8, "10000d0001", true));
            CommandList.Add(new CommandTemplate("COMMON_INFO", "0300000008", 21, "0310"));
            CommandList.Add(new CommandTemplate("RECORD_INFO", "03001c0002", 9, "0304"));
            CommandList.Add(new CommandTemplate("SETTING_NEW_BAUDRATE", "10001b000102", 8, "10001b0001"));
            CommandList.Add(new CommandTemplate("GETTING_FIRST_FRAME_INDEX", "03001e0001", 7, "0302"));
            CommandList.Add(new CommandTemplate("SETTING_FIRST_FRAME_INDEX", "10001e000102", 8, "10001e0001"));
            CommandList.Add(new CommandTemplate("GETTING_FRAME", "03001f0001", (8 * 1056) + 6, "032100"));
            CommandList.Add(new CommandTemplate("GETTING_MEMORY_DUMP", "0300200001", 0, "0000")); // Dummy receive Bytes and Template 
        }

        public byte CommandIdFinder(string Header) {
            for(int i = 0; i < CommandList.Count; i++) {
                if(CommandList[i].HeaderString == Header) {
                    return (byte)i;
                }
            }
            return 0;
        }

        public AlgorithmType() {
            TransmitDataLength = 0;
            ReceiveDataLength = 0;
            TransmitData = new byte[1024];
            ReceiveData = new byte[16384];
            CommandListInit();
            CommandSequencePointer = 0;
            Address = 0;
        }

        public void SetAddress(byte tm) {
            Address = tm;
        }

        public void AddNextCommandAndUsefulData(string tm, string dt = "") {
            CommandSequence.Add(tm);
            UsefulData.Add(dt);
        }
        public void ClearNextCommandAndUsefulData() {
            CommandSequence.Clear();
            UsefulData.Clear();
        }

        public void SetStep(string stp) {
            Step = stp;
        }

        public string GetStep() {
            return Step;
        }

        public void GetGuiDateTime(int hour, int minute, int second,
                                    int dayT, int dayOfWeek, int month, int year) {
            MomentDateTime.Hour = hour;
            MomentDateTime.Minute = minute;
            MomentDateTime.Second = second;
            MomentDateTime.Day = dayT;
            MomentDateTime.DayOfWeek = dayOfWeek;
            MomentDateTime.Month = month;
            MomentDateTime.Year = year;
        }
        public int DayOfWeekGetIndex(DateTime dateTime) {
            switch(dateTime.DayOfWeek) {
                case DayOfWeek.Monday: return 1;
                case DayOfWeek.Tuesday: return 2;
                case DayOfWeek.Wednesday: return 3;
                case DayOfWeek.Thursday: return 4;
                case DayOfWeek.Friday: return 5;
                case DayOfWeek.Saturday: return 6;
                default: return 7;
            }
        }

        public void GetSystemDateTime() {
            DateTime dateTimeNow = DateTime.Now;
            GetGuiDateTime(dateTimeNow.Hour, dateTimeNow.Minute, dateTimeNow.Second,
                         dateTimeNow.Day, DayOfWeekGetIndex(dateTimeNow), dateTimeNow.Month, (dateTimeNow.Year > 2013) ? (dateTimeNow.Year - 2000) : 14);
        }

        public string DateTimeToString() {
            return MomentDateTime.Hour.ToString("D2") + MomentDateTime.Minute.ToString("D2") + MomentDateTime.Second.ToString("D2") +
                 MomentDateTime.Day.ToString("D2") + MomentDateTime.DayOfWeek.ToString("D2") + MomentDateTime.Month.ToString("D2") + MomentDateTime.Year.ToString("D2");
        }

        public int CharToInteger(char c) {
            return "0123456789ABCDEF".IndexOf(char.ToUpper(c));
        }

        public char ByteToChar(byte i) {
            switch(i) {
                case 0x00: return '0';
                case 0x01: return '1';
                case 0x02: return '2';
                case 0x03: return '3';
                case 0x04: return '4';
                case 0x05: return '5';
                case 0x06: return '6';
                case 0x07: return '7';
                case 0x08: return '8';
                case 0x09: return '9';
                case 0x0a: return 'a';
                case 0x0b: return 'b';
                case 0x0c: return 'c';
                case 0x0d: return 'd';
                case 0x0e: return 'e';
                case 0x0f: return 'f';
            }
            return '\r';
        }

        public void BinArrayToStringConverter() // Convert bin to char array
        {
            ReceiveDataStr = "";
            int offset = 4;
            for(int i = offset; i < ReceiveDataLength - 2; i++) {
                ReceiveDataStr += ReceiveData[i].ToString("X2").ToUpper();
            }
            ReceiveDataStr += "\n\n";
        }

        public void ModbusRWMessageCreator() {
            string s = CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].DataToSend;
            string s1 = UsefulData[CommandSequencePointer];
            TransmitData[0] = Address;
            if(!CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].NeedToConvertUsefulData) {
                s += s1;
                for(int i = 0, j = 0; i < s.Length; i += 2, j++) {
                    TransmitData[j + 1] = (byte)(16 * CharToInteger(s[i]) + CharToInteger(s[i + 1]));
                }
                UInt16 tmp = CRC_Calc16(TransmitData, (UInt16)((s.Length / 2) + 1));
                TransmitData[(s.Length / 2) + 1] = (byte)(tmp >> 8);
                TransmitData[(s.Length / 2) + 2] = (byte)(tmp);
                TransmitDataLength = (s.Length / 2) + 3;
            } else {
                int j = 0;
                for(int i = 0; i < s.Length; i += 2) {
                    TransmitData[j + 1] = (byte)(16 * CharToInteger(s[i]) + CharToInteger(s[i + 1]));
                    j++;
                }

                for(int i = 0; i < s1.Length; i++) {
                    TransmitData[j + 1] = (byte)s1[i];
                    j++;
                }
                TransmitData[j + 1] = 0;
                UInt16 tmp = CRC_Calc16(TransmitData, (UInt16)(j + 2));
                TransmitData[j + 2] = (byte)(tmp >> 8);
                TransmitData[j + 3] = (byte)(tmp);
                TransmitDataLength = j + 4;
            }
        }

        public bool TestCommand() {
            if(ReceiveDataLength < 100) {
                if(CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].ByteToAnswer != ReceiveDataLength)
                    return true;
            } else {
                if(!(CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].ByteToAnswer == ReceiveDataLength
                    || CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].ByteToAnswer == (ReceiveDataLength - 1)))
                    return true;
            }
            // fix bug
            if(ReceiveData[0] == 0 || ReceiveData[0] == 0xff) {
                for(int i = 0; i < ReceiveDataLength - 1; i++) {
                    ReceiveData[i] = ReceiveData[i + 1];
                }
                ReceiveDataLength -= 1;
            }
            // fix bug

            UInt16 tmp = (UInt16)(ReceiveData[ReceiveDataLength - 2] * 256 + ReceiveData[ReceiveDataLength - 1]);
            if(tmp != CRC_Calc16(ReceiveData, (UInt16)(ReceiveDataLength - 2))) return true;
            if(ReceiveData[0] != Address) return true;
            string s = CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].HeaderOfAnswer;
            for(int i = 0, j = 1; i < s.Length; i += 2, j++) {
                if((16 * CharToInteger(s[i]) + CharToInteger(s[i + 1])) != ReceiveData[j]) return true;
            }
            return false;
        }


        public string ExecuteCommand() {
            int indx = CommandList[CommandIdFinder(CommandSequence[CommandSequencePointer])].HeaderOfAnswer.Length / 2 + 1;
            string s = "";
            int tmp = 0;
            switch(CommandSequence[CommandSequencePointer]) {
                case "SETTING_DATA_CONTROL":
                case "SETTING_CODE_PROTECT":
                case "SETTING_DATE_TIME":
                case "SETTING_FIRST_FRAME_INDEX":
                case "SETTING_NEW_BAUDRATE":  // nothing to do!
                    break;

                case "GETTING_DATA_CONTROL":
                    for(int i = 0; i < 7; i++) {
                        tmp = 16 * ReceiveData[indx++];
                        tmp += ReceiveData[indx++];
                        s += tmp.ToString("D4");
                    }
                    return s;

                case "GETTING_DATE_TIME":
                    for(int i = 0; i < 7; i++) {
                        tmp = 10 * CharToInteger((char)ReceiveData[indx++]);
                        tmp += CharToInteger((char)ReceiveData[indx++]);
                        s += tmp.ToString("D2");
                    }
                    return s;

                case "COMMON_INFO":
                    for(int i = 0; i < 8; i++) {
                        tmp = 256 * ReceiveData[indx++];
                        tmp += ReceiveData[indx++];
                        s += tmp.ToString("D5");
                    }
                    return s;

                case "RECORD_INFO":
                    for(int i = 0; i < 2; i++) {
                        tmp = 256 * ReceiveData[indx++];
                        tmp += ReceiveData[indx++];
                        s += tmp.ToString("D5");
                    }
                    return s;
                case "GETTING_FIRST_FRAME_INDEX":
                    tmp = 256 * ReceiveData[indx++];
                    tmp += ReceiveData[indx++];
                    s += tmp.ToString("D5");
                    return s;

                case "GETTING_FRAME":
                    BinArrayToStringConverter();
                    return s;
            }
            return "";
        }

        private UInt16 CRC_Calc16(byte[] b, UInt16 N) // CRC check
        {
            UInt16 crc = 0xffff;

            for(UInt16 i = 0; i < N; i++) {
                crc ^= (UInt16)b[i];

                for(UInt16 j = 0; j < 8; j++) {
                    if((crc & 0x0001) == 1) {
                        crc >>= 1;
                        crc ^= 0xa001;
                    } else {
                        crc >>= 1;
                    }
                }
            }
            UInt16 temp1 = (UInt16)(crc >> 8); // Перестановка байт
            UInt16 temp2 = (UInt16)(crc << 8);
            return (UInt16)(temp1 + temp2);
        }
    }
}
