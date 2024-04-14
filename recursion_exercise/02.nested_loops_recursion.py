def simulate_nested_loop(idx, vector):
    if idx >= len(vector):
        print(*vector, sep=' ')
        return
    for n in range(1, num + 1):
        vector[idx] = n
        simulate_nested_loop(idx + 1, vector)


num = int(input())
vector = [1] * num

simulate_nested_loop(0, vector)
