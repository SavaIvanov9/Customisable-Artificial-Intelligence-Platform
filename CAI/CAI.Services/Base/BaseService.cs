namespace CAI.Services.Base
{
    using Data.Abstraction;

    public abstract class BaseService
    {
        private readonly IUnitOfWork _data;

        protected BaseService(IUnitOfWork data)
        {
            this._data = data;
        }

        protected IUnitOfWork Data { get => this._data;  }
    }
}
