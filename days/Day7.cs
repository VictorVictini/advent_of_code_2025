namespace AdventOfCode2025 {
    public class Day7 : Day<long> {
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
        public override long Part1() {
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
                    queue.Enqueue((x + 1, y + 1));
                    queue.Enqueue((x + 1, y - 1));

                    // increment split counter
                    count++;

                // traverse normally
                } else {
                    queue.Enqueue((x + 1, y));
                }
            }
            return count;
        }
        public override long Part2() {
            // keeps track of how many paths have reached a point, and adds it to the next point

            // standard BFS approach
            bool[,] visited = new bool[map.Length, map[0].Length];
            long[,] paths = new long[map.Length, map[0].Length];
            (int startX, int startY) = startCoord;
            paths[startX, startY] = 1;
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(startCoord);
            while (queue.Count > 0) {
                // visit current node
                (int x, int y) = queue.Dequeue();
                if (x < 0 || x >= map.Length) continue;
                if (y < 0 || y >= map[x].Length) continue;
                if (visited[x, y]) continue;
                visited[x, y] = true;

                // split if needed
                if (map[x][y] == SPLITTER_SYMBOL) {
                    queue.Enqueue((x + 1, y + 1));
                    if (x + 1 < map.Length && y + 1 < map[x].Length) paths[x + 1, y + 1] += paths[x, y];
                    queue.Enqueue((x + 1, y - 1));
                    if (x + 1 < map.Length && y - 1 >= 0) paths[x + 1, y - 1] += paths[x, y];

                // traverse normally
                } else {
                    queue.Enqueue((x + 1, y));
                    if (x + 1 < map.Length) paths[x + 1, y] += paths[x, y];
                }
            }

            // calculate sum of paths that reached final row
            long count = 0;
            for (int i = 0; i < map[^1].Length; i++) {
                count += paths[map.Length - 1, i];
            }
            return count;
        }
    }
}