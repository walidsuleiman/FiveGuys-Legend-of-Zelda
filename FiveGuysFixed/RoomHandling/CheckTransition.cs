using System;
using System.Collections.Generic;
using System.Diagnostics;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;

namespace FiveGuysFixed.RoomHandling
{
    public static class CheckTransition
    {
        // these rectangles should exactly match the gaps in our room borders
        private static readonly Rectangle TOP_DOOR = new Rectangle(552, 50, 160, 32);
        private static readonly Rectangle BOTTOM_DOOR = new Rectangle(552, 626, 160, 32);
        private static readonly Rectangle LEFT_DOOR = new Rectangle(200, 306, 32, 128);
        private static readonly Rectangle RIGHT_DOOR = new Rectangle(1032, 306, 32, 128);

        // this stores which neighbors exist for the current room
        private static Dictionary<Dir, bool> availableDoors = new Dictionary<Dir, bool>();

        // check if the player is at a door to transition rooms
        public static void CheckRoomExit()
        {
            //don't check during transitions
            if (GameState.IsTransitioning) return;

            UpdateAvailableDoors();

            var playerPos = GameState.PlayerState.position;
            var playerRect = new Rectangle((int)playerPos.X - 16, (int)playerPos.Y - 16, 32, 32);

            // check each door for transition, but only if that door exists
            if (availableDoors[Dir.UP] && playerRect.Intersects(TOP_DOOR))
            {
                AttemptRoomTransition(Dir.UP);
            }
            else if (availableDoors[Dir.DOWN] && playerRect.Intersects(BOTTOM_DOOR))
            {
                AttemptRoomTransition(Dir.DOWN);
            }
            else if (availableDoors[Dir.LEFT] && playerRect.Intersects(LEFT_DOOR))
            {
                AttemptRoomTransition(Dir.LEFT);
            }
            else if (availableDoors[Dir.RIGHT] && playerRect.Intersects(RIGHT_DOOR))
            {
                AttemptRoomTransition(Dir.RIGHT);
            }
        }

        private static void UpdateAvailableDoors()
        {
            // initialize dictionary 
            if (availableDoors.Count == 0)
            {
                availableDoors[Dir.UP] = false;
                availableDoors[Dir.DOWN] = false;
                availableDoors[Dir.LEFT] = false;
                availableDoors[Dir.RIGHT] = false;
            }

            availableDoors[Dir.UP] = GameState.roomManager.TryGetNeighborRoomID(GameState.currentRoomID, Dir.UP, out _);
            availableDoors[Dir.DOWN] = GameState.roomManager.TryGetNeighborRoomID(GameState.currentRoomID, Dir.DOWN, out _);
            availableDoors[Dir.LEFT] = GameState.roomManager.TryGetNeighborRoomID(GameState.currentRoomID, Dir.LEFT, out _);
            availableDoors[Dir.RIGHT] = GameState.roomManager.TryGetNeighborRoomID(GameState.currentRoomID, Dir.RIGHT, out _);

            Debug.WriteLine($"Room {GameState.currentRoomID} doors: U:{availableDoors[Dir.UP]} D:{availableDoors[Dir.DOWN]} L:{availableDoors[Dir.LEFT]} R:{availableDoors[Dir.RIGHT]}");
        }

        private static void AttemptRoomTransition(Dir direction)
        {
            Debug.WriteLine($"Attempting to transition: {direction}");

            if (GameState.roomManager.TryGetNeighborRoomID(GameState.currentRoomID, direction, out int neighborRoomID))
            {
                Debug.WriteLine($"Transitioning from Room {GameState.currentRoomID} to Room {neighborRoomID} via {direction}");

                GameState.previousRoomContents = GameState.currentRoomContents;

                InitiateTransition(direction, neighborRoomID);
            }
        }

        private static void InitiateTransition(Dir direction, int newRoomID)
        {
            GameState.currentRoomID = newRoomID;

            GameState.transitionDir = direction;
            GameState.transitionX = 0.0f;
            GameState.IsTransitioning = true;

            RepositionPlayerForNewRoom(direction);

            GameState.ShouldSwitchRoom = true;
        }

        private static void RepositionPlayerForNewRoom(Dir direction)
        {
            float playerX = GameState.PlayerState.position.X;
            float playerY = GameState.PlayerState.position.Y;

            //position Link at the appropriate entrance of the new room
            switch (direction)
            {
                case Dir.LEFT:
                    // if going left, reposition Link to the right door of new room
                    playerX = RIGHT_DOOR.X - 40;
                    playerY = RIGHT_DOOR.Y + RIGHT_DOOR.Height / 2;
                    break;

                case Dir.RIGHT:
                    playerX = LEFT_DOOR.X + LEFT_DOOR.Width + 40;
                    playerY = LEFT_DOOR.Y + LEFT_DOOR.Height / 2;
                    break;

                case Dir.UP:
                    playerX = BOTTOM_DOOR.X + BOTTOM_DOOR.Width / 2;
                    playerY = BOTTOM_DOOR.Y - 40;
                    break;

                case Dir.DOWN:
                    playerX = TOP_DOOR.X + TOP_DOOR.Width / 2;
                    playerY = TOP_DOOR.Y + TOP_DOOR.Height + 40;
                    break;
            }

            GameState.PlayerState.position = new Vector2(playerX, playerY);
        }
    }
}