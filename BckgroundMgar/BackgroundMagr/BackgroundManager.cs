using NaiveInkCanvas.Background.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Background
{
    public class BackgroundManager
    {
        private static BackgroundManager inst;
        public static BackgroundManager Inst => (inst ?? (inst = new BackgroundManager()));

        public ObservableCollection<ImgBackground> Backgrounds { get; }
        private BackgroundManager()
        {
            Backgrounds = new ObservableCollection<ImgBackground>();
        }
    }
}
