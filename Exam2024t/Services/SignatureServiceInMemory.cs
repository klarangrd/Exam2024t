using Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public class SignatureServiceInMemory : ISignatureService
    {
        private static List<Signature> Signatures = new List<Signature>()
        {
            new Signature { SignatureID= 1, ApplicationID= 1, Signed = false, SignatureDate = DateTime.Now},
        };

        public Task Add(Signature signature)
        {
            Task t = new Task(() => Signatures.Add( signature));
            t.Start();
            return t;
        }

        public Task<Signature[]> GetAllSignatures()
        {
            Task<Signature[]> t = new Task<Signature[]>(() => Signatures.ToArray());
            t.Start();
            return t;
        }
    }
}
