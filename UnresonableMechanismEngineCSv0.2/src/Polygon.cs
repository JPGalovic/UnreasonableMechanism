﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace UnreasonableMechanismEngineCS
{
    /// <summary>
    /// Polygon defines 2D objects made up of an equal number of vertices (points) and edges (vectors).
    /// </summary>
    public class Polygon
    {
        private List<Point> _vertices;
        private List<Vector> _edges;

        /// <summary>
        /// Constructs an empty polygon.
        /// </summary>
        public Polygon()
        {
            _vertices = new List<Point>();
            _edges = new List<Vector>();
        }

        /// <summary>
        /// Constructs a polygon using an array of vertices.
        /// </summary>
        /// <param name="vertices">Array of vertices (point).</param>
        public Polygon(Point[] vertices)
        {
            _vertices = new List<Point>(vertices);
            _edges = new List<Vector>();
            BuildEdges();
        }

        /// <summary>
        /// Constructs a polygon using an array of vertices (point) and location.
        /// </summary>
        /// <param name="vertices">Array of vertices (point).</param>
        /// <param name="location">Location (point).</param>
        public Polygon(Point[] vertices, Point location)
        {
            _vertices = new List<Point>(vertices);
            _edges = new List<Vector>();
            BuildEdges();
            Offset(new Vector(location, Center));
        }

        /// <summary>
        /// Readonly Property: Center point of polygon (calculated).
        /// </summary>
        public Point Center
        {
            get
            {
                Point result = new Point();
                foreach(Point vertex in _vertices)
                {
                    result += vertex;
                }
                return result / _vertices.Count;
            }
        }

        /// <summary>
        /// Readonly Property: List of edges (vector).
        /// </summary>
        public List<Vector> Edges
        {
            get
            {
                return _edges;
            }
        }

        /// <summary>
        /// Readonly Property: List of vertices (point).
        /// </summary>
        public List<Point> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        private void BuildEdges()
        {
            _edges.Clear();
            for (int i = 0; i < _vertices.Count; ++i)
            {
                if (i + 1 >= _vertices.Count)
                {
                    _edges.Add(new Vector(_vertices[0], _vertices[i]));
                }
                else
                {
                    _edges.Add(new Vector(_vertices[i + 1], _vertices[i]));
                }
            }
        }

        /// <summary>
        /// Draws the polygon, face, edges and vertives.
        /// </summary>
        /// <param name="clrFace">Colour to draw face.</param>
        /// <param name="clrEdge">Colour to draw edges.</param>
        /// <param name="clrVertex">Color to draw vertices.</param>
        public void Draw(Color clrFace, Color clrEdge, Color clrVertex)
        {
            DrawFace(clrFace);
            DrawEdge(clrEdge);
            DrawVertex(clrVertex);
        }

        /// <summary>
        /// Draws the polygon, face and edges.
        /// </summary>
        /// <param name="clrFace">Colour to draw face.</param>
        /// <param name="clrEdge">Colour to draw edges.</param>
        public void Draw(Color clrFace, Color clrEdge)
        {
            DrawFace(clrFace);
            DrawEdge(clrEdge);
        }

        /// <summary>
        /// Draws the polygon, face.
        /// </summary>
        /// <param name="clrFace">Colour to draw face.</param>
        public void Draw(Color clrFace)
        {
            DrawFace(clrFace);
        }

        /// <summary>
        /// Draws polygon edges.
        /// </summary>
        /// <param name="clr">Colour to draw edges.</param>
        public void DrawEdge(Color clr)
        {
            for(int i = 0; i < _vertices.Count; i++)
            {
                _edges[i].Draw(clr, _vertices[i]);
            }
        }

        /// <summary>
        /// Draws polygon face.
        /// </summary>
        /// <param name="clr">Colour to draw face.</param>
        public void DrawFace(Color clr)
        {
            for(int i = 0; i < _vertices.Count; i++)
            {
                float x = (float)Center.X;
                float y = (float)Center.Y;

                float x1 = (float)_vertices[i].X;
                float y1 = (float)_vertices[i].Y;

                float x2, y2;

                if (i + 1 >= _vertices.Count)
                {
                    x2 = (float)_vertices[0].X;
                    y2 = (float)_vertices[0].Y;
                }
                else
                {
                    x2 = (float)_vertices[i + 1].X;
                    y2 = (float)_vertices[i + 1].Y;
                }

                SwinGame.FillTriangle(clr, x, y, x1, y1, x2, y2);
            }
        }

        /// <summary>
        /// Draws polygon vertices.
        /// </summary>
        /// <param name="clr">Color to draw vertices.</param>
        public void DrawVertex(Color clr)
        {
            foreach(Point vertex in _vertices)
            {
                vertex.Draw(clr);
            }
        }

        /// <summary>
        /// Offsets the polygon by the given movment vector.
        /// </summary>
        /// <param name="movement">Movement vector.</param>
        public void Offset(Vector movement)
        {
            for(int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i].Offset(movement);
            }
        }

        /// <summary>
        /// Pitches the polygon about the y coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to pitch.</param>
        /// <param name="point">Point to pitch about.</param>
        public void PitchY(double angle, Point point)
        {
            for(int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i].PitchY(angle, point);
            }
            BuildEdges();
        }

        /// <summary>
        /// Rolls the polygon about the x coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to roll.</param>
        /// <param name="point">Point to roll about.</param>
        public void RollX(double angle, Point point)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i].RollX(angle, point);
            }
            BuildEdges();
        }

        /// <summary>
        /// Scales the polygon about the given point.
        /// </summary>
        /// <param name="scale">Scale.</param>
        /// <param name="center">Point to scale about.</param>
        public void Scale(double scale, Point point)
        {
            for(int i = 0; i < _vertices.Count; i++)
            {
                Point working = _vertices[i] - point;

                working = working * scale;

                _vertices[i] = working + point;
            }
            BuildEdges();
        }

        /// <summary>
        /// Yaws the polygon about the z coordinate of the given point.
        /// </summary>
        /// <param name="angle">Angle to yaw.</param>
        /// <param name="point">Point to yaw about.</param>
        public void YawZ(double angle, Point point)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i].YawZ(angle, point);
            }
            BuildEdges();
        }
    }
}