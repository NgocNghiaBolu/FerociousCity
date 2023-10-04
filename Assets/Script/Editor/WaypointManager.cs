using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class WaypointManager : EditorWindow
{
    [MenuItem("Waypoint/Waypoint Editor Tools")]
    public static void ShowWindow()//xuat hien o Menu Window
    {
        GetWindow<WaypointManager>("Waypoint Editor Tools");
    }

    public Transform waypointOrigin;
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointOrigin"));

        if(waypointOrigin == null)//nu chua co thi hien thong bao
        {
            EditorGUILayout.HelpBox("Please assign a Waypoint origin transform. ", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("Box");
            CreateButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    private void CreateButton()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypont();
        }
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if(GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }
            if(GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }
            if(GUILayout.Button("Add Branch Waypoint"))
            {
                CreateBranch();
            }
            if(GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    private void CreateWaypont()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointOrigin.childCount, typeof(Waypoint)); //tao ra cac waypoint khac theo thu tu "0"-->
        waypointObject.transform.SetParent(waypointOrigin, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        if (waypointOrigin.childCount >1)
        {
            waypoint.previousWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount - 2).GetComponent<Waypoint>(); 
            waypoint.previousWaypoint.nextWaypoint = waypoint; //neu waypoint tao ra nhieu hon 1, thi previousWP cua WP2 se la Wp1

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }

    private void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointOrigin.childCount, typeof(Waypoint)); //tao ra cac waypoint khac theo thu tu "0"-->
        waypointObject.transform.SetParent(waypointOrigin, false);

        Waypoint newWP = waypointObject.GetComponent<Waypoint>();
        Waypoint selectWP = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectWP.transform.position;
        waypointObject.transform.position = selectWP.transform.forward;

        if (selectWP.previousWaypoint)
        {
            newWP.previousWaypoint = selectWP.previousWaypoint;
            selectWP.previousWaypoint.nextWaypoint = newWP;
        }

        newWP.nextWaypoint = selectWP;
        selectWP.previousWaypoint = newWP;

        newWP.transform.SetSiblingIndex(selectWP.transform.GetSiblingIndex());
        Selection.activeGameObject = newWP.gameObject;
    }

    private void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointOrigin.childCount, typeof(Waypoint)); //tao ra cac waypoint khac theo thu tu "0"-->
        waypointObject.transform.SetParent(waypointOrigin, false);

        Waypoint newWP = waypointObject.GetComponent<Waypoint>();
        Waypoint selectWP = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectWP.transform.position;
        waypointObject.transform.position = selectWP.transform.forward;

        if(selectWP.nextWaypoint != null)
        {
            selectWP.nextWaypoint.previousWaypoint = newWP;
            newWP.nextWaypoint = selectWP.nextWaypoint;
        }
        selectWP.nextWaypoint = newWP;

        newWP.transform.SetSiblingIndex(selectWP.transform.GetSiblingIndex());
        Selection.activeGameObject = newWP.gameObject;
    }

    void CreateBranch()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointOrigin.childCount, typeof(Waypoint)); //tao ra cac waypoint khac theo thu tu "0"-->
        waypointObject.transform.SetParent(waypointOrigin, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        Waypoint branched = Selection.activeGameObject.GetComponent<Waypoint>();

        waypoint.transform.position = branched.transform.position;
        waypoint.transform.forward = branched.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }

    private void RemoveWaypoint()
    {
        Waypoint selectWP = Selection.activeGameObject.GetComponent<Waypoint>();
         
        if(selectWP.nextWaypoint != null)
        {
            selectWP.nextWaypoint.previousWaypoint = selectWP.previousWaypoint;
        }
        if(selectWP.previousWaypoint != null)
        {
            selectWP.previousWaypoint.nextWaypoint = selectWP.nextWaypoint;
            Selection.activeGameObject = selectWP.previousWaypoint.gameObject;

            DestroyImmediate(selectWP.gameObject);
        } 

    }
   
}
