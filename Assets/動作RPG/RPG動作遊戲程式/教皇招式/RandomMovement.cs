using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RandomMovement : MonoBehaviour
    {
        public Transform[] allPoints; // �Ҧ��i�઺�I

        public GameObject objectsToMovePrefeb;

        public List<GameObject> objectsToMove; // �n���ʪ�����

        public CameraShake cameraShake;
        public CharacterMusicEffect characterMusicEffect;
        public float waitTime = 5f; // �C�����d���ɶ�
        public float amount;
        public bool canMove = true;

        void Start()
        {
            
           // StartCoroutine(MoveObjects());
        }

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
                cameraShake.ShakeCamera(1.5f, 5f);
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
    }
}
