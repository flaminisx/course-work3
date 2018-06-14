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
    public class GameController : Controller
    {
        ITeamService teamService;
        IGameService gameService;
        IStadiumService stadiumService;

        public GameController(IGameService games, ITeamService teams, IStadiumService stadiums)
        {
            teamService = teams;
            gameService = games;
            stadiumService = stadiums;
        }
        // GET: Game
        public ActionResult Index(string sortBy)
        {
            IEnumerable<GameDTO> gameDTOs = gameService.GetGames();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GameDTO, GameViewModel>()).CreateMapper();
            var games = mapper.Map<IEnumerable<GameDTO>, List<GameViewModel>>(gameDTOs).AsQueryable();
            ViewBag.FirstTeamNameSort = sortBy == "FirstTeam" ? "FirstTeam desc" : "FirstTeam";
            ViewBag.SecondTeamNameSort = sortBy == "SecondTeam" ? "SecondTeam desc" : "SecondTeam";
            ViewBag.ResultSort = sortBy == "Result" ? "Result desc" : "Result";
            switch (sortBy)
            {
                case "FirstTeam desc":
                    games = games.OrderByDescending(s => s.FirstTeamName);
                    break;
                case "FirstTeam":
                    games = games.OrderBy(s => s.FirstTeamName);
                    break;
                case "SecondTeam desc":
                    games = games.OrderByDescending(s => s.SecondTeamName);
                    break;
                case "SecondTeam":
                    games = games.OrderBy(s => s.SecondTeamName);
                    break;
                case "Result desc":
                    games = games.OrderByDescending(s => (s.FirstTeamResult > s.SecondTeamResult));
                    break;
                case "Result":
                    games = games.OrderBy(s => (s.FirstTeamResult > s.SecondTeamResult));
                    break;
                default:
                    break;
            }
            return View(games);
        }

        public ActionResult Create(int? id)
        {
            try
            {
                var game = new GameViewModel();
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ViewBag.Stadiums = new SelectList(stadiumService.GetStadiums(), "Id", "Name");
                return View(game);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create(GameViewModel game)
        {
            try
            {
                var gameDTO = new GameDTO
                {
                    FirstTeamId = game.FirstTeamId,
                    SecondTeamId = game.SecondTeamId,
                    StadiumId = game.StadiumId,
                    FirstTeamResult = game.FirstTeamResult,
                    SecondTeamResult = game.SecondTeamResult
                };
                gameService.Create(gameDTO);
                return RedirectToAction("Show");
            }
            catch (ValidationException ex)
            {
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ViewBag.Stadiums = new SelectList(stadiumService.GetStadiums(), "Id", "Name");
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(game);
        }

        public ActionResult Show(int? id)
        {
            try
            {
                var game = gameService.GetGame(id);
                return View(new GameViewModel
                {
                    Id = game.Id,
                    FirstTeamName = game.FirstTeamName,
                    SecondTeamName = game.SecondTeamName,
                    FirstTeamResult = game.FirstTeamResult,
                    SecondTeamResult = game.SecondTeamResult,
                    FirstTeamId = game.FirstTeamId,
                    SecondTeamId = game.SecondTeamId,
                    StadiumId = game.StadiumId,
                    StadiumName = game.StadiumName
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
                var game = gameService.GetGame(id);
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ViewBag.Stadiums = new SelectList(stadiumService.GetStadiums(), "Id", "Name");
                return View(new GameViewModel
                {
                    Id = game.Id,
                    FirstTeamName = game.FirstTeamName,
                    SecondTeamName = game.SecondTeamName,
                    FirstTeamResult = game.FirstTeamResult,
                    SecondTeamResult = game.SecondTeamResult,
                    FirstTeamId = game.FirstTeamId,
                    SecondTeamId = game.SecondTeamId,
                    StadiumId = game.StadiumId,
                    StadiumName = game.StadiumName
                });
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(GameViewModel game)
        {
            try
            {
                var gameDTO = new GameDTO
                {
                    Id = game.Id,
                    FirstTeamId = game.FirstTeamId,
                    SecondTeamId = game.SecondTeamId,
                    StadiumId = game.StadiumId,
                    FirstTeamResult = game.FirstTeamResult,
                    SecondTeamResult = game.SecondTeamResult
                };
                gameService.UpdateGame(gameDTO);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Teams = new SelectList(teamService.GetTeams(), "Id", "Name");
                ViewBag.Stadiums = new SelectList(stadiumService.GetStadiums(), "Id", "Name");
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return View(game);
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var gameDTO = gameService.GetGame(id);
                GameViewModel game = new GameViewModel
                {
                    Id = gameDTO.Id,
                    FirstTeamName = gameDTO.FirstTeamName,
                    SecondTeamName = gameDTO.SecondTeamName,
                    FirstTeamResult = gameDTO.FirstTeamResult,
                    SecondTeamResult = gameDTO.SecondTeamResult,
                    FirstTeamId = gameDTO.FirstTeamId,
                    SecondTeamId = gameDTO.SecondTeamId,
                    StadiumId = gameDTO.StadiumId,
                    StadiumName = gameDTO.StadiumName
                };
                return View(game);
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
                gameService.DeleteGame(id);
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
            gameService.Dispose();
            stadiumService.Dispose();
            base.Dispose(disposing);
        }
    }
}