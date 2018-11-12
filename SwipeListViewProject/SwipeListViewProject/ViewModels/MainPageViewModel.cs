using Prism.Mvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SwipeListViewProject
{
    public class MainPageViewModel : BindableBase
    {
        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public MainPageViewModel()
        {
            int s0 = 0;

            Items = new ObservableCollection<string>();

            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Items.Add((s0 + i).ToString());
                }
            });
        }
    }
}
