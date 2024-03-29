using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using MoreLinq;

public class Pathfinder
{
    bool NodesAdded = false;
    public bool PathFollowed;
    public Node OriginNode;
    public Node TargetNode;
    public _Grid Grid;

    public Stack<Vector3> FindPath(Vector3 Origin, Vector3 Target)
    {
        Stack<Vector3> Path = new Stack<Vector3>();
        List<Node> NodesNotVisited = new List<Node>();
        List<Node> NodesVisited = new List<Node>();
        List<Node> NodesCalculated = new List<Node>();

        ResetNodes(Grid);

        Node CurrentNode = OriginNode;

        if (NodesAdded == false)
        {
            for (int i = 0; i < Grid.Nodes.Length; i++)
            {
                NodesNotVisited.Add(Grid.Nodes[i]);
            }
            NodesAdded = true;
        }

        if (CurrentNode == null || TargetNode == null)
        {
            return null;
        }

        while (CurrentNode.GridPosition != TargetNode.GridPosition)
        {

            if (CurrentNode == OriginNode)
            {
                CurrentNode.Gcost = 0;
                CurrentNode.Fcost = GetFCost(Grid, CurrentNode, TargetNode, OriginNode);
            }

            foreach (Node i in FindNeigbours(Grid, CurrentNode))
            {
                if (i.IsObstacle == true)
                {
                    i.Fcost = Mathf.Infinity;
                }
                else
                {
                    if (i.Gcost > GetGCost(i, OriginNode))
                    {
                        i.Gcost = GetGCost(i, OriginNode);
                        i.PreviousNode = CurrentNode;
                    }

                    i.Hcost = GetHCost(Grid, i, TargetNode);
                    i.Fcost = GetFCost(Grid, i, TargetNode, OriginNode);

                    if (!NodesCalculated.Contains(i) && !NodesVisited.Contains(i))
                    {
                        NodesCalculated.Add(i);
                    }
                }
            }

            NodesNotVisited.Remove(CurrentNode);
            NodesCalculated.Remove(CurrentNode);
            NodesVisited.Add(CurrentNode);

            CurrentNode = GetNextNode(NodesCalculated);
        }

        while (CurrentNode.GridPosition != OriginNode.GridPosition)
        {
            Path.Push(CurrentNode.WorldPosition);
            Debug.DrawLine(CurrentNode.WorldPosition, CurrentNode.PreviousNode.WorldPosition);
            CurrentNode = CurrentNode.PreviousNode;
        }

        foreach (Node n in NodesVisited)
        {
            n.Gcost = Mathf.Infinity;
            n.Fcost = Mathf.Infinity;
            n.Hcost = 0;
            n.PreviousNode = null;
            NodesNotVisited.Add(n);
        }
        NodesVisited.Clear();

        foreach (Node n in NodesCalculated)
        {
            n.Gcost = Mathf.Infinity;
            n.Fcost = Mathf.Infinity;
            n.Hcost = 0;
        }
        NodesCalculated.Clear();

        return Path;
    }

    public Node GetNextNode(List<Node> NodesCalculated)
    {
        Node CurrentLowest = null;

        CurrentLowest = NodesCalculated[0];

        for (int i = 0; i < NodesCalculated.Count; i++)
        {
            if (NodesCalculated[i].Fcost < CurrentLowest.Fcost)
            {
                CurrentLowest = NodesCalculated[i];
            }
        }

        return CurrentLowest;
    }

