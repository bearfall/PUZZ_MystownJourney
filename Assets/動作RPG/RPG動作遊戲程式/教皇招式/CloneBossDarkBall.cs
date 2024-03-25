using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBossDarkBall : MonoBehaviour
{
    [Header("要移動到的地點")]
    public Vector3 TargetTransform;
    [Header("教皇複製體")]
    public GameObject BossCloneObj;

    public BossSkillManager bossSkillManager;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( MoveAndCloneBoss());
        bossSkillManager = GameObject.Find("教皇").GetComponent<BossSkillManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveAndCloneBoss()
    {
        transform.DOMove(TargetTransform, 0.5f, false).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.5f);
        GameObject obj =  Instantiate(BossCloneObj, transform.position, BossCloneObj.transform.rotation);
        
    }


    
}
