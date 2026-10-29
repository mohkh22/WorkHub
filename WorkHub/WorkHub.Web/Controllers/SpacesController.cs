using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkHub.Application.Dtos.Space;
using WorkHub.Application.Interfaces;
using WorkHub.Domain.Enums;

namespace WorkHub.Web.Controllers
{
    [Route("[Controller]")]
    public class SpacesController : Controller
    {
        private readonly ISpaceService _spaceService;

        public SpacesController(ISpaceService spaceService)
        {
            _spaceService = spaceService;
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
        public IActionResult Create()
        {
            List<SelectListItem> selectLists = new List<SelectListItem>();

            foreach (var item in Enum.GetNames(typeof(SpaceType)))
            {
                selectLists.Add(new SelectListItem { Text = item, Value = item });
            }

            ViewBag.SpaceTypeSelectList = selectLists;

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddSpaceRequest request)
        {
            if(!ModelState.IsValid)
            {
                List<SelectListItem> selectLists = new List<SelectListItem>();

                foreach (var item in Enum.GetNames(typeof(SpaceType)))
                {
                    selectLists.Add(new SelectListItem { Text = item, Value = item });
                }

                ViewBag.SpaceTypeSelectList = selectLists;
                return View(request);
            }

            Guid spaceId = await _spaceService.AddSpaceAsync(request);

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
            var space = await _spaceService.GetSpaceById(id);
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
            return View(space.ToUpdateSpaceRequest()); 

        }


        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id , UpdateSpaceRequest updateSpaceRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(updateSpaceRequest); 
            }
            await _spaceService.UpdateSpaceAsync(id, updateSpaceRequest);
            return RedirectToAction("Index"); 
        }
    }
}
