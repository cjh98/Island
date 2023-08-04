using UnityEngine;
using System.Collections.Generic;

public class QuadTreeNode
{
    public static List<QuadTreeNode> AllNodes = new List<QuadTreeNode>();

    const int MIN_NODE_SIZE = 500;

    public Box Bounds;

    QuadTreeNode SW;
    QuadTreeNode NW;
    QuadTreeNode NE;
    QuadTreeNode SE;

    public List<QuadTreeNode> Children = new List<QuadTreeNode>();

    public Vector2Int Center { get { return Bounds.GetCenter(); } }
    public int Size { get { return Bounds.GetSize(); } }

    public QuadTreeNode(Box bounds)
    {
        Bounds = bounds;
    }

    public void Insert(Vector2Int point)
    {
        if (Vector2Int.Distance(Bounds.GetCenter(), point) >= Bounds.GetSize() || Bounds.GetSize() < MIN_NODE_SIZE)
            return;

        // create children
        SW = new QuadTreeNode(new Box(Bounds.Min, new Vector2Int(Bounds.GetCenter().x, Bounds.GetCenter().y)));
        Children.Add(SW);
        AllNodes.Add(SW);

        NW = new QuadTreeNode(new Box(new Vector2Int(Bounds.Min.x, Bounds.GetCenter().y), new Vector2Int(Bounds.GetCenter().x, Bounds.Max.y)));
        Children.Add(NW);
        AllNodes.Add(NW);

        NE = new QuadTreeNode(new Box(new Vector2Int(Bounds.GetCenter().x, Bounds.GetCenter().y), new Vector2Int(Bounds.Max.x, Bounds.Max.y)));
        Children.Add(NE);
        AllNodes.Add(NE);

        SE = new QuadTreeNode(new Box(new Vector2Int(Bounds.GetCenter().x, Bounds.Min.y), new Vector2Int(Bounds.Max.x, Bounds.GetCenter().y)));
        Children.Add(SE);
        AllNodes.Add(SE);

        SW.Insert(point);
        NW.Insert(point);
        NE.Insert(point);
        SE.Insert(point);
    }
}

public struct Box
{
    public Vector2Int Min;
    public Vector2Int Max;

    public Box(Vector2Int min, Vector2Int max)
    {
        Min = min;
        Max = max;
    }

    public Vector2Int GetCenter()
    {
        int x = Min.x + Max.x;
        int y = Min.y + Max.y;

        return new Vector2Int(x / 2, y / 2);
    }

    public int GetSize()
    {
        return Max.x - Min.x;
    }

    public bool ContainsPoint(Vector2Int point)
    {
        return point.x > Min.x && point.x < Max.x && point.y > Min.y && point.y < Max.y;
    }
}


 