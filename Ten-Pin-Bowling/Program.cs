using System;
using System.Linq;

namespace Ten_Pin_Bowling {
    
    class Program {
        
        static void Main(string[] args) {
                                                                             // Should be (according to random website):
            Console.WriteLine(bowlingScore("22 54 90 80 53 36 90 81 70 44")); // 80
            Console.WriteLine(bowlingScore("X X 9/ 80 X X 90 8/ 7/ 44"));    // 171
            Console.WriteLine(bowlingScore("X X X X X X X X X XXX"));       // 300
            Console.WriteLine(bowlingScore("9/ 54 8/ 52 4/ 6/ 4/ 8/ 21 XXX")); // 139
            Console.WriteLine(bowlingScore("X X X 8/ X 8/ 5/ X X 7/X"));   // 220
        }

        static int bowlingScore(string score) {
            string[] frames = score.Split(' ');
            if (frames.Length == 1) {
                if (frames[0].Length == 1) {
                    if (new char[] {'X', '/'}.Contains(frames[0][0])) {
                        return 10;
                    }
                    return Convert.ToInt32(Convert.ToString(frames[0][0]));
                }

                if (frames[0].Length == 2 && frames[0].Contains('/')) {
                    return 10;
                }
                return frames[0][0] == 'X' ? 10 + bowlingScore(frames[0].Substring(1)):
                    frames[0][1] == '/' ? 10 + bowlingScore(Convert.ToString(frames[0][2])):
                    Convert.ToInt32(Convert.ToString(frames[0][0])) + Convert.ToInt32(Convert.ToString(frames[0][1]));
            }
            int fScore = frames[0].Contains('/') ? 10 + bowlingScore(Convert.ToString(frames[1][0])):
                frames[0].Contains('X') && frames[1].Length < 2 ? 10 + bowlingScore(String.Concat(frames[1], frames[2]).Substring(0, 2)):
                frames[0].Contains('X') ? 10 + bowlingScore(frames[1].Substring(0, 2)):
                Convert.ToInt32(Convert.ToString(frames[0][0])) + Convert.ToInt32(Convert.ToString(frames[0][1]));
            int total = fScore + bowlingScore(String.Join(' ', frames.Skip(1)));
            return total;
        }
    }
}