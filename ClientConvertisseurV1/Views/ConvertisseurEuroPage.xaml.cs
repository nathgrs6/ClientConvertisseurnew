// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WSConvertisseur.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private ObservableCollection<Devise> lesDevises;

        public ObservableCollection<Devise> LesDevises
        {
            get { return lesDevises; }
            set { lesDevises = value; OnPropertyChanged("LesDevises"); }
        }

        private Devise selectedDevise;

        public event PropertyChangedEventHandler PropertyChanged;

        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set { selectedDevise = value; OnPropertyChanged(nameof(selectedDevise)); }
        }

        private double montant;

        public double Montant
        {
            get { return montant; }
            set { montant = value; OnPropertyChanged(nameof(montant)); }
        }

        private double convertedAmount;

        public double ConvertedAmount
        {
            get { return convertedAmount; }
            set { convertedAmount = value; OnPropertyChanged("ConvertedAmount"); }
        }




        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }



        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7008/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if(result == null)
            {
                DisplayNoApiDialog();
               return ;
                
            }
            else
            {
                LesDevises = new ObservableCollection<Devise>(result);
            }
        }

        private void buttonConvert_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDevise != null)
            {
                ConvertedAmount = Montant * SelectedDevise.Taux;
                
            }
            else DisplayNoDeviseDialog(); 
            
        }
        public async void DisplayNoDeviseDialog()
        {
            ContentDialog noDeviseDialog = new ContentDialog
            {
                Title = "Erreur",
                Content = "Vous devez selectionner un devise.",
                CloseButtonText = "Ok"
            };
            noDeviseDialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await noDeviseDialog.ShowAsync();
        }
        public async void DisplayNoApiDialog()
        {
            ContentDialog noDApiDialog = new ContentDialog
            {
                Title = "Erreur",
                Content = "Api non disponible",
                CloseButtonText = "Ok"
            };
            noDApiDialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await noDApiDialog.ShowAsync();
        }
    }
}
