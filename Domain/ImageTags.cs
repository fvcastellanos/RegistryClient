using System.Collections.Generic;

namespace RegistryClient.Domain
{
    public class ImageTags
    {
        public string ImageName { get; set; }
        public IEnumerable<string> TagList { get; set; }
    }
}