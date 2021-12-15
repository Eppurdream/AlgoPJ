using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager instance;

    public Transform[] floorPanels;

    public int currentFloor = 0;

    private void Awake()
    {
        if (instance == null) instance = this;    
    }

    public void FirstOpen()
    {
        UIManager.instance.OpenPanel(floorPanels[0]);
    }

    public void UpFloor()
    {
        if(floorPanels.Length >= currentFloor + 1)
        {
            currentFloor++;
            UIManager.instance.OpenPanel(floorPanels[currentFloor]);
        }
    }

    public void DownFloor()
    {
        if (0 <= currentFloor - 1)
        {
            currentFloor--;
            UIManager.instance.OpenPanel(floorPanels[currentFloor]);
        }
    }

    public void ResetFloor()
    {
        currentFloor = 0;
    }
}
