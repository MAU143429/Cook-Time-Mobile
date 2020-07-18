﻿using System;
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
    /// This class allows users to see a company member list. In this page the user can see all the members in this company and the profile view for each one
    /// @author Mauricio C.
    /// </summary>
    public partial class CompanyMemberList : ContentPage
    {

        /// <summary>
        /// This constructor execute CompanyMemberList partial class 
        /// @author Mauricio C.
        /// </summary>
        public CompanyMemberList()
        {
            InitializeComponent();
        }
    }
}