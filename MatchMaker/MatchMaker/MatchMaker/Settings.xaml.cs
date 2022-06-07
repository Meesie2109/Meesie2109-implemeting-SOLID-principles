using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchMaker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
            Picker picker = (Picker)this.FindByName("CalculatorSetting");
            picker.SelectedItem = Preferences.Get("CalculatorSetting", string.Empty);
        }

        private void CalculatorSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;            
            Preferences.Set("CalculatorSetting", (string)picker.SelectedItem);            
        }
    }
}