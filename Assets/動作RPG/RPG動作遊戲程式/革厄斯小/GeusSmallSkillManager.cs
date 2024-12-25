using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GeusSmallSkillManager : MonoBehaviour
{
    [Header("玩家位置")]
    public Transform playerTransform;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region 毒液
    [Header("毒液發射點")]
    public Transform poisionTransform;
    [Header("毒液物件")]
    public GameObject poisionObj;
    [Header("毒液射擊次數")]
    public int poisionAmount;
    [Header("毒液射擊間隔")]
    public float poisionWaitTime;
    [Header("毒液計時器")]
    public float poisionTime;
    public float poisionTimer;


    public IEnumerator ShotPoision()
    {
        RotateEnemy();

        ani.SetTrigger("poision");
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < poisionAmount; i++)
        {
            Instantiate(poisionObj, poisionTransform.position, poisionObj.transform.rotation);
            yield return new WaitForSeconds(poisionWaitTime);
        }

    }
    #endregion
    #region 裂地衝撞
    [Header("衝裝目標位置")]
    public Vector3 impactPosition;
    [Header("裂地物件")]
    public GameObject crackGroundObj;

    public IEnumerator EarthShatter()
    {
        RotateEnemy();
        StartCoroutine(RushToPlayer());
        ani.SetTrigger("earthShatter");
        yield return new WaitForSeconds(1f);
        Instantiate(crackGroundObj, transform.position, crackGroundObj.transform.rotation);

        yield return null;
    }


    #endregion
    #region 爪痕
    [Header("爪痕物件")]
    public GameObject clawObj;
    [Header("爪痕目標位置")]
    public Vector3 clawPosition;
    [Header("爪痕位置")]
    public Transform clawTransform1;
    [Header("爪痕位置2")]
    public Transform clawTransform2;


    public IEnumerator ClawAttack()
    {
        RotateEnemy();
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(1f);
        RotateEnemy();
        Instantiate(clawObj, clawTransform1.position, clawTransform1.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(clawObj, clawTransform2.position, clawTransform2.rotation);


        RotateEnemy();
        StartCoroutine(RushToPlayer());
        yield return new WaitForSeconds(1f);
        RotateEnemy();
        Instantiate(clawObj, clawTransform1.position, clawTransform1.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(clawObj, clawTransform2.position, clawTransform2.rotation);


    }
    #endregion
    #region 爆氣
    [Header("爆氣物件")]
    public GameObject madAirObj;
    [Header("爆氣位置")]
    public Transform madAirTransform;

    public void MadAir()
    {
        ani.SetTrigger("madAir");
        Instantiate(madAirObj, madAirTransform.position, madAirObj.transform.rotation);
    }

    #endregion

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
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // 如果敌人与玩家之间的距离小于阈值，则执行技能
        if (distanceToPlayer < activationDistance)
        {
            bool pointInRectangle = false;
            int attempts = 0;
            int maxAttempts = 10;

            while (!pointInRectangle && attempts < maxAttempts)
            {
                Vector2 randomPoint = Random.insideUnitCircle.normalized * teleportRadius;
                Vector3 tempteleport = playerTransform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
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
                teleportDestination = new Vector3(teleportDestination.x, 1.3f, teleportDestination.z);
                transform.DOMove(teleportDestination, 0.8f, false).SetEase(Ease.InCirc);
                yield return new WaitForSeconds(0);
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
    }

    public void RotateEnemy()
    {

        if (playerTransform != null && playerTransform.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (playerTransform != null && playerTransform.transform.position.x < gameObject.transform.position.x)
        {
            gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
