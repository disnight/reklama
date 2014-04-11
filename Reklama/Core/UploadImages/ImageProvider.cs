using System;
using Domain.Enums;
using Domain.Repository.Announcements;
using Domain.Repository.Realty;
using Reklama.Models.Announcements;
using Reklama.Models.Realty;

namespace Reklama.Core.UploadImages
{
    public class ImageProvider
    {
        private readonly IAnnouncementRepository _announcement = new AnnouncementModel();
        private readonly IAnnouncementImageRepository _announcementImage = new AnnouncementImageModel();
        private readonly IRealtyRepository _realty = new RealtyModel();
        private readonly IRealtyPhotoRepository _realtyImage = new RealtyPhotoModel();

        public static string PublicAnouncementImagesPath
        {
            get
            {
                return "/" + ProjectConfiguration.Get.FilePaths["users"];
            }
        }

        public static string PublicRealtyImagesPath
        {
            get
            {
                return "/" + ProjectConfiguration.Get.FilePaths["realty"];
            }
        }

        public static string AnouncementImagesPath
        {
            get
            {
                return ProjectConfiguration.Get.FilePaths["base"] +
                                            ProjectConfiguration.Get.FilePaths["users"];
            }
        }

        public static string ThumbAnouncementImagesPath
        {
            get
            {
                return ProjectConfiguration.Get.FilePaths["base"] +
                                              ProjectConfiguration.Get.FilePaths["announcement_thumb"];
            }
        }

        public static string RealtyImagesPath
        {
            get
            {
                return ProjectConfiguration.Get.FilePaths["base"] +
                                    ProjectConfiguration.Get.FilePaths["realty"];
            }
        }

        public static string ThumbRealtyImagesPath
        {
            get
            {
                return ProjectConfiguration.Get.FilePaths["base"] +
                                         ProjectConfiguration.Get.FilePaths["realty_thumb"];
            }
        }

        public bool RemoveImage(string imageName, ImagesType type)
        {
            try
            {
                var path = "";
                var thumbPath = "";

                switch (type)
                {
                    case ImagesType.Anouncement:
                        path = AnouncementImagesPath + imageName;
                        thumbPath = ThumbAnouncementImagesPath + imageName;
                        break;
                    case ImagesType.Realty:
                        path = RealtyImagesPath + imageName;
                        thumbPath = ThumbRealtyImagesPath + imageName;
                        break;
                }
                System.IO.File.Delete(path);
                System.IO.File.Delete(thumbPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveImage(string announcementId, string imageName, ImagesType type)
        {
            var id = Convert.ToInt32(announcementId);

            try
            {
                switch (type)
                {
                    case ImagesType.Anouncement:
                        var announcement = _announcement.Read(id);
                        if (announcement == null)
                        {
                            throw new Exception();
                        }
                        _announcementImage.DeleteImage(id, imageName);
                        break;

                    case ImagesType.Realty:
                        var realty = _realty.Read(id);
                        if (realty == null)
                        {
                            throw new Exception();
                        }
                        _realtyImage.DeleteImage(id, imageName);
                        break;
                }

                RemoveImage(imageName, type);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}