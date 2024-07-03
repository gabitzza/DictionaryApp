using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tema1;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Tema1
{
    internal class Handler
    {

        //---------------------------------->Settings.xaml.cs:
        public static void PopulateComboBox(ComboBox categoryComboBox)
        {
            string filePath = "word_list.txt";

            if (File.Exists(filePath))
            {
                // Citește toate liniile din fișier
                string[] lines = File.ReadAllLines(filePath);

                // Iterează prin fiecare linie și adaugă categoria în combobox
                foreach (string line in lines)
                {
                    // Separă cuvântul și categoria din linie
                    string[] parts = line.Split(',');

                    if (parts.Length >= 2)
                    {
                        string category = parts[1].Trim();

                        // Verifică dacă categoria nu este deja adăugată în combobox
                        if (!categoryComboBox.Items.Contains(category))
                        {
                            // Adaugă categoria în combobox
                            categoryComboBox.Items.Add(category);
                        }
                    }
                }
            }
        }

        public static void AddNewCategoryButtonClick(TextBox newCategoryTextBox, ComboBox categoryComboBox)
        {
            string newCategory = newCategoryTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                bool categoryExists = false;

                // Verificăm dacă categoria există deja în combobox
                foreach (string item in categoryComboBox.Items)
                {
                    if (item.Equals(newCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        categoryExists = true;
                        break;
                    }
                }

                if (!categoryExists)
                {
                    // Adăugăm noua categorie în combobox
                    categoryComboBox.Items.Add(newCategory);
                    MessageBox.Show("Categorie adăugată cu succes: " + newCategory);
                }
                else
                {
                    MessageBox.Show("Categorie deja existentă: " + newCategory);
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți o categorie validă.");
            }
        }

        private static bool WordExistsInFile(string word, string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4 && parts[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void AddNewWordButtonClick(TextBox newWordTextBox, TextBox descriptionTextBox, ComboBox categoryComboBox, string imagePath)
        {
            string newWord = newWordTextBox.Text.Trim();
            string description = descriptionTextBox.Text.Trim();

            // Verificați dacă calea către imagine este goală
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                // Dacă calea către imagine este goală, setați-o la calea implicită și numele imaginii
                imagePath = @"C:\Users\CUB Bike Service\Desktop\Gabit\Lab\Anul 2\Sem 2\MAP\Tema1\images\images.png";
            }

            if (!string.IsNullOrWhiteSpace(newWord) && !string.IsNullOrWhiteSpace(description))
            {
                if (categoryComboBox.SelectedItem != null)
                {
                    string selectedCategory = categoryComboBox.SelectedItem.ToString();
                    string filePath = "word_list.txt";

                    // Verificați dacă cuvântul există deja în fișier
                    if (WordExistsInFile(newWord, filePath))
                    {
                        MessageBox.Show("Cuvântul este deja în listă: " + newWord);
                        return;
                    }

                    // Adăugați cuvântul în fișier dacă nu există deja
                    Word.word word = new Word.word(newWord, selectedCategory, description, imagePath, false);
                    AddWordToTextFile(word);
                    MessageBox.Show("Cuvânt adăugat cu succes: " + newWord);
                }
                else
                {
                    MessageBox.Show("Trebuie să selectați o categorie înainte de a adăuga un cuvânt.");
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți un cuvânt și o descriere valide.");
            }
        }

        private static void AddWordToTextFile(Word.word word)
        {
            string filePath = "word_list.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{word.Name},{word.Category},{word.Description},{word.ImagePath},{word.Showed}");
            }
        }


        public static void ModifyWordButtonClick(string wordToModify)
        {
            if (!string.IsNullOrWhiteSpace(wordToModify))
            {
                // Aici puteți implementa logica pentru modificarea cuvântului
                MessageBox.Show("Cuvânt modificat cu succes: " + wordToModify);
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți un cuvânt valid.");
            }
        }


        public static void DeleteWordButtonClick(string wordToDelete)
        {
            string filePath = "word_list.txt";
            List<string> lines = new List<string>();

            // Verificăm dacă fișierul există
            if (File.Exists(filePath))
            {
                // Citim toate liniile din fișier
                lines = File.ReadAllLines(filePath).ToList();

                // Iterăm prin fiecare linie pentru a căuta cuvântul de șters
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] parts = lines[i].Split(',');

                    // Verificăm dacă prima parte (numele) este egală cu cuvântul de șters
                    if (parts.Length >= 1 && parts[0].Trim().Equals(wordToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        // Ștergem linia din lista
                        lines.RemoveAt(i);
                        break; // Odată ce am găsit și șters cuvântul, nu mai este nevoie să continuăm căutarea
                    }
                }

                // Suprascriem fișierul cu liniile actualizate
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("Cuvânt șters cu succes: " + wordToDelete);
            }
            else
            {
                MessageBox.Show("Fișierul word_list.txt nu există.");
            }
        }

      


        public static string SelectImage()
        {
            string imagePath = null;

            // Inițializați calea implicită cu calea către imaginea de bază
            string defaultImagePath = @"C:\Users\CUB Bike Service\Desktop\Gabit\Lab\Anul 2\Sem 2\MAP\Tema1\images\images.png";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                MessageBox.Show("Imagine selectată: " + imagePath);
            }
            else
            {
                // Dacă utilizatorul nu selectează o imagine, returnați calea către imaginea implicită
                imagePath = defaultImagePath;
            }

            return imagePath;
        }


        //------------------------------------------------> searching.xaml.cs:

        public static void LoadCategoriesFromTextFile(ComboBox categoryComboBox)
        {
            string filePath = "word_list.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 2)
                    {
                        string category = parts[1].Trim();

                        if (!categoryComboBox.Items.Contains(category))
                        {
                            categoryComboBox.Items.Add(category);
                        }
                    }
                }
            }
        }

        public static List<string> LoadWordList()
        {
            List<string> wordList = new List<string>();
            string filePath = "word_list.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 1)
                    {
                        string word = parts[0].Trim();
                        wordList.Add(word);
                    }
                }
            }

            return wordList;
        }

        public static void FilterWordsByCategory(ComboBox categoryComboBox, ComboBox searchComboBox, List<string> wordList)
        {
            string selectedCategory = categoryComboBox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var filteredWords = wordList.Where(word =>
                    File.ReadAllLines("word_list.txt")
                        .Any(line => line.Split(',')[1].Trim().Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                );

                searchComboBox.ItemsSource = filteredWords;
            }
            else
            {
                searchComboBox.ItemsSource = wordList;
            }
        }

        public static void FilterWordsBySearch(ComboBox categoryComboBox, ComboBox searchComboBox, List<string> wordList)
        {
            string selectedCategory = categoryComboBox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var filteredWords = wordList.Where(word =>
                    word.StartsWith(searchComboBox.Text, StringComparison.OrdinalIgnoreCase) &&
                    File.ReadAllLines("word_list.txt")
                        .Any(line => line.Split(',')[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase) &&
                                     line.Split(',')[1].Trim().Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                );

                searchComboBox.ItemsSource = filteredWords;
            }
            else
            {
                var filteredWords = wordList.Where(word =>
                    word.StartsWith(searchComboBox.Text, StringComparison.OrdinalIgnoreCase)
                );

                searchComboBox.ItemsSource = filteredWords;
            }
        }

        public static string GetCategoryForWord(string word)
        {
            string category = "";
            string[] lines = File.ReadAllLines("word_list.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 2 && parts[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    category = parts[1].Trim();
                    break;
                }
            }

            return category;
        }

        public static void OnSelectWordButtonClick(object sender, RoutedEventArgs e, ComboBox searchComboBox, TextBlock descriptionTextBlock, TextBlock wordNameTextBlock, TextBlock wordCategoryTextBlock, Image imageControl)
        {
            string selectedWord = searchComboBox.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedWord))
            {
                string selectedImagePath = GetImagePathForWord(selectedWord);
                string selectedDescription = GetDescriptionForWord(selectedWord);
                string selectedCategory = GetCategoryForWord(selectedWord);

                wordNameTextBlock.Text = $"Cuvânt: {selectedWord}";
                wordCategoryTextBlock.Text = $"Categorie: {selectedCategory}";
                descriptionTextBlock.Text = selectedDescription;

                // Setăm imaginea
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedImagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imageControl.Source = bitmap;
            }
        }

        public static string GetImagePathForWord(string word)
        {
            string imagePath = "";
            string[] lines = File.ReadAllLines("word_list.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 4 && parts[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    imagePath = parts[3].Trim();
                    break;
                }
            }

            return imagePath;
        }

        public static string GetDescriptionForWord(string word)
        {
            string description = "";
            string[] lines = File.ReadAllLines("word_list.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 3 && parts[0].Trim().Equals(word, StringComparison.OrdinalIgnoreCase))
                {
                    description = parts[2].Trim();
                    break;
                }
            }

            return description;
        }


        //---------------------------------------> Divertisment.xaml.cs:

        public static List<Word.word> LoadWordListForGame()
        {
            List<Word.word> wordList = new List<Word.word>();

            string filePath = "word_list.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4)
                    {
                        string name = parts[0].Trim();
                        string category = parts[1].Trim();
                        string description = parts[2].Trim();
                        string imagePath = parts[3].Trim();

                        // Creăm un obiect Word.word și îl adăugăm în listă
                        Word.word newWord = new Word.word(name, category, description, imagePath, false);
                        wordList.Add(newWord);
                    }
                }
            }
            else
            {
                MessageBox.Show("Fișierul word_list.txt nu a fost găsit.");
            }

            return wordList;
        }


    }
}