    public List<Node> FindNeigbours(_Grid Grid, Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = 0; y <= 1; y++)
            {
                neighbours.Add(GetNodeFromGridPosition(Grid, new Vector2(node.GridPosition.x + x, node.GridPosition.y + y)));
            }
        }

        neighbours.RemoveAll(x => x == null);
        neighbours.Remove(node.PreviousNode);

        foreach (Node i in neighbours)
        {
            if (i.PreviousNode == null)
            {
                i.PreviousNode = node;
            }
        }
        return neighbours;
    }

    public float GetHCost(_Grid Grid, Node node, Node TargetNode)
    {
        float D = Grid.CellSize;
        float D2 = Mathf.Sqrt(2 * (Grid.CellSize * Grid.CellSize));

        float dx = Mathf.Abs(node.GridPosition.x - TargetNode.GridPosition.x);
        float dy = Mathf.Abs(node.GridPosition.y - TargetNode.GridPosition.y);

        float Hcost = D * (dx + dy) + (D2 - (2 * D)) * Mathf.Min(dx, dy);

        return Hcost;
    }

    public float GetGCost(Node node, Node OriginNode)
    {
        float Gcost;


        if (node.GridPosition != OriginNode.GridPosition)
        {
            Gcost = Vector3.Distance(node.PreviousNode.WorldPosition, node.WorldPosition) + node.PreviousNode.Gcost;
        }
        else
            Gcost = 0;

        return Gcost;
    }

    public float GetFCost(_Grid Grid, Node node, Node TargetNode, Node OriginNode)
    {
        float Fcost;

        if (node.IsObstacle == false)
        {
            Fcost = GetHCost(Grid, node, TargetNode) + GetGCost(node, OriginNode);
        }
        else
            Fcost = node.Fcost;

        return Fcost;
    }

    public Node GetNodeFromWorldPosition(Vector3 Position, _Grid Grid)
    {
        Node node = null;

        //for (int i = 0; i < Grid.Nodes.Length; i++)
        //{
        //    Vector3 origin = Grid.Nodes[i].WorldPosition;
        //    if (Vector3.Distance(new Vector3(Position.x, 0, 0), new Vector3(origin.x, 0, 0)) < (Grid.CellSize / 2)
        //        && Vector3.Distance(new Vector3(0, 0, Position.z), new Vector3(0, 0, origin.z)) < (Grid.CellSize / 2))
        //    {
        //        node = Grid.Nodes[i];
        //    }
        //}
        Vector3 NodeWorldPos = new Vector3(Grid.NodeWorldPositionsx.MinBy(x => Math.Abs((float)x - Position.x)).First(), Grid.transform.position.y, Grid.NodeWorldPositionsz.MinBy(x => Math.Abs((float)x - Position.z)).First());

        node = Array.Find<Node>(Grid.Nodes, x => x.WorldPosition == NodeWorldPos);


        return node;
    }

    public Node GetNodeFromGridPosition(_Grid Grid, Vector2 GridPosition)
    {
        if (GridPosition.x >= 0 && GridPosition.x <= Grid.GridSize
            && GridPosition.y >= 0 && GridPosition.y <= Grid.GridSize)
        {
            int i = Mathf.RoundToInt(GridPosition.x + (GridPosition.y * Grid.GridSize));
            return Grid.Nodes[i];
        }
        else
            return null;
    }

    //public void CheckForObstacles(Node Origin, Node Node)
    //{
    //    RaycastHit hit;
    //
    //    if (Physics.Raycast(Origin.WorldPosition, (Node.WorldPosition - Origin.WorldPosition), out hit, Vector3.Distance(Origin.WorldPosition, Node.WorldPosition), LayerMask.NameToLayer("Obstacle")))
    //    {
    //        Node.IsObstacle = true;
    //    }
    //}

    public bool FollowPath(Stack<Vector3> Path, GameObject Object, float Speed)
    {
        Vector3 TargetPos = Vector3.zero;

        if (Path == null)
        {
            return true;
        }

        if (Path.Count == 0)
        {
            PathFollowed = true;
            return true;
        }
        else
            PathFollowed = false;

        if (Path.Count != 0)
        {
            TargetPos = Path.Peek();
        }

        if (Vector3.Distance(Object.transform.position, TargetPos) < 0.5 && Path.Count != 0)
        {
            TargetPos = Path.Pop();
        }

        Object.transform.LookAt(TargetPos);
        
        //Object.transform.position = Vector3.MoveTowards(Object.transform.position, TargetPos, Speed * Time.deltaTime);
        //Object.GetComponent<Rigidbody>().velocity += (TargetPos - Object.transform.position).normalized * (Speed * Time.deltaTime);
        //Object.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(Object.GetComponent<Rigidbody>().velocity, Speed);
        return false;
    }

    public void ResetNodes(_Grid Grid)
    {
        for (int i = 0; i < Grid.NodeCount; i++)
        {
            Grid.Nodes[i].IsObstacle = false;
            Grid.Nodes[i].Fcost = Mathf.Infinity;
            Grid.Nodes[i].Gcost = Mathf.Infinity;
            Grid.Nodes[i].Hcost = 0;
            Grid.Nodes[i].PreviousNode = null;
        }
    }
}
