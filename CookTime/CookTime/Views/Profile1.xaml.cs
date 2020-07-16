﻿
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile1 : ContentPage
    {
        public Profile1()
        {
            InitializeComponent();
            Update();
            
        }
        private void Update()
        {
            
         

        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR());

        }
        private void Create2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateC());
            


        }
        private void CompanyP(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyProfile());

        }

        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }


    }
}