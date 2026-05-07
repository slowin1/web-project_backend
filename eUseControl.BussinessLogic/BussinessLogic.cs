using eUseControl.BussinessLogic.Interfaces;

namespace eUseControl.BussinessLogic;

public class BussinessLogic
{
    public ISession GetSessionBL()
    {
        return new SessionBL();
    }
    
}