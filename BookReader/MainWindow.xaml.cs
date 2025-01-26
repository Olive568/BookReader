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

namespace BookReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string[]> db = new List<string[]>();
        public MainWindow()
        {
            InitializeComponent();
            Populate_Database();
            Populate_List();
            Populate_Genre_ComboBox();
            Populate_Price_ComboBox();
        }

        private void Populate_Database()
        {
            using (StreamReader sr = new StreamReader("books.csv"))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {

                    string[] Split_Array = ParseCsvLine(line);


                    for (int x = 0; x < 4; x++)
                    {
                        Split_Array[x] = Split_Array[x].ToUpper();
                    }


                    db.Add(Split_Array);
                }
            }
        }
        private static string[] ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            string currentField = "";

            foreach (char c in line)
            {
                if (c == '"' && inQuotes)
                {
                    inQuotes = false;
                }
                else if (c == '"' && !inQuotes)
                {
                    inQuotes = true;
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(currentField);
                    currentField = "";
                }
                else
                {
                    currentField += c;
                }
            }


            if (!string.IsNullOrEmpty(currentField))
            {
                fields.Add(currentField);
            }

            return fields.ToArray();
        }
        private void Populate_Genre_ComboBox()
        {
            Genre_Combo_Box.Items.Add("None");
            string[] Genres = new string[] {
    "Historical fiction",
    "Novella",
    "Fantasy",
    "Mystery",
    "Family saga",
    "Adventure",
    "Detective",
    "Mystery thriller",
    "Coming-of-age",
    "Romance",
    "Self-help",
    "Magic realism",
    "Novel",
    "Children's fiction",
    "Manual",
    "Historical novel",
    "War",
    "Thriller",
    "Sexology",
    "Children's literature",
    "Picture book",
    "Popular science",
    "Philosophy",
    "Young adult",
    "Dystopian",
    "Political fiction",
    "Social science fiction",
    "Fiction",
    "Science fiction",
    "Autobiography",
    "Feminist novel",
    "Pregnancy guide",
    "Picaresque novel",
    "Satirical allegorical novella",
    "Travel literature",
    "Unfinished satirical dark comedy novel",
    "Motivational",
    "Business fable",
    "Leadership",
    "Parable",
    "Science fiction novel",
    "Crime novel",
    "Erotic",
    "Young adult romantic novel",
    "Adventure",
    "War novel",
    "Semi-autobiographical novel",
    "Memoir",
    "Thriller",
    "Coming-of-age murder mystery",
    "Japanese novel",
    "Young adult novel",
    "Children's fantasy novel",
    "Classic regency novel",
    "Historical non-fiction",
    "Autobiography",
    "Satirical fiction",
    "Philosophy novel",
    "Romance novel",
    "Historical fiction novel",
    "Satirical novel",
    "Horror",
    "Political satire",
    "Crime thriller novel",
    "Novel, tragedy",
    "Historical fiction, war novel",
    "Young adult fiction",
    "Novel, fantasy",
    "Children's literature, picture book",
    "Science fiction, mystery",
    "Biography novel",
    "Contemporary fiction"
};
            foreach (string s in Genres)
            {
                Genre_Combo_Box.Items.Add(s);
            }
            Genre_Combo_Box.SelectedIndex = 0;
        }
        private void Populate_Price_ComboBox()
        {
            string[] strings = new string[8] { "none", "<10M", "<20M", "<30M", "<40M", "<50M", "<60M", "60M+" };
            foreach (string s in strings)
            {
                Price_Combo_Box.Items.Add(s);
            }
            Price_Combo_Box.SelectedIndex = 0;
        }
        private void Populate_List()
        {
            foreach (string[] split in db)
            {
                string name = "N/A";
                string author = "N/A";
                string date = "N/A";
                string genre = "N/A";
                string sales = "N/A";


                if (split.Length > 0) name = split[0].ToUpper();
                if (split.Length > 1) author = split[1].ToUpper();
                if (split.Length > 2) date = split[2].ToUpper();
                if (split.Length > 3) sales = split[3].ToUpper();
                if (split.Length > 4) genre = split[4].ToUpper();
                

                ListViewItem item = new ListViewItem();
                name.ToUpper();
                author.ToUpper();
                genre.ToUpper();
                item.Content = new { Name = name, Author = author, Date = date, Sales = sales , Genre = genre };
                Game_List.Items.Add(item);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game_List.Items.Clear();

            foreach (string[] split in db)
            {
                // Ensure the split array has at least 5 elements
                if (split.Length >= 5)
                {
                    // Check if the game name contains the search text
                    if (split[0].Contains(Search_Box.Text.ToUpper()))
                    {
                        // Check if the selected genre is valid
                        if (Genre_Combo_Box.SelectedIndex == 0 ||
                            (Genre_Combo_Box.SelectedItem != null &&
                             split[4].ToUpper().Contains(Genre_Combo_Box.SelectedItem.ToString().ToUpper())))
                        {
                            // Check the price conditions based on the selected price filter
                            if (Price_Combo_Box.SelectedIndex == 0)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 1 &&
                                     float.TryParse(split[3], out float price2) && price2 < 10)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 2 &&
                                     float.TryParse(split[3], out float price3) && price3 < 20)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 3 &&
                                     float.TryParse(split[3], out float price4) && price4 < 30)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 4 &&
                                     float.TryParse(split[3], out float price5) && price5 < 40)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 5 &&
                                     float.TryParse(split[3], out float price6) && price6 < 50)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 6 &&
                                     float.TryParse(split[3], out float price7) && price7 < 60)
                            {
                                AddGameToList(split);
                            }
                            else if (Price_Combo_Box.SelectedIndex == 7 &&
                                     float.TryParse(split[3], out float price8) && price8 >= 60)
                            {
                                AddGameToList(split);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Malformed line: " + string.Join(",", split));
                }
            }
        }

        private void AddGameToList(string[] split)
        {

            string name = "N/A";
            string author = "N/A";
            string date = "N/A";
            string genre = "N/A";
            string sales = "N/A";


            if (split.Length > 0) name = split[0].ToUpper();
            if (split.Length > 1) author = split[1].ToUpper();
            if (split.Length > 2) date = split[2].ToUpper();
            if (split.Length > 3) sales = split[3].ToUpper();
            if (split.Length > 4) genre = split[4].ToUpper();


            ListViewItem item = new ListViewItem();
            name.ToUpper();
            author.ToUpper();
            genre.ToUpper();
            item.Content = new { Name = name, Author = author, Date = date, Sales = sales, Genre = genre };
            Game_List.Items.Add(item);

        }


        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "FilteredBooks.csv";

            using (StreamWriter sw = new StreamWriter(filePath))
            {

                sw.WriteLine("Name,Author,Date,Sales,Genre");

                foreach (ListViewItem item in Game_List.Items)
                {

                    var gameData = (dynamic)item.Content;

                    string csvLine = $"{gameData.Name},{gameData.Author},{gameData.Date},{gameData.Sales},{gameData.Genre}";

 
                    sw.WriteLine(csvLine);
                }
            }

            MessageBox.Show("Export Complete");
        }
    }
}
