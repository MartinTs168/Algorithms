public class Trie
{

    private class Node
    {
        public bool isEnd;
        public Dictionary<char, Node> children;

        public Node()
        {
            children = [];
            isEnd = false;
        }
    }

    private Node root;

    public Trie()
    {
        root = new Node();
    }

    public void Insert(string word)
    {
        Node cur = root;

        foreach (char c in word.ToCharArray())
        {
            if (!cur.children.ContainsKey(c))
            {
                cur.children.Add(c, new Node());
            }
            cur = cur.children[c];

        }

        cur.isEnd = true;
    }

    public bool Search(string word)
    {
        Node cur = root;
        foreach (char c in word.ToCharArray())
        {
            if (!cur.children.ContainsKey(c))
            {
                return false;
            }
            cur = cur.children[c];
        }

        return cur.isEnd;
    }

    public bool StartsWith(string prefix)
    {
        Node cur = root;
        foreach (char c in prefix.ToCharArray())
        {
            if (!cur.children.ContainsKey(c))
            {
                return false;
            }
            cur = cur.children[c];
        }

        return true;
    }
}

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */