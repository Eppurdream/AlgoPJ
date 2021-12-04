using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform playerEventPoint;
    public float moveSpeed;
    
    private Rigidbody2D rigid;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        rigid.velocity = new Vector2(InputManager.instance.horizontal,
                                     InputManager.instance.vertical) * moveSpeed;

        if(InputManager.instance.isMoving)
        {
            playerEventPoint.position = transform.position + new Vector3(InputManager.instance.horizontal,
                                                                         InputManager.instance.vertical);
        }
    }
}
