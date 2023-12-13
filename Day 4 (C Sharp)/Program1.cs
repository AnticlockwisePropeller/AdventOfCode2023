using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file which lists the winning and played numbers from a set of scratchcards
//The program identifies how many of the played numbers are winning numbers on each scratchcard and calculates its score
//If there is only one matching number, the scratchcard's score is one
//For every additional matching number, the score is doubled
//The program then adds the scores from every scratchcard together and prints the total to the console.


class Program
{
    static void Main(string[] args)
    {
        //Variables to add up the total and subtotals
        int total = 0;
        int subtotal = 0;

        //Regular expressions, first matches winning numbers, second matches played numbers
        Regex rxWins = new Regex(@"(?=[ |\d]*\|)[\d]+");
        Regex rxPlays = new Regex(@"(?<=\|[ |\d]*)[\d]+");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file
        while (line != null)
        {
            //Find all the winning and played numbers in that line
            MatchCollection wins = rxWins.Matches(line);
            MatchCollection plays = rxPlays.Matches(line);

            //Compare the played numbers to the winning numbers
            foreach (Match play in plays)
            {
                foreach (Match win in wins)
                {
                    //When a match is found, if it is the first match, add one to the subtotal
                    //Otherwise, double the subtotal
                    if (win.Value == play.Value)
                    {
                        if (subtotal == 0) subtotal++;
                        else subtotal *= 2;
                    }
                }
            }

            //Add the subtotal to the total, then reset the subtotal
            total += subtotal;
            subtotal = 0;

            //Read the next line
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Write the total to the console
        Console.WriteLine(total);
    }
}
