using NaiveInkCanvas.Model.NewModels.Layer.Model.DataModelDefs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Layer
{
    public class CanvasDataReader:ICanvasDataReader
    {
        private ICanvasData Data { get; }
        public event Action<KeyValuePair<string, string>> DataFormatError;
        public CanvasDataReader(ICanvasData data)
        {
            Data = data;
        }

        private KeyValuePair<string, TRESULT> GetValue<TRESULT>(Func<string, TRESULT> convert)
        {
            Debug.Assert(Data.Datas.Count != 0);
            var data = Data.Datas.First.Value;
            TRESULT value = default(TRESULT);
            if (convert != null)
            {
                try
                {
                    convert?.Invoke(data.Value);
                }
                catch (Exception)
                {
                    DataFormatError?.Invoke(new KeyValuePair<string, string>(data.Key, data.Value));
                }
            }
            Data.Datas.RemoveFirst();
            return new KeyValuePair<string, TRESULT>(data.Key, value);
        }

        public KeyValuePair<string, bool> ReadBoolean()
        {
            return GetValue(v => Convert.ToBoolean(v));
        }

        public KeyValuePair<string, byte> ReadByte()
        {
            return GetValue(v => Convert.ToByte(v));

        }

        public KeyValuePair<string, double> ReadDouble()
        {
            return GetValue(v => Convert.ToDouble(v));
        }


        public KeyValuePair<string, short> ReadInt16()
        {
            return GetValue(v => Convert.ToInt16(v));
        }

        public KeyValuePair<string, int> ReadInt32()
        {
            return GetValue(v => Convert.ToInt32(v));
        }

        public KeyValuePair<string, long> ReadInt64()
        {
            return GetValue(v => Convert.ToInt64(v));
        }

        public KeyValuePair<string, float> ReadSingle()
        {
            return GetValue(v => Convert.ToSingle(v));
        }

        public KeyValuePair<string, string> ReadString()
        {
            return GetValue(v => Convert.ToString(v));
        }

        public KeyValuePair<string, TimeSpan> ReadTimeSpan()
        {
            return GetValue(v =>
            {
                TimeSpan.TryParse(v, out var res);
                return res;
            });
        }

        public KeyValuePair<string, ushort> ReadUInt16()
        {
            return GetValue(v => Convert.ToUInt16(v));
        }

        public KeyValuePair<string, uint> ReadUInt32()
        {
            return GetValue(v => Convert.ToUInt32(v));
        }

        public KeyValuePair<string, ulong> ReadUInt64()
        {
            return GetValue(v => Convert.ToUInt64(v));
        }
    }
}
