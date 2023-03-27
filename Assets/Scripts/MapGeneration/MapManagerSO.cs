using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


namespace Map
{
    public class MapManagerSO : MonoBehaviour
    {
        public MapConfigSO config;
        public MapRendererSO mapRenderer;
        private List<GameSceneSO> sceneGreylist = new List<GameSceneSO>();

        public Map CurrentMap { get; private set; }

/*
        void Awake()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }
*/

        public GameSceneSO GetValidScene(List<GameSceneSO> idealScenes)
        {
            foreach(var idealScene in idealScenes)
            {
                if(!sceneGreylist.Contains(idealScene))
                {
                    sceneGreylist.Add(idealScene);
                    return idealScene;
                }
            }
            return idealScenes[Random.Range(0, idealScenes.Count)];
        }

        public void GenerateNewMap()
        {
            var map = MapGeneratorSO.GetMap(config);
            CurrentMap = map;
            // Debug.Log(map.ToJson());
            mapRenderer.ShowMap(map);
        }
        
/*
        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Serialize});
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }*/

        /*
        public void OnSceneChanged(Scene current, Scene next)
        {
            mapRenderer.mapHolderRect = GameObject.FindGameObjectWithTag("MapHolder").GetComponent<RectTransform>();
            Debug.Log(mapRenderer);
            Debug.Log(mapRenderer.mapHolderRect);
        }
        */
    }
}
