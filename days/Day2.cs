namespace AdventOfCode2025 {
    public class Day2 : Day<long> {
        private struct Range {
            public long start, end;
            public Range(long start, long end) {
                this.start = start;
                this.end = end;
            }
        }
        private Range[] sequences;
        public Day2() {
            sequences = ReadFile();
        }
        private Range[] ReadFile() {
            string contents = File.ReadAllText("inputs/day2.txt");
            string[] components = contents.Split(",");
            Range[] seq = new Range[components.Length];
            for (int i = 0; i < components.Length; i++) {
                string[] parts = components[i].Split("-");
                seq[i] = new Range(Convert.ToInt64(parts[0]), Convert.ToInt64(parts[1]));
            }
            return seq;
        }
        public override long Part1() {
            long sum = 0;
            foreach (Range seq in sequences) {
                for (long i = seq.start; i <= seq.end; i++) {
                    if (InvalidIDPart1(i)) sum += i;
                }
            }
            return sum;
        }
        public override long Part2() {
            long sum = 0;
            foreach (Range seq in sequences) {
                for (long i = seq.start; i <= seq.end; i++) {
                    if (InvalidIDPart2(i)) sum += i;
                }
            }
            return sum;
        }

        private bool InvalidIDPart1(long ID) {
            return isEqualSegments(Convert.ToString(ID), 2);
        }
        private bool InvalidIDPart2(long ID) {
            // check dividing ID by 2 onwards
            string strID = Convert.ToString(ID);
            for (int divisor = 2; divisor <= strID.Length; divisor++) {
                if (isEqualSegments(strID, divisor)) return true;
            }
            return false;
        }
        private bool isEqualSegments(string ID, int divisor) {
            // check the ID can be split into divisor segments
            if ((ID.Length % divisor) != 0) return false;

            // check each segment is equal
            for (int i = 0; i < ID.Length; i++) {
                if (ID[i] != ID[i % (ID.Length / divisor)]) return false;
            }
            return true;
        }
    }
}