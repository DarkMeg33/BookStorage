using BookStorage.Models.Entities.BookEntities;
using BookStorage.Repositories.Base;

namespace BookStorage.Repositories.BookRepository
{
    public interface IBookRepository : IRepository
    {
        #region Book

        Task<List<RetrieveBookEntity>> GetBooksAsync(int currentUserId);
        Task<RetrieveBookEntity> GetBookAsync(int bookId, int currentUserId);
        Task<RetrieveBookEntity> GetBookAsync(string title, int currentUserId);
        Task<RetrieveBookEntity> UpsertBookAsync(SaveBookEntity entity);

        #endregion

        #region BookCover

        Task<bool> UpdateBookCoverAsync(int bookId, string storageReference);

        #endregion

        #region BoughtBook

        Task<bool> InsertUserBoughtBookAsync(int bookId, int userId);

        #endregion


    }
}