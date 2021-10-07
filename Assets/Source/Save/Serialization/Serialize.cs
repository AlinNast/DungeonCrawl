using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonCrawl.Serialization
{
    class Serialize
    {
        public static string SerializeGame(Player player)
        {
            return JsonConvert.SerializeObject(new GameDataToSerialize(player));
        }

        public static void SaveGameToFile(Player player)
        {
            string json = SerializeGame(player);
            string path = Application.dataPath + @"/exported_saves/" + DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".json";
            File.WriteAllText(path, json);
        }

        public static void LoadGameFromFile(string JsonString)
        {

        }

        public static GameDataToSerialize DeserializeGame()
        {
            string folderPath = Application.dataPath + @"/exported_saves";
            var directory = new DirectoryInfo(folderPath);

            string latestSaveFile = directory.GetFiles()
             .Where(f => f.Extension == ".json")
             .OrderByDescending(f => f.LastWriteTime)
             .First().FullName;

            string json = File.ReadAllText(latestSaveFile);
            return JsonConvert.DeserializeObject<GameDataToSerialize>(json);
        }
        public static void Load()
        {
            try
            {
                var gameObject = DeserializeGame();
                Load(gameObject);
            }
            catch (Exception)
            {
                Debug.LogError("Something went wrong when you tried to import game from the local database. Make sure you have a game saved.");
            }

        }

        public static void Load(GameDataToSerialize gameObject)
        {
            
            ActorManager.Singleton.DestroyAllActors();
            MapLoader.LoadMapFromGameObject(gameObject.Map, gameObject);
        }
    }
}
