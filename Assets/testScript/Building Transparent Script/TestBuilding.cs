using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuilding : MonoBehaviour
{
    public Transform player;

    public TestCharactersManager testCharactersManager;
    public List<TestCharacter> characters = new List<TestCharacter>();
    public bool allNoBlock = true;

    RaycastHit HitInfo;
    List<GameObject> TransparentObjects = new List<GameObject>();


    private void Start()
    {
        testCharactersManager = GameObject.Find("Manager").GetComponent<TestCharactersManager>();
    }
    void Update()
    {
        if (allNoBlock)
        {
            ClearTransparentObjects();
        }
        SetTrans();

    }

    private void SetTrans()
    {
        characters = testCharactersManager.testCharacters;
        foreach (var character in characters)
        {
            if (character == null)
            {

                return;

            }


            Ray ray = new Ray(Camera.main.transform.position, character.gameObject.transform.position - Camera.main.transform.position);
            //Vector3 Direction = player.position - transform.position;//射线方向为摄像头指向人物

            int layerMask = LayerMask.GetMask("Building");
            RaycastHit hitInfo;
            RaycastHit[] hits;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("射线检测到的物体名为：" + hitInfo.transform.name);
                if (hitInfo.transform.tag != "Player")//若人物被遮挡了
                {
                    character.isBlock = true;
                    print("被擋住了");
                    hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);//只检测Default图层的物体，其它图层检测不到
                    //ClearTransparentObjects();
                    print(hits.Length);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        var hit = hits[i];


                        //首先先将透明物体全部恢复，不然会出现人物持续被遮挡时，多个物体全部变成透明的问题。也就是说，只能将挡在人物前面的首个物体透明。如果人物前面有多个物体遮挡，这句语句可以删掉或进行调整


                        if (hit.transform.GetComponent<MeshRenderer>())//若障碍物带有碰撞器组件
                        {
                            if (hit.transform.GetComponent<MeshRenderer>().material.GetFloat("_Alpha") != 0.2f)//若障碍物的透明度不为0.2
                            {
                                var ChangeColor = hit.transform.GetComponent<MeshRenderer>().material.GetFloat("_Alpha");
                                ChangeColor = 0.2f;

                                // SetMaterialRenderingMode(hit.transform.GetComponent<MeshRenderer>().material, RenderingMode.Transparent);

                                hit.transform.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", ChangeColor);
                                TransparentObjects.Add(hit.transform.gameObject);//将透明物体添加到数组中


                                Debug.Log("射线检测到的物体名为：" + hit.transform.name);

                            }
                        }



                    }
                }
                
                else //若人物没有被遮挡
                {

                    character.isBlock = false;

                }
                

            }

            /*
            else
            {
                
                if (isHave)
                {
                    ClearTransparentObjects();
                    print("退回材質");
                }
            }
            */
        }

    }

    public void Check()
    {
        characters = testCharactersManager.testCharacters;
        foreach (var character in characters)
        {
            if (character.isBlock)
            {
                allNoBlock = false;
                break;
            }
            allNoBlock = true;
                
        }
        
    }
    

    void ClearTransparentObjects()//将透明物体恢复不透明
    {
        if (TransparentObjects != null)
        {
            for (int i = 0; i < TransparentObjects.Count; i++)
            {
                var ChangeColor = TransparentObjects[i].transform.GetComponent<MeshRenderer>().material.GetFloat("_Alpha");
                ChangeColor = 1f;

                TransparentObjects[i].transform.GetComponent<MeshRenderer>().material.SetFloat("_Alpha", ChangeColor);
            }
            TransparentObjects.Clear();//清除数组
        }
    }
}
