using System.Security.Cryptography;

namespace AdventOfCode2025 {
    public class Day8 : Day<int> {
        private int[][] coords;
        public Day8() {
            coords = ReadFile();
        }
        private int[][] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day8.txt");
            int[][] coordinates = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                string[] components = lines[i].Split(",");
                coordinates[i] = new int[]{
                    Convert.ToInt32(components[0]),
                    Convert.ToInt32(components[1]),
                    Convert.ToInt32(components[2])
                };
            }
            return coordinates;
        }
        public override int Part1() {
            int ITERATIONS = 1000;
            int TOP_AMOUNT = 3;

            // set up initial circuits
            HashSet<int>[] circuits = new HashSet<int>[coords.Length];
            for (int i = 0; i < circuits.Length; i++) {
                circuits[i] = new HashSet<int>{i};
            }

            bool[,] tried = new bool[coords.Length, coords.Length];
            for (int _ = 0; _ < ITERATIONS; _++) {
                // find smallest euclidean distance
                (int, int) min = (0, 0);
                double dist = Int32.MaxValue;
                for (int i = 0; i < coords.Length; i++) {
                    for (int j = i + 1; j < coords.Length; j++) {
                        if (tried[i, j]) continue;
                        double curr = EuclideanDistance(coords[i], coords[j]);
                        if (curr < dist) {
                            min = (i, j);
                            dist = curr;
                        }
                    }
                }

                // visit them
                (int first, int second) = min;
                tried[first, second] = true;
                tried[second, first] = true;

                // merge them
                circuits[first].UnionWith(circuits[second]);
                foreach (int i in circuits[second]) {
                    circuits[i] = circuits[first];
                }
            }
            
            // sort circuits by size
            Array.Sort(circuits, (e1, e2) => e2.Count.CompareTo(e1.Count));

            // calculate product of top circuits
            int product = 1;
            HashSet<HashSet<int>> visited = new HashSet<HashSet<int>>();
            for (int i = 0, used = 0; used < TOP_AMOUNT && i < circuits.Length; i++) {
                if (visited.Contains(circuits[i])) continue;
                visited.Add(circuits[i]);
                product *= circuits[i].Count;
                used++;
            }
            return product;
        }
        public override int Part2() {
            return -1;
        }
        private double EuclideanDistance(int[] first, int[] second) {
            return Math.Pow(first[0] - second[0], 2) +
                   Math.Pow(first[1] - second[1], 2) +
                   Math.Pow(first[2] - second[2], 2);
        }
    }
}