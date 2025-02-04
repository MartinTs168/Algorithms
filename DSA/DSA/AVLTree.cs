using System.Collections;

class AVLTree<T> : IEnumerable<T> where T : IComparable<T>
{
    private class Node
    {
        //balance factor
        public int bf;

        public T value;

        public int height;

        public Node? left, right;

        public Node(T value)
        {
            this.value = value;
        }
    }

    private Node root;

    private int nodeCount = 0;

    public int Height()
    {
        if (root == null) return 0;
        return root.height;
    }

    public bool Contains(T value)
    {
        return Contains(root, value);
    }

    private bool Contains(Node node, T value)
    {
        if (node == null) return false;

        int cmp = value.CompareTo(node.value);
        if (cmp < 0)
        {
            return Contains(node.left, value);
        }
        else if (cmp > 0)
        {
            return Contains(node.right, value);
        }

        return true;
    }

    public bool Insert(T value)
    {
        if (value == null) return false;
        if (!Contains(root, value))
        {
            root = Insert(root, value);
            nodeCount++;
            return true;
        }
        return false;
    }

    private Node Insert(Node node, T value)
    {
        //base case
        if (node == null) return new Node(value);
        int cmp = value.CompareTo(node.value);
        if (cmp < 0)
        {
            node.left = Insert(node.left, value);
        }
        else
        {
            node.right = Insert(node.right, value);
        }

        Update(node);

        return Balance(node);
    }
    private void Update(Node node)
    {
        int leftNodeHeight = node.left == null ? -1 : node.left.height;
        int rightNodeHeigh = node.right == null ? -1 : node.right.height;

        node.height = 1 + Math.Max(leftNodeHeight, rightNodeHeigh);
        //update balance factor
        node.bf = rightNodeHeigh - leftNodeHeight;
    }

    private Node Balance(Node node)
    {
        //left heavy
        if (node.bf == -2)
        {
            //left-left case
            if (node.left.bf <= 0)
            {
                return LeftLeftCase(node);
            }
            else
            {
                return LeftRightCase(node);
            }
        }
        else if (node.bf == +2)
        {
            if (node.right.bf >= 0)
            {
                return RightRightCase(node);
            }
            else
            {
                return RightLeftCase(node);
            }
        }

        return node;
    }

    private Node RightLeftCase(Node node)
    {
        node.right = RightRotation(node.right);
        return RightRightCase(node);
    }

    private Node RightRightCase(Node node)
    {
        return LeftRotation(node);
    }

    private Node LeftRightCase(Node node)
    {
        node.left = LeftRotation(node.left);
        return LeftLeftCase(node);
    }
    private Node LeftLeftCase(Node node)
    {
        return RightRotation(node);
    }

    private Node LeftRotation(Node node)
    {
        Node newParent = node.right;
        node.right = newParent.left;
        newParent.left = node;
        //always update the child first
        Update(node);
        Update(newParent);

        return newParent;
    }

    private Node RightRotation(Node node)
    {
        Node newParent = node.left;
        node.left = newParent.right;
        newParent.right = node;
        //always update the child first
        Update(node);
        Update(newParent);

        return newParent;
    }

    public bool Remove(T elem)
    {
        if (elem == null) return false;

        if (Contains(root, elem))
        {
            root = Remove(root, elem);
            nodeCount--;
            return true;
        }

        return false;
    }

    private Node Remove(Node node, T elem)
    {
        if (node == null) return null;

        int cmp = elem.CompareTo(node.value);
        if (cmp < 0)
        {
            node.left = Remove(node.left, elem);
        }
        else if (cmp > 0)
        {
            node.right = Remove(node.right, elem);
        }
        else
        {
            if (node.left == null)
            {
                return node.right;
            }
            else if (node.right == null)
            {
                return node.left;
            }
            else
            {

                if (node.left.height > node.right.height)
                {
                    // choose to remove from the left subtree

                    //swap the successor into the node
                    T successorValue = FindMax(node.left);
                    node.value = successorValue;

                    //remove the duplicate node
                    node.left = Remove(node.left, successorValue);
                }
                else
                {
                    T successorValue = FindMin(node.right);
                    node.value = successorValue;

                    //remove the duplicate node
                    node.right = Remove(node.right, successorValue);
                }
            }
        }

        Update(node);
        return Balance(node);
    }

    private T FindMin(Node node)
    {
        while (node.left != null)
        {
            node = node.left;
        }
        return node.value;
    }

    private T FindMax(Node node)
    {
        while (node.right != null)
        {
            node = node.right;
        }
        return node.value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Stack<Node> stack = new Stack<Node>();
        if (root == null) throw new ArgumentNullException("The tree is empty");
        Node current = root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.left;
            }

            current = stack.Pop();
            yield return current.value;
            current = current.right;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}