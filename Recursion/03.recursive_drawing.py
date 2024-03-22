def draw(num):
    if num <= 0:
        return

    print("*" * num)
    draw(num - 1)
    print("#" * num)


number = int(input())
draw(number)
