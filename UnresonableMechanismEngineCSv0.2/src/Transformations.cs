﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnreasonableMechanismEngineCS
{
    public static class Transformations
    {
        /// <summary>
        /// Pitches the point about the y coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to pitch.</param>
        /// <param name="point">Point to pitch about.</param>
        public static Point PitchY(Point delta,double angle, Point point)
        {
            delta -= point;

            double x = delta.X;
            double z = delta.Z;

            delta.X = Math.Cos(angle) * x - Math.Sin(angle) * z;
            delta.Z = Math.Sin(angle) * x + Math.Cos(angle) * z;

            delta += point;
            return delta;
        }

        /// <summary>
        /// Rolls the point about the x coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to roll.</param>
        /// <param name="point">Point to roll about.</param>
        public static Point RollX(Point delta, double angle, Point point)
        {
            delta -= point;

            double y = delta.Y;
            double z = delta.Z;

            delta.Y = Math.Cos(angle) * y + Math.Sin(angle) * z;
            delta.Z = -Math.Sin(angle) * y + Math.Cos(angle) * z;

            delta += point;
            return delta;
        }

        /// <summary>
        /// Yaws the point about the z coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to yaw.</param>
        /// <param name="point">Point to yaw about.</param>
        public static Point YawZ(Point delta, double angle, Point point)
        {
            delta -= point;

            double x = delta.X;
            double y = delta.Y;

            delta.X = Math.Cos(angle) * x + Math.Sin(angle) * y;
            delta.Y = -Math.Sin(angle) * x + Math.Cos(angle) * y;

            delta += point;
            return delta;
        }
    }
}