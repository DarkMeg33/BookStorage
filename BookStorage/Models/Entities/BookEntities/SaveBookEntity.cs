using BookStorage.Models.Dto.BookDto;
using BookStorage.Models.ViewModels.BookViewModel;

namespace BookStorage.Models.Entities.BookEntities
{
    public class SaveBookEntity : BookEntity
    {
        public SaveBookEntity(FormBookViewModel vm, int currentUserId)
        {
            Title = vm.Title;
            Description = vm.Description;
            AuthorId = currentUserId;
        }
    }
}