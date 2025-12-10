namespace AdventOfCode2025 {
    public class Day9 : Day<long> {
        private int[][] coords;
        public Day9() {
            coords = ReadFile();
        }
        private int[][] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day9.txt");
            int[][] res = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                string[] components = lines[i].Split(",");
                res[i] = new int[]{
                    Convert.ToInt32(components[0]),
                    Convert.ToInt32(components[1])
                };
            }
            return res;
        }
        public override long Part1() {
            long max = Int64.MinValue;
            for (int i = 0; i < coords.Length; i++) {
                for (int j = 0; j < coords.Length; j++) {
                    max = Math.Max(max, CalculateArea(coords[i], coords[j]));
                }
            }
            return max;
        }
        public override long Part2() {
            return -1;
        }
        private long CalculateArea(int[] first, int[] second) {
            return (long)(Math.Abs(first[0] - second[0]) + 1) *
                   (long)(Math.Abs(first[1] - second[1]) + 1);
        }
    }
}