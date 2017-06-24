using Orchard.ContentManagement;
using Orchard.ContentManagement.Metadata.Models;
using Orchard.Media.Fields;

namespace Orchard.Media.ViewModels
{
    public class EditMediaFieldViewModel
    {
        // A Jsong serialized array of media paths
        public string Paths { get; set; }
        public MediaField Field { get; set; }
        public ContentPart Part { get; set; }
        public ContentPartFieldDefinition PartFieldDefinition { get; set; }
    }
}
