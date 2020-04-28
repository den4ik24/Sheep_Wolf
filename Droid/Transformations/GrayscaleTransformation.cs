using System;
using Android.Graphics;
using Square.Picasso;

namespace Sheep_Wolf.Droid
{
    public class GrayscaleTransformation :Java.Lang.Object, ITransformation
    {
        public Bitmap Transform(Bitmap source)
        {
            int width = source.Width;
            int height = source.Height;

            Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(bitmap);
            ColorMatrix saturation = new ColorMatrix();
            saturation.SetSaturation(0f);
            Paint paint = new Paint();
            paint.SetColorFilter(new ColorMatrixColorFilter(saturation));
            canvas.DrawBitmap(source, 0, 0, paint);
            source.Recycle();
            return bitmap;
        }
        public string Key => "GrayscaleTransformation";
    }
}
//ColorMatrix matrix = new ColorMatrix();
//matrix.SetSaturation(0);
//ColorMatrixColorFilter filter = new ColorMatrixColorFilter(matrix);
//animalsFoto.SetColorFilter(filter);