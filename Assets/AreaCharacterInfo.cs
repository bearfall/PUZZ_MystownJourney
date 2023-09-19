using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCharacterInfo : MonoBehaviour
{

    public List<GameObject> areaCharacters = new List<GameObject>();
    public Transform charactersParent;
    public int enemyCount;
    public bool areaSetDone = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = areaCharacters.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SetCharacterToChracters(List<GameObject> areaCharacters)
    {
        for (int i = 0; i < areaCharacters.Count; i++)
        {
            Vector3 position = new Vector3(areaCharacters[i].GetComponent<TestCharacter>().initPos_X, 0.36f, areaCharacters[i].GetComponent<TestCharacter>().initPos_Z);
            Instantiate(areaCharacters[i], position, Quaternion.identity, charactersParent);
            yield return new WaitForSeconds(0.2f);
        }
        areaSetDone = true;


    }
}
