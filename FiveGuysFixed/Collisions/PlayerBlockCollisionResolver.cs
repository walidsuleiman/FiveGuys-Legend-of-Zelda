using Microsoft.Xna.Framework;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Blocks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Collisions;

public class PlayerBlockCollisionResolver
{
    private readonly CollisionDetector collisionDetector;

    public PlayerBlockCollisionResolver()
    {
        collisionDetector = new CollisionDetector();
    }

    public void Resolve(IPlayer player, IBlock block)
    {
        Rectangle playerRect = player.GetBoundingBox(70, 70);
        Rectangle blockRect = block.BoundingBox;

        if (collisionDetector.IsColliding(playerRect, blockRect))
        {
            Rectangle intersection = Rectangle.Intersect(playerRect, blockRect);

            if (intersection.Width < intersection.Height)
            {
                GameState.PlayerState.position.X += playerRect.Center.X < blockRect.Center.X
                    ? -intersection.Width : intersection.Width;
            }
            else
            {
                GameState.PlayerState.position.Y += playerRect.Center.Y < blockRect.Center.Y
                    ? -intersection.Height : intersection.Height;
            }
        }
    }
}