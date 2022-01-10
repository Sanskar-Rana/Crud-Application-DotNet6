
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            /*var objCategoryList = _db.Categories.ToList();*/
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly matched");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Added Successfully";
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
            var categoryFromDBFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
           /* var categoryFromDBSingle = _db.Categories.SingleOrDefault(u => u.Id == id);*/
            if (categoryFromDBFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDBFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly matched");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
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
            var categoryFromDBFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
/*            var categoryFromDBSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
*/            if (categoryFromDBFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDBFirst);
        }
        //POST
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken] //help to prevent cross side attack
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
         
            
        }
    }
}
