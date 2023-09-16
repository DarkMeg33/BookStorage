﻿using System.Data;

namespace BookStorage.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin(bool isAtomic = false);
        void Commit();
        void RollBack();

        Task<IEnumerable<T>> GetAllAsync<T>(string query, object parameter = null,
            CommandType commandType = CommandType.StoredProcedure);
    }
}