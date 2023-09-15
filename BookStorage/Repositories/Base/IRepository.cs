namespace BookStorage.Repositories.Base
{
    public interface IRepository
    {
        void Attach(IUnitOfWork unitOfWork);
        void BeginUnitOfWork();
        void Commit();
        void Rollback();
    }
}