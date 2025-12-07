namespace AdventOfCode2025 {
    public class Day7 : Day<int> {
        private string[] map;
        private (int, int) startCoord;
        private char SPLITTER_SYMBOL = '^';
        private char START_SYMBOL = 'S';
        public Day7() {
            (map, startCoord) = ReadFile();
        }
        private (string[], (int, int)) ReadFile() {
            string[] mapRes = File.ReadAllLines("inputs/day7.txt");
            (int, int) start = (0, 0);
            bool found = false;
            for (int i = 0; i < mapRes.Length && !found; i++) {
                for (int j = 0; j < mapRes[i].Length; j++) {
                    if (mapRes[i][j] == START_SYMBOL) {
                        start = (i, j);
                        found = true;
                        break;
                    }
                }
            }
            return (mapRes, start);
        }
        public override int Part1() {
            // standard BFS approach
            bool[,] visited = new bool[map.Length, map[0].Length];
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(startCoord);
            int count = 0;
            while (queue.Count > 0) {
                // visit current node
                (int x, int y) = queue.Dequeue();
                if (x < 0 || x >= map.Length) continue;
                if (y < 0 || y >= map[x].Length) continue;
                if (visited[x, y]) continue;
                visited[x, y] = true;

                // split if needed
                if (map[x][y] == SPLITTER_SYMBOL) {
                    queue.Enqueue((x, y + 1));
                    queue.Enqueue((x, y - 1));

                    // increment split counter
                    count++;

                // traverse normally
                } else {
                    queue.Enqueue((x + 1, y));
                }
            }
            return count;
        }
        public override int Part2() {
            return -1;
        }
    }
}