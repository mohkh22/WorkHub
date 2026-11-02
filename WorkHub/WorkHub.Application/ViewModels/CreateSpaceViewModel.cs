using WorkHub.Application.Dtos.Feature;
using WorkHub.Application.Dtos.Space;

namespace WorkHub.Application.ViewModels
{
    public class CreateSpaceViewModel
    {
        public AddSpaceRequest AddSpaceRequest { get; set; } = new AddSpaceRequest();

        public List<FeatureResponse> FeatureResponses { get; set; } = new List<FeatureResponse>();
    }

}
