using System.Collections.Generic;

namespace NaiveInkCanvas.Model.NewModels.Layer.Model.DataModelDefs
{
    public interface ICanvasData
    {
        LinkedList<KeyValuePair<string, string>> Datas { get; set; }
        bool IsSystem { get; set; }
        string Name { get; set; }
    }
}