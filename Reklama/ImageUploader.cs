using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Reklama
{
    public class ImageUploader: FileUploader
    {
        private Bitmap _image;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public CompositingQuality Quality { get; set; }
        public InterpolationMode Interpolation { get; set; }
        public CompositingMode Mode { get; set; }

        public ImageUploader(HttpPostedFileBase file) : base(file)
        {
            try
            {
                _image = new Bitmap(file.InputStream);
            }
            catch(ArgumentException)
            {
                throw new HttpException(404, "Photo not found.");
            }

            Width = _image.Width;
            Height = _image.Height;
            Quality = CompositingQuality.HighSpeed;
            Interpolation = InterpolationMode.Default;
            Mode = CompositingMode.SourceCopy;
        }


        public void Convert(int maxWidth, int maxHeight)
        {
            if (Width > maxWidth)
            {
                float koef = (float)Width / maxWidth;
                Width = maxWidth;
                Height = (int)(Height / koef);
            }

            if (Height > maxHeight && Height <= 2*maxHeight)
            {
                float koef = (float) Height / maxHeight;
                Height = maxHeight;
                Width = (int) (Width / koef);
            }
            else if(Height > 2 * maxHeight)
            {
                Height = 2 * maxHeight;
            }

            var target = new Bitmap(Width, Height);
            using (Graphics graphics = Graphics.FromImage(target))
            {
                graphics.CompositingQuality = Quality;
                graphics.InterpolationMode = Interpolation;
                graphics.CompositingMode = Mode;
                graphics.DrawImage(_image, 0, 0, Width, Height);
            }

            _image = target;
        }


        public override void Save(string keypath)
        {
            var path = ProjectConfiguration.Get.FilePaths["base"] + ProjectConfiguration.Get.FilePaths[keypath];
            _image.Save(Path.Combine(path, UniqueName));
        }
    }
}