using FiveGuysFixed.Blocks;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;
using Microsoft.Xna.Framework;
using FiveGuysFixed.Common;
using FiveGuysFixed.Items;
using FiveGuysFixed.Weapons___Items;
using FiveGuysFixed.Commands;
using FiveGuysFixed.RoomHandling;
using System.Diagnostics;

public class CollisionHandler
{
    private CollisionDetector collisionDetector;

    public CollisionHandler()
    {
        collisionDetector = new CollisionDetector();
    }

    public void HandlePlayerBlockCollision(IPlayer player, IBlock block)
    {
        // Compute the bounding boxes for both player and block.
        Rectangle playerRect = player.GetBoundingBox(32, 32);
        Rectangle blockRect = block.BoundingBox; // Provided by IBlock

        if (playerRect.Intersects(blockRect))
        {
            // Calculate the intersection rectangle
            Rectangle intersection = Rectangle.Intersect(playerRect, blockRect);

            // Resolve collision by moving player out along the smallest penetration axis
            if (intersection.Width < intersection.Height)
            {
                if (playerRect.Center.X < blockRect.Center.X)
                    GameState.PlayerState.position.X -= intersection.Width;
                else
                    GameState.PlayerState.position.X += intersection.Width;
            }
            else
            {
                if (playerRect.Center.Y < blockRect.Center.Y)
                    GameState.PlayerState.position.Y -= intersection.Height;
                else
                    GameState.PlayerState.position.Y += intersection.Height;
            }
        }
    }

    public void HandlePlayerEnemyCollision(IPlayer player, IEnemy enemy)
    {
        // Compute the bounding boxes for both player and enemy.
        Rectangle playerRect = player.GetBoundingBox(32, 32);
        Rectangle enemyRect = enemy.BoundingBox;

        if (playerRect.Intersects(enemyRect))
        {
            GameState.Player.takeDamage(1);

            switch (GameState.PlayerState.direction)
            {
                case Dir.UP:
                    GameState.PlayerState.position.Y += 35;
                    break;
                case Dir.DOWN:
                    GameState.PlayerState.position.Y -= 35;
                    break;
                case Dir.LEFT:
                    GameState.PlayerState.position.X += 35;
                    break;
                case Dir.RIGHT:
                    GameState.PlayerState.position.X -= 35;
                    break;
            }

        }
    }

    public void HandlePlayerItemCollision(IPlayer player, IItem item)
    {
        Rectangle playerRect = player.GetBoundingBox(32, 32);
        Rectangle itemRect = item.BoundingBox;

        // Ensure item is only collected once per room load
        if (playerRect.Intersects(itemRect) && !GameState.collectedItems.Contains(item))
        {
            Debug.WriteLine("Item Collected");

            item.Use();

            GameState.collectedItems.Add(item);
            GameState.itemsToRemove.Add(item);
        }
    }



}

