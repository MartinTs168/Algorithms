from typing import List

# Given a list of stock prices, return the maximum profit
# that can be made by buying and selling a stock.
#
# If it's not possible to make any profit, return 0
#
# Input: [4, 9, 2, 8, 6, 12]
# Output: 10


def max_profit(prices: List[int]) -> int:
    a = 0
    b = 1

    max_profit = 0

    while b < len(prices):

        if prices[a] > prices[b]:
            a = b
            b += 1
        else:

            curr_profit = prices[b] - prices[a]

            if curr_profit > max_profit:
                max_profit = curr_profit

            b += 1

    return max_profit


print(max_profit([4, 9, 2, 8, 6, 12]))
