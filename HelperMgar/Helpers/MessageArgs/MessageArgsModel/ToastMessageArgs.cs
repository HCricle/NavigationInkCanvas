using NaiveInkCanvas.MessageArgs.Helpers;
using NotificationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Helpers.MessageArgs.MessageArgsModel
{
    public class ToastMessageArgs : MessageArgsBase
    {
        public ToastMessageArgs(string message, string Title = "")
        {
            MsgType = MessageType.Toast;

        }
        public override void ShowUI()
        {
            NotificationManager.NotifyText(Title + "\n" + Message);
        }
    }
}
