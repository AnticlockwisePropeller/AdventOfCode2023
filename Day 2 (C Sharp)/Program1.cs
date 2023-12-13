using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//On each line of the text file there are the results of a game involving numbers of coloured cubes
//The program determines if each result was possible with only 12 red, 13 green, and 14 blue cubes
//The program adds together the game IDs of every game that was possible, and writes the total to the console

class Program
{
    static void Main(string[] args)
    {
        //Constants to define the maximum allowed number of red, green, and blue cubes
        const int maxRed = 12;
        const int maxGreen = 13;
        const int maxBlue = 14;

        //Variable to add up the total of the game IDs
        int total = 0;

        //Regular expressions that match the number of red, green, and blue cubes
        Regex rxReds = new Regex(@"[0-9]+(?= red)");
        Regex rxGreens = new Regex(@"[0-9]+(?= green)");
        Regex rxBlues = new Regex(@"[0-9]+(?= blue)");

        //Regular expression that matches the game ID
        Regex rxGameID = new Regex(@"(?<=Game )[0-9]+");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file
        while (line != null)
        {
            //Check if the number of cubes drawn is less than the maximum allowed for each colour
            if (IsPossible(rxReds,maxRed,line) && IsPossible(rxGreens,maxGreen,line) && IsPossible(rxBlues,maxBlue,line))
            {
                //If the game was possible, find the game ID and add it to the total
                total += int.Parse(rxGameID.Match(line).Value);
            }

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Write the total to the console
        Console.WriteLine(total);
    }

    //Checks if the number of cubes drawn is greater than the max allowed
    //Takes a regular expression to match the number of cubes drawn, the maximum number of cubes allowed, and the string to search in
    //Returns true if possible, false if not
    static bool IsPossible(Regex rx, int max, string input)
    {
            //Gets all the number of cubes drawn of the chosen colour
            MatchCollection matches = rx.Matches(input);

            //If any of the draws are greater than the maximum allowed, return false
            foreach (Match match in matches)
            {
                if (int.Parse(match.Value) > max) return false;
            }

            //Otherwise, return true
            return true;
    }
}
