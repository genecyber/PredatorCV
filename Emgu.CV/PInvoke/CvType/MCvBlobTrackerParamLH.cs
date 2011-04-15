using System;
using System.Runtime.InteropServices;

namespace Emgu.CV.Structure
{
   /// <summary>
   /// Some declarations for specific likelihood tracker
   /// </summary>
   [StructLayout(LayoutKind.Sequential)]
   public struct MCvBlobTrackerParamLH
   {
      /// <summary>
      /// see Prob.h 
      /// </summary>
      public int HistType; 
      /// <summary>
      /// 
      /// </summary>
      public int ScaleAfter;
   }
}
