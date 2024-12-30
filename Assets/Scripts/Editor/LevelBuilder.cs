using System.Collections.Generic;
using GameCore;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LevelBuilder : EditorWindow
    {
        private int rows = 5;

        private int columns = 5;

        private int[,] grid;

        private GameObject wall;
        private Road road;

        private LevelPrefab _levelPrefab;

        private List<Object> _blocks;

    

        [MenuItem("Window/Level Editor")]

        public static void ShowWindow()

        {
            GetWindow<LevelBuilder>("Level Editor");
        }


        private void OnGUI()
        {
            GUILayout.Label("Level Size", EditorStyles.boldLabel);

        
            rows = EditorGUILayout.IntField("Rows (N)", rows);
            columns = EditorGUILayout.IntField("Columns (M)", columns);
            wall = (GameObject)EditorGUILayout.ObjectField("Wall Prefab", wall, typeof(GameObject), false);
            road = (Road)EditorGUILayout.ObjectField("Road Prefab", road, typeof(Road), false);
            _levelPrefab = (LevelPrefab)EditorGUILayout.ObjectField("LevelPrefab", _levelPrefab, typeof(LevelPrefab), true);
        
            if (GUILayout.Button("Create Grid"))
            {
                CreateGrid();
            }
        
            if (grid != null)
            {
                DrawGrid();
            }


            if (GUILayout.Button("Generate Level"))
            {
                GenerateLevel();
            }

            if (GUILayout.Button("Clear Board"))
            {
                ClearBoard();
            }
        }

        private void ClearBoard()
        {
            foreach (var block in _blocks)
            {
                DestroyImmediate(block);
            }
        }


        private void CreateGrid()
        {
            grid = new int[rows, columns];
        }


        private void DrawGrid()
        {
            for (int i = 0; i < rows; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = EditorGUILayout.IntField(grid[i, j], GUILayout.Width(15));
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    
        private void GenerateLevel()
        {
            SpawnBlocks();
        }

        private LevelPrefab lvl;

        private void SpawnBlocks()

        {

            _blocks = new List<Object>();

            lvl = Instantiate(_levelPrefab, Vector3.zero, Quaternion.identity);

            Transform parent = lvl.transform;

            lvl.roads = new List<Road>();
        
            float offsetX = columns / 2f - 0.5f;
            float offsetY = rows / 2f - 0.5f; 
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                
                    Vector3 position = new Vector3(j - offsetX, i - offsetY, 0); // i для Y, j для X


                    switch (grid[i, j])

                    {

                        case 0:

                            GameObject newWall = Instantiate(this.wall, position, Quaternion.identity, parent);

                            _blocks.Add(newWall);

                            break;

                        case 1:

                            Road rd = Instantiate(road, position, Quaternion.identity, parent);

                            rd.x = j;

                            rd.y = i;

                            _blocks.Add(rd);

                            break;

                    }

                }

            }

        }
    }
}
