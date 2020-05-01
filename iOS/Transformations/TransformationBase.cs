using FFImageLoading.Work;
using PImage = UIKit.UIImage;

namespace Sheep_Wolf.iOS
{
    public abstract class TransformationBase : ITransformation
    {
        public abstract string Key { get; }

        public IBitmap Transform(IBitmap bitmapHolder, string path, ImageSource source, bool isPlaceholder, string key)
        {
            var sourceBitmap = bitmapHolder.ToNative();
            return new BitmapHolder(Transform(sourceBitmap, path, source, isPlaceholder, key));
        }

        protected virtual PImage Transform(PImage sourceBitmap, string path, ImageSource source, bool isPlaceholder, string key)
        {
            return sourceBitmap;
        }
    }
}
