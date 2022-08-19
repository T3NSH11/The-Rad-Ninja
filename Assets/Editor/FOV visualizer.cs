using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EnemyFOV))]
public class FieldOfViewEditor : Editor
{

    void OnSceneGUI()
    {
        EnemyFOV Fov = (EnemyFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(Fov.transform.position, Vector3.up, Vector3.forward, 360, Fov.RangeRadius);
        Vector3 viewAngleA = Fov.DirectionFromAngle(-Fov.FOVAngle / 2, false);
        Vector3 viewAngleB = Fov.DirectionFromAngle(Fov.FOVAngle / 2, false);

        Handles.DrawLine(Fov.transform.position, Fov.transform.position + viewAngleA * Fov.RangeRadius);
        Handles.DrawLine(Fov.transform.position, Fov.transform.position + viewAngleB * Fov.RangeRadius);

        Handles.color = Color.red;

        if (Fov.PlayerDetected)
        {
            Handles.DrawLine(Fov.transform.position, Fov.PlayerTransform.position);
        }

    }

}
