using GalaSoft.MvvmLight.Ioc;
using NaiveInkCanvas.Helpers;
using NotificationBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.UI.Popups;

namespace NaiveInkCanvas.ViewModel.News.Exects
{
    public class ShareExecuts : CanExecuteSingleInstanceBase<ShareExecuts>
    {

        public async override void Execute()
        {
            if (!DataTransferManager.IsSupported())
            {
                await new MessageDialog("当前设备不支持共享.").ShowAsync();
                return;
            }
            var view = DataTransferManager.GetForCurrentView();
            view.DataRequested += View_DataRequested;
            DataTransferManager.ShowShareUI();
        }
        private async void View_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var dp = new DataPackage();
            //var LocProject = ApplicationHelper.Default;
            //dp.Properties.Title = LocProject.ProjectName;
            //dp.SetApplicationLink(new Uri(LocProject.Path));
            var memstrean = new InMemoryRandomAccessStream();
            var scvm = SimpleIoc.Default.GetInstance<SingleCanvasViewModel>();
            Debug.Assert(scvm != null);
            await scvm.SaveInStreamAsync(memstrean);
            dp.SetBitmap(RandomAccessStreamReference.CreateFromStream(memstrean));
            dp.SetText("NaiveCanvas");
            args.Request.Data = dp;
            sender.DataRequested -= View_DataRequested;
        }
    }
}
