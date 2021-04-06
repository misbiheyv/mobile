using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyPage : ContentPage
    {
        public class Water
        {
            public string Name;
            public string ImageUrl;
        }

        List<Water> Drinks = new List<Water>()
        {
            new Water()
            {
                Name = "Water",
                ImageUrl = "wat.jpg"
            },
            new Water()
            {
                Name = "Cola",
                ImageUrl = "cock.jpg"
            },
            new Water()
            {
                Name = "Juice",
                ImageUrl = "JacqueFresko.jpg"
            }
        };

        public BuyPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetTitleView(this, SetBackView("X"));
            InitializeComponent();

            picker.ItemsSource = Drinks.Select(x => x.Name).ToList();
            picker.SelectedIndex = 0;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            MainPage.Instance.goods_list.ItemsSource = MainPage.Products.Values;
        }

        private void stepper_ValueChanged(object _sender, ValueChangedEventArgs _e)
        {
            count.Text = (_sender as Stepper)?.Value.ToString();
        }

        StackLayout SetBackView(string backButtonContent)
        {
            Button backButton = new Button()
            {
                Text = backButtonContent,
                TextColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.None,
                BackgroundColor = Color.Transparent,
                Margin = new Thickness(-20, 0, 0, 0),
            };
            backButton.Clicked += BackButton_Clicked;

            StackLayout stackLayout = new StackLayout
            {
                Children = { 
                    backButton, 
                    new Label{
                        HorizontalTextAlignment=TextAlignment.Center,
                        VerticalTextAlignment=TextAlignment.Center,
                        TextColor=Color.White,
                        BackgroundColor=Color.Transparent,
                    },
                },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
            };

            return stackLayout;
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            count.Text = "0";
            stepper.Value = 0;

            IMG.Source = ImageSource.FromFile(Drinks.First(a => a.Name == picker.SelectedItem?.ToString()).ImageUrl);
            
            if (MainPage.Products.ContainsKey(picker.SelectedItem?.ToString()))
            {
                stepper.Value = MainPage.Products[picker.SelectedItem?.ToString()].Count;
                count.Text = MainPage.Products[picker.SelectedItem?.ToString()].Count.ToString();
            }
        }

        private void count_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var temp = String.Join("", count.Text.Where(x => Char.IsDigit(x)));
                var res = int.Parse(temp);

                if (res > 99999)
                    throw new Exception();

                count.Text = res.ToString();
                stepper.Value = res;
            }
            catch
            {
                count.Text = stepper.Value.ToString();
            }
        }

        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            button.IsEnabled = false;

            MainPage.Products[picker.SelectedItem?.ToString()] = new WaterGood()
            {
                Name = picker.SelectedItem?.ToString(),
                Count = int.Parse(count.Text),
                ImageUrl = Drinks.First(a => a.Name == picker.SelectedItem?.ToString()).ImageUrl
            };

            await DisplayAlert("Confirm", "Successful", "OK");

            button.IsEnabled = true;
        }
    }
}