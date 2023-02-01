using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
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
        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();

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
    }
    
}
