using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointScript))]
public class WaypointEditorScript : Editor
{
    WaypointScript WaypointScript => target as WaypointScript;

    private void OnSceneGUI()
    {
        Handles.color = Color.grey;

        for(int i = 0; i < WaypointScript.Waypoints.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            // Draw handles
            Vector2 currentWaypoint = WaypointScript.CurrentPosition + WaypointScript.Waypoints[i];
            Vector2 newWaypoint = Handles.FreeMoveHandle(currentWaypoint, Quaternion.identity, 0.2f, new Vector2(0.25f, 0.25f), Handles.SphereHandleCap);

            // Draw Text Indicator
            GUIStyle textStyle = new();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 14;
            textStyle.normal.textColor = Color.black;
            Vector2 textAlignment = Vector2.down * 0.25f + Vector2.left * 0.25f;
            Handles.Label(WaypointScript.CurrentPosition + WaypointScript.Waypoints[i] + textAlignment, $"Waypoint: {i + 1}", textStyle);
            EditorGUI.EndChangeCheck();

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Waypoint Handle");
                WaypointScript.Waypoints[i] = newWaypoint - WaypointScript.CurrentPosition;
            }
        }
    }
}
