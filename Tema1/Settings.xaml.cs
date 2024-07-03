using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tema1;

namespace Tema1
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private string imagePath;
        private List<Word.word> wordList;
        private Divertisment divertisment;
       
        public Settings()
        {
            InitializeComponent();
            Handler.PopulateComboBox(categoryComboBox);
            wordList = new List<Word.word>();
         
        }

        private void OnAddNewCategoryButtonClick(object sender, RoutedEventArgs e)
        {
            Handler.AddNewCategoryButtonClick(newCategoryTextBox, categoryComboBox);
        }

        private void OnAddNewWordButtonClick(object sender, RoutedEventArgs e)
        {
            Handler.AddNewWordButtonClick(newWordTextBox, descriptionTextBox, categoryComboBox, imagePath);
        }

        private void OnModifyWordButtonClick(object sender, RoutedEventArgs e)
        {
            string wordToModify = modifyWordTextBox.Text.Trim();
            Handler.ModifyWordButtonClick(wordToModify);
        }
        private void OnDeleteWordButtonClick(object sender, RoutedEventArgs e)
        {
            string wordToDelete = deleteWordTextBox.Text.Trim();
            Handler.DeleteWordButtonClick(wordToDelete);
        }

        private void OnSelectImageButtonClick(object sender, RoutedEventArgs e)
        {
            imagePath = Handler.SelectImage();
        }
    }

}
