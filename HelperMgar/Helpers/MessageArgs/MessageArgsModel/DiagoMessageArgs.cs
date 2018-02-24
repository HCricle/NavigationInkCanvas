using NaiveInkCanvas.MessageArgs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace NaiveInkCanvas.Helpers.MessageArgs.MessageArgsModel
{
    public class DiagoMessageArgs : MessageArgsBase
    {
        public DiagoMessageArgs(string message,string Title="")
        {
            MsgType = MessageType.Diago;
            
        }
        public override async void ShowUI()
        {            
            await new MessageDialog(Message, Title).ShowAsync();
        }
    }
}
