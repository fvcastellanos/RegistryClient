using System.ComponentModel.DataAnnotations;

namespace RegistryClient.Models.Catalog
{
    public class DeleteTagRequestView
    {
        [Required]
        public string ImageName { get; set; }
        
        [Required]
        public string TagName { get; set; }
    }
}