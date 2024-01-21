using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillManager : MonoBehaviour
{

    [Header("�l�ܼu�ޯ�")]
    [Header("�l�ܼu�w�s��")]
    public GameObject prefab; // �ݭn�ͦ�������
    [Header("�l�ܼu�w�s���ƶq")]
    public int numberOfObjects = 6; // ���󪺼ƶq
    [Header("�l�ܼu��骺�b�|")]
    public float radius = 5f; // ��骺�b�|
    [Header("�l�ܼu��餤�߮y��")]
    public Vector3 center;// ��骺���ߦ�m�y��


    [Header("�H�������z���ޯ�")]
    [Header("�H�������z���Ҧ��i�઺�I")]
    public Transform[] allPoints; // �Ҧ��i�઺�I
    [Header("�H�������z���w�s��")]
    public GameObject objectsToMovePrefeb;
    [Header("�H�������z���n���ʪ�����(�L���]�w)")]
    public List<GameObject> objectsToMove; // �n���ʪ�����
    [Header("�H�������z������")]
    public CharacterMusicEffect characterMusicEffect;
    [Header("�H�������z���C�����d���ɶ�")]
    public float waitTime = 5f; // �C�����d���ɶ�
    [Header("�H�������z������ƶq")]
    public float amount;
    public bool canMove = true;


    [Header("�¬}�ޯ�")]
    [Header("�¬}��m")]
    public Transform blackHoleTransform;
    [Header("�¬}�w�m��")]
    public GameObject blackHolePrefeb;


    [Header("�{�q�ޯ�")]
    [Header("�{�q�ޯ�w�s��")]
    public GameObject objectToGenerate; // �n�ͦ�������
    [Header("�{�q��m(�L���]�w)")]
    public List<Vector3> strikesTransform;
    [Header("�{�q����")]
    public Vector2 centerPoint;
    [Header("�{�q�ޯ�ƶq")]
    public int numberOfStrikes = 5;     // �n�ͦ�������ƶq
    [Header("�{�q�ޯ�d��b�|")]
    public float spawnRadius = 10f;     // �ͦ����d��b�|
    [Header("�{�q�ޯध�����̤p�Z��")]
    public float minDistance = 2f;     // ���餧�����̤p�Z��
    [Header("�{�q�ޯ��`����ɶ�")]
    public int strikesTime;
    [Header("�{�q�`����")]
    public int strikesAmount = 5;
    [Header("�{�q���j�ɶ�")]
    public float waitStrikesTime = 4;
    [Header("����ĵ�i�d��w�s��")]
    public GameObject warningRangeObject;
    [Header("directionalLight")]
    public Light directionalLight;
    [Header("audioSource")]
    public AudioSource audioSource;
    [Header("�U�B�w�s��")]
    public GameObject rainGameObject;
    [Header("pointLight")]
    public GameObject pointLight;
    [Header("���O")]
    public bool start = false;



    public CircleSpawner circleSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (start)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.0f, Time.deltaTime);


        }
        else
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1.5f, Time.deltaTime);
        }
    }
    
    public void TrackBallSkill()
    {
        circleSpawner.SpawnObjects();

    }
    
    /*
    public void RandomMovementBallSkill()
    {
        randomMovement.StartMove();
    }
    

    public void BlackHoleSkill()
    {
        Instantiate(blackHolePrefeb, blackHoleTransform);
    }
    

    public void RandomStrikeSkill()
    {
       StartCoroutine( randomStrike.GenerateObjects());
    }
    */

    #region �l�ܼu�ޯ�
    public void SpawnObjects()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius + center.x;
            float z = Mathf.Sin(angle) * radius + center.z;

            Vector3 spawnPosition = new Vector3(x, center.y, z);
            Quaternion spawnRotation = Quaternion.Euler(0f, angle * Mathf.Rad2Deg, 0f);


            print(spawnPosition);
            Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
    #endregion

    #region �H�������z���ޯ�
    public IEnumerator MoveObjects()
    {
        CanMove();
        while (canMove)
        {
            canMove = false;
            print("�ͦ�");

            // ��ܥ|�Ӥ��P���I
            List<Transform> selectedPoints = new List<Transform>();
            List<int> selectedIndices = new List<int>();

            while (selectedPoints.Count < amount)
            {
                int randomIndex = Random.Range(0, allPoints.Length);
                if (!selectedIndices.Contains(randomIndex))
                {
                    selectedIndices.Add(randomIndex);
                    selectedPoints.Add(allPoints[randomIndex]);
                }
            }

            // �ͦ�����
            for (int i = 0; i < amount; i++)
            {

                var temp = Instantiate(objectsToMovePrefeb, transform.position, transform.rotation);
                objectsToMove.Add(temp);
                /*
                System.Array.Resize(ref objectsToMove, objectsToMove.Length + 1);
                objectsToMove[objectsToMove.Length - 1] = temp;
                */
                objectsToMove[i].transform.position = selectedPoints[i].position;

            }

            // ���ݤ@�q�ɶ�
            yield return new WaitForSeconds(waitTime);

            // ��ܥ|�ӷs�����P���I
            selectedPoints.Clear();
            selectedIndices.Clear();

            while (selectedPoints.Count < amount)
            {
                int randomIndex = Random.Range(0, allPoints.Length);
                if (!selectedIndices.Contains(randomIndex))
                {
                    selectedIndices.Add(randomIndex);
                    selectedPoints.Add(allPoints[randomIndex]);
                }
            }

            // ���ʨ�s���I
            for (int i = 0; i < amount; i++)
            {
                StartCoroutine(MoveTo(objectsToMove[i], selectedPoints[i].position));
            }

            // ���ݤ@�q�ɶ�
            yield return new WaitForSeconds(waitTime);
            CameraShake.Shake(5f, 1.5f);
            characterMusicEffect.PlayAttackSoundEffect();
            objectsToMove.Clear();

        }
    }

    IEnumerator MoveTo(GameObject obj, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveTime = 1f; // ���ʩһݪ��ɶ�

        Vector3 startingPosition = obj.transform.position;

        while (elapsedTime < moveTime)
        {
            obj.transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition; // �T�O����ǽT��F�ؼЦ�m
    }

    public void StartMove()
    {
        StartCoroutine(MoveObjects());
    }


    public void CanMove()
    {
        canMove = true;
    }
    #endregion

    #region �{�q�ޯ�
    public IEnumerator GenerateObjects()
    {

        start = true;

        rainGameObject.SetActive(true);
        pointLight.SetActive(true);
        for (int j = 0; j < strikesAmount; j++)
        {

            yield return new WaitForSeconds(waitStrikesTime);

            for (int i = 0; i < numberOfStrikes; i++)
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
            audioSource.Play();
            strikesTransform.Clear();
        }

        yield return new WaitForSeconds(1f);

        rainGameObject.SetActive(false);
        pointLight.SetActive(false);
        start = false;

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
    #endregion
}




