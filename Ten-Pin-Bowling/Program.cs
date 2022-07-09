using System;
using System.Linq;

namespace Ten_Pin_Bowling {
    internal static class Program {
        private static void Main(string[] args) {// Should be (according to random website):
            Console.WriteLine(BowlingScore("22 54 90 80 53 36 90 81 70 44")); // 80
            Console.WriteLine(BowlingScore("X X 9/ 80 X X 90 8/ 7/ 44"));    // 171
            Console.WriteLine(BowlingScore("X X X X X X X X X XXX"));       // 300
            Console.WriteLine(BowlingScore("9/ 54 8/ 52 4/ 6/ 4/ 8/ 21 XXX")); // 139
            Console.WriteLine(BowlingScore("X X X 8/ X 8/ 5/ X X 7/X"));   // 220
        }

        private static int BowlingScore(string score) {
            // Split string into frames
            string[] frames = score.Split(' ');
            // If only score of one frame is being considered...
            if (frames.Length == 1) {
                // If this single frame has only one roll, if the roll was a strike or spare, return 10
                if (frames[0].Length == 1) {
                    if (new [] {'X', '/'}.Contains(frames[0][0])) {
                        return 10;
                    } // Otherwise return the integer value of the roll score
                    return Convert.ToInt32(Convert.ToString(frames[0][0]));
                }
                // If the one frame has 2 rolls with a spare, return 10
                if (frames[0].Length == 2 && frames[0].Contains('/')) {
                    return 10;
                }
                // If the one frame has a strike in the first roll, return 10 + the score for the rest of the frame
                return frames[0][0] == 'X' ? 10 + BowlingScore(frames[0][1..]):
                    // Elif there is a spare in the second position, return 10 + the score of the 3rd roll in the frame
                    frames[0][1] == '/' ? 10 + BowlingScore(Convert.ToString(frames[0][2])):
                    // Else return the sum of the first 2 rolls in the frame
                    Convert.ToInt32(Convert.ToString(frames[0][0])) + Convert.ToInt32(Convert.ToString(frames[0][1]));
            }
            // Else (if more than one frame is being considered...)
            // If the first frame being considered has a spare, set the frame score to 10 + the first roll of the next frame
            int fScore = frames[0].Contains('/') ? 10 + BowlingScore(Convert.ToString(frames[1][0])):
                // If the first frame has a strike and the frame after it has less than 2 rolls, set the frame score to 10 + the score of the next 2 frames (considering a max of 2 rolls)
                frames[0].Contains('X') && frames[1].Length < 2 ? 10 + BowlingScore(string.Concat(frames[1], frames[2])[..2]):
                // Otherwise, if the first frame has a strike, set the frame score to 10 + the first 2 rolls of the next frame 
                frames[0].Contains('X') ? 10 + BowlingScore(frames[1][..2]):
                // Otherwise, add the first two rolls in the frame as the frame score
                Convert.ToInt32(Convert.ToString(frames[0][0])) + Convert.ToInt32(Convert.ToString(frames[0][1]));
            // Set the total game score to the frame score plus the score of the rest of the frames
            int total = fScore + BowlingScore(string.Join(' ', frames.Skip(1)));
            // Return the total score
            return total;
        }
    }
}