using WorkHub.Application.Dtos.Feature;
using WorkHub.Application.Dtos.Space;

namespace WorkHub.Application.ViewModels
{
    public class UpdateSpaceViewModel
    {
        public UpdateSpaceRequest UpdateSpaceRequest { get; set; } = new UpdateSpaceRequest();
        public List<FeatureResponse> FeatureResponses { get; set; } = new List<FeatureResponse>();

    }
}
