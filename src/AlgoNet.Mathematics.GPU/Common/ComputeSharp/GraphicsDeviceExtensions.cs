// Adam Dernis © 2022

using Microsoft.Toolkit.HighPerformance;

namespace ComputeSharp
{
    internal static class GraphicsDeviceExtensions
    {
        internal static ReadOnlyBuffer<T> AllocateReadOnlyBuffer<T>(this GraphicsDevice device, ReadOnlySpan2D<T> source)
            where T : unmanaged
        {
            ReadOnlyBuffer<T> buffer = device.AllocateReadOnlyBuffer<T>(source.Width * source.Height);
            buffer.CopyFrom(source);
            return buffer;
        }

        internal static ReadWriteBuffer<T> AllocateReadWriteBuffer<T>(this GraphicsDevice device, ReadOnlySpan2D<T> source)
            where T : unmanaged
        {
            ReadWriteBuffer<T> buffer = device.AllocateReadWriteBuffer<T>(source.Width * source.Height);
            buffer.CopyFrom(source);
            return buffer;
        }
        internal static ReadOnlyTexture2D<T> AllocateReadOnlyTexture2D<T>(this GraphicsDevice device, ReadOnlySpan2D<T> source)
            where T : unmanaged
        {
            ReadOnlyTexture2D<T> texture = device.AllocateReadOnlyTexture2D<T>(source.Width, source.Height);
            texture.CopyFrom(source);
            return texture;
        }

        internal static ReadWriteTexture2D<T> AllocateReadWriteTexture2D<T>(this GraphicsDevice device, ReadOnlySpan2D<T> source)
            where T : unmanaged
        {
            ReadWriteTexture2D<T> texture = device.AllocateReadWriteTexture2D<T>(source.Width, source.Height);
            texture.CopyFrom(source);
            return texture;
        }
    }
}
