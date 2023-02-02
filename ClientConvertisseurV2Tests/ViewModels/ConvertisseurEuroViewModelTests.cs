using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSConvertisseur.Models;

namespace ClientConvertisseurV2.ViewModels.Tests
{
    [TestClass()]
    public class ConvertisseurEuroViewModelTests
    {
        [TestMethod()]
        public void ActionSetConversionTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuroViewModel = new ConvertisseurEuroViewModel();
            //Création d'un objet de type ConvertisseurEuroViewModel

            convertisseurEuroViewModel.Montant = 100;

            //Création d'un objet Devise, dont Taux=1.07
            Devise laDevise = new Devise();
            laDevise.Taux = 1.07;
            convertisseurEuroViewModel.SelectedDevise = laDevise;

            //Act
            //Appel de la méthode ActionSetConversion
            ActionSetConversionTest();

            //Assert
            //Assertion : MontantDevise est égal à la valeur espérée 107
            Assert.Equals(convertisseurEuroViewModel.ConvertedAmount, 107);

        }
        [TestMethod()]
        public void ConvertisseurEuroViewModelTest()
        {
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            Assert.IsNotNull(convertisseurEuro);
        }

        [TestMethod()]
        public void GetDataOnLoadAsyncTest()
        {
            //Arrange
            ConvertisseurEuroViewModel convertisseurEuro = new ConvertisseurEuroViewModel();
            //Act
            convertisseurEuro.GetDataOnLoadAsync();
            Thread.Sleep(1000);
            //Assert
            Assert.IsNotNull(convertisseurEuro.LesDevises);
        }
    }
}