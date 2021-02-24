using Atomus.Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Atomus.Windows.Controls.Menu
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ModernMenu : UserControl, IAction
    {
        private AtomusControlEventHandler beforeActionEventHandler;
        private AtomusControlEventHandler afterActionEventHandler;

        private double maxWIdth = 0.0f;

        #region Init
        public ModernMenu()
        {
            InitializeComponent();

            this.DataContext = new ViewModel.ModernMenuViewModel(this);
        }
        #endregion


        #region Dictionary
        #endregion

        #region Spread
        #endregion

        #region IO
        object IAction.ControlAction(ICore sender, AtomusControlArgs e)
        {
            try
            {
                this.beforeActionEventHandler?.Invoke(this, e);

                switch (e.Action)
                {
                    //case "ExpandAllNodes":
                    //    this.treeView.Items.OfType<System.Windows.Controls.TreeViewItem>().ToList().ForEach(x => this.ExpandAllNodes(x, (bool)e.Value));
                    //    return true;

                    //case "CollapseAllNodes":
                    //    this.treeView.Items.OfType<System.Windows.Controls.TreeViewItem>().ToList().ForEach(x => this.ExpandAllNodes(x, (bool)e.Value));
                    //    return true;

                    case "Menu.MenuFolding":
                        return true;

                    case "Menu.OpenControl":
                        return true;

                    case "Menu.TopMenu":
                        return (this.DataContext as ViewModel.ModernMenuViewModel).GetSearchTopMenu();

                    case "Menu.TopMenuSelected":
                        if (e.Value != null && e.Value is object[] && (e.Value as object[]).Length == 2)
                        {
                            (this.DataContext as ViewModel.ModernMenuViewModel).MenuID = (decimal)(e.Value as object[])[0];
                            (this.DataContext as ViewModel.ModernMenuViewModel).ParentMenuID = (decimal)(e.Value as object[])[1];

                            (this.DataContext as ViewModel.ModernMenuViewModel).SearchCommand.Execute(null);
                        }
                        return true;

                    default:
                        throw new AtomusException("'{0}'은 처리할 수 없는 Action 입니다.".Translate(e.Action));
                }
            }
            finally
            {
                if (!new string[] { "ExpandAllNodes", "CollapseAllNodes", "Menu.TopMenu", "Menu.TopMenuSelected" }.Contains(e.Action))
                    this.afterActionEventHandler?.Invoke(this, e);
            }
        }


        #endregion

        #region Event
        event AtomusControlEventHandler IAction.BeforeActionEventHandler
        {
            add
            {
                this.beforeActionEventHandler += value;
            }
            remove
            {
                this.beforeActionEventHandler -= value;
            }
        }
        event AtomusControlEventHandler IAction.AfterActionEventHandler
        {
            add
            {
                this.afterActionEventHandler += value;
            }
            remove
            {
                this.afterActionEventHandler -= value;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as ViewModel.DefaultMenuViewModel).NotifyPropertyChanged("RefreshBackgroundImage");
            //(this.DataContext as ViewModel.DefaultMenuViewModel).NotifyPropertyChanged("ExpendAllBackgroundImage");
            //(this.DataContext as ViewModel.DefaultMenuViewModel).NotifyPropertyChanged("CollapseAllBackgroundImage");
        }
        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(btn_Fold.IsChecked == true)
            {
                btn_Fold.IsChecked = false;
                Btn_Fold_Checked(btn_Fold, null);
            }

            ViewModel.ModernMenuViewModel.MenuItem menuItem;

            if (sender is TreeViewItem && (sender as TreeViewItem).DataContext != null && (sender as TreeViewItem).DataContext is ViewModel.ModernMenuViewModel.MenuItem)
            {
                menuItem = (sender as TreeViewItem).DataContext as ViewModel.ModernMenuViewModel.MenuItem;

                if (menuItem.assemblyID > 0)
                    this.ControlAction(this, "Menu.OpenControl", new object[] { menuItem.MenuID, menuItem.AssemblyID, menuItem.VisibleOne });
            }

            //Label label;
            //MenuItem menuItem;

            //label = (sender as Label);



            //if ((this.DataContext as ViewModel.ModernMenuViewModel).selec)
            //{
            //    menuItem = (label.Tag as MenuItem);
            //    this.ControlAction(this, "Menu.OpenControl", new object[] { menuItem.MenuID, menuItem.AssemblyID, menuItem.VisibleOne });
            //}
        }
        #endregion

        #region ETC
        private void ExpandAllNodes(System.Windows.Controls.TreeViewItem treeItem, bool isExpanded)
        {
            treeItem.IsExpanded = isExpanded;
            foreach (var childItem in treeItem.Items.OfType<System.Windows.Controls.TreeViewItem>())
            {
                ExpandAllNodes(childItem, isExpanded);
            }
        }
        
        #endregion

        private void Btn_Fold_Checked(object sender, RoutedEventArgs e)
        {            
            this.ControlAction(this, "Menu.MenuFolding", new object[] { btn_Fold.IsChecked });
            
            if(tempItem is TreeViewItem)
            {
                (tempItem as TreeViewItem).IsExpanded = false;
            }
        }
        private TreeViewItem tempItem = null;
        private void Tree_ImgOnly_Selected(object sender, RoutedEventArgs e)
        {
            tempItem = e.OriginalSource as TreeViewItem;
        }
    }
}
