﻿// Adam Dernis © 2021

using System.Numerics;

namespace AlgoNet.Tests.Gradients.Shape
{
    public struct Vector2GradientShape : IGradient<Vector2>
    {
        public int N => 2;

        public Vector2 For(double[] coords)
        {
            return new Vector2((float)coords[0], (float)coords[1]);
        }
    }
}
