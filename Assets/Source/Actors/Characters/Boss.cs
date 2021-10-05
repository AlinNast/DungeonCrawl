using Assets.Source.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Boss : Character
    {
        public override int Health { get; set; } = 50;
        public override int Damage { get; set; } = 30;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("Kite of Kites: Curse you Beautiful Fetus, you defeated the Kite of Kites!!!", UserInterface.TextPosition.BottomLeft);
            UserInterface.Singleton.SetText("GAME WON!!! You got to marry Marry!!!", UserInterface.TextPosition.MiddleCenter);

        }

        public override int DefaultSpriteId => 317;
        public override string DefaultName => "Boss";
    }
}
