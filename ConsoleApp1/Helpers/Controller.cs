using System.Text;
using WisdomMSocketServiceCore.Helper;

namespace WisdomMSocketServiceCore
{
    public class Controller
    {
        public byte startf_ = Convert.ToByte('F');          // 帧头 F (凡)
        public byte starte_ = Convert.ToByte('E');          // 帧头 E (尔)
        public byte startz_ = Convert.ToByte('Z');          // 帧头 Z (智)
        public byte startn_ = Convert.ToByte('N');          // 帧头 N (能)

        public int version_ = 1;                            // 版本号
        public int funcCode_ = 1;                           // 功能类型
        public int len_ = 32;                               // 长度
        public int srcZjID_ = 206;                          // 源
        public int destStartZjID_ = 1;                      // 目标起始ID
        public int destEndZjID_ = 255;                      // 目标终止ID
        public int packsernum_ = 0;                         // 包序列

        public string body_ = "";                           // 包体，json

        // 协议功能码枚举
        public enum FuncCode
        {
            Test = 0x00,                                    // 测试用数据，为测试网络延迟
            Select = 0x01,                                  // 选架数据
            Opera = 0x02,                                   // 操作数据
            Emerglock = 0x03,                               // 闭锁与急停
            Stopexit = 0x04,                                // 被控支架按停止
            SensorData = 0x05,                              // 传感器数据变化主动上报
            ParaSync = 0x06,                                // 参数同步

            /*
            F = 0x07:  输出版本（保留）
            F = 0x08:  得到版本（保留）
            F = 0x09:  设置时间
            F = 0x0A:  获取煤机位置（未用）
            F = 0x10:  云台角度, 雨刷查询（已不用）
            F = 0x11:  云台角度控制（已不用）
            F = 0x12:  雨刮控制（已不用）
            F = 0x13:  获取时间（保留）
            F = 0x14:  带参数操作
            F = 0x15:  上位机传感器数据查询
            F = 0x16:  操作心跳（暂未用）
            F = 0x17:  低频接收开关
            F = 0x10001:  发布煤机红外（不用）
            F = 0x10002:  进入跟机状态
            F = 0x20001:  跟机传感器模拟（不用）
            F = 0x30001:  网络探测
            */

            ERRUNKNOWN = 0xFF                               // 异常功能码，不在上面的，统一用这个
        }

        //急停闭锁状态枚举
        public enum EmerglockStatus
        {
            NORMAL = 0x00,                                  // 正常状态
            EMERT = 0x01,                                   // 急停
            LOCK = 0x02                                     // 闭锁
        }
        public int emerglockStatus_;                        //急停闭锁状态

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bytesReceivedMsg">收到的消息</param>
        public Controller(byte[]? bytesReceivedMsg = null)
        {
            if (bytesReceivedMsg != null) AnalysisMsg(bytesReceivedMsg);
        }

        /// <summary>
        /// 构造要发送的信息
        /// </summary>
        /// <param name="sendDataStruct">要构造信息的类</param>
        /// <returns></returns>
        public byte[] MakeSendMsg()
        {
            List<byte> lstbteNetMessage = new()
            {
                (byte)'F',
                (byte)'E',
                (byte)'Z',
                (byte)'N'
            };
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(version_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(funcCode_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(len_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(srcZjID_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(destStartZjID_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(destEndZjID_));
            lstbteNetMessage.AddRange(ConvertsHelpers.Int2Bytes_High2Low(packsernum_));
            lstbteNetMessage.AddRange(Encoding.Default.GetBytes(body_));
            byte[] sendBuffer = new byte[lstbteNetMessage.Count];
            int iCount = 0;
            foreach (byte bte in lstbteNetMessage)
            {
                sendBuffer[iCount] = bte;
                iCount++;
            }
            return sendBuffer;
        }

        /// <summary>
        /// 解析收到的消息
        /// </summary>
        /// <param name="bytesReceivedMsg">收到的消息byte数组</param>
        /// <returns></returns>
        public bool AnalysisMsg(byte[] bytesReceivedMsg)
        {
            startf_ = bytesReceivedMsg[0];
            starte_ = bytesReceivedMsg[1];
            startz_ = bytesReceivedMsg[2];
            startn_ = bytesReceivedMsg[3];

            version_ = ConvertsHelpers.Bytes2Int_High2Low(bytesReceivedMsg, 4);
            funcCode_ = ConvertsHelpers.Bytes2Int_High2Low(bytesReceivedMsg, 8);
            len_ = ConvertsHelpers.Bytes2Int_High2Low(bytesReceivedMsg, 12);
            srcZjID_ = Convert.ToInt16(bytesReceivedMsg[19].ToString("x2"), 16);
            destStartZjID_ = Convert.ToInt16(bytesReceivedMsg[23].ToString("x2"), 16);
            destEndZjID_ = Convert.ToInt16(bytesReceivedMsg[27].ToString("x2"), 16);
            packsernum_ = Convert.ToInt16(bytesReceivedMsg[31].ToString("x2"), 16);

            body_ = "";
            int i = 32;
            if (bytesReceivedMsg.Length > 32)
            {

                for (int j = i; j < bytesReceivedMsg.Length; j++)
                {
                    body_ += Convert.ToChar(Convert.ToUInt32(bytesReceivedMsg[j]));
                }
            }
            return true;
        }
    }
}
