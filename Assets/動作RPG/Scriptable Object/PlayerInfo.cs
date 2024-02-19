using RPGbearfall;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "New Character", menuName = "Character/New Character")]
public class PlayerInfo : ScriptableObject
{
    /*
    public delegate void OnAttackChangedDelegate(int newAttackValue);
    public event OnAttackChangedDelegate OnAttackChanged;
    */
    public string Name;
    public Sprite image;
    public int originMaxHP;
    public int maxHP;
    public int nowHP;
    public int originDef;
    public int def;

    public UnityAction<int> OnAttackChange;
    public int originATK;
    public int Attack;
    

    public GameObject normalAttack; 
    public float normalAttackCD;
    public GameObject heavyAttack;
    public int heavyAttackCD;
    public RuntimeAnimatorController animatorController;

    public bool isdie;

    
}
