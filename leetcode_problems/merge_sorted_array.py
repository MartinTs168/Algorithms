class Solution:
    def merge(self, nums1, m, nums2, n):

        if m == 0:

            for i in range(n):
                nums1[i] = nums2[i]

            nums1 = nums1[:n]

        elif n != 0:

            i = 0
            j = 0
            while i < n:
                val_to_insert = nums2[i]
                while j < m + n:
                    if val_to_insert < nums1[j] or nums1[j] == 0 and j >= m:
                        nums1.insert(j, val_to_insert)
                        nums1.pop()
                        m += 1
                        j += 1
                        break
                    j += 1
                i += 1

        print(nums1)


obj = Solution()

nums1 = [1, 2, 3, 0, 0, 0]
m = 3
nums2 = [2, 5, 6]
n = 3

obj.merge(nums1, m, nums2, n)
obj.merge(nums1, 0, nums2, n)

nums1 = [-1, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0]
m = 5
nums2 = [-1, -1, 0, 0, 1, 2]
n = 6

obj.merge(nums1, m, nums2, n)
