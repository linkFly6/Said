﻿using Said.Application;
using Said.Common;
using Said.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Said.Areas.Back.Controllers
{
    public class ClassifyController : BaseController
    {
        //
        // GET: /Back/Classify/
        private readonly string ICONPATH = "~/Source/Sys/Images/Icons/";


        public ActionResult Index()
        {
            ViewData["Classify"] = ClassifyApplication.Find();
            string[] iconsFilePath = FileCommon.GetFileNames(Server.MapPath(ICONPATH));
            if (iconsFilePath != null)
                for (int i = 0; i < iconsFilePath.Length; i++)
                    iconsFilePath[i] = FileCommon.getFileName(iconsFilePath[i]);
            ViewData["Tags"] = TagApplication.Find();
            ViewData["iconFiles"] = iconsFilePath;
            return View();
        }

        #region Classify Services
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="name">分类名</param>
        /// <param name="imgName">分类Icon</param>
        /// <returns></returns>
        public JsonResult AddClassify(string name, string imgName)
        {
            if (string.IsNullOrWhiteSpace(imgName) || !FileCommon.Exists(Server.MapPath(ICONPATH) + imgName))
                return ResponseResult(2, "上传的Icon不正确");
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResult(1, "分类名称不正确");
            name = name.Trim();
            if (ClassifyApplication.FindByName(name) != null)
                return ResponseResult(4, "该分类已经存在！");
            Classify model = new Classify
            {
                CCount = 0,
                CIcon = imgName,
                CLastBlogId = string.Empty,
                CLastBlogName = string.Empty,
                ClassifyId = Guid.NewGuid().ToString(),
                CName = name,
                Date = DateTime.Now,
                IsDel = 0
            };
            return ClassifyApplication.Add(model) > 0 ?
                ResponseResult(model.ClassifyId) :
                ResponseResult(3, "服务器删除异常");
        }

        /// <summary>
        /// 编辑分类
        /// </summary>
        /// <param name="name">分类名</param>
        /// <param name="imgName">分类Icon</param>
        /// <param name="id">分类Icon</param>
        /// <returns></returns>
        public JsonResult EditClassify(string name, string imgName, string id)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResult(1, "分类名称不正确");
            if (string.IsNullOrWhiteSpace(imgName) || !FileCommon.Exists(Server.MapPath(ICONPATH) + imgName))
                return ResponseResult(2, "上传的Icon不正确");
            if (string.IsNullOrWhiteSpace(id))
                return ResponseResult(3, "分类信息不正确");
            name = name.Trim();
            if (ClassifyApplication.FindByName(name) != null)
                return ResponseResult(6, "该分类已经存在");
            Classify model = ClassifyApplication.Find(id);
            if (model == null)
                return ResponseResult(4, "没有找到该分类信息");
            if (model.CIcon == imgName.Trim() && model.CName == name.Trim())//没有改动直接编辑成功
                ResponseResult();
            model.CIcon = imgName.Trim();
            model.CName = name.Trim();
            return ClassifyApplication.Update(model) > 0 ?
                ResponseResult() :
                ResponseResult(5, "服务器编辑异常");
        }

        public JsonResult DeleteClassify(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ResponseResult(1, "要删除的数据标志不正确");
            return ClassifyApplication.Delete(id) > 0 ?
                ResponseResult() :
                ResponseResult(2, "服务器删除异常");
        }
        #endregion

        #region Tag Services
        //下面为Tag逻辑

        /// <summary>
        /// 添加tag
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult AddTag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResult(1, "标签不允许为空");
            if (TagApplication.FindByName(name) != null)
                return ResponseResult(2, "标签已存在");
            var model = new Tag
            {
                Count = 0,
                TagId = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                TagName = name.Trim(),
                IsDel = 0
            };
            return TagApplication.Add(model) > 0 ?
                ResponseResult(model.TagId) :
                ResponseResult(3, "服务器异常");
        }

        /// <summary>
        /// 编辑tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult EditTag(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ResponseResult(1, "要修改的标签标志不正确");
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResult(2, "标签名不允许为空");
            name = name.Trim();
            var model = TagApplication.Find(id.Trim());
            var existsTag = TagApplication.FindByName(name);
            if (model == null)
                return ResponseResult(3, "没有找到要修改的标签信息");
            if (existsTag != null)
                return ResponseResult(4, "标签已存在");
            model.TagName = name;
            model.Count = 0;
            return TagApplication.Update(model) > 0 ?
                ResponseResult() :
                ResponseResult(5, "服务器异常");
        }
        public JsonResult DeleteTag(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ResponseResult(1, "要删除的标签标志不正确");
            var model = TagApplication.Find(id);
            if (model == null)
                return ResponseResult(2, "没有找到要删除的标签信息");
            return TagApplication.Delete(id) > 0 ?
                ResponseResult() :
                ResponseResult(3, "服务器异常");
        }
        #endregion
    }
}