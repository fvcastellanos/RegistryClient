using System.Collections.Generic;

namespace RegistryClient.Client.Model
{
    public class Catalog
    {
        public ICollection<string> Repositories { get; set; }
    }
}