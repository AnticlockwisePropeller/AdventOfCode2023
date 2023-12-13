package com.adventofcode2023;
import java.util.ArrayList;

//Custom class for storing and evaluating hands of five cards
//Expected input values are a string of five characters (A,K,Q,J,T, or the digits 2-9), and an integer bid value
//In this version, J cards are treated as jacks

public class Hand implements Comparable<Hand> {
    public String cards; //A string listing each card
    private int[] values; //An array representing each card as an integer value
    private String type; //A two character string representing the type of hand, e.g. "5K" for Five of a Kind
    public int bid; //An integer that stores the bid value

    //Parameterised constructor of the class
    Hand(String cards, int bid) {
        //Cards and bid are input
        this.cards = cards;
        this.bid = bid;
        //Values and type are calculated
        this.SetValues();
        this.SetType();
    }

    //Calculate then return the card values
    public int[] GetValues() {
        this.SetValues();
        return values;
    }

    //Card values are calculated based on the string input
    //(A=14, K=13, Q=12, J=11, T=10)
    private void SetValues() {
        this.values = new int[5];
        for (int i = 0; i < 5; i++) {
            switch ( cards.charAt(i) ) {
                case 'A': this.values[i] = 14; break;
                case 'K': this.values[i] = 13; break;
                case 'Q': this.values[i] = 12; break;
                case 'J': this.values[i] = 11; break;
                case 'T': this.values[i] = 10; break;
                default: this.values[i] = cards.charAt(i) - '0'; break;
            }
        }
    }

    //Calculate, then return the hand type
    public String GetType() {
        this.SetType();
        return type;
    }

    //Calculate the number of kinds of card in the hand to deterimne the hand type (Five of a Kind, Four of a Kind, Full House, etc.)
    private void SetType() {

        ArrayList<Integer> kindFaces = new ArrayList<Integer>(); //List to store the unique face values of cards in the hand
        ArrayList<Integer> kindCopies = new ArrayList<Integer>(); //List to store the number of copies of each face value

        //Add 1 copy of the first card
        kindFaces.add(values[0]);
        kindCopies.add(1);

        //For all subsequent cards, check if there is already a card with the same face value
        for (int i = 1; i < 5; i++) {
            boolean found = false;
            for (int j = 0; j < kindFaces.size(); j++) {
                //If there is already a copy, increase the number of copies by 1 and stop searching
                if (values[i] == kindFaces.get(j)) {
                    kindCopies.set(j, kindCopies.get(j) + 1);
                    found = true;
                    break;
                }
            }
            //Otherwise add one copy of the new card
            if (!found) {
                kindFaces.add(values[i]);
                kindCopies.add(1);
            }
        }

        //Set the hand type based on the number of kinds
        switch (kindCopies.size()) {
            case 1: this.type = "5K"; break;
            case 2: if (kindCopies.contains(4)) this.type = "4K"; else this.type = "FH"; break;
            case 3: if (kindCopies.contains(3)) this.type = "3K"; else this.type = "2P"; break;
            case 4: this.type = "1P"; break;
            default: this.type = "HC"; break;
        }
    }

    //Custom comparison method to sort hands by the successive values of each card
    public int compareTo(Hand hand) {
        //Compare each card value until one is found that is higher
        for (int i = 0; i < 5; i++) {
            if (values[i] > hand.values[i]) return 1;
            else if (values[i] < hand.values[i]) return -1;
        }
        //If all the cards were the same, return 0
        return 0;
    }
}
