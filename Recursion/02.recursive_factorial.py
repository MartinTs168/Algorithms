def calc_factorial(num):
    if num == 1:
        return num
    return num * calc_factorial(num - 1)


print(calc_factorial(int(input())))
