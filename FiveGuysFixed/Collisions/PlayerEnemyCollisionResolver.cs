using Microsoft.Xna.Framework;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Common;

public class PlayerEnemyCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public PlayerEnemyCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
    }

    public void Resolve(IPlayer player, IEnemy enemy)
    {
        if (player.IsInvincible) return;

        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle enemyRect = enemy.BoundingBox;

        if (collisionDetector.IsColliding(playerRect, enemyRect))
        {
            player.TakeDamage(1);
            player.SetInvincibility(2.5f);

            const int knockback = 35;
            switch (GameState.PlayerState.direction)
            {
                case Dir.UP: GameState.PlayerState.position.Y += knockback; break;
                case Dir.DOWN: GameState.PlayerState.position.Y -= knockback; break;
                case Dir.LEFT: GameState.PlayerState.position.X += knockback; break;
                case Dir.RIGHT: GameState.PlayerState.position.X -= knockback; break;
            }
        }
    }
}
