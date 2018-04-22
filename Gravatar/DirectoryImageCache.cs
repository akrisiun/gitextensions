using System;
using System.Drawing;
using System.IO;
// using System.IO.Abstractions;
using System.Threading.Tasks;

namespace Gravatar
{
    public interface IImageCache2
    {
        // Occurs whenever the cache is invalidated.
        event EventHandler Invalidated;

        // Adds the image to the cache from the supplied stream.
        Task AddImageAsync(string imageFileName, Stream imageStream);

        // Clears the cache by deleting all images.
        Task ClearAsync();

        // Deletes the specified image from the cache.
        Task DeleteImageAsync(string imageFileName);

        // Retrieves the image from the cache.
        Image GetImage(string imageFileName, Bitmap defaultBitmap);

        // Retrieves the image from the cache.
        Task<Image> GetImageAsync(string imageFileName, Bitmap defaultBitmap);

        void ClearCache();
        bool FileIsCached(string imageFileName);
        bool FileIsExpired(string imageFileName, int cacheDays);
        void DeleteCachedFile(string imageFileName);
        void CacheImage(string imageFileName, Stream imageStream);
        Image LoadImageFromCache(string imageFileName, Bitmap data);
    }

    public sealed class DirectoryImageCache : IImageCache2
    {
        private const int DefaultCacheDays = 30;
        private readonly string _cachePath;
        private readonly int _cacheDays;

        public DirectoryImageCache(string cachePath, int? cacheDays = null) // , IFileSystem fileSystem)
        {
            _cachePath = cachePath;
            // _fileSystem = fileSystem;
            _cacheDays = cacheDays ?? DefaultCacheDays;
            if (_cacheDays < 1)
            {
                _cacheDays = DefaultCacheDays;
            }
        }

#if IOABSTRACT
        // private readonly IFileSystem _fileSystem;

        public DirectoryImageCache(string cachePath, int? cacheDays, IFileSystem fileSystem)
        {
            _cachePath = cachePath;
            _fileSystem = fileSystem;
            _cacheDays = cacheDays ?? DefaultCacheDays;
            if (_cacheDays < 1)
            {
                _cacheDays = DefaultCacheDays;
            }
        }

        public DirectoryImageCache(string cachePath, int cacheDays)
            : this(cachePath, cacheDays, new FileSystem())
        {
        }
#endif
        /// <summary>
        /// Occurs whenever the cache is invalidated.
        /// </summary>
        public event EventHandler Invalidated;

        /// <summary>
        /// Adds the image to the cache from the supplied stream.
        /// </summary>
        /// <param name="imageFileName">The image file name.</param>
        /// <param name="imageStream">The stream which contains the image.</param>
        public async Task AddImageAsync(string imageFileName, Stream imageStream)
        {
            if (string.IsNullOrWhiteSpace(imageFileName) || imageStream == null)
            {
                return;
            }

            if (!Directory.Exists(_cachePath))
            {
                Directory.CreateDirectory(_cachePath);
            }

            try
            {
                string file = Path.Combine(_cachePath, imageFileName);
                using (var output = new FileStream(file, FileMode.Create))
                {
                    await imageStream.CopyToAsync(output);
                }
            }
            catch
            {
                // do nothing
            }

            OnInvalidated(EventArgs.Empty);
        }

        public void CacheImage(string imageFileName, Stream imageStream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears the cache by deleting all images.
        /// </summary>
        public async Task ClearAsync()
        {
            if (!Directory.Exists(_cachePath))
            {
                return;
            }

            await Task.Run(() =>
            {
                foreach (var file in Directory.GetFiles(_cachePath))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            });
            OnInvalidated(EventArgs.Empty);
        }

        public void ClearCache()
        {
            throw new NotImplementedException();
        }

        public void DeleteCachedFile(string imageFileName)
        {
            DeleteImageAsync(imageFileName).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes the specified image from the cache.
        /// </summary>
        /// <param name="imageFileName">The image file name.</param>
        public async Task DeleteImageAsync(string imageFileName)
        {
            if (string.IsNullOrWhiteSpace(imageFileName))
            {
                return;
            }

            string file = Path.Combine(_cachePath, imageFileName);
            if (!File.Exists(file))
            {
                return;
            }

            try
            {
                await Task.Run(() => File.Delete(file));
            }
            catch
            {
                // do nothing
            }

            OnInvalidated(EventArgs.Empty);
        }

        public bool FileIsCached(string imageFileName)
        {
            return false;
        }

        public bool FileIsExpired(string imageFileName, int cacheDays)
        {
            return HasExpired(imageFileName);
        }

        public Image LoadImageFromCache(string imageFileName, Bitmap data) => GetImage(imageFileName, data);

        // Retrieves the image from the cache.
        public Image GetImage(string imageFileName, Bitmap defaultBitmap)
        {
            if (string.IsNullOrWhiteSpace(imageFileName))
            {
                return null;
            }

            string file = Path.Combine(_cachePath, imageFileName);
            try
            {
                if (HasExpired(file))
                {
                    return null;
                }

                using (Stream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    return Image.FromStream(fileStream);
                }
            }
            catch
            {
                return null;
            }
        }

        // Retrieves the image from the cache.
        public async Task<Image> GetImageAsync(string imageFileName, Bitmap defaultBitmap)
        {
            return await Task.Run(() => GetImage(imageFileName, defaultBitmap));
        }

        private bool HasExpired(string fileName)
        {
            // var file = _fileSystem.FileInfo.FromFileName(fileName);
            var file = Path.GetFullPath(fileName);
            if (!File.Exists(file))
            {
                return true;
            }

            var fi = new FileInfo(fileName);
            return fi.LastWriteTime < DateTime.Now.AddDays(-_cacheDays);
        }

        private void OnInvalidated(EventArgs e)
        {
            var handler = Invalidated;
            handler?.Invoke(this, e);
        }
    }
}
