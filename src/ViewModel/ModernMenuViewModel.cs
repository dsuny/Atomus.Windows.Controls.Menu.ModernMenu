using Atomus.Control;
using Atomus.Diagnostics;
using Atomus.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Atomus.Windows.Controls.Menu.Controllers;
using Atomus.Windows.Controls.Menu.Models;
using System.Threading.Tasks;

namespace Atomus.Windows.Controls.Menu.ViewModel
{
    public class ModernMenuViewModel : Atomus.MVVM.ViewModel
    {
        #region Declare
        private ImageBrush refreshBackgroundImage;
        private ImageBrush expendAllBackgroundImage;
        private ImageBrush collapseAllBackgroundImage;

        private bool isEnabledControl;

        private ObservableCollection<MenuItem> menuData;

        private decimal menuID;
        private decimal parentMenuID;
        private string _FilterText;

        public class MenuItem : Atomus.MVVM.ViewModel
        {
            private decimal menuID;
            private decimal parentMenuID;
            private string name;
            private string decription;            

            public ImageSource image1;
            public ImageSource image2;
            public ImageSource image3;
            public ImageSource image4;

            public decimal assemblyID;
            public string namespace1;
            public bool visibleOne;            

            private ObservableCollection<MenuItem> children;

            private ImageSource _DefImage;

            public ImageSource DefImage
            {
                get { return _DefImage; }
                set
                {
                    if (this._DefImage != value)
                    {
                        this._DefImage = value; this.NotifyPropertyChanged();
                    }
                }
            }

