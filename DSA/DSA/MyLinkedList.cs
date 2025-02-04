public class MyLinkedList
{

    private class Node
    {
        internal int value;
        internal Node next;

        public Node(int value)
        {
            this.value = value;
            next = null;
        }
    }

    private Node head;
    private Node tail;


    public void AddFirst(int item)
    {
        Node new_node = new Node(item);

        if (head == null)
        {
            head = tail = new_node;
        }
        else
        {
            new_node.next = head;
            head = new_node;
        }

    }

    public void AddLast(int item)
    {
        Node new_node = new Node(item);
        if (head == null)
        {
            head = tail = new_node;
        }
        else
        {
            tail.next = new_node;
            tail = new_node;
        }
    }

    public void DeleteFirst()
    {
        if (head != null)
        {
            Node new_head = head.next;
            head.next = null;
            head = new_head;

        }
    }

    public void DeleteLast()
    {
        if (head != null)
        {
            Node curr_node = head;
            while (curr_node.next != tail)
            {
                curr_node = curr_node.next;
            }

            curr_node.next = null;
            tail = curr_node;

        }
    }

    public bool Contains(int item)
    {
        Node curr_node = head;

        while (curr_node != null)
        {
            if (curr_node.value == item)
            {
                return true;
            }
            curr_node = curr_node.next;
        }

        return false;
    }

    public int IndexOf(int item)
    {
        Node curr_node = head;
        int index = 0;
        while (curr_node != null)
        {
            if (curr_node.value == item)
            {
                return index;
            }
            index++;
            curr_node = curr_node.next;
        }

        return -1;
    }

    public void Display()
    {
        Node curr_node = head;
        while (curr_node != null)
        {
            System.Console.WriteLine(curr_node.value);
            curr_node = curr_node.next;
        }
    }
}