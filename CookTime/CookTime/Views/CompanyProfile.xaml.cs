﻿
using CookTime.REST_API_CompanyModel;
using CookTime.REST_API_RecipeListModel;
using CookTime.User;
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
    /// <summary>
    /// This class allows users to manage the company account, create recipes and share recipes with other company members
    /// @author Jose A.
    /// </summary>
    public partial class CompanyProfile : ContentPage
    {
        /// <summary>
        /// This constructor execute CompanyProfile partial class and start a new pull search request to the server 
        /// @author Jose A.
        /// </summary>
        ArrayList RecipeList;
        public static Company mycompany;
        

        public CompanyProfile()
        {
            InitializeComponent();
            Pull_Company_Request();
            Pull_Search_Request();

        }



        /// <summary>
        /// This method create a new HTTP client and execute async method with the server to get the company data 
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

        private async void Pull_Company_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getCompany/user/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("SERVER:" + json);
            Company currentcompany = Company.FromJson(json);
            Console.WriteLine("RESULTTTTTTTT"+currentcompany);
            mycompany = currentcompany;
            profileimg.Source = mycompany.Logo;
            companyname.Text = mycompany.Name;
            posts.Text = Convert.ToString(mycompany.Recipes.Count);
            followers.Text = Convert.ToString(mycompany.Followers.Count);
            following.Text = Convert.ToString(mycompany.Following.Count);
            companyshedule.Text = mycompany.Schedule;
            companynumber.Text = Convert.ToString(mycompany.Number);


        }
        /// <summary>
        /// This method take the first element and add it
        /// @author Jose A.
        /// </summary>
        public void StartList(RecipeListModel model)
        {
            
            InitList();
            if(model.Head.Next != null) {
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
        /// This method take the data and add it to the CompanyList 
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
        /// This method verify the current data is the last element in json file, if it is the last element the method add it and return the finally list
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
        /// This method display the list
        /// @author Jose A.
        /// </summary>
        public void ListReturn()
        {
            ListaRCP.ItemsSource = RecipeList;
            Console.WriteLine(RecipeList[0]);
        }
        /// <summary>
        /// This method inicializate ArrayList
        /// @author Jose A.
        /// </summary>
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

        /// <summary>
        /// This method is used to change the current page to Create Recipe page
        /// @author Mauricio C.
        /// </summary>

        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR());


        }
        /// <summary>
        /// This method is used to change the current page to Company Member List page
        /// @author Mauricio C.
        /// </summary>
        private void View_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMemberList());

        }
        /// <summary>
        /// This method is used to change the current page to Change Photo page
        /// @author Mauricio C.
        /// </summary>
        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhoto(2));

        }
        
        /// <summary>
        /// This method is used to change the current page to AddMember page
        /// @author Mauricio C.
        /// </summary>
        private void Add_Member(object sender, EventArgs e)
        {
        
            Navigation.PushAsync(new AddMember());

        }
        private async void Sort_Difficulty(object sender, EventArgs e)
        {


        }

        private async void Sort_Date(object sender, EventArgs e)
        {
            
        }

        private async void Sort_Rating(object sender, EventArgs e)
        {




        }

    }
}