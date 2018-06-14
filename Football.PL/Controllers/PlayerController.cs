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
    public class PlayerController : Controller
    {
        IPlayerService playerService;
        ITeamService teamService;

        public PlayerController(IPlayerService players, ITeamService teams)
        {
            playerService = players;
            teamService = teams;

        }
        // GET: Player
        public ActionResult Index(string sortBy, string searching)
        {
            IEnumerable<PlayerDTO> playerDTOs = playerService.GetPlayers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PlayerDTO, PlayerViewModel>()).CreateMapper();
            var players = mapper.Map<IEnumerable<PlayerDTO>, List<PlayerViewModel>>(playerDTOs).AsQueryable();
            ViewBag.SurnameSort = sortBy == "Surname" ? "Surname desc" : "Surname";
            ViewBag.NameSort = sortBy == "Name" ? "Name desc" : "Name";
            switch (sortBy)
            {
                case "Surname desc":
                    players = players.OrderByDescending(s => s.Surname);
                    break;
                case "Surname":
                    players = players.OrderBy(s => s.Surname);
                    break;
                case "Name desc":
                    players = players.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    players = players.OrderBy(s => s.Name);
                    break;
                default:
                    break;
            }
            if (!String.IsNullOrEmpty(searching))
            {
                players = players.Where(a => a.Name.Contains(searching) || a.Surname.Contains(searching));
            }
            return View(players);
        }

        [HttpPost]
        public ActionResult Create(PlayerViewModel player)
        {
            try
            {
                var playerDto = new PlayerDTO { Name = player.Name, Surname = player.Surname, TeamId = player.TeamId };
                playerService.Create(playerDto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(player);
        }

        public ActionResult Create(int? id)
        {
            try
            {
                var player = new PlayerViewModel();
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                return View(player);
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
                var player = playerService.GetPlayer(id);
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                return View(new PlayerViewModel
                {
                    Id = player.Id,
                    Name = player.Name,
                    Surname = player.Surname,
                    TeamId = player.TeamId
                });
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(PlayerViewModel player)
        {
            try
            {
                var playerDTO = new PlayerDTO { Id = player.Id, Name = player.Name, Surname = player.Surname, TeamId = player.TeamId };
                playerService.UpdatePlayer(playerDTO);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(player);
        }


        public ActionResult Delete(int? id)
        {
            try
            {
                PlayerViewModel player = new PlayerViewModel();
                var playerDTO = playerService.GetPlayer(id);
                player.Name = playerDTO.Name;
                player.Surname = playerDTO.Surname;
                return View(player);
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
                playerService.DeletePlayer(id);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

    }
}