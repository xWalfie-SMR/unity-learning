using System;
using System.Collections.Generic;
using UnityEngine;

namespace Terrain {
    public class Chunk : MonoBehaviour {
        [SerializeField] private int chunkWidth = 16;
        
        private const int WorldBottom = -64;
        private const int WorldTop = 319;
        private const int SeaLevel = 63;
        
        private static readonly int WorldHeight = WorldTop + Mathf.Abs(WorldBottom);

        private byte[,,] _blocks;

        private void Start() {
            _blocks = new byte[chunkWidth, WorldHeight, chunkWidth];
            Generate();
            BuildMesh();
        }
        
        private void Generate() {
            for (var x = 0; x < chunkWidth; x++) {
                for (var y = 0; y < WorldHeight; y++) {
                    for (var z = 0; z < chunkWidth; z++) {
                        var height = GetTerrainHeight(x, z);
                        
                        var worldY = y + WorldBottom;

                        if (worldY < height) {
                            // Solid terrain
                            _blocks[x, y, z] = GetBlockType(worldY, height);
                        } else if (worldY < SeaLevel) {
                            _blocks[x, y, z] = (byte)BlockType.Water;
                        } else { _blocks[x, y, z] = (byte)BlockType.Air; }
                    }
                }
            }
        }
        
        private void BuildMesh() {
            // Lists to hold mesh data
            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            
            // Center offsets
            var centerX = chunkWidth / 2f;
            var centerY = WorldHeight / 2f;
            var centerZ = chunkWidth / 2f;

            // Loop through each block
            for (var x = 0; x < chunkWidth; x++) {
                for (var y = 0; y < WorldHeight; y++) {
                    for (var z = 0; z < chunkWidth; z++) {
                        // Skip air (0)
                        if (_blocks[x, y, z] == 0) continue;
                        
                        // Check each direction for a neighbor air block
                        if (y == WorldHeight - 1 || _blocks[x, y + 1, z] == 0) {
                            // Add top face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z + 1 - centerZ));

                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 1);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 3);
                            triangles.Add(vertIndex + 2);
                        }
                        if (y == 0 || _blocks[x, y - 1, z] == 0) {
                            // Add bottom face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z + 1 - centerZ));
    
                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 1);
                            triangles.Add(vertIndex + 2);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 3);
                        }
                        if (z == chunkWidth - 1 || _blocks[x, y, z + 1] == 0) {
                            // Add north face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z + 1 - centerZ));

                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 1);
                            triangles.Add(vertIndex + 2);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 3);
                        }
                        if (z == 0 || _blocks[x, y, z - 1] == 0) {
                            // Add south face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z - centerZ));

                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 1);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 3);
                            triangles.Add(vertIndex + 2);
                        }
                        if (x == chunkWidth - 1 || _blocks[x + 1, y, z] == 0) {
                            // Add east face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x + 1 - centerX, y + 1 + WorldBottom, z - centerZ));

                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 1);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 3);
                            triangles.Add(vertIndex + 2);
                        }
                        if (x == 0 || _blocks[x - 1, y, z] == 0) {
                            // Add west face
                            var vertIndex = vertices.Count;

                            // Each corner
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z + 1 - centerZ));
                            vertices.Add(new Vector3(x - centerX, y + 1 + WorldBottom, z - centerZ));

                            // Each triangle
                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 1);
                            triangles.Add(vertIndex + 2);

                            triangles.Add(vertIndex + 0);
                            triangles.Add(vertIndex + 2);
                            triangles.Add(vertIndex + 3);
                        }
                    }
                }
            }
            
            
            
            var mesh = new Mesh {
                // Convert to array
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray()
            };
            
            mesh.RecalculateNormals();

            GetComponent<MeshFilter>().mesh = mesh;
        }

        private static float GetTerrainHeight(int x, int z) {
            // Base terrain (bedrock to sea level)
            var baseNoise = Mathf.PerlinNoise(x * 0.1f, z * 0.1f);
            var baseHeight = baseNoise * (SeaLevel - WorldBottom) + WorldBottom;
    
            // Terrain variation above sea level (plains, hills, mountains)
            var terrainNoise = Mathf.PerlinNoise(x * 0.05f, z * 0.05f);
            var terrainVariation = terrainNoise * (WorldTop - SeaLevel);
    
            return baseHeight + terrainVariation;
        }

        private static byte GetBlockType(int worldY, float terrainHeight) {
            // Get block type from depth
            var depthFromSurface = terrainHeight - worldY;

            return depthFromSurface switch {
                < 1 => (byte)BlockType.Grass,
                < 4 => (byte)BlockType.Dirt,
                _ => (byte)BlockType.Stone
            };
        }
    }

    public enum BlockType {
        Air = 0,
        Stone = 1,
        Water = 2,
        Grass = 3,
        Dirt = 4
    }
}