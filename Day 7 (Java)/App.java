package com.adventofcode2023;
import java.io.File;  // Import the File class
import java.io.FileNotFoundException; // Import the IOException class to handle errors
import java.util.ArrayList; // Import the ArrayList class
import java.util.Collections; // Import the Collections class to sort lists
import java.util.Scanner; // Import the Scanner class to read text files
import java.util.regex.Matcher; // Import the regex Matcher class
import java.util.regex.Pattern; // Import the regex Pattern class

//The program reads a text file containing a list of Hands drawn in a card game
//The Hands consist of five cards, represented by the characters A,K,Q,J,T, or the digits 2-9
//J cards can either be handled as Jacks or as Jokers, depending on which Hand class file is used
//The card values are followed by a Bid amount
//The program identifies the Hands and sorts them according to their strength
//The strength of a hand is primarily determined by the number of kinds of card
//In descending order: Five of a Kind, Four of a Kind, Full House, Three of a Kind, Two Pairs, One Pair, and High Card
//The strength is secondarily determined by the face values of the cards, in the order they were drawn
//After the Hands have been sorted, the program calculates the winnings from each hand
//The winnings are the Bid amount multiplied by the rank of the Hand in the list
//The program then writes the total winnings to the console

public class App {
    public static void main( String[] args ) {
        //Regular expressions, first matches any hand, the second matches a bid
        Pattern rxHand = Pattern.compile("[A|K|Q|J|T|[2-9]]{5}(?= )");
        Pattern rxBid = Pattern.compile("(?<= )\\d+");
        Matcher mHand;
        Matcher mBid;

        //Lists to store the hands
        ArrayList<Hand> fiveKind = new ArrayList<Hand>();
        ArrayList<Hand> fourKind = new ArrayList<Hand>();
        ArrayList<Hand> fullHouse = new ArrayList<Hand>();
        ArrayList<Hand> threeKind = new ArrayList<Hand>();
        ArrayList<Hand> twoPair = new ArrayList<Hand>();
        ArrayList<Hand> onePair = new ArrayList<Hand>();
        ArrayList<Hand> highCard = new ArrayList<Hand>();
        ArrayList<Hand> finalList = new ArrayList<Hand>();

        //Variable to store the total winnings
        long total = 0;

        //Read the input file
        File inputFile = new File("input.txt");
        Scanner myReader;
        try {
            //For every line in the file read the hand and bid values, and add them to the appropriate list
            myReader = new Scanner(inputFile);
            while (myReader.hasNextLine()) {

                //Read the next line of the file
                String line = myReader.nextLine();

                //Find the hand and bid values
                mHand = rxHand.matcher(line);
                mBid = rxBid.matcher(line);
                mHand.find();
                mBid.find();

                //Create a new Hand object - see Hand class file
                Hand currentHand = new Hand(mHand.group(), Integer.parseInt(mBid.group()));

                //Calculate the type of hand and add it to the appropriate list
                switch (currentHand.GetType()) {
                    case "5K": fiveKind.add(currentHand); break;
                    case "4K": fourKind.add(currentHand); break;
                    case "FH": fullHouse.add(currentHand); break;
                    case "3K": threeKind.add(currentHand); break;
                    case "2P": twoPair.add(currentHand); break;
                    case "1P": onePair.add(currentHand); break;
                    case "HC": highCard.add(currentHand); break;
                }
            }
            //Close the file
            myReader.close();

            //Sort the lists into ascending order of hand strength
            //The strength is judged by the value of each successive card - see the custom comparison method in the Hand class
            Collections.sort(fiveKind);
            Collections.sort(fourKind);
            Collections.sort(fullHouse);
            Collections.sort(threeKind);
            Collections.sort(twoPair);
            Collections.sort(onePair);
            Collections.sort(highCard);

            //Combine the lists to determine the final rank of each hand
            finalList.addAll(highCard);
            finalList.addAll(onePair);
            finalList.addAll(twoPair);
            finalList.addAll(threeKind);
            finalList.addAll(fullHouse);
            finalList.addAll(fourKind);
            finalList.addAll(fiveKind);

            //Calculate the total winnings of every hand
            //The winnings equal the final rank of each hand multiplied by its bid amount
            for (int i = 0; i < finalList.size(); i++){
                Hand currentHand = finalList.get(i);
                total += currentHand.bid * (i + 1);
            }

            //Print the total winnings to the console
            System.out.println(total);

        //Handle FileNotFoundException
        } catch (FileNotFoundException e) {
            System.out.println( "An error occurred" );
            e.printStackTrace();
        }
    }
}
