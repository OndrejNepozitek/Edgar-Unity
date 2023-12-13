using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edgar.Unity.Export;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Edgar.Unity.Editor.Import
{
    #if OndrejNepozitekEdgar
    public class ImportRunner
    {
        private string _name;
        private Tile _outlineTile = AssetDatabase.LoadAssetAtPath<Tile>($"Assets\\Edgar\\Examples\\Grid2D\\Example1\\Tiles\\example1_69.asset");
        
        public void Run(string exportPath)
        {
            _name = "EdgarImportTest";
            var fileContent = File.ReadAllText(exportPath);
            var exportDto = JsonUtility.FromJson<ExportDto>(fileContent);
            var outputFolder = "Assets/EdgarImportTest";
            
            if (Directory.Exists(outputFolder))
            {
                Directory.Delete(outputFolder, true); 
            }
            Directory.CreateDirectory(outputFolder);

            var roomTemplatesFolder = Path.Combine(outputFolder, "Room templates");
            Directory.CreateDirectory(roomTemplatesFolder);

            var tile = AssetDatabase.LoadAssetAtPath<Tile>($"Assets\\Edgar\\Examples\\Grid2D\\Example1\\Tiles\\example1_79.asset");

            // TODO: handle duplicate names
            foreach (var roomTemplateDto in exportDto.RoomTemplates)
            {
                SaveRoomTemplateAsPrefab(roomTemplateDto, roomTemplatesFolder, tile);
            }
            
            CreateLevelGraph(exportDto.LevelGraph, outputFolder, roomTemplatesFolder);

            CreateScene(outputFolder);
        }

        private void CreateScene(string folder)
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(scene, Path.Combine(folder, $"{_name}.unity"));

            var generator = new GameObject("Generator");
            var dungeonGenerator = generator.AddComponent<DungeonGeneratorGrid2D>();
            generator.AddComponent<ExportPostProcessing>();
            
            var levelGraph = AssetDatabase.LoadAssetAtPath<LevelGraph>(Path.Combine(folder, "Level graph.asset"));
            dungeonGenerator.FixedLevelGraphConfig = new FixedLevelGraphConfigGrid2D()
            {
                LevelGraph = levelGraph,
            };
            
            EditorSceneManager.SaveScene(scene);
        }

        private void CreateLevelGraph(LevelGraphDto levelGraphDto, string folder, string roomTemplatesFolder)
        {
            var levelGraph = ScriptableObject.CreateInstance<LevelGraph>();
            AssetDatabase.CreateAsset(levelGraph, Path.Combine(folder, "Level graph.asset"));
            
            levelGraph.name = "Level graph";
            levelGraph.EditorData = new LevelGraphEditorData()
            {
                Zoom = 0.7f,
                PanOffset = new Vector2(222f, -55f),
            };
            levelGraph.Rooms = new List<RoomBase>();
            levelGraph.Connections = new List<ConnectionBase>();
            
            levelGraphDto.DefaultRoomTemplates.ForEach(x =>
            {
                var prefabPath = Path.Combine(roomTemplatesFolder, $"{x}.prefab");
                var roomTemplate = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
                levelGraph.DefaultIndividualRoomTemplates.Add(roomTemplate as GameObject);
            });
            
            levelGraphDto.DefaultCorridorRoomTemplates.ForEach(x =>
            {
                var prefabPath = Path.Combine(roomTemplatesFolder, $"{x}.prefab");
                var roomTemplate = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
                levelGraph.CorridorIndividualRoomTemplates.Add(roomTemplate as GameObject);
            });

            foreach (var roomDto in levelGraphDto.Rooms)
            {
                var room = ScriptableObject.CreateInstance<Room>();
                AssetDatabase.AddObjectToAsset(room, levelGraph);
                
                room.Name = roomDto.DisplayName;
                room.name = roomDto.Id;
                room.Position = roomDto.Position;
                
                roomDto.RoomTemplates.ForEach(x =>
                {
                    var prefabPath = Path.Combine(roomTemplatesFolder, $"{x}.prefab");
                    var roomTemplate = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
                    room.IndividualRoomTemplates.Add(roomTemplate as GameObject);
                });

                levelGraph.Rooms.Add(room);
            }

            foreach (var connectionDto in levelGraphDto.Connections)
            {
                var connection = ScriptableObject.CreateInstance<ExportConnection>();
                AssetDatabase.AddObjectToAsset(connection, levelGraph);

                connection.From = levelGraph.Rooms.Single(x => x.name == connectionDto.From);
                connection.To = levelGraph.Rooms.Single(x => x.name == connectionDto.To);
                connection.name = $"C: {connectionDto.From} -> {connectionDto.To}";
                
                connectionDto.RoomTemplates.ForEach(x =>
                {
                    var prefabPath = Path.Combine(roomTemplatesFolder, $"{x}.prefab");
                    var roomTemplate = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
                    connection.RoomTemplates.Add(roomTemplate as GameObject);
                });
                
                levelGraph.Connections.Add(connection);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void SaveRoomTemplateAsPrefab(RoomTemplateDto roomTemplateDto, string folder, Tile tile)
        {
            // Create empty game object
            var roomTemplate = new GameObject();

            // Add room template initializer, initialize room template, destroy initializer
            var roomTemplateInitializer = roomTemplate.AddComponent<DungeonRoomTemplateInitializerGrid2D>();
            roomTemplateInitializer.Initialize();
            Object.DestroyImmediate(roomTemplateInitializer);

            var floor = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate).Single(x => x.name == "Floor");
            foreach (var position in roomTemplateDto.Tiles)
            {
                floor.SetTile(position, tile);
            }
            
            var walls = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate).Single(x => x.name == "Walls");
            walls.gameObject.AddComponent<OutlineOverrideGrid2D>();
            walls.gameObject.AddComponent<IgnoreTilemapGrid2D>();
            foreach (var position in roomTemplateDto.OutlineTiles)
            {
                walls.SetTile(position, _outlineTile);
            }
            
            var other = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplate).Single(x => x.name == "Other 1");
            foreach (var position in roomTemplateDto.OutlineTiles)
            {
                other.SetTile(position, _outlineTile);
            }

            var doors = roomTemplate.GetComponent<DoorsGrid2D>();
            var doorsDto = roomTemplateDto.Doors;
            doors.DoorsList = doorsDto.DoorsList;
            doors.SelectedMode = doorsDto.SelectedMode;
            doors.HybridDoorModeData = doorsDto.HybridDoorModeData;
            doors.ManualDoorModeData = doorsDto.ManualDoorModeData;
            doors.SimpleDoorModeData = doorsDto.SimpleDoorModeData;

            var roomTemplateComponent = roomTemplate.GetComponent<RoomTemplateSettingsGrid2D>();
            roomTemplateComponent.RepeatMode = roomTemplateDto.RepeatMode;
    
            var prefabPath = Path.Combine(folder, $"{roomTemplateDto.Name}.prefab");
            PrefabUtility.SaveAsPrefabAsset(roomTemplate, AssetDatabase.GenerateUniqueAssetPath(prefabPath));

            // Remove game object from scene
            Object.DestroyImmediate(roomTemplate);
        }
        
        
        [MenuItem("Edgar debug/Import")]
        public static void SyncBuildScenes()
        {
            var runner = new ImportRunner();
            runner.Run("export.json");
        }
    }
    #endif
}