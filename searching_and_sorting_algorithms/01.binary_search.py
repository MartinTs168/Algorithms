def binary_search(nums, target):
    left = 0
    right = len(nums) - 1
    while left <= right:
        middle = (left + right) // 2
        if nums[middle] == target:
            return middle
        elif target < nums[middle]:
            right = middle - 1
        elif target > nums[middle]:
            left = middle + 1
    return -1


nums = [int(x) for x in input().split()]
target = int(input())

print(binary_search(nums, target))