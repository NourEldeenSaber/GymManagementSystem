using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        #region GetAllTrainers
        public IActionResult Index()
        {
            var Trainers = _trainerService.GetAllTrainers();
            return View(Trainers);
        }

        #endregion

        #region Create Trainer
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check missing Fields");
                return View(nameof(Create), model);
            }
            var Result = _trainerService.CreateTrainer(model);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Added Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Create";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Trainer Details

        public IActionResult Details(int id)
        {
            if(id<= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if(trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        #endregion

        #region Edit Trainer

        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerToUpdate(id);
            if(trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id , TrainerToUpdateViewModel model) {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Fields");
                return View(model);
            }
            var result = _trainerService.UpdateTrainerDetails(model, id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Updated Successfully!!";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Update ";

            }
                return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete Trainer

        public IActionResult Delete(int id) {
            if (id <= 0) {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if(trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = trainer.Id;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id";
                return RedirectToAction(nameof(Index));
            }
            var result =_trainerService.RemoveTrainer(id);
            if (result) {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to Delete";

            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
