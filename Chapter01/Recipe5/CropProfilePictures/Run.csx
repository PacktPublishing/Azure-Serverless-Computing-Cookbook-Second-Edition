using ImageResizer;
 
        public static void Run(
          Stream image,              Stream imageSmall, Stream imageMedium) 
          {
            var imageBuilder = ImageResizer.ImageBuilder.Current;
            var size = imageDimensionsTable[ImageSize.Small];
            imageBuilder.Build(image, imageSmall, new ResizeSettings
             (size.Item1, size.Item2, FitMode.Max, null), false);
            image.Position = 0;
            size = imageDimensionsTable[ImageSize.Medium];
            imageBuilder.Build(image, imageMedium, new ResizeSettings
             (size.Item1, size.Item2, FitMode.Max, null), false);
          }

        public enum ImageSize
        {
            Small, Medium
        }

        private static Dictionary<ImageSize, Tuple<int, int>> 
         imageDimensionsTable = new Dictionary<ImageSize, Tuple<int,         
         int>>()
        {
            { ImageSize.Small, Tuple.Create(100, 100) },
            { ImageSize.Medium, Tuple.Create(200, 200) }
        };