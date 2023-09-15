namespace BookStorage.Repositories.Base
{
    public class UnitOfWorkRepository : IRepository
    {
        protected IUnitOfWork _unitOfWork;

        public void Attach(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void BeginUnitOfWork()
        {
            _unitOfWork.Begin();
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Rollback()
        {
            _unitOfWork.RollBack();
        }
    }
}