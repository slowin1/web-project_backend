using eUseControl.Domain.Entities.services;

namespace eUseControl.Domain.Entities.User;

public class UserData
{
   
        public string Id { get; set; } = string.Empty;
        //имя фамилия 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        //ник пользователя
        public string UserName { get; set; } = string.Empty;
        //данные пользователя
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; }  = string.Empty;
        //время регистрации 
        public DateTime RegisteredOn { get; set; }
        
        public ICollection<ServiceBookingData> ServiceBookingData { get; set; } = new List<ServiceBookingData>();
    
}