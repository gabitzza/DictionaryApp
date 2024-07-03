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
using System.Windows.Shapes;
using System.IO;

namespace Tema1
{
    /// <summary>
    /// Interaction logic for Divertisment.xaml
    /// </summary>
    public partial class Divertisment : Page
    {

        private List<Word.word> wordList;
        private bool gameStarted = false;
        private string displayedWord;
        private Random random = new Random();
        private int wordsDisplayedCount = 0;   
        private int Score { get; set; } = 0;


        public Divertisment()
        {
            InitializeComponent();

        }

        public string DisplayedWord
        {
            get { return displayedWord; }
            set { displayedWord = value; }
        }

        private void LoadWordList()
        {
            wordList = new List<Word.word>();

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
        }

        private void DisplayRandomDescription()
        {
            // Filtrăm lista de cuvinte care nu au fost încă afișate
            var filteredWords = wordList.Where(word => !word.Showed).ToList();

            // Verificăm dacă mai sunt cuvinte disponibile pentru a fi afișate
           
            if (filteredWords.Count > 0 && wordsDisplayedCount < 5)
            {
                int randomIndex = random.Next(0, filteredWords.Count);
                Word.word randomWord = filteredWords[randomIndex];

                string word = randomWord.Name.Trim();
                DisplayedWord = word;
                string category = randomWord.Category.Trim();
                string description = randomWord.Description.Trim();
                string imagePath = randomWord.ImagePath.Trim();

                // Generăm un număr aleatoriu între 0 și 1 pentru a decide dacă afișăm imaginea sau descrierea
                int randomNumber = random.Next(0, 2);

                if (randomNumber == 0 && !string.Equals(imagePath, @"C:\Users\CUB Bike Service\Desktop\Gabit\Lab\Anul 2\Sem 2\MAP\Tema1\images\images.png", StringComparison.OrdinalIgnoreCase))
                {
                    // Afisăm imaginea cuvântului
                    if (File.Exists(imagePath))
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                        imageControl.Source = bitmap;
                        descriptionTextBlock.Text = ""; // Ștergem descrierea
                    }
                    else
                    {
                        MessageBox.Show("Imaginea asociată cuvântului nu poate fi găsită.");
                    }
                }
                else
                {
                    // Afișăm descrierea cuvântului
                    descriptionTextBlock.Text = description;
                    imageControl.Source = null; // Ștergem imaginea
                }

                // Setăm indicatorul showed pe true pentru cuvântul afișat
                randomWord.Showed = true;
                wordsDisplayedCount++; // Incrementăm numărul de cuvinte afișate

            }
            else
            {
                // Afișăm scorul acumulat
                MessageBox.Show($"Jocul s-a încheiat. Scorul dvs. este: {Score}", "Scor final", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
                guessedWords.Text = Score.ToString();

            }
        }
        private void ResetGame()
        {
            // Resetați jocul prin reinițializarea tuturor variabilelor și controalelor necesare
            wordList.Clear();
            LoadWordList();
            wordsDisplayedCount = 0;
            Score = 0;
            gameStarted = true;
            nextButton.Content = "Next";
            nextButton.Background = Brushes.Transparent;
            startGameButton.IsEnabled = true;
            foreach (var word in wordList)
            {
                word.Showed = false;
            }
        }

        private void OnStartGameButtonClick(object sender, RoutedEventArgs e)
        {
            LoadWordList(); // Încarcă lista de cuvinte din fișier la fiecare pornire a jocului
            DisplayRandomDescription();
            gameStarted = true;
            startGameButton.IsEnabled = false;
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
            {
                OnStartGameButtonClick(sender, e);
            }
            else
            {
                    string guessedWord = enterTheWord.Text.Trim(); // Obținem textul introdus în textBox
                    // Comparăm cuvântul introdus cu cel afișat
                    if (string.Equals(guessedWord, DisplayedWord, StringComparison.OrdinalIgnoreCase))
                    {
                        Score++; // Creștem scorul cu 1
                     //   MessageBox.Show($"Scorul este: {Score}");
                        guessedWords.Text = Score.ToString(); // Actualizăm textul din guessedWords cu scorul nou
                    }
                    else
                    {
                        MessageBox.Show($"Cuvantul corect era: {DisplayedWord}");
                    }
                if (wordsDisplayedCount == 4)
                {
                    nextButton.Content = "Finish";
                    nextButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89023E"));
                }
                enterTheWord.Text = "";
                DisplayRandomDescription();         
            }
        }
    }
}

