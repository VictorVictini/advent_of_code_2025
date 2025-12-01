namespace AdventOfCode2025 {
    public class Day1 : Day<int> {
        private struct Instruction {
            public int direction, distance;
            public Instruction(bool isRight, int distance) {
                this.direction = isRight ? 1 : -1;
                this.distance = distance;
            }
        }
        private Instruction[] instructions;
        public Day1() {
            instructions = ReadFile();
        }
        private Instruction[] ReadFile() {
            string[] lines = File.ReadAllLines("inputs/day1.txt");
            Instruction[] inst = new Instruction[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                inst[i] = new Instruction(line[0] == 'R', Convert.ToInt32(line.Substring(1)));
            }
            return inst;
        }
        public override int Part1() {
            int INITIAL_VALUE = 50;
            int UPPER_LIMIT = 100;

            int dial = INITIAL_VALUE;
            int count = 0;
            foreach (Instruction inst in instructions) {
                // apply instruction
                dial += inst.direction * inst.distance;
                dial %= UPPER_LIMIT;
                while (dial < 0) dial += UPPER_LIMIT;

                // check value
                if (dial == 0) count++;
            }
            return count;
        }
        public override int Part2() {
            int INITIAL_VALUE = 50;
            int UPPER_LIMIT = 100;

            // brute force simulation
            int dial = INITIAL_VALUE;
            int count = 0;
            foreach (Instruction inst in instructions) {
                for (int i = 0; i < inst.distance; i++) {
                    dial += inst.direction;
                    dial %= UPPER_LIMIT;
                    if (dial < 0) dial += UPPER_LIMIT;
                    if (dial == 0) count++;
                }
            }
            return count;
        }
    }
}