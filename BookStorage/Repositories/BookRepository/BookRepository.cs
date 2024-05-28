using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;
using System.Net;

namespace BookStorage.Repositories.BookRepository
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        #region Book

        public async Task<List<RetrieveBookEntity>> GetBooksAsync(int currentUserId)
        {
            return await GetAllAsync<RetrieveBookEntity>("Book_Select", new { currentUserId });
        }

        public async Task<RetrieveBookEntity> GetBookAsync(int bookId, int currentUserId)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Select", new { bookId, currentUserId });
        }

        public async Task<RetrieveBookEntity> GetBookAsync(string title, int currentUserId)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Select", new { title, currentUserId });
        }

        public async Task<RetrieveBookEntity> UpsertBookAsync(SaveBookEntity entity)
        {
            return await GetAsync<RetrieveBookEntity>("Book_Upsert", entity);
        }

        #endregion

        #region BookCover

        public async Task<bool> UpdateBookCoverAsync(int bookId, string storageReference)
        {
            return await ExecuteAsync("BookCover_Update", new { bookId, storageReference });
        }

        #endregion

        #region BoughtBook

        public async Task<bool> InsertUserBoughtBookAsync(int bookId, int userId)
        {
            return await ExecuteAsync("UserBoughtBook_Insert", new { bookId, userId });
        }

        #endregion
    }
}