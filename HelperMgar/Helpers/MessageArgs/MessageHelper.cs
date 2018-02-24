using NaiveInkCanvas.Helpers.MessageArgs.MessageArgsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.MessageArgs.Helpers
{
    public class MessageHelper
    {
        public static MessageHelper Default => new MessageHelper();
        private MessageHelper() { }
        public object Sender { get; private set; }
        /// <summary>
        /// 注册默认toast消息通知
        /// </summary>
        /// <param name="sender"></param>
        public void InitDefaultMessager(object sender)
        {
            throw new Exception("没有做");
            //Messenger.Default.Register<ToastMessageArgs>((Sender=sender),MessageAction);
        }
        /// <summary>
        /// 自定义注册消息通知
        /// </summary>
        /// <typeparam name="TMESSAGER">MessageArgsBase基类</typeparam>
        /// <param name="sender">注册者</param>
        /// <param name="action">消息发送接收动作</param>
        public void InitMessager<TMESSAGER>(object sender,Action<TMESSAGER> action)
            where TMESSAGER:MessageArgsBase
        {
            throw new Exception("没有做");
            //Messenger.Default.Register((Sender = sender), action);
        }
        /// <summary>
        /// 显示ui
        /// </summary>
        /// <param name="e">数据</param>
        private void MessageAction(ToastMessageArgs e)
        {
            e.ShowUI();
        }

    }
}
