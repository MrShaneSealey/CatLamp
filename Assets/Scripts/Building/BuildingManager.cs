using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    //objects
    public GameObject[] objects;
    public GameObject   pendingObject;

    //object Pos,Rot & Scale
    private Vector3 ObjPos;
    private Quaternion ObjRot;
    private Vector3 ObjScale;

    //RayCast
    [Range(0, 1000)] public int maxDistance;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    //Grid
    public float gridSize;
    private bool gridOn;
    [SerializeField] private Toggle gridToggle;

    void Update()
    {
        UpdateObject();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }
    private void FixedUpdate()
    {
        RaycastMouseLocation();
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], ObjPos, ObjRot);
    }

    public void PlaceObject()
    {
        pendingObject = null;

    }

    private void UpdateObject()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    GridCalucations(ObjPos.x),
                    GridCalucations(ObjPos.y),
                    GridCalucations(ObjPos.z)
                    );
            }
            else
            {
                pendingObject.transform.position = ObjPos;
                pendingObject.transform.rotation = ObjRot;
            }
        }
    }

    private void RaycastMouseLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //change input to the new input SYSTEM <<

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            ObjPos = hit.point;
        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else 
        {
            gridOn = false;
        }
    }

    private float GridCalucations(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
