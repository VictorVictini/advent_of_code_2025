using System.Text.RegularExpressions;

namespace AdventOfCode2025 {
    public class Day6 : Day<long> {
        private struct Calculation {
            public List<int> numbers;
            public char operation;
            public Calculation(char operation) {
                this.operation = operation;
                numbers = new List<int>();
            }
            public void AddNumber(int number) {
                numbers.Add(number);
            }
        }
        private char EMPTY_SPACE = ' ';
        private List<int>[] nums;
        private List<char> ops;
        string[] hw;
        public Day6() {
            (nums, ops, hw) = ReadFile();
        }
        private (List<int>[], List<char>, string[]) ReadFile() {
            string PATTERN = @"\s+";
            string[] lines = File.ReadAllLines("inputs/day6.txt");

            // parse numbers
            List<int>[] numbers = new List<int>[lines.Length - 1];
            for (int i = 0; i < lines.Length - 1; i++) {
                string[] components = Regex.Split(lines[i], PATTERN);
                numbers[i] = new List<int>();
                foreach (string part in components) {
                    if (part == "") continue;
                    numbers[i].Add(Convert.ToInt32(part));
                }
            }

            // parse characters
            List<char> operations = new List<char>();
            foreach (string component in Regex.Split(lines[^1], PATTERN)) {
                if (component == "") continue;
                operations.Add(component[0]);
            }
            return (numbers, operations, lines);
        }
        public override long Part1() {
            // simulation approach
            long sum = 0;
            for (int i = 0; i < ops.Count; i++) {
                long curr = nums[0][i];
                for (int j = 1; j < nums.Length; j++) {
                    curr = ApplyOperation(ops[i], curr, nums[j][i]);
                }
                sum += curr;
            }
            return sum;
        }
        public override long Part2() {
            // go through each column
            List<Calculation> calculations = new List<Calculation>();
            for (int i = 0; i < hw[0].Length; i++) {
                // create a new calculation if needed
                if (hw[^1][i] != EMPTY_SPACE) calculations.Add(new Calculation(hw[^1][i]));
                
                // create the number
                int num = 0;
                for (int j = 0; j < hw.Length - 1; j++) {
                    if (hw[j][i] == EMPTY_SPACE) continue;
                    num = num * 10 + (hw[j][i] - '0');
                }
                if (num == 0) continue;
                calculations[^1].AddNumber(num);
            }

            // calculate total
            long sum = 0;
            foreach (Calculation calculation in calculations) {
                sum += ApplyCalculation(calculation);
            }
            return sum;
        }
        // calculate the result of a calculation
        // i.e. apply the relevant math operation
        private long ApplyCalculation(Calculation calc) {
            if (calc.numbers.Count == 0) return 0;
            long res = calc.numbers[0];
            for (int i = 1; i < calc.numbers.Count; i++) {
                res = ApplyOperation(calc.operation, res, calc.numbers[i]);
            }
            return res;
        }

        // applies a single math operation to 2 numbers
        private long ApplyOperation(char operation, long num1, long num2) {
            switch (operation) {
                case '*':
                    return num1 * num2;
                case '+':
                    return num1 + num2;
                default:
                    throw new Exception("Unrecognised operation: " + operation);
            }
        }
    }
}