using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGbearfall
{


    public class RandomMovement : MonoBehaviour
    {
        public Transform[] allPoints; // 所有可能的點

        public GameObject objectsToMovePrefeb;

        public List<GameObject> objectsToMove; // 要移動的物體

        public CameraShake cameraShake;
        public CharacterMusicEffect characterMusicEffect;
        public float waitTime = 5f; // 每次停留的時間
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
                print("生成");

                // 選擇四個不同的點
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

                // 生成物體
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

                // 等待一段時間
                yield return new WaitForSeconds(waitTime);

                // 選擇四個新的不同的點
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

                // 移動到新的點
                for (int i = 0; i < amount; i++)
                {
                    StartCoroutine(MoveTo(objectsToMove[i], selectedPoints[i].position));
                }

                // 等待一段時間
                yield return new WaitForSeconds(waitTime);
                cameraShake.ShakeCamera(1.5f, 5f);
                characterMusicEffect.PlayAttackSoundEffect();
                objectsToMove.Clear();

            }
        }

        IEnumerator MoveTo(GameObject obj, Vector3 targetPosition)
        {
            float elapsedTime = 0f;
            float moveTime = 1f; // 移動所需的時間

            Vector3 startingPosition = obj.transform.position;

            while (elapsedTime < moveTime)
            {
                obj.transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / moveTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            obj.transform.position = targetPosition; // 確保物體準確到達目標位置
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
