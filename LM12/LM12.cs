using System;

namespace LM12
{
    // An enum representing our five states A, B, C, D, and Accept
    public enum State {A, B, C, D, Accept}
    
    public class Program
    {
        // Method that will prints a state (final state for our context)
        private static void PrintFinalState(string binaryInput, State endState) {
            Console.WriteLine($"It ended in state " + endState);
        }
        
        // Helper method to print state changes
        private static void PrintStateChange(State currState, State nextState, int changeNum) {
            Console.WriteLine($"State Change #" + (changeNum + 1) + ": " + currState + " -> " + nextState);
        }
        
        // State machine. Takes in a state and the char to parse. Returns the state to transition to
        private static State StateTransition(State currState, char input) {
            switch(currState) {
                case State.A when input == '0':
                    return State.B;
                case State.A when input == '1':
                    return State.A;
                case State.B when input == '0':
                    return State.C;
                case State.B when input == '1':
                    return State.A;
                case State.C when input == '0':
                    return State.D;
                case State.C when input == '1':
                    return State.A;
                case State.D when input == '0':
                    return State.Accept;
                case State.D when input == '1':
                    return State.A;
                default:
                    return State.Accept;
            }
        }
        
        // Handles calls to the state machine
        public static bool ValidateString(string input) {
            State currState = State.A;

            for(int i = 0; i < input.Length; i++) {
                State newState = StateTransition(currState, input[i]);
                PrintStateChange(currState, newState, i);
                currState = newState;
            }

            PrintFinalState(input, currState);
            
            return currState == State.Accept;
        }
        
        static void Main()
        {
            // List of LM12's binary sequences
            string[] binarySequences = new string[8] {
                "0111101001",
                "0010001000",
                "1111111111",
                "0011110000",
                "0101111111",
                "1111100000",
                "1111110000",
                "0100100010"
            };

            // Passes each binary sequence into the state machine and prints them
            for(int i = 0; i < binarySequences.Length; i++) {
                Console.WriteLine("Your entered binary sequence: " + binarySequences[i]);
                
                if(ValidateString(binarySequences[i])) {
                    Console.WriteLine("Sequence Accepted!\n");
                }
                else Console.WriteLine("Sequence Denied\n");
            }
        }
    }
}