            public decimal MenuID
            {
                get { return this.menuID; }
                set
                {
                    if (this.menuID != value)
                    {
                        this.menuID = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public decimal ParentMenuID
            {
                get { return this.parentMenuID; }
                set
                {
                    if (this.parentMenuID != value)
                    {
                        this.parentMenuID = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Name
            {
                get { return this.name; }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Decription
            {
                get { return this.decription; }
                set
                {
                    if (this.decription != value)
                    {
                        this.decription = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource Image1
            {
                get { return this.image1; }
                set
                {
                    if (this.image1 != value)
                    {
                        this.image1 = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource Image2
            {
                get { return this.image2; }
                set
                {
                    if (this.image2 != value)
                    {
                        this.image2 = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource Image3
            {
                get { return this.image3; }
                set
                {
                    if (this.image3 != value)
                    {
                        this.image3 = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public ImageSource Image4
            {
                get { return this.image4; }
                set
                {
                    if (this.image4 != value)
                    {
                        this.image4 = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public decimal AssemblyID
            {
                get { return this.assemblyID; }
                set
                {
                    if (this.assemblyID != value)
                    {
                        this.assemblyID = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public string Namespace
            {
                get { return this.namespace1; }
                set
                {
                    if (this.namespace1 != value)
                    {
                        this.namespace1 = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            public bool VisibleOne
            {
                get { return this.visibleOne; }
                set
                {
                    if (this.visibleOne != value)
                    {
                        this.visibleOne = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }

            public ObservableCollection<MenuItem> Children
            {
                get { return this.children; }
                set
                {
                    if (this.children != value)
                    {
                        this.children = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
        }
        #endregion

        #region Property
        public ICore Core { get; set; }

        public ImageBrush RefreshBackgroundImage
        {
            get  {  return this.refreshBackgroundImage;   }
            set
            {
                if (this.refreshBackgroundImage != value)
                {
                    this.refreshBackgroundImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ImageBrush ExpendAllBackgroundImage
        {
            get   {   return this.expendAllBackgroundImage;    }
            set
            {
                if (this.expendAllBackgroundImage != value)
                {
                    this.expendAllBackgroundImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ImageBrush CollapseAllBackgroundImage
        {
            get { return this.collapseAllBackgroundImage; }
            set
            {
                if (this.collapseAllBackgroundImage != value)
                {
                    this.collapseAllBackgroundImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
                
        public ObservableCollection<MenuItem> MenuData
        {
            get
            {
                if (this.menuData == null)
                {
                    this.menuData = new ObservableCollection<MenuItem>();
                }
                var view = System.Windows.Data.CollectionViewSource.GetDefaultView(this.menuData);
                if (view.CanFilter)
                {
                    view.Filter = this.MenuFilter;
                }
                return this.menuData;
            }
            
            set
            {
                if (this.menuData != value)
                {
                    this.menuData = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public decimal MenuID
        {
            get
            {
                return this.menuID;
            }
            set
            {
                if (this.menuID != value)
                {
                    this.menuID = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public decimal ParentMenuID
        {
            get
            {
                return this.parentMenuID;
            }
            set
            {
                if (this.parentMenuID != value)
                {
                    this.parentMenuID = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool IsEnabledControl
        {
            get
            {
                return this.isEnabledControl;
            }
            set
            {
                if (this.isEnabledControl != value)
                {
                    this.isEnabledControl = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        
        public string FilterText
        {
            get { return this._FilterText; }
            set
            {
                if (this._FilterText != value)
                {
                    this._FilterText = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ExpandAllCommand { get; set; }
        public ICommand CollapseAllCommand { get; set; }
        public ICommand MenuFilterCommand { get; set; }
        
        
        #endregion

        #region INIT
        public ModernMenuViewModel()
        {
            this.menuID = -1;
            this.ParentMenuID = -1;

            this.SearchCommand = new MVVM.DelegateCommand(() => { this.SearchProcess(this.menuID, this.ParentMenuID); }
                                                            , () => { return this.isEnabledControl; });
            this.ExpandAllCommand = new MVVM.DelegateCommand(() => { this.ExpandAllProcess(); }
                                                            , () => { return this.isEnabledControl; });
            this.CollapseAllCommand = new MVVM.DelegateCommand(() => { this.CollapseAllProcess(); }
                                                            , () => { return this.isEnabledControl; });

            this.MenuFilterCommand = new MVVM.DelegateCommand(() => { this.MenuFilterProcess(); });
        }

        

        public ModernMenuViewModel(ICore core) : this()
        {
            this.Core = core;

            try
            {
                this.isEnabledControl = true;
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }

        }
        #endregion

        #region IO

        private async void SearchProcess(decimal MENU_ID, decimal PARENT_MENU_ID)
        {
            Service.IResponse result;

            try
            {
                this.IsEnabledControl = false;
                (this.SearchCommand as Atomus.MVVM.DelegateCommand).RaiseCanExecuteChanged();

                result = await this.Core.SearchAsync(new ModernMenuSearchModel()
                {
                    START_MENU_ID = MENU_ID,
                    ONLY_PARENT_MENU_ID = PARENT_MENU_ID
                });

                if (result.Status == Service.Status.OK)
                {
                    this.MenuData = null;
                    if (result.DataSet != null && result.DataSet.Tables.Count > 0)
                        this.MenuData = await this.SetTree(result.DataSet.Tables[1]);
                }
                else
                    this.WindowsMessageBoxShow(Application.Current.Windows[0], result.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
                //this.WindowsMessageBoxShow(Application.Current.Windows[0], ex);
            }
            finally
            {
                this.IsEnabledControl = true;
                (this.SearchCommand as Atomus.MVVM.DelegateCommand).RaiseCanExecuteChanged();
            }
        }
        public DataTable GetSearchTopMenu()
        {
            Service.IResponse result;

            try
            {
                result = this.Core.Search(new ModernMenuSearchModel()
                {
                    START_MENU_ID = 0,
                    ONLY_PARENT_MENU_ID = 0
                });

                if (result.Status == Service.Status.OK && result.DataSet != null && result.DataSet.Tables.Count > 0)
                    return result.DataSet.Tables[1];
                else
                    return null;
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
                //this.WindowsMessageBoxShow(Application.Current.Windows[0], ex);
                return null;
            }
            finally
            {
            }
        }

        private async Task<ObservableCollection<MenuItem>> SetTree(DataTable dataTable)
        {
            ObservableCollection<MenuItem> trees;
            MenuItem menuItem;
            string rootImagesUrlPath;
            bool isAdd;

            trees = new ObservableCollection<MenuItem>();

            rootImagesUrlPath = Factory.FactoryConfig.GetAttribute("Atomus", "RootImagesUrlPath");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                menuItem = new MenuItem()
                {
                    MenuID = (decimal)dataRow["MENU_ID"],
                    ParentMenuID = (decimal)dataRow["PARENT_MENU_ID"],
                    Name = (string)dataRow["NAME"],
                    Decription = (string)dataRow["DESCRIPTION"],

                    Image1 = dataRow["IMAGE_URL1"] != DBNull.Value && (string)dataRow["IMAGE_URL1"] != "" ?
                        (((string)dataRow["IMAGE_URL1"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL1"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL1"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image2 = dataRow["IMAGE_URL2"] != DBNull.Value && (string)dataRow["IMAGE_URL2"] != "" ?
                        (((string)dataRow["IMAGE_URL2"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL2"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL2"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image3 = dataRow["IMAGE_URL3"] != DBNull.Value && (string)dataRow["IMAGE_URL3"] != "" ?
                        (((string)dataRow["IMAGE_URL3"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL3"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL3"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image4 = dataRow["IMAGE_URL4"] != DBNull.Value && (string)dataRow["IMAGE_URL4"] != "" ?
                        (((string)dataRow["IMAGE_URL4"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL4"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL4"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    AssemblyID = dataRow["ASSEMBLY_ID"] == DBNull.Value ? -1 : (decimal)dataRow["ASSEMBLY_ID"],
                    Namespace = dataRow["NAMESPACE"].ToString(),
                    VisibleOne = dataRow["VISIBLE_ONE"] != DBNull.Value && (string)dataRow["VISIBLE_ONE"] != "N",
                    Children = new ObservableCollection<MenuItem>(),
                    DefImage = null,
                };
                

                isAdd = false;

                foreach (MenuItem treeViewItemTmp in trees)
                {
                    if (treeViewItemTmp.MenuID == menuItem.ParentMenuID)
                    {
                        treeViewItemTmp.Children.Add(menuItem);
                        isAdd = true;
                    }
                    else
                    {
                        var a = treeViewItemTmp.Children.OfType<MenuItem>().ToList().Where(x => x.MenuID == menuItem.ParentMenuID);

                        if (a.Count() == 1)
                        {
                            a.First().Children.Add(menuItem);
                            isAdd = true;
                        }
                    }
                }

                if (!isAdd)
                {
                    SetDefaultImage(menuItem);
                    trees.Add(menuItem);                    
                }
                        
            }

            return trees;
        }
        private async Task<ObservableCollection<TreeViewItem>> SetTree1(DataTable dataTable)
        {
            ObservableCollection<TreeViewItem> trees;
            TreeViewItem treeViewItem;
            MenuItem menuItem;
            StackPanel stackPanel;
            Image image;
            ImageSource imageSourceFolder;
            ImageSource imageSourceAssemblies;
            Label label;
            bool showNamespace;
            bool showAssemblyID;
            bool isAdd;
            string rootImagesUrlPath;

            imageSourceFolder = null;
            imageSourceAssemblies = null;
            showNamespace = false;
            showAssemblyID = false;
            trees = new ObservableCollection<TreeViewItem>();

            try
            {
                imageSourceFolder = new ImageBrush(await this.Core.GetAttributeMediaWebImage("FolderImage")).ImageSource;
                imageSourceAssemblies = new ImageBrush(await this.Core.GetAttributeMediaWebImage("AssembliesImage")).ImageSource;
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }

            try
            {
                showNamespace = this.Core.GetAttribute("ShowNamespace.RESPONSIBILITY_ID").Split(',').Contains(Config.Client.GetAttribute("Account.RESPONSIBILITY_ID").ToString());
                showAssemblyID = this.Core.GetAttribute("ShowAssemblyID.RESPONSIBILITY_ID").Split(',').Contains(Config.Client.GetAttribute("Account.RESPONSIBILITY_ID").ToString());
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }

            rootImagesUrlPath = Factory.FactoryConfig.GetAttribute("Atomus", "RootImagesUrlPath");

            foreach (DataRow dataRow in dataTable.Rows)
            {
                menuItem = new MenuItem()
                {
                    MenuID = (decimal)dataRow["MENU_ID"],
                    ParentMenuID = (decimal)dataRow["PARENT_MENU_ID"],
                    Name = (string)dataRow["NAME"],
                    Decription = (string)dataRow["DESCRIPTION"],

                    Image1 = dataRow["IMAGE_URL1"] != DBNull.Value && (string)dataRow["IMAGE_URL1"] != "" ?
                        (((string)dataRow["IMAGE_URL1"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL1"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL1"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image2 = dataRow["IMAGE_URL2"] != DBNull.Value && (string)dataRow["IMAGE_URL2"] != "" ?
                        (((string)dataRow["IMAGE_URL2"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL2"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL2"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image3 = dataRow["IMAGE_URL3"] != DBNull.Value && (string)dataRow["IMAGE_URL3"] != "" ?
                        (((string)dataRow["IMAGE_URL3"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL3"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL3"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    Image4 = dataRow["IMAGE_URL4"] != DBNull.Value && (string)dataRow["IMAGE_URL4"] != "" ?
                        (((string)dataRow["IMAGE_URL4"]).Contains("http") ?
                            new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri((string)dataRow["IMAGE_URL4"]))).ImageSource
                            : new ImageBrush(await this.Core.GetAttributeMediaWebImage(new Uri(string.Format("{0}{1}", (string)dataRow["IMAGE_URL4"], rootImagesUrlPath)))).ImageSource)
                        : null,

                    AssemblyID = dataRow["ASSEMBLY_ID"] == DBNull.Value ? -1 : (decimal)dataRow["ASSEMBLY_ID"],
                    Namespace = dataRow["NAMESPACE"].ToString(),
                    VisibleOne = dataRow["VISIBLE_ONE"] != DBNull.Value && (string)dataRow["VISIBLE_ONE"] != "N"
                };

                stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

                if (menuItem.Image1 != null)
                    image = new Image() { Source = menuItem.Image1 };
                else
                {
                    if (menuItem.AssemblyID > 0)
                        image = new Image() { Source = imageSourceAssemblies };
                    else
                        image = new Image() { Source = imageSourceFolder };
                }
                stackPanel.Children.Add(image);

                label = new Label() { Content = showAssemblyID ? string.Format("{0} ({1}.{2})", menuItem.Name.Translate(), menuItem.MenuID, menuItem.AssemblyID) : menuItem.Name.Translate() };
                label.ToolTip = showNamespace ? string.Format("{0} {1}", menuItem.Decription.Translate(), menuItem.Namespace) : menuItem.Decription.Translate();
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Tag = menuItem;
                label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                stackPanel.Children.Add(label);


                treeViewItem = new TreeViewItem() { Header = stackPanel, Tag = menuItem };

                if (menuItem.AssemblyID > 0)
                    label.MouseDoubleClick += Label_MouseDoubleClick;

                if (trees.Count > 0)
                {
                    isAdd = false;

                    foreach (TreeViewItem treeViewItemTmp in trees)
                    {
                        if ((treeViewItemTmp.Tag as MenuItem).MenuID == menuItem.ParentMenuID)
                        {
                            treeViewItemTmp.Items.Add(treeViewItem);
                            isAdd = true;
                        }
                        else
                        {
                            var a = treeViewItemTmp.Items.OfType<TreeViewItem>().ToList().Where(x => (x.Tag as MenuItem).MenuID == menuItem.ParentMenuID);

                            if (a.Count() == 1)
                            {
                                a.First().Items.Add(treeViewItem);
                                isAdd = true;
                            }
                        }
                    }

                    if (!isAdd)
                        trees.Add(treeViewItem);
                }
                else
                    trees.Add(treeViewItem);
            }

            return trees;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label label;
            MenuItem menuItem;

            label = (sender as Label);

            if (label.Tag != null && label.Tag is MenuItem)
            {
                menuItem = (label.Tag as MenuItem);
                (this.Core as IAction).ControlAction(this, "Menu.OpenControl", new object[] { menuItem.MenuID, menuItem.AssemblyID, menuItem.VisibleOne });
            }
        }

        /// <summary>
        /// 기본 메뉴 이미지 설정 
        /// </summary>
        /// <param name="menuItem"></param>
        private void SetDefaultImage(MenuItem menuItem)
        {
            if (menuItem == null)
                return;

            string strKeyName = "";
            if (menuItem.Name.Contains("개발"))
            {
                strKeyName = "Image_Menu_Development_N";
            }
            else if (menuItem.Name.Contains("메뉴 리스트"))
            {
                strKeyName = "Image_Menu_MenuList_N";
            }
            else if (menuItem.Name.Contains("메뉴,툴바 권한"))
            {
                strKeyName = "Image_Menu_MenuResponsibility_N";
            }
            else if (menuItem.Name.Contains("권한"))
            {
                strKeyName = "Image_Menu_Responsibility_N";
            }
            else if (menuItem.Name.Contains("계정"))
            {
                strKeyName = "Image_Menu_Account_N";
            }
            else if (menuItem.Name.Contains("지역화"))
            {
                strKeyName = "Image_Menu_Localization_N";
            }
            else if (menuItem.Name.Contains("팩토리"))
            {
                strKeyName = "Image_Menu_Factory_N";
            }
            else if (menuItem.Name.Contains("트렌드"))
            {
                strKeyName = "Image_Menu_TrendAndData_N";
            }
            else if (menuItem.Name.Contains("기술통계"))
            {
                strKeyName = "Image_Menu_Statistics_N";
            }
            else if (menuItem.Name.Contains("구매"))
            {
                strKeyName = "Image_Menu_AMX_N";
            }
            else if (menuItem.Name.Contains("출하"))
            {
                strKeyName = "Image_Menu_AMT_N";
            }
            else if (menuItem.Name.Contains("품질"))
            {
                strKeyName = "Image_Menu_AMU_N";
            }
            else if (menuItem.Name.Contains("생산"))
            {
                strKeyName = "Image_Menu_Production_N";
            }

            try
            {
                if (!strKeyName.IsNullOrWhiteSpace())
                {
                    System.Windows.Media.Imaging.BitmapImage bitimg =
                                (System.Windows.Media.Imaging.BitmapImage)Application.Current.FindResource(strKeyName);
                    Image image = new Image() { Source = bitimg };
                    menuItem.DefImage = image.Source;
                }
            }
            catch (Exception ex)
            {
                DiagnosticsTool.MyTrace(ex);
            }
        }

        private void ExpandAllProcess()
        {
            (this.Core as IAction).ControlAction(this, "ExpandAllNodes", true);
        }
        private void CollapseAllProcess()
        {
            (this.Core as IAction).ControlAction(this, "CollapseAllNodes", false);
        }
        #endregion

        #region ETC

        /// <summary>
        /// 필터 처리된 메뉴리스트 갱신
        /// </summary>
        public void MenuFilterProcess()
        {
            System.Windows.Data.CollectionViewSource.GetDefaultView(this.MenuData).Refresh();
        }

        /// <summary>
        /// 메뉴 필터링
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool MenuFilter(object item)
        {
            MenuItem Item = item as MenuItem;

            if (this.FilterText == null || Item.Name.Contains(this.FilterText))
                return true;

            if (Item.Children.Count > 0)
            {
                foreach (MenuItem child in Item.Children)
                {
                    if (child.Name.Contains(this.FilterText))
                    {
                        if (child.Name.Contains(this.FilterText))
                            return true;
                    }
                }
            }

            return false;
        }
        #endregion
    }
}