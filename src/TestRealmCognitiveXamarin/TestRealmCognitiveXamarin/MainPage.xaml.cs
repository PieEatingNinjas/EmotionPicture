using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRealmCognitiveXamarin.ViewModels;
using Xamarin.Forms;

namespace TestRealmCognitiveXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = new MainPageViewModel();
            InitializeComponent();
        }
    }
}
