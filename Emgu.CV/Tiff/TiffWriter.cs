﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Emgu.CV.Geodetic;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.Util;
using System.Drawing;
using System.Diagnostics;

namespace Emgu.CV.Tiff
{

   internal static partial class TIFFInvoke
   {
      #region PInvoke
      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      public extern static IntPtr tiffWriterOpen(
         [MarshalAs(CvInvoke.StringMarshalType)]
         string fileSpec);

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      public extern static void tiffWriterClose(ref IntPtr pTiff);

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      public extern static void tiffWriteGeoTag(IntPtr pTiff, IntPtr modelTiepoint, IntPtr ModelPixelScale);

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      public extern static void tiffWriteImage(IntPtr pTiff, IntPtr image);

      [DllImport(CvInvoke.EXTERN_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      public extern static void tiffWriteImageInfo(IntPtr pTiff, int bitsPerSample, int samplesPerPixel);
      #endregion
   }

   /// <summary>
   /// A class that can be used for writing geotiff
   /// </summary>
   /// <typeparam name="TColor">The color type of the image to be written</typeparam>
   /// <typeparam name="TDepth">The depth type of the image to be written</typeparam>
   public class TiffWriter<TColor, TDepth> : UnmanagedObject
      where TColor : struct, IColor
      where TDepth : new()
   {

      /// <summary>
      /// Create a tiff writer to save an image
      /// </summary>
      /// <param name="fileName">The file name to be saved</param>
      public TiffWriter(String fileName)
      {
         _ptr = TIFFInvoke.tiffWriterOpen(fileName);
         TIFFInvoke.tiffWriteImageInfo(_ptr, Image<TColor, TDepth>.SizeOfElement * 8, new TColor().Dimension);
      }

      /// <summary>
      /// Write the image to the tiff file
      /// </summary>
      /// <param name="image">The image to be written</param>
      public virtual void WriteImage(Image<TColor, TDepth> image)
      {
         if (image is Image<Gray, Byte> || image is Image<Rgb, Byte> || image is Image<Rgba, Byte>)
         {
            TIFFInvoke.tiffWriteImage(_ptr, image);
         }
         else if (image is Image<Bgra, Byte>)
         {
            //swap the B and R channel since geotiff assume RGBA for 4 channels image of depth Byte
            using (Image<Rgba, Byte> rgba = (image as Image<Bgra, Byte>).Convert<Rgba, Byte>())
            {
               TIFFInvoke.tiffWriteImage(_ptr, rgba);
            }
         }
         else if (image is Image<Bgr, Byte>)
         {
            //swap the B and R channel since geotiff assume RGB for 3 channels image of depth Byte
            using (Image<Rgb, Byte> rgb = (image as Image<Bgr, Byte>).Convert<Rgb, Byte>())
            {
               TIFFInvoke.tiffWriteImage(_ptr, rgb);
            }
         }
         else
         {
            throw new NotImplementedException("The specific image type is not supported");
         }
      }

      /// <summary>
      /// Write the geo information into the tiff file
      /// </summary>
      /// <param name="originCoordinate">The coordinate of the origin. To be specific, this is the coordinate of the upper left corner of the pixel in the origin</param>
      /// <param name="pixelResolution">The resolution of the pixel in meters</param>
      /// <param name="imageSize">The size of the image</param>
      public void WriteGeoTag(GeodeticCoordinate originCoordinate, Size imageSize, MCvPoint2D64f pixelResolution)
      {
         GeodeticCoordinate lowerRight = Datum.WGS84.NED2Geodetic(
            new MCvPoint3D64f(pixelResolution.x * imageSize.Height, pixelResolution.y * imageSize.Width, 0.0),
            originCoordinate);
         MCvPoint3D64f res = new MCvPoint3D64f(
            (lowerRight.Longitude - originCoordinate.Longitude) * (180.0 / Math.PI) / imageSize.Width,
            (lowerRight.Latitude - originCoordinate.Latitude) * (180.0 / Math.PI) / imageSize.Height,
            0.0);

         double latitude = lowerRight.Latitude * (180.0 / Math.PI);
         double longitude = lowerRight.Longitude * (180.0 / Math.PI);
         double[] modelTiepoint = { 
            0, 0, 0, 
            longitude, latitude, 0 };
         double[] modelPixelScale = { res.x, res.y, 0.0 };
         WriteGeoTag(modelTiepoint, modelPixelScale);
      }

      /// <summary>
      /// Write the geo information into the tiff file
      /// </summary>
      /// <param name="modelTiepoint">Model Tie Point, an array of size 6</param>
      /// <param name="modelPixelScale">Model pixel scale, an array of size 3</param>
      public void WriteGeoTag(double[] modelTiepoint, double[] modelPixelScale)
      {
         Debug.Assert(modelTiepoint.Length == 6, "Model Tiepoint should have length of 6");
         Debug.Assert(modelPixelScale.Length == 3, "Model Pixel Scale should have length of 3");

         GCHandle tiepointHandle = GCHandle.Alloc(modelTiepoint, GCHandleType.Pinned);
         GCHandle pixelScaleHandle = GCHandle.Alloc(modelPixelScale, GCHandleType.Pinned);

         TIFFInvoke.tiffWriteGeoTag(_ptr, tiepointHandle.AddrOfPinnedObject(), pixelScaleHandle.AddrOfPinnedObject());

         tiepointHandle.Free();
         pixelScaleHandle.Free();
      }

      /// <summary>
      /// Release the writer and write all data on to disk.
      /// </summary>
      protected override void DisposeObject()
      {
         TIFFInvoke.tiffWriterClose(ref _ptr);
      }
   }
}
