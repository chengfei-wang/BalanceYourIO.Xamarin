﻿using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BalanceYourIO.DataProvider;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace BalanceYourIO
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        private static string DB_FILE = "byio.db3";

        static Database _database;

        public static Database Database => _database ??=
            new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                DB_FILE));


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}