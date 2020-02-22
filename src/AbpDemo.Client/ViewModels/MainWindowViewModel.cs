using System;
using System.Collections.Generic;
using System.Text;
using AbpDemo.Business;

namespace AbpDemo.Client
{
    public class MainWindowViewModel:ViewModelBase
    {
        private readonly IGoodsAppService _goodsAppService;
        public MainWindowViewModel(IGoodsAppService goodsAppService)
        {
            _goodsAppService = goodsAppService;
            InitData();
        }

        #region Properties
        private List<DetailGoodsDto> _goodsList;

        public List<DetailGoodsDto> GoodsList
        {
            get { return _goodsList; }
            set
            {
                if (_goodsList != value)
                {
                    _goodsList = value;
                    OnPropertyChanged("GoodsList");
                }
            }
        }
        #endregion

        #region Commands

        #endregion

        #region Methods
        private void InitData()
        {
            GoodsList = _goodsAppService.All();
        } 
        #endregion

    }
}
