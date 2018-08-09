using System.Collections.Generic;

namespace RegistryClient.Models.Catalog
{
    public class TagsViewModel
    {
        public TagsViewModel(string imageName) 
        {
            ImageName = imageName;
        }
        
        public string ImageName { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}