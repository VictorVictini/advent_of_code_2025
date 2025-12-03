namespace AdventOfCode2025 {
    public class Day3 : Day<long> {
        private int[][] banks;
        public Day3() {
            banks = ReadFile();
        }
        private int[][] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day3.txt");
            int[][] res = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                res[i] = new int[line.Length];
                for (int j = 0; j < line.Length; j++) {
                    res[i][j] = Convert.ToInt32(line[j] - '0');
                }
            }
            return res;
        }
        public override long Part1() {
            long sum = 0;
            foreach (int[] bank in banks) {
                sum += ObtainNumber(bank, 2);
            }
            return sum;
        }
        public override long Part2() {
            long sum = 0;
            foreach (int[] bank in banks) {
                sum += ObtainNumber(bank, 12);
            }
            return sum;
        }
        private long ObtainNumber(int[] bank, int digits) {
            // repeat digit times
            // i.e. get best combination for n digits
            long res = 0;
            int lastMaxIndex = -1;
            for (int digitsLeft = digits; digitsLeft > 0; digitsLeft--) {

                // find current maximum after position of last maximum
                // while leaving room for remaining digits
                int maxIndex = -1;
                for (int i = lastMaxIndex + 1; i <= bank.Length - digitsLeft; i++) {
                    if (maxIndex == -1 || bank[i] > bank[maxIndex]) maxIndex = i;
                }

                // calculate new result
                res = (res * 10) + bank[maxIndex];

                // replace most recent maximum position
                lastMaxIndex = maxIndex;
            }
            return res;
        }
    }
}