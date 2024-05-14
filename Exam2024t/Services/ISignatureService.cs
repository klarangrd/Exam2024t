using Core.Models;
using System.Threading.Tasks;

namespace Exam2024t.Services
{
    public interface ISignatureService
    {
        Task<Signature[]> GetAllSignatures();

        Task Add(Signature signature);
    }
}
