import { AdminRule } from '../models/admin'
import { IImage, ImageType } from '../models/image'
import { IArticle } from '../models/article'
import { OutputImage } from './image'
import { OutputSong } from './song'
import { ISong } from "../models/song";

/**
 * 前端入参
 */
export interface SimpleArticle {
  _id?: string,
  title: string,
  context: string,
  summary: string,
  songId: string,
  posterId: string,
}

/**
 * 输出对象
 */
export interface OutputArticle {
  _id: string,
  title: string,
  context: string,
  key: string,
  author: {
    _id?: string,
    nickName: string,
    avatar?: string,
    email?: string,
    bio?: string,
    rule: AdminRule,
  },
  summary: string,
  poster: OutputImage,
  song: OutputSong,
  other: {
    xml: string,
    html: string,
    summaryHTML: string
  },
  info: {
    likeCount: number,
    createTime: number,
    updateTime: number,
  },
  // config: {
  //   script: string,
  //   css: string,
  // }
}

/**
 * 前端输出到前台页面中的文章，会针对日期进行本地化处理
 */
interface IViewArticle extends IArticle {
  info: IArticle['info'] & {
    /**
     * 根据 createTime 修正为日期，格式为 'YYYY-MM-DD HH:mm'
     */
    createTimeString: string,
    /**
     * 同时针对本地化输出时间
     */
    localDate: string
  }
}

/**
 * 剔除无关信息的文章对象
 * 用于文章列表使用，剔除了 HTML/源码/配置/管理员等信息
 */
export interface SafeSimpleArticle {
  _id: string,
  title: string,
  key: string,
  author: {
    nickName: string,
  },
  summary: string,
  poster: OutputImage,
  song: ISong,
  other: {
    summaryHTML: string
  },
  info: {
    localDate: string,
    likeCount: number,
    createTimeString: string,
    createTime: number,
    updateTime: number,
    pv: number,
  },
}
