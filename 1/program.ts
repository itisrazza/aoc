import * as fs from "fs";
import { argv } from "process";

type Calories = number;
type Inventory = Calories[];

const inventoryTotal = (inventory: Inventory) => inventory.reduce((acc, val) => acc + val, 0);

const inputPath = argv.slice(2)[0];
if (typeof inputPath != "string") {
    console.error("Usage: node program.js <input>")
    process.exit(1);
}

const elves: Inventory[] = [[]];

// FIXME: woefully inefficient
const input = fs.readFileSync(inputPath, { encoding: "utf-8" }).split("\n");
for (const line of input) {
    if (line.length == 0) {
        elves.push([]);
        continue;
    }

    const elf = elves[elves.length - 1];
    elf.push(parseInt(line));
}

const sortedElves = [...elves]
    .map((elf, index) => ({
        items: elf,
        total: inventoryTotal(elf),
        index
    }))
    .sort((a, b) => b.total - a.total);

console.log(`Part 1: ${sortedElves[0].total}`);
console.log(`Part 2: ${sortedElves.slice(0, 3).map(elf => elf.total).reduce((acc, val) => acc + val, 0)}`)
