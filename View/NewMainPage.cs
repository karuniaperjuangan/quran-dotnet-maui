using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace QuranApp
{
    public partial class NewMainPage : ContentPage
    {
        private int count = 0;
        List<Surah> surahList = new List<Surah>();


        public NewMainPage()
        {
            NavigationPage.SetHasNavigationBar(this,false);
            BackgroundColor = Colors.White;

            
            surahList = Quran.GetSurahList();
            
            
            
            

            
            var surahViewCollection = new StackLayout {Spacing= 12 };
            foreach (var surah in surahList) {
                Grid grid = new Grid
                {
                    Padding = new Thickness(0, 12, 0, 12),
                    RowDefinitions =
                            {
                                new RowDefinition{Height=GridLength.Auto},
                                new RowDefinition{Height=GridLength.Auto},
                                new RowDefinition{Height=GridLength.Auto},
                                new RowDefinition{Height=GridLength.Auto},
                            },
                    ColumnDefinitions =
                            {
                                new ColumnDefinition{Width=GridLength.Auto},
                                new ColumnDefinition{Width=GridLength.Star}
                            }
                };

                Label arabicName = new Label { Text = surah.arabicName, FontSize = 32, FontFamily = "Arabic", TextColor = Color.FromArgb("#512bdf"), HorizontalTextAlignment=TextAlignment.End };
                grid.SetRowSpan(arabicName, 4);
                grid.Add(arabicName, column: 1);
                grid.Add(new Label { Text = $"{surah.number}. {surah.name}", FontSize=20, TextColor=Colors.Black }, row:0);
                grid.Add(new Label { Text = surah.translation, FontSize = 16, TextColor = Colors.Grey },row:1);
                grid.Add(new Label { Text = $"{surah.ayatCount} Ayat - {(surah.revelationType == "mekah"?"Makkiyah":"Madaniyah")}", FontSize = 16, TextColor = Colors.Grey }, row: 2);

                surahViewCollection.Add(
                    new ContentView {

                        GestureRecognizers = {
                            new TapGestureRecognizer{
                            Command = new Command((object obj) => {Navigation.PushAsync(new SurahPage(surah.number, surah.name,(surah.revelationType == "mekah"?"Makkiyah":"Madaniyah"), surah.ayatCount )); })
                            },
                            new ClickGestureRecognizer
                            {
                                Command = new Command((object obj) => {Navigation.PushAsync(new SurahPage(surah.number, surah.name,(surah.revelationType == "mekah"?"Makkiyah":"Madaniyah"), surah.ayatCount )); })
                            }
                        },
                        Content = new Frame { 
                            Content= grid,
                            BorderColor = Colors.Gray,
                            
                            CornerRadius = 10,
                            Padding = 12,
                        }
                        
                    
                    }
                    ) ;
            }

            
            
            Content = new ScrollView
            {

                Content = new StackLayout
                {
                    Margin = 30,
                    Spacing = 12,
                    Children =
                {
                    new Label{ Text= "Quran App", FontSize=32,HorizontalOptions=LayoutOptions.Center },
                    surahViewCollection
                }
                }
            };

        }
    }
}