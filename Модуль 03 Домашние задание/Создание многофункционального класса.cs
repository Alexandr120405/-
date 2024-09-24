public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

public class UserService
{
    public void SaveToDatabase(User user)
    {

    }
}

public class EmailService
{
    public void SendEmail(string email)
    {
       
    }
}

public class LabelPrinter
{
    public void PrintAddressLabel(string address)
    {
        
    }
}
