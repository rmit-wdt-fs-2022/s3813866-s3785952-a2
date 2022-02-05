namespace Assignment2.DTO;

public class CustomerDTO
{
    
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string address { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public List<AccountDTO> Accounts { get; set; }
    public LoginDTO Login { get; set; }
    

    
}