using Microsoft.AspNetCore.Mvc;
using WorkHub.Application.Dtos.Feature;
using WorkHub.Application.Interfaces;

namespace WorkHub.Web.Controllers
{
    [Route("[Controller]")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Index(int page =1, int pageSize=10)
        {
            var features = await _featureService.GetAllAsync(page,pageSize); 
            return View(features);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            return View(); 
        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddFeatureRequest addFeature)
        {
            if (!ModelState.IsValid)
            {
                return View(addFeature); 
            }
            var result = await _featureService.AddFeatureAsync(addFeature); 

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Update(Guid id)
        {
            var feature = await _featureService.GetByIdAsync(id);
            ViewBag.Id = id; 
            return View(feature?.ToUpdateFeatureRequest());
        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id , UpdateFeatureRequest updateFeature)
        {
            if (!ModelState.IsValid)
                return View(updateFeature); 

            await _featureService.UpdateFeatureAsync(id, updateFeature);
            return RedirectToAction("Index"); 
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var feature = await _featureService.GetByIdAsync(id);
            ViewBag.Id = id;
            return View(feature);
        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _featureService.SoftDeleteAsync(id);
            return RedirectToAction("Index");
        }




    }
}
