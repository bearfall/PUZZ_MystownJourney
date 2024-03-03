using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizlowSkillManager : MonoBehaviour
{
    [Header("�����i�ޯ�")]
    [Header("�����i�w�s��")]
    public GameObject shockWavePrefab; // �ݭn�ͦ�������
    [Header("�����i�ͦ��I")]
    public List<Transform> shockWaveSpawnPoints;
    [Header("�����i�ƶq")]
    public int shockWaveAmount;
    [Header("�����i��m")]
    public Transform wizlowShockWavePosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region �����i
    public void ShockWave()
    {
        gameObject.transform.position = wizlowShockWavePosition.position;
        List<Transform> selectedPoints = new List<Transform>();
        List<int> selectedIndices = new List<int>();

        while (selectedPoints.Count < shockWaveAmount)
        {
            int randomIndex = Random.Range(0, shockWaveSpawnPoints.Count);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedPoints.Add(shockWaveSpawnPoints[randomIndex]);
            }
        }

        for (int i = 0; i < shockWaveAmount; i++)
        {

            var temp = Instantiate(shockWavePrefab, selectedPoints[i]);
            //temp.transform.position = selectedPoints[i].position;
            //DetachAndPreserveRotation(temp);

        }
    }
    #endregion

    #region ����
    public GameObject rockSkillPrefab; // �ޯ�ͦ�������
    public Transform playerPosition;
    public int maxrockCount = 20; // �ޯ�I�񪺳̤j����
    public float betweenRockTime = 0.5f; // �C���I��ޯ઺���j
    private int currentRockCount = 0; // �ثe�I�񪺧ޯস��

    public void StartRockSkill()
    {
        StartCoroutine(RockSkill());
    }

    public IEnumerator RockSkill()
    {
        while (currentRockCount < maxrockCount)
        {
            // �b���a��m�ͦ��ޯફ��
            Instantiate(rockSkillPrefab, playerPosition.position, Quaternion.identity);
            currentRockCount++;

            // ���ݤ@�q�ɶ��A�I��U�@�ӧޯ�
            yield return new WaitForSeconds(betweenRockTime);
        }
        currentRockCount = 0;
    }
    #endregion

    void DetachAndPreserveRotation(GameObject child)
    {
        // ���o�l���󪺥@�ɪŶ��y�ЩM����
        Vector3 worldPosition = child.transform.position;
        Quaternion worldRotation = child.transform.rotation;

        // �Ѱ��l����P�����󪺤��l���Y
        child.transform.SetParent(null);

        // �N�l����m��@�ɪŶ���
        child.transform.position = worldPosition;
        child.transform.rotation = worldRotation;
    }
}
