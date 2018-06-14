using System.Collections.Generic;
using Football.BLL.DTO;
using Football.BLL.Interfaces;
using Football.DAL.Interfaces;
using Football.DAL.Entities;
using Football.BLL.Infrastructure;
using AutoMapper;

namespace Football.BLL.Services
{
    public class StadiumService : IStadiumService
    {
        IUnitOfWork Database { get; set; }

        public StadiumService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Create(StadiumDTO stadiumDto)
        {
            if (stadiumDto.Name == null || stadiumDto.Name.Length < 1)
                throw new ValidationException("Имя стадиона должно состоять минимум из одного символа", "Name");
            var stadium = new Stadium { Name = stadiumDto.Name };
            Database.Stadiums.Create(stadium);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public StadiumDTO GetStadium(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id стадиона", "id");
            var stadium = Database.Stadiums.Get(id.Value);
            if (stadium == null)
                throw new ValidationException("Стадион не найден", "");
            return new StadiumDTO { Id = stadium.Id, Name = stadium.Name };
        }

        public IEnumerable<StadiumDTO> GetStadiums()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Stadium, StadiumDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Stadium>, List<StadiumDTO>>(Database.Stadiums.GetAll());
        }

        public void DeleteStadium(int? id)
        {
            if (id == null)
                throw new ValidationException("Нет Id стадиона", "id");
            Database.Stadiums.Delete(id.Value);
            Database.Save();
        }
    }
}
