using MatchMaker.Abstract;
using MatchMaker.Concrete;
using Nancy.TinyIoc;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatchMaker
{
    public partial class App : Application
    {

        private static TinyIoCContainer _container = new TinyIoCContainer();

        public App()
        {
            InitializeComponent();

            _container.Register<IASCIICalculator, ASCIICalculator>();
            MainPage = new NavigationPage(new MainPage(_container.Resolve<IASCIICalculator>()));            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
