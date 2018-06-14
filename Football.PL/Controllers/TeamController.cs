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
        IPlayerService playerService;

        public TeamController(ITeamService teams, IPlayerService players)
        {
            teamService = teams;
            playerService = players;

        }
        // GET: Team
        public ActionResult Index(string sortBy)
        {
            IEnumerable<TeamDTO> teamDTOs = teamService.GetTeams();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TeamDTO, TeamViewModel>()).CreateMapper();
            var teams = mapper.Map<IEnumerable<TeamDTO>, List<TeamViewModel>>(teamDTOs).AsQueryable();
            ViewBag.NameSort = sortBy == "Name" ? "Name desc" : "Name";
            switch (sortBy)
            {
                case "Name desc":
                    teams = teams.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    teams = teams.OrderBy(s => s.Name);
                    break;
                default:
                    break;
            }
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

        public ActionResult Show(int? id)
        {
            try
            {
                var team = teamService.GetTeam(id);
                ViewBag.Players = playerService.GetPlayersByTeam(id);
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
            playerService.Dispose();
            base.Dispose(disposing);
        }
    }
}