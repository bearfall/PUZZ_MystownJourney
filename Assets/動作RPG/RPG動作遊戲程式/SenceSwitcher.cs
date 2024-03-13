using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPGbearfall
{
    public class SenceSwitcher : MonoBehaviour
    {
        public RPGGameManager rpgGameManager;
        public PlayableDirector playableDirector;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playableDirector.Play();
                rpgGameManager.currentArea = RPGGameManager.AreaType.Dialogue;
            }
        }
    }
}
