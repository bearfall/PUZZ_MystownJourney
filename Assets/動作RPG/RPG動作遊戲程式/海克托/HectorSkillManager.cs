using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HectorSkillManager : MonoBehaviour
{
    public Transform player; // 玩家角色的Transform
    public Transform AttackSpawnTransform;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Header("橫砍")]
    [Header("橫砍物件")]
    public GameObject SlashObj1;
    [Header("橫砍物件2")]
    public GameObject SlashObj2;
    [Header("橫砍物件3")]
    public GameObject SlashObj3;


    public IEnumerator HorizontalSlash()
    {
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.7f);
        RotateEnemy();
        Instantiate(SlashObj1, AttackSpawnTransform);
        ani.SetTrigger("attack1");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);
    }

    public IEnumerator DoubleSlash()
    {
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.7f);
        RotateEnemy();
        Instantiate(SlashObj1, AttackSpawnTransform);
        ani.SetTrigger("attack1");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.1f);
        RotateEnemy();
        Vector3 temp = transform.position + transform.right * 0.5f;
        transform.DOMove(temp, 1f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(SlashObj2, AttackSpawnTransform);
        ani.SetTrigger("attack2");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);

    }
    public IEnumerator TripleSlash()
    {
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.7f);
        RotateEnemy();
        Instantiate(SlashObj1, AttackSpawnTransform);
        ani.SetTrigger("attack1");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);

        yield return new WaitForSeconds(0.3f);
        Vector3 temp = transform.position + transform.right * 0.5f;
        transform.DOMove(temp, 1f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(SlashObj2, AttackSpawnTransform);
        ani.SetTrigger("attack2");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);

        yield return new WaitForSeconds(0.3f);
        temp = transform.position + transform.right * 0.5f;
        transform.DOMove(temp, 1f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(SlashObj3, AttackSpawnTransform);
        ani.SetTrigger("attack3");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);

    }

    public IEnumerator RandomTripleSlash()
    {
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.7f);
        RotateEnemy();
        Instantiate(SlashObj1, AttackSpawnTransform);
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);
        ani.SetTrigger("attack1");
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.1f);
        RotateEnemy();
        Vector3 temp = transform.position + transform.right * 0.5f;
        transform.DOMove(temp, 1f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(SlashObj2, AttackSpawnTransform);
        ani.SetTrigger("attack2");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(0.1f);
        RotateEnemy();
        temp = transform.position + transform.right * 0.5f;
        transform.DOMove(temp, 1f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(SlashObj3, AttackSpawnTransform);
        ani.SetTrigger("attack3");
        CameraShake.instence.ShakeCamera(0.8f, 0.2f);

    }







    public float activationDistance = 3f; // 触发技能的距离阈值
    public float teleportRadius = 2f; // 瞬移到玩家周围的半径
    public int numberOfProjectiles = 1; // 要施放的物体数量
    public float WidthMax = 10f; // 矩形范围的宽度
    public float widthMin = 10f; // 矩形范围的高度
    public float HeightMax = 10f; // 矩形范围的宽度
    public float HeightMin = 10f; // 矩形范围的高度
    private Vector3 teleportDestination;
    public IEnumerator RushToPlayer()
    {
        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        // 如果敌人与玩家之间的距离小于阈值，则执行技能
        if (distanceToPlayer < activationDistance)
        {
            bool pointInRectangle = false;
            int attempts = 0;
            int maxAttempts = 10;

            while (!pointInRectangle && attempts < maxAttempts)
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized * teleportRadius;
                Vector3 tempteleport = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);
                //Vector3 teleportDestination = player.position + new Vector3(randomPoint.x, 0, randomPoint.y);

                if (tempteleport.x >= widthMin && tempteleport.x <= WidthMax &&
                    tempteleport.z >= HeightMin && tempteleport.z <= HeightMax)
                {
                    teleportDestination = tempteleport;
                    pointInRectangle = true;
                }
                attempts++;

            }
            if (pointInRectangle)
            {
                print(teleportDestination);
                transform.DOMove(teleportDestination, 0.5f, false);
                

            }
            else
            {
                print("瞬移失敗");
            }

        }
        else
        {
            print("瞬移失敗");
        }
        yield return new WaitForSeconds(0.5f);
    }



    public void RotateEnemy()
    {

        if (player != null && player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (player != null && player.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
