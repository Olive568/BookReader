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
            using (StreamReader sr = new StreamReader("steam.csv"))
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
            string[] Genres = new string[17] {"Action", "Free to Play", "Strategy", "Adventure", "Indie", "RPG", "Animation & Modeling",
            "Video Production", "Casual", "Racing", "Simulation", "Massively Multiplayer", "Sports",
            "Early Access", "Violent", "Gore", "Nudity"};
            foreach (string s in Genres)
            {
                Genre_Combo_Box.Items.Add(s);
            }
            Genre_Combo_Box.SelectedIndex = 0;
        }
        private void Populate_Price_ComboBox()
        {
            string[] strings = new string[9] { "none", "free", "<$10", "<$20", "<$30", "<$40", "<$50", "<$60", "$60+" };
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
                ListViewItem item = new ListViewItem();
                item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
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
            string[] Split_Array = new string[4];
            foreach (string[] split in db)
            {
                if (split[0].Contains(Search_Box.Text.ToUpper()))
                {
                    if (Genre_Combo_Box.SelectedIndex == 0 || (Genre_Combo_Box.SelectedItem != null && split[3].ToUpper().Contains(Genre_Combo_Box.SelectedItem.ToString().ToUpper())))
                    {
                        if (Price_Combo_Box.SelectedIndex == 0)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);
                        }
                        else if (Price_Combo_Box.SelectedItem.ToString().ToLower() == "free" && int.TryParse(split[4], out int price) && price == 0)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);
                        }
                        else if (Price_Combo_Box.SelectedIndex == 2 && float.TryParse(split[4], out float price2) && price2 < 10)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 3 && float.TryParse(split[4], out float price3) && price3 < 20)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 4 && float.TryParse(split[4], out float price4) && price4 < 30)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 5 && float.TryParse(split[4], out float price5) && price5 < 40)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 6 && float.TryParse(split[4], out float price6) && price6 < 50)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 7 && float.TryParse(split[4], out float price7) && price7 < 60)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[4], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                        else if (Price_Combo_Box.SelectedIndex == 8 && float.TryParse(split[4], out float price8) && price8 >= 60)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Content = new { Name = split[0], Date = split[1], Dev = split[2], Genre = split[3], Price = split[4] };
                            Game_List.Items.Add(item);

                        }
                    }



                }
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "FilteredGames.csv";

            using (StreamWriter sw = new StreamWriter(filePath))
            {

                sw.WriteLine("Name,Date,Developer,Genre,Price");

                foreach (ListViewItem item in Game_List.Items)
                {

                    var gameData = (dynamic)item.Content;

                    string csvLine = $"{gameData.Name},{gameData.Date},{gameData.Dev},{gameData.Genre},{gameData.Price}";

 
                    sw.WriteLine(csvLine);
                }
            }

            MessageBox.Show("Export Complete");
        }
    }
}
