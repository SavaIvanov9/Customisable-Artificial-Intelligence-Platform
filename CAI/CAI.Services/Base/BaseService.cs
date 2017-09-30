namespace CAI.Services.Base
{
    using System;
    using Data.Abstraction;

    public abstract class BaseService : IDisposable
    {
        private readonly IUnitOfWork _data;

        protected BaseService(IUnitOfWork data)
        {
            this._data = data;
        }

        protected IUnitOfWork Data { get => this._data;  }

        public void Dispose()
        {
            this._data.Dispose();
        }
    }
}
