using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject>[,] EnemyPosition { get; private set; }

    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private int NumberOfEnemy;
    [SerializeField]
    private float MinX;
    [SerializeField]
    private float MaxX;
    [SerializeField]
    private float MinZ;
    [SerializeField]
    private float MaxZ;

    void Awake()
    {
        EnemyPosition = new List<GameObject>[201, 201];
        for (int i = 0; i < 201; i++)
        {
            for (int j = 0; j < 201; j++)
            {
                EnemyPosition[i, j] = new List<GameObject>();
            }
        }

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector3 position = new Vector3(0f, 0f, 0f);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        for (int i = 0; i < NumberOfEnemy; i++)
        {
            position = SetRandomPosition();
            GameObject newObject = Instantiate(EnemyPrefab, position, rotation);
            SavePosition(position, newObject);
        }
    }

    // ������Ʈ ���� ��ġ �������� ����
    private Vector3 SetRandomPosition()
    {
        float positionX = Random.Range(MinX, MaxX);
        float positionZ = Random.Range(MinZ, MaxZ);

        return new Vector3(positionX, 0f, positionZ);
    }

    // �������� �迭�� ����
    private void SavePosition(Vector3 position, GameObject newObject)
    {
        Vector3Int newIndex = ChangeToIndex(position);
        EnemyPosition[newIndex.x, newIndex.z].Add(newObject);
    }

    // �������� ������ �������� ���� �迭�� ���� �� �ִ� ���·� ��ȯ
    private Vector3Int ChangeToIndex(Vector3 index)
    {
        // ���� �� �ִ� �� : -100 ~ 100
        int newIndexX = (int)index.x;
        if (newIndexX <= 0)
        {
            newIndexX *= -1;
            newIndexX = 100 - newIndexX;
        }
        else
        {
            newIndexX += 100;
        }

        int newIndexZ = (int)index.z;
        if (newIndexZ <= 0)
        {
            newIndexZ *= -1;
            newIndexZ = 100 - newIndexZ;
        }
        else
        {
            newIndexZ += 100;
        }

        return new Vector3Int(newIndexX, 0, newIndexZ);
    }
}