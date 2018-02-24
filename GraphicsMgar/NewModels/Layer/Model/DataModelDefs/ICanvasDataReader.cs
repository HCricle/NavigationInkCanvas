using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Layer.Model.DataModelDefs
{
    public interface ICanvasDataReader
    {
        KeyValuePair<string,byte> ReadByte();
        KeyValuePair<string, bool> ReadBoolean();
        KeyValuePair<string, short> ReadInt16();
        KeyValuePair<string, int> ReadInt32();
        KeyValuePair<string, long> ReadInt64();
        KeyValuePair<string, ushort> ReadUInt16();
        KeyValuePair<string, uint> ReadUInt32();
        KeyValuePair<string, ulong> ReadUInt64();
        KeyValuePair<string, Single> ReadSingle();
        KeyValuePair<string, double> ReadDouble();
        KeyValuePair<string, string> ReadString();
        KeyValuePair<string, TimeSpan> ReadTimeSpan();
        event Action<KeyValuePair<string, string>> DataFormatError;
    }
}
