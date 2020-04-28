using System;
using CoreGraphics;
using CoreImage;
using FFImageLoading.Work;
using Foundation;
using UIKit;

namespace Sheep_Wolf.iOS
{
    public class GrayscaleTransformation :TransformationBase
    {
        public GrayscaleTransformation()
        {
        }

        public override string Key => "GrayscaleTransformation";

        protected override UIImage Transform(UIImage sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
        {
            using(var effect = new CIPhotoEffectMono() { Image = sourceBitmap.CGImage })
            using(var output = effect.OutputImage)
            using (var context = CIContext.FromOptions(null))
            using (var cgimage = context.CreateCGImage(output, output.Extent))
            {
                return UIImage.FromImage(cgimage);
            }
        }
    }
}
