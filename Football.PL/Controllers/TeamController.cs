using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Football.PL.Models;
using Football.BLL.Interfaces;
using Football.BLL.Infrastructure;
using Football.BLL.DTO;
using AutoMapper;

namespace Football.PL.Controllers
{
    public class TeamController : Controller
    {
        ITeamService teamService;

        public TeamController(ITeamService service)
        {
            teamService = service;
        }
        // GET: Team
        public ActionResult Index()
        {
            IEnumerable<TeamDTO> teamDTOs = teamService.GetTeams();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeamDTO, TeamViewModel>()).CreateMapper();
            var teams = mapper.Map<IEnumerable<TeamDTO>, List<TeamViewModel>>(teamDTOs);
            return View(teams);
        }

        public ActionResult Create(int? id)
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
        public ActionResult Create(TeamViewModel team)
        {
            try
            {
                var teamDTO = new TeamDTO { Name = team.Name };
                teamService.Create(teamDTO);
                return Content("<h2>Команда создана</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(team);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                var team = teamService.GetTeam(id);

                return View(new TeamViewModel
                {
                    Id = team.Id,
                    Name = team.Name
                });
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(TeamViewModel team)
        {
            try
            {
                var teamDTO = new TeamDTO { Id = team.Id, Name = team.Name };
                teamService.UpdateTeam(teamDTO);
                return Content("<h2>Команда отредактирована</h2>");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(team);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                TeamViewModel client = new TeamViewModel();
                var clientDTO = teamService.GetTeam(id);
                client.Name = clientDTO.Name;
                return View(client);
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
                teamService.DeleteTeam(id);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            teamService.Dispose();
            base.Dispose(disposing);
        }
    }
}