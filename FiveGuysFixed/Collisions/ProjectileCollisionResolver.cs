using Microsoft.Xna.Framework;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Projectiles;
using FiveGuysFixed.Enemies;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Collisions;
using System.Diagnostics;

public class ProjectileCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public ProjectileCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
    }

    public void ResolvePlayerHit(IPlayer player, IProjectile proj)
    {
        if (!proj.isEnemyProjectile()) return;

        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle projectileRect = proj.BoundingBox;

        if (collisionDetector.IsColliding(playerRect, projectileRect))
        {
            player.TakeDamage(1);
            player.SetInvincibility(2.5f);
            Debug.WriteLine("Projectile Hit Player");
        }
    }

    public void ResolveEnemyHit(IProjectile proj, IEnemy enemy)
    {
        if (proj.isEnemyProjectile()) return;

        if (collisionDetector.IsColliding(proj.BoundingBox, enemy.BoundingBox))
        {
            enemy.TakeDamage(1);
        }
        if (proj is Boomerang)
        {
            ContentLoader.hitSound.Play();
        }
    }
}
