using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LEGACY_UILineRenderer : Graphic
{
    public List<Vector2> points;

    public float thickness = 10f;

    public Vector2Int gridSize;

    float width;

    float height;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        if (points.Count < 2)
        {
            return;
        }
        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = new Vector2(points[i].x - (width / 2), points[i].y - (height / 2));
            DrawVerticesForPoint (point, vh);
        }
        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    void DrawVerticesForPoint(Vector2 point, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(-thickness / 2, 0);
        vertex.position +=
            new Vector3(point.x, point.y);
        vh.AddVert (vertex);

        vertex.position = new Vector3(thickness / 2, 0);
        vertex.position +=
            new Vector3(point.x, point.y);
        vh.AddVert (vertex);
    }
}
