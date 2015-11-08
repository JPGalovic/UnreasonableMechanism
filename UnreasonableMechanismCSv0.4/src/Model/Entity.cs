﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UM = UnreasonableMechanismEngineCS;
using UnreasonableMechanismEngineCS;
using SwinGameSDK;

namespace UnreasonableMechanismCS
{
    /// <summary>
    /// Entity defines base class for entities within the play area.
    /// </summary>
    public abstract class Entity
    {
        private string _bitmap;
        private Polygon _hitbox;
        private int _hitpoints;
        private int _tick;

        /// <summary>
        /// Constructs new entity.
        /// </summary>
        /// <param name="bitmap">Name of bitmap to use.</param>
        /// <param name="hitbox">Definition of hitbox (polygon).</param>
        /// <param name="hitpoints">Number of hitpoints.</param>
        public Entity(string bitmap, Polygon hitbox, int hitpoints)
        {
            _bitmap = bitmap;
            _hitbox = hitbox;
            _hitpoints = hitpoints;
            _tick = 0;
        }

        /// <summary>
        /// Property: Bitmap name.
        /// </summary>
        public string Bitmap
        {
            get
            {
                return _bitmap;
            }

            set
            {
                _bitmap = value;
            }
        }

        /// <summary>
        /// Readonly Property: Hitbox.
        /// </summary>
        public Polygon Hitbox
        {
            get
            {
                return _hitbox;
            }
        }

        /// <summary>
        /// Property: Hitpoints.
        /// </summary>
        public int Hitpoints
        {
            get
            {
                return _hitpoints;
            }

            set
            {
                _hitpoints = value;
            }
        }

        /// <summary>
        /// Draws the game bitmap.
        /// </summary>
        public virtual void DrawEntity()
        {
            SwinGame.DrawBitmap(GameResources.GameImage(_bitmap), (float)_hitbox.Center.X - GameResources.GameImage(Bitmap).Width / 2, (float)_hitbox.Center.Y - GameResources.GameImage(Bitmap).Height / 2);
        }

        /// <summary>
        /// Processes entity events.
        /// </summary>
        public abstract void ProcessEvents();

        /// <summary>
        /// Processes entity movement.
        /// </summary>
        public abstract void ProcessMovement();

        /// <summary>
        /// Offsets the entity by the given movement vector.
        /// </summary>
        /// <param name="movement">Movement vector.</param>
        public void Offset(UM.Vector movement)
        {
            _hitbox.Offset(movement);
        }
    }
}