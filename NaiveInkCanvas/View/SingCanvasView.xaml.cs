using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using NaiveInkCanvas.Background;
using NaiveInkCanvas.Background.Models;
using NaiveInkCanvas.Controls;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Helpers.DefHelper;
using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.ViewModel.News;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace NaiveInkCanvas.View
{
    public sealed partial class SingCanvasView : Page
    {
        private SingleCanvasViewModel ViewModel =>
            (SingleCanvasViewModel)DataContext;

        private FrameworkElement TLayers;
        private BackgroundManager BackgroundMgar => BackgroundManager.Inst;
        public SingCanvasView()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.Back)
                ViewModel.SetProject(e.Parameter as ProjectInfoModel);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            ViewModel.SetSenderDependencyObject(Dispatcher);
            ViewModel.SelectedLayer += ViewModel_SelectedLayer;
            ViewModel.RectangleSelecting += ViewModel_RectangleSelecting;
            BackgroundMgar.Backgrounds.CollectionChanged += Backgrounds_CollectionChanged;
            ViewModel.PenControlChanged += ViewModel_PenControlChanged;

            ViewModel.AppearChanged += ViewModel_AppearChanged;
            ViewModel.NameChanged += ViewModel_NameChanged;
            ViewModel.LockChanged += ViewModel_LockChanged;
            ViewModel.PaintModelChanged += ViewModel_PaintModelChanged;
            TLayers = OtherLayesGird;
#if DEBUG
            Test_InitBaseLayer();
#endif
        }
        private FrameworkElement PaintEle;
        private void ViewModel_PaintModelChanged(PaintingModels obj)
        {
            if (obj== PaintingModels.BackgroundPainting)
            {
                //TLayers.Visibility = Visibility.Collapsed;
                PaintEle = OtherLayesGird;
                LayersGrid.Children.Remove(PaintEle);
            }
            else if (obj== PaintingModels.LayerPainting)
            {
                //TLayers.Visibility = Visibility.Visible;
                LayersGrid.Children.Add(PaintEle);
            }
            lvMenus.ItemsSource = null;
            lvMenus.ItemsSource = ViewModel.MenuContexts;
        }

        private void ViewModel_LockChanged(LayerModel arg1, bool arg2)
        {
        }

        private void ViewModel_NameChanged(LayerModel arg1, string arg2)
        {
            var index = LstvLayers.SelectedIndex;
            LstvLayers.ItemsSource = null;
            LstvLayers.ItemsSource = ViewModel.Layers;
            LstvLayers.SelectedIndex = index;
        }

        private void ViewModel_AppearChanged(LayerModel arg1, bool arg2)
        {
        }

        private void ViewModel_PenControlChanged(PenAttributesControl oldcontrol, PenAttributesControl newcontrol)
        {
            if (oldcontrol != null)
            {
                LayerCanvas.Children.Remove(oldcontrol);
            }
            LayerCanvas.Children.Add(newcontrol);

        }

        private void Backgrounds_CollectionChanged(object sender,NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
            {
                BackgroundsCanvas.Children.Clear();
                foreach (var bck in BackgroundManager.Inst.Backgrounds)
                {
                    BackgroundsCanvas.Children.Add(bck.BorderControl);
                }
            }
        }
        

        private void ViewModel_RectangleSelecting(LayerModel obj)
        {
            
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (ViewModel.LocLayer != null)
                ViewModel.SelectSingleLayer(ViewModel.LocLayer);
        }

        private void ViewModel_SelectedLayer(LayerModel obj)
        {
            OtherLayesGird.Children.Clear();
            ViewModel.OtherLayers.ForEach(lm =>
            {
                OtherLayesGird.Children.Add(lm.Canvas);//呜呜呜
            });
            //BackgroundsCanvas.Children.Clear();
            //foreach (var bk in obj.BckManager.BackgroundCollection)
            //{
            //  BackgroundsCanvas.Children.Add(bk);
            //}
            OtherLayesGird.Children.Add(obj.Canvas);
            OtherLayesGird.Children.Add(obj.TopCanvas);
            //LstvLayers.SelectedIndex = ViewModel.GetSelectIndex(obj);
        }
        

        [Conditional("DEBUG")]
        private async void Test_InitBaseLayer()
        {
            await ViewModel.NewDefaultLayerCustom("second");
            await ViewModel.NewDefaultLayerCustom("first");
            await ViewModel.NewDefaultLayerCustom("three");
            //lm.IsStartPointCenter = true;
            //await LayerModelDataKeeper.SaveInLocalFolder("1.txt", lm);
        }

        private void LayerCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LocalLayer.Width = LayerCanvas.ActualWidth;
            LocalLayer.Height = LayerCanvas.ActualHeight;
        }

        private void LstvLayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView lv)
            {
                if (lv.SelectedItem is LayerModel lm)
                {
                    ViewModel.SelectSingleLayer(lm);
                    //lm.PenType = GraphicsTypes.SelectRectangle;
                }
            }
        }

        private void StackPanel_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

            if (sender is FrameworkElement s)
            {
                lstvMenus.SelectedItem = s.Tag;
            }
            if (lstvMenus.SelectedItem is MenuModel mm)
            {
                mm.Command?.Execute(null);
            }
        }

        private void LayerGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is FrameworkElement uie)
            {
                var fo = FlyoutBase.GetAttachedFlyout(uie);
                if (fo != null && e.GetCurrentPoint(uie).Properties.IsRightButtonPressed)
                {
                    fo.ShowAt(uie);
                }
            }
        }

        private void TsIsAppearSetting_Toggled(object sender, RoutedEventArgs e)
        {
            //LstvLayers.UpdateLayout();
        }

        private void lstvMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstvMenus.SelectedItem is MenuModel mm)
            {
                mm.Command?.Execute(null);
            }
        }
        private void LayerCanvas_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key== VirtualKey.Shift)
            {
                if (LayersGrid.Children.IndexOf(TLayers)==-1)
                {
                    LayersGrid.Children.Add(TLayers);
                }
                else
                {
                    LayersGrid.Children.Remove(TLayers);
                }
            }
        }

        private void SpMenu_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is FrameworkElement s)
            {
                lvMenus.SelectedItem = s.Tag;
            }
            if (lvMenus.SelectedItem is MenuContextModel mcm)
            {
                object par = mcm.Text == "加入背景" ? new ImgBackground("Default",LayersGrid) : null;
                mcm.MenuCommad?.Execute(par);
            }
        }

        private void LayerCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(sender as UIElement);
            if (p.Properties.IsRightButtonPressed)
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }
        }
    }
}
/*
 *                             <ListView x:Name="LstvCanvases"
                                  BorderBrush="{x:Bind ViewModel.PaintBorderColor}"
                                  BorderThickness="{x:Bind ViewModel.PaintBorderWidth}"
                                  ItemsSource="{x:Bind ViewModel.OtherLayers}">
                                <ListView.ItemsPanel>
                                    <ItemsPanelTemplate>
                                        <Grid HorizontalAlignment="Stretch"
                                              VerticalAlignment="Stretch"/>
                                    </ItemsPanelTemplate>
                                </ListView.ItemsPanel>
                                <ListView.ItemTemplate>
                                    <DataTemplate x:DataType="model:LayerModel">
                                        <Grid Tag="{x:Bind}">
                                            <Border Child="{x:Bind Canvas}"/>
                                        </Grid>
                                    </DataTemplate>
                                </ListView.ItemTemplate>
                                <ListView.ItemContainerStyle>
                                    <Style TargetType="ListViewItem">
                                        <Setter Property="HorizontalAlignment" Value="Stretch"/>
                                        <Setter Property="VerticalAlignment" Value="Stretch"/>
                                        <Setter Property="HorizontalContentAlignment" Value="Stretch"/>
                                        <Setter Property="VerticalContentAlignment" Value="Stretch"/>
                                    </Style>
                                </ListView.ItemContainerStyle>
                            </ListView>
 */



/*
 * Maybe I need to find some truely to me.
 * I think I must to do.
 * To my friends or my ???
 */