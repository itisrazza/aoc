#!/usr/bin/env python3

from sys import argv
from typing import TextIO


class Move:
    _opponent_lookup = {}
    _init_count = 3

    def __init__(self, name: str, score: int, opponent_move: str) -> None:
        # this is really hacky, but we can only allow ROCK, PAPER, SCISSORS to instantiate
        if Move._init_count <= 0:
            raise Exception(
                "You cannot init this class, use ROCK, PAPER, SCISSORS, or a lookup method")
        Move._init_count -= 1

        self.name = name
        self.score = score
        self.opponent_move = opponent_move

        # store lookup table for later
        Move._opponent_lookup[opponent_move] = self

    def of_opponent(opponent_move: str) -> "Move":
        return Move._opponent_lookup[opponent_move]

    def ends(self, other: str) -> "Move":
        if other == DRAW_MOVE:
            return self
        elif other == WIN_MOVE:
            if self == ROCK:
                return PAPER
            elif self == PAPER:
                return SCISSORS
            elif self == SCISSORS:
                return ROCK
            else:
                raise Exception(
                    "Move.ends: self should not be anything other than ROCK, PAPER, SCISSORS")
        elif other == LOSE_MOVE:
            if self == ROCK:
                return SCISSORS
            elif self == PAPER:
                return ROCK
            elif self == SCISSORS:
                return PAPER
            else:
                raise Exception(
                    "Move.ends: self should not be anything other than ROCK, PAPER, SCISSORS")
        else:
            raise Exception("Move.ends: other is not a known end string")

    def beats(self, other: "Move") -> bool:
        if self == ROCK:
            return other == SCISSORS
        elif self == PAPER:
            return other == ROCK
        elif self == SCISSORS:
            return other == PAPER
        else:
            raise Exception("Move.beats: other is not a known string")

    def __str__(self) -> str:
        return f"{self.name} ({self.score})"

    def __repr__(self) -> str:
        return f"Move(\"{self.name}\", score={self.score}, opponent_move='{self.opponent_move}', player_move='{self.player_move}')"


ROCK = Move("Rock", score=1, opponent_move='A')
PAPER = Move("Paper", score=2, opponent_move='B')
SCISSORS = Move("Scissors", score=3, opponent_move='C')

LOSE_MOVE = 'X'
DRAW_MOVE = 'Y'
WIN_MOVE = 'Z'

LOSE_SCORE = 0
DRAW_SCORE = 3
WIN_SCORE = 6


def round(opponent_move: Move, player_move: Move) -> int:
    if opponent_move == player_move:
        print(f"result:   DRAW ({DRAW_SCORE})")
        return DRAW_SCORE + player_move.score

    if opponent_move.beats(player_move):
        print(f"result:   LOSE ({LOSE_SCORE})")
        return LOSE_SCORE + player_move.score

    if player_move.beats(opponent_move):
        print(f"result:   WIN ({WIN_SCORE})")
        return WIN_SCORE + player_move.score

    raise Exception("something went wrong here")


def play_game(input: TextIO):
    score = 0

    while True:
        line = input.readline()
        if line == '':
            break

        opponent_move_key, end = line.strip().split(' ')

        opponent_move = Move.of_opponent(opponent_move_key)
        player_move = opponent_move.ends(end)

        print(f"opponent: {opponent_move}")
        print(f"player:   {player_move}")
        score += round(opponent_move, player_move)
        print()

    print(f"total score: {score}")


if __name__ == "__main__":
    if len(argv) < 2:
        print("Usage: ./program <input>")
        exit(1)

    with open(argv[1], "r") as input:
        play_game(input)
