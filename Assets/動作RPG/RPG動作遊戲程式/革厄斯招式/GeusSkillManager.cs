using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GeusSkillManager : MonoBehaviour
{
    [Header("玩家位置")]
    public Transform playerTransform;
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


    public IEnumerator ShotPoision()
    {
        RotateEnemy();
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
        impactPosition = new Vector3(playerTransform.position.x,transform.position.y, playerTransform.position.z);
        transform.DOMove(impactPosition, 1f, false).SetEase(Ease.InCirc).OnComplete(()=> 
        {
            Instantiate(crackGroundObj, transform.position, crackGroundObj.transform.rotation);
        });
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
        clawPosition  = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.DOMove(clawPosition, 1f, false).SetEase(Ease.InCirc);
        yield return new WaitForSeconds(0.95f);
        Instantiate(clawObj, clawTransform1.position, clawTransform1.rotation);
        yield return new WaitForSeconds(0.1f);
        Instantiate(clawObj, clawTransform2.position, clawTransform2.rotation);
        
        
    }
    #endregion

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
