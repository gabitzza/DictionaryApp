using System;
using System.Collections.Generic;
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
using System.IO;
using System.Windows.Shapes;
using Tema1; 

namespace Tema1
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        private List<string> wordList;
        public Search()
        {
            InitializeComponent();
            Handler.LoadCategoriesFromTextFile(categoryComboBox);
            wordList = Handler.LoadWordList();
            searchComboBox.KeyUp += SearchComboBox_KeyUp;
            categoryComboBox.SelectionChanged += CategoryComboBox_SelectionChanged;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Handler.FilterWordsByCategory(categoryComboBox, searchComboBox, wordList);
        }
        private void SearchComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            Handler.FilterWordsBySearch(categoryComboBox, searchComboBox, wordList);
        }

        private void OnSelectWordButtonClick(object sender, RoutedEventArgs e)
        {
            Handler.OnSelectWordButtonClick(sender, e, searchComboBox, descriptionTextBlock, wordNameTextBlock, wordCategoryTextBlock, imageControl);
        }


    }
}
