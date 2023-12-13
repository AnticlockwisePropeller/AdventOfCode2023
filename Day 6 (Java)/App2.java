package com.adventofcode2023;
import java.io.File;  // Import the File class
import java.io.FileNotFoundException; // Import the IOException class to handle errors
import java.util.Scanner; // Import the Scanner class to read text files
import java.util.regex.Matcher; // Import the regex Matcher class
import java.util.regex.Pattern; // Import the regex Pattern class

//The program reads a text file containing the result of a toy boat race
//This program treats the data in the file as the result of one race, with bad kerning
//The race is of a set amount of time, and the winning boat is the one which travelled the furthest
//The first line of the file contains the time limit of the race (in milliseconds)
//The second line contains the record distance a winning boat has travelled (in millimetres)
//When a race starts, you hold down a button to set the boat's speed. The boat doesn't move while the button is held
//For each millisecond the button is held, the boat's speed will increase by 1 mm/ms
//When you release the button, the boat starts to move at a constant speed for the remainder of the race
//The program determines the number of ways it is possible to beat the record for the race, and prints the result to the console

public class App {
    public static void main( String[] args ) {
        //Regular expression to match numbers
        Pattern rxNum = Pattern.compile("\\d+");
        Matcher matcher;

        //Variable to concatenate the regex matches as they are being read
        String data = "";

        //Variables to store the time, record distance, and ways to beat the race
        long time = 0;
        long distance = 0;
        long waysToBeat = 0;

        //Read the input file and add the time and distance values to the arrays
        File inputFile = new File("input.txt");
        Scanner myReader;
        try {
            //Read the first line of the file
            myReader = new Scanner(inputFile);
            String line = myReader.nextLine();

            //For each matching number, concatenate it with the data string
            matcher = rxNum.matcher(line);
            while (matcher.find()) {
                data += matcher.group();
            }
            //Convert the data string to a long integer and set the time value
            time = Long.parseLong(data);

            //Reset the data string and read the next line
            data = "";
            line = myReader.nextLine();

            //For each matching number, concatenate it with the data string
            matcher = rxNum.matcher(line);
            while (matcher.find()) {
                data += matcher.group();
            }
            //Convert the data string to a long integer and set the distance value, then close the file
            distance = Long.parseLong(data);
            myReader.close();

            //Calculate the number of ways to beat the record for each race, then print the result to the console
            for (long i = 1; i < time; i++) {
                if (i * (time - i) > distance) waysToBeat++;
            }
            System.out.println(waysToBeat);

        //Handle FileNotFoundException
        } catch (FileNotFoundException e) {
            System.out.println( "An error occurred" );
            e.printStackTrace();
        }
    }
}
