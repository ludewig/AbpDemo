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

            AddCommand = new DelegateCommands<string>(Add);
            EditCommand = new DelegateCommands<DetailGoodsDto>(Edit);
            DeleteCommand = new DelegateCommands<DetailGoodsDto>(Delete);

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
        public DelegateCommands<string> AddCommand { get; set; }
        public DelegateCommands<DetailGoodsDto> EditCommand { get; set; }
        public DelegateCommands<DetailGoodsDto> DeleteCommand { get; set; }
        #endregion

        #region Methods
        private void InitData()
        {
            GoodsList = _goodsAppService.All();
        }

        private void Add(string obj)
        {

        }

        private void Edit(DetailGoodsDto obj)
        {
            
        }

        private void Delete(DetailGoodsDto obj)
        {
            int num=_goodsAppService.Delete(new string[] { obj.Id }).Result;
        }
        #endregion

    }
}
