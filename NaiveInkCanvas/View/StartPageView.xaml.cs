using NaiveInkCanvas.Model;
using NotificationBuilder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NaiveInkCanvas.Helpers;
using NaiveInkCanvas.Model.NewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using NaiveInkCanvas.ViewModel;
using NaiveInkCanvas.ViewModel.News;
using System.Collections.Specialized;
using NaiveInkCanvas.CanvasPartControls.BaseControl;

namespace NaiveInkCanvas.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StartPageView : Page
    {
        private StartPageViewModel ViewModel => (StartPageViewModel)DataContext;
        public StartPageView()
        {
            this.InitializeComponent();


        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode== NavigationMode.Back)
            {
                //ResetProjectItem();
            }
        }
        private void asbName_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ViewModel.ProjectName = sender.Text;
            ViewModel.UpdataSearchProject();
        }

        private void asbName_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args != null && args.ChosenSuggestion != null)
                ViewModel.ItemQuerySubmitted((args.ChosenSuggestion as ProjectInfoModel));
        }
        private ObservableCollection<MenuFlyoutItem> ProjectItems 
            = new ObservableCollection<MenuFlyoutItem>();
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProjectItems.CollectionChanged += ProjectItems_CollectionChanged;
            ViewModel.DeleteProjected += ViewModel_DeleteProjected;
            ResetProjectItem();
        }

        private void ViewModel_DeleteProjected(ProjectInfoModel obj)
        {
            try
            {
                ProjectItems.ToList().ForEach(pj => 
                {
                    if (pj.Tag == obj)
                        ProjectItems.Remove(pj);
                });
                ResetProjectItem();
            }
            catch (Exception) { }
        }

        private void ProjectItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove) 
            {
                ResetProjectItem();
            }
        }
        public void ResetProjectItem()
        {
            menuprojects.Items.Clear();
            ViewModel.UpdataData();
            UpdataProjectItems();
            ProjectItems.ToList().ForEach(project =>
            {
                menuprojects.Items.Add(project);
            });
            menuprojects.UpdateLayout();
            var i = menuprojects.Items.Count;
        }
        private void UpdataProjectItems()
        {
            ProjectItems.Clear();
            ViewModel.Projects.ToList().ForEach(project =>
            {
                var mfi = new MenuFlyoutItem()
                {
                    Text = project.Name,
                    Command = ViewModel.DeleteProjectCommand,
                    CommandParameter = project,
                    Tag = project
                };
                ProjectItems.Add(mfi);
            });
        }
        private void asbName_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if(args!=null&&args.SelectedItem!=null)
                ViewModel.ItemQuerySubmitted((args.SelectedItem as ProjectInfoModel));
        }

    }
}
