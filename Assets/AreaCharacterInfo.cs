using bearfall;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCharacterInfo : MonoBehaviour
{
    public List<GameObject> playerAreaCharacters = new List<GameObject>();
    public List<GameObject> enemyAreaCharacters = new List<GameObject>();

    public List<GameObject> deleteGameObjects = new List<GameObject>();

    public Transform charactersParent;

    public TestCharactersManager testCharactersManager;
    public TestGUIManager testGUIManager;

    public GameObject headCanva;

    public int enemyCount;

    public int playerCount = 0;

    public bool areaSetDone = false;
    // Start is called before the first frame update
    void Start()
    {
        testCharactersManager = GameObject.Find("Manager").GetComponent<TestCharactersManager>();
        testGUIManager = GameObject.Find("Manager").GetComponent<TestGUIManager>();
        enemyCount = enemyAreaCharacters.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SetCharacterToChracters(List<GameObject> playerAreaCharacters, List<GameObject> enemyAreaCharacters)
    {
        for (int i = 0; i < playerAreaCharacters.Count; i++)
        {
            Vector3 position = new Vector3(playerAreaCharacters[i].GetComponent<TestCharacter>().initPos_X, 0.36f, playerAreaCharacters[i].GetComponent<TestCharacter>().initPos_Z);
            Instantiate(playerAreaCharacters[i], position, Quaternion.identity, charactersParent);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < enemyAreaCharacters.Count; i++)
        {
            Vector3 position = new Vector3(enemyAreaCharacters[i].GetComponent<TestCharacter>().initPos_X, 0.36f, enemyAreaCharacters[i].GetComponent<TestCharacter>().initPos_Z);
            Instantiate(enemyAreaCharacters[i], position, Quaternion.identity, charactersParent);
            yield return new WaitForSeconds(0.2f);
        }


        testCharactersManager.reFreshCharactorList();

        foreach (var item in testCharactersManager.testCharacters)
        {
            if (!item.isEnemy)
            {
                playerCount++;
            }
        }

        testGUIManager.ShowHeadWindow();

        for (int i = 0; i < playerCount; i++)
        {
            headCanva.transform.GetChild(i).GetComponent<InfoByImage>().SetInfo(testCharactersManager.testCharacters[i].gameObject);
        }

        for (int i = 0; i < deleteGameObjects.Count; i++)
        {
            Destroy(deleteGameObjects[i]);
        }

        areaSetDone = true;


    }
}
