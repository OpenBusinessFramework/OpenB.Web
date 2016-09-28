using OpenB.Core.ACL;
using OpenB.Web.Content.Elements;

namespace OpenB.Web.View
{
    

    public interface IViewModel
    {
        

    }

    public abstract class BaseViewModel
    {
        protected BaseViewModel(User user)
        {
            if (user == null)
                throw new System.ArgumentNullException(nameof(user));
        }        
    }   
}