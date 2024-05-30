namespace Core.Models;

public class Signature
{
    public int SignatureID { get; set; }


    public bool Signed { get; set; } 
    
    public DateTime SignatureDate { get; set; }
    
    public byte[] Sign { get; set; }
}