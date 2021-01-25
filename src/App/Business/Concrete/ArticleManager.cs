using Business.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Net;
using System.Web;
using Entities.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using AutoMapper;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDA _articleDA;
        private readonly IMapper _mapper;

        public ArticleManager(IArticleDA articleDA, IMapper mapper)
        {
            _articleDA = articleDA;
            _mapper = mapper;
        }

        public IResult InitArticles(int count)
        {
            List<ArticleDTO> articles = ScrapeArticles(count).Data;
            if (!Tools.IsObjectNullOrEmpty(articles))
            {
                InsertArticles(articles);
            }

            return new SuccessResult(new List<int> { 6 });
        }

        private IDataResult<List<ArticleDTO>> ScrapeArticles(int count)
        {
            List<ArticleDTO> articles = null;

            Uri url = new Uri(Core.Constants.ScrapeConstants.UrlToScrape);
            WebClient client = new WebClient();
            string html = client.DownloadString(url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            HtmlNodeCollection headers = document.DocumentNode.SelectNodes("/html/body/div[3]/div/div[3]/div/div[2]/div/div[1]/div/div/ul/li");

            if (!Tools.IsObjectNullOrEmpty(headers))
            {
                articles = new List<ArticleDTO>();
            }

            int counter = 0;
            foreach (var htmlNode in headers)
            {
                if (counter == 5)
                {
                    break;
                }
                string nodeHeader = htmlNode.SelectSingleNode(".//h2[@class='archive-item-component__title']").InnerText;
                nodeHeader = HttpUtility.HtmlDecode(nodeHeader);
                string nodeContent = htmlNode.SelectSingleNode(".//p[@class='archive-item-component__desc']").InnerText;
                nodeContent = HttpUtility.HtmlDecode(nodeContent);
                articles.Add(new ArticleDTO { Header = nodeHeader, Content = nodeContent });

                counter++;
            }

            return new SuccessDataResult<List<ArticleDTO>>(articles);
        }

        private IResult InsertArticles(List<ArticleDTO> articles)
        {
            if (!Tools.IsObjectNullOrEmpty(articles))
            {
                foreach (ArticleDTO item in articles)
                {
                    Article itemEntity = _mapper.Map<Article>(item);
                    _articleDA.Insert(itemEntity);
                }
            }

            return new SuccessResult();
        }

        public IDataResult<List<ArticleDTO>> GetList()
        {
            List<Article> entityList = _articleDA.GetList();
            List<ArticleDTO> dtoList = _mapper.Map<List<ArticleDTO>>(entityList);

            return new SuccessDataResult<List<ArticleDTO>>(dtoList);
        }

        public IDataResult<List<ArticleDTO>> GetLastDescending(int take)
        {
            List<Article> entityList = _articleDA.GetLastDescending(take);
            List<ArticleDTO> dtoList = _mapper.Map<List<ArticleDTO>>(entityList);

            return new SuccessDataResult<List<ArticleDTO>>(dtoList);
        }

        public IDataResult<ArticleDTO> Get(int id)
        {
            Article entity = _articleDA.Get(x => x.Id == id);
            ArticleDTO dto = _mapper.Map<ArticleDTO>(entity);

            return new SuccessDataResult<ArticleDTO>(dto);
        }
    }
}
