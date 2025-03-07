using FiveGuysFixed.Blocks;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;
using Microsoft.Xna.Framework;

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
    }

