
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            /*var objCategoryList = _db.Categories.ToList();*/
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult Create(CoverType obj)
        {
           

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Added Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /*var categoryFromDB = _db.Categories.Find(id);*/
            var coverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
           /* var categoryFromDBSingle = _db.Categories.SingleOrDefault(u => u.Id == id);*/
            if (coverTypeFromDBFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDBFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult Edit(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            /* var categoryFromDB = _db.Categories.Find(id);*/
            var coverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
/*            var categoryFromDBSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
*/            if (coverTypeFromDBFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDBFirst);
        }
        //POST
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
                TempData["success"] = "CoverType Deleted Successfully";
                return RedirectToAction("Index");
         
            
        }
    }
}
