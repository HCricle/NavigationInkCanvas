using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

namespace NaiveInkCanvas.Helpers
{
    public class ApplicationHelper
    {
        private static ApplicationHelper Instance;
        public static ApplicationHelper Default => 
            Instance ?? (Instance = new ApplicationHelper());
        private ApplicationHelper() { }
        public string ApplicationTitle => 
            ApplicationView.GetForCurrentView().Title;
        /// <summary>
        /// 设置窗口标题
        /// </summary>
        /// <param name="title"></param>
        public void SetWindowTitle(string title)
        {
            ApplicationView.GetForCurrentView().Title = title;
        }
        public async Task<bool> Quire(string text, string title = "")
        {
            var msgd = new MessageDialog(text, title);
            msgd.DefaultCommandIndex = 1;
            msgd.Commands.Add(new UICommand("确认"));
            msgd.Commands.Add(new UICommand("取消"));
            var res = await msgd.ShowAsync();
            if (res.Label == "取消")
            {
                return false;
            }
            return true;
        }

    }
}
