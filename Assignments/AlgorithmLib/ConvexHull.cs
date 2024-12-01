/* CSE 381 - Convex Hull (Graham Scan)
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Orientation, GetAngle, GetDist, and
*  GenerateHull functions per the instructions in the comments.  
*  Run all tests in ConvexHullTest.cs to verify your code.
*/
namespace AlgorithmLib
{
    // The ConvexHull class contains methods for computing the convex hull of a set of points using the Graham Scan algorithm.
    public static class ConvexHull
    {
        /* Valid angle types used in the code */
        public enum Angle
        {
            Convex,    // Angle formed by points is convex (counter-clockwise turn)
            Concave,   // Angle formed by points is concave (clockwise turn)
            Colinear   // Points are collinear (lie on the same straight line)
        }

        /* Representation of a 2D point with X and Y coordinates */
        public class Point
        {
            public readonly double X;   // X-coordinate of the point
            public readonly double Y;   // Y-coordinate of the point
            
            // Constructor to initialize point coordinates
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            // This method allows comparing two points to check if they are nearly equal (within a small tolerance)
            public bool Equals(Point point)
            {
                return Math.Abs(X - point.X) < 0.001 && 
                       Math.Abs(Y - point.Y) < 0.001;
            }
        }

        /* 
         * Determines whether the angle formed by three points is convex, concave, or collinear.
         * This is used in Graham Scan to check if we need to make a left or right turn.
         */
        public static Angle Orientation(Point a, Point b, Point c)
        {
            // Cross product to determine the orientation
            double crossProduct = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);

            // If the cross product is positive, the angle is convex (counter-clockwise)
            if (crossProduct > 0)
                return Angle.Convex;
            // If negative, the angle is concave (clockwise)
            else if (crossProduct < 0)
                return Angle.Concave;
            // If zero, the points are collinear (lie on the same line)
            else
                return Angle.Colinear;
        }

        /* 
         * Calculates the angle between the anchor point and another point using the arctangent function.
         * The angle is measured in radians relative to the anchor.
         */
        public static double GetAngle(Point anchor, Point point)
        {
            // atan2 calculates the angle between the positive X-axis and the line to the point (x, y)
            return Math.Atan2(point.Y - anchor.Y, point.X - anchor.X);
        }

        /* 
         * Calculates the Euclidean distance between two points.
         * This is used in sorting points by distance from the anchor during the convex hull generation.
         */
        public static double GetDist(Point anchor, Point point)
        {
            // Distance formula: sqrt((x2 - x1)^2 + (y2 - y1)^2)
            double dx = point.X - anchor.X;
            double dy = point.Y - anchor.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /* 
         * Generates the Convex Hull for a given set of points.
         * Implements the Graham Scan algorithm for convex hull construction.
         * Returns a list of points that form the convex hull.
         */
        public static List<Point> GenerateHull(List<Point> points)
        {
            // If there are fewer than 3 points, no hull can be formed
            if (points.Count < 3)
                return new List<Point>();

            // Step 1: Find the point with the lowest Y value (and lowest X if there's a tie)
            Point anchor = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();

            // Step 2: Sort the points by polar angle relative to the anchor
            var sortedPoints = points
                .Where(p => p != anchor)          // Remove the anchor point from the list
                .OrderBy(p => GetAngle(anchor, p)) // Sort by angle
                .ThenBy(p => GetDist(anchor, p))  // If angles are the same, sort by distance
                .ToList();

            // Step 3: Insert the anchor point back to the sorted list at the start
            sortedPoints.Insert(0, anchor);

            // Step 4: Initialize the convex hull with the anchor point
            List<Point> hull = new List<Point> { anchor };

            // Step 5: Iterate through the sorted points and build the hull
            foreach (var point in sortedPoints.Skip(1))  // Skip the anchor itself
            {
                // Check the orientation of the last two points in the hull and the current point
                // If it's concave (clockwise turn), remove the last point from the hull
                while (hull.Count > 1 && Orientation(hull[hull.Count - 2], hull[hull.Count - 1], point) != Angle.Convex)
                {
                    hull.RemoveAt(hull.Count - 1); // Remove the point that doesn't form a convex angle
                }
                hull.Add(point); // Add the current point to the hull
            }

            // Step 6: Ensure the hull is closed by adding the anchor point to the end of the list
            if (!hull[0].Equals(hull[hull.Count - 1]))  // Check if the first and last points are the same
            {
                hull.Add(hull[0]); // Close the hull by adding the first point again
            }

            // Return the list of points that form the convex hull
            return hull;
        }
    }
}