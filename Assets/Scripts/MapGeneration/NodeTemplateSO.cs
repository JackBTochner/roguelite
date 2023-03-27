using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public enum NodeType
    {
        Start,
        MinorEnemy,
        EliteEnemy,
        RestSite,
        Store,
        Encounter,
        Boon,
        Boss
    }
}

namespace Map
{
    [CreateAssetMenu(menuName = "Map/NodeTemplate")]
    public class NodeTemplateSO : ScriptableObject
    {
        public Sprite sprite;

        public NodeType nodeType;

        public List<GameSceneSO> possibleScenes;
    }
}
