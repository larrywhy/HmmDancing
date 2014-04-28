using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Topology;

namespace hmmdecoding
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Create a model with given probabilities
            //HiddenMarkovModel hmm = new HiddenMarkovModel(
            //transitions: new[,] // matrix A
            //{
            //    { 0.25, 0.25, 0.00 },
            //    { 0.33, 0.33, 0.33 },
            //    { 0.90, 0.10, 0.00 },
            //},
            //emissions: new[,] // matrix B
            //{
            //    { 0.1, 0.1, 0.8 },
            //    { 0.6, 0.2, 0.2 },
            //    { 0.9, 0.1, 0.0 },
            //},
            //initial: new[]  // vector pi
            //{ 
            //    0.25, 0.25, 0.0 
            //});

            //// Create an observation sequence of up to 2 symbols (0 or 1)
            //int[] observationSequence = new[] { 0, 1, 1, 0};

            //// Decode the sequence: the path will be 1-1-1-1-2-0-1-1
            //int[] stateSequence = hmm.Decode(observationSequence);
            //foreach (int a in stateSequence)
            //{
            //    System.Console.Write(a + " ");
            //}
            //////////////////////////////////////////////////////////////////////////////
            // Create the transation matrix A 
            double[,] transition = 
            {  
                { 0.3, 0.2 , 0.2},
                { 0.4, 0.3 , 0.1},
                { 0.4, 0.3 , 0.1}
            };

            // emission probability by kmean algo.
            double[,] emission = 
            {  
                { 0.55 , 0.075, 0.275 , 0 , 0.1},  //Dance 1
                { 0.52 , 0.22 , 0.16 , 0 , 0.1},   //Dance 2
                { 0.357, 0.357, 0.186 , 0 , 0.1}   //Dance 3
                
            };

            // Create the initial probabilities pi 
            double[] initial =
            {
                0.4, 0.2 ,0.4 
            };

            // Create a new hidden Markov model
            HiddenMarkovModel hmm = new HiddenMarkovModel(transition, emission, initial);

            // After that, one could, for example, query the probability 
            // of a sequence ocurring. We will consider the sequence 
            int[] sequence = new int[] { 0, 1, 1,1,2,2,2,2,2,2,2,1 ,2,1,1,1,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1};

            // And now we will evaluate its likelihood 
            double logLikelihood = hmm.Evaluate(sequence);

            // At this point, the log-likelihood of the sequence 
            // ocurring within the model is -3.3928721329161653. 

            // We can also get the Viterbi path of the sequence 
            int[] path = hmm.Decode(sequence, out logLikelihood);

            // At this point, the state path will be 1-0-0 and the 
            // log-likelihood will be -4.3095199438871337


            foreach (int a in path)
            {
                System.Console.Write(a + " ");
            }
            System.Console.ReadKey();
        }
    }
}
