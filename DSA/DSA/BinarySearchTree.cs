using System.Security.AccessControl;

class BinarySearchTree<T> where T : IComparable<T>
{
    //Tracks the number of nodes
    private int nodeCount = 0;
    //Root of the tree
    private Node root = null;
    private class Node
    {
        internal T data;
        internal Node left;
        internal Node right;

        public Node(Node left, Node right, T elem)
        {
            this.data = elem;
            this.left = left;
            this.right = right;
        }
    }

    public int Count()
    {
        return nodeCount;
    }

    public bool IsEmpty()
    {
        return Count() == 0;
    }

    public bool Add(T elem)
    {
        if (Contains(elem))
        {
            return false;
        }

        root = Add(root, elem);
        nodeCount++;
        return true;
    }


    //method to recursively add element to the tree
    private Node Add(Node node, T elem)
    {
        if (node == null)
        {
            node = new Node(null, null, elem);
        }
        else
        {
            if (elem.CompareTo(node.data) < 0)
            {
                node.left = Add(node.left, elem);
            }
            else
            {
                node.right = Add(node.right, elem);
            }
        }

        return node;
    }

    public bool Remove(T elem)
    {
        if (Contains(elem))
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

        int cmp = elem.CompareTo(node.data);

        // dig into the left subtree, the elem is smaller than the current one
        if (cmp < 0)
        {
            node.left = Remove(node.left, elem);
        }
        // dig into the right tree, the elem is bigger than the current one
        else if (cmp > 0)
        {
            node.right = Remove(node.right, elem);
        }
        // Found the node to remove
        else
        {
            //the node has only right subtree or none at all
            if (node.left == null)
            {
                Node rightChild = node.right;

                node.data = default;
                node = null;

                return rightChild;
            }
            //The node has only left subtree or none at all
            else if (node.right == null)
            {
                Node leftChild = node.left;

                node.data = default;
                node = null;

                return leftChild;
            }
            //the node has both a right and left subtree
            //in this case the successor of the node being removed
            //can be either the smallest value in the right subtree
            //or the largest value in the left subtree
            //we will fing the smallest value in the right subtree
            //by going to the leftmost side of the right subtree
            //of the current node
            else
            {
                Node tmp = DigLeft(node.right);

                //swap the data
                node.data = tmp.data;

                node.right = Remove(node.right, tmp.data);
            }
        }

        return node;
    }

    private Node DigLeft(Node node)
    {
        Node curr = node;
        while (curr.left != null)
        {
            curr = curr.left;
        }
        return curr;

    }

    public bool Contains(T elem)
    {
        return Contains(root, elem);
    }

    private bool Contains(Node node, T elem)
    {
        if (node == null) return false;

        int cmp = elem.CompareTo(node.data);

        if (cmp < 0)
        {
            return Contains(node.left, elem);
        }
        else if (cmp > 0)
        {
            return Contains(node.right, elem);
        }

        else
        {
            return true;
        }
    }

    public int Height()
    {
        return Height(root);
    }

    private int Height(Node node)
    {
        if (node == null) return 0;

        return Math.Max(Height(node.left), Height(node.right)) + 1;
    }
}
