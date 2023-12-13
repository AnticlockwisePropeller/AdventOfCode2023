package com.adventofcode2023;
import java.io.File;  // Import the File class
import java.io.FileNotFoundException; // Import the IOException class to handle errors
import java.util.Scanner; // Import the Scanner class to read text files
import java.util.regex.Matcher; // Import the regex Matcher class
import java.util.regex.Pattern; // Import the regex Pattern class

//The program reads a text file containing the results of a set of toy boat races
//Each race is of a set amount of time, and the winning boat is the one which travelled the furthest
//The first line of the file contains the time limit of each race (in milliseconds)
//The second line contains the record distance a winning boat has travelled (in millimetres)
//When a race starts, you hold down a button to set the boat's speed. The boat doesn't move while the button is held
//For each millisecond the button is held, the boat's speed will increase by 1 mm/ms
//When you release the button, the boat starts to move at a constant speed for the remainder of the race
//The program determines the number of ways it is possible to beat the record for each race
//The program then multiplies those numbers together and prints the result to the console

public class App {
    public static void main( String[] args ) {
        //Regular expression to match numbers
        Pattern rxNum = Pattern.compile("\\d+");
        Matcher matcher;

        //Arrays to store the time, record distance, and ways to beat each race
        //(There are only four race results in the file)
        int[] time = new int[4];
        int[] distance = new int[4];
        int[] waysToBeat = new int[4];

        //Read the input file and add the time and distance values to the arrays
        File inputFile = new File("input.txt");
        Scanner myReader;
        try {
            //Read the first line of the file
            myReader = new Scanner(inputFile);
            String line = myReader.nextLine();

            //For each matching number, add the number to the time array
            matcher = rxNum.matcher(line);
            int i = 0;
            while (matcher.find()) {
                time[i] = Integer.parseInt(matcher.group());
                i++;
            }
            //Read the next line of the file
            line = myReader.nextLine();

            //For each matching number, add the number to the distance array
            matcher = rxNum.matcher(line);
            i = 0;
            while (matcher.find()) {
                distance[i] = Integer.parseInt(matcher.group());
                i++;
            }
            //Close the file
            myReader.close();

            //Calculate the number of ways to beat the record for each race
            for (i = 0; i < 4; i++) {
                for (int j = 1; j < time[i]; j++) {
                    if (j * (time[i] - j) > distance[i]) waysToBeat[i]++;
                }
            }
            //Multiply the number of ways together and print the result to the console
            System.out.println(waysToBeat[0]*waysToBeat[1]*waysToBeat[2]*waysToBeat[3]);

        //Handle FileNotFoundException
        } catch (FileNotFoundException e) {
            System.out.println( "An error occurred" );
            e.printStackTrace();
        }
    }
}
