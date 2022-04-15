using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace QuranApp
{
    class Surah
    {
        public int number { get; set; }
        public string name { get; set; }
        public string arabicName { get; set; }
        public string translation { get; set; }
        public int ayatCount { get; set; }
        public string revelationType { get; set; }
    }

    class Ayat
    {
        public int number { get; set; }
        public string arabic { get; set; }
        public string transliteration { get; set; }
        public string meaning { get; set; }
    }

    class Quran
    {
        public static List<Surah> GetSurahList() {
            List<Surah> surahList = new List<Surah>();
            var client = new RestClient("https://equran.id/api/surat/");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) { return surahList; }
            JsonArray surahArray = (JsonArray) SimpleJson.DeserializeObject(response.Content);
            foreach(JsonObject surah in surahArray)
            {
                
                surahList.Add(
                    new Surah
                    {
                        number = Convert.ToInt32(surah["nomor"]),
                        name = (string) surah["nama_latin"],
                        arabicName = (string) surah["nama"],
                        translation = (string) surah["arti"],
                        ayatCount = Convert.ToInt32(surah["jumlah_ayat"]),
                        revelationType = (string) surah["tempat_turun"]

                    }
                    ) ; 
            }
            Console.WriteLine(surahList.ToString());
            return surahList;
        }

        public static List<Ayat> GetAyatList(int surat) {
        List<Ayat> ayatList = new List<Ayat>();
            var client = new RestClient("https://equran.id/api/surat/"+surat);
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { return ayatList; }
            Console.WriteLine(response.Content);
            JsonObject obj = (JsonObject)SimpleJson.DeserializeObject(response.Content);
            JsonArray ayatArray = (JsonArray) obj["ayat"];
            foreach (JsonObject ayat in ayatArray)
            {
                ayatList.Add(new Ayat
                {
                    number = Convert.ToInt32(ayat["nomor"]),
                    arabic = (string) ayat["ar"],
                    meaning = (string) ayat["idn"],
                    transliteration = (string) ayat["tr"]
                }) ;
            }
            Console.WriteLine(ayatList.ToString());
            return ayatList;
        }
    }
}
