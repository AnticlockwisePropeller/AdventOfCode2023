using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//The text file contains a grid of numbers and symbols separated by stops (.)
//The program searches for any number that is adjacent to a symbol (including diagonals) and adds them together
//It then writes the total to the console

class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the total of the numbers
        int total = 0;

        //Regular expressions, first matches numbers, second matches any character except digits and stops
        Regex rxNumber = new Regex(@"[0-9]+");
        Regex rxSymbol = new Regex(@"[\D-[\.]]");

        //List to store every line of the input file
        List<string> lines = [];

        //Variables to store the lines being read
        string line;

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        line = sr.ReadLine();

        //Loop through every line of the file and add it to the list
        while (line != null)
        {
            lines.Add(line);
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Loop though every line from the file
        for (int i = 0; i < lines.Count; i++)
        {
            //Set the current line to be searched
            line = lines.ElementAt(i);

            //Search for all the numbers in the current line
            MatchCollection numbers = rxNumber.Matches(line);

            //For every number found, check if there is an adjacent symbol
            foreach (Match number in numbers)
            {
                //Variable to check if an adjacent symbol was found
                bool found = false;

                //First search for symbols in the same line
                MatchCollection symbols = rxSymbol.Matches(line);
                found = SearchAdj(number, symbols);

                //If no match was found and there is a previous line, search that
                if (found == false && i > 0)
                {
                    //Set line to previous line
                    line = lines.ElementAt(i-1);

                    //Search for adjacent symbols
                    symbols = rxSymbol.Matches(line);
                    found = SearchAdj(number, symbols);

                    //Reset line to current line
                    line = lines.ElementAt(i);
                }

                //If no match was found and there is a next line, search that
                if (found == false && i < lines.Count - 1)
                {
                    //Set line to next line
                    line = lines.ElementAt(i+1);

                    //Search for adjacent symbols
                    symbols = rxSymbol.Matches(line);
                    found = SearchAdj(number, symbols);

                    //Reset line to current line
                    line = lines.ElementAt(i);
                }

                //If an adjacent symbol was found, add the number to the total
                if (found) total += int.Parse(number.Value);
            }
        }

        //Write the total to the console
        Console.WriteLine(total);
    }

    //Takes a Match object (number) and a MatchCollection object (symbols)
    //Compares the indices of the Matches in symbols with the index of the Match number to see if any are adjacent
    //Returns true if an adjacent Match was found
    static bool SearchAdj(Match number, MatchCollection symbols)
    {
        //Variable to check if an adjacent symbol was found
        bool found = false;

        foreach (Match symbol in symbols)
        {
            //If an adjacent symbol is found, set 'found' to true and stop searching
            if (symbol.Index >= number.Index - 1 && symbol.Index <= number.Index + number.Length)
            {
                found = true;
                break;
            }
        }

        return found;
    }
}
