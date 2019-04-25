namespace C.B.Spider.sites {
    using System.Collections.Generic;
    using System;
    using C.B.Mongo.data;
    using C.B.Mongo.service;
    using HtmlAgilityPack;
    using HttpCode.Core;

    public class cnblogs {

        private HttpHelpers httpHelpers;

        private MgContentService mgContentService;
        public void Start () {
            httpHelpers = new HttpHelpers ();
            mgContentService = new MgContentService ();

            var pageIndex = 1;
            do {
                var html = GetHtml (pageIndex);
                var list = ReadHtml (html);
                SaveDB (list);
                pageIndex++;
            } while (pageIndex < 100);

        }

        private string LoadHtml (int pageIndex) {
            System.Threading.Thread.Sleep (1000);
            System.Console.WriteLine ($" ----------------------------------> {pageIndex}");

            HttpItems items = new HttpItems ();
            items.Url = "https://www.cnblogs.com/mvc/AggSite/PostList.aspx"; //请求地址
            items.Method = "Post"; //请求方式 post
            //items.ContentType = "application/json; charset=UTF-8";
            items.Header.Add ("content-type", "application/json; charset=UTF-8");
            //items.Postdata = @"{'CategoryId':808,'CategoryType':'SiteHome','ItemListActionName':'PostList','PageIndex':#PAGEINDEX,'ParentCategoryId':0,'TotalPostCount':4000}"; //请求数据
            // items.Postdata = "{\"CategoryType\":\"SiteHome\"," +
            //     "\"ParentCategoryId\":0," +
            //     "\"CategoryId\":808," +
            //     "\"PageIndex\":" + pageIndex + "," +
            //     "\"TotalPostCount\":4000," +
            //     "\"ItemListActionName\":\"PostList\"}"; //请求数据
            //items.Postdata = items.Postdata.Replace ("#PAGEINDEX", pageIndex.ToString ());
            HttpResults hr = httpHelpers.GetHtml (items);
            System.Console.WriteLine (items.Postdata);
            // System.Console.WriteLine (hr.Html);
            return hr.Html;
        }
        private string GetHtml (int pageIndex) {
            HttpItems items = new HttpItems ();
            items.Url = "https://www.cnblogs.com/mvc/AggSite/PostList.aspx?pageIndex=" + pageIndex; //请求地址
            items.Method = "GET";

            HttpResults hr = httpHelpers.GetHtml (items);
            // System.Console.WriteLine (hr.Html);
            return hr.Html;
        }
        private List<MgContent> ReadHtml (string html) {

            //解析数据
            HtmlDocument doc = new HtmlDocument ();
            //加载html
            doc.LoadHtml (html);

            //获取 class=post_item_body 的div列表
            HtmlNodeCollection itemNodes = doc.DocumentNode.SelectNodes ("div[@class='post_item']/div[@class='post_item_body']");

            //循环根据每个div解析我们想要的数据
            var list = new List<MgContent> ();
            foreach (var item in itemNodes) {
                //获取包含博文标题和地址的 a 标签
                var nodeA = item.SelectSingleNode ("h3/a");
                //获取博文标题
                string title = nodeA.InnerText;
                //获取博文地址 a标签的 href 属性
                string url = nodeA.GetAttributeValue ("href", "");

                string content = item.SelectSingleNode ("p").InnerText;

                //获取包含作者名字的 a 标签
                var nodeAuthor = item.SelectSingleNode ("div[@class='post_item_foot']/a[@class='lightblue']");
                string author = nodeAuthor.InnerText;

                System.Console.WriteLine ($"标题：{title} | 作者：{author} | 地址：{url} | {content}");
                list.Add (new MgContent () { Title = title, Content = content, Author = author, Url = url });
            }
            return list;
        }

        private void SaveDB (List<MgContent> list) {
            mgContentService.InsertMany (list);
        }
    }
}