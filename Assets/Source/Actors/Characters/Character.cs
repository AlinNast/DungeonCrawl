using Assets.Source.Core;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Static.Items;
using DungeonCrawl.Core;
using UnityEngine;

namespace DungeonCrawl.Actors.Characters
{
    public abstract class Character : Actor
    {
        public abstract int Health { get; set; }

        public abstract int Damage { get; set; }

        public void GetDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }



        public void TryMove(Direction direction)
        {
            var vector = direction.ToVector();
            (int x, int y) targetPosition = (Position.x + vector.x, Position.y + vector.y);

            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt(targetPosition);
            
            if (actorAtTargetPosition == null)
            {
                // No obstacle found, just move
                Position = targetPosition;
               // CameraController.Singleton.Position = this.Position;
            }
            else
            {
                if (actorAtTargetPosition != null)
                {
                    if (actorAtTargetPosition is Item item)
                    {
                        Position = targetPosition;
                    }


                    if (actorAtTargetPosition is Character character)
                    {
                        this.GetDamage(character.Damage);
                        character.GetDamage(this.Damage);
                    }

                    if (actorAtTargetPosition is Door door && this is Player player)
                    {
                        if (player.CheckForKey())
                        {
                            door.SetSprite(433);
                            Position = targetPosition;
                            if (Position == targetPosition)
                            {
                                ActorManager.Singleton.DestroyAllActors();
                                GameManager.LoadNextMap(ActorManager.Singleton.level);
                                ActorManager.Singleton.level++;
                            }
                        }
                        else
                        {
                            UserInterface.Singleton.SetText("The door is locked \n get a key!", UserInterface.TextPosition.BottomLeft);
                        }
                    }
                }
            }
            
        }

        protected abstract void OnDeath();


        /// <summary>
        ///     All characters are drawn "above" floor etc
        /// </summary>
        public override int Z => -1;
    }
}
