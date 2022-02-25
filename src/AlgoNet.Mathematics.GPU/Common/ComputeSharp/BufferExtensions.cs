// Adam Dernis © 2022

using ComputeSharp.Resources;
using Microsoft.Toolkit.HighPerformance;
using System;

namespace ComputeSharp
{
    internal static class BufferExtensions
    {
#if NETSTANDARD2_0
        internal static unsafe void CopyTo<T>(this StructuredBuffer<T> source, Span2D<T> destination)
#else
        internal static void CopyTo<T>(this StructuredBuffer<T> source, Span2D<T> destination)
#endif
            where T : unmanaged
        {

            // If the source can be expressed in a Span, copy direclty.
            if (destination.TryGetSpan(out var span))
            {
                source.CopyTo(span);
            }

            // The source is too large to be expressed as a single Span.
            // Copy row by row to a read back texture
            GraphicsDevice device = source.GraphicsDevice;
            ReadBackBuffer<T> readBack = device.AllocateReadBackBuffer<T>(destination.Width * destination.Height);
            for (int i = 0; i < destination.Height; i++)
            {
#if NETSTANDARD2_0
                ref T r0 = ref destination.DangerousGetReferenceAt(i, 0);
                fixed (T* p = &r0)
                {
                    Span<T> row = new(p, destination.Width);
                    source.CopyTo(row, i * destination.Width);
                }
#else
                source.CopyTo(destination.GetRowSpan(i), i * destination.Width);
#endif
            }
        }

#if NETSTANDARD2_0
        internal static unsafe void CopyFrom<T>(this StructuredBuffer<T> destination, ReadOnlySpan2D<T> source)
#else
        internal static void CopyFrom<T>(this StructuredBuffer<T> destination, ReadOnlySpan2D<T> source)
#endif
            where T : unmanaged
        {
            // If the source can be expressed in a Span, copy direclty.
            if (source.TryGetSpan(out var span))
            {
                destination.CopyFrom(span);
            }

            // The source is too large to be expressed as a single Span.
            // Copy row by row to an upload texture
            GraphicsDevice device = destination.GraphicsDevice;
            UploadBuffer<T> upload = device.AllocateUploadBuffer<T>(source.Width * source.Height);
            for (int i = 0; i < source.Height; i++)
            {
#if NETSTANDARD2_0
                ref T r0 = ref source.DangerousGetReferenceAt(i, 0);
                fixed (T* p = &r0)
                {
                    ReadOnlySpan<T> row = new(p, source.Width);
                    row.CopyTo(upload.Span.Slice(i * source.Width));
                }
#else
                source.GetRowSpan(i).CopyTo(upload.Span.Slice(i * source.Width));
#endif
            }

            upload.CopyTo(destination);
        }

#if NETSTANDARD2_0
        internal static unsafe void CopyTo<T>(this Texture2D<T> source, Span2D<T> destination)
#else
        internal static void CopyTo<T>(this Texture2D<T> source, Span2D<T> destination)
#endif
            where T : unmanaged
        {

            // If the source can be expressed in a Span, copy direclty.
            if (destination.TryGetSpan(out var span))
            {
                source.CopyTo(span);
            }


            // The source is too large to be expressed as a single Span.
            // Copy row by row to a read back texture
            GraphicsDevice device = source.GraphicsDevice;
            ReadBackTexture2D<T> readBack = device.AllocateReadBackTexture2D<T>(destination.Width, destination.Height);
            for (int i = 0; i < source.Height; i++)
            {
#if NETSTANDARD2_0
                ref T r0 = ref destination.DangerousGetReferenceAt(i, 0);
                fixed (T* p = &r0)
                {
                    Span<T> row = new(p, destination.Width);
                    source.CopyTo(row, i, 0, destination.Width, 1);
                }
#else
                source.CopyTo(destination.GetRowSpan(i), i, 0, destination.Width, 1);
#endif
            }
        }

#if NETSTANDARD2_0
        internal static unsafe void CopyFrom<T>(this Texture2D<T> destination, ReadOnlySpan2D<T> source)
#else
        internal static void CopyFrom<T>(this Texture2D<T> destination, ReadOnlySpan2D<T> source)
#endif
            where T : unmanaged
        {
            // If the source can be expressed in a Span, copy direclty.
            if (source.TryGetSpan(out var span))
            {
                destination.CopyFrom(span);
            }

            // The source is too large to be expressed as a single Span.
            // Copy row by row to an upload texture
            GraphicsDevice device = destination.GraphicsDevice;
            UploadTexture2D<T> upload = device.AllocateUploadTexture2D<T>(source.Width, source.Height);
            for (int i = 0; i < source.Height; i++)
            {
#if NETSTANDARD2_0
                ref T r0 = ref source.DangerousGetReferenceAt(i, 0);
                fixed (T* p = &r0)
                {
                    ReadOnlySpan<T> row = new(p, source.Width);
                    row.CopyTo(upload.View.GetRowSpan(i));
                }
#else
                source.GetRowSpan(i).CopyTo(upload.View.GetRowSpan(i));
#endif
            }

            upload.CopyTo(destination);
        }
    }
}
