using OpenB.Core;
using System;

namespace OpenB.Web.View
{

    public class BaseViewModel<TModel> where TModel : IModel
    {
        IRepository<TModel> modelRepository;

        private BaseViewModel()
        {
           // modelRepository = RepositoryManager.GetRepository<TModel>();
        }

        private BaseViewModel(TModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

           
        }

        public ICommand CreateOrUpdate { get; set; }
        
    }
}