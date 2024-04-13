def print_board(board):
    for row in board:
        print(' '.join(row))
    print()


def can_place_queen(row, col, rows, cols, l_diagonals, r_diagonals):
    if row in rows:
        return False
    if col in cols:
        return False
    if row - col in l_diagonals:
        return False
    if row + col in r_diagonals:
        return False
    return True


def set_queen(row, col, board, rows, cols, l_diagonals, r_diagonals):
    board[row][col] = '*'
    rows.add(row)
    cols.add(col)
    l_diagonals.add(row - col)
    r_diagonals.add(row + col)


def remove_queen(row, col, board, rows, cols, l_diagonals, r_diagonals):
    board[row][col] = '-'
    rows.remove(row)
    cols.remove(col)
    l_diagonals.remove(row - col)
    r_diagonals.remove(row + col)


def put_queens(row, board, rows, cols, l_diagonals, r_diagonals):
    if row == 8:
        print_board(board)
        return

    for col in range(8):
        if can_place_queen(row, col, rows, cols, l_diagonals, r_diagonals):
            set_queen(row, col, board, rows, cols, l_diagonals, r_diagonals)
            put_queens(row + 1, board, rows, cols, l_diagonals, r_diagonals)
            remove_queen(row, col, board, rows, cols, l_diagonals, r_diagonals)


n = 8
board = []
[board.append(['-'] * n) for _ in range(8)]

put_queens(0, board, set(), set(), set(), set())
