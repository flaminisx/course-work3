using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Football.BLL.Services;
using Football.BLL.Interfaces;
using Football.BLL.Infrastructure;
using Football.BLL.DTO;
using AutoMapper;
using Football.PL.Models;

namespace Football.PL.Controllers
{
    public class StadiumController : Controller
    {
        IStadiumService stadiumService;

        public StadiumController(IStadiumService service)
        {
            stadiumService = service;
        }
        // GET: Stadium
        public ActionResult Index(string sortBy)
        {
            IEnumerable<StadiumDTO> stadiumDTOs = stadiumService.GetStadiums();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<StadiumDTO, StadiumViewModel>()).CreateMapper();
            var stadiums = mapper.Map<IEnumerable<StadiumDTO>, List<StadiumViewModel>>(stadiumDTOs).AsQueryable();
            ViewBag.NameSort = sortBy == "Name" ? "Name desc" : "Name";
            switch (sortBy)
            {
                case "Name desc":
                    stadiums = stadiums.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    stadiums = stadiums.OrderBy(s => s.Name);
                    break;
                default:
                    break;
            }
            return View(stadiums);
        }

        public ActionResult Create(int? id)
        {
            try
            {
                var stadium = new StadiumViewModel();

                return View(stadium);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create(StadiumViewModel stadium)
        {
            try
            {
                var stadiumDTO = new StadiumDTO { Name = stadium.Name };
                stadiumService.Create(stadiumDTO);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(stadium);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                StadiumViewModel stadium = new StadiumViewModel();
                var stadiumDTO = stadiumService.GetStadium(id);
                stadium.Name = stadiumDTO.Name;
                return View(stadium);
            }
            catch (ValidationException e)
            {
                return Content("Not Found");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            try
            {
                stadiumService.DeleteStadium(id);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}