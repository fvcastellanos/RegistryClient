using System.Collections.Generic;

namespace RegistryClient.Client.Model
{
    public class ImageTags
    {
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}