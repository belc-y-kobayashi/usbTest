using System;
using System.Linq;
using System.Text;
using HidApiAdapter;

namespace SerialPortTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var devices = HidDeviceManager.GetManager().SearchDevices(0xAAE, 0x20);
            byte[] buff = new byte[32];
            byte[] scanCmd =
            [
                0x05,
                0x02,
                0x01,
                0x00,
                0x53,
                0x03,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00
            ];
            foreach (var device in devices)
            {
                // デバイスに接続
                device.Connect();
                // スキャン要求
                device.Write(scanCmd);
                while (device.Read(buff, 32) > 0)
                {
                    HandleReceivedData(buff, 32);
                    // スキャン要求
                    var res2 = device.Write(scanCmd);
                }
            }
        }

        static void HandleReceivedData(byte[] bytes, int length)
        {
            Util.PrintBuffer(bytes, length);
            for (int i = 4; i < length; i++)
            {
                if (bytes[i] == 3)
                {
                    Keyboard.PressKey(0x0D); //CR
                    Keyboard.PressKey(0x0A); //LF
                    break;
                }
                else
                    Keyboard.PressKey(bytes[i]);
            }
        }
    }
}
