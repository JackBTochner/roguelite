using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Modified version of https://github.com/silverua/slay-the-spire-map-in-unity
// MIT Licence
namespace Map
{
    public class Node
    {
        public readonly Point point;

        public readonly List<Point> incoming = new List<Point>();

        public readonly List<Point> outgoing = new List<Point>();

        public readonly NodeType nodeType;

        public readonly string templateName;

        public Vector2 position;

        public Node(NodeType nodeType, string templateName, Point point)
        {
            this.nodeType = nodeType;
            this.templateName = templateName;
            this.point = point;
        }

        public void AddIncoming(Point p)
        {
            if (incoming.Any(element => element.Equals(p))) return;

            incoming.Add (p);
        }

        public void AddOutgoing(Point p)
        {
            if (outgoing.Any(element => element.Equals(p))) return;

            outgoing.Add (p);
        }

        public void RemoveIncoming(Point p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Point p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }

        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }
    }
}
