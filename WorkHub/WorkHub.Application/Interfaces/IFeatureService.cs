using System;
using System.Collections.Generic;
using System.Text;
using WorkHub.Application.Dtos.Feature;

namespace WorkHub.Application.Interfaces
{
    public interface IFeatureService
    {
        Task<List<FeatureResponse>> GetAllAsync(int page=1,int pageSize=10);
        Task<FeatureResponse?> GetByIdAsync(Guid id); 

        Task<Guid> AddFeatureAsync(AddFeatureRequest addFeatureRequest);

        Task UpdateFeatureAsync(Guid id , UpdateFeatureRequest updateFeatureRequest);

        Task SoftDeleteAsync(Guid id); 
    }
}
