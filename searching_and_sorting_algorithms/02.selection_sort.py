def selection_sort(numbers):
    for idx in range(len(numbers)):
        min_number = numbers[idx]
        min_index = idx
        for j in range(idx + 1, len(numbers)):
            next_num = numbers[j]
            if min_number > next_num:
                min_number = next_num
                min_index = j
        numbers[idx], numbers[min_index] = numbers[min_index], numbers[idx]

    return numbers


numbers = [int(x) for x in input().split()]

print(*selection_sort(numbers), sep=' ')
