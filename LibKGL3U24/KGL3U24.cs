using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibKGL3U24
{
    public class KGL3U24
    {
        public ushort OutputReg { get; private set; }
        public bool IsDeviceOpen { get; private set; }
        private IntPtr _deviceHandle;
        

        public KGL3U24()
        {
            OutputReg = 0x0000;
            IsDeviceOpen = false;
            _deviceHandle = IntPtr.Zero;
        }

        /// <summary>
        /// 打开3U24开关量控制板设备控制
        /// 如果设备打开正常会将开关量板的输出信号全部断开
        /// </summary>
        /// <returns>当设备打开正常返回true, 失败返回false</returns>
        public bool OpenDevice()
        {
            this._deviceHandle = KGL3U24API.Kgl3u24Open();

            if(_deviceHandle== IntPtr.Zero)
            {
                IsDeviceOpen = false;
                return false;
            }
            else
            {
                IsDeviceOpen = true;
                DisableAllOutputChannel();
                return true;
            }
        }

        /// <summary>
        /// 关闭3U24开关量控制板设备
        /// 首先断开所有开关量输出，然后尝试释放控制句柄
        /// </summary>
        /// <returns>关闭设备成功返回true， 否则返回false</returns>
        public bool CloseDevice()
        {
            DisableAllOutputChannel();

            if (KGL3U24API.Kgl3u24Close(_deviceHandle))
            {
                IsDeviceOpen = false;
                _deviceHandle = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 单通道控制开关量输出
        /// </summary>
        /// <param name="channelNo">通道编号 1...16</param>
        /// <param name="isEnable">是否使能输出</param>
        /// <returns>操作成功返回true, 失败返回false</returns>
        public bool CtrlOutputChannel(int channelNo, bool isEnable)
        {
            if(channelNo<1 || channelNo >16)
            {
                return false;
            }

            ushort channels =(ushort)(0x1 << (channelNo-1));

            return CtrlOutputChannels(channels, isEnable);
        }

        /// <summary>
        /// 多通道控制开关量输出
        /// </summary>
        /// <param name="channels">最高位为通道16,最低位为通道1,
        /// 通道位上被置为1代表要对该通道有操作</param>
        /// <param name="isEnable">要操作的通道是否为使能输出</param>
        /// <returns>操作成功返回true，失败返回false</returns>
        public bool CtrlOutputChannels(ushort channels, bool isEnable)
        {
            ushort willOutputReg = OutputReg;
            if (isEnable)
            {
                willOutputReg |= channels;
            }
            else
            {
                willOutputReg &= (ushort)(~channels);
            }

            return KglOutputSet(willOutputReg);
        }

        /// <summary>
        /// 断开所有的开关量输出
        /// </summary>
        /// <returns></returns>
        public bool DisableAllOutputChannel()
        {
            return CtrlAllOutputChannel(false);
        }

        /// <summary>
        /// 打开所有的开关量输出
        /// </summary>
        /// <returns></returns>
        public bool EnableAllOutputChannel()
        {
            return CtrlAllOutputChannel(true);
        }

        private bool CtrlAllOutputChannel(bool isEnable)
        {
            ushort tempOutCtl=0x0000;

            if(isEnable)
            {
                tempOutCtl = 0xFFFF;
            }

            return KglOutputSet(tempOutCtl);
        }

        private bool KglOutputSet(ushort tempOutput)
        {
            if (KGL3U24API.Kgl3u24OutputSet(_deviceHandle, tempOutput))
            {
                OutputReg = tempOutput;

                return true;
            }
            return false;
        }

        #region KGL3U24API

        static private class KGL3U24API
        {
            [DllImport("DllKGL3U24.dll")]
            public static extern IntPtr Kgl3u24Open();

            [DllImport("DllKGL3U24.dll")]
            public static extern bool Kgl3u24Close(IntPtr deviceHandle);

            [DllImport("DllKGL3U24.dll")]
            public static extern bool Kgl3u24OutputSet(IntPtr deviceHandle, ushort param);
        }

        #endregion
    }

}
