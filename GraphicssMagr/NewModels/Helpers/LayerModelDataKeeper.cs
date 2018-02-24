using NaiveInkCanvas.Model.NewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace NaiveInkCanvas.Helpers
{
    public static class LayerModelDataKeeper
    {
        public static void SaveInStream(Stream stream,LayerModel layer)
        {
            var xs = new XmlSerializer(typeof(LayerModel));
            xs.Serialize(stream, layer);
        }
        public static async Task SaveInLocalFolder(string name,LayerModel layer)
        {
            var xs = new XmlSerializer(typeof(LayerModel));
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(name, CreationCollisionOption.GenerateUniqueName);
            using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                xs.Serialize(stream.AsStream(), layer);
            }
        }
    }
}
