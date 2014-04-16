using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace HotelVp.Common.DBUtility
{
    public enum EffentNextType
    {
        /// <summary>
        /// ������������κ�Ӱ�� 
        /// </summary>
        None,
        /// <summary>
        /// ��ǰ������Ϊ"select count(1) from .."��ʽ��������������ִ�У������ڻع�����
        /// </summary>
        WhenHaveContine,
        /// <summary>
        /// ��ǰ������Ϊ"select count(1) from .."��ʽ����������������ִ�У����ڻع�����
        /// </summary>
        WhenNoHaveContine,
        /// <summary>
        /// ��ǰ���Ӱ�쵽�������������0������ع�����
        /// </summary>
        ExcuteEffectRows,
        /// <summary>
        /// �����¼�-��ǰ������Ϊ"select count(1) from .."��ʽ����������������ִ�У����ڻع�����
        /// </summary>
        SolicitationEvent
    }

    public enum DataBaseType
    {
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// SqlServer
        /// </summary>
        SQL
    }

    public enum MessageType
    {
        /// <summary>
        /// WARN
        /// </summary>
        WARN,
        /// <summary>
        /// INFO
        /// </summary>
        INFO,
        /// <summary>
        /// DEBUG
        /// </summary>
        DEBUG,
        /// <summary>
        /// FATAL
        /// </summary>
        FATAL,
        /// <summary>
        /// ERROR
        /// </summary>
        ERROR
    }

    public class CommandInfo
    {
        public object ShareObject = null;
        public object OriginalData = null;
        event EventHandler _solicitationEvent;
        public event EventHandler SolicitationEvent
        {
            add
            {
                _solicitationEvent += value;
            }
            remove
            {
                _solicitationEvent -= value;
            }
        }
        public void OnSolicitationEvent()
        {
            if (_solicitationEvent != null)
            {
                _solicitationEvent(this,new EventArgs());
            }
        }
        public string CommandText;
        public string SqlName;
        public string SqlId;
        public System.Data.Common.DbParameter[] Parameters;
        public EffentNextType EffentNextType = EffentNextType.None;
        public CommandInfo()
        {

        }
        public CommandInfo(string SqlName, string SqlId, SqlParameter[] para)
        {
            this.SqlId = SqlId;
            this.SqlName = SqlName;
            this.Parameters = para;
        }
        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }
        public CommandInfo(string sqlText, SqlParameter[] para, EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }
    }
}
