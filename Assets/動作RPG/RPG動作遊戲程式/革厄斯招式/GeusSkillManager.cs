using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GeusSkillManager : MonoBehaviour
{
    [Header("���a��m")]
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region �r�G
    [Header("�r�G�o�g�I")]
    public Transform poisionTransform;
    [Header("�r�G����")]
    public GameObject poisionObj;
    [Header("�r�G�g������")]
    public int poisionAmount;
    [Header("�r�G�g�����j")]
    public float poisionWaitTime;


    public IEnumerator ShotPoision()
    {
        for (int i = 0; i < poisionAmount; i++)
        {
            Instantiate(poisionObj, poisionTransform.position, poisionObj.transform.rotation);
            yield return new WaitForSeconds(poisionWaitTime);
        }
        
    }
    #endregion
    #region ���a�ļ�
    [Header("�ĸ˥ؼЦ�m")]
    public Vector3 impactPosition;
    [Header("���a����")]
    public GameObject crackGroundObj;

    public IEnumerator EarthShatter()
    {
        impactPosition = new Vector3(playerTransform.position.x,transform.position.y, playerTransform.position.z);
        transform.DOMove(impactPosition, 1f, false).SetEase(Ease.InCirc).OnComplete(()=> 
        {
            Instantiate(crackGroundObj, transform.position, crackGroundObj.transform.rotation);
        });
        yield return null;
    }


    #endregion
}
