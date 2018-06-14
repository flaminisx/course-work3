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
        public ActionResult Index()
        {
            IEnumerable<PlayerDTO> playerDTOs = playerService.GetPlayers();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PlayerDTO, PlayerViewModel>()).CreateMapper();
            var players = mapper.Map<IEnumerable<PlayerDTO>, List<PlayerViewModel>>(playerDTOs);
            return View(players);
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
    }
}