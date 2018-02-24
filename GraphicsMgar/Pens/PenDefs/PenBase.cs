using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml.Controls;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.Reflection;

namespace NaiveInkCanvas.Pens.PenDefs
{
    /// <summary>
    /// 实体笔基类
    /// </summary>
    public abstract class PenBase : IPenShape
    {
        public IGraphicsDraw Drawer { get; set; }

        public string Name { get; }

        public ObservableCollection<IPenPartControl> PenParts { get; }

        public PensManagerBase Manager { get; }

        public virtual Action PenChanged { get; } 

        public PenBase(IGraphicsDraw drawer, PensManagerBase pensManager, string name)
        {
            Drawer = drawer;
            Name = name;
            Manager = pensManager;
            PenParts = new ObservableCollection<IPenPartControl>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}