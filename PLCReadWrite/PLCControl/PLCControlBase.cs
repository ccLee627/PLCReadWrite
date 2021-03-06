﻿using HslCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCReadWrite.PLCControl
{
    public abstract class PLCControlBase
    {
        protected IPLC m_plc;
        private bool m_isConnected = false;

        /// <summary>
        /// Plc连接状态发生改变时触发
        /// </summary>
        public event EventHandler OnPlcStatusChanged;

        public PLCControlBase(IPLC plc)
        {
            m_plc = plc;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        public string IpAddress
        {
            get { return m_plc.IpAddress; }
        }

        /// <summary>
        /// 获取端口号
        /// </summary>
        public int Port
        {
            get { return m_plc.Port; }
        }
        /// <summary>
        /// 指示Plc连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return m_isConnected;
            }
            protected set
            {
                if (value != m_isConnected)
                {
                    if (OnPlcStatusChanged != null)
                    {
                        OnPlcStatusChanged.Invoke(this, new PlcStatusArgs(value ? PlcStatus.Connected : PlcStatus.Unconnected));
                    }
                }
                m_isConnected = value;
            }
        }

        /// <summary>
        /// 尝试建立连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                var connect = m_plc.ConnectServer();
                IsConnected = connect.IsSuccess;
                return IsConnected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (IsConnected)
            {
                m_plc.ConnectClose();
                IsConnected = false;
            }
        }

        /// <summary>
        /// 客户端设置为长连接模式，相当于跳过了ConnectServer的结果验证
        /// </summary>
        public void SetPersistentConnection()
        {
            m_plc.SetPersistentConnection();
        }

        /// <summary>
        /// 批量读取PLC位软元件指定地址的Bool数据
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="uSize"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public bool ReadBool(string startAddr, ushort uSize, ref bool[] sData)
        {
            if (uSize == 0) { return false; }
            OperateResult<bool[]> read = m_plc.ReadBool(startAddr, uSize);
            IsConnected = read.IsSuccess;
            if (IsConnected)
            {
                sData = read.Content;
            }

            return IsConnected;
        }
        /// <summary>
        /// 批量读取PLC字软元件指定地址的Int16数据
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="uSize"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public bool ReadInt16(string startAddr, ushort uSize, ref short[] sData)
        {
            if (uSize == 0) { return false; }
            OperateResult<short[]> read = m_plc.ReadInt16(startAddr, uSize);
            IsConnected = read.IsSuccess;
            if (IsConnected)
            {
                sData = read.Content;
            }

            return IsConnected;
        }

        public bool WriteInt16(string startAddr, short sData)
        {
            OperateResult write = m_plc.Write(startAddr, sData);
            IsConnected = write.IsSuccess;
            return IsConnected;
        }
        public bool WriteInt16(string startAddr, short[] sData)
        {
            OperateResult write = m_plc.Write(startAddr, sData);
            IsConnected = write.IsSuccess;
            return IsConnected;
        }

        public bool WriteBool(string startAddr, bool sData)
        {
            OperateResult write = m_plc.Write(startAddr, sData);
            IsConnected = write.IsSuccess;
            return IsConnected;
        }
        public bool WriteBool(string startAddr, bool[] sData)
        {
            OperateResult write = m_plc.Write(startAddr, sData);
            IsConnected = write.IsSuccess;
            return IsConnected;
        }

        public Task<OperateResult<byte[]>> ReadAsync(string address, ushort length)
        {
            var tcs = new TaskCompletionSource<OperateResult<byte[]>>();
            m_plc.BeginRead(address, length, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult<bool>> ReadBoolAsync(string address)
        {
            var tcs = new TaskCompletionSource<OperateResult<bool>>();
            m_plc.BeginReadBool(address, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult<bool[]>> ReadBoolAsync(string address, ushort length)
        {
            var tcs = new TaskCompletionSource<OperateResult<bool[]>>();
            m_plc.BeginReadBool(address, length, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult<short>> ReadInt16Async(string address)
        {
            var tcs = new TaskCompletionSource<OperateResult<short>>();
            m_plc.BeginReadInt16(address, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult<short[]>> ReadInt16Async(string address, ushort length)
        {
            var tcs = new TaskCompletionSource<OperateResult<short[]>>();
            m_plc.BeginReadInt16(address, length, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }

        public Task<OperateResult> WriteAsync(string address, byte[] value)
        {
            var tcs = new TaskCompletionSource<OperateResult>();
            m_plc.BeginWrite(address, value, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult> WriteAsync(string address, bool value)
        {
            var tcs = new TaskCompletionSource<OperateResult>();
            m_plc.BeginWrite(address, value, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult> WriteAsync(string address, bool[] values)
        {
            var tcs = new TaskCompletionSource<OperateResult>();
            m_plc.BeginWrite(address, values, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult> WriteAsync(string address, short value)
        {
            var tcs = new TaskCompletionSource<OperateResult>();
            m_plc.BeginWrite(address, value, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
        public Task<OperateResult> WriteAsync(string address, short[] values)
        {
            var tcs = new TaskCompletionSource<OperateResult>();
            m_plc.BeginWrite(address, values, read =>
            {
                tcs.SetResult(read);
            });
            return tcs.Task;
        }
    }

    #region PLC状态改变的事件参数定义
    public enum PlcStatus
    {
        Connected,
        Unconnected,
    }
    public class PlcStatusArgs : EventArgs
    {
        private PlcStatus plcStatus;

        public PlcStatus Status
        {
            get { return plcStatus; }
            set { plcStatus = value; }
        }

        public PlcStatusArgs(PlcStatus status)
        {
            this.plcStatus = status;
        }
    }
    #endregion
}
