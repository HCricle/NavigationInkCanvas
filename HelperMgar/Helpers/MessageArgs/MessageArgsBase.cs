using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.MessageArgs.Helpers
{
    /// <summary>
    /// 消息发送参数基础
    /// </summary>
    public abstract class MessageArgsBase:EventArgs
    {
        public abstract void ShowUI();
        public string Message { get; set; }
        public string Title { get; set; }
        public MessageType MsgType { get; set; }
    }
    public enum MessageType
    {
        Diago,
        Toast,
        None
    }
}
