using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class VedioController : BaseController
    {
        //
        // GET: /Vedio/

        public JsonResult GetVedioList(int pi = 0, int ps = 6)
        {
            var videoList = Model.Video.GetList(pi, ps, true);
            if (videoList == null)
            {
                videoList = new List<Video>();
            }
            return JsonResult(new
            {
                m = 1,
                d = new
                {
                    l = videoList.Select(x => new
                    {
                        ID = x.ID,
                        Src = FormatVideoUrl(x.AppSrc),
                        Title = x.Title,
                        Content = x.Content,
                        CreateDate = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),
                        IntroImage = FormatImgUrl(x.IntroImage)
                    }),
                    minId = videoList != null && videoList.Count > 0 ? videoList.Min(x => x.ID) : 0
                }

            });
        }

    }
}
