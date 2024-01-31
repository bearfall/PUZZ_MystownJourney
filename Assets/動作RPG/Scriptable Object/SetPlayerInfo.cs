using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class SetPlayerInfo : MonoBehaviour
{
    public PlayerInfo playetInfo;
    // Start is called before the first frame update
    public string characterName;
    public SpriteRenderer characterSpriteRenderer;
    public int characterHp;
    public int characterDef;
    public int characterAtk;
    public GameObject characterNormalAttack;
    public int characterNormalAttackCD;
    public GameObject characterHeavyAttack;
    public int characterHeavyAttackCD;
    public RuntimeAnimatorController characterAnimator;

    private void Start()
    {
        SetPlayer();
    }

    public void SetPlayer()
    {
        characterName = playetInfo.Name;
        characterSpriteRenderer.sprite = playetInfo.image;
        characterHp = playetInfo.nowHP;
        characterDef = playetInfo.def;
        characterAtk = playetInfo.Attack;
        characterNormalAttack = playetInfo.normalAttack;
        characterNormalAttackCD = playetInfo.normalAttackCD;
        characterHeavyAttack = playetInfo.heavyAttack;
        characterHeavyAttackCD = playetInfo.heavyAttackCD;
        characterAnimator = playetInfo.animatorController;


    }
}
