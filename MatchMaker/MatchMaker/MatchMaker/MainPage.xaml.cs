using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MatchMaker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private static readonly string vowels = "aeiouy";
        private static readonly char[] vowelsArray = vowels.ToCharArray();
        private static readonly string love = "love";
        private static readonly char[] loveArray = love.ToCharArray();

        private async void btnCalculate_OnClick(object sender, EventArgs e)
        {
            Entry name1 = (Entry)this.FindByName("Name1");
            Entry name2 = (Entry)this.FindByName("Name2");
            Label resultLabel = (Label)this.FindByName("ResultLabel");
            ProgressBar progress = (ProgressBar)this.FindByName("Progress");

            resultLabel.IsVisible = false;
            string name1Text = name1.Text;
            string name2Text = name2.Text;
            int result = 0;

            string calculatePreference = Preferences.Get("CalculatorSetting", string.Empty);
            switch (calculatePreference)
            {
                case "Chinese":                    
                    result = CalculateMatchChinese(name1Text, name2Text);

                    break;

                case "Default":
                    result = CalculateMatchDefault(name1Text, name2Text);
                    break;

                default:
                    result = CalculateMatchDefault(name1Text, name2Text);

                    break;
            }
            progress.IsVisible = true;
            progress.Progress = 0;                                 
            await progress.ProgressTo(1, 2000, Easing.Linear);
            
            resultLabel.Text = $"{result}%";
            resultLabel.IsVisible = true;
        }

        private static int CalculateMatchDefault(string name, string name2)
        {
            int score1 = CalculateASCIIScore(name);
            int score2 = CalculateASCIIScore(name2);

            int diff = Math.Abs(score1 - score2);
            int result = 100 - diff;

            return result;
        }

        private static int CalculateASCIIScore(string name)
        {
            int asciScore = 0;
            byte[] textAsASCII = Encoding.ASCII.GetBytes(name);

            foreach (var item in textAsASCII)
            {
                asciScore += item;
            }
            return asciScore;
        }

        private static int CalculateMatchChinese(string name, string name2)
        {
            int score = CalculateScoreChinese(name, name2);

            return score;
        }

        private static int CalculateScoreChinese(string name, string name2)
        {
            int score = 0;
            int numberOfVowelsName1 = CountChars(name, vowelsArray, false);
            int numberOfVowelsName2 = CountChars(name2, vowelsArray, false);
            int numberOfConsonantsName1 = CountChars(name, vowelsArray, true);
            int numberOfConsonantsName2 = CountChars(name2, vowelsArray, true);
            int numberofLoveName1 = CountChars(name, loveArray, false);
            int numberofLoveName2 = CountChars(name2, loveArray, false);

            if (name.Length == name2.Length)
            {
                score += 20;
            }
            if (numberOfVowelsName1 == numberOfVowelsName2)
            {
                score += 20;
            }
            if (numberOfConsonantsName1 == numberOfConsonantsName2)
            {
                score += 20;
            }
            if (numberofLoveName1 > 0 || numberofLoveName2 > 0)
            {
                score += (numberofLoveName1 * 5);
                score += (numberofLoveName2 * 5);
            }
            if ((StartsWithConstonant(name) && StartsWithConstonant(name2)))
            {
                score += 10;
            }
            if (StartsWithVowel(name) && StartsWithVowel(name2))
            {
                score += 10;
            }

            return score;
        }

        private async void btn_Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }

        private static bool StartsWithVowel(string name)
        {
            char firstChar = name[0];

            return vowelsArray.Contains(firstChar);
        }

        private static bool StartsWithConstonant(string name)
        {
            char firstChar = name[0];

            return !vowelsArray.Contains(firstChar);
        }

        private static int CountChars(string searchIn, char[] searchFor, bool inverse)
        {
            int result = 0;
            int resultReverse = 0;

            result = searchIn.Count(searchFor.Contains);
            if (inverse)
            {
                for (int i = 0; i < searchFor.Length; i++)
                {
                    if (!searchIn.Contains(searchFor[i]))
                    {
                        resultReverse++;
                    }
                }
            }

            return inverse ? resultReverse : result;
        }
    }
}