using eUseControl.Domain;

namespace eUseControl.BussinessLogic.Interfaces;

public interface ISession
{
    ULoginResp UserLogin(ULoginData data);
}
