using CoreImage;
using FFImageLoading.Work;
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
            var effect = new CIPhotoEffectMono() { InputImage = sourceBitmap.CGImage };
            var output = effect.OutputImage;
            var context = CIContext.FromOptions(null);
            CoreGraphics.CGImage cgimage = context.CreateCGImage(output, output.Extent);
            return UIImage.FromImage(cgimage);
        }
    }
}
