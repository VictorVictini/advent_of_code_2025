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

            // set up order of traversal
            (int, int)[] values = new (int, int)[coords.Length * coords.Length];
            for (int i = 0; i < coords.Length; i++) {
                for (int j = 0; j < coords.Length; j++) {
                    values[coords.Length * i + j] = (i, j);
                }
            }

            // sort values based on Euclidean Algorithm
            Array.Sort(values, (e1, e2) => {
                (int first1, int second1) = e1;
                (int first2, int second2) = e2;
                return EuclideanDistance(coords[first1], coords[second1]).CompareTo(EuclideanDistance(coords[first2], coords[second2]));
            });

            // set up initial circuits
            HashSet<int>[] circuits = new HashSet<int>[coords.Length];
            for (int i = 0; i < circuits.Length; i++) {
                circuits[i] = new HashSet<int>{i};
            }

            // go through with a visited array
            bool[,] occupied = new bool[coords.Length, coords.Length];
            for (int i = 0; i < ITERATIONS && i < values.Length; i++) {
                // get smallest unvisited euclidean distance
                (int first, int second) = values[i];
                if (first == second || occupied[first, second] || occupied[second, first]) {
                    ITERATIONS++;
                    continue;
                }

                // visit it
                occupied[first, second] = true;
                occupied[second, first] = true;
                if (circuits[first].Contains(second)) continue;

                // merge them
                circuits[first].UnionWith(circuits[second]);
                foreach (int box in circuits[second]) {
                    circuits[box] = circuits[first];
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
            // set up order of traversal
            (int, int)[] values = new (int, int)[coords.Length * coords.Length];
            for (int i = 0; i < coords.Length; i++) {
                for (int j = 0; j < coords.Length; j++) {
                    values[coords.Length * i + j] = (i, j);
                }
            }

            // sort values based on Euclidean Algorithm
            Array.Sort(values, (e1, e2) => {
                (int first1, int second1) = e1;
                (int first2, int second2) = e2;
                return EuclideanDistance(coords[first1], coords[second1]).CompareTo(EuclideanDistance(coords[first2], coords[second2]));
            });

            // set up initial circuits
            HashSet<int>[] circuits = new HashSet<int>[coords.Length];
            for (int i = 0; i < circuits.Length; i++) {
                circuits[i] = new HashSet<int>{i};
            }

            // go through with a visited array
            bool[,] occupied = new bool[coords.Length, coords.Length];
            (int, int) last = (-1, -1);
            for (int i = 0; i < values.Length; i++) {
                // get smallest unvisited euclidean distance
                (int first, int second) = values[i];
                if (first == second || occupied[first, second] || occupied[second, first]) continue;

                // visit it
                occupied[first, second] = true;
                occupied[second, first] = true;
                if (circuits[first].Contains(second)) continue;
                last = values[i];

                // merge them
                circuits[first].UnionWith(circuits[second]);
                foreach (int box in circuits[second]) {
                    circuits[box] = circuits[first];
                }
            }
            
            // calculate result
            (int a, int b) = last;            
            return coords[a][0] * coords[b][0];
        }
        private double EuclideanDistance(int[] first, int[] second) {
            return Math.Pow(first[0] - second[0], 2) +
                   Math.Pow(first[1] - second[1], 2) +
                   Math.Pow(first[2] - second[2], 2);
        }
    }
}