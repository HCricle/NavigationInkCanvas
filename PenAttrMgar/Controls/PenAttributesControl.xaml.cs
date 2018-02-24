using NaiveInkCanvas.CanvasPartControls.BaseControl;
using NaiveInkCanvas.Controls.PenAttributesModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Hsv;
using Windows.UI;
using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.ObjectModel;
using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System.Linq;
using Microsoft.Graphics.Canvas.Brushes;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;
using NaiveInkCanvas.Model.NewModels;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NaiveInkCanvas.Controls
{
    public sealed partial class PenAttributesControl : UserControl
    {
        public PenAttributesControl(LayerModel penAttribute)
        {
            this.InitializeComponent();
            DataContext = new PenAttributeModel(penAttribute.Name, penAttribute);
            //DataContextChanged += (o, e) => this.Bindings.Update();
            Borders = new ObservableCollection<Border>();
        }
        public PenAttributeModel ViewModel => (PenAttributeModel)DataContext;
        private ObservableCollection<Border> Borders { get; }

        public Color LocalColor
        {
            get
            {
                try
                {
                    return ViewModel.LocalLayerModel.LocPenModel.SelectColor;
                }
                catch (Exception)
                {
                    
                    return Colors.White;
                }
            }
        }

        public ColorPoint LocalColorPoint => cp?.ColorPoints[0];
        public bool IsUpest { get; set; } = false;
        private CanMoveMoverManager MoveManager;
        private double TmpHeight;        
        private void Pen_Loaded(object sender, RoutedEventArgs e)
        {
            MoveManager = new CanMoveMoverManager(this);
            MoveManager.InitCanMoveControl(TxtTitle);
            cp.ColorPoints[0].ColorChanged += OnColorChanged;
            ViewModel.LayerModelChanged += ViewModel_LayerModelChanged;
        }
        /*
        public void UpdataBinding()
        {
            Bindings.Update();
        }
        */
        private void ViewModel_LayerModelChanged()
        {
            if (LocalColorPoint == null)
                return;
            //Bindings.Update();
            LocalColorPoint.Color = LocalColor ;
            SdrOpacity.Value = LocalColor.A;
        }

        private void TxtTitle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.GetCurrentPoint(sender as UIElement).Properties.IsRightButtonPressed)
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }
        }

        private void Pen_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void SmallestButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsUpest)
            {
                TmpHeight = Height;
                Height = TitleGrid.ActualHeight;
            }
            else
            {
                Height = TmpHeight;
            }
            IsUpest = !IsUpest;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
        private void LstvPens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Borders.ToList().ForEach(b => b.Child = null);
            Borders.Clear();
            var pb = (LstvPens.SelectedItem as PenBase);
            pb.PenChanged?.Invoke();
            pb.PenParts.ToList().ForEach(p => 
            {
                var b = new Border() { Child = p.Control };
                Borders.Add(b);
            });

        }

        private void SdrOpacity_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (LocalColorPoint == null)
                return;
            var cpoint = LocalColorPoint;
            cpoint.Color = BuildColor();
        }
        private void OnColorChanged(object o, PropertyEventArgs e)
        {
            if (ViewModel.BushType == PenAttributesModels.BrushTypes.SolidBrush)
            {
                if (o is ColorPoint cp)
                {
                    LocalColorPoint.Color = BuildColor();
                    ViewModel.LocalLayerModel.LocPenModel.SetSolidColorBrush(LocalColorPoint.Color);
                    RecLocBrush.Fill = new SolidColorBrush(LocalColor);
                }
            }        
        }
        private Color BuildColor()
        {
            return new Color()
            {
                R = LocalColorPoint.Color.R,
                G = LocalColorPoint.Color.G,
                B = LocalColorPoint.Color.B,
                A = (byte)SdrOpacity.Value
            };
        }

        private void Pivots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var piv = sender as Pivot;
            ViewModel.BushType = (PenAttributesModels.BrushTypes)Enum.Parse(typeof(PenAttributesModels.BrushTypes), (piv.SelectedItem as FrameworkElement).Tag.ToString());
        }

        private void Rct_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
        /*
        private void BtnLinerAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.InsetLinearStopCommand.Execute(new MyGradientStop() { Color = LocalColorPoint.Color, Position = Single.Parse(SdrLinerPosition.Value.ToString()) });
        }

        private void BtnGradialAdd_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.InsetRadialStopCommand.Execute(new MyGradientStop() { Color = LocalColorPoint.Color, Position = Single.Parse(SdrGradialPosition.Value.ToString()) });
        }

        private void BtnLinerDelete_Click(object sender, RoutedEventArgs e)
        {
            var gs = (sender as FrameworkElement).Tag as MyGradientStop;
            ViewModel.RemoveLinearStopCommand.Execute(gs);
        }

        private void BtnGradilDelete_Click(object sender, RoutedEventArgs e)
        {
            var gs = (sender as FrameworkElement).Tag as MyGradientStop;
            ViewModel.RemoveRadialStopCommand.Execute(gs);
        }
        */
    }
}
