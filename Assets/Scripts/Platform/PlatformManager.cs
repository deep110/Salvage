using UnityEngine;
using System.Collections.Generic;


namespace Platform {
    public class PlatformManager : MonoBehaviour {

        public GameObject platform;
        public float platformGap = 1.6f;
        public float initialPlatformPos = -2.0f;

        private ObjectPooler objectPooler;
        private int minPlatformIndex = -2;
        private int maxPlatformIndex;
        private int currentPlatformIndex;

        private Dictionary<int, GameObject> hashMap;

        void Start() {
            objectPooler = new ObjectPooler(platform, 8);
            hashMap = new Dictionary<int, GameObject>();
            initPlatforms();
        }

        private void initPlatforms() {
            for (int i = 0; i < 5; i++) {
                GeneratePlatform(true);
            }
        }

        public void GeneratePlatform(bool up) {
            if (up) { 
                currentPlatformIndex = maxPlatformIndex;
                addPlatform(currentPlatformIndex);
                maxPlatformIndex++;
            } else {
                currentPlatformIndex = minPlatformIndex;
                if (currentPlatformIndex >= 0) {   
                    addPlatform(currentPlatformIndex);
                    minPlatformIndex--;
                } else {
                    minPlatformIndex = -2;
                }
            }
            
        }

        public void RemovePlatform(bool up) {
            if (up) {
                maxPlatformIndex--;
                currentPlatformIndex = maxPlatformIndex;
            } else {
                minPlatformIndex++;
                currentPlatformIndex = minPlatformIndex;
            }
            if (hashMap.ContainsKey(currentPlatformIndex)) {
                hashMap[currentPlatformIndex].SetActive(false);
                hashMap.Remove(currentPlatformIndex);
            }
        }

        private void addPlatform(int platformIndex) {
            float currentPlatformPos = initialPlatformPos + platformIndex * platformGap;
            hashMap.Add(platformIndex, objectPooler.Spawn(new Vector3(0, currentPlatformPos, 5)));
        }

    }
}