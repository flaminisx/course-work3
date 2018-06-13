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
    public class HomeController : Controller
    {
        ITeamService teamService;

        public HomeController(ITeamService service)
        {
            teamService = service;
        }

        public ActionResult Index()
        {
            IEnumerable<TeamDTO> teamDTOs = teamService.GetTeams();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeamDTO, TeamViewModel>()).CreateMapper();
            var teams = mapper.Map<IEnumerable<TeamDTO>, List<TeamViewModel>>(teamDTOs);
            return View(teams);
        }

        public ActionResult MakeOrder(int? id)
        {
            try
            {
                var team = new TeamViewModel();

                return View(team);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakeOrder(TeamViewModel team)
        {
            try
            {
                var teamDTO = new TeamDTO { Name = team.Name };
                teamService.Create(teamDTO);
                return Content("<h2>Ваш заказ успешно оформлен</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(team);
        }
        protected override void Dispose(bool disposing)
        {
            teamService.Dispose();
            base.Dispose(disposing);
        }
    }
}