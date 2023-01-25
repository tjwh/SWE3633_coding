using System;

namespace StdDev {

    class Program {
        // Method to create a list of arrays containing random numbers. Array number, size, and random number bounds are all specified in-code
        public static List<int[]> generateListOfArrays() {
            List<int[]> listOfArrays = new List<int[]>();

            // Array parameters
            int amountOfArrays = 2;
            int arraySize = 3;

            // Random number parameters
            int ranMin = 1;
            int ranMax = 10;
            Random rng = new Random();

            // Creating and assigning random values to each index in each created array
            for (int i = 0; i < amountOfArrays; i++) {
                int[] newArr = new int[arraySize];

                for (int j = 0; j < arraySize; j++) {
                    newArr[j] = rng.Next(ranMin, ranMax);
                }

                listOfArrays.Add(newArr);
            }

            return listOfArrays;
        }

        // Gets the standard deviation of an array of integers, I got the two-liner standard deviation algorithm from Google, my solution was ugly :^)
        // Rounds the final double value to 3 decimal places
        public static double getStdDev(int[] arrToCalculate) {
            double avg = arrToCalculate.Average();
            return Math.Round(Math.Sqrt(arrToCalculate.Average(x=>Math.Pow(x-avg,2))), 3);
        }

        // Function to take in an array to get the standard deviation of and an index value to correctly place it in the stdDevList array
        public static void addStdDevToList(int[] arrayToGet, int index, double[] stdDevList) {
            stdDevList[index] = getStdDev(arrayToGet);
        }

        // I could not find a way to safely pass two paramters into a method to run on a thread. This is the solution I came up with :)
        public class ThreadParam {
            public int[] intArray;
            public int index;
            public double[] stdDevList;

            public ThreadParam(int[] intArray, int index, double[] stdDevList) {
                this.intArray = intArray;
                this.index = index;
                this.stdDevList = stdDevList;
            }
        }

        // Method that converts params to required signature for thread.Start()
        public static void runThread(Object? obj) {
            ThreadParam param = (ThreadParam)obj!;
            addStdDevToList(param.intArray, param.index, param.stdDevList);
        }

        static void Main(string[] args)
        {
            List<int[]> randomArrayList = generateListOfArrays();

            int arrayListSize = randomArrayList.Count;

            double[] stdDevList = new double[arrayListSize];

            // List<Thread> listOfThreads = new List<Thread>();
            Thread[] listOfThreads = new Thread[arrayListSize];

            // Creates the threads
            for (int i = 0; i < arrayListSize; i++) {
                ThreadParam param = new ThreadParam(randomArrayList[i], i, stdDevList);

                Thread thread = new Thread(runThread);
                listOfThreads[i] = thread;
                thread.Start(param);
            }

            // Joins all of our threads
            for (int i = 0; i < arrayListSize; i++) {
                listOfThreads[i].Join();
            }

            // Prints stuff
            Console.WriteLine($"Standard Deviations for {arrayListSize} Lists of Integers");
            for (int i = 0; i < arrayListSize; i++) {
                Console.WriteLine($"{i + 1}) Elements: {randomArrayList[i].Length,9:n0}    StdDev: {stdDevList[i],9}");
            }
        }
    }
}