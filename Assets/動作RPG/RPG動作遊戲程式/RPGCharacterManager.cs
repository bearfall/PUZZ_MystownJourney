using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace RPGbearfall
{


    public class RPGCharacterManager : MonoBehaviour
    {
        public static RPGCharacterManager instance;
        public GameObject vrCamera;

        public RPGCharacter nowRPGCharacter;
        public RPGGameManager rPGGameManager;

        public List<GameObject> characters; // 所有可用的角色

        public List<PlayerInfo> playerInfos;

        [Header("切換腳色相關")]
        public List<Image> Images;
        public bool canSwitch;
        public float switchTime = 5f; // 填充量减少的持续时间
        


        public int currentCharacterIndex = 0; // 當前控制的角色索引
        private int lastCharacterIndex;
        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            // 初始時設置第一個角色為玩家控制的角色
            //SwitchCharacter(currentCharacterIndex);
            rPGGameManager = GetComponent<RPGGameManager>();
        }

        void Update()
        {
            
            /*
            // 監聽玩家按鍵輸入，進行角色切換
            if (Input.GetKeyDown(KeyCode.Alpha1) && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                //SwitchCharacter(0);
                SwitchCharacterInfo(0);
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && characters.Count > 0 && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                //SwitchCharacter(1);
                SwitchCharacterInfo(1);
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && characters.Count > 0 && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                print("第三隻腳色");
                //SwitchCharacter(1);
                SwitchCharacterInfo(2);
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && characters.Count > 0 && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                print("第四隻腳色");
                //SwitchCharacter(1);
                SwitchCharacterInfo(3);
                
            }
            // 可根據需要添加更多按鍵和相應的切換邏輯
            */
        }

        public void ChangePlayer1(InputAction.CallbackContext ctx)
        {
            if (ctx.started && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                SwitchCharacterInfo(0);
            }
        }
        public void ChangePlayer2(InputAction.CallbackContext ctx)
        {
            if (ctx.started && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                SwitchCharacterInfo(1);
            }
        }
        public void ChangePlayer3(InputAction.CallbackContext ctx)
        {
            if (ctx.started && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                SwitchCharacterInfo(2);
            }

        }
        public void ChangePlayer4(InputAction.CallbackContext ctx)
        {
            if (ctx.started && !nowRPGCharacter.playetInfo.isdie && !nowRPGCharacter.isHeavyAttack && canSwitch)
            {
                SwitchCharacterInfo(3);
            }
        }

        void SwitchCharacter(int newIndex)
        {
            // 檢查索引是否有效
            if (newIndex >= 0 && newIndex < characters.Count)
            {
                // 關閉當前角色的控制
                //characters[currentCharacterIndex].transform.GetChild(0).GetComponent<Animator>().SetTrigger("outSide");
                characters[currentCharacterIndex].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().healthBar.SetActive(false);
                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().enabled = false;
                characters[currentCharacterIndex].GetComponent<NavMeshAgent>().enabled = false;
                characters[currentCharacterIndex].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                characters[currentCharacterIndex].transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

                characters[currentCharacterIndex].GetComponent<Collider>().enabled = false;


                lastCharacterIndex = currentCharacterIndex;
                // 切換到新的角色
                currentCharacterIndex = newIndex;

                // 開啟新角色的控制
                //nowRPGCharacter = characters[currentCharacterIndex].GetComponent<RPGCharacter>();
                characters[currentCharacterIndex].GetComponent<Collider>().enabled = true;
                characters[currentCharacterIndex].GetComponent<NavMeshAgent>().enabled = true;
                characters[currentCharacterIndex].gameObject.transform.position = characters[lastCharacterIndex].gameObject.transform.position;

                vrCamera.GetComponent<CinemachineVirtualCamera>().Follow = characters[currentCharacterIndex].transform;

                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().enabled = true;
                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().healthBar.SetActive(true);
                characters[currentCharacterIndex].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                characters[currentCharacterIndex].transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                //characters[currentCharacterIndex].transform.GetChild(0).GetComponent<Animator>().SetTrigger("inSide");
                rPGGameManager.nowRPGCharacter = characters[currentCharacterIndex].transform.GetChild(0).GetComponent<RPGCharacter>();
                // 可根據需要添加視覺效果的邏輯
            }
        }


        void SwitchCharacterInfo(int newIndex)
        {
            // 檢查索引是否有效
            if (newIndex >= 0 && !nowRPGCharacter.playerInfos[newIndex].isdie && newIndex != currentCharacterIndex)
            {
                print("切換角色");


                lastCharacterIndex = currentCharacterIndex;
                // 切換到新的角色
                currentCharacterIndex = newIndex;
                nowRPGCharacter.playetInfo = playerInfos[currentCharacterIndex];
                nowRPGCharacter.SetPlayer();
                StartCoroutine(DecreaseFill());


            }
        }

        public IEnumerator DecreaseFill()
        {
            canSwitch = false;
            // 使用DOVirtual.Float方法逐渐减少 fill Amount 的值
            // 参数分别是：起始值，目标值，持续时间，回调函数

            Sequence sequence = DOTween.Sequence();

            // 为每个 Image 添加填充动画到序列中
            foreach (Image image in Images)
            {
                image.fillAmount = 1;
                // 创建填充动画，将 fillAmount 从 1 动态变化到 0
                Tween tween = DOTween.To(() => image.fillAmount, x => image.fillAmount = x, 0f, switchTime);

                // 将动画添加到序列中，让它们同时进行
                sequence.Join(tween);
            }

            // 在所有动画完成后执行某些操作（这里是输出一段文字）
            sequence.OnComplete(() => canSwitch = true);
            yield return new WaitForSeconds(0.0f);
            /*
            foreach (var image in Images)
            {
                image.fillAmount = 1f;
                yield return DOTween.To(() => image.fillAmount, x => image.fillAmount = x, 0f, switchTime).WaitForCompletion();

                // 在减少 fill Amount 完成后，执行任何其他操作
                Debug.Log("Fill Amount decreased to 0");

                // 将 canSwitch 设置为 true，并将 fill Amount 设置回 1
                canSwitch = true;
                //image.fillAmount = 0f;
            }
            */
        }
        public void ReLoadGame()
        {
            foreach (var item in playerInfos)
            {
                item.maxHP = item.originMaxHP;
                item.nowHP = item.originMaxHP;
            }
        }
    }

}