using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace NotificationBuilder
{
    public static class NotificationManager
    {
        public static void NotifyText(string text, bool isLongStop = true)
        {
            var toastxml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            var toastnode = toastxml.SelectSingleNode("/toast");
            ((XmlElement)toastnode).SetAttribute("duration", isLongStop ? "long" : "short");
            var ele = toastxml.GetElementsByTagName("text");
            ele[0].AppendChild(toastxml.CreateTextNode(text));
            var toast = new ToastNotification(toastxml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="imgpath">图片必须在assets文件夹内</param>
        /// <param name="isLongStop"></param>
        public static void NotifyTextWidthImg(string text,string imgpath, bool isLongStop = true)
        {
            var toastxml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText02);
            var toastnode = toastxml.SelectSingleNode("/toast");
            ((XmlElement)toastnode).SetAttribute("duration", isLongStop ? "long" : "short");
            var toastimg = toastxml.GetElementsByTagName("image");
            ((XmlElement)toastimg[0]).SetAttribute("src", $"ms-appx:////assets/" + imgpath);
            ((XmlElement)toastimg[0]).SetAttribute("alt", "logo");
            var ele = toastxml.GetElementsByTagName("text");
            ele[0].AppendChild(toastxml.CreateTextNode(text));
            var toast = new ToastNotification(toastxml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }
    }
}
