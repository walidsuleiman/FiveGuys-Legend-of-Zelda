using Microsoft.Xna.Framework;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.Collisions;

public class EnemyBlockCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public EnemyBlockCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
    }

    public void Resolve(IEnemy enemy, IBlock block)
    {
        Rectangle enemyRect = enemy.BoundingBox;
        Rectangle blockRect = block.BoundingBox;

        if (collisionDetector.IsColliding(enemyRect, blockRect))
        {
            Rectangle intersection = Rectangle.Intersect(enemyRect, blockRect);
            Vector2 enemyPos = enemy.Position;

            if (intersection.Width < intersection.Height)
            {
                enemyPos.X += enemyRect.Center.X < blockRect.Center.X
                    ? -intersection.Width : intersection.Width;
            }
            else
            {
                enemyPos.Y += enemyRect.Center.Y < blockRect.Center.Y
                    ? -intersection.Height : intersection.Height;
            }

            enemy.Position = enemyPos;
        }
    }
}
