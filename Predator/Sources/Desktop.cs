﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PredatorCV.Sources
{
    ///
    /// This class shall keep the GDI32 APIs used in our program.
    ///
    public class PlatformInvokeGDI32
    {
        #region Class Variables
        public const int SRCCOPY = 13369376;
        #endregion
        #region Class Functions

        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);

        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest,
                                         int yDest, int wDest,
                                         int hDest, IntPtr hdcSource,
                                         int xSrc, int ySrc, int RasterOp);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap
                                    (IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        #endregion
    }

    ///
    /// This class shall keep the User32 APIs used in our program.
    ///
    public class PlatformInvokeUSER32
    {
        #region Class Variables
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;
        #endregion

        #region Class Functions
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int abc);

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern IntPtr GetWindowDC(Int32 ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        #endregion
    }

    ///
    /// This class shall keep all the functionality for capturing
    /// the desktop. 
    /// <remarks>
    /// Created by Agha Ali Raza. 
    /// </remarks>
    public class Desktop : IVideo
    {
        #region Public Class Functions
        public Bitmap GetDesktopImage(int destX = 0, int destY = 0, int cx = -1, int cy = -1)
        {
            //In size variable we shall keep the size of the screen.
            SIZE size;


            //Variable to keep the handle to bitmap.
            IntPtr hBitmap;

            //Here we get the handle to the desktop device context.
            IntPtr hDC = PlatformInvokeUSER32.GetDC
                          (PlatformInvokeUSER32.GetDesktopWindow());

            //Here we make a compatible device context in memory for screen
            //device context.
            IntPtr hMemDC = PlatformInvokeGDI32.CreateCompatibleDC(hDC);

            if (cx == -1 & cy == -1)
            {
                //We pass SM_CXSCREEN constant to GetSystemMetrics to get the
                //X coordinates of the screen.
                size.cx = PlatformInvokeUSER32.GetSystemMetrics
                    (PlatformInvokeUSER32.SM_CXSCREEN);

                //We pass SM_CYSCREEN constant to GetSystemMetrics to get the
                //Y coordinates of the screen.
                size.cy = PlatformInvokeUSER32.GetSystemMetrics
                    (PlatformInvokeUSER32.SM_CYSCREEN);
            }
            else
            {
                size.cx = cx + destX;
                size.cy = cy + destY;
            }

            //We create a compatible bitmap of the screen size and using
            //the screen device context.
            hBitmap = PlatformInvokeGDI32.CreateCompatibleBitmap
                        (hDC, size.cx, size.cy);

            //As hBitmap is IntPtr, we cannot check it against null.
            //For this purpose, IntPtr.Zero is used.
            if (hBitmap != IntPtr.Zero)
            {
                //Here we select the compatible bitmap in the memeory device
                //context and keep the refrence to the old bitmap.
                IntPtr hOld = (IntPtr)PlatformInvokeGDI32.SelectObject
                                       (hMemDC, hBitmap);
                //We copy the Bitmap to the memory device context.
                PlatformInvokeGDI32.BitBlt(hMemDC, destX, destY, size.cx, size.cy, hDC,
                                           0, 0, PlatformInvokeGDI32.SRCCOPY);
                //We select the old bitmap back to the memory device context.
                PlatformInvokeGDI32.SelectObject(hMemDC, hOld);
                //We delete the memory device context.
                PlatformInvokeGDI32.DeleteDC(hMemDC);
                //We release the screen device context.
                PlatformInvokeUSER32.ReleaseDC(PlatformInvokeUSER32.
                                               GetDesktopWindow(), hDC);
                //Image is created by Image bitmap handle and stored in
                //local variable.
                Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                //Release the memory to avoid memory leaks.
                PlatformInvokeGDI32.DeleteObject(hBitmap);
                //This statement runs the garbage collector manually.
                GC.Collect();
                //Return the bitmap 
                return bmp;
            }
            //If hBitmap is null, retun null.
            return null;
        }
        #endregion

        public Image<Bgr, byte> QueryFrame()
        {
            var result = GetDesktopImage();
            return new Image<Bgr, byte>(result);
        }

        public Image<Bgr, byte> QueryFrame(int destX, int destY, int cx, int cy)
        {
            var result = GetDesktopImage(destX, destY, cx, cy);
            return new Image<Bgr, byte>(result);
        }
    }

    //This structure shall be used to keep the size of the screen.
    public struct SIZE
    {
        public int cx;
        public int cy;
    }
}
