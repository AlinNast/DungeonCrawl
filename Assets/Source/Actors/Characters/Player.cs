﻿using DungeonCrawl.Core;
using UnityEngine;
using DungeonCrawl.Actors.Static;
using DungeonCrawl.Actors.Static.Items;
using System.Collections.Generic;
using Assets.Source.Core;
using System.Linq;
using System.Data.Common;

namespace DungeonCrawl.Actors.Characters
{
    public class Player : Character
    {
        DbProviderFactory factory;
       
        string connectionString;

        public override int Health { get; set; } = 100;
        public override int Damage { get; set; } = 10;

        public override int DefaultSpriteId => 24;
        public override string DefaultName => "Player";

        public List<Item> Inventory = new List<Item>();

        protected void DisplayInventory()
        {
            foreach (var item in Inventory)
            {
                string inventoryItems = "";
                for (int i = 1; i <= Inventory.Count; i++)
                {
                    inventoryItems += $"{i}. {Inventory.ToArray()[i - 1].DefaultName}" + "\n";
                }
                UserInterface.Singleton.SetText($"Inventory:\n \n{inventoryItems}", UserInterface.TextPosition.BottomRight);
            }
        }


        protected override void OnUpdate(float deltaTime)
        {
            CameraController.Singleton.Position = this.Position;

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Move up
                TryMove(Direction.Up);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Move down
                TryMove(Direction.Down);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // Move left
                TryMove(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                // Move right
                TryMove(Direction.Right);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Pick-up item in inventory
                PickUpItem();
                DisplayInventory();

            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                // Save
                Debug.Log("ceva");
                SaveToDb();
                

            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                // Load
                LoadFromDb();

            }

        }

        public void SaveToDb()
        {
            
            connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=DcDb;Trusted_Connection=True;";
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = $"Insert Into player (player_name, hp, x, y) Values ('John', 100, 4, 6);";
                command.ExecuteNonQuery();
            }
            UserInterface.Singleton.SetText("Game Saved!", UserInterface.TextPosition.TopLeft);
        }

        public void LoadFromDb()
        {

        }

        public void PickUpItem()
        {
            var actorAtTargetPosition = ActorManager.Singleton.GetActorAt<Item>(Position);
            if (actorAtTargetPosition.DefaultName == "Alive-dead water Potion")
            {
                UserInterface.Singleton.SetText($"Beautiful Fetus picked up \n a {actorAtTargetPosition.DefaultName}!", UserInterface.TextPosition.BottomLeft);
                //Inventory.Add(actorAtTargetPosition);
                this.Health += 50;
            }
            if (actorAtTargetPosition.DefaultName == "BroadSword")
            {
                UserInterface.Singleton.SetText($"Beautiful Fetus picked up \n a {actorAtTargetPosition.DefaultName}!", UserInterface.TextPosition.BottomLeft);
                Inventory.Add(actorAtTargetPosition);
                this.Damage += 30;
                this.SetSprite(26);

            }
            if (actorAtTargetPosition.DefaultName == "Key")
            {
                UserInterface.Singleton.SetText($"Beautiful Fetus picked up \n a {actorAtTargetPosition.DefaultName}!", UserInterface.TextPosition.BottomLeft);
                Inventory.Add(actorAtTargetPosition);
            }
            ActorManager.Singleton.DestroyActor(actorAtTargetPosition);

        }

        public bool CheckForKey()
        {
            foreach(var item in Inventory)
            {
                if(item.DefaultName == "Key")
                {
                    return true;
                }
            }
            return false;
        }


        public void GetDamage(Character character)
        {
            this.Health -= character.Damage;

            if (this.Health <= 0)
            {
                // Die
                OnDeath();

                ActorManager.Singleton.DestroyActor(this);
            }
        }


        public override bool OnCollision(Actor anotherActor)
        {
            return false;
        }

        protected override void OnDeath()
        {
            UserInterface.Singleton.SetText("You are no match for me... RIP Beatiful Fetus!", UserInterface.TextPosition.BottomLeft);
            
        }



        
        
    }
}
