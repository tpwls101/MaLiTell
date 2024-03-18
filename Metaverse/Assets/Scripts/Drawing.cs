using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public GameObject linePrefab;
    public RawImage drawingCanvas;
    public List<Image> buttonImages;

    private List<Vector2> points = new List<Vector2>();
    private LineRenderer lineRenderer;
    private EdgeCollider2D collider2D;
    private Color pendingColor; // ������ ������ �����ϱ� ���� ����

    private void Start()
    {
        // �� ��ư�� ���� �Լ��� ����
        foreach (Image buttonImage in buttonImages)
        {
            Button button = buttonImage.GetComponent<Button>();
            button.onClick.AddListener(() => pendingColor = buttonImage.color); // Ŭ���� ��ư�� ������ pendingColor�� ����
        }
    }

    private void CreateNewLine(Vector2 position, float lineWidth)
    {
        GameObject go = Instantiate(linePrefab, drawingCanvas.transform);
        lineRenderer = go.GetComponent<LineRenderer>();
        collider2D = go.GetComponent<EdgeCollider2D>();

        points.Clear();
        points.Add(position);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, position);
        ApplyLineColor(pendingColor); // pendingColor�� ����� ������ ���ο� ���ο� ����

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    private void ApplyLineColor(Color color)
    {
        if (lineRenderer != null)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }

    private void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(drawingCanvas.rectTransform, Input.mousePosition, Camera.main))
        {
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingCanvas.rectTransform, Input.mousePosition, Camera.main, out localCursor);

            if (Input.GetMouseButtonDown(0))
            {
                float lineWidth = 0.1f; // �⺻ ���� ����
                int buttonIndex = buttonImages.FindIndex(image => image.color == pendingColor);
                if (buttonIndex == 8) // �ε��� 8���� ��
                {
                    lineWidth = 0.4f;
                }
                CreateNewLine(localCursor, lineWidth);
            }
            else if (Input.GetMouseButton(0))
            {
                points.Add(localCursor);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, localCursor);
                collider2D.points = points.ToArray();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
}
