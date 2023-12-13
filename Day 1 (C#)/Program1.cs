using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//On each line of the text file there is at least one numeric character
//The program locates the first and last numeric character of each line and concatenates them into a two digit number
//If the line has only one numeric character, the two digit number will be that character repeated (e.g. "22", "33", etc.)
//The program writes the total of all the two digit numbers to the console

class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the total of the two digit numbers
        int total = 0;

        //Regular expression that matches a single numeric character
        Regex rx = new Regex(@"[0-9]");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file
        while (line != null)
        {
            //Find all characters that match the regular expression
            MatchCollection matches = rx.Matches(line);

            //Concatenate the first and last matches into a two digit number
            string x = matches.First().Value + matches.Last().Value;

            //Add the number to the total
            total += int.Parse(x);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Write the total to the console
        Console.WriteLine(total);
    }
}
