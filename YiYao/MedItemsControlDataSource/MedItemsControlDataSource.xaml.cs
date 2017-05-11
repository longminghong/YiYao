//   *********  请勿修改此文件   *********
//   此文件由设计工具再生成。更改
//   此文件可能会导致错误。
namespace Expression.Blend.SampleData.MedItemsControlDataSource
{
    using System; 
    using System.ComponentModel;

// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
    internal class MedItemsControlDataSource { }
#else

    public class MedItemsControlDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public MedItemsControlDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("/YiYao;component/SampleData/MedItemsControlDataSource/MedItemsControlDataSource.xaml", UriKind.RelativeOrAbsolute);
                System.Windows.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private ItemCollection _Collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return this._Collection;
            }
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _name = string.Empty;

        public string name
        {
            get
            {
                return this._name;
            }

            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged("name");
                }
            }
        }

        private string _drugstype = string.Empty;

        public string drugstype
        {
            get
            {
                return this._drugstype;
            }

            set
            {
                if (this._drugstype != value)
                {
                    this._drugstype = value;
                    this.OnPropertyChanged("drugstype");
                }
            }
        }

        private string _regular = string.Empty;

        public string regular
        {
            get
            {
                return this._regular;
            }

            set
            {
                if (this._regular != value)
                {
                    this._regular = value;
                    this.OnPropertyChanged("regular");
                }
            }
        }
    }

    public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
    { 
    }
#endif
}
