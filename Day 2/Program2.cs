using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//On each line of the text file there are the results of a game involving numbers of coloured cubes
//The program finds the minimum number of each coloured cube possible to achieve each game's result
//It then multiplies those numbers together to get each game's 'power'
//The program writes the total of all the powers to the console

class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the total of the game powers
        int total = 0;

        //Regular expressions that match the number of red, green, and blue cubes
        Regex rxReds = new Regex(@"[0-9]+(?= red)");
        Regex rxGreens = new Regex(@"[0-9]+(?= green)");
        Regex rxBlues = new Regex(@"[0-9]+(?= blue)");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file
        while (line != null)
        {
            //Calculate the game's 'power' and add it to the total
            total += GetMin(rxReds,line) * GetMin(rxGreens,line) * GetMin(rxBlues,line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Write the total to the console
        Console.WriteLine(total);
    }

    //Finds and returns the maximum number of cubes drawn of a certain colour in a game
    //Takes a regular expression to match the cubes drawn of one colour, and the string to search in
    static int GetMin(Regex rx, string input)
    {
            //List to hold the numbers of cubes drawn
            List<int> matches = [];

            //Adds the matches to the list
            foreach (Match match in rx.Matches(input))
            {
                matches.Add(int.Parse(match.Value));
            }

            //Returns the largest number in the list
            return matches.Max();
    }
}
