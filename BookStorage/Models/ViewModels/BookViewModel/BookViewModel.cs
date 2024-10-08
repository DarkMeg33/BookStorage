﻿using BookStorage.Helpers.Formatter;
using BookStorage.Models.Entities.BookEntities;

namespace BookStorage.Models.ViewModels.BookViewModel
{
    public class BookViewModel
    {
        public int? BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public string CoverUrl { get; }
        public decimal? Price { get; set; }
        public bool IsBought { get; set; }

        public BookViewModel(RetrieveBookEntity entity)
        {
            BookId = entity.BookId;
            Title = entity.Title;
            Description = entity.Description;
            AuthorId = entity.AuthorId;
            AuthorName = entity.AuthorName;
            CoverUrl = StringFormatter.ToBookCoverUrl(entity.CoverStorageReference);
            AuthorAvatarUrl = StringFormatter.ToAvatarUrl(entity.AuthorAvatarStorageReference);
            Price = entity.Price;
            IsBought = entity.IsBought;
        }
    }
}