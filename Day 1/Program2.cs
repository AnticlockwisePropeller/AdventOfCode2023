using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//On each line of the text file there is at least one number, either a numeric character ("0" to "9"), or written ("zero" to "nine")
//The program locates the first and last number of each line and concatenates them into a two digit number
//If the line has only one number, the two digit number will be that number repeated (e.g. "22", "33", etc.)
//The program writes the total of all the two digit numbers to the console

class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the total of the two digit numbers
        int total = 0;

        //Regular expression that matches a single numeric character, or a written number ("zero" to "nine")
        Regex rx = new Regex(@"([0-9]|zero|one|two|three|four|five|six|seven|eight|nine)");

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Loop through every line of the file
        while (line != null)
        {
            //List to contain the matches
            List<string> matches = new List<string>();

            //Find all characters that match the regular expression
            //Must specify the index to start next match to find overlapping matches
            Match mx = rx.Match(line);
            while (mx.Success)
            {
                matches.Add(mx.Value);
                mx = rx.Match(line, mx.Index + 1);
            }

            //Get the first and last match, and convert them to a numeric character if necessary
            string first = GetNum(matches.First());
            string last = GetNum(matches.Last());

            //Concatenate the first and last matches into a two digit number
            string x = first + last;

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


    //Takes an input string and, if it matches a written number "zero" to "nine",
    //returns a string containing the corresponding numeric character "0" to "9".
    //Otherwise it returns the input string unchanged
    static string GetNum(string input)
    {
        //String to contain the output
        string output;

        switch (input)
        {
            //If the input is a written number ("zero" to "nine"), set the output to the correct numeric character
            case "zero": output = "0"; break;
            case "one": output = "1"; break;
            case "two": output = "2"; break;
            case "three": output = "3"; break;
            case "four": output = "4"; break;
            case "five": output = "5"; break;
            case "six": output = "6"; break;
            case "seven": output = "7"; break;
            case "eight": output = "8"; break;
            case "nine": output = "9"; break;
            //Else set the output to the input
            default: output = input; break;
        }

        return output;
    }
}
