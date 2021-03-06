﻿
using CookTime.REST_API_RecipeListModel;
using CookTime.REST_API_RecipeModel;
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class show the user profile, and allows create companies and recipes
    /// @author Mauricio C.
    /// </summary>
    public partial class Profile1 : ContentPage
    {

        /// <summary>
        /// This constructor execute Profile1 partial class 
        /// @author Mauricio C.
        /// </summary>
        /// 
        CookTime.REST_API_UserModel.User myprofile = LoginPage.CURRENTUSER;
        ArrayList RecipeList;
        public Profile1()
        {
            InitializeComponent();
            Pull_Search_Request();

            if (myprofile.Image == null)
            {
                profileimg.Source = "defaultimg.jpg";
            }
            else
            {
                profileimg.Source = myprofile.Image;
            }
            
            username.Text = myprofile.Name;
            posts.Text = Convert.ToString(myprofile.Recipes.Count);
            followers.Text = Convert.ToString(myprofile.Followers.Count);
            following.Text = Convert.ToString(myprofile.Following.Count);
            LoginPage.updateUser();
            if (LoginPage.CURRENTUSER.Hascompany == true)
            {
                CreateCompany.IsEnabled = false;
            }
            else
            {
                CompanyProfile.IsEnabled = false;
            }
        }
        /// <summary>
        /// This method brings all the user's recipes from the server and starts the list travel
        /// @author Jose A.
        /// </summary>
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getRecipe/" + LoginPage.CURRENTUSER.Email + "/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel listofrecipes = RecipeListModel.FromJson(json);
            Console.WriteLine("RESULT" + json);
            if (listofrecipes.Length == 0)
            {
                return;
            }
            else
            {
                StartList(listofrecipes);
            }


        }
        /// <summary>
        /// This method starts the list traversing by adding the head.data to the arraylist
        /// @author Jose A.
        /// </summary>
        public void StartList(RecipeListModel model)
        {
            InitList();
            if (model.Head.Next != null)
            {
                RecipeList.Add(model.Head.Data);
                ListAdd(model.Head.Next);
            }
            else
            {
                RecipeList.Add(model.Head.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method is for traversing the list and adding every Data object it encounters
        /// @author Jose A.
        /// </summary>
        private void ListAdd(CookTime.REST_API_RecipeListModel.Next next)
        {
            if (next.NextNext != null)
            {
                RecipeList.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                RecipeList.Add(next.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method is for traversing the list and adding every Data object it encounters
        /// @author Jose A.
        /// </summary>
        private void ListAddRest(CookTime.REST_API_RecipeListModel.Head head)
        {
            if (head.Next != null)
            {
                RecipeList.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                RecipeList.Add(head.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method binds every data on the arraylist into the listview (visua aid)
        /// @author Jose A.
        /// </summary>
        public void ListReturn()
        {
            ListaR.ItemsSource = RecipeList;
            Console.WriteLine(RecipeList[0]);
        }
        /// <summary>
        /// This method instances the used arraylist
        /// @author Jose A.
        /// </summary>
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

        /// <summary>
        /// This method is used to change the current page to Home page
        /// @author Mauricio C.
        /// </summary>
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
        /// <summary>
        /// This method is used to change the current page to Search page
        /// @author Mauricio C.
        /// </summary>
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        /// <summary>
        /// This method is used to update profile1 page
        /// @author Mauricio C.
        /// </summary>
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method is used to change the current page to Create Recipe page
        /// @author Mauricio C.
        /// </summary>
        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR(1));

        }
        /// <summary>
        /// This method is used to change the current page to Create Company page
        /// @author Mauricio C.
        /// </summary>
        private void Create2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateC());
        }
        /// <summary>
        /// This method is used to change the current page to CompanyProfile page
        /// @author Mauricio C.
        /// </summary>
        private void CompanyP(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new CompanyProfile());

        }
        /// <summary>
        /// This method is used to change the current page to ChnagePhoto page
        /// @author Mauricio C.
        /// </summary>

        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhoto(1));

        }
        /// <summary>
        /// This method is used to change the current page to ChangePassword page
        /// @author Mauricio C.
        /// </summary>
        private void Change_Pass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }

        /// <summary>
        /// This method brings every recipe from an especific user sorted by difficulty
        /// @author Jose A.
        /// </summary>
        private async void Sort_Difficulty(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getDifficulties/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);

        }
        /// <summary>
        /// This method brings every recipe from an especific user sorted by date
        /// @author Jose A.
        /// </summary>
        private async void Sort_Date(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getDates/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);
        }
        /// <summary>
        /// This method brings every recipe from an especific user sorted by ratings
        /// @author Jose A.
        /// </summary>
        private async void Sort_Rating(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getRatings/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);
        }



        /// <summary>
        /// This method is used to change the current page to ViewRecipe page
        /// @author Mauricio C.
        /// </summary>
        public void View_Recipe(object sender, EventArgs e)
        {
            Recipe item = (Recipe)ListaR.SelectedItem;
            Navigation.PushAsync(new ViewRecipe(item));
        }
        /// <summary>
        /// This method updates the data of the user currently using the app.
        /// @author Jose A.
        /// </summary>
        public async void updateuser(object sender, EventArgs e)
        {

           
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/email/getUser/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            myprofile = CookTime.REST_API_UserModel.User.FromJson(json);
            Console.WriteLine(myprofile.Image);
            if (LoginPage.CURRENTUSER.Hascompany == true)
            {
                CreateCompany.IsEnabled = false;
            }
            else
            {
                CompanyProfile.IsEnabled = false;
            }
            await Navigation.PushAsync(new Profile1());
        }


    }
}