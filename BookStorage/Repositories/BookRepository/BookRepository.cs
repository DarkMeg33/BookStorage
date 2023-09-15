﻿using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.BookRepository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public async Task<List<RetrieveBookEntity>> GetBooksAsync()
        {
            return new List<RetrieveBookEntity>();
        }
    }
}