using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Articles;
using Domain.Repository.Articles;
using PagedList;
using WebMatrix.WebData;
using Reklama.Models.SortModels.Article;
using Domain.Entity.Announcements;
using Reklama.Models;
using Domain.Enums;

namespace Reklama.Controllers
{
    public class ArticleController : Controller
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IArticleRepository _articleRepository;
        private readonly IArticleSectionRepository _sectionRepository;
        private readonly IArticleSubsectionRepository _subsectionRepository;


        public ArticleController(IArticleRepository articleRepository, 
            IArticleSectionRepository sectionRepository,
            IArticleSubsectionRepository subsectionRepository)
        {
            _articleRepository = articleRepository;
            _articleRepository.Context = rc;

            _sectionRepository = sectionRepository;
            _sectionRepository.Context = rc;

            _subsectionRepository = subsectionRepository;
            _subsectionRepository.Context = rc;

            ViewBag.Slogan = ProjectConfiguration.Get.ArticleConfig.Slogan;
            ViewBag.SelectedSiteCategory = CategorySearch.Article;
        }



        //
        // GET: /Article/

        public ActionResult Index(ArticleSortModel sortModel = null)
        {
            ViewBag.IsVisibleBest = Request.QueryString["v"] != null && Request.QueryString["v"] == "1";
            IEnumerable<Article> bestArticles;
            IEnumerable<Article> articles = _articleRepository.Read().ToArray();

            if (sortModel == null)
            {
                sortModel = new ArticleSortModel();
            }

            if (sortModel.SubsectionId.HasValue)
            {
                articles = articles.Where(a => a.SubsectionId == sortModel.SubsectionId.Value);
            }

            bestArticles = new[]
                                  {
                                      ProjectConfiguration.Get.ArticleConfig.Best1,
                                      ProjectConfiguration.Get.ArticleConfig.Best2,
                                      ProjectConfiguration.Get.ArticleConfig.Best3,
                                      ProjectConfiguration.Get.ArticleConfig.Best4
                                  };

            if (articles != null && bestArticles != null)
            {
                articles = ArticleExcept(articles, bestArticles);
            }
            ViewBag.ArticleSortModel = sortModel;
            return View("Index", articles.ToPagedList(sortModel.CurrentPage.Value, ProjectConfiguration.Get.ItemsOnPage[1]));
        }

        //
        // GET: /Article/Details/5

        public ActionResult Details(int id, int? commentPage)
        {
            var article = _articleRepository.Read(id);

            if(article == null)
            {
                return HttpNotFound();
            }

            if (!article.IsActive)
            {
                if (User.IsInRole("Administrator"))
                {
                    TempData["notice"] = "Это объявление заблокировано";
                }
                else
                {
                    return HttpNotFound();
                }
            }

            _articleRepository.IncrementViewsCount(article);
            ViewBag.ArticleId = article.Id;
            ViewBag.Comments = (article.Comments != null && article.Comments.Count > 0)
                ? article.Comments.ToPagedList(commentPage ?? 1, ProjectConfiguration.Get.CommentsOnPage)
                : null;

            return View(article);
        }



