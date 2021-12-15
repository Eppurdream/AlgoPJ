using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonClickMove : MonoBehaviour
{
    public Transform point;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager.instance.AllClosePanel();
            FloorManager.instance.ResetFloor();
            PlayerManager.instance.playerObj.transform.position = point.position;
        });
    }
}
