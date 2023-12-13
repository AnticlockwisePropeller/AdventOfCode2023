using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace AdventOfCode;

//The program reads a text file containing a list of seed values for a farm
//The values come in pairs, the first being a range start and the second being a range length
//The seed values are followed by a series of mapping values, which map the seeds onto other values (for soil, fertilizer, etc.)
//The mapping values consist of three numbers per line: the destination range start, the source range start, and the range length
//The program takes the seed values and maps them onto each subsequent set of values, until finally ending with a set of location values
//The program then finds the lowest location value and prints it to the console

class Program
{
    //Regular expression that matches a number
    static Regex rxNum = new Regex(@"[\d]+");

    static void Main(string[] args)
    {
        //List to hold the current mapping values as they are read from the file
        List<string> currentMap = [];

        //Open the file and read the first line
        StreamReader sr = new StreamReader("input.txt");
        string line = sr.ReadLine();

        //Get the seed values from the first line, and store them in a 2D long integer array
        //The values come in pairs: the first value is the Range Start and the second value is the Range Length
        MatchCollection matches = rxNum.Matches(line);
        long[,] seeds = new long[matches.Count/2,2];
        for (int i = 0; i < matches.Count; i += 2)
        {
            seeds[i/2,0] = long.Parse(matches[i].Value);
            seeds[i/2,1] = long.Parse(matches[i+1].Value);
        }


        //**SEED-TO-SOIL**

        //Loop until the next set of map values
        while (line != "soil-to-fertilizer map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the seed values to the soil values
        long[,] soil = MapNext(seeds, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**SOIL-TO-FERTILIZER**

        //Loop until the next set of map values
        while (line != "fertilizer-to-water map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the soil values to the fertilizer values
        long[,] fertilizer = MapNext(soil, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**FERTILIZER-TO-WATER**

        //Loop until the next set of map values
        while (line != "water-to-light map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the fertiizer values to the water values
        long[,] water = MapNext(fertilizer, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**WATER-TO-LIGHT**

        //Loop until the next set of map values
        while (line != "light-to-temperature map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the water values to the light values
        long[,] light = MapNext(water, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**LIGHT-TO-TEMPERATURE**

        //Loop until the next set of map values
        while (line != "temperature-to-humidity map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the light values to the temperature values
        long[,] temperature = MapNext(light, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**TEMPERATURE-TO-HUMIDITY**

        //Loop until the next set of map values
        while (line != "humidity-to-location map:")
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Convert the list to an integer array and map the temperature values to the humidity values
        long[,] humidity = MapNext(temperature, ToIntArray(currentMap));

        //Reset the map values list
        currentMap.Clear();


        //**HUMIDITY-TO-LOCATION**

        //Loop until the end of the file
        while (line != null)
        {
            //If there are three numbers on the line, add them to the list
            if (rxNum.Count(line) == 3) currentMap.Add(line);

            //Read the next line of the file
            line = sr.ReadLine();
        }

        //Close the file
        sr.Close();

        //Convert the list to an integer array and map the humidity values to the location values
        long[,] location = MapNext(humidity, ToIntArray(currentMap));

        //Find the lowest location number and write it to the console
        long min = location[0,0];
        for (int i = 0; i <= location.GetUpperBound(0); i++)
        {
            if (location[i,0] < min) min = location[i,0];
        }
        Console.WriteLine(min);
    }

    //Converts a list of strings with three numbers on each line into a 2D array of long integers
    static long[,] ToIntArray(List<string> input)
    {
        //Array to store the output
        long[,] output = new long[input.Count,3];

        //String to store each line
        string line;

        //For each line, use a regular expression to find the numbers and add them to the output array
        for (int i = 0; i < input.Count; i++)
        {
            line = input[i];
            MatchCollection matches = rxNum.Matches(line);
            for (int j = 0; j < 3; j++)
            {
                output[i,j] = long.Parse(matches[j].Value);
            }
        }
        return output;
    }

    //Takes a 2D long integer array of input values, and a 2D long integer array of mapping values
    //The input value array should have two columns: The Source Range Start, and the Range Length
    //The mapping value array should have three columns: The Destination Range Start, the Source Range Start, and the Range Length
    //Using these values, the subroutine maps the input array values onto an output array value
    //If the input value did not fit within the range of any of the mapping values, the output value defaults to the input value
    //If the input range only partially fits within a mapping range, separate output ranges are created for the part that fit, and the part that did not
    //The output range that did not fit within the mapping range is then checked to see if it fits within a different mapping range
    static long[,] MapNext(long[,] input, long[,] maps)
    {
        //Lists of long integers to store the output Range Start and Range Length values as they are calculated
        List<long> outputStart = [];
        List<long> outputRange = [];

        //Variables to hold the input Range Start and Range Length values
        long inStart;
        long inRange;

        //Variables to hold the map Destination, Source, and Range Length values
        long mapDest;
        long mapSource;
        long mapRange;

        //Loop through each of the input values
        for (int i = 0; i <= input.GetUpperBound(0); i++)
        {
            //Get the input Range Start and Range Length values
            inStart = input[i,0];
            inRange = input[i,1];

            //By default make the output value the input value
            outputStart.Add(inStart);
            outputRange.Add(inRange);

            //Search through the maps to see if any match the input value
            int j = 0;
            while (j <= maps.GetUpperBound(0))
            {
                //Set the destination, source, and range to the appropriate values
                mapDest = maps[j,0];
                mapSource = maps[j,1];
                mapRange = maps[j,2];


                //If the input start value is inside the map range, set the appropriate output Range Start value
                if (inStart >= mapSource && inStart < mapSource + mapRange)
                {
                    outputStart[^1] = mapDest + (inStart - mapSource);

                    //If the input end value is outside the map range, add the portion within the map range to the output
                    // then create a new input range to search for the remainder
                    if (inStart + inRange > mapSource + mapRange)
                    {
                        //Set the appropriate output Range Length value
                        outputRange[^1] = (mapSource + mapRange) - inStart;

                        //Calculate new input Range Start and Range Length values
                        inStart =  mapSource + mapRange;
                        inRange -= outputRange[^1];

                        //By default make the new output values the new input values
                        outputStart.Add(inStart);
                        outputRange.Add(inRange);

                        //Reset j to search the maps from the beginning
                        j = -1;
                    }
                }
                //Increment j to check the next map
                j++;
            }
        }

        //Create a 2D array to hold the output and populate it with the values from the lists
        long[,] output = new long[outputStart.Count,2];
        for (int i = 0; i < outputStart.Count; i++)
        {
            output[i,0] = outputStart[i];
            output[i,1] = outputRange[i];
        }
        return output;
    }
}
