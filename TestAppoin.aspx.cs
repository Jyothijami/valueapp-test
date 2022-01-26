// C# program to print all colliding appointments in a given set of appointments
using System;
public class GfG
{

    // Structure to represent an interval
    public class Interval
    {
        public int low, high;
    }
    public class ITNode
    {

        // 'i' could also be a normal variable
        public Interval i;
        public int max;
        public ITNode left, right;
    }

    // A utility function to create a new node
    static Interval newNode(int l, int h)
    {
        Interval temp = new Interval();
        temp.low = l;
        temp.high = h;
        return temp;
    }

    // A utility function to create a new node
    static ITNode newNode(Interval i)
    {
        ITNode temp = new ITNode();
        temp.i = i;
        temp.max = i.high;
        temp.left = temp.right = null;
        return temp;
    }

    // A utility function to insert a new Interval Search Tree Node. This is similar to BST Insert. 
    //Here the low value of interval is used to maintain BST property
    static ITNode insert(ITNode root, Interval i)
    {

        // Base case: Tree is empty,
        // new node becomes root
        if (root == null)
            return newNode(i);

        // Get low value of interval at root
        int l = root.i.low;

        // If root's low value is smaller,
        // then new interval goes to left subtree
        if (i.low < l)
            root.left = insert(root.left, i);

        // Else, new node goes to right subtree.
        else
            root.right = insert(root.right, i);

        // Update the max value of this ancestor if needed
        if (root.max < i.high)
            root.max = i.high;
        return root;
    }
     
    // A utility function to check if given two intervals overlap
    static bool doOVerlap(Interval i1, Interval i2)
    {
        if (i1.low < i2.high && i2.low < i1.high)
            return true;
        return false;
    }

    // The main function that searches a given interval i in a given Interval Tree.
    static Interval overlapSearch(ITNode root, Interval i)
    {

        // Base Case, tree is empty
        if (root == null)
            return null;

        // If given interval overlaps with root
        if (doOVerlap(root.i, i))
            return root.i;

        // If left child of root is present and max of left child is greater than or equal to given interval,
        // then i may overlap with an interval is left subtree
        if (root.left != null &&
            root.left.max >= i.low)
            return overlapSearch(root.left, i);

        // Else interval can only
        // overlap with right subtree
        return overlapSearch(root.right, i);
    }

    // This function prints all colliding appointments in a given array of apointments.
    static void printcolliding(Interval[] appt, int n)
    {

        // Create an empty Interval Search  Tree, add first appointment
        ITNode root = null;
        root = insert(root, appt[0]);

        // Process rest of the intervals
        for (int i = 1; i < n; i++)
        {

            // If current appointment conflicts with any of the existing intervals, print it
            Interval res = overlapSearch(root, appt[i]);

            if (res != null)
                Console.Write("[" + appt[i].low +
                                "," + appt[i].high +
                                "] Conflicts with [" +
                                res.low + "," +
                                res.high + "]\n");

            // Insert this appointment
            root = insert(root, appt[i]);
        }
    }

    // Driver code
    public static void Main(String[] args)
    {
        Interval[] appt = new Interval[6];
        appt[0] = newNode(1, 5);
        appt[1] = newNode(3, 7);
        appt[2] = newNode(2, 6);
        appt[3] = newNode(10, 15);
        appt[4] = newNode(5, 6);
        appt[5] = newNode(4, 100);
        int n = appt.Length;
        Console.Write(
            "Following are colliding intervals\n");
        printcolliding(appt, n);
    }
}


