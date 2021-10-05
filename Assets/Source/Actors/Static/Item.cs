using DungeonCrawl.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonCrawl.Actors.Static
{
    public abstract class Item : Actor
    {
        public override bool OnCollision(Actor anotherActor)
        {
            return true;
        }


        public override int Z => -1;

    }
}
