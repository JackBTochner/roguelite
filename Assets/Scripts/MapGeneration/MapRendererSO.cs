using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Map
{
    // [CreateAssetMenu(menuName = "Map/MapRenderer")]
    public class MapRendererSO : MonoBehaviour
    {
        public MapManagerSO mapManager;
        public MapPlayerTrackerSO mapPlayerTracker;

        public RectTransform mapHolderRect;

        [Tooltip(
            "List of all the MapConfig scriptable objects from the Assets folder that might be used to construct maps. " +
            "Similar to Acts in Slay The Spire (define general layout, types of bosses.)")]
        public List<MapConfigSO> allMapConfigs;
        public GameObject nodePrefab;
        [Tooltip("Offsets of the start/end nodes of the map from the edges of the rect")]
        public float horizontalMargin = 0.1f;
        public float verticalMargin = 0.1f;

        [Header("Background Settings")]
        [Tooltip("If the background sprite is null, background will not be shown")]
        public Sprite background;
        public Color32 backgroundColor = Color.white;
        public float xSize;
        public float yOffset;

        [Header("Line Settings")]
        public GameObject linePrefab;
        [Tooltip("Distance from the node till the line starting point")]
        public float offsetFromNodes = 0.5f;

        [Header("Colors")]
        [Tooltip("Node Visited or Attainable color")]
        public Color32 visitedColor = Color.white;
        [Tooltip("Locked node color")]
        public Color32 lockedColor = Color.gray;
        [Tooltip("Visited or available path color")]
        public Color32 lineVisitedColor = Color.white;
        [Tooltip("Unavailable path color")]
        public Color32 lineLockedColor = Color.gray;

        private List<List<Point>> paths;
        // ALL nodes:
        public readonly List<MapNode> MapNodes = new List<MapNode>();
        private readonly List<UILineConnector> lineConnections = new List<UILineConnector>();

        public static MapRendererSO Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void ClearMap()
        {
            MapNodes.Clear();
            lineConnections.Clear();
        }

        public void ShowMap(Map m)
        {
            if(mapHolderRect == null)
            {
                /*
                foreach(Transform child in gameObject.transform)
                {
                    if(child.CompareTag("MapHolder"))
                    {
                        mapHolderRect = child.GetComponent<RectTransform>();
                    }
                }*/
            }
            if (m == null)
            {
                Debug.LogWarning("Map was null in MapRenderer.ShowMap()");
                return;
            }

            ClearMap();
            
            CreateNodes(m.nodes);

            DrawLines();

            SetAttainableNodes();

            // SetLineColors();
            
        }

        private void CreateNodes(List<Node> nodes)
        {
            var nodesHeight = Mathf.Abs(nodes.Max(pos => pos.position.x)) + Mathf.Abs(nodes.Min(pos => pos.position.x));
            var offset = new Vector2(mapHolderRect.rect.width * horizontalMargin, mapHolderRect.rect.height * verticalMargin);
            var innerArea = new Vector2(mapHolderRect.rect.width - (offset.x * 2), mapHolderRect.rect.height - (offset.y * 2));
            var units = new Vector2(innerArea.x / mapManager.CurrentMap.DistanceBetweenFirstAndLastLayers(), innerArea.y / nodesHeight);
            Debug.Log(innerArea);
            
            foreach (var node in nodes)
            {
                var mapNode = CreateMapNode(node, innerArea, offset, units);
                MapNodes.Add(mapNode);
            }
        }
        
        private MapNode CreateMapNode(Node node, Vector2 innerArea, Vector2 offset, Vector2 units)
        {
            var mapNodeObject = Instantiate(nodePrefab, mapHolderRect.transform);
            var mapNode = mapNodeObject.GetComponent<MapNode>();
            var template = GetTemplate(node.templateName);
            mapNode.SetUp(node, template, mapManager, mapPlayerTracker);
            mapNode.transform.localPosition = new Vector2(
                offset.x + (units.x * node.point.y) - mapHolderRect.rect.width / 2,
                (units.y * node.point.x)- offset.y);
            return mapNode;
        }

        public void SetAttainableNodes()
        {
            // first set all the nodes as unattainable/locked:
            foreach (var node in MapNodes)
                node.SetState(NodeStates.Locked);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // we have not started traveling on this map yet, set entire first layer as attainable:
                foreach (var node in MapNodes.Where(n => n.Node.point.y == 0))
                    node.SetState(NodeStates.Attainable);
            }
            else
            {
                // we have already started moving on this map, first highlight the path as visited:
                foreach (var point in mapManager.CurrentMap.path)
                {
                    var mapNode = GetNode(point);
                    if (mapNode != null)
                        mapNode.SetState(NodeStates.Visited);
                }

                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                // set all the nodes that we can travel to as attainable:
                foreach (var point in currentNode.outgoing)
                {
                    var mapNode = GetNode(point);
                    if (mapNode != null)
                        mapNode.SetState(NodeStates.Attainable);
                }
            }
        }
        
        private void DrawLines()
        {
            foreach (var node in MapNodes)
            {
                foreach (var connection in node.Node.outgoing)
                    AddLineConnection(node, GetNode(connection));
            }
        }

        public void AddLineConnection(MapNode from, MapNode to)
        {
            var lineObject = Instantiate(linePrefab, mapHolderRect.transform);
            var lineRenderer = lineObject.GetComponent<UILineRenderer>();
            
            var points = new Vector2[2];
            points[0] = from.transform.localPosition;
            points[1] = to.transform.localPosition;
            lineRenderer.Points = points;
            lineRenderer.transform.SetSiblingIndex(0);
        }
        
        private MapNode GetNode(Point p)
        {
            return MapNodes.FirstOrDefault(n => n.Node.point.Equals(p));
        }

        private MapConfigSO GetConfig(string configName)
        {
            return allMapConfigs.FirstOrDefault(c => c.name == configName);
        }

        public NodeTemplateSO GetTemplate(NodeType type)
        {
            var config = GetConfig(mapManager.CurrentMap.configName);
            return config.nodeTemplates.FirstOrDefault(n => n.nodeType == type);
        }

        public NodeTemplateSO GetTemplate(string templateName)
        {
            var config = GetConfig(mapManager.CurrentMap.configName);
            if(config.nodeTemplates == null)
            {
                Debug.Log("cant find config.nodeTemplates");
            }
            return config.nodeTemplates.FirstOrDefault(n => n.name == templateName);
        }
    }
}