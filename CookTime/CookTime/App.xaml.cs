﻿using CookTime.User;
using CookTime.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new Views.LoginPage());


            
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
