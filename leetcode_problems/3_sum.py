from typing import List


def sumThree(nums: List[int]) -> List[List[int]]:
    nums.sort()

    result = []

    for i in range(len(nums)):
        a = i + 1
        b = len(nums) - 1
        curr_target = -nums[i]

        while a < b:
            res_calc = nums[a] + nums[b]
            if res_calc == curr_target:
                ls = [nums[i], nums[a], nums[b]]
                if ls not in result:
                    result.append(ls)

            if res_calc >= curr_target:
                b -= 1
            else:
                a += 1

    return result


print(sumThree([-1, 0, -2, 1, 3, 2]))  # Output: [[-2, -1, 3], [-2, 0, 2], [-1, 0, 1]]
print(sumThree([-1, 0, 1, 2, -1, -4]))  # Output: [[-1,-1,2],[-1,0,1]]
print(sumThree([0, 1, 1]))  # Output: []
print(sumThree([0, 0, 0]))  # Output: [[0,0,0]]
