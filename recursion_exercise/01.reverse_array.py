def reverse_array(arr, idx=0):
    if idx >= len(arr) // 2:
        return

    swap_index = len(arr) - 1 - idx
    arr[idx], arr[swap_index] = arr[swap_index], arr[idx]
    reverse_array(arr, idx + 1)


vector = input().split()
reverse_array(vector)
print(*vector)
