using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Core;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public class Orc : Character
    {
        public override int Health { get; set; } = 40;
        public override int Damage { get; set; } = 30;
        public float Time { get; set; } = 0;
        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }


        protected override void OnUpdate(float deltaTime)
        {

            var random = new System.Random();
            Time += deltaTime;
            if (Time > 1)
            {
                Time = 0;
                (int x, int y) orcPosition = (Position.x, Position.y);

                var orcAtPosition = ActorManager.Singleton.GetActorAt(orcPosition);
                List<(int i, int j)> possiblePositions = new List<(int i, int j)>();
                possiblePositions.Add((Position.x, Position.y + 1));
                possiblePositions.Add((Position.x, Position.y - 1));
                possiblePositions.Add((Position.x + 1, Position.y));
                possiblePositions.Add((Position.x - 1, Position.y));
                while (true)
                {
                    int index = random.Next(0, possiblePositions.Count);
                    Debug.Log(possiblePositions[index]);
                    var pos = possiblePositions[index];
                    if (ActorManager.Singleton.GetActorAt(pos) == null)
                    {
                        this.Position = pos;
                        break;
                    }
                }
                

            }
        }


        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("Orc: You fought with honor Beautiful Fetus!", UserInterface.TextPosition.BottomLeft);
        }

        public override int DefaultSpriteId => 121;
        public override string DefaultName => "Orc";
    }
}
