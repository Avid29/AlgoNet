// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic.Kernels
{
    /// The shape of a Gaussian Distribution
    /// 
    ///                *
    ///              *   *
    ///             *     *
    ///            *       *
    ///           *         *
    ///        *               *
    ///  *                           *
    ///  -----------------------------

    /// <summary>
    /// A Kernel with a gaussian falloff.
    /// </summary>
    public struct GaussianKernel<T> : IKernel<T>
        where T : IFloatingPoint<T>
    {
        private T _denominatorBandwidth;
        private T _bandwidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianKernel{T}"/> struct.
        /// </summary>
        /// <param name="bandwidth">The bandwidth of the <see cref="GaussianKernel{T}"/>.</param>
        public GaussianKernel(T bandwidth)
        {
            // These will be set in WindowSize
            _bandwidth = T.Zero;
            _denominatorBandwidth = T.Zero;

            WindowSize = bandwidth;
        }

        /// <inheritdoc/>
        public T WindowSize
        {
            get => _bandwidth;
            set
            {
                _bandwidth = value;
                _denominatorBandwidth = value * value * T.Create(-2);
            }
        }

        /// <inheritdoc/>
        public T WeightDistance(T distanceSquared)
        {
            // Unoptimized equivilent.
            // return T.Pow(T.Create(Math.E), -.5 * distanceSquared / (_bandwidth * _bandwidth);

            return T.Exp(distanceSquared / _denominatorBandwidth);
        }
    }
}
