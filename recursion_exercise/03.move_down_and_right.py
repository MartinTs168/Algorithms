def find_all_path(row, col, rows, cols):
    if row >= rows or col >= cols:
        return 0
    if row == rows - 1 and col == cols - 1:
        return 1
    return find_all_path(row + 1, col, rows, cols) + find_all_path(row, col + 1, rows, cols)


rows = int(input())
cols = int(input())

print(find_all_path(0, 0, rows, cols))
