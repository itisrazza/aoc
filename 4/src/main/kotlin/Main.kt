import java.io.File
import java.io.InputStreamReader
import kotlin.system.exitProcess

fun main(args: Array<String>) {
    if (args.isEmpty()) {
        System.err.println("Usage: MainKt <input>")
        exitProcess(1)
    }

    val list = ArrayList<String>()
    InputStreamReader(File(args.first()).inputStream()).use {
        list.addAll(it.readLines())
    }

    var countIncludes = 0
    var countOverlaps = 0

    for (line in list) {
        val pair = parseLine(line)

        if (pair.first contains pair.second || pair.second contains pair.first) {
            countIncludes += 1
        }

        if (pair.first overlaps pair.second || pair.second overlaps pair.first) {
            countOverlaps += 1
        }
    }

    println("Part 1: $countIncludes")
    println("Part 2: $countOverlaps")
}

private fun parseLine(line: String): Pair<IntRange, IntRange> {
    val (firstElf, secondElf) = line.split(',').map { parseElfRange(it) }
    return Pair(firstElf, secondElf)
}

private fun parseElfRange(elf: String): IntRange {
    val (rangeStartText, rangeEndText) = elf.split('-')
    return IntRange(rangeStartText.toInt(), rangeEndText.toInt())
}

private infix fun IntRange.contains(other: IntRange): Boolean {
    return this.first >= other.first && this.last <= other.last
}

private infix fun IntRange.overlaps(other: IntRange): Boolean {
    return this contains other || this.last >= other.first && this.first <= other.last || this.first >= other.last && this.last <= other.first
}
