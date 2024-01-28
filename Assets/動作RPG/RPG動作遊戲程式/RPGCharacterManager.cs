using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGbearfall
{


    public class RPGCharacterManager : MonoBehaviour
    {
        public GameObject vrCamera;

        public RPGCharacter nowRPGCharacter;
        public RPGGameManager rPGGameManager;

        public List<GameObject> characters; // 所有可用的角色

        public List<PlayerInfo> playerInfos;
        

        private int currentCharacterIndex = 0; // 當前控制的角色索引
        private int lastCharacterIndex;

        void Start()
        {
            // 初始時設置第一個角色為玩家控制的角色
            //SwitchCharacter(currentCharacterIndex);
            rPGGameManager = GetComponent<RPGGameManager>();
        }

        void Update()
        {
            // 監聽玩家按鍵輸入，進行角色切換
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //SwitchCharacter(0);
                SwitchCharacterInfo(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && characters.Count > 0)
            {
                //SwitchCharacter(1);
                SwitchCharacterInfo(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && characters.Count > 0)
            {
                print("第三隻腳色");
                //SwitchCharacter(1);
                SwitchCharacterInfo(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && characters.Count > 0)
            {
                print("第四隻腳色");
                //SwitchCharacter(1);
                SwitchCharacterInfo(3);
            }
            // 可根據需要添加更多按鍵和相應的切換邏輯
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
            if (newIndex >= 0)
            {
                print("切換角色");


                lastCharacterIndex = currentCharacterIndex;
                // 切換到新的角色
                currentCharacterIndex = newIndex;
                nowRPGCharacter.playetInfo = playerInfos[currentCharacterIndex];
                nowRPGCharacter.SetPlayer();


            }
        }
    }
}