        //
        // GET: /Article/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            PopulateSectionDropDownList(allowEmpty: true);
            return View();
        }

        //
        // POST: /Article/Create

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Article article)
        {
            var file = Request.Files["ArticleLogo"];
            ArticleSubsection subsection;
            int firstSectionId; 

            try
            {
                if (article.SubsectionId == 0)
                {
                    ModelState.AddModelError("SubsectionId", "Необходимо указать подраздел");
                    TempData["error"] = "Укажи подраздел!";
                }

                if (file == null || file.ContentLength == 0)
                {
                    ModelState.AddModelError("Logo", "Вы не загрузили изображение");
                }

                int id;
                var uploader = new ImageUploader(file);
                uploader.Interpolation = InterpolationMode.HighQualityBilinear;
                uploader.Quality = CompositingQuality.HighQuality;
                article.Logo = uploader.UniqueName;
                article.CreatedAt = DateTime.Now;
                article.ViewsCount = 0;

                // TODO: Add a slug logic!

                if (ModelState.IsValid)
                {
                    try
                    {
                        uploader.Convert(ProjectConfiguration.Get.ArticleImageWidth, ProjectConfiguration.Get.ArticleImageHeight);
                        uploader.Save("articles");
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Возникли проблемы при загрузке изображения. Попробуйте снова";
                    }

                    try
                    {
                        id = _articleRepository.Save(article);
                    }
                    catch (Exception)
                    {
                        TempData["error"] = "Возникли проблемы при загрузке изображения. Попробуйте снова";
                        return View(article);
                    }

                    return RedirectToAction("Details", new {id = id});
                }
            }
            catch
            {
                subsection = _subsectionRepository.Read(article.SubsectionId);

                if (subsection != null)
                {
                    firstSectionId = PopulateSectionDropDownList(subsection.Section);
                    PopulateSubsectionDropDownList((subsection.Section != null) ? subsection.Section.Id : firstSectionId, article.Subsection);
                }
                else
                {
                    PopulateSectionDropDownList(allowEmpty: true);
                }
                return View(article);
            }

            subsection = _subsectionRepository.Read(article.SubsectionId);

            if (subsection != null)
            {
                firstSectionId = PopulateSectionDropDownList(subsection.Section);
                PopulateSubsectionDropDownList((subsection.Section != null) ? subsection.Section.Id : firstSectionId, article.Subsection);
            }
            else
            {
                PopulateSectionDropDownList(allowEmpty: true);
            }
            return View(article);
        }



        //
        // GET: /Article/Edit/5

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var article = _articleRepository.Read(id);

            if(article == null)
            {
                return HttpNotFound();
            }

            var section = _sectionRepository.Read(article.SubsectionId);

            if (section != null)
            {
                PopulateSectionDropDownList(section, allowEmpty: true);
            }
            else
            {
                PopulateSectionDropDownList();
            }

            if (article.Subsection != null)
            {
                PopulateSubsectionDropDownList(article.Subsection.SectionId, article.Subsection);
            }
            else
            {
                PopulateSubsectionDropDownList(1, article.Subsection);
            }
            return View(article);
        }



        //
        // POST: /Article/Edit/5

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, Article article)
        {
            var oldArticle = _articleRepository.Read(id);
            article.Id = id;

            if(oldArticle == null)
            {
                return HttpNotFound();
            }

            if (article.SubsectionId == 0)
            {
                TempData["error"] = "Обязательно укажи подраздел!";
                ModelState.AddModelError("SubsectionId", "Необходимо указать подраздел");
            }

            var file = Request.Files["ArticleLogo"];

            if(file == null)
            {
                article.Logo = oldArticle.Logo;
            }
            else
            {
                if (file == null || file.ContentLength == 0)
                {
                    TempData["error"] = "Необходимо загрузить изображение";
                    ModelState.AddModelError("Logo", "Вы не загрузили изображение");
                }
                else
                {
                    var uploader = new ImageUploader(file);
                    uploader.Interpolation = InterpolationMode.HighQualityBilinear;
                    uploader.Quality = CompositingQuality.HighQuality;
                    article.Logo = uploader.UniqueName;

                    try
                    {
                        uploader.Convert(ProjectConfiguration.Get.ArticleImageWidth, ProjectConfiguration.Get.ArticleImageHeight);
                        uploader.Save("articles");
                    }
                    catch
                    {
                        TempData["error"] = "Не удалось загрузить изображение. Пожалуйста, попробуйте снова";
                        return View(article);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int articleId = _articleRepository.Save(article);
                    return RedirectToAction("Details", "Article", new { id = articleId });
                }
                catch
                {
                    TempData["error"] = "Возникла ошибка при сохранении. Пожалуйста, попробуйте снова";
                    return View(article);
                }
            }

            int sid = PopulateSectionDropDownList(oldArticle.Subsection.Section);
            PopulateSubsectionDropDownList(oldArticle.Subsection.SectionId, oldArticle.Subsection);
            return View(article);
        }

        //
        // GET: /Article/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var article = _articleRepository.Read(id);

            if(article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        //
        // POST: /Article/Delete/5

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var article = _articleRepository.Read(id);

            if(article == null)
            {
                return HttpNotFound();
            }

            try
            {
                FileUploader.Delete("articles", article.Logo);
                _articleRepository.Delete(article);

                return RedirectToAction("Index", "Article");
            }
            catch
            {
                TempData["error"] = "Возникла ошибка при удалении статьи";
                return View(article);
            }
        }


        public ActionResult RenderSections(int? subsectionId)
        {
            var sections = _sectionRepository.Read();

            if (subsectionId.HasValue)
            {
                var subsection = _subsectionRepository.Read(subsectionId.Value);
                ViewBag.SectionId = subsection.SectionId;
                ViewBag.SubsectionId = subsectionId.Value;
            }
            else
            {
                ViewBag.SectionId = null;
                ViewBag.SubsectionId = null;
            }

            return View(sections);
        }



        private int PopulateSectionDropDownList(object selectedSection = null, bool allowEmpty = false)
        {
            var sections = _sectionRepository.Read().OrderBy(c => c.Name).ToList();
            if (allowEmpty) { sections.Insert(0, new ArticleSection() { Id = 0, Name = "Выберите раздел" }); }
            ViewBag.Sections = new SelectList(sections, "Id", "Name", selectedSection);
            return sections.FirstOrDefault().Id;
        }

        private void PopulateSubsectionDropDownList(int sectiondId, object selectedSubsection)
        {
            var subsections = _subsectionRepository.ReadBySection(sectiondId).OrderBy(c => c.Name);
            ViewBag.Subsections = new SelectList(subsections, "Id", "Name", selectedSubsection);
        }


        private IEnumerable<Article> ArticleExcept(IEnumerable<Article> queue1, IEnumerable<Article> queue2)
        {
            var list = new List<Article>();

            foreach(var article in queue1)
            {
                bool isFinded = false;

                foreach (var article1 in queue2)
                {
                    if (article != null && article1 != null)
                    {
                        if (article.Id == article1.Id)
                        {
                            isFinded = true;
                            break;
                        }
                    }
                }

                if(!isFinded)
                {
                    list.Add(article);
                }
                isFinded = false;
            }

            return list;
        }

        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            ProjectConfiguration.Get.Dispose();
            base.Dispose(disposing);
        }
    }
}
