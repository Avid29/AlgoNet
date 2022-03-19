// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic.Kernels
{
    /// The shape of a Uniform Distribution
    ///
    /// 
    ///        * * * * * * * * *
    ///        *               *
    ///        *               *
    ///        *               *
    ///        *               *
    ///  -----------------------------

    /// <summary>
    /// A Kernel with a rectangular shape and flat cutoff on its window size.
    /// </summary>
    public struct UniformKernel<T> : IKernel<T>
        where T : IFloatingPoint<T>
    {
        private T _windowSquared;
        private T _window;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformKernel{T}"/> struct.
        /// </summary>
        /// <param name="window">The window size of the Kernel.</param>
        public UniformKernel(T window)
        {
            // These will be set in WindowSize
            _window = T.Zero;
            _windowSquared = T.Zero;

            WindowSize = window;
        }

        /// <inheritdoc/>
        public T WindowSize
        {
            get => _window;
            set
            {
                _window = value;
                _windowSquared = value * value;
            }
        }

        /// <inheritdoc/>
        public T WeightDistance(T distanceSquared)
        {
            return distanceSquared < _windowSquared ? T.One : T.Zero;
        }
    }
}
