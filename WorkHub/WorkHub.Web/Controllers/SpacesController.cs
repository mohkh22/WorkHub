using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkHub.Application.Dtos.Space;
using WorkHub.Application.Interfaces;
using WorkHub.Application.ViewModels;
using WorkHub.Domain.Entities;
using WorkHub.Domain.Enums;

namespace WorkHub.Web.Controllers
{
    [Route("[Controller]")]
    public class SpacesController : Controller
    {
        private readonly ISpaceService _spaceService;
        private readonly IFeatureService _featureService;

        public SpacesController(ISpaceService spaceService, IFeatureService featureService)
        {
            _spaceService = spaceService;
            _featureService = featureService;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Index(int page =1, int pageSize=10)
        {
            List<SpaceResponse> spaces = await _spaceService.GetAllSpacesAsync(page,pageSize); 
            return View(spaces);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> selectLists = new List<SelectListItem>();

            foreach (var item in Enum.GetNames(typeof(SpaceType)))
            {
                selectLists.Add(new SelectListItem { Text = item, Value = item });
            }

            ViewBag.SpaceTypeSelectList = selectLists;

            CreateSpaceViewModel createSpaceViewModel = new CreateSpaceViewModel();
            createSpaceViewModel.FeatureResponses = await _featureService.GetAllAsync(); 

            return View(createSpaceViewModel);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpaceViewModel request)
        {
            if(!ModelState.IsValid)
            {
                List<SelectListItem> selectLists = new List<SelectListItem>();

                foreach (var item in Enum.GetNames(typeof(SpaceType)))
                {
                    selectLists.Add(new SelectListItem { Text = item, Value = item });
                }

                ViewBag.SpaceTypeSelectList = selectLists;
                request.FeatureResponses = await _featureService.GetAllAsync();
                return View(request);
            }

            await _spaceService.AddSpaceAsync(request.AddSpaceRequest);


            return RedirectToAction("Index"); 
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var space = await _spaceService.GetSpaceById(id); 

            return View(space); 
        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _spaceService.SoftDeleteAsync(id);
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Update(Guid id)
        { 
            var space = await _spaceService.GetSpaceById(id, nameof(Space.SpaceFeatures));
            List<SelectListItem> selectLists = new List<SelectListItem>();

            foreach (var item in Enum.GetNames(typeof(SpaceType)))
            {
                if (item == space.Type.ToString())
                {
                    selectLists.Add(new SelectListItem { Text = item, Value = item, Selected = true });

                }
                else
                {
                    selectLists.Add(new SelectListItem { Text = item, Value = item });

                }
            }
            ViewBag.SpaceTypeSelectList = selectLists;
            ViewBag.SpaceId = id; 

            UpdateSpaceViewModel updateSpaceViewModel = new UpdateSpaceViewModel();
            updateSpaceViewModel.UpdateSpaceRequest = space.ToUpdateSpaceRequest();
            updateSpaceViewModel.FeatureResponses = await _featureService.GetAllAsync();

            return View(updateSpaceViewModel); 

        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id , UpdateSpaceViewModel updateSpaceViewModel)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> selectLists = new List<SelectListItem>();

                foreach (var item in Enum.GetNames(typeof(SpaceType)))
                {
                    if (item == updateSpaceViewModel.UpdateSpaceRequest.Type.ToString())
                    {
                        selectLists.Add(new SelectListItem { Text = item, Value = item, Selected = true });

                    }
                    else
                    {
                        selectLists.Add(new SelectListItem { Text = item, Value = item });

                    }
                }
                updateSpaceViewModel.FeatureResponses = await _featureService.GetAllAsync();
                ViewBag.SpaceTypeSelectList = selectLists;
                return View(updateSpaceViewModel); 
            }
            await _spaceService.UpdateSpaceAsync(id, updateSpaceViewModel.UpdateSpaceRequest);
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Details(Guid id)
        {
            var space = await _spaceService.GetSpaceById(id, "SpaceFeatures.Feature");
            return View(space);
        }
    }
}
