using System.Collections.Generic;
using Malee.List;
using Utilities;
using UnityEngine;

// Modified version of https://github.com/silverua/slay-the-spire-map-in-unity
// MIT Licence
namespace Map
{
    [CreateAssetMenu(menuName ="Map/MapConfig")]
    public class MapConfigSO : ScriptableObject
    {
        public List<NodeTemplateSO> nodeTemplates;
        public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);

        public IntMinMax numOfPreBossNodes;
        public IntMinMax numOfStartingNodes;

        [Tooltip("Increase this number to generate more paths")]
        public int extraPaths;
        [Reorderable]
        public ListOfMapLayers layers;

        [System.Serializable]
        public class ListOfMapLayers : ReorderableArray<MapLayer>
        {
        }
    }
}