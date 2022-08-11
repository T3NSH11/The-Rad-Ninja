using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Grid : MonoBehaviour
{
    public float CellSize;
    public int GridSize;
    public GameObject SphereCollider;
    public GameObject SphereColliderContainer;
    public Pathfinder PathFinder;
    public Node[] Nodes;
    public float[] NodeWorldPositionsx;
    public float[] NodeWorldPositionsz;
    public int NodeCount;
    float lengthx;
    float lengthy;
    int RowCount = 0;
    int ColCount = 0;
    void Awake()
    {
        NodeCount = (int)(GridSize * GridSize);
        Nodes = new Node[NodeCount];
        NodeWorldPositionsx = new float[NodeCount];
        NodeWorldPositionsz = new float[NodeCount];

        SetNodes();
    }

    private void OnDrawGizmos()
    {
        lengthx = CellSize * GridSize;
        lengthy = CellSize * GridSize;
        float LineDisplacement = 0;
        Gizmos.color = new Color(0, 0, 1, 0.25f);
    
        for (float i = 0; i < GridSize + 1; i++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0, 0, LineDisplacement), new Vector3(lengthy, 0, transform.position.z + LineDisplacement));
            LineDisplacement += CellSize;
        }
    
        LineDisplacement = 0;
    
        for (float i = 0; i < GridSize + 1; i++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(LineDisplacement, 0, lengthx), new Vector3(transform.position.x + LineDisplacement, 0, transform.position.z));
            LineDisplacement += CellSize;
        }
    
        for (int i = 0; i < NodeCount; i++)
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
    
            if (Nodes[i].IsObstacle == true)
            {
                Gizmos.color = Color.red;
            }
    
            Gizmos.DrawSphere(Nodes[i].WorldPosition, ((CellSize * 2) + (2 * CellSize)) / 20);
        }
    }

    public void SetNodes()
    {
        for (int i = 0; i < NodeCount; i++)
        {
            Nodes[i] = new Node();
        }

        for (int i = 0; i < NodeCount;)
        {
            while (ColCount < GridSize)
            {
                Nodes[i].GridPosition = new Vector2((float)ColCount, (float)RowCount);
                Nodes[i].WorldPosition = new Vector3(transform.position.x + (CellSize / 2) + (CellSize * ColCount), transform.position.y, transform.position.z + (CellSize / 2) + (CellSize * RowCount));
                NodeWorldPositionsx[i] = Nodes[i].WorldPosition.x;
                NodeWorldPositionsz[i] = Nodes[i].WorldPosition.z;

                GameObject collider = Instantiate(SphereCollider, Nodes[i].WorldPosition, Quaternion.identity, SphereColliderContainer.transform);
                collider.GetComponent<SphereCollider>().radius = CellSize/2;
                collider.GetComponent<Collisiondetection>().node = Nodes[i];
                ColCount++;
                i++;
            }

            if (RowCount < GridSize)
            {
                RowCount++;
                ColCount = 0;
            }
        }

        ColCount = 0;
        RowCount = 0;
    }
}
