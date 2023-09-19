using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace QuranApp
{
    public class SurahPage : ContentPage
    {
        List<Ayat> ayatList = new List<Ayat>();
        public SurahPage(int noSurat, string suratName, string revelationType, int jumlahAyat)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.White;
            ayatList = Quran.GetAyatList(noSurat);

            var ayatViewCollection = new StackLayout { Spacing = 18 };
            foreach(var ayat in ayatList)
            {
                ayatViewCollection.Add(
                    new StackLayout{ 
                        Spacing = 12,
                        Children = 
                        {
                            new Frame
                            {
                                Padding = new Thickness(12,24,12,24),
                                BorderColor = Colors.SkyBlue,
                                BackgroundColor = Colors.LightSteelBlue,
                                CornerRadius = 12,
                                Content = new Label{Text = ayat.arabic, FontSize = 32, FontFamily = "Arabic", HorizontalTextAlignment=TextAlignment.End }
                            },
                            new Label{Text = $"{ayat.number}. {ayat.transliteration}", FontSize = 18},
                            new Label{Text = $"{ayat.number}. {ayat.meaning}", FontSize = 18},
                        }
                    }
                    );
            }

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Margin = 30,
                    Spacing = 12,
                    Children =
                {
                    new Label{ Text= suratName, FontSize=32,HorizontalOptions=LayoutOptions.Center },
                    new Label { Text = $"{jumlahAyat} Ayat - {revelationType}", FontSize = 16, TextColor = Colors.Grey,HorizontalOptions=LayoutOptions.Center },
                    ayatViewCollection

                }

                }
            };
        }
    }
}