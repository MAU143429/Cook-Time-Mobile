﻿using CookTime.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to see a summary of recipes details
    /// @author Mauricio C.
    /// </summary>
    public partial class ViewRecipe : ContentPage
    {
        ArrayList RecipeList;
        /// <summary>
        /// This constructor execute ViewRecipe partial class 
        /// @author Mauricio C.
        /// </summary>
        public ViewRecipe(CookTime.REST_API_RecipeModel.Recipe recipe)
        {
            InitializeComponent();
            InitList();
            StartPage(recipe);
        }
        /// <summary>
        /// This method take the data of recipe and add it to the RecipeList
        /// @author Mauricio C.
        /// </summary>
        private void StartPage(CookTime.REST_API_RecipeModel.Recipe Recipe)
        {
            RecipeList.Add(Recipe);
            ListReturn();
        }
        /// <summary>
        /// This method take one recipe and delete it of MyMenu
        /// @author Mauricio C.
        /// </summary>
        private void DeleteR(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowSearch());
            DisplayAlert("COOKTIME", "RECIPE HAS BEEN DELETED", "ACCEPT");
        }
        /// <summary>
        /// This method take one recipe and share in newsfeed page
        /// @author Mauricio C.
        /// </summary>
        private void ShareRecipe (object sender, EventArgs e)
        {
           
            DisplayAlert("COOKTIME", "RECIPE HAS BEEN SHARED", "ACCEPT");
        }

        /// <summary>
        /// This method allow users to comment in one recipe
        /// @author Mauricio C.
        /// </summary>
        private void CommentR(object sender, EventArgs e)
        {


            var commentValidate = Comment.Text;
            if (!string.IsNullOrEmpty(commentValidate))
            {
                DisplayAlert("COOKTIME", "YOUR COMMENT HAS BEEN PUBLISHED", "ACCEPT");
                DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario ha comentado en tu receta!");
                Navigation.PushAsync(new ShowSearch());

            }
            else
            {
                DisplayAlert("COOKTIME", "ENTER A COMMENT TO CONTINUE ", "ACCEPT");
            }

        }
        public void ListReturn()
        {
            ListaRecipe.ItemsSource = RecipeList;
            Console.WriteLine(RecipeList[0]);
        }
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

    }
}