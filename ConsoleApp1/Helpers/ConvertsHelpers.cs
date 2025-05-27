using System.Text.RegularExpressions;
using System.Text;

namespace WisdomMSocketServiceCore.Helper
{
    public static class ConvertsHelpers
    {
        // 16进制字符串（空格隔开）转 Byte格式
        public static byte[] HexStr2Byte(string textdata)
        {
            string HexStr = textdata.Replace(" ", string.Empty);
            byte[] data = new byte[HexStr.Length / 2];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(HexStr.Substring(i * 2, 2), 16);
            }
            return data;
        }

        //  Byte[] 转 16进制字符串（空格隔开）
        public static string Byte2HexStr(byte[] data)
        {
            //16进制显示
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.AppendFormat("{0:x2}" + " ", data[i]);
            }
            return sb.ToString().ToUpper();
        }

        //  Byte[] 转 浮点数 Float
        public static float Byte2Float(byte[] data)
        {
            data = data.Reverse().ToArray();
            unsafe
            {
                float a = 0.0F;
                byte i;
                byte[] x = data;
                void* pf;
                fixed (byte* px = x)
                {
                    pf = &a;
                    for (i = 0; i < data.Length; i++)
                    {
                        *((byte*)pf + i) = *(px + i);
                    }
                }
                return a;
            }
        }

        // 10进制数 转 定长16进制字符串（且+空格）
        public static string Int2HexStr_Normalization(int num, int len)
        {
            string hex_str = Convert.ToString(num, 16);
            string result = hex_str.ToString().PadLeft(len, '0');
            for (int i = 2; i < len; i += 2)
            {
                result = result.Insert(i, " ");
            }
            return result;
        }

        //16进制字符串转2进制字符串
        public static string HexString2BinString(string hexString)
        {
            string result = string.Empty;
            foreach (char c in hexString)
            {
                int v = Convert.ToInt32(c.ToString(), 16);
                int v2 = int.Parse(Convert.ToString(v, 2));
                // 去掉格式串中的空格，即可去掉每个4位二进制数之间的空格，
                result += string.Format("{0:d4} ", v2);
            }
            return result;
        }

        public static float HexString2FloatString(string strHexString)
        {
            if (strHexString.Length != 8) return 0;
            float fReturn = 0;
            MatchCollection matches = Regex.Matches(strHexString, @"[0-9A-Fa-f]{2}");
            byte[] bytes = new byte[matches.Count];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = byte.Parse(matches[i].Value, System.Globalization.NumberStyles.AllowHexSpecifier);
            fReturn = BitConverter.ToSingle(bytes.Reverse().ToArray(), 0);
            return fReturn;
        }

        /// <summary>
        /// 将int数值转换为占四个字节的byte数组，低位在前，高位在后的顺序
        /// </summary>
        /// <param name="value">要转换的int值</param>
        /// <returns>四字节byte数组</returns>
        public static byte[] Int2Bytes_Low2High(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }

        /// <summary>
        /// 将int数值转换为占四个字节的byte数组，高位在前，低位在后的顺序
        /// </summary>
        /// <param name="value">要转换的int值</param>
        /// <returns>四字节byte数组</returns>
        public static byte[] Int2Bytes_High2Low(int value)
        {
            byte[] src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }

        /// <summary>
        /// byte数组中取int数值，高位在前，低位在后
        /// </summary>
        /// <param name="src">byte数组</param>
        /// <param name="offset">从数组的第offset位开始</param>
        /// <returns>int数值</returns>
        public static int Bytes2Int_High2Low(byte[] src, int offset)
        {
            int value;
            value = (int)(((src[offset] & 0xFF) << 24)
                    | ((src[offset + 1] & 0xFF) << 16)
                    | ((src[offset + 2] & 0xFF) << 8)
                    | (src[offset + 3] & 0xFF));
            return value;
        }
    }
}
