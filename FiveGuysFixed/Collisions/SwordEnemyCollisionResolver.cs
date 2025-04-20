using Microsoft.Xna.Framework;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Collisions;
using System.Collections.Generic;
using FiveGuysFixed.Common;
using System.Diagnostics;
using System.Linq;

public class SwordEnemyCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public SwordEnemyCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
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

        foreach (var enemy1 in GameState.currentRoomContents.Enemies.ToList())
        {
            if (playerRect.Intersects(enemy.BoundingBox) && GameState.PlayerState.isAttacking)
            {
                enemy.TakeDamage(1);
                Debug.WriteLine("hello");
            }
        }
    }
}

