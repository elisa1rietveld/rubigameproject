using UnityEngine;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour
{
    public float viewAngle = 90f;  // Increased angle for a wider vision cone
    public float viewDistance = 3f; // Reduced distance for a shorter vision cone
    public int segments = 20;  // More segments for smoother vision


    private Mesh mesh;
    private GameObject player;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        UpdateMesh();

        player = GameObject.FindWithTag("Player");
    }

    void UpdateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices.Add(Vector3.zero); // Center of the cone

        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + (viewAngle / segments) * i;
            Vector3 point = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle)) * viewDistance;
            vertices.Add(point);
        }

        for (int i = 1; i < vertices.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = true; // Player is detected
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Detector>().isSeen = false; // Player left the vision cone
        }
    }
}
