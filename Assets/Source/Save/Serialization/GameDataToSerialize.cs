using DungeonCrawl.Actors;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Serialization
{
    [System.Serializable]
    public class GameDataToSerialize
    {
        public int Map;
        public int x;
        public int y;
        public int Health;

        public List<string> Inventory = new List<string>();
        public List<ActorToSerialize> AllActors = new List<ActorToSerialize>();

        public GameDataToSerialize()
        {

        }
        public GameDataToSerialize(Player player)
        {
            Map = MapLoader.Map;
            x = player.Position.x;
            y = player.Position.y;
            Health = player.Health;
            foreach(var item in player.Inventory)
            {
                Inventory.Add(item.DefaultName);
            }
            PopulateActorList();
        }

        public void PopulateActorList()
        {
            HashSet<Actor> allActors = ActorManager.Singleton.GetAllActors();
            foreach (Actor actor in allActors)
                AllActors.Add(new ActorToSerialize(actor));
        }
    }
}
