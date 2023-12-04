using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file
//The text file contains a grid of numbers and symbols separated by stops (.)
//The program searches for any asterisk (*) that is adjacent to exactly two separate numbers
//Each number may be several digits long, and adjacency includes diagonals
//When it finds a match, the program multiplies the two numbers together to get a 'gear ratio'
//The program adds all the gear ratios together and writes the total to the console

class Program
{
    static void Main(string[] args)
    {
        //Variable to add up the total of the 'gear ratios'
        int total = 0;

        //Regular expressions, first matches numbers, second matches asterisks
        Regex rxNumber = new Regex(@"[0-9]+");
        Regex rxStar = new Regex(@"\*");

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

            //Search for all the asterisks in the current line
            MatchCollection stars = rxStar.Matches(line);

            //For every asterisk found, check if there is an adjacent number
            foreach (Match star in stars)
            {
                //Variable to count how many adjacent numbers are found
                //int count = 0;

                //Variables to store the first and second numbers found
                //int num1 = 0;
                //int num2 = 0;

                //Array to contain the results
                int[] result = [0,0,0];

                //If there is a previous line, search that line for numbers
                if (i > 0)
                {
                    //Set line to previous line
                    line = lines.ElementAt(i-1);

                    //Search for adjacent numbers
                    MatchCollection numbers = rxNumber.Matches(line);

                    foreach(Match number in numbers)
                    {
                        if (star.Index >= number.Index - 1 && star.Index <= number.Index + number.Length)
                        {
                            count++;
                            if (count > 2) break;
                            if (count == 1) num1 = int.Parse(number.Value);
                            if (count == 2) num2 = int.Parse(number.Value);
                        }
                    }
                }

                //If < 3 numbers were found and there is a next line, search that
                if (count < 3 && i < lines.Count - 1)
                {
                    //Set line to next line
                    line = lines.ElementAt(i+1);

                    //Search for adjacent numbers
                    MatchCollection numbers = rxNumber.Matches(line);

                    foreach(Match number in numbers)
                    {
                        if (star.Index >= number.Index - 1 && star.Index <= number.Index + number.Length)
                        {
                            count++;
                            if (count > 2) break;
                            if (count == 1) num1 = int.Parse(number.Value);
                            if (count == 2) num2 = int.Parse(number.Value);
                        }
                    }
                }

                //If < 3 numbers were found, search the current line
                if (count < 3)
                {
                    //Set line to current line
                    line = lines.ElementAt(i);

                    //Search for adjacent numbers
                    MatchCollection numbers = rxNumber.Matches(line);

                    foreach(Match number in numbers)
                    {
                        if (star.Index >= number.Index - 1 && star.Index <= number.Index + number.Length)
                        {
                            count++;
                            if (count > 2) break;
                            if (count == 1) num1 = int.Parse(number.Value);
                            if (count == 2) num2 = int.Parse(number.Value);
                        }
                    }
                }

                //If exactly two adjacent numbers were found, multiply them together and add them to the total
                if (count == 2) total += (num1 * num2);
            }
        }

        //Write the total to the console
        Console.WriteLine(total);
    }
}
