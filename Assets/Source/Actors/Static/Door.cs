using DungeonCrawl.Actors.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawl.Actors.Static
{
    public class Door : Actor
    {
        public override int DefaultSpriteId => 431;
        public override string DefaultName => "Door";
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }
    }
}
