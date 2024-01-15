using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStrike : MonoBehaviour
{
    public GameObject objectToGenerate; // �n�ͦ�������

    public GameObject warningRangeObject;
    public int numberOfObjects = 5;     // �n�ͦ�������ƶq
    public float spawnRadius = 10f;     // �ͦ����d��b�|
    public float minDistance = 2f;     // ���餧�����̤p�Z��

    public List<Vector3> strikesTransform;

    public Vector2 centerPoint;
    void Start()
    {
        //GenerateObjects();
    }

    public IEnumerator GenerateObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector2 randomPos = centerPoint + Random.insideUnitCircle * spawnRadius;

            // �ˬd�P��L���骺�Z��
            while (IsTooCloseToOthers(randomPos))
            {
                randomPos = centerPoint + Random.insideUnitCircle * spawnRadius;
            }

            strikesTransform.Add(new Vector3(randomPos.x, 0f, randomPos.y));
            // ��Ҥƪ���
            Instantiate(warningRangeObject, new Vector3(randomPos.x, 0.3f, randomPos.y), Quaternion.identity);
        }

        yield return new WaitForSeconds(2f);
        foreach (var strikeTransform in strikesTransform)
        {
            Instantiate(objectToGenerate, strikeTransform, Quaternion.identity);
        }
        
    }

    bool IsTooCloseToOthers(Vector2 position)
    {
        // ���o�Ҧ��ͦ�������
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag(objectToGenerate.tag);

        // �ˬd�P��L���骺�Z��
        foreach (var obj in generatedObjects)
        {
            float distance = Vector2.Distance(position, obj.transform.position);
            if (distance < minDistance)
            {
                return true; // �P��L����ӱ���
            }
        }

        return false; // �P��L���騬����
    }

}
