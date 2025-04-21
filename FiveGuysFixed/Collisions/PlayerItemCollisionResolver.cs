using Microsoft.Xna.Framework;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Items;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Collisions;
using System.Diagnostics;

public class PlayerItemCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public PlayerItemCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
    }

    public void Resolve(IPlayer player, IItem item)
    {
        Rectangle playerRect = player.GetBoundingBox(80, 80);
        Rectangle itemRect = item.BoundingBox;

        if (collisionDetector.IsColliding(playerRect, itemRect) && !GameState.collectedItems.Contains(item))
        {
            Debug.WriteLine("Item Collected");

            ((Player)player).ItemPickupSound?.Play();
            item.Use();

            GameState.collectedItems.Add(item);
            GameState.itemsToRemove.Add(item);
        }
    }
}
