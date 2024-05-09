using Core;
using Core.Models;

namespace Serverapi.Repositories
{
    public interface Iapplycationrepository
    {
        //Tildeler item en unik id og tilføjer den.
        void AddItem(Application item);

        // Fjerner item, hvor item.Id = id. Hvis den ikke
        // findes sker ingenting
        void DeleteById(int id);

        List<Application> GetAll();

    }
}
