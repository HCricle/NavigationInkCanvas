using NaiveInkCanvas.Model.NewModels.Layer.Model.DataModelDefs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace NaiveInkCanvas.Model.NewModels.Layer.Model
{
    [DataContract(IsReference = true)]
    public class CanvasData : ICanvasData
    {
        public CanvasData()
        {
            Datas = new LinkedList<KeyValuePair<string, string>>();
        }

        public CanvasData(string name, bool isSystem = true)
            : this()
        {
            Name = name;
            IsSystem = isSystem;
        }

        /// <summary>
        /// 数据名字
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 是否系统(自动生成)数据
        /// </summary>
        [DataMember]
        public bool IsSystem { get; set; }
        [DataMember]
        public string DataTypeString { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        [DataMember]
        public LinkedList<KeyValuePair<string,string>> Datas { get; set;}

        public void SetDatas(params KeyValuePair<string, string>[] datas)
        {
            foreach (var data in datas)
            {
                Datas.AddFirst(data);
            }
        }
        public void InsertData<TVALUE>(string key,TVALUE value)
        {
            Datas.AddFirst(new KeyValuePair<string, string>(key, value.ToString()));
        }
        public CanvasData GroupByName(string name)
        {
            var cd = new CanvasData(name);
            Datas.ToList().ForEach(kp => 
            {
                if (kp.Key == name) 
                    cd.Datas.AddFirst(kp);
            });
            return cd;
        }
    }
}
