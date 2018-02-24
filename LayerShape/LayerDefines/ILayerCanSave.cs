using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface ILayerCanSave
    {
        Task SaveInMemory(IRandomAccessStream stream,Size canvasSize,float dpi=98f);
        Task SaveInRandomAccessStream(IRandomAccessStream stream, Size canvasSize, Action<ICanvasResourceCreator, CanvasDrawingSession> draw, CanvasBitmapFileFormat canvasBitmapFile = CanvasBitmapFileFormat.Auto, float dpi = 98f);
    }
}
