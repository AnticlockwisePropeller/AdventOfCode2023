using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file which lists the winning and played numbers from a set of scratchcards
//The program identifies how many of the played numbers are winning numbers on each scratchcard
//For each matching number, you win copies of the scratchcards following the winning card equal to the number of matches
//E.g. if card 10 had 5 matching numbers, you would win one copy each of cards 11, 12, 13, 14, and 15
//This repeats for every copy of every scratchcard
//The program calculates the number of copies of every scratchcard, including the originals, and prints the total to the console


class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the subtotal of each scratchcard
        int subtotal = 0;

        //List to store each line of the file
        List<string> lines = [];

        //Regular expressions, first matches winning numbers, second matches played numbers
        Regex rxWins = new Regex(@"(?=[ |\d]*\|)[\d]+");
        Regex rxPlays = new Regex(@"(?<=\|[ |\d]*)[\d]+");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file and add it to the list
        while (line != null)
        {
            lines.Add(line);
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Array to store the number of copies of each scratchcard
        int[] copies = new int[lines.Count];

        //Loop through every line
        for (int i = 0; i < lines.Count; i++)
        {
            //Read the current line
            line = lines[i];

            //Add one copy of the current scratchcard to the array
            copies[i]++;

            //Find all the winning and played numbers in the current line
            MatchCollection wins = rxWins.Matches(line);
            MatchCollection plays = rxPlays.Matches(line);

            //Compare the played numbers to the winning numbers
            foreach (Match play in plays)
            {
                foreach (Match win in wins)
                {
                    //When a match is found, increase the subtotal by one
                    if (win.Value == play.Value)
                    {
                        subtotal++;

                        //If the scratchcard exists, add copies of the appropriate scratchcard
                        //equal to the number of copies of the current scratchcard
                        if (i + subtotal < copies.Length)
                        {
                            copies[i+subtotal] += copies[i];
                        }
                    }
                }
            }

            //Reset the subtotal
            subtotal = 0;
        }

        //Add up the number of scratchcards and write the total to the console
        Console.WriteLine(copies.Sum());
    }
}
