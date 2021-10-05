using Assets.Source.Core;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Skeleton : Character
    {
        public override int Health { get; set; } = 30;
        public override int Damage { get; set; } = 10;
        
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("Skeleton: Well, I was already dead anyway...", UserInterface.TextPosition.BottomLeft);
        }

        public override int DefaultSpriteId => 316;
        public override string DefaultName => "Skeleton";
    }
}
