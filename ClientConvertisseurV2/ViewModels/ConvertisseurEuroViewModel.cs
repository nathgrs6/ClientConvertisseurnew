using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Models;

namespace ClientConvertisseurV2.ViewModels

{
    public class ConvertisseurEuroViewModel : ObservableObject
    {
        public IRelayCommand BtnSetConversion { get; }
        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);

        }

        private void ActionSetConversion()
        {
           
            if (selectedDevise != null)
            {
                ConvertedAmount = Montant * SelectedDevise;

            }
            else DisplayNoDeviseDialog();
        }

        private ObservableCollection<Devise> lesDevises;

        public ObservableCollection<Devise> LesDevises
        {
            get { return lesDevises; }
            set { lesDevises = value; OnPropertyChanged(); }
        }

        private Devise selectedDevise;


        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set { selectedDevise = value; OnPropertyChanged(); }
        }

        private double montant;

        public double Montant
        {
            get { return montant; }
            set { montant = value; OnPropertyChanged(); }
        }

        private double convertedAmount;

        public double ConvertedAmount
        {
            get { return convertedAmount; }
            set { convertedAmount = value; OnPropertyChanged(); }
        }

        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7008/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {

                return;

            }
            else
            {
                LesDevises = new ObservableCollection<Devise>(result);
            }
            

        }
        public async void DisplayNoDeviseDialog()
        {
            ContentDialog noDeviseDialog = new ContentDialog
            {
                Title = "Erreur",
                Content = "Vous devez selectionner un devise.",
                CloseButtonText = "Ok"
            };
            noDeviseDialog.XamlRoot = App.MainRoot.XamlRoot;
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
            noDApiDialog.XamlRoot = App.MainRoot.XamlRoot;
            ContentDialogResult result = await noDApiDialog.ShowAsync();
        }
      

    }
   

}
