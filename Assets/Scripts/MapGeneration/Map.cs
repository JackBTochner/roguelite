using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

// Modified version of https://github.com/silverua/slay-the-spire-map-in-unity
// MIT Licence
namespace Map
{
    public class Map
    {
        public List<Node> nodes;

        public List<Point> path;

        public string endNodeName;

        public string configName;

        public List<float> layerDistances;

        public Map(
            string configName,
            string endNodeName,
            List<Node> nodes,
            List<Point> path,
            List<float> layerDistances
        )
        {
            this.nodes = nodes;
            this.path = path;
            this.endNodeName = endNodeName;
            this.configName = configName;
            this.layerDistances = layerDistances;
        }

        public Node GetEndNode()
        {
            return nodes
                .FirstOrDefault(n =>
                    n.nodeType == NodeType.Boss ||
                    n.nodeType == NodeType.Boon ||
                    n.nodeType == NodeType.Store);
        }


        public float DistanceBetweenFirstAndLastLayers()
        {
            var endNode = GetEndNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);
            if (endNode == null || firstLayerNode == null) return 0f;
            return endNode.position.y - firstLayerNode.position.y;
        }

        public Node GetNode(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

namespace Map
{
    [System.Serializable]
    public class MapLayer
    {
        [
            Tooltip(
                "Default node for this map layer. If Randomize Nodes is 0, you will get this node 100% of the time")
        ]
        public NodeType nodeType;

        // public IntMinMax numOfNodes;
        public FloatMinMax distanceFromPreviousLayer;

        [Tooltip("Distance between the nodes on this layer")]
        public float nodesApartDistance;

        [
            Tooltip(
                "If this is set to 0, nodes on this layer will appear in a straight line. Closer to 1f = more position randomization")
        ]
        [Range(0f, 1f)]
        public float randomizePosition;

        [
            Tooltip(
                "Chance to get a random node that is different from the default node on this layer")
        ]
        [Range(0f, 1f)]
        public float randomizeNodes;
    }
}
