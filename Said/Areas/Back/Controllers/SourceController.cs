﻿using Said.Application;
using Said.Common;
using Said.Config;
using Said.Models;
using Said.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Said.Areas.Back.Controllers
{
    public class SourceController : BaseController
    {
        #region 配置

        /// <summary>
        /// 图片过滤
        /// </summary>
        readonly static Array IMGFILTERARRAY = ConfigTable.Get(ConfigEnum.ImgFilter).Split(',');
        /// <summary>
        /// 音乐文件过滤
        /// </summary>
        readonly static Array MUSICFILTERARRAY = ConfigTable.Get(ConfigEnum.MusicFilter).Split(',');

        /// <summary>
        /// Blog上传的图片路径
        /// </summary>
        readonly static string SourceBlogPath = ConfigTable.Get(ConfigEnum.SourceBlogImages);

        /// <summary>
        /// Said上传的图片路径
        /// </summary>
        readonly static string SourceSaidPath = ConfigTable.Get(ConfigEnum.SourceSaidImages);

        /// <summary>
        /// 音乐上传的路径
        /// </summary>
        readonly static string SourceMusicPath = ConfigTable.Get(ConfigEnum.MusicPath);

        /// <summary>
        /// Icon上传的路径
        /// </summary>
        readonly static string SourceIconsPath = ConfigTable.Get(ConfigEnum.MusicPath);

        /// <summary>
        /// 系统图片上传的路径
        /// </summary>
        readonly static string SourceSystemPath = ConfigTable.Get(ConfigEnum.SystemImages);

        /// <summary>
        /// 资源删除后存放的路径
        /// </summary>
        readonly static string SourceSystemDelete = ConfigTable.Get(ConfigEnum.SystemDelete);


        /// <summary>
        /// Blog允许的最大上传图片
        /// </summary>
        readonly int SizeBlogImage = int.Parse(ConfigTable.Get(ConfigEnum.SourceBlogImagesMaxSize));

        /// <summary>
        /// Said允许的最大图片
        /// </summary>
        readonly int SizeSaidImage = int.Parse(ConfigTable.Get(ConfigEnum.SourceBlogImagesMaxSize));

        /// <summary>
        /// Music允许的最大图片
        /// </summary>
        readonly int SizeMusic = int.Parse(ConfigTable.Get(ConfigEnum.MusicMaxSize));

        /// <summary>
        /// Icons允许的最大图片
        /// </summary>
        readonly int SizeIcons = int.Parse(ConfigTable.Get(ConfigEnum.SourceIconsMaxSize));

        /// <summary>
        /// 系统图片允许的最大上传大小
        /// </summary>
        readonly int SizeSystem = int.Parse(ConfigTable.Get(ConfigEnum.SystemImagesSize));

        #endregion



        #region 上传一个文件
        // GET: /Source/
        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="maxSize">接受的最大的文件大小</param>
        /// <param name="filters">可接受的扩展名</param>
        /// <param name="dirPath">要保存的路径（路径即可，文件名是自动生成的，注意路径最后要加斜杠）</param>
        /// <returns></returns>
        private JsonResult UploadFile(HttpPostedFileBase file, Array filters, int maxSize, string dirPath)
        {
            FileCommon.ExistsCreate(dirPath);
            if (file == null)
            {
                return UploadResult(1, "没有文件");
            }
            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                return UploadResult(1, "上传文件大小超过限制");
            }
            //file.InputStream可以获取到System.io.Stream对象，由此可以对文件进行hash加密运算
            string fileName = file.FileName,
            fileExt = Path.GetExtension(fileName).ToLower();//扩展名
            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(filters, fileExt.Substring(1).ToLower()) == -1)
                return UploadResult(1, "上传文件扩展名是不允许的扩展名");
            string newFileName = string.Empty, //新生成的文件名
                   filePath = string.Empty;
            if (string.IsNullOrEmpty(dirPath))
                return UploadResult(1, "服务器异常");
            newFileName = FileCommon.CreateFileNameByTime() + fileExt;
            filePath = dirPath + newFileName;
            file.SaveAs(filePath);

            //分析上传的文件信息，返回解析得到的结果
            return UploadResult(0, "上传成功", newFileName);
        }
        #endregion


        #region 上传一个图片，保存并返回图片信息新生成的文件名
        /// <summary>
        /// 上传一个图片，保存并返回图片信息新生成的文件名
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filters"></param>
        /// <param name="maxSize"></param>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        private Dictionary<string, string> Save(HttpPostedFileBase file, Array filters, int maxSize, string dirPath)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            FileCommon.ExistsCreate(dirPath);
            if (file == null)
            {
                result.Add("code", "1");
                result.Add("msg", "没有文件");
                return result;
            }
            if (file.InputStream == null || file.InputStream.Length > maxSize)
            {
                result.Add("code", "1");
                result.Add("msg", "上传文件大小超过限制");
                return result;
            }
            //file.InputStream可以获取到System.io.Stream对象，由此可以对文件进行hash加密运算
            string fileName = file.FileName,
            fileExt = Path.GetExtension(fileName).ToLower();//扩展名
            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(filters, fileExt.Substring(1).ToLower()) == -1)
            {
                result.Add("code", "1");
                result.Add("msg", "上传文件扩展名是不允许的扩展名");
                return result;
            }
            string newFileName = string.Empty, //新生成的文件名
                   filePath = string.Empty;
            if (string.IsNullOrEmpty(dirPath))
            {
                result.Add("code", "1");
                result.Add("msg", "服务器异常");
                return result;
            }
            newFileName = FileCommon.CreateFileNameByTime() + fileExt;
            filePath = dirPath + newFileName;
            file.SaveAs(filePath);
            result.Add("code", "0");
            result.Add("name", newFileName);
            return result;
        }

        #endregion

        #region 上传Said图片
        /// <summary>
        /// 上传Said图片
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadSaidImg()
        {
            //分析上传的文件信息，返回解析得到的结果
            return UploadFile(Request.Files["uploadFile"], IMGFILTERARRAY, SizeSaidImage, SourceSaidPath);
        }
        #endregion


        #region 上传Said图片
        /// <summary>
        /// 上传Said图片
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadBlogImg()
        {
            //分析上传的文件信息，返回解析得到的结果
            return UploadFile(Request.Files["uploadFile"], IMGFILTERARRAY, SizeBlogImage, SourceBlogPath);
        }
        #endregion


        #region 上传歌曲
        /// <summary>
        /// 上传歌曲
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadMusic()
        {
            return UploadFile(Request.Files["uploadFile"],
                MUSICFILTERARRAY,
                SizeMusic,
                SourceMusicPath);
        }
        #endregion


        #region 上传类别Icon
        /// <summary>
        /// 上传类别Icon
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadClassifyIcons()
        {
            return UploadFile(
                Request.Files["uploadFile"],
                IMGFILTERARRAY,
                SizeSaidImage,
                SourceIconsPath);
        }
        #endregion


        #region 获取图片
        public JsonResult GetImagesList(int limit, int offset)
        {
            var page = new Page
            {
                PageNumber = offset / limit + 1,
                PageSize = limit
            };
            var res = ImageApplication.FindToList(page);
            return Json(new
            {
                //hasNextPage = res.HasNextPage,
                //hasPreviousPage = res.HasPreviousPage,
                total = res.Count,
                datas = res.Select(m => new { id = m.ImageId, name = m.IName, img = m.IFileName, data = m.IFileName })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 删除图片
        /// <summary>
        /// 删除图片（逻辑删除，修改isDelete，移动图片路径）
        /// </summary>
        /// <param name="id">image</param>
        /// <returns></returns>
        public JsonResult DeleteImage(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ResponseResult(1, "没有数据");
            Image image = ImageApplication.Find(id);
            if (image == null)
                return ResponseResult(2, "没有找到图片");
            image.IsDel = 1;
            string path = string.Empty;
            switch (image.Type)
            {
                case ImageType.Blog:
                    path = SourceBlogPath;
                    break;
                case ImageType.Said:
                    path = SourceSaidPath;
                    break;
                case ImageType.System:
                case ImageType.Icon:
                case ImageType.Page:
                case ImageType.Lab:
                case ImageType.Other:
                default:
                    path = SourceSystemPath;
                    break;
            }
            FileCommon.Move(path + image.IFileName, string.Format("{0}${1}-${2}-${3}", SourceSystemDelete, image.ImageId, image.IFileName, image.Type));
            //更新到数据库，改动了isDel
            ImageApplication.Update(image);
            return ResponseResult();
        }
        #endregion

        #region 资源中心的上传
        public JsonResult UploadSaidImage(int type = 0)
        {
            //分析上传的文件信息，返回解析得到的结果
            return UploadImage(Request.Files["uploadFile"], IMGFILTERARRAY, SizeSaidImage, SourceSaidPath, ImageType.Said);
        }


        #endregion


        #region 通用方法
        private JsonResult UploadResult(int code, string msg, string name = null)
        {
            return Json(new { code = code, msg = msg, name = name });
        }



        private JsonResult UploadImage(HttpPostedFileBase file, Array filters, int maxSize, string dirPath, ImageType type)
        {
            //分析上传的文件信息，返回解析得到的结果
            Dictionary<string, string> result = Save(file, IMGFILTERARRAY, SizeBlogImage, SourceBlogPath);
            if (result["code"] == "1")
                return Json(new { code = 1, msg = result["msg"] });
            Image model = new Image
            {
                //TODO   -  UserID,ISize
                Date = DateTime.Now,
                IFileName = result["name"],
                Type = type,
                ImageId = Guid.NewGuid().ToString().Replace("-", ""),
                IName = result["name"]
            };
            if (ImageApplication.Add(model) > 0)
                return Json(new
                {
                    code = 0,
                    id = model.ImageId,
                    name = model.IFileName,
                    data = model.IFileName,
                    img = model.IFileName
                });
            return Json(new { code = 2, msg = "插入到数据库失败" });
        }
        #endregion


        public ActionResult Index()
        {
            return View();
        }

    }
}
