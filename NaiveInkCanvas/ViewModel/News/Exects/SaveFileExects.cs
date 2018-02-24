using GalaSoft.MvvmLight.Ioc;
using Microsoft.Graphics.Canvas;
using NotificationBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.ViewModel.News.Exects
{
    public class SaveFileExects : CanExecuteSingleInstanceBase<SaveFileExects>
    {
        public override async void Execute()
        {
            var sfp = new FileSavePicker();
            sfp.FileTypeChoices.Add("图片文件", new List<string>() { ".bmp", ".png", ".jpg" });
            sfp.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            var file = await sfp.PickSaveFileAsync();
            if (file == null)
                return;
            var scvm = SimpleIoc.Default.GetInstance<SingleCanvasViewModel>();
            Debug.Assert(scvm != null);
            await scvm.SaveInStreamAsync(await file.OpenAsync(FileAccessMode.ReadWrite));
            NotificationManager.NotifyText("保存到文件失败");

        }
    }
}

