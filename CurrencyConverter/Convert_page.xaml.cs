using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Media.SpeechSynthesis;
using SQLite;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CurrencyConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    class currency
    {
        public string C_Name { get; set; }
        public float value { get; set; }
    }
    public sealed partial class Convert_page : Page
    {
        SQLiteAsyncConnection con;
        int num;
        float value1, value2;
        
        public  Convert_page()
        {
            this.InitializeComponent();
             
              table();
              rates();
          //  del();
           
          
             //ddata();
          
           
        }
        public async void table()
        {
            var path = ApplicationData.Current.LocalFolder.Path + @"\MyDb";
            con = new SQLiteAsyncConnection(path);
            await con.CreateTableAsync<currency>();
       
        }
        private async void ddata()
        {
            num = await con.ExecuteAsync("select * from currency where value >= 1;");

            MessageDialog msg = new MessageDialog("This is total number of data into currency table", num.ToString());
            await msg.ShowAsync();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Combo_B_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String name = Combo_B.SelectedItem.ToString();
            StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Assets\\"+name+".jpg");
            var a = await file.OpenAsync(FileAccessMode.Read);
            BitmapImage img = new BitmapImage();

            img.SetSource(a);
           Img_B.Source = img;
         //  string cmb = Combo_A.SelectedItem.ToString();
           var valu = await con.QueryAsync<currency>("select value from currency where C_Name = '" + name + "';");
           foreach (var va in valu)
           {
               value2 = va.value;
           }
         
           int val = Combo_B.SelectedIndex;
           switch (val)
           {
               case 0:
                   txt_B.Text = "104.45";
                   break;
               case 1:
                   txt_B.Text = "134.92";
                   break;
               case 2:
                   txt_B.Text = "154.87";
                   break;
                       

           }
             }

        private async void Combo_A_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                String name = Combo_A.SelectedItem.ToString();
                StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("Assets\\" + name + ".jpg");
                var b = await file.OpenAsync(FileAccessMode.Read);
                BitmapImage img = new BitmapImage();

                img.SetSource(b);
                Img_A.Source = img;
         
                //string cmb = Combo_A.SelectedItem.ToString();
                var val = await con.QueryAsync<currency>("select value from currency where C_Name = '" + name + "';");
                foreach (var va in val)
                {
                    value1 = va.value;
                }
         
            }
            catch (Exception edd)
            {
               
                MessageDialog da = new MessageDialog("There are some exceptions or not a valid currency","Invalid Currency");
                da.ShowAsync();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Modify));
        }
        private async void CurrencyData()
        {

            await con.QueryAsync<currency>("Insert into currency (C_Name,value) values ('Yen',14.4) , ('Egypt',38.45) , ('France',92.87) , ('Euro',134.92) , ('Indonesia',7.65) , ('Soomalia',12.32) , ('Pakistani',1.00) , ('Indian',1.00) , ('Riyal',27.78) , ('Darham',28.15) , ('US',104.52) , ('British',140.25);");
            //await con.QueryAsync<currency>("Insert into currency (C_Name,value) values ('US',104.52) , ('British',140.25);"); 
                //, ('Euro',134.92) , ('Indonasia',7.65) , ('Soomalia',12.32) , ('Pak Rupee',1.00) , ('Indain Rupee',1.45) , ('Saudi Riyal',27.78) , ('Dubai_Darham',28.15);"); 
        

            //, (" + TEgypt.Text + ",00.0)  , (" + TFrance.Text + ",00.0)  , (" + TEuro.Text + ",00.0)  , (" + TIndonasia.Text + ",00.0)  , (" + TSoomalia.Text + ",00.0)  , (" + TPakRupee.Text + ",00.0)  , (" + TIndianRupee.Text + ",00.0)  , (" + TSaudiRiyal.Text + ",00.0)  , (" + TDubaiDarham.Text + ",00.0);  ");
        }
       // private async void Convertion()
        //{
            //int combo_a = Combo_A.SelectedIndex;
            //int combo_b = Combo_B.SelectedIndex;

            //await con.QueryAsync<currency>("Select * from currency where ");
            //if(Convert.ToInt32(txt_A.Text) >= Convert.ToInt32(txt_B.Text)){

 //           }

//        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
             try
            {
                float text1 = Convert.ToInt32(txt_A.Text);
                float res, result;
                string opt;

                res = value1 * text1;
                if (value1 > value2)
                {
                    result = res / value2;

                    txt_B.Text = Math.Round(result, 2).ToString();
                    opt = "x";

                    //MessageDialog md = new MessageDialog("value operator is = "+opt+"", value1.ToString());
                    //await md.ShowAsync();
                    

                }
                else
                {
                    result = res / value2;
                    txt_B.Text = Math.Round(result, 2).ToString();
                    opt = "/";
                //    MessageDialog md = new MessageDialog("value operator is = "+opt+"", value1.ToString());
                //    await md.ShowAsync();

                //
                }
            }catch(Exception ew){
                 MessageDialog ms = new MessageDialog("Some eror accured", ew.Message);
                ms.ShowAsync();

            }

            //MessageDialog md = new MessageDialog("value", value1.ToString());
            //await  md.ShowAsync();
            
            
            //if (value1)
            //{
            //    //res = text1 / value2;
            //    //txt_B.Text = res.ToString();
            //    MessageDialog ms = new MessageDialog("value", value1.ToString());
            //    ms.ShowAsync();

            //}
            //else if (value1 < value2)
            //{

            //    res = text1 * value2;
            //    txt_B.Text = res.ToString();

            //}
            //else
            //{
            //    MessageDialog mm = new MessageDialog("Please re-enter a valid value in integer:", "Error");
            //    await mm.ShowAsync();
            //}
            
        }
        public async void del()
        {
            await con.QueryAsync<currency>("delete from currency where value>0;");
            MessageDialog da = new MessageDialog("", "Data has been deleted");
            await da.ShowAsync();
        }
        public async void rates()
        {


            try
            {
                var val = await con.QueryAsync<currency>("select * from currency;");

                if (val.Count < 1)
                {

                    
                    CurrencyData();
                    foreach (var va in val)
                    {


                        list.Items.Add("  " + va.C_Name + "  <===========>  " + va.value);

                    }
                    
                }
                else
                {
                    foreach (var va in val)
                    {


                        list.Items.Add("  " + va.C_Name + "  <===========>  " + va.value);

                    }
                    
                }
            }
            catch (Exception er)
            {
                MessageDialog da = new MessageDialog("Some error accured in Forex rates view.", er.Message);
                da.ShowAsync();
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
     }
 }

        
    

