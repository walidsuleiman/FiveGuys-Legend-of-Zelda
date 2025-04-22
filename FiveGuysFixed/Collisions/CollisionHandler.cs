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
using FiveGuysFixed.Projectiles;
using System.Linq;
using Microsoft.Xna.Framework.Media;

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
        Rectangle playerRect = player.GetBoundingBox(80, 80);
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

        if (GameState.Player.IsInvincible)
        {
            return; // If the player is invincible, ignore the collision and enter an untouchable state.
        }
        // Compute the bounding boxes for both player and enemy.
        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle enemyRect = enemy.BoundingBox;

        if (playerRect.Intersects(enemyRect))
        {
            GameState.Player.TakeDamage(1);
            GameState.Player.SetInvincibility(2.5f); // Set invincibility for 2.5 seconds after being hit

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

    public void HandleSwordEnemyCollision(IPlayer player, IEnemy enemy)
    {


        Rectangle playerRect = player.GetBoundingBox(80, 80);

        switch (GameState.PlayerState.direction)
        {
            case Dir.UP:
                playerRect = player.NEWGetBoundingBox(200, 200);
                break;
            case Dir.DOWN:
                playerRect = player.NEWGetBoundingBox(200, 200);
                break;
            case Dir.LEFT:
                playerRect = player.NEWGetBoundingBox(200, 200);
                break;
            case Dir.RIGHT:
                playerRect = player.NEWGetBoundingBox(200, 200);
                break;
        }

        Rectangle enemyRect = enemy.BoundingBox;


        foreach (var enemy1 in GameState.roomManager.getCurrentRoom().Enemies.ToList())
        {
            if (playerRect.Intersects(enemy.BoundingBox) && GameState.PlayerState.isAttacking)
            {
                enemy.TakeDamage(1);
                Debug.WriteLine("hello");
            }
        }



    }

    public void HandlePlayerItemCollision(IPlayer player, IItem item)
    {
        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle itemRect = item.BoundingBox;

        // Ensure item is only collected once per room load
        if (playerRect.Intersects(itemRect) && !GameState.collectedItems.Contains(item))
        {

            ((Player)player).ItemPickupSound?.Play(); // Play item pickup sound
            item.Use();

            GameState.collectedItems.Add(item);
            GameState.itemsToRemove.Add(item);
        }
    }

    public void HandleEnemyBlockCollision(IEnemy enemy, IBlock block)
    {
        // Compute the bounding boxes for both enemy and block.
        Rectangle enemyRect = enemy.BoundingBox;
        Rectangle blockRect = block.BoundingBox;

        if (enemyRect.Intersects(blockRect))
        {

            Rectangle intersection = Rectangle.Intersect(enemyRect, blockRect);
            Vector2 enemyPosition = enemy.Position;
            if (intersection.Width < intersection.Height)
            {
                if (enemyRect.Center.X < blockRect.Center.X)
                    enemyPosition.X -= intersection.Width;
                else
                    enemyPosition.X += intersection.Width;
            }
            else
            {
                if (enemyRect.Center.Y < blockRect.Center.Y)
                    enemyPosition.Y -= intersection.Height;
                else
                    enemyPosition.Y += intersection.Height;
            }
            enemy.Position = enemyPosition;
        }
    }

    public void HandlePlayerProjectileCollision(IPlayer player, IProjectile projectile)
    {
        if (projectile is FiveGuysFixed.Projectiles.Boomerang b && b.IsLinkBoomerang)
            return;
        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle projectileRect = projectile.BoundingBox;

        if (playerRect.Intersects(projectileRect))
        {
            GameState.Player.TakeDamage(1);
            GameState.Player.SetInvincibility(2.5f); // Set invincibility for 2.5 seconds after being hit

            //remove projectile from screen
        }
    }
    public void HandleProjectileEnemyCollision(IProjectile proj, IEnemy enemy)
    {
        if (proj is Boomerang b)
        {
            if (b.Owner == enemy) return;
            if (proj.BoundingBox.Intersects(enemy.BoundingBox))
            {
                enemy.TakeDamage(1);
            }
        }
    }
}