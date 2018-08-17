using System.ComponentModel.Design.Serialization;

namespace RegistryClient.Controllers
{
    public class Routes
    {
        public const string Root = "/RegistryUI";

        // Static content
        public const string Css = Root + "/css";
        public const string Js = Root + "/js";
        public const string Images = Root + "/images";
        
        // Controllers
        public const string Catalog = Root + "/Catalog";
        public const string Settings = Root + "/Settings";
        
        // Actions
        public const string DeleteTag = Catalog + "/DeleteTag";
    }
}