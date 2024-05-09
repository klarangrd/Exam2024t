namespace Core.Models;

public class Signature
{
    public int SignatureID { get; set; }
    
    public int ApplicationID { get; set; }
    
    public bool Signed { get; set; }
    
    public DateTime SignatureDate { get; set; }
    
    public string DocumentPath { get; set; }
}