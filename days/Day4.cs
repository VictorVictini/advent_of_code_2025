namespace AdventOfCode2025 {
    public class Day4 : Day<int> {
        private int[][] changes = new int[][]{
            new int[]{-1, -1},
            new int[]{ 0, -1},
            new int[]{ 1, -1},
            new int[]{-1,  0},
            new int[]{ 1,  0},
            new int[]{-1,  1},
            new int[]{ 0,  1},
            new int[]{ 1,  1}
        };
        private char PAPER_ROLL_SYMBOL = '@';
        private char EMPTY_SPACE_SYMBOL = '.';
        private char[][] map; // char[] means we can edit the values with less hassle compared to strings
        public Day4() {
            map = ReadFile();
        }
        private char[][] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day4.txt");
            char[][] res = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                res[i] = lines[i].ToCharArray();
            }
            return res;
        }
        public override int Part1() {
            int LIMIT = 4;

            // count paper rolls that are within limits
            int count = 0;
            for (int x = 0; x < map.Length; x++) {
                for (int y = 0; y < map[x].Length; y++) {
                    if (map[x][y] == PAPER_ROLL_SYMBOL && CountPaperRolls(x, y, map) < LIMIT) count++;  
                }
            }
            return count;
        }
        public override int Part2() {
            int LIMIT = 4;

            // may be more optimised to use a bfs approach

            // repeat until no more coords are found
            int count = 0;
            List<(int, int)> coords = new List<(int, int)>();
            do {
                // add all coords to a list
                coords.Clear();
                for (int x = 0; x < map.Length; x++) {
                    for (int y = 0; y < map[x].Length; y++) {
                        if (map[x][y] == PAPER_ROLL_SYMBOL && CountPaperRolls(x, y, map) < LIMIT) {
                            coords.Add((x, y));
                        }
                    }
                }

                // add number of coords found to count
                count += coords.Count;

                // set coords to empty spaces
                foreach ((int x, int y) in coords) {
                    map[x][y] = EMPTY_SPACE_SYMBOL;
                }
            } while (coords.Count > 0); // post-check since it's empty initially
            return count;
        }
        private int CountPaperRolls(int x, int y, char[][] map) {
            int count = 0;
            foreach (int[] change in changes) {
                // out of bounds checks
                int newX = x + change[0];
                int newY = y + change[1];
                if (newX < 0 || newX >= map.Length) continue;
                if (newY < 0 || newY >= map[newX].Length) continue;

                // increment count if paper roll
                if (map[newX][newY] == PAPER_ROLL_SYMBOL) count++;
            }
            return count;
        }
    }
}