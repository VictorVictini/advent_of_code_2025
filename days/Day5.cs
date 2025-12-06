namespace AdventOfCode2025 {
    public class Day5 : Day<long> {
        private struct Range {
            public long start, end;
            public Range(long start, long end) {
                this.start = start;
                this.end = end;
            }
        }
        private List<Range> ranges;
        private long[] IDs;
        public Day5() {
            (ranges, IDs) = ReadFile();
        }
        private (List<Range>, long[]) ReadFile() {
            string input = File.ReadAllText("inputs/day5.txt");
            string[] components = input.Split("\n\r\n");

            // calculate ranges
            string[] rangeLines = components[0].Split("\n");
            List<Range> ranges = new List<Range>();
            for (int i = 0; i < rangeLines.Length; i++) {
                string[] parts = rangeLines[i].Split("-");
                long first = Convert.ToInt64(parts[0]);
                long second = Convert.ToInt64(parts[1]);
                ranges.Add(new Range(first, second));
            }

            // calculate IDs
            string[] IDLines = components[1].Split("\n");
            long[] IDres = new long[IDLines.Length];
            for (int i = 0; i < IDLines.Length; i++) {
                IDres[i] = Convert.ToInt64(IDLines[i]);
            }
            return (ranges, IDres);
        }
        public override long Part1() {
            // brute force counter
            long count = 0;
            foreach (long ID in IDs) {
                foreach (Range range in ranges) {
                    if (ID >= range.start && ID <= range.end) {
                        count++;
                        break;
                    }
                }
            }
            return count;
        }
        public override long Part2() {
            // merge ranges until you can't
            for (int i = 0; i < ranges.Count; i++) {
                Range outer = ranges[i];
                for (int j = i + 1; j < ranges.Count; j++) {
                    Range inner = ranges[j];

                    // if they can be merged
                    if ((inner.start >= outer.start && inner.start <= outer.end) || (outer.start >= inner.start && outer.start <= inner.end)) {
                        // create new range
                        ranges.Add(new Range(Math.Min(inner.start, outer.start), Math.Max(inner.end, outer.end)));

                        // delete inner and outer ranges from list
                        ranges.RemoveAt(j); // remove outer first since it appears after inner
                        ranges.RemoveAt(i);
                        
                        // goes back an iteration since we deleted items
                        i--;
                        break;
                    }
                }
            }

            // simple sum
            long sum = 0;
            foreach (Range range in ranges) {
                sum += range.end - range.start + 1;
            }
            return sum;
        }
    }
}