using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeliveryApp
{
    public class WaterGood
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
    }
    
    public sealed partial class MainPage : ContentPage
    {
        public static SortedDictionary<string, WaterGood> Products = new SortedDictionary<string, WaterGood>();
        public List<WaterGood> Drinks { get; set; }
        public static MainPage Instance { get; set; }

        public MainPage()
        {
            Instance = this;

            Title = "Cart";
            
            InitializeComponent();

            BindingContext = this;
        }

        private void buttonDelete_clicked(object sender, EventArgs e)
        {
            var item = sender as Button;
            var listItem = Products.First(x => x.Value.Name == item.CommandParameter.ToString()).Key;

            Products.Remove(listItem);

            UpdateCart();
        }

        private async void buttonOrder_clicked(object sender, EventArgs e)
        {
            orderButton.IsEnabled = false;

            if (await DisplayAlert("Order", "Are you sure?", "Yes", "No"))
            {
                await DisplayAlert("Order", "Good", "Ok");
            }
            else
            {
                await DisplayAlert("YOU", "Bad Boy", "Ohh..");
            }

            orderButton.IsEnabled = true;
        }

        public void UpdateCart()
        {
            goods_list.ItemsSource = Products.Values;
        }

        void plusButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BuyPage());
        }
    }
}
