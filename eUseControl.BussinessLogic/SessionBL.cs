using eUseControl.BussinessLogic.Core;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain;

namespace eUseControl.BussinessLogic;

public class SessionBL : UserApi, ISession 
{
    public ULoginResp UserLogin(ULoginData data)
    {
        // логика проверки логина
        return new ULoginResp { Status = false, StatusMsg = "Not implemented" };
    }
}
