using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyStateMachine))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyStateMachine fow = (EnemyStateMachine)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360f, fow.ViewDistance);

        Vector3 viewAngleA = fow.DirectionFromAngle(-fow.ViewAngle / 2, false);
        Vector3 viewAngleB = fow.DirectionFromAngle(fow.ViewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.ViewDistance);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.ViewDistance);


    }


}
