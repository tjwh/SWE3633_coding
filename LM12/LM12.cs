using System;

namespace LM12
{
    // An enum representing our five states A, B, C, D, and Accept
    public enum State {A, B, C, D, Accept}
    
    public class Program
    {
        // Method that will print the parsed binary sequence, its ending state, and a confirmation if it was accepted or denied
        public static void PrintStateStuff(string binaryInput, State endState) {
            if(endState == State.Accept) 
            Console.WriteLine($"It ended in state " + endState + " (Accepted!)\n");
            
            else Console.WriteLine($"It ended in state " + endState + " (Denied)\n");
        }
        
        // A helper method used by the state machine to print state changes without spamming like 3 lines of code in each switch case
        public static State ChangeState(State currState, State nextState, int changeNum) {
            Console.WriteLine($"State Change #" + (changeNum + 1) + ": " + currState + " -> " + nextState);

            return nextState;
        }
        
        /* 
        The state machine which takes in an input string of assumed even-length binary data and transitions through states
        according to LM12's diagram. Returns the final state of the binary sequence
        */
        public static State StateMachine(string inputBinaryString) {
            State currState = State.A; 
            
            Console.WriteLine($"Your entered binary sequence: " + inputBinaryString);
            // This is pretty ugly I guess, maybe we could discuss better ways of handling this? :)
            for(int i = 0; i < inputBinaryString.Length; i++) {
                switch(currState) {
                    case State.A when inputBinaryString[i] == '0':
                        currState = ChangeState(State.A, State.B, i);
                        break;
                    case State.A when inputBinaryString[i] == '1':
                        currState = ChangeState(State.A, State.A, i);
                        break;
                    case State.B when inputBinaryString[i] == '0':
                        currState = ChangeState(State.A, State.C, i);
                        break;
                    case State.B when inputBinaryString[i] == '1':
                        currState = ChangeState(State.A, State.A, i);
                        break;
                    case State.C when inputBinaryString[i] == '0':
                        currState = ChangeState(State.A, State.D, i);
                        break;
                    case State.C when inputBinaryString[i] == '1':
                        currState = ChangeState(State.A, State.A, i);
                        break;
                    case State.D when inputBinaryString[i] == '0':
                        currState = ChangeState(State.A, State.Accept, i);
                        break;
                    case State.D when inputBinaryString[i] == '1':
                        currState = ChangeState(State.A, State.A, i);
                        break;
                }
            }
            return currState;
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
                PrintStateStuff(binarySequences[i], StateMachine(binarySequences[i]));
            }
        }
    }
}