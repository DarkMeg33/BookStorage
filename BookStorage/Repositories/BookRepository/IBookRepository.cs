﻿using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.BookRepository
{
    public interface IBookRepository : IRepository
    {
        Task<List<RetrieveBookEntity>> GetBooksAsync();
    }
}