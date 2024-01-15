using RPGbearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
    public int hp;
    public int def;

    public UnityAction<int> OnAttackChange;
    public int originATK;
    public int Attack;
    

    public GameObject normalAttack; 
    public int normalAttackCD;
    public GameObject heavyAttack;
    public int heavyAttackCD;
    public AnimatorController animatorController;

    
}
