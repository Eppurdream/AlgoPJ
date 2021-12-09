using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClassroomEvent : MonoBehaviour, IEventObject
{
    public int row = 4; // ���� 
    public int col = 5; // ���� �̰� �� �򰥸� => row, col

    public float padding = 1.5f;

    public Transform tileParent;
    public GameObject tilePrefab;
    public Transform selectObj;
    public CinemachineVirtualCamera ClassroomVirCam;

    private GameObject[,] tiles;
    private GameObject[] goals;

    private Vector2 playerPos;
    private Vector2 ghostPos;

    private Color normalColor;
    private Color playerColor;
    private Color ghostColor;

    private bool isMyTurn = true;

    private int dir = 0; // 1, 2, 3, 4 == ��, ����, �Ʒ�, ������

    private void Awake()
    {
        tiles = new GameObject[col, row];
        goals = new GameObject[2];

        normalColor = Color.white;
        playerColor = Color.green;
        ghostColor = Color.red;
    }

    public List<Scenario> GetScenario()
    {
        Clear();
        Init();
        return null;
    }

    private void Init()
    {
        StopCoroutine(TurnCheck());
        playerPos = new Vector2(0, 3);

        int rand = Random.Range(0, 4);
        isMyTurn = true;

        PlayerManager.instance.playerObj.SetActive(false);

        switch (rand)
        {
            case 0:
                ghostPos = new Vector2(0, 2);
                break;
            case 1:
                ghostPos = new Vector2(2, 0);
                break;
            case 2:
                ghostPos = new Vector2(3, 2);
                break;
            case 3:
                ghostPos = new Vector2(2, 4);
                break;
            default:
                Debug.Log("�̰� Random�� �̻��ؿ�");
                break;
        }

        goals[0] = MakeTile(col - 1, row - 1, 0, padding, Color.blue);
        goals[1] = MakeTile(0, row - 1, 0, -padding, Color.blue);

        for (int i = 0; i < col; i++)
        {
            for(int n = 0; n < row; n++)
            {
                if (Equals(playerPos, new Vector2(n, i)))
                {
                    tiles[i, n] = MakeTile(i, n, 0, 0, playerColor);
                }else if (Equals(ghostPos, new Vector2(n, i)))
                {
                    tiles[i, n] = MakeTile(i, n, 0, 0, ghostColor);
                }else
                {
                    tiles[i, n] = MakeTile(i, n, 0, 0, normalColor);
                }
            }
        }

        StartCoroutine(TurnCheck());
    }

    private GameObject MakeTile(int i, int n, float xPadding, float yPadding, Color color)
    {
        GameObject g = Instantiate(tilePrefab, tileParent);
        g.transform.position = new Vector3(tileParent.position.x + n * padding + xPadding
                                                   , tileParent.position.y + i * padding + yPadding);
        g.GetComponent<SpriteRenderer>().color = color;
        g.SetActive(true);

        return g;
    }

    private void Clear() // ���� �ð��� �Ƴ��� ���ؼ� Destroy �Լ��� ���
    {
        Destroy(goals[0]);
        Destroy(goals[1]);

        for (int i = 0; i < col; i++)
        {
            for (int n = 0; n < row; n++)
            {
                Destroy(tiles[i, n]);
            }
        }
    }

    IEnumerator TurnCheck()
    {
        InputManager.instance.BindingKey();
        GetComponent<BoxCollider2D>().enabled = false;
        ClassroomVirCam.gameObject.SetActive(true);

        while (true)
        {
            if(!isMyTurn)
            {
                GhostMove();
                dir = 0;
                isMyTurn = true;
                continue;
            }

            if (playerPos == new Vector2(row - 1, col - 1) && InputManager.instance.W)
            {
                SetDir(1);
            }
            else if(playerPos == new Vector2(row - 1, 0) && InputManager.instance.S)
            {
                SetDir(3);
            }

            if (InputManager.instance.EventKeyDown && dir != 0)
            {
                if ((playerPos == new Vector2(row - 1, col - 1) && dir == 1) || (playerPos == new Vector2(row - 1, 0) && dir == 3))
                {
                    Success();
                    break;
                }

                PlayerMove(dir);

                dir = 0;
                isMyTurn = false;
                selectObj.gameObject.SetActive(false);
            }

            if(InputManager.instance.W && playerPos.y + 1 != col)
            {
                SetDir(1);
            }
            else if(InputManager.instance.A && playerPos.x - 1 != -1)
            {
                SetDir(2);
            }
            else if (InputManager.instance.S && playerPos.y - 1 != -1)
            {
                SetDir(3);
            }
            else if (InputManager.instance.D && playerPos.x + 1 != row)
            {
                SetDir(4);
            }

            if(playerPos == ghostPos)
            {
                Dead();
                break;
            }

            yield return null;
        }
    }

    private void SetDir(int dir) // �ڵ尡 �������� �������ֽŴٸ� �����ϰڽ��ϴ�
    {
        this.dir = dir;
        selectObj.gameObject.SetActive(true);

        switch (dir)
        {
            case 1:
                selectObj.position = tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)].transform.position + new Vector3(0, padding);
                break;
            case 2:
                selectObj.position = tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)].transform.position + new Vector3(-padding, 0);
                break;
            case 3:
                selectObj.position = tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)].transform.position + new Vector3(0, -padding);
                break;
            case 4:
                selectObj.position = tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)].transform.position + new Vector3(padding, 0);
                break;
            default:
                Debug.Log("�̻��� ���� ���Ծ��");
                break;
        }
    }

    private void PlayerMove(int dir)
    {
        tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)]
            .GetComponent<SpriteRenderer>().color = normalColor;

        switch (dir)
        {
            case 1:
                playerPos += new Vector2(0, 1);
                break;
            case 2:
                playerPos += new Vector2(-1, 0);
                break;
            case 3:
                playerPos += new Vector2(0, -1);
                break;
            case 4:
                playerPos += new Vector2(1, 0);
                break;
            default:
                Debug.Log("�̻��� ���� ���Ծ��");
                break;
        }

        tiles[Mathf.RoundToInt(playerPos.y), Mathf.RoundToInt(playerPos.x)]
            .GetComponent<SpriteRenderer>().color = playerColor;
    }

    private void GhostMove()
    {
        int dir = 0;

        //bool isSmart = Random.Range(0, 2) == 0 ? true : false;
        bool isSmart = true;


        tiles[Mathf.RoundToInt(ghostPos.y), Mathf.RoundToInt(ghostPos.x)]
            .GetComponent<SpriteRenderer>().color = normalColor;

        if (isSmart)
        {
            Vector2 result = playerPos - ghostPos;

            dir = SetGhostDir(result);
        }

        switch (dir)
        {
            case 1:
                ghostPos += new Vector2(1, 1);
                break;
            case 2:
                ghostPos += new Vector2(1, -1);
                break;
            case 3:
                ghostPos += new Vector2(-1, -1);
                break;
            case 4:
                ghostPos += new Vector2(-1, 1);
                break;
            default:
                Debug.Log("�̻��� ���� ���Ծ��");
                break;
        }

        tiles[Mathf.RoundToInt(ghostPos.y), Mathf.RoundToInt(ghostPos.x)]
                    .GetComponent<SpriteRenderer>().color = ghostColor;
    }

    public int SetGhostDir(Vector2 result)
    {
        bool changeValue = false;

        if (result.x >= 0 && result.y >= 0)
        {
            dir = 1;

            if (ghostPos.x + 1 == row)
            {
                result.x = -1;
                changeValue = true;
            }
            if (ghostPos.y + 1 == col)
            {
                result.y = -1;
                changeValue = true;
            }
        }
        if (result.x >= 0 && result.y <= 0)
        {
            dir = 2;

            if (ghostPos.x + 1 == row)
            {
                result.x = -1;
                changeValue = true;
            }
            if (ghostPos.y - 1 == -1)
            {
                result.y = 1;
                changeValue = true;
            }
        }
        if (result.x <= 0 && result.y <= 0)
        {
            dir = 3;

            if (ghostPos.x - 1 == -1)
            {
                result.x = 1;
                changeValue = true;
            }
            if (ghostPos.y - 1 == -1)
            {
                result.y = 1;
                changeValue = true;
            }
        }
        if (result.x <= 0 && result.y >= 0)
        {
            dir = 4;

            if (ghostPos.x - 1 == -1)
            {
                result.x = 1;
                changeValue = true;
            }
            if (ghostPos.y + 1 == col)
            {
                result.y = -1;
                changeValue = true;
            }
        }

        if (changeValue)
        {
            dir = SetGhostDir(result);
        }

        return dir;
    }

    public void Success()
    {
        Clear();
        dir = 0;
        selectObj.gameObject.SetActive(false);

        InputManager.instance.DeBindingKey();
        GetComponent<BoxCollider2D>().enabled = true;
        PlayerManager.instance.playerObj.SetActive(true);

        ClassroomVirCam.gameObject.SetActive(false);
    }

    public void Dead()
    {
        Clear();
        dir = 0;
        selectObj.gameObject.SetActive(false);
        ClassroomVirCam.gameObject.SetActive(false);
        SceneMoveManager.instance.SceneMove("TitleScene");
    }
}
