using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_To_Controls.ControlViewModelManager;
using Windows.UI;
using NaiveInkCanvas.Helpers;
using Windows.UI.Xaml.Media;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NaiveInkCanvas.Pens.PenDefs;
using NaiveInkCanvas.Model.NewModels;

namespace NaiveInkCanvas.Controls.PenAttributesModels
{
    public class PenAttributeModel:ControlViewModelBase
    {
        private string title;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        private SolidColorBrush backgroundColor;

        public SolidColorBrush BackgroundColor
        {
            get => backgroundColor;
            set => Set(ref backgroundColor, value);
        }
        private SolidColorBrush textColor;

        public SolidColorBrush TextColor
        {
            get => textColor;
            set => Set(ref textColor, value);
        }
        private bool isLightModel;

        public bool IsLightModel
        {
            get => isLightModel;
            set
            {
                if (value)
                {
                    BackgroundColor = new SolidColorBrush(Colors.Black);
                    TextColor = new SolidColorBrush(Colors.White);
                }
                else
                {
                    BackgroundColor = new  SolidColorBrush(Colors.White);
                    TextColor = new SolidColorBrush(Colors.Black);
                }
                SettingHelper.LocContainer.Values[LightKey] = value;
                isLightModel = value;
            }
        }
        private LayerModel localLayerModel;

        public LayerModel LocalLayerModel
        {
            get => localLayerModel;
            set
            {
                localLayerModel = value;
                LayerModelChanged?.Invoke();
            }
        }
        public ObservableCollection<MyGradientStop> LinerGradientStops { get; }
        public ObservableCollection<MyGradientStop> RadialGradientStops { get; }
        private SystemPensManager systemPens;
        public SystemPensManager SystemPens => systemPens;
        public static ICanObservation LayerObservated { get; set; }
        public BrushTypes BushType { get; set; }

        private readonly string LightKey = "Light";//晚间模式,1开启,0关闭
        #region Events
        public event Action LayerModelChanged;
        #endregion
        #region Commands
        //public RelayCommand<MyGradientStop> InsetLinearStopCommand { get; }
        //public RelayCommand<MyGradientStop> InsetRadialStopCommand { get; }
        //public RelayCommand<MyGradientStop> RemoveLinearStopCommand { get; }
        //public RelayCommand<MyGradientStop> RemoveRadialStopCommand { get; }
        //public RelayCommand CleanLinearStopsCommand { get; }
        //public RelayCommand CleanRadialStopsCommand { get; }
        #endregion
        public PenAttributeModel(string title,LayerModel layer)
        {
            LocalLayerModel = layer;
            LocalLayerModel.NameChanged += LocalLayerModel_NameChanged;
            BushType = BrushTypes.SolidBrush;            
            LinerGradientStops = new ObservableCollection<MyGradientStop>();
            RadialGradientStops = new ObservableCollection<MyGradientStop>();
            /*
            InsetLinearStopCommand = new RelayCommand<MyGradientStop>(InsetLinearStop);
            InsetRadialStopCommand = new RelayCommand<MyGradientStop>(InsetRadialStop);
            RemoveLinearStopCommand = new RelayCommand<MyGradientStop>(RemoveLinearStop);
            RemoveRadialStopCommand = new RelayCommand<MyGradientStop>(RemoveRadialStop);
            CleanLinearStopsCommand = new RelayCommand(CleanLinearStops);
            CleanRadialStopsCommand = new RelayCommand(CleanRadialStops);
            */
            LayerObservated.ValueChanged += LayerObservated_ValueChanged;
            if (!SettingHelper.LocContainer.Values.Keys.Contains(LightKey))
            {
                SettingHelper.LocContainer.Values.Add(LightKey, false);
            }
            IsLightModel = (bool)SettingHelper.LocContainer.Values[LightKey];            
        }

        private void LocalLayerModel_NameChanged(LayerModel arg1, string arg2)
        {
            if (SystemPens == null)
                systemPens = new SystemPensManager(arg1, arg2);
            Title = arg2 + "-笔属性";
        }
        /*
        private void CleanRadialStops()
        {
            RadialGradientStops.Clear();
            UpdataRadialBrush();
        }

        private void CleanLinearStops()
        {
            LinerGradientStops.Clear();
            UpdataLinerBrush();
        }

        private void RemoveRadialStop(MyGradientStop obj)
        {
            Debug.Assert(LinerGradientStops.IndexOf(obj) != -1);
            LinerGradientStops.Remove(obj);
            UpdataRadialBrush();
        }

        private void RemoveLinearStop(MyGradientStop obj)
        {
            Debug.Assert(LinerGradientStops.IndexOf(obj) != -1);
            LinerGradientStops.Remove(obj);
            UpdataLinerBrush();
        }

        private void InsetRadialStop(MyGradientStop obj)
        {
            RadialGradientStops.Add(obj);
            UpdataRadialBrush();
        }

        public void InsetLinearStop(MyGradientStop color)
        {
            LinerGradientStops.Add(color);
            UpdataLinerBrush();
        }
        */
        private void LayerObservated_ValueChanged(object obj)
        {
            if (obj is LayerModel lm)
            {
                LocalLayerModel = lm;
            }
        }
        /*
        private void UpdataLinerBrush()
        {
            LocalLayerModel.LocPenModel.SetLinearColorBrush((from stop in LinerGradientStops select stop.ToCanvasGradientStop()).ToArray());
        }
        private void UpdataRadialBrush()
        {
            LocalLayerModel.LocPenModel.SetRadialColorBrush((from stop in RadialGradientStops select stop.ToCanvasGradientStop()).ToArray());
        }
        */
    }
    public enum BrushTypes
    {
        SolidBrush,
        LinerBrush,
        GradialBrush
    }
}
