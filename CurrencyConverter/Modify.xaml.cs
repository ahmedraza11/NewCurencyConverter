using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Modify : Page
    {
        string cur_Name;
        SQLiteAsyncConnection con;
        public Modify()
        {
            this.InitializeComponent();
            table();
            rate();
        }
        public async void table()
        {
            var path = ApplicationData.Current.LocalFolder.Path + @"\MyDb";
            con = new SQLiteAsyncConnection(path);
            await con.CreateTableAsync<currency>();

        }
        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lst.SelectedIndex;

            switch (index)
            {
                case 0:
                txtName.Text = "France";
                cur_Name = "France";
                break;
                case 1:
                txtName.Text = "Pakistani Rupee";
                cur_Name = "Pakistani";
                break;
                case 2:
                txtName.Text = "Indian Rupee";
                cur_Name = "Indian";
                break;
                case 3:
                txtName.Text = "Soomalia";
                cur_Name = "Soomalia";
                break;
                case 4:
                txtName.Text = "China Yen";
                cur_Name = "Yen";
                break;
                case 5:
                txtName.Text = "Egypt";
                cur_Name = "Egypt";
                break;
                case 6:
                txtName.Text = "Euro";
                cur_Name = "Euro";
                break;
                case 7:
                txtName.Text = "Indonesia";
                cur_Name = "Indonesia";
                break;
                case 8:
                txtName.Text = "Saudi Riyal";
                cur_Name = "Riyal";
                break;
                case 9:
                txtName.Text = "Dubai Darham";
                cur_Name = "Darham";
                break;
            
           
          
            }
           
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Convert_page));
        }
        private async void rate()
        {
            try
            {
                var val = await con.QueryAsync<currency>("select * from currency;");
                foreach (var va in val)
                {

                    list1.Items.Add("  "+va.C_Name + "   <==============>   " + va.value);

                }
            }
            catch (Exception er)
            {
                MessageDialog da = new MessageDialog("Some error accured in Forex rates view.", er.Message);
                da.ShowAsync();
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                float value = float.Parse(txt_value.Text);
                await con.QueryAsync<currency>("Update currency set value = " + value + " where C_Name='" + cur_Name + "';");
                var syn = new SpeechSynthesizer();
                SpeechSynthesisStream stream = await syn.SynthesizeTextToStreamAsync(" Currency Updated!" + cur_Name + " Sucessfully Updated");
                MediaElement ele = new MediaElement();
                ele.SetSource(stream, stream.ContentType);
                ele.Play();

                MessageDialog da = new MessageDialog("(" + cur_Name + ") Sucessfully Updated.", "Currency Update");
                await da.ShowAsync();
                rate();
            }
            catch (Exception err)
            {

                MessageDialog dra = new MessageDialog("Some Error acured please set value in Float", err.Message);
                dra.ShowAsync();
            }
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Convert_page));
        }
    }
}
