/* CSE 381 - Convex Hull (Graham Scan)
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Orientation, GetAngle, GetDist, and
*  GenerateHull functions per the instructions in the comments.  
*  Run all tests in ConvexHullTest.cs to verify your code.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLib;

public static class ConvexHull
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        // Constructor to allow initialization with (x, y)
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Point other)
                return false;

            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public enum Angle
    {
        Colinear,
        Convex,
        Concave
    }

    public static List<Point> GenerateHull(List<Point> points)
    {
        if (points.Count < 3)
            return new List<Point>();

        // Step 1: Find the anchor point (lowest Y, then leftmost X)
        Point anchor = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();

        // Step 2: Sort points by polar angle and distance from anchor
        var sortedPoints = points
            .OrderBy(p => GetAngle(anchor, p))
            .ThenBy(p => GetDist(anchor, p))
            .ToList();

        // Step 3: Process points to generate the convex hull using a stack
        Stack<Point> hull = new();
        hull.Push(sortedPoints[0]);
        hull.Push(sortedPoints[1]);

        for (int i = 2; i < sortedPoints.Count; i++)
        {
            Point top = hull.Pop();
            while (hull.Count > 0 &&
                   Orientation(hull.Peek(), top, sortedPoints[i]) != Angle.Convex)
            {
                top = hull.Pop();
            }
            hull.Push(top);
            hull.Push(sortedPoints[i]);
        }

        // Step 4: Filter out unnecessary collinear points
        var finalHull = hull.ToList();
        return RemoveInteriorCollinearPoints(finalHull);
    }

    private static List<Point> RemoveInteriorCollinearPoints(List<Point> hull)
    {
        var filteredHull = new List<Point>();

        for (int i = 0; i < hull.Count; i++)
        {
            Point prev = hull[(i - 1 + hull.Count) % hull.Count];
            Point curr = hull[i];
            Point next = hull[(i + 1) % hull.Count];

            // Check if current point is collinear with its neighbors
            if (Orientation(prev, curr, next) != Angle.Colinear)
            {
                filteredHull.Add(curr);
            }
            else
            {
                // Include the farthest point only
                if (GetDist(prev, curr) > GetDist(prev, next))
                    filteredHull.Add(curr);
            }
        }

        return filteredHull;
    }

    private static double GetAngle(Point a, Point b)
    {
        return Math.Atan2(b.Y - a.Y, b.X - a.X);
    }

    private static double GetDist(Point a, Point b)
    {
        return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
    }

    // Changed to public for accessibility in tests
    public static Angle Orientation(Point a, Point b, Point c)
    {
        // Calculate the cross product
        int crossProduct = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);

        if (Math.Abs(crossProduct) < 0.001) return Angle.Colinear;
        return crossProduct > 0 ? Angle.Convex : Angle.Concave;
    }
}
