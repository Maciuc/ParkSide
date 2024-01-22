using exp.NET6.Services.DBServices;
using Parkside.Infrastructure.Entities;
using Parkside.Infrastructure.Repositories.Newses;
using Parkside.Models.Helpers;
using Parkside.Models.ViewModels;

namespace Parkside.Services.Newss
{
    public class NewsService : INewsService
    {
        private readonly INewsRepo _newsRepo;
        private readonly IGenericService _genericService;
        public NewsService(INewsRepo newsRepo, IGenericService genericService)
        {
            _newsRepo = newsRepo;
            _genericService = genericService;
        }

        public async Task<NewsDetailsViewModel> GetNews(int id)
        {
            var news = await _newsRepo.GetAsync(id);

            if (news == null)
                throw new NotFoundException("News not found!");

            var finalNews = new NewsDetailsViewModel
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                PublishedDate = news.PublishedDate,
                Content = news.Content,
                IsPublished = news.IsPublished,
                ImageBase64 = _genericService.GetImgBase64(news.ImageUrl),
                IsPrimary = news.IsPrimary,
            };

            return finalNews;
        }

        public PagingViewModel<NewsViewModel> GetNewses(
            string? NameSearch, string? OrderBy, string? PublishedDate,
            bool? IsPublished, bool? IsPrimary,
            int PageNumber, int PageSize)
        {
            var newses = _newsRepo.GetAllQuerable();

            if (!string.IsNullOrWhiteSpace(NameSearch))
            {
                NameSearch = NameSearch.Trim();
                newses = newses.Where(c => c.Name.Contains(NameSearch));
            }

            if (!string.IsNullOrWhiteSpace(PublishedDate))
            {
                PublishedDate = PublishedDate.Trim();

                if (DateTime.TryParse(PublishedDate, out var publishedDate))
                {
                    newses = newses.Where(n => n.PublishedDate != null && n.PublishedDate.Value.Date == publishedDate.Date);
                }
            }

            if (IsPublished != null)
            {
                newses = newses.Where(n => n.IsPublished == IsPublished);
            }

            if (IsPrimary != null)
            {
                newses = newses.Where(n => n.IsPrimary == IsPrimary);
            }


            switch (OrderBy)
            {

                case ("name"):
                    newses = newses.OrderBy(c => c.Name);
                    break;

                case ("name_desc"):
                    newses = newses.OrderByDescending(c => c.Name);
                    break;

                case ("PublishedDate"):
                    newses = newses.OrderBy(c => c.PublishedDate);
                    break;

                case ("PublishedDate_desc"):
                    newses = newses.OrderByDescending(c => c.PublishedDate);
                    break;

                default:
                    newses = newses.OrderByDescending(c => c.PublishedDate);
                    break;
            }

            var numberOfNewss = newses.Count();

            var newsesPerPage = newses.Skip(PageSize * (PageNumber - 1))
              .Take(PageSize).Select(news => new NewsViewModel
              {
                  Id = news.Id,
                  Name = news.Name,
                  Description = news.Description,
                  PublishedDate = news.PublishedDate.HasValue ? news.PublishedDate.Value.ToString("dd/MM/yyyy") : null,
                  IsPublished = news.IsPublished,
                  ImageBase64 = _genericService.GetImgBase64(news.ImageUrl),
                  IsPrimary = news.IsPrimary
              })
              .ToList();

            var paginingList = new PagingViewModel<NewsViewModel>
            {
                Count = numberOfNewss,
                NumberOfPages = (int)Math.Ceiling(numberOfNewss / (double)PageSize),
                Items = newsesPerPage
            };

            return paginingList;
        }

        public async Task AddNews(NewsCreateViewModel model)
        {

            var finalNews = new News()
            {
                Name = model.Name,
                Description = model.Description,
                PublishedDate = model.PublishedDate,
                IsPublished = model.IsPublished,
                IsPrimary = model.IsPrimary,
                Content = model.Content,
                ImageUrl = _genericService.GetImagePath(model.ImageBase64, null, "Newses")
            };

            await _newsRepo.Add(finalNews);
        }
        public async Task DeleteNews(int id)
        {
            var news = await _newsRepo.GetAsync(id);
            if (news == null)
                throw new NotFoundException("News not found!");

            await _newsRepo.Delete(id);
        }

        public async Task VirtualDeleteNews(int id)
        {
            var news = await _newsRepo.GetAsync(id);
            if (news == null)
                throw new NotFoundException("News not found!");

            news.IsDeleted = true;

            await _newsRepo.Update(news);
        }

        public async Task UpdateNews(int id, NewsUpdateViewModel model)
        {
            var news = await _newsRepo.GetAsync(id);
            if (news == null)
                throw new NotFoundException("News not found!");

            news.Name = model.Name;
            news.Description = model.Description;
            news.PublishedDate = model.PublishedDate;
            news.Content = model.Content;
            news.IsPublished = model.IsPublished;
            news.IsPrimary = model.IsPrimary;
            news.ImageUrl = _genericService.GetImagePath(model.ImageBase64, news.ImageUrl, "Newses");

            await _newsRepo.Update(news);
        }

        public IQueryable<NewsViewModel> GetHomePageNewses()
        {
            var newses = _newsRepo.GetAllQuerable().Where(x => x.IsPublished == true); ;

            var finalNewses = newses.Select(news => new NewsViewModel
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                PublishedDate = news.PublishedDate.HasValue ? news.PublishedDate.Value.ToString("dd/MM/yyyy") : null,
                IsPublished = news.IsPublished,
                ImageBase64 = _genericService.GetImgBase64(news.ImageUrl)
            });


            return finalNewses;
        }

        public IQueryable<NewsViewModel> GetLatestNewses()
        {
            var newses = _newsRepo.GetAllQuerable().Where(x => x.IsPublished == true); ;

            var finalNewses = newses.OrderByDescending(c => c.PublishedDate).Take(6)
                .Select(news => new NewsViewModel
                {
                    Id = news.Id,
                    Name = news.Name,
                    Description = news.Description,
                    PublishedDate = news.PublishedDate.HasValue ? news.PublishedDate.Value.ToString("dd/MM/yyyy") : null,
                    IsPublished = news.IsPublished,
                    ImageBase64 = _genericService.GetImgBase64(news.ImageUrl)
                });

            return finalNewses;
        }

        public IQueryable<NewsViewModel> GetPrimaryNewses()
        {
            var newses = _newsRepo.GetAllQuerable().Where(x => x.IsPublished == true && x.IsPrimary == true); ;

            var finalNewses = newses.OrderByDescending(c => c.PublishedDate).Take(6)
                .Select(news => new NewsViewModel
                {
                    Id = news.Id,
                    Name = news.Name,
                    Description = news.Description,
                    PublishedDate = news.PublishedDate.HasValue ? news.PublishedDate.Value.ToString("dd/MM/yyyy") : null,
                    IsPublished = news.IsPublished,
                    ImageBase64 = _genericService.GetImgBase64(news.ImageUrl)
                });

            return finalNewses;
        }
    }
}

