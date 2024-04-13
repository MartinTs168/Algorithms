def gen01(idx, vector):
    if idx >= len(vector):
        print(*vector, sep='')
        return
    for n in range(2):
        vector[idx] = n
        gen01(idx + 1, vector)


num = int(input())
vector = [0] * num

gen01(0, vector)
