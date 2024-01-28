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

        public List<GameObject> characters; // �Ҧ��i�Ϊ�����

        public List<PlayerInfo> playerInfos;
        

        private int currentCharacterIndex = 0; // ��e����������
        private int lastCharacterIndex;

        void Start()
        {
            // ��l�ɳ]�m�Ĥ@�Ө��⬰���a�������
            //SwitchCharacter(currentCharacterIndex);
            rPGGameManager = GetComponent<RPGGameManager>();
        }

        void Update()
        {
            // ��ť���a�����J�A�i�樤�����
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
                print("�ĤT���}��");
                //SwitchCharacter(1);
                SwitchCharacterInfo(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && characters.Count > 0)
            {
                print("�ĥ|���}��");
                //SwitchCharacter(1);
                SwitchCharacterInfo(3);
            }
            // �i�ھڻݭn�K�[��h����M�����������޿�
        }

        void SwitchCharacter(int newIndex)
        {
            // �ˬd���ެO�_����
            if (newIndex >= 0 && newIndex < characters.Count)
            {
                // ������e���⪺����
                //characters[currentCharacterIndex].transform.GetChild(0).GetComponent<Animator>().SetTrigger("outSide");
                characters[currentCharacterIndex].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().healthBar.SetActive(false);
                characters[currentCharacterIndex].GetComponent<RPGPlayerController>().enabled = false;
                characters[currentCharacterIndex].GetComponent<NavMeshAgent>().enabled = false;
                characters[currentCharacterIndex].transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                characters[currentCharacterIndex].transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

                characters[currentCharacterIndex].GetComponent<Collider>().enabled = false;


                lastCharacterIndex = currentCharacterIndex;
                // ������s������
                currentCharacterIndex = newIndex;

                // �}�ҷs���⪺����
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
                // �i�ھڻݭn�K�[��ı�ĪG���޿�
            }
        }


        void SwitchCharacterInfo(int newIndex)
        {
            // �ˬd���ެO�_����
            if (newIndex >= 0)
            {
                print("��������");


                lastCharacterIndex = currentCharacterIndex;
                // ������s������
                currentCharacterIndex = newIndex;
                nowRPGCharacter.playetInfo = playerInfos[currentCharacterIndex];
                nowRPGCharacter.SetPlayer();


            }
        }
    }
}