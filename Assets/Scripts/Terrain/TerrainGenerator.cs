using UnityEngine;

namespace Terrain {
    public class TerrainGenerator : MonoBehaviour {
        public Vector2Int gridSize;

        private void Start() {
            var vertices = new Vector3[gridSize.x * gridSize.y];

            var i = 0;
            for (var z = 0; z < gridSize.y; z++) {
                for (var x = 0; x < gridSize.x; x++) {
                    var y = Mathf.Round(Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 5f);
                    vertices[i] = new Vector3(x, y, z);
                    
                    i++;
                }
            }
            
            var triangles = new int[(gridSize.x - 1) * (gridSize.y - 1) * 6];

            var vert = 0;
            var tris = 0;
            for (var y = 0; y < gridSize.y - 1; y++) {
                for (var x = 0; x < gridSize.x - 1; x++) {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + 1;
                    triangles[tris + 2] = vert + gridSize.x;
                    
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + gridSize.x + 1;
                    triangles[tris + 5] = vert + gridSize.x;

                    vert++;
                    tris += 6;
                }
                vert++;
            }
            
            var mesh = new Mesh {
                vertices = vertices,
                triangles = triangles
            };
            
            mesh.RecalculateNormals();
            
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}