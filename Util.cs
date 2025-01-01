using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTest
{
    internal class Util
    {
        // バッファを16バイトごとにフォーマットして出力するメソッド
        public static void PrintBuffer(byte[] buffer, int length)
        {
            for (int i = 0; i < length; i += 16)
            {
                // アドレスを表示
                Console.Write($"{i.ToString("X8")}: ");

                // 16バイトのHEXデータを表示
                for (int j = 0; j < 16; j++)
                {
                    if (i + j < length)
                        Console.Write($"{buffer[i + j].ToString("X2")} ");
                    else
                        Console.Write("   "); // データが足りない部分を空白で埋める
                }

                // スペースを挟んでASCII文字列を表示
                Console.Write(" ");

                // 16バイトのASCIIデータを表示
                for (int j = 0; j < 16; j++)
                {
                    if (i + j < length)
                    {
                        byte b = buffer[i + j];
                        // 表示可能なASCII文字はそのまま表示し、そうでない場合はドットを表示
                        if (b >= 32 && b <= 126)
                            Console.Write((char)b);
                        else
                            Console.Write(".");
                    }
                }

                Console.WriteLine(); // 改行
            }
        }
    }
}
