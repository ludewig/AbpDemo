using Abp.Dependency;
using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;
using AbpDemo.Business;

namespace AbpDemo.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, ISingletonDependency
    {
        private MainWindowViewModel _viewModel;
        private readonly IGoodsAppService _goodsAppService;

        public MainWindow(IGoodsAppService goodsAppService)
        {
            _goodsAppService = goodsAppService;
            _viewModel = new MainWindowViewModel(goodsAppService);
            this.DